using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Structure;
using FFTTools;

namespace Steganography
{
    /// <summary>
    ///     Класс инструментов для работы с изображениями формата BMP
    /// </summary>
    public class CvBitmap
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

        public CvBitmap(CvBitmap sample, int requiredLength, int itemIndex, bool autoResize)
        {
            Image<Bgr, Byte> temp;
            switch (sample.NumberOfChannels)
            {
                case 1:
                    temp = ((Image<Gray, Byte>) sample.Image).Convert<Bgr, Byte>();
                    break;
                case 3:
                case 4:
                    temp = ((Image<Bgr, Byte>) sample.Image).Convert<Bgr, Byte>();
                    break;
                default:
                    throw new NotImplementedException();
            }

            int numberOfChannels = ((ComboBoxItem) ComboBoxItems[itemIndex]).HiddenValue;
            if (numberOfChannels == 0) numberOfChannels = temp.NumberOfChannels;
            NumberOfChannels = numberOfChannels;

            if (autoResize)
            {
                Size size = temp.Size;
                double ratio = Math.Sqrt((double) requiredLength/(size.Height*size.Width*numberOfChannels));
                var imageSize = new Size((int) Math.Ceiling(ratio*size.Width),
                    (int) Math.Ceiling(ratio*size.Height));

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
        public CvBitmap(CvBitmap input, int filterStep)
        {
            Size size = input.Image.Size;
            var blinderSize = new Size(size.Width/filterStep, size.Height/filterStep);
            using (var builder = new BlurBuilder(blinderSize))
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
        public byte[,,] Data
// ReSharper restore MemberCanBePrivate.Global
        {
            get
            {
                switch (NumberOfChannels)
                {
                    case 1:
                        return ((Image<Gray, Byte>) Image).Data;
                    case 3:
                    case 4:
                        return ((Image<Bgr, Byte>) Image).Data;
                    default:
                        throw new NotImplementedException();
                }
            }
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

        public void Replace(int[] index, byte[] colors)
        {
            byte[,,] data = Data;
            Debug.Assert(index.Length <= colors.Length);
            Debug.Assert(index.Length <= data.Length);

            var array = new byte[data.Length];
            Buffer.BlockCopy(data, 0, array, 0, data.Length);
            for (int i = 0; i < index.Length; i++)
                array[index[i]] = colors[i];
            Buffer.BlockCopy(array, 0, data, 0, data.Length);
        }

        public byte[] Select(int[] index)
        {
            byte[,,] data = Data;
            Debug.Assert(index.Length <= data.Length);
            var array = new byte[data.Length];
            Buffer.BlockCopy(data, 0, array, 0, data.Length);
            return index.Select(i => array[i]).ToArray();
        }

        public void Save(string fileName)
        {
            Image.Bitmap.Save(fileName, ImageFormat.Bmp);
        }

        public Bitmap GetBitmap()
        {
            return Image.Bitmap;
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