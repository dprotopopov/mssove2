using System.Linq;

namespace BBSLib
{
    /// <summary>
    ///     Класс подсчёта количества ненулевых бит в массиве данных
    /// </summary>
    public static class BitCounter
    {
        private static readonly long[] BitCount = new long[256]; // Количество ненулевых бит в байте

        static BitCounter()
        {
            BitCount[0] = 0;
            for (var k = 1; k < BitCount.Length; k <<= 1)
                for (var i = 0; i < k; i++)
                    BitCount[k + i] = BitCount[i] + 1;
        }

        /// <summary>
        ///     Подсчёт количества ненулевых бит в массиве данных
        /// </summary>
        /// <param name="bytes">Массиве данных</param>
        /// <returns>Количество ненулевых бит</returns>
        public static long Count(byte[] bytes)
        {
            var byteCount = new int[256];
            foreach (var ch in bytes) byteCount[ch]++;
            return BitCount.Zip(byteCount, (x, y) => x*y).Sum();
        }
    }
}