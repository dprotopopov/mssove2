using System;
using System.Diagnostics;

namespace Steganography
{
    /// <summary>
    ///     Все СГС-НЗБ не выдерживают атаки по удалению вложенных сообщений даже при сохранении при этом высокого качества ПС.
    ///     Эта атака реализуется при помощи рандомизации НЗБ во временной или частотной области. Для защиты от такой атаки
    ///     необходимо использовать широкополосные сигналы (ШПС-СГ):
    /// </summary>
    public class BroadbandSignals
    {
        private const int BitsPerByte = 8;
        private readonly int _expandSize;

        public BroadbandSignals(int expandSize)
        {
            _expandSize = expandSize;
        }

        public byte[] EncodeAndCombine(byte[] colors, byte[] data, byte[] gamma, byte alpha)
        {
            Debug.Assert(colors.Length >= data.Length*BitsPerByte*_expandSize);
            Debug.Assert(gamma.Length*BitsPerByte >= _expandSize);

            var result = new byte[colors.Length];
            Array.Copy(colors, result, colors.Length);

            ulong delta = alpha;
            int index = 0;
            for (int i = 0; i < data.Length*BitsPerByte; i++)
                for (int m = 0; m < _expandSize; m++)
                {
                    ulong color = colors[index];
                    int b = (data[i >> 3] >> (i & 7)) & 1;
                    int g = (gamma[m >> 3] >> (m & 7)) & 1;
                    if ((b ^ g) == 0) color += delta;
                    else if (delta <= color) color -= delta;
                    else color = 0;
                    if (color > 255) color = 255;
                    result[index++] = (byte) color;
                }
            return result;
        }

        public byte[] DecodeAndExtract(byte[] colors, byte[] gamma, byte[] median)
        {
            Debug.Assert(gamma.Length*BitsPerByte >= _expandSize);

            int count = colors.Length/BitsPerByte/_expandSize; // Длина строки в байтах
            var votes = new long[BitsPerByte*count];
            var result = new byte[count];

            Array.Clear(votes, 0, votes.Length);
            Array.Clear(result, 0, result.Length);

            int index = 0;
            for (int i = 0; i < BitsPerByte*count; i++)
                for (int m = 0; m < _expandSize; m++)
                {
                    ulong value1 = colors[index];
                    ulong value2 = median[index];
                    long d = (long) value1 - (long) value2;
                    votes[i] += (((gamma[m >> 3] >> (m & 7)) & 1) == 0) ? d : -d;
                    index++;
                }
            for (int i = 0; i < BitsPerByte*count; i++)
                if (votes[i] < 0)
                    result[i >> 3] |= (byte) (1 << (i & 7));
            return result;
        }
    }
}