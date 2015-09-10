using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BBSLib.UnitTest
{
    [TestClass]
    public class BbSignalsUnitTest
    {
        private const int BitsPerByte = 8; // Количество битов в байте
        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        [TestMethod]
        public void BbSignalsTestMethod1()
        {
            const int length = 1000;
            const int expandSize = 100;
            var input = new byte[length];
            var output = new byte[length];
            var gamma = new byte[length*expandSize];
            var buffer = new double[length*BitsPerByte*expandSize];

            Rng.GetBytes(input);
            Rng.GetBytes(gamma);

            double alpha = 1;
            double betta = 1;

            using (var bbSignals = new BbSignals(expandSize, true))
            {
                bbSignals.Combine(buffer, input, gamma, alpha, betta);
                bbSignals.Extract(buffer, output, gamma, alpha, betta);
            }

            Assert.IsTrue(input.SequenceEqual(output));
        }

        [TestMethod]
        public void BbSignalsTestMethod2()
        {
            const int length = 1000;
            const int expandSize = 100;
            var input = new byte[length];
            var output = new byte[length];
            var gamma = new byte[length*expandSize];
            var buffer = new double[length*BitsPerByte*expandSize];

            Rng.GetBytes(input);
            Rng.GetBytes(gamma);

            double alpha = 1;
            double betta = 1;

            using (var bbSignals = new BbSignals(expandSize, true))
            {
                // Вычисление количества единиц в исходных данных и псевдослучайной последовательности
                double trueData = BitCounter.Count(input);
                double trueGamma = BitCounter.Count(gamma);

                // Вычисление количества нулей в исходных данных и псевдослучайной последовательности
                double falseData = (long) input.Length*BitsPerByte - trueData;
                double falseGamma = (long) gamma.Length*BitsPerByte - trueGamma;

                // Вычисление оценки количества единиц и нулей при смешивании исходных данных и псевдослучайной последовательности
                double trueCount = trueGamma*falseData + falseGamma*trueData;
                double falseCount = trueGamma*trueData + falseGamma*falseData;
                Console.WriteLine("p0 = {0} p1 = {1}", trueCount/(trueCount + falseCount),
                    falseCount/(trueCount + falseCount));

                betta = ((falseCount - trueCount)*alpha/(trueCount + falseCount));
                Console.WriteLine("alpha = {0} betta = {1}", alpha, betta);

                bbSignals.Combine(buffer, input, gamma, alpha, betta);
                //////////////////////////////////////////////////
                buffer = buffer.Select(x => x + 1).ToArray();

                double e1 = buffer.Average();
                double e2 = buffer.Average(x => x*x);
                betta = e1;
                alpha = Math.Sqrt(e2 - e1*e1);
                Console.WriteLine("alpha = {0} betta = {1}", alpha, betta);

                bbSignals.Extract(buffer, output, gamma, alpha, betta);
                /////////////////////////////////////////////////////////
            }

            Assert.IsTrue(input.SequenceEqual(output));
        }
    }
}