using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Steganography
{
    /// <summary>
    ///     Класс инструментов для работы с изображениями формата BMP
    /// </summary>
    public class SteganographyBitmap
    {
        public static readonly object[] ComboBoxItems =
        {
            new ComboBoxItem(0, "Оригинал"),
            new ComboBoxItem(1, "Grey"),
            new ComboBoxItem(3, "RGB")
        };

        public SteganographyBitmap(string fileName)
        {
            using (var bitmap = new Bitmap(fileName))
                switch (NumberOfChannels = GetNumberOfChannels(bitmap.PixelFormat))
                {
                    case 1:
                        Image = new Image<Gray, Byte>(bitmap);
                        break;
                    case 3:
                    case 4:
                        Image = new Image<Rgb, Byte>(bitmap);
                        break;
                    default:
                        throw new NotImplementedException();
                }
        }

        public SteganographyBitmap(SteganographyBitmap sample, int requiredLength, int itemIndex, bool autoResize)
        {
            int numberOfChannels = ((ComboBoxItem) ComboBoxItems[itemIndex]).HiddenValue;
            if (numberOfChannels == 0) numberOfChannels = sample.NumberOfChannels;
            NumberOfChannels = numberOfChannels;
            Image<Rgb, Byte> temp;
            switch (sample.NumberOfChannels)
            {
                case 1:
                    temp = ((Image<Gray, Byte>) sample.Image).Convert<Rgb, Byte>();
                    break;
                case 3:
                case 4:
                    temp = ((Image<Rgb, Byte>) sample.Image).Convert<Rgb, Byte>();
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (autoResize)
            {
                Size size = temp.Size;
                double ratio = Math.Sqrt((double) requiredLength/(size.Height*size.Width*numberOfChannels));
                temp = temp.Resize(
                    (int) Math.Ceiling(ratio*size.Width),
                    (int) Math.Ceiling(ratio*size.Height),
                    INTER.CV_INTER_CUBIC);
            }

            switch (NumberOfChannels)
            {
                case 1:
                    Image = temp.Convert<Gray, Byte>();
                    break;
                case 3:
                case 4:
                    Image = temp.Convert<Rgb, Byte>();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public SteganographyBitmap(SteganographyBitmap input, int boxSize)
        {
            switch (NumberOfChannels = input.NumberOfChannels)
            {
                case 1:
                    Image = ((Image<Gray, Byte>) input.Image).SmoothBlur(boxSize, boxSize);
                    break;
                case 3:
                case 4:
                    Image = ((Image<Rgb, Byte>) input.Image).SmoothBlur(boxSize, boxSize);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private IImage Image { get; set; }
        public int NumberOfChannels { get; set; }

        public int Length
        {
            get { return Data.Length; }
        }

        public byte[,,] Data
        {
            get
            {
                switch (NumberOfChannels)
                {
                    case 1:
                        return ((Image<Gray, Byte>) Image).Data;
                    case 3:
                    case 4:
                        return ((Image<Rgb, Byte>) Image).Data;
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