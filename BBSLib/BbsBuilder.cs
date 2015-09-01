using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using BBSLib.Options;
using FFTTools;

namespace BBSLib
{
    /// <summary>
    ///     Класс алгоритма широкополосного сигнала
    /// </summary>
    public class BbsBuilder
    {
        private const int BitsPerByte = 8; // Количество битов в байте

        public CvBitmap Pack(BbsOptions bbsOptions)
        {
            Debug.WriteLine(bbsOptions.ToString());

            string key = bbsOptions.Key;
            int expandSize = bbsOptions.ExpandSize;
            int codeSize = bbsOptions.EccCodeSize;
            int dataSize = bbsOptions.EccDataSize;
            int alpha = bbsOptions.Alpha;
            bool autoResize = bbsOptions.AutoResize;
            bool autoAlpha = bbsOptions.AutoAlpha;
            bool maximumGamma = bbsOptions.MaximumGamma;
            int politicIndex = bbsOptions.PoliticIndex;
            string politicText = bbsOptions.PoliticText;
            int eccIndex = bbsOptions.EccIndex;
            int mixerIndex = bbsOptions.MixerIndex;
            int gammaIndex = bbsOptions.GammaIndex;
            int archiverIndex = bbsOptions.ArchiverIndex;
            int barcodeIndex = bbsOptions.BarcodeIndex;
            int pixelFormatIndex = bbsOptions.PixelFormatIndex;
            int filterStep = bbsOptions.FilterStep;

            CvBitmap sampleBitmap = bbsOptions.SampleBitmap;

            Size minSize = sampleBitmap.Image.Size;
            CvBitmap barcodeBitmap = null;
            try
            {
                using (var barcode = new Barcode(barcodeIndex)
                {
                    ArchiverIndex = archiverIndex,
                    EccIndex = eccIndex,
                    MixerIndex = mixerIndex,
                    GammaIndex = gammaIndex,
                    ExpandSize = expandSize,
                    EccCodeSize = codeSize,
                    EccDataSize = dataSize,
                    MaximumGamma = maximumGamma,
                    Key = key,
                })
                {
                    barcodeBitmap = new CvBitmap(barcode.Encode());
                    Size size = barcodeBitmap.Image.Size;
                    minSize.Width = Math.Max(minSize.Width, 4*size.Width);
                    minSize.Height = Math.Max(minSize.Height, 4*size.Height);
                }
            }
            catch (ArgumentNullException exception)
            {
            }


            byte[] bytes = Encoding.Default.GetBytes(bbsOptions.RtfText);
            Debug.WriteLine(string.Join("", bytes.Select(x => x.ToString("X02"))));

            IStreamTransform[] streamTransforms =
            {
                new Archiver(archiverIndex), // Алгоритм сжатия данных
                new Envelope(), // Добавление конверта
                new Ecc(eccIndex, codeSize, dataSize) // Алгоритм коррекции ошибок
            };
            var input = new MemoryStream(bytes);
            Debug.WriteLine("input {0}", input.Length);
            foreach (IStreamTransform transform in streamTransforms)
            {
                using (MemoryStream prev = input)
                    transform.Forward(prev, input = new MemoryStream());
                input.Seek(0, SeekOrigin.Begin);
                Debug.WriteLine("{0} {1}", transform, input.Length);
            }

            // Требуемое число пикселей
            long count = input.Length*expandSize*BitsPerByte;
            var formatedBitmap = new CvBitmap(sampleBitmap, count, pixelFormatIndex, autoResize, minSize);
            Size formatedSize = formatedBitmap.Image.Size;
            // для каждого бита сообщения нужно N байт носителя
            if (count > formatedBitmap.Length)
                throw new Exception(
                    string.Format("Размер изображения недостаточен для сохранения данных {0}/{1}",
                        count, formatedBitmap.Length));

            if (minSize.Width > formatedSize.Width || minSize.Height > formatedSize.Height)
                throw new Exception(
                    string.Format(
                        "Размер изображения недостаточен для сохранения данных {0}x{1}/{2}x{3}",
                        formatedSize.Width, formatedSize.Height, minSize.Width, minSize.Height));

            if (barcodeBitmap != null) formatedBitmap.DrawCopyright(barcodeBitmap);
            using (IStreamTransform streamTransform = new Politic(politicIndex, politicText, expandSize, formatedBitmap)
                )
            using (MemoryStream prev = input)
                streamTransform.Forward(prev, input = new MemoryStream());
            input.Seek(0, SeekOrigin.Begin);
            Debug.WriteLine("input {0}", input.Length);
            using (var reader = new BinaryReader(input))
            {
                byte[] data = reader.ReadBytes((int) input.Length);
                Debug.WriteLine(string.Join("", data.Select(x => x.ToString("X02"))));

                int length = formatedBitmap.Length;

                var index = new int[length];
                var colors = new byte[length];
                var cw = new byte[length];

                var gamma = new byte[maximumGamma
                    ? ((length + BitsPerByte - 1)/BitsPerByte)
                    : ((expandSize + BitsPerByte - 1)/BitsPerByte)];

                using (var builder = new Mixer(mixerIndex, key))
                    builder.GetInts(index);
                using (var builder = new Gamma(gammaIndex, key))
                    builder.GetBytes(gamma);
                formatedBitmap.Select(index, colors);

                using (var bbSignals = new BbSignals(expandSize, maximumGamma))
                    bbSignals.Combine(colors, data, gamma, (byte) alpha, cw);

                var outputBitmap = new CvBitmap(formatedBitmap, cw, index);
                if (!autoAlpha) return bbsOptions.OutputBitmap = outputBitmap;
                using (var builder = new BlurBuilder(filterStep))
                    for (;;)
                    {
                        var medianBitmap = new CvBitmap(outputBitmap, builder);
                        Debug.WriteLine(medianBitmap.ToString());
                        Debug.WriteLine(outputBitmap.ToString());
                        double average, average2;
                        double delta, delta2;
                        outputBitmap.AverageAndDelta(out average, out delta);
                        medianBitmap.AverageAndDelta(out average2, out delta2);
                        if (Math.Abs(average - average2) < alpha) break;
                        alpha = (int) Math.Ceiling(Math.Abs(average - average2));
                        using (var bbSignals = new BbSignals(expandSize, maximumGamma))
                            bbSignals.Combine(colors, data, gamma, (byte) alpha, cw);
                        outputBitmap = new CvBitmap(formatedBitmap, cw, index);
                        Debug.WriteLine("alpha {0}", alpha);
                    }
                return bbsOptions.OutputBitmap = outputBitmap;
            }
        }

        public string Unpack(BbsOptions bbsOptions)
        {
            Debug.WriteLine(bbsOptions.ToString());

            string key = bbsOptions.Key;
            int expandSize = bbsOptions.ExpandSize;
            int codeSize = bbsOptions.EccCodeSize;
            int dataSize = bbsOptions.EccDataSize;
            int filterStep = bbsOptions.FilterStep;
            int eccIndex = bbsOptions.EccIndex;
            int mixerIndex = bbsOptions.MixerIndex;
            int gammaIndex = bbsOptions.GammaIndex;
            int archiverIndex = bbsOptions.ArchiverIndex;
            bool maximumGamma = bbsOptions.MaximumGamma;
            bool extractBarcode = bbsOptions.ExtractBarcode;

            CvBitmap inputBitmap = bbsOptions.InputBitmap;

            if (extractBarcode)
                using (var barcode = new Barcode(inputBitmap.Image.Bitmap))
                {
                    barcode.Decode();
                    archiverIndex = barcode.ArchiverIndex;
                    eccIndex = barcode.EccIndex;
                    mixerIndex = barcode.MixerIndex;
                    gammaIndex = barcode.GammaIndex;
                    expandSize = barcode.ExpandSize;
                    codeSize = barcode.EccCodeSize;
                    dataSize = barcode.EccDataSize;
                    maximumGamma = barcode.MaximumGamma;
                    key = barcode.Key;
                }

            using (var builder = new BlurBuilder(filterStep))
                bbsOptions.MedianBitmap = new CvBitmap(inputBitmap, builder);
            CvBitmap medianBitmap = bbsOptions.MedianBitmap;

            int length = inputBitmap.Length;

            var index = new int[length];
            var cw = new byte[length];
            var median = new byte[length];
            var data = new byte[length/expandSize/BitsPerByte];

            using (var builder = new Mixer(mixerIndex, key))
                builder.GetInts(index);
            var gamma = new byte[maximumGamma
                ? ((length + BitsPerByte - 1)/BitsPerByte)
                : ((expandSize + BitsPerByte - 1)/BitsPerByte)];
            using (var builder = new Gamma(gammaIndex, key))
                builder.GetBytes(gamma);
            inputBitmap.Select(index, cw);
            medianBitmap.Select(index, median);

            using (var bbSignals = new BbSignals(expandSize, maximumGamma))
                bbSignals.Extract(cw, median, gamma, data);
            Debug.WriteLine(string.Join("", data.Select(x => x.ToString("X02"))));

            IStreamTransform[] streamTransforms =
            {
                new Ecc(eccIndex, codeSize, dataSize), // Алгоритм коррекции ошибок
                new Envelope(), // Извлечение из конверта
                new Archiver(archiverIndex) // Алгоритм извлечения из сжатого архива
            };

            var input = new MemoryStream(data);
            Debug.WriteLine("input {0}", input.Length);
            foreach (IStreamTransform transform in streamTransforms)
            {
                using (MemoryStream prev = input)
                    transform.Backward(prev, input = new MemoryStream());
                input.Seek(0, SeekOrigin.Begin);
                Debug.WriteLine("{0} {1}", transform, input.Length);
            }
            using (var reader = new BinaryReader(input))
            {
                byte[] bytes = reader.ReadBytes((int) input.Length);
                Debug.WriteLine(string.Join("", bytes.Select(x => x.ToString("X02"))));
                return bbsOptions.RtfText = Encoding.Default.GetString(bytes);
            }
        }
    }
}