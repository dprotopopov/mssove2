using System;
using System.Linq;
using System.Security.Cryptography;
using BBSLib.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BBSLib.UnitTest
{
    [TestClass]
    public class ZipperUnitTest
    {
        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        [TestMethod]
        public void ZipperTestMethod1()
        {
            double[] delta = {-2, -1, 0, 1, 2};

            for (var i = 0; i < 4; i++)
                using (var zipper = new Zipper(i))
                {
                    var input = delta.ToArray();
                    var output = delta.ToArray();
                    zipper.Expand(input);
                    zipper.Compact(output);
                    Console.WriteLine(Zipper.ComboBoxItems[i]);
                    Console.WriteLine(string.Join(Environment.NewLine,
                        input.Zip(delta, (x, y) => string.Format("{0} {1}", x, y))
                            .Zip(output, (x, y) => string.Format("{0} {1}", x, y))));
                }
        }

        [TestMethod]
        public void ZipperTestMethod2()
        {
            const int length = 1000;
            var buffer = new byte[length];
            Rng.GetBytes(buffer);

            var delta = buffer.Select(x => (double) x).ToArray();

            for (var i = 0; i < 4; i++)
                using (var zipper = new Zipper(i))
                {
                    Console.WriteLine(Zipper.ComboBoxItems[i]);
                    var input = delta.ToArray();
                    zipper.Expand(delta);
                    zipper.Compact(delta);
                    var output = delta.ToArray();
                    Assert.IsTrue(input.Zip(output, (x, y) => Math.Abs(x - y) < 0.1).All(x => x));
                }
        }
    }
}