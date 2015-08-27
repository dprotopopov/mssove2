using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Steganography.Utils;
using ZXing.Common.ReedSolomon;

namespace Steganography.UnitTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            int codeSize = 255;
            int dataSize = 15;
            GenericGF gf = GenericGF.QR_CODE_FIELD_256;
            var decoder = new ReedSolomonDecoder(gf);
            var encoder = new ReedSolomonEncoder(gf);
            int twoS = System.Math.Abs(codeSize - dataSize);
            var buffer = new byte[System.Math.Max(codeSize, dataSize)];
            new Noise(0.5, 0.5).GetBytes(buffer);
            Console.WriteLine(string.Join("", buffer.ToList().GetRange(0, dataSize).Select(x => x.ToString("X02"))));
            int[] array = buffer.Select(x => (int)x).ToArray();
            encoder.encode(array, twoS);
            buffer = array.Select(x => (byte)x).ToArray();
            Console.WriteLine(string.Join("", buffer.ToList().GetRange(0, codeSize).Select(x => x.ToString("X02"))));
            array = buffer.Select(x => (int)x).ToArray();
            decoder.decode(array, twoS);
            buffer = array.Select(x => (byte)x).ToArray();
            Console.WriteLine(string.Join("", buffer.ToList().GetRange(0, dataSize).Select(x => x.ToString("X02"))));
        }

        [TestMethod]
        public void TestMethod2()
        {
            for (int i = 0; i < 10; i++)
            {
                var message = new byte[1000];
                new Noise(0.5, 0.5).GetBytes(message);

                var source = new MemoryStream(message);
                var compressed = new MemoryStream();
                var decompressed = new MemoryStream();

                source.Seek(0, SeekOrigin.Begin);
                compressed.Seek(0, SeekOrigin.Begin);
                decompressed.Seek(0, SeekOrigin.Begin);

                using (var compessionStream = new DeflateStream(compressed, CompressionMode.Compress, true))
                    source.CopyTo(compessionStream);

                source.Seek(0, SeekOrigin.Begin);
                compressed.Seek(0, SeekOrigin.Begin);
                decompressed.Seek(0, SeekOrigin.Begin);

                using (var decompessionStream = new DeflateStream(compressed, CompressionMode.Decompress, true))
                    decompessionStream.CopyTo(decompressed);

                source.Seek(0, SeekOrigin.Begin);
                compressed.Seek(0, SeekOrigin.Begin);
                decompressed.Seek(0, SeekOrigin.Begin);

                Console.WriteLine(string.Join("", source.ToArray().Select(x => x.ToString("X02"))));
                Console.WriteLine(string.Join("", compressed.ToArray().Select(x => x.ToString("X02"))));
                Console.WriteLine(string.Join("", decompressed.ToArray().Select(x => x.ToString("X02"))));
                Console.WriteLine();

                Assert.IsTrue(new Comparer().Compare(source, decompressed) == 0);
            }
        }

        [TestMethod]
        public void TestMethod4()
        {
            for (int i = 0; i < 10; i++)
            {
                var message = new byte[1000];
                var random = new byte[1000];

                new Noise(0.5, 0.5).GetBytes(message);
                new Noise(0.5, 0.5).GetBytes(random);

                var source = new MemoryStream(message);
                var enveloped = new Envelope().Seal(source);
                var temp = new MemoryStream();
                enveloped.CopyTo(temp);
                temp.Write(random, 0, random.Length);
                temp.Seek(0, SeekOrigin.Begin);
                var extracted = new Envelope().Extract(temp);
                var memoryStream = new MemoryStream();
                extracted.CopyTo(memoryStream);


                Console.WriteLine(new Comparer().Distance(memoryStream.ToArray(), source.ToArray()));

                Assert.IsTrue(new Comparer().Compare(source, extracted) == 0);
            }
        }
    }
}