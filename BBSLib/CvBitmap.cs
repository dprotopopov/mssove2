using System;
using System.Diagnostics;
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
    ///      Класс инструментов для работы с изображениями формата BMP
    /// </summary>
    public class CvBitmap : IDisposable
    {
        public static readonly object[] ComboBoxItems =
        {
            new ComboBoxItem(0, "Оригинал"),
            new ComboBoxItem(1, "Grey"),
            new ComboBoxItem(3, "RGB")
        };

        public CvBitmap(string fileName)
        {
            using (var bitmap = new Bitmap(fileName))
                switch (NumberOfChannels = GetNumberOfChannels(bitmap.PixelFormat))
                {
                    case 1:
                        Image = new Image<Gray, Byte>(bitmap);
                        break;
                    case 3:
                    case 4:
                        Image = new Image<Bgr, Byte>(bitmap);
                        break;
                    default:
                        throw new NotImplementedException();
                }
        }

        public CvBitmap(CvBitmap input, long requiredLength, int itemIndex, bool autoResize, Size minSize)
        {
            Image<Bgr, Byte> temp;
            switch (input.NumberOfChannels)
            {
                case 1:
                    temp = ((Image<Gray, Byte>)input.Image).Convert<Bgr, Byte>();
                    break;
                case 3:
                case 4:
                    temp = ((Image<Bgr, Byte>)input.Image).Convert<Bgr, Byte>();
                    break;
                default:
                    throw new NotImplementedException();
            }

            int numberOfChannels = ((ComboBoxItem)ComboBoxItems[itemIndex]).HiddenValue;
            if (numberOfChannels == 0) numberOfChannels = temp.NumberOfChannels;
            NumberOfChannels = numberOfChannels;

            if (autoResize)
            {
                Size size = temp.Size;
                double ratio = Math.Max(Math.Max(Math.Sqrt((double)requiredLength / (size.Height * size.Width * numberOfChannels)),
                    (double)minSize.Width / size.Width),
                    (double)minSize.Height / size.Height);
                var imageSize = new Size((int)Math.Ceiling(ratio * size.Width),
                    (int)Math.Ceiling(ratio * size.Height));

                //     Resize bitmap with the Fastest Fourier Transform
                using (var builder = new StretchBuilder(imageSize))
                    temp = builder.Stretch(temp);
            }

            switch (NumberOfChannels)
            {
                case 1:
                    Image = temp.Convert<Gray, Byte>();
                    break;
                case 3:
                case 4:
                    Image = temp.Convert<Bgr, Byte>();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Blur bitmap with the Fastest Fourier Transform
        /// </summary>
        public CvBitmap(CvBitmap input, BlurBuilder builder)
        {
            switch (NumberOfChannels = input.NumberOfChannels)
            {
                case 1:
                    Image = builder.Blur(input.Image as Image<Gray, Byte>);
                    break;
                case 3:
                case 4:
                    Image = builder.Blur(input.Image as Image<Bgr, Byte>);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public CvBitmap(Bitmap bitmap)
        {
            switch (NumberOfChannels = GetNumberOfChannels(bitmap.PixelFormat))
            {
                case 1:
                    Image = new Image<Gray, Byte>(bitmap);
                    break;
                case 3:
                case 4:
                    Image = new Image<Bgr, Byte>(bitmap);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        /// <summary>
        /// Replace bitmap colors
        /// </summary>
        /// <param name="input"></param>
        /// <param name="colors"></param>
        /// <param name="index"></param>
        public CvBitmap(CvBitmap input, byte[] colors, int[] index)
        {
            switch (NumberOfChannels = input.NumberOfChannels)
            {
                case 1:
                    Image = ((Image<Gray, Byte>)input.Image).Convert<Gray, Byte>();
                    break;
                case 3:
                case 4:
                    Image = ((Image<Bgr, Byte>)input.Image).Convert<Bgr, Byte>();
                    break;
                default:
                    throw new NotImplementedException();
            }
            byte[, ,] data = Data;
            var array = new byte[data.Length];
            Buffer.BlockCopy(data, 0, array, 0, data.Length);
            using (var stream = new MemoryStream(colors))
                foreach (int i in index)
                    array[i] = (byte)stream.ReadByte();
            Buffer.BlockCopy(array, 0, data, 0, data.Length);
        }

        // ReSharper disable MemberCanBePrivate.Global
        public IImage Image { get; set; }
        // ReSharper restore MemberCanBePrivate.Global
        // ReSharper disable MemberCanBePrivate.Global
        public int NumberOfChannels { get; set; }
        // ReSharper restore MemberCanBePrivate.Global

        public int Length
        {
            get { return Data.Length; }
        }

        // ReSharper disable MemberCanBePrivate.Global
        public byte[, ,] Data
        // ReSharper restore MemberCanBePrivate.Global
        {
            get
            {
                switch (NumberOfChannels)
                {
                    case 1:
                        return ((Image<Gray, Byte>)Image).Data;
                    case 3:
                    case 4:
                        return ((Image<Bgr, Byte>)Image).Data;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        private int GetNumberOfChannels(PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case PixelFormat.Format8bppIndexed:
                case PixelFormat.Format16bppGrayScale:
                    return 1;
                case PixelFormat.Format16bppRgb555:
                case PixelFormat.Format16bppRgb565:
                case PixelFormat.Format24bppRgb:
                case PixelFormat.Format32bppRgb:
                case PixelFormat.Format48bppRgb:
                case PixelFormat.Format16bppArgb1555:
                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                case PixelFormat.Format64bppArgb:
                case PixelFormat.Format64bppPArgb:
                    return 3;
                default:
                    throw new NotImplementedException();
            }
        }

        public void Select(int[] index, byte[] colors)
        {
            Debug.Assert(index.Length == colors.Length);
            int length = Math.Min(index.Length, colors.Length);
            byte[, ,] data = Data;
            Debug.Assert(index.Length <= data.Length);
            var array = new byte[data.Length];
            Buffer.BlockCopy(data, 0, array, 0, data.Length);
            using (var stream = new MemoryStream(colors))
                foreach (int i in index)
                    stream.WriteByte(array[i]);
        }

        public void Save(string fileName)
        {
            Image.Bitmap.Save(fileName, ImageFormat.Bmp);
        }

        public Bitmap GetBitmap()
        {
            return Image.Bitmap;
        }

        public void DrawCopyright(CvBitmap barcodeBitmap)
        {
            IImage image = Image;
            IImage barcodeImage = barcodeBitmap.Image;
            using (Bitmap bitmap = image.Bitmap)
            using (Graphics gfx = Graphics.FromImage(bitmap))
            {
                Size size = barcodeImage.Size;
                var pt = new Point(image.Size - new Size(size.Width * 3 / 2, size.Height * 3 / 2));
                gfx.DrawImage(barcodeImage.Bitmap, pt.X, pt.Y, size.Width, size.Height);
                switch (NumberOfChannels)
                {
                    case 1:
                        Image = new Image<Gray, Byte>(bitmap);
                        break;
                    case 3:
                    case 4:
                        Image = new Image<Bgr, Byte>(bitmap);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public override string ToString()
        {
            double average;
            double delta;
            AverageAndDelta(out average, out delta);
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("NumberOfChannels {0}", NumberOfChannels));
            sb.AppendLine(string.Format("Length {0}", Length));
            sb.AppendLine(string.Format("Average {0}", average));
            sb.AppendLine(string.Format("Delta {0}", delta));
            return sb.ToString();
        }

        public void AverageAndDelta(out double average, out double delta)
        {
            byte[, ,] data = Data;
            int length = data.Length;
            var bytes = new byte[length];
            Buffer.BlockCopy(data, 0, bytes, 0, length);
            average = bytes.Average(x => (double)x);
            delta = Math.Sqrt(bytes.Average(x => (double)x * x) - average * average);
        }

        private class ComboBoxItem
        {
            private readonly string _displayValue;
            private readonly int _hiddenValue;

            //Constructor
            public ComboBoxItem(int h, string d)
            {
                _displayValue = d;
                _hiddenValue = h;
            }

            //Accessor
            public int HiddenValue
            {
                get { return _hiddenValue; }
            }

            //Override ToString method
            public override string ToString()
            {
                return _displayValue;
            }
        }
    }
}