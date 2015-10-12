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
        ///     Вычисление изменения яркости пикселей в зависимости от значений бит в массиве байт и бит псевдослучайной
        ///     последовательности.
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
        /// <param name="delta">Массив изменений яркостей пикселей</param>
        /// <param name="data">Массив помещаемых в контейнер данных</param>
        /// <param name="gamma">Массив значений псевдослучайной последовательности</param>
        /// <param name="alpha">Значение изменения яркости</param>
        /// <param name="betta">Значение изменения яркости</param>
        public void Combine(int[] delta, byte[] data, byte[] gamma, double alpha, double betta)
        {
            Debug.Assert(delta.Length >= data.Length*BitsPerByte*_expandSize);
            Debug.Assert(gamma.Length*BitsPerByte >= _expandSize);

            // Применение вычисленной оценки смещения средней яркости для компенсации в алгоритме
            var delta0 = (int) Math.Round(alpha - betta);
            var delta1 = (int) Math.Round(-alpha - betta);
            var index = 0;
            var negGammaBits = Enumerable.Range(index, _expandSize)
                .Select(i => ((gamma[i >> 3] >> (i & 7)) & 1) == 0).ToArray();
            foreach (var ch in data)
                foreach (var negDataBit in Enumerable.Range(0, BitsPerByte).Select(j => ((ch >> j) & 1) == 0))
                {
                    if (_maximumGamma && index > 0)
                        negGammaBits = Enumerable.Range(index, _expandSize)
                            .Select(i => ((gamma[i >> 3] >> (i & 7)) & 1) == 0).ToArray();
                    var buffer = negDataBit
                        ? negGammaBits.Select(negGammaBit => (negGammaBit ? delta0 : delta1)).ToArray()
                        : negGammaBits.Select(negGammaBit => (negGammaBit ? delta1 : delta0)).ToArray();
                    Array.Copy(buffer, 0, delta, index, _expandSize);
                    index += _expandSize;
                }
        }

        /// <summary>
        ///     Вычисление изменения яркости пикселей в зависимости от значений бит в массиве байт и бит псевдослучайной
        ///     последовательности.
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
        /// <param name="delta">Массив изменений яркостей пикселей</param>
        /// <param name="data">Массив помещаемых в контейнер данных</param>
        /// <param name="gamma">Массив значений псевдослучайной последовательности</param>
        /// <param name="alpha">Значение изменения яркости</param>
        /// <param name="betta">Значение изменения яркости</param>
        public void Combine(double[] delta, byte[] data, byte[] gamma, double alpha, double betta)
        {
            Debug.Assert(delta.Length >= data.Length*BitsPerByte*_expandSize);
            Debug.Assert(gamma.Length*BitsPerByte >= _expandSize);

            // Применение вычисленной оценки смещения средней яркости для компенсации в алгоритме
            var delta0 = alpha - betta;
            var delta1 = -alpha - betta;
            var index = 0;
            var negGammaBits = Enumerable.Range(index, _expandSize)
                .Select(i => ((gamma[i >> 3] >> (i & 7)) & 1) == 0).ToArray();
            foreach (var ch in data)
                foreach (var negDataBit in Enumerable.Range(0, BitsPerByte).Select(j => ((ch >> j) & 1) == 0))
                {
                    if (_maximumGamma && index > 0)
                        negGammaBits = Enumerable.Range(index, _expandSize)
                            .Select(i => ((gamma[i >> 3] >> (i & 7)) & 1) == 0).ToArray();
                    var buffer = negDataBit
                        ? negGammaBits.Select(negGammaBit => (negGammaBit ? delta0 : delta1)).ToArray()
                        : negGammaBits.Select(negGammaBit => (negGammaBit ? delta1 : delta0)).ToArray();
                    Array.Copy(buffer, 0, delta, index, _expandSize);
                    index += _expandSize;
                }
        }

        /// <summary>
        ///     Выделение последовательности байт из значений изменений яркостей пикселей и значений бит псевдослучайной
        ///     последовательности.
        ///     Каждый бит данных кодируется последовательностью изменённых яркостей пикселей в соответствии с битами
        ///     псевдослучайной последовательности.
        ///     При извлечении данных из массива яркостей пикселей применяется взвешенная схема голосования с коэффициентами
        ///     равными разности между средним значением яркости и яркостью голосующего пикселя.
        /// </summary>
        /// <param name="delta">Массив изменений яркостей пикселей</param>
        /// <param name="gamma">Массив значений псевдослучайной последовательности</param>
        /// <param name="alpha">Среднее значение изменения яркости</param>
        /// <param name="betta">Среднее значение изменения яркости</param>
        /// <param name="data">Массив выделенных из контейнера данных</param>
        public void Extract(int[] delta, byte[] data, byte[] gamma, int alpha, int betta)
        {
            Debug.Assert(gamma.Length*BitsPerByte >= _expandSize);

            using (var z = new MemoryStream(data))
            {
                var count = delta.Length/BitsPerByte/_expandSize; // Длина строки в байтах
                var votes = new double[BitsPerByte];
                var buffer = new int[_expandSize];
                var index = 0;
                var negGammaBits = Enumerable.Range(index, _expandSize)
                    .Select(i => ((gamma[i >> 3] >> (i & 7)) & 1) == 0).ToArray();
                while (count-- > 0)
                {
                    Array.Clear(votes, 0, votes.Length);
                    for (var j = 0; j < BitsPerByte; j++)
                    {
                        Array.Copy(delta, index, buffer, 0, buffer.Length);
                        if (_maximumGamma && index > 0)
                            negGammaBits = Enumerable.Range(index, _expandSize)
                                .Select(i => ((gamma[i >> 3] >> (i & 7)) & 1) == 0).ToArray();
                        votes[j] = buffer.Zip(negGammaBits,
                            (weight, negGammaBit) => (negGammaBit ? (weight - betta) : (betta - weight))*alpha)
                            .Sum();
                        index += _expandSize;
                    }
                    z.WriteByte((byte) votes.Select((vote, j) => (vote < 0) ? (1 << j) : 0).Sum());
                }
            }
        }

        /// <summary>
        ///     Выделение последовательности байт из значений изменений яркостей пикселей и значений бит псевдослучайной
        ///     последовательности.
        ///     Каждый бит данных кодируется последовательностью изменённых яркостей пикселей в соответствии с битами
        ///     псевдослучайной последовательности.
        ///     При извлечении данных из массива яркостей пикселей применяется взвешенная схема голосования с коэффициентами
        ///     равными разности между средним значением яркости и яркостью голосующего пикселя.
        /// </summary>
        /// <param name="delta">Массив изменений яркостей пикселей</param>
        /// <param name="gamma">Массив значений псевдослучайной последовательности</param>
        /// <param name="alpha">Среднее значение изменения яркости</param>
        /// <param name="betta">Среднее значение изменения яркости</param>
        /// <param name="data">Массив выделенных из контейнера данных</param>
        public void Extract(double[] delta, byte[] data, byte[] gamma, double alpha, double betta)
        {
            Debug.Assert(gamma.Length*BitsPerByte >= _expandSize);

            using (var z = new MemoryStream(data))
            {
                var count = delta.Length/BitsPerByte/_expandSize; // Длина результата в байтах
                var votes = new double[BitsPerByte];
                var buffer = new double[_expandSize];
                var index = 0;
                var negGammaBits = Enumerable.Range(index, _expandSize)
                    .Select(i => ((gamma[i >> 3] >> (i & 7)) & 1) == 0).ToArray();
                while (count-- > 0)
                {
                    Array.Clear(votes, 0, votes.Length);
                    for (var j = 0; j < BitsPerByte; j++)
                    {
                        Array.Copy(delta, index, buffer, 0, buffer.Length);
                        if (_maximumGamma && index > 0)
                            negGammaBits = Enumerable.Range(index, _expandSize)
                                .Select(i => ((gamma[i >> 3] >> (i & 7)) & 1) == 0).ToArray();
                        votes[j] = buffer.Zip(negGammaBits,
                            (weight, negGammaBit) => (negGammaBit ? (weight - betta) : (betta - weight))*alpha)
                            .Sum();
                        index += _expandSize;
                    }
                    z.WriteByte((byte) votes.Select((vote, j) => (vote < 0) ? (1 << j) : 0).Sum());
                }
            }
        }
    }
}