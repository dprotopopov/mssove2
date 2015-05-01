using System.IO;

namespace Steganography
{
    /// <summary>
    ///     Сравнение двоичных последовательностей
    /// </summary>
    public class Comparer
    {
        private const int BufferSize = 32768;

        /// <summary>
        ///     Сравнение двух потоков
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Stream x, Stream y)
        {
            var b1 = new byte[BufferSize];
            var b2 = new byte[BufferSize];
            for (;;)
            {
                int r1 = x.Read(b1, 0, b1.Length);
                int r2 = y.Read(b2, 0, b2.Length);
                int delta = r1 - r2;
                if (delta != 0) return delta;
                if (r1 == 0 || r2 == 0) return 0;
                for (int i = 0; i < r1 && i < r2; i++)
                {
                    int delta2 = b1[i] - b2[i];
                    if (delta2 != 0) return delta2;
                }
            }
        }

        /// <summary>
        ///     Сравнение двух массивов
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(byte[] x, byte[] y)
        {
            int delta = x.Length - y.Length;
            if (delta != 0) return delta;
            for (int i = 0; i < x.Length && i < y.Length; i++)
            {
                int delta2 = x[i] - y[i];
                if (delta2 != 0) return delta2;
            }
            return 0;
        }

        public int Distance(byte[] x, byte[] y)
        {
            int count = 0;
            int i = 0;
            for (; i < x.Length && i < y.Length; i++) count += Distance(x[i], y[i]);
            for (; i < x.Length; i++) count += Distance(x[i], 0);
            for (; i < y.Length; i++) count += Distance(0, y[i]);
            return count;
        }

        public int Distance(byte x, byte y)
        {
            int count = 0;
            for (var z = (byte) (x ^ y); z != 0; z >>= 1) count += z & 1;
            return count;
        }
    }
}