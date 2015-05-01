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
            var result = new byte[colors.Length];
            int index = 0;
            for (int m = 0; m < _expandSize; m++)
                for (int i = 0; i < data.Length*8; i++)
                {
                    var color = (int) colors[index];
                    int b = (data[i >> 3] >> (i & 7)) & 1;
                    int g = (gamma[index >> 3] >> (index & 7)) & 1;
                    if ((b ^ g) == 0) color += alpha;
                    else color -= alpha;
                    if (color < 0) color = 0;
                    if (color > 255) color = 255;
                    result[index++] = (byte) color;
                }
            return result;
        }

        public byte[] DecodeAndExtract(byte[] colors, byte[] gamma, byte[] median)
        {
            int count = colors.Length/8/_expandSize;
            var votes = new int[8*count];
            var result = new byte[count];
            Array.Clear(votes, 0, votes.Length);
            Array.Clear(result, 0, result.Length);
            for (int m = 0; m < _expandSize; m++)
                for (int i = 0; i < 8*count; i++)
                {
                    int d = colors[i] - median[i];
                    int b = (((gamma[i >> 3] >> (i & 7)) & 1) == 0) ? d : -d;
                    votes[i] += b;
                }
            for (int i = 0; i < 8 * count; i++) 
                if (votes[i] < 0) 
                    result[i >> 3] |= (byte)(1 << (i & 7));
            return result;
        }
    }
}