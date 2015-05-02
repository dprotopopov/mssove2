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
        private readonly int _expandSize;

        public BroadbandSignals(int expandSize)
        {
            _expandSize = expandSize;
        }

        public byte[] EncodeAndCombine(byte[] colors, byte[] data, byte[] gamma, int alpha)
        {
            Debug.Assert(colors.Length >= data.Length*8*_expandSize);
            Debug.Assert(gamma.Length*8 >= _expandSize);

            var result = new byte[colors.Length];
            Array.Copy(colors, result, colors.Length);

            for (int m = 0; m < _expandSize; m++)
            {
                int index = m*colors.Length/_expandSize; // Индекс начала строки
                for (int i = 0; i < data.Length*8; i++)
                {
                    var color = (int) colors[index];
                    int b = (data[i >> 3] >> (i & 7)) & 1;
                    int g = (gamma[m >> 3] >> (m & 7)) & 1;
                    if ((b ^ g) == 0) color += alpha;
                    else color -= alpha;
                    if (color < 0) color = 0;
                    if (color > 255) color = 255;
                    result[index++] = (byte) color;
                }
            }
            return result;
        }

        public byte[] DecodeAndExtract(byte[] colors, byte[] gamma, byte[] median)
        {
            Debug.Assert(gamma.Length*8 >= _expandSize);

            int count = colors.Length/8/_expandSize; // Длина строки в байтах
            var votes = new int[8*count];
            var result = new byte[count];

            Array.Clear(votes, 0, votes.Length);
            Array.Clear(result, 0, result.Length);

            for (int m = 0; m < _expandSize; m++)
            {
                int index = m*colors.Length/_expandSize; // Индекс начала строки
                for (int i = 0; i < 8*count; i++)
                {
                    int d = colors[index + i] - median[index + i];
                    int b = (((gamma[m >> 3] >> (m & 7)) & 1) == 0) ? d : -d;
                    votes[i] += b;
                }
            }
            for (int i = 0; i < 8*count; i++)
                if (votes[i] < 0)
                    result[i >> 3] |= (byte) (1 << (i & 7));
            return result;
        }
    }
}