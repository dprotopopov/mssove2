using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using FFTWSharp;

namespace BBSLib
{
    /// <summary>
    ///     Класс инструментов для работы с DHT изображений
    /// </summary>
    public class DhtOfBitmap : IDataContainer
    {
        private static readonly Mutex FFTW_Lock = new Mutex();
        private readonly CvBitmap _cvBitmap;
        private readonly double[] _data;
        private readonly double _divider;

        private readonly IntPtr _fftwplan; // pointer to the FFTW plan object
        private readonly GCHandle _gcHandle;

        public DhtOfBitmap(CvBitmap cvBitmap)
        {
            int n0 = cvBitmap.Data.GetLength(0);
            int n1 = cvBitmap.Data.GetLength(1);
            int n2 = cvBitmap.Data.GetLength(2);

            _cvBitmap = cvBitmap;
            _data = new double[cvBitmap.Length];
            _gcHandle = GCHandle.Alloc(_data, GCHandleType.Pinned);
            _fftwplan = fftw.r2r_3d(n0, n1, n2,
                _gcHandle.AddrOfPinnedObject(), _gcHandle.AddrOfPinnedObject(),
                fftw_kind.DHT, fftw_kind.DHT, fftw_kind.DHT,
                fftw_flags.Estimate);
            _divider = Math.Sqrt(cvBitmap.Length);
        }

        public void Dispose()
        {
            fftw.destroy_plan(_fftwplan);
            _gcHandle.Free();
        }

        /// <summary>
        ///     Извлечение из изображения значений ¤ркостей пикселей, в соответствии с указанными индексами
        /// </summary>
        /// <param name="index">Массив индексов пикселей</param>
        /// <param name="colors">Массив извлечённых значений яркостей пикселей</param>
        public void Select(int[] index, double[] colors)
        {
            var bytes = new byte[_cvBitmap.Bytes.Length];
            Array.Copy(_cvBitmap.Bytes, 0, bytes, 0, _cvBitmap.Bytes.Length);

            int j = 0;
            foreach (byte ch in bytes)
                _data[j++] = ch;

            Debug.Assert(_data.All(x => x >= 0));

            FFTW_Lock.WaitOne();
            fftw.execute(_fftwplan);
            FFTW_Lock.ReleaseMutex();

            j = 0;
            foreach (int i in index)
                colors[j++] = _data[i]/_divider;
        }

        /// <summary>
        ///     Замена в изображении, в соответствии с указанными индексами, значений яркостей пикселей
        /// </summary>
        /// <param name="index">Массив индексов пикселей</param>
        /// <param name="colors">Массив извлечённых значений яркостей пикселей</param>
        public void Replace(int[] index, double[] colors)
        {
            var bytes = new byte[_cvBitmap.Bytes.Length];
            Array.Copy(_cvBitmap.Bytes, 0, bytes, 0, _cvBitmap.Bytes.Length);

            int j = 0;
            foreach (int i in index)
                _data[i] = colors[j++];

            FFTW_Lock.WaitOne();
            fftw.execute(_fftwplan);
            FFTW_Lock.ReleaseMutex();

            j = 0;
            foreach (double d in _data)
                bytes[j++] = (byte) Math.Max(0, Math.Min(Math.Round(d/_divider), 255));

            Buffer.BlockCopy(bytes, 0, _cvBitmap.Data, 0, (int) _cvBitmap.Length);
        }

        /// <summary>
        ///     Вычисление статистических характеристик изображения
        /// </summary>
        /// <param name="average">Средняя яркость пикселей</param>
        /// <param name="delta">Дисперсия яркости пикселей</param>
        public void AverageAndDelta(out double average, out double delta)
        {
            average = _data.Average(x => x);
            delta = Math.Sqrt(_data.Average(x => x*x) - average*average);
        }

        public override string ToString()
        {
            double average;
            double delta;
            AverageAndDelta(out average, out delta);
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Length {0}", _data.Length));
            sb.AppendLine(string.Format("Average {0}", average));
            sb.AppendLine(string.Format("Delta {0}", delta));
            return sb.ToString();
        }
    }
}