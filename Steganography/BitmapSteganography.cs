using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Steganography.Options;

namespace Steganography
{
    public class BitmapSteganography
    {
        private const int BitsPerByte = 8;

        public CvBitmap Pack(BbsOptions bbsOptions)
        {
            string steganographyKey = bbsOptions.Key;
            int expandSize = bbsOptions.ExpandSize;
            int alpha = bbsOptions.Alpha;
            string text = bbsOptions.RtfText;
            bool autoResize = bbsOptions.SampleAutoresize;
            bool maximumGamma = bbsOptions.MaximumGamma;
            int politicIndex = bbsOptions.PoliticIndex;
            string politicText = bbsOptions.PoliticText;
            int mixerIndex = bbsOptions.MixerIndex;
            int gammaIndex = bbsOptions.GammaIndex;
            int archiverIndex = bbsOptions.ArchiverIndex;
            int pixelFormatIndex = bbsOptions.PixelFormatIndex;
            CvBitmap sampleBitmap = bbsOptions.SampleBitmap;

            var decompressed = new MemoryStream(Encoding.Default.GetBytes(text));
            var compressed = new MemoryStream();
            var enveloped = new MemoryStream();
            var output = new MemoryStream();

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);
            output.Seek(0, SeekOrigin.Begin);

            new Archiver(archiverIndex).Compress(decompressed, compressed);

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);
            output.Seek(0, SeekOrigin.Begin);

            new SequenceIndicator().Envelop(compressed, enveloped);

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);
            output.Seek(0, SeekOrigin.Begin);

            int count = (int) enveloped.Length*expandSize*BitsPerByte; // Требуемое число пикселей
            var outputBitmap = new CvBitmap(sampleBitmap, count, pixelFormatIndex, autoResize);

            // для каждого бита сообщения нужно N байт носителя
            if (enveloped.Length*expandSize*BitsPerByte > outputBitmap.Length)
                throw new Exception(string.Format("Размер изображения недостаточен для сохранения данных {0}/{1}",
                    enveloped.Length*expandSize*BitsPerByte, outputBitmap.Length));

            var politic = new Politic(politicIndex, outputBitmap, expandSize, politicText);
            politic.Fill(enveloped, output);

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);
            output.Seek(0, SeekOrigin.Begin);

            byte[] data = output.ToArray();

            int[] index = new Mixer(mixerIndex, steganographyKey).GetIndeces(outputBitmap.Length);
            byte[] gamma = new Gamma(gammaIndex, steganographyKey).GetGamma(maximumGamma
                ? ((outputBitmap.Length + BitsPerByte - 1)/BitsPerByte)
                : ((expandSize + BitsPerByte - 1)/BitsPerByte));
            byte[] colors = outputBitmap.Select(index);
            byte[] cw = new BbSignals(expandSize, maximumGamma).EncodeAndCombine(colors, data, gamma, (byte) alpha);
            outputBitmap.Replace(index, cw);
            return bbsOptions.OutputBitmap = outputBitmap;
        }

        public string Unpack(BbsOptions bbsOptions)
        {
            string steganographyKey = bbsOptions.Key;
            int expandSize = bbsOptions.ExpandSize;
            int filterStep = bbsOptions.FilterStep;
            int mixerIndex = bbsOptions.MixerIndex;
            int gammaIndex = bbsOptions.GammaIndex;
            int archiverIndex = bbsOptions.ArchiverIndex;
            bool maximumGamma = bbsOptions.MaximumGamma;

            CvBitmap inputBitmap = bbsOptions.InputBitmap;
            var medianBitmap = new CvBitmap(inputBitmap, filterStep);

            Debug.WriteLine(inputBitmap.Length);
            Debug.WriteLine(medianBitmap.Length);

            int[] index = new Mixer(mixerIndex, steganographyKey).GetIndeces(inputBitmap.Length);
            byte[] gamma = new Gamma(gammaIndex, steganographyKey).GetGamma(maximumGamma
                ? ((inputBitmap.Length + BitsPerByte - 1)/BitsPerByte)
                : ((expandSize + BitsPerByte - 1)/BitsPerByte));
            byte[] cw = inputBitmap.Select(index);
            byte[] median = medianBitmap.Select(index);
            byte[] data = new BbSignals(expandSize, maximumGamma).DecodeAndExtract(cw, gamma, median);

            var enveloped = new MemoryStream(data);
            var compressed = new MemoryStream();
            var decompressed = new MemoryStream();

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            new SequenceIndicator().Extract(enveloped, compressed);

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            new Archiver(archiverIndex).Decompress(compressed, decompressed);

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            bbsOptions.MedianBitmap = medianBitmap;
            return bbsOptions.RtfText = Encoding.Default.GetString(decompressed.ToArray());
        }
    }
}