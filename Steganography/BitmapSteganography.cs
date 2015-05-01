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

        public Bitmap Packing(Image imageSample, string steganographyKey, int expandSize, int alpha, bool compress, string text)
        {
            var decompressed = new MemoryStream(Encoding.Default.GetBytes(text));
            var compressed = new MemoryStream();
            var enveloped = new MemoryStream();

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            if(compress) using (var compessionStream = new DeflateStream(compressed, CompressionMode.Compress, true))
                decompressed.CopyTo(compessionStream);
            else decompressed.CopyTo(compressed);

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            new SequenceIndicator().Envelop(compressed, enveloped);

            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            int count = (int) enveloped.Length*expandSize*8;
            double ratio = Math.Sqrt((double) count/(imageSample.Width*imageSample.Height*3));
            var bitmap = new Bitmap(
                (int) (ratio*imageSample.Width + 1),
                (int) (ratio*imageSample.Height + 1),
                PixelFormat.Format32bppArgb);
            Graphics.FromImage(bitmap).DrawImage(imageSample, new Rectangle(0, 0, bitmap.Width, bitmap.Height));

            var data = new byte[bitmap.Width*bitmap.Height*3/8/expandSize];
            Rng.GetBytes(data);
            Array.Copy(enveloped.ToArray(), data, enveloped.Length);
            int[] index = new Arcfour(steganographyKey).Ksa(bitmap.Width*bitmap.Height*3);
            byte[] colors = new BitmapTools(bitmap).Select(index);
            byte[] gamma = new Arcfour(steganographyKey).Prga(bitmap.Width * bitmap.Height * 3 / 8);

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

        public string Unpacking(Bitmap bitmap, string steganographyKey, int expandSize, int filterStep, bool decompress)
        {
            byte[] gamma = new Arcfour(steganographyKey).Prga(bitmap.Width*bitmap.Height*3/8);
            int[] index = new Arcfour(steganographyKey).Ksa(bitmap.Width*bitmap.Height*3);
            byte[] cw = new BitmapTools(bitmap).Select(index);
            byte[] median = new BitmapTools(new BoxFilter(filterStep).Filter(bitmap)).Select(index);
            byte[] data = new BroadbandSignals(expandSize).DecodeAndExtract(cw, gamma, median);

            var encoded = new MemoryStream(data);
            var enveloped = new MemoryStream();
            var compressed = new MemoryStream();
            var decompressed = new MemoryStream();

            encoded.Seek(0, SeekOrigin.Begin);
            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            new ErrorCorrection(bitmap.Width*bitmap.Height*3/8, expandSize).Decode(encoded, enveloped);

            encoded.Seek(0, SeekOrigin.Begin);
            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            new SequenceIndicator().Extract(enveloped, compressed);

            encoded.Seek(0, SeekOrigin.Begin);
            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            if(decompress) using (var decompessionStream = new DeflateStream(compressed, CompressionMode.Decompress, true))
                decompessionStream.CopyTo(decompressed);
            else compressed.CopyTo(decompressed);

            encoded.Seek(0, SeekOrigin.Begin);
            enveloped.Seek(0, SeekOrigin.Begin);
            compressed.Seek(0, SeekOrigin.Begin);
            decompressed.Seek(0, SeekOrigin.Begin);

            Debug.WriteLine(bitmap.Width*bitmap.Height*3);
            Debug.WriteLine(encoded.Length);
            Debug.WriteLine(enveloped.Length);
            Debug.WriteLine(compressed.Length);
            Debug.WriteLine(decompressed.Length);

            Debug.WriteLine("          cw" + string.Join("", cw.ToArray().Select(x => x.ToString("X02"))));
            Debug.WriteLine("      median" + string.Join("", median.ToArray().Select(x => x.ToString("X02"))));
            Debug.WriteLine("       gamma" + string.Join("", gamma.ToArray().Select(x => x.ToString("X02"))));
            Debug.WriteLine("     encoded" + string.Join("", encoded.ToArray().Select(x => x.ToString("X02"))));
            Debug.WriteLine("   enveloped" + string.Join("", enveloped.ToArray().Select(x => x.ToString("X02"))));
            Debug.WriteLine("  compressed" + string.Join("", compressed.ToArray().Select(x => x.ToString("X02"))));
            Debug.WriteLine("decompressed" + string.Join("", decompressed.ToArray().Select(x => x.ToString("X02"))));

            return Encoding.Default.GetString(decompressed.ToArray());
        }
    }
}