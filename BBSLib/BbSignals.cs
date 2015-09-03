using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace BBSLib
{
    /// <summary>
    ///     Класс алгоритма обработки данных широкополосного сигнала.
    ///     Каждый бит данных кодируется последовательностью изменённых яркостей пикселей в соответствии со значениями бит
    ///     псевдослучайной последовательности.
    ///     При извлечении данных из массива яркостей пикселей применяется взвешенная схема голосования с коэффициентами
    ///     равными разности между средним значением яркости и яркостью голосующего пикселя.
    ///     Метод широкополосного сигнала предполагает возможность выработки у отправляемой и принимающих сторон одинаковой
    ///     псевдослучайной последовательности или, по крайней мере, псевдослучайных последовательностей со статистическими
    ///     характеристиками эквивалентными равным.
    ///     Использование псевдослучайной последовательности с характеритиками приближенными к равновероятной, для кодирования
    ///     данных, позволяет сохранить среднюю яркость пикселей у исходного графического изображения и у изображения,
    ///     содержащего внедрённые данные.
    ///     Однако при прямом и дословном применении алгоритма средняя яркость пикселей могла бы иметь смещение относительно
    ///     средней яркости у исходного изображения.
    ///     Поэтому производим статистическую оценку такого смещения и вводим её в качестве компенсирующего слагаемого в
    ///     алгоритм.
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
        ///     Изменение яркости пикселей в зависимости от значений бит в массиве байт и бит псевдослучайной последовательности.
        ///     Каждый бит данных кодируется последовательностью изменённых яркостей пикселей в соответствии с битами
        ///     псевдослучайной последовательности.
        ///     Использование псевдослучайной последовательности с характеристиками приближенными к равновероятной, для кодирования
        ///     данных, позволяет сохранить среднюю яркость пикселей у исходного графического изображения и у изображения,
        ///     содержащего внедрённые данные.
        ///     Однако при прямом и дословном применении алгоритма средняя яркость пикселей могла бы иметь смещение относительно
        ///     средней яркости у исходного изображения.
        ///     Поэтому производим статистическую оценку такого смещения и вводим её в качестве компенсирующего слагаемого в
        ///     алгоритм.
        /// </summary>
        /// <param name="colors">Массив яркостей пикселей</param>
        /// <param name="data">Массив помещаемых в контейнер данных</param>
        /// <param name="gamma">Массив значений псевдослучайной последовательности</param>
        /// <param name="alpha">Значение изменения яркости</param>
        /// <param name="betta">Значение изменения яркости</param>
        /// <param name="cw">Массив с изменёными яркостями пикселелей</param>
        public void Combine(byte[] colors, byte[] data, byte[] gamma, int alpha, int betta, byte[] cw)
        {
            Debug.Assert(colors.Length >= data.Length*BitsPerByte*_expandSize);
            Debug.Assert(gamma.Length*BitsPerByte >= _expandSize);

            // Применение вычисленной оценки смещения средней яркости для компенсации в алгоритме
            int delta0 = alpha - betta;
            int delta1 = -alpha - betta;

            using (var y = new MemoryStream(colors))
            using (var z = new MemoryStream(cw))
            {
                var buffer = new byte[_expandSize];
                int index = 0;
                bool[] negGammaBits = Enumerable.Range(index, _expandSize)
                    .Select(i => ((gamma[i >> 3] >> (i & 7)) & 1) == 0).ToArray();
                foreach (
                    bool negDataBit in
                        data.SelectMany(ch => Enumerable.Range(0, BitsPerByte).Select(j => ((ch >> j) & 1) == 0)))
                {
                    y.Read(buffer, 0, buffer.Length);
                    if (_maximumGamma)
                        negGammaBits = Enumerable.Range(index, _expandSize)
                            .Select(i => ((gamma[i >> 3] >> (i & 7)) & 1) == 0).ToArray();
                    buffer = negDataBit
                        ? buffer.Zip(negGammaBits,
                            (color, negGammaBit) =>
                                (byte)
                                    (negGammaBit
                                        ? Math.Min(Math.Max(0, delta0 + color), 255)
                                        : Math.Min(Math.Max(0, delta1 + color), 255)))
                            .ToArray()
                        : buffer.Zip(negGammaBits,
                            (color, negGammaBit) =>
                                (byte)
                                    (negGammaBit
                                        ? Math.Min(Math.Max(0, delta1 + color), 255)
                                        : Math.Min(Math.Max(0, delta0 + color), 255)))
                            .ToArray();
                    z.Write(buffer, 0, buffer.Length);
                    index += _expandSize;
                }
                y.CopyTo(z);
            }
        }

        /// <summary>
        ///     Выделение последовательности байт из значений яркости пикселей и значений бит псевдослучайной последовательности.
        ///     Каждый бит данных кодируется последовательностью изменённых яркостей пикселей в соответствии с битами
        ///     псевдослучайной последовательности.
        ///     При извлечении данных из массива яркостей пикселей применяется взвешенная схема голосования с коэффициентами
        ///     равными разности между средним значением яркости и яркостью голосующего пикселя.
        /// </summary>
        /// <param name="colors">Массив яркостей пикселей</param>
        /// <param name="median">Массив средних яркостей полученных из размытого изображения</param>
        /// <param name="gamma">Массив значений псевдослучайной последовательности</param>
        /// <param name="alpha">Значение изменения яркости</param>
        /// <param name="betta">Значение изменения яркости</param>
        /// <param name="data">Массив выделенных из контейнера данных</param>
        public void Extract(byte[] colors, byte[] median, byte[] gamma, int alpha, int betta, byte[] data)
        {
            Debug.Assert(gamma.Length*BitsPerByte >= _expandSize);

            // Формирование массива весовых коэффициентов для статистического критерия
            // для разделения двух гипотез о распределении c параметрами (alpha-betta,alpha) или (-alpha-betta,alpha)
            var voteValues = new double[512];
            for (int i = -256; i < 256; i++)
                voteValues[256 + i] = (double) i*alpha - betta;

            using (var x = new MemoryStream(colors))
            using (var y = new MemoryStream(median))
            using (var z = new MemoryStream(data))
            {
                int count = colors.Length/BitsPerByte/_expandSize; // Длина строки в байтах
                var votes = new double[BitsPerByte];
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
                                .Zip(negGammaBits,
                                    (weight, negGammaBit) => voteValues[256 + (negGammaBit ? weight : -weight)])
                                .Sum();
                        index += _expandSize;
                    }
                    int ch = votes.Select((vote, j) => (vote < 0) ? (1 << j) : 0).Sum();
                    z.WriteByte((byte) ch);
                }
            }
        }
    }
}