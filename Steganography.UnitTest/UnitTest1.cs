using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Steganography.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            for (int i = 0; i < 10; i++)
            {
                var message = new byte[1000];
                new Noise(0.5, 0.5).GetBytes(message);

                var source = new MemoryStream(message);
                var encoded = new MemoryStream();
                var noised = new MemoryStream();
                var decoded = new MemoryStream();

                source.Seek(0, SeekOrigin.Begin);
                encoded.Seek(0, SeekOrigin.Begin);
                noised.Seek(0, SeekOrigin.Begin);
                decoded.Seek(0, SeekOrigin.Begin);

                new ErrorCorrection(10000, 10).Encode(source, encoded);

                source.Seek(0, SeekOrigin.Begin);
                encoded.Seek(0, SeekOrigin.Begin);
                noised.Seek(0, SeekOrigin.Begin);
                decoded.Seek(0, SeekOrigin.Begin);

                new Noise(99/100, 1/100).Add(encoded, noised);

                source.Seek(0, SeekOrigin.Begin);
                encoded.Seek(0, SeekOrigin.Begin);
                noised.Seek(0, SeekOrigin.Begin);
                decoded.Seek(0, SeekOrigin.Begin);

                new ErrorCorrection(10000, 10).Decode(noised, decoded);

                source.Seek(0, SeekOrigin.Begin);
                encoded.Seek(0, SeekOrigin.Begin);
                noised.Seek(0, SeekOrigin.Begin);
                decoded.Seek(0, SeekOrigin.Begin);

                Console.WriteLine(string.Join("", source.ToArray().Select(x => x.ToString("X02"))));
                Console.WriteLine(string.Join("", encoded.ToArray().Select(x => x.ToString("X02"))));
                Console.WriteLine(string.Join("", noised.ToArray().Select(x => x.ToString("X02"))));
                Console.WriteLine(string.Join("", decoded.ToArray().Select(x => x.ToString("X02"))));
                Console.WriteLine();

                Assert.IsTrue(new Comparer().Compare(source, decoded) == 0);
            }
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
                var enveloped = new MemoryStream();
                var extracted = new MemoryStream();

                source.Seek(0, SeekOrigin.Begin);
                enveloped.Seek(0, SeekOrigin.Begin);
                extracted.Seek(0, SeekOrigin.Begin);

                new SequenceIndicator().Envelop(source, enveloped);
                enveloped.Write(random, 0, random.Length);

                source.Seek(0, SeekOrigin.Begin);
                enveloped.Seek(0, SeekOrigin.Begin);
                extracted.Seek(0, SeekOrigin.Begin);

                new SequenceIndicator().Extract(enveloped, extracted);

                source.Seek(0, SeekOrigin.Begin);
                enveloped.Seek(0, SeekOrigin.Begin);
                extracted.Seek(0, SeekOrigin.Begin);

                Console.WriteLine(new Comparer().Distance(extracted.ToArray(), source.ToArray()));

                Assert.IsTrue(new Comparer().Compare(source, extracted) == 0);
            }
        }
    }
}