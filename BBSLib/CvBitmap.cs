using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using FFTTools;

namespace BBSLib
{
    /// <summary>
    ///      Класс инструментов для работы с BMP изображениями 
    /// Основан на классе изображения из пакета Emgu.CV
    /// </summary>
    public class CvBitmap : Image<Bgr, Byte>, IDataContainer
    {
        public CvBitmap(string fileName)
            : base(new Bitmap(fileName))
        {
        }

        public CvBitmap(CvBitmap input, StretchBuilder builder, bool autoResize)
            : base(autoResize ? builder.Stretch(input).Data : input.Data)
        {
        }



        /// <summary>
        ///     Blur bitmap with the Fastest Fourier Transform
        /// </summary>
        public CvBitmap(CvBitmap input, BlurBuilder builder)
            : base(builder.Blur(input).Data)
        {
        }

        public CvBitmap(Bitmap bitmap)
            : base(bitmap)
        {
        }

        public CvBitmap(CvBitmap bitmap)
            : base(bitmap.Data)
        {
        }

        /// <summary>
        /// Количество пикселей в изображении
        /// </summary>
        public long Length { get { return Data.Length; } }

        /// <summary>
        /// Извлечение из изображения значений яркостей пикселей, в соответствии с указанными индексами
        /// </summary>
        /// <param name="index">Массив индексов пикселей</param>
        /// <param name="colors">Массив извлечённых значений яркостей пикселей</param>
        public void Select(int[] index, double[] colors)
        {
            var bytes = new byte[Bytes.Length];
            Array.Copy(Bytes, 0, bytes, 0, Bytes.Length);

            int j = 0;
            foreach (int i in index)
                colors[j++] = bytes[i];
        }

        /// <summary>
        /// Замена в изображении, в соответствии с указанными индексами, значений яркостей пикселей 
        /// </summary>
        /// <param name="index">Массив индексов пикселей</param>
        /// <param name="colors">Массив заменяющих значений яркостей пикселей</param>
        public void Replace(int[] index, double[] colors)
        {
            var bytes = new byte[Bytes.Length];
            Array.Copy(Bytes, 0, bytes, 0, Bytes.Length);
            int j = 0;
            foreach (int i in index)
                bytes[i] = (byte)Math.Max(0, Math.Min(colors[j++], 255));
            Buffer.BlockCopy(bytes, 0, Data, 0, Data.Length);
        }

        /// <summary>
        /// Вычисление статистических характеристик изображения
        /// </summary>
        /// <param name="average">Средняя яркость пикселей</param>
        /// <param name="delta">Дисперсия яркости пикселей</param>
        public void AverageAndDelta(out double average, out double delta)
        {
            byte[] bytes = Bytes;
            average = bytes.Average(x => (double)x);
            delta = Math.Sqrt(bytes.Average(x => (double)x * x) - average * average);
        }

        /// <summary>
        /// Извлечение из изображения значений яркостей пикселей, в соответствии с указанными индексами
        /// </summary>
        /// <param name="index">Массив индексов пикселей</param>
        /// <param name="colors">Массив извлечённых значений яркостей пикселей</param>
        public void Select(int[] index, byte[] colors)
        {
            var bytes = new byte[Bytes.Length];
            Array.Copy(Bytes, 0, bytes, 0, Bytes.Length);

            using (var stream = new MemoryStream(colors))
                foreach (int i in index)
                    stream.WriteByte(bytes[i]);
        }

        /// <summary>
        /// Замена в изображении, в соответствии с указанными индексами, значений яркостей пикселей 
        /// </summary>
        /// <param name="index">Массив индексов пикселей</param>
        /// <param name="colors">Массив заменяющих значений яркостей пикселей</param>
        public void Replace(int[] index, byte[] colors)
        {
            var bytes = new byte[Bytes.Length];
            Array.Copy(Bytes, 0, bytes, 0, Bytes.Length);

            using (var stream = new MemoryStream(colors))
                foreach (int i in index)
                    bytes[i] = (byte)stream.ReadByte();
            Buffer.BlockCopy(bytes, 0, Data, 0, Data.Length);
        }

        /// <summary>
        /// Сохранение изображения в файл.
        /// Формат сохраняемого файла BMP.
        /// </summary>
        /// <param name="fileName"></param>
        public new void Save(string fileName)
        {
            Bitmap.Save(fileName, ImageFormat.Bmp);
        }
        /// <summary>
        ///  Размещение дополнительного изображения в правой нижней части.
        /// Используется для внедрения в исходное изображение баркода с настроечными параметрами программы, вклучая стеганографический ключ для выработки принимающей стороной псевдослучайной последовательности и псевдослучайной перестановки.
        /// </summary>
        /// <param name="barcodeBitmap">Внедряемое изображение</param>
        public void DrawCopyright(CvBitmap barcodeBitmap)
        {
            Size size = barcodeBitmap.Size;
            var pt = new Point(Size - new Size(size.Width * 3 / 2, size.Height * 3 / 2));
            ROI = new Rectangle(pt, size);
            barcodeBitmap.CopyTo(this);
            ROI = Rectangle.Empty;
        }

        public override string ToString()
        {
            double average;
            double delta;
            AverageAndDelta(out average, out delta);
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("NumberOfChannels {0}", NumberOfChannels));
            sb.AppendLine(string.Format("Length {0}", Data.Length));
            sb.AppendLine(string.Format("Average {0}", average));
            sb.AppendLine(string.Format("Delta {0}", delta));
            return sb.ToString();
        }
    }
}