using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Steganography
{
    public class BitmapSteganography
    {
        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        public Bitmap Packing(SteganographyOptions steganographyOptions)
        {
            Image imageSample = steganographyOptions.ImageSample;
            string steganographyKey = steganographyOptions.SteganographyKey;
            int expandSize = steganographyOptions.ExpandSize;
            int alpha = steganographyOptions.Alpha;
            bool compress = steganographyOptions.Compress;
            string text = steganographyOptions.Text;
            bool autoResize = steganographyOptions.SampleAutoresize;
            bool politicsNone = steganographyOptions.PoliticsNone;
            bool politicsZero = steganographyOptions.PoliticsZero;
            bool politicsRandom = steganographyOptions.PoliticsRandom;
            bool politicsFake = steganographyOptions.PoliticsFake;
            string politicsText = steganographyOptions.PoliticsText;

            var decompressed = new MemoryStream(Encoding.Default.GetBytes(text));
            var compressed = new MemoryStream();
            var enveloped = new MemoryStream();

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            if (compress)
                using (var compessionStream = new DeflateStream(compressed, CompressionMode.Compress, true))
                    decompressed.CopyTo(compessionStream);
            else decompressed.CopyTo(compressed);

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            new SequenceIndicator().Envelop(compressed, enveloped);

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            int count = (int) enveloped.Length*expandSize*8; // “ребуемое число бит
            double ratio = Math.Sqrt((double) count/(imageSample.Width*imageSample.Height*3));
            Bitmap bitmap = autoResize
                ? new Bitmap(
                    (int) (ratio*imageSample.Width + 1),
                    (int) (ratio*imageSample.Height + 1),
                    PixelFormat.Format32bppArgb)
                : new Bitmap(
                    imageSample.Width,
                    imageSample.Height,
                    PixelFormat.Format32bppArgb);

            // дл€ каждого бита сообщени€ нужно N байт носител€
            if (enveloped.Length*expandSize*8 > bitmap.Width*bitmap.Height*3)
                throw new Exception("–азмер изображени€ недостаточен дл€ сохранени€ данных " +
                                    (enveloped.Length*expandSize*8) + "/" + (bitmap.Width*bitmap.Height*3));

            Graphics.FromImage(bitmap).DrawImage(imageSample, new Rectangle(0, 0, bitmap.Width, bitmap.Height));

            var data = new byte[politicsNone ? enveloped.Length : bitmap.Width*bitmap.Height*3/8/expandSize];
            if (politicsZero) Array.Clear(data, 0, data.Length);
            if (politicsRandom) Rng.GetBytes(data);
            if (politicsFake)
            {
                if (string.IsNullOrWhiteSpace(politicsText)) throw new Exception("ќтсутствует заполн€ющий текст");
                byte[] buffer = Encoding.Default.GetBytes(politicsText);
                for (int i = 0; i < data.Length; i += buffer.Length)
                    Array.Copy(buffer, 0, data, i, Math.Min(buffer.Length, data.Length - i));
            }
            Array.Copy(enveloped.ToArray(), data, enveloped.Length);

            int[] index = new Arcfour(steganographyKey).Ksa(bitmap.Width*bitmap.Height*3);
            byte[] colors = new BitmapTools(bitmap).Select(index);
            byte[] gamma = new Arcfour(steganographyKey).Prga((expandSize + 7)/8);

            byte[] cw = new BroadbandSignals(expandSize).EncodeAndCombine(colors, data, gamma, alpha);
            new BitmapTools(bitmap).Replace(index, cw);

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            Debug.WriteLine("decompressed" + string.Join("", decompressed.ToArray().Select(x => x.ToString("X02"))));
            Debug.WriteLine("  compressed" + string.Join("", compressed.ToArray().Select(x => x.ToString("X02"))));
            Debug.WriteLine("   enveloped" + string.Join("", enveloped.ToArray().Select(x => x.ToString("X02"))));
            Debug.WriteLine("       gamma" + string.Join("", gamma.ToArray().Select(x => x.ToString("X02"))));
            Debug.WriteLine("          cw" + string.Join("", cw.ToArray().Select(x => x.ToString("X02"))));

            Debug.WriteLine(decompressed.Length);
            Debug.WriteLine(compressed.Length);
            Debug.WriteLine(enveloped.Length);
            Debug.WriteLine(bitmap.Width*bitmap.Height*3);

            return bitmap;
        }

        public string Unpacking(SteganographyOptions steganographyOptions)
        {
            Bitmap bitmap = steganographyOptions.Bitmap;
            string steganographyKey = steganographyOptions.SteganographyKey;
            int expandSize = steganographyOptions.ExpandSize;
            int filterStep = steganographyOptions.FilterStep;
            bool decompress = steganographyOptions.Compress;

            byte[] gamma = new Arcfour(steganographyKey).Prga((expandSize + 7)/8);
            int[] index = new Arcfour(steganographyKey).Ksa(bitmap.Width*bitmap.Height*3);
            byte[] cw = new BitmapTools(bitmap).Select(index);
            byte[] median = new BitmapTools(new BoxFilter(filterStep).Filter(bitmap)).Select(index);
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

            if (decompress)
                using (var decompessionStream = new DeflateStream(compressed, CompressionMode.Decompress, true))
                    decompessionStream.CopyTo(decompressed);
            else compressed.CopyTo(decompressed);

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            Debug.WriteLine(bitmap.Width*bitmap.Height*3);
            Debug.WriteLine(enveloped.Length);
            Debug.WriteLine(compressed.Length);
            Debug.WriteLine(decompressed.Length);

            Debug.WriteLine("          cw" + string.Join("", cw.ToArray().Select(x => x.ToString("X02"))));
            Debug.WriteLine("      median" + string.Join("", median.ToArray().Select(x => x.ToString("X02"))));
            Debug.WriteLine("       gamma" + string.Join("", gamma.ToArray().Select(x => x.ToString("X02"))));
            Debug.WriteLine("   enveloped" + string.Join("", enveloped.ToArray().Select(x => x.ToString("X02"))));
            Debug.WriteLine("  compressed" + string.Join("", compressed.ToArray().Select(x => x.ToString("X02"))));
            Debug.WriteLine("decompressed" + string.Join("", decompressed.ToArray().Select(x => x.ToString("X02"))));

            return Encoding.Default.GetString(decompressed.ToArray());
        }
    }
}