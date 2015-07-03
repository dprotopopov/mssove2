using System;
using System.IO;

namespace Steganography.Utils
{
    /// <summary>
    ///     Генератор шума с заданными вероятностями 0 и 1
    /// </summary>
    public class Noise
    {
        private const int BufferSize = 32768;
        private static readonly Random Rnd = new Random((int) DateTime.Now.Ticks);
        private readonly double _p0;
        private readonly double _p1;

        public Noise(double p0, double p1)
        {
            _p0 = p0;
            _p1 = p1;
        }

        public void GetBytes(byte[] noise)
        {
            double median = (0.0*_p1 + 1.0*_p0)/(_p0 + _p1);
            for (int i = 0; i < noise.Length; i++)
            {
                byte bits = 0;
                for (int j = 0; j < 8; j++)
                    if (Rnd.NextDouble() >= median)
                        bits = (byte) (bits | (0x1 << j));
                noise[i] = bits;
            }
        }

        public void Add(Stream input, Stream output)
        {
            var buffer = new byte[BufferSize];
            var noise = new byte[BufferSize];
            int r;
            while ((r = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                GetBytes(noise);
                for (int i = 0; i < r; i++)
                    buffer[i] ^= noise[i];

                output.Write(buffer, 0, r);
            }
        }

// ReSharper disable UnusedMember.Global
        public void GetBytes(long[] noise)
// ReSharper restore UnusedMember.Global
        {
            var temp = new byte[noise.Length*8];
            GetBytes(temp);
            Buffer.BlockCopy(temp, 0, noise, 0, noise.Length*8);
        }
    }
}