using System.Drawing;

namespace Steganography
{
    public class BoxFilter
    {
        #region Переменные

        private readonly int _nh;

        #endregion

        public BoxFilter(int step = 3)
        {
            int n = (step%2 == 0) ? step += 1 : step;
            _nh = n/2;
        }

        public Bitmap Filter(Bitmap bitmap)
        {
            var bitmap1 = new Bitmap(bitmap);
            var a0 = new byte[bitmap1.Height, bitmap1.Width];
            var r0 = new byte[bitmap1.Height, bitmap1.Width];
            var g0 = new byte[bitmap1.Height, bitmap1.Width];
            var b0 = new byte[bitmap1.Height, bitmap1.Width];

            for (int y = 0; y < bitmap.Height; y++)
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color c = bitmap1.GetPixel(x, y);
                    a0[y, x] = c.A;
                    r0[y, x] = c.R;
                    g0[y, x] = c.G;
                    b0[y, x] = c.B;
                }

            for (int y1 = 0; y1 < bitmap1.Height; y1++)
            {
                for (int x1 = 0; x1 < bitmap1.Width; x1++)
                {
                    int a = 0;
                    int r = 0;
                    int g = 0;
                    int b = 0;
                    int i = 0;
                    for (int y2 = -_nh; y2 <= _nh; y2++)
                    {
                        int y3 = y1 + y2;
                        if (y3 < 0) continue;
                        if (y3 >= bitmap1.Height) continue;
                        for (int x2 = -_nh; x2 <= _nh; x2++)
                        {
                            int x3 = x1 + x2;
                            if (x3 < 0) continue;
                            if (x3 >= bitmap1.Width) continue;
                            a += a0[y3, x3];
                            r += r0[y3, x3];
                            g += g0[y3, x3];
                            b += b0[y3, x3];
                            i++;
                        }
                    }
                    a /= i;
                    r /= i;
                    g /= i;
                    b /= i;
                    bitmap1.SetPixel(x1, y1, Color.FromArgb(a, r, g, b));
                }
            }
            return bitmap1;
        }
    }
}