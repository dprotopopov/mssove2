using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace BBSLib
{
    /// <summary>
    ///     Класс алгоритма обработки данных широкополосного сигнала
    /// </summary>
    public class BbSignals : IDisposable
    {
        private const int BitsPerByte = 8; // Количество битов в байте
        private readonly int _expandSize;
        private readonly bool _maximumGamma;

        /// <summary>
        /// </summary>
        /// <param name="expandSize"></param>
        /// <param name="maximumGamma"></param>
        public BbSignals(int expandSize, bool maximumGamma)
        {
            _expandSize = expandSize;
            _maximumGamma = maximumGamma;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        ///     Изменение яркости пикселей в зависимости от значений бит в массиве байт и бит гаммы.
        ///     Каждый бит данных кодируется последовательностью изменённых яркостей пикселей в соответствии с битами гаммы.
        /// </summary>
        /// <param name="colors">Массив яркостей пикселей</param>
        /// <param name="data">Массив помещаемых в контейнер данных</param>
        /// <param name="gamma">Массив значений гаммы</param>
        /// <param name="alpha">Значение изменения яркости</param>
        /// <returns>Массив с изменёными яркостями пикселелей</returns>
        public byte[] Combine(byte[] colors, byte[] data, byte[] gamma, byte alpha)
        {
            Debug.Assert(colors.Length >= data.Length*BitsPerByte*_expandSize);
            Debug.Assert(gamma.Length*BitsPerByte >= _expandSize);

            using (var x = new MemoryStream(data))
            {
                using (var y = new MemoryStream(colors))
                {
                    using (var z = new MemoryStream())
                    {
                        int delta = alpha;
                        var buffer = new byte[_expandSize];
                        int index = 0;
                        bool[] negGammaBits = Enumerable.Range(index, _expandSize)
                            .Select(i => ((gamma[i >> 3] >> (i & 7)) & 1) == 0).ToArray();
                        for (int ch = x.ReadByte(); ch >= 0; ch = x.ReadByte())
                        {
                            foreach (
                                bool negDataBit in Enumerable.Range(0, BitsPerByte).Select(j => ((ch >> j) & 1) == 0))
                            {
                                y.Read(buffer, 0, buffer.Length);
                                if (_maximumGamma)
                                    negGammaBits = Enumerable.Range(index, _expandSize)
                                        .Select(i => ((gamma[i >> 3] >> (i & 7)) & 1) == 0).ToArray();
                                buffer = negDataBit
                                    ? buffer.Zip(negGammaBits,
                                        (color, negGammaBit) =>
                                            (byte)
                                                (negGammaBit ? Math.Min(color + delta, 255) : Math.Max(0, color - delta)))
                                        .ToArray()
                                    : buffer.Zip(negGammaBits,
                                        (color, negGammaBit) =>
                                            (byte)
                                                (negGammaBit ? Math.Max(0, color - delta) : Math.Min(color + delta, 255)))
                                        .ToArray();
                                z.Write(buffer, 0, buffer.Length);
                                index += _expandSize;
                            }
                        }
                        y.CopyTo(z);
                        z.Seek(0, SeekOrigin.Begin);
                        return z.ToArray();
                    }
                }
            }
        }

        /// <summary>
        ///     Выделение последовательности байт из значений яркости пикселей и значений бит гаммы.
        ///     Каждый бит данных кодируется последовательностью изменённых яркостей пикселей в соответствии с битами гаммы.
        /// </summary>
        /// <param name="colors">Массив яркостей пикселей</param>
        /// <param name="median">Массив средних яркостей вокруг пикселя полученных из размытого изображения</param>
        /// <param name="gamma">Массив значений гаммы</param>
        /// <returns>Массив выделенных из контейнера данных</returns>
        public byte[] Extract(byte[] colors, byte[] median, byte[] gamma)
        {
            Debug.Assert(gamma.Length*BitsPerByte >= _expandSize);

            using (var x = new MemoryStream(colors))
            {
                using (var y = new MemoryStream(median))
                {
                    using (var z = new MemoryStream())
                    {
                        int count = colors.Length/BitsPerByte/_expandSize; // Длина строки в байтах
                        var votes = new long[BitsPerByte];
                        var xBuffer = new byte[_expandSize];
                        var yBuffer = new byte[_expandSize];
                        int index = 0;
                        bool[] negGammaBits = Enumerable.Range(index, _expandSize)
                            .Select(i => ((gamma[i >> 3] >> (i & 7)) & 1) == 0).ToArray();
                        while (count-- > 0)
                        {
                            Array.Clear(votes, 0, votes.Length);
                            for (int j = 0; j < BitsPerByte; j++)
                            {
                                x.Read(xBuffer, 0, xBuffer.Length);
                                y.Read(yBuffer, 0, yBuffer.Length);
                                if (_maximumGamma)
                                    negGammaBits = Enumerable.Range(index, _expandSize)
                                        .Select(i => ((gamma[i >> 3] >> (i & 7)) & 1) == 0).ToArray();
                                votes[j] =
                                    xBuffer.Zip(yBuffer,
                                        (xValue, yValue) => ((int) xValue - (int) yValue))
                                        .Zip(negGammaBits, (weight, negGammaBit) => negGammaBit ? weight : -weight)
                                        .Sum(value => (long) value);
                                index += _expandSize;
                            }
                            int ch = votes.Select((vote, j) => (vote < 0) ? (1 << j) : 0).Sum();
                            z.WriteByte((byte) ch);
                        }
                        z.Seek(0, SeekOrigin.Begin);
                        return z.ToArray();
                    }
                }
            }
        }
    }
}