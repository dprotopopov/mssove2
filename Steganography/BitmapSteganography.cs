using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Steganography
{
    public class BitmapSteganography
    {
        private const int BitsPerByte = 8;
        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        public SteganographyBitmap Packing(SteganographyOptions steganographyOptions)
        {
            string steganographyKey = steganographyOptions.SteganographyKey;
            int expandSize = steganographyOptions.ExpandSize;
            int alpha = steganographyOptions.Alpha;
            string text = steganographyOptions.Text;
            bool autoResize = steganographyOptions.SampleAutoresize;
            bool politicsNone = steganographyOptions.PoliticsNone;
            bool politicsZero = steganographyOptions.PoliticsZero;
            bool politicsRandom = steganographyOptions.PoliticsRandom;
            bool politicsFake = steganographyOptions.PoliticsFake;
            string politicsText = steganographyOptions.PoliticsText;
            int mixerIndex = steganographyOptions.MixerIndex;
            int gammaIndex = steganographyOptions.GammaIndex;
            int archiverIndex = steganographyOptions.ArchiverIndex;
            int pixelFormatIndex = steganographyOptions.PixelFormatIndex;
            SteganographyBitmap sampleBitmap = steganographyOptions.SampleBitmap;

            var decompressed = new MemoryStream(Encoding.Default.GetBytes(text));
            var compressed = new MemoryStream();
            var enveloped = new MemoryStream();

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            new Archiver(archiverIndex).Compress(decompressed, compressed);

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            new SequenceIndicator().Envelop(compressed, enveloped);

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            int count = (int) enveloped.Length*expandSize*BitsPerByte; // Требуемое число пикселей

            var outputBitmap = new SteganographyBitmap(sampleBitmap, count, pixelFormatIndex, autoResize);

            // для каждого бита сообщения нужно N байт носителя
            if (enveloped.Length*expandSize*BitsPerByte > outputBitmap.Length)
                throw new Exception("Размер изображения недостаточен для сохранения данных " +
                                    (enveloped.Length*expandSize*BitsPerByte) + "/" + outputBitmap.Length);

            var data = new byte[politicsNone ? enveloped.Length : (outputBitmap.Length/BitsPerByte/expandSize)];
            if (politicsZero) Array.Clear(data, 0, data.Length);
            if (politicsRandom) Rng.GetBytes(data);
            if (politicsFake)
            {
                if (string.IsNullOrWhiteSpace(politicsText)) throw new Exception("Отсутствует заполняющий текст");
                byte[] buffer = Encoding.Default.GetBytes(politicsText);
                for (int i = 0; i < data.Length; i += buffer.Length)
                    Array.Copy(buffer, 0, data, i, Math.Min(buffer.Length, data.Length - i));
            }
            Array.Copy(enveloped.ToArray(), data, enveloped.Length);

            int[] index = new Mixer(mixerIndex, steganographyKey).GetIndeces(outputBitmap.Length);
            byte[] gamma = new Gamma(gammaIndex, steganographyKey).GetGamma((expandSize + BitsPerByte - 1)/BitsPerByte);
            byte[] colors = outputBitmap.Select(index);
            byte[] cw = new BroadbandSignals(expandSize).EncodeAndCombine(colors, data, gamma, (byte) alpha);
            outputBitmap.Replace(index, cw);

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            steganographyOptions.OutputBitmap = outputBitmap;
            return outputBitmap;
        }

        public string Unpacking(SteganographyOptions steganographyOptions)
        {
            string steganographyKey = steganographyOptions.SteganographyKey;
            int expandSize = steganographyOptions.ExpandSize;
            int filterStep = steganographyOptions.FilterStep;
            int mixerIndex = steganographyOptions.MixerIndex;
            int gammaIndex = steganographyOptions.GammaIndex;
            int archiverIndex = steganographyOptions.ArchiverIndex;

            SteganographyBitmap inputBitmap = steganographyOptions.InputBitmap;
            var blurBitmap = new SteganographyBitmap(inputBitmap, filterStep);

            Debug.WriteLine(inputBitmap.Length);
            Debug.WriteLine(blurBitmap.Length);

            int[] index = new Mixer(mixerIndex, steganographyKey).GetIndeces(inputBitmap.Length);
            byte[] gamma = new Gamma(gammaIndex, steganographyKey).GetGamma((expandSize + BitsPerByte - 1)/BitsPerByte);
            byte[] cw = inputBitmap.Select(index);
            byte[] median = blurBitmap.Select(index);
            byte[] data = new BroadbandSignals(expandSize).DecodeAndExtract(cw, gamma, median);

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

            steganographyOptions.BlurBitmap = blurBitmap;
            return Encoding.Default.GetString(decompressed.ToArray());
        }
    }
}