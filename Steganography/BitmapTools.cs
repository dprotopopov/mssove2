using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Steganography
{
    /// <summary>
    ///  Класс инструментов для работы с изображениями формата BMP
    /// </summary>
    public class BitmapTools
    {
        private readonly Bitmap _bitmap;

        public BitmapTools(Bitmap bitmap)
        {
            Debug.Assert(bitmap.PixelFormat == PixelFormat.Format32bppArgb);
            _bitmap = bitmap;
        }

        public void Replace(int[] index, byte[] colors)
        {
            Debug.Assert(index.Length <= colors.Length);
            Debug.Assert(index.Length <= _bitmap.Width*_bitmap.Height*3);
            var values = new byte[_bitmap.Width*_bitmap.Height*3];
            for (int row = 0; row < _bitmap.Height; row++)
            {
                var rect = new Rectangle(0, row, _bitmap.Width, 1);
                BitmapData bmpData =
                    _bitmap.LockBits(rect, ImageLockMode.ReadWrite,
                        _bitmap.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                var temp = new byte[_bitmap.Width*4];
                Marshal.Copy(ptr, temp, 0, _bitmap.Width*4);
                _bitmap.UnlockBits(bmpData);
                for (int i = 0; i < _bitmap.Width; i++)
                {
                    values[row*_bitmap.Width*3 + i*3 + 0] = temp[i*4 + 0];
                    values[row*_bitmap.Width*3 + i*3 + 1] = temp[i*4 + 1];
                    values[row*_bitmap.Width*3 + i*3 + 2] = temp[i*4 + 2];
                }
            }
            for (int i = 0; i < index.Length; i++) values[index[i]] = colors[i];
            for (int row = 0; row < _bitmap.Height; row++)
            {
                var rect = new Rectangle(0, row, _bitmap.Width, 1);
                var temp = new byte[_bitmap.Width*4];
                for (int i = 0; i < _bitmap.Width; i++)
                {
                    temp[i*4 + 0] = values[row*_bitmap.Width*3 + i*3 + 0];
                    temp[i*4 + 1] = values[row*_bitmap.Width*3 + i*3 + 1];
                    temp[i*4 + 2] = values[row*_bitmap.Width*3 + i*3 + 2];
                    temp[i*4 + 3] = 255;
                }
                BitmapData bmpData =
                    _bitmap.LockBits(rect, ImageLockMode.ReadWrite,
                        _bitmap.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                Marshal.Copy(temp, 0, ptr, _bitmap.Width*4);
                _bitmap.UnlockBits(bmpData);
            }
        }

        public byte[] Select(int[] index)
        {
            Debug.Assert(index.Length <= _bitmap.Width*_bitmap.Height*3);
            var values = new byte[_bitmap.Width*_bitmap.Height*3];
            for (int row = 0; row < _bitmap.Height; row++)
            {
                var rect = new Rectangle(0, row, _bitmap.Width, 1);
                BitmapData bmpData =
                    _bitmap.LockBits(rect, ImageLockMode.ReadWrite,
                        _bitmap.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                var temp = new byte[_bitmap.Width*4];
                Marshal.Copy(ptr, temp, 0, _bitmap.Width*4);
                _bitmap.UnlockBits(bmpData);
                for (int i = 0; i < _bitmap.Width; i++)
                {
                    values[row*_bitmap.Width*3 + i*3 + 0] = temp[i*4 + 0];
                    values[row*_bitmap.Width*3 + i*3 + 1] = temp[i*4 + 1];
                    values[row*_bitmap.Width*3 + i*3 + 2] = temp[i*4 + 2];
                }
            }
            var colors = new byte[index.Length];
            for (int i = 0; i < index.Length; i++) colors[i] = values[index[i]];
            return colors;
        }
    }
}