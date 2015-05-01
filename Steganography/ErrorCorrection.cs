using System;
using System.IO;
using System.Security.Cryptography;

namespace Steganography
{
    public class ErrorCorrection
    {
        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();
        private readonly int _expand;
        private readonly int _total;

        public ErrorCorrection(int total, int expand)
        {
            _total = total;
            _expand = expand;
        }

        public void Encode(Stream input, Stream output)
        {
            int bytes = _total/_expand;
            var data = new byte[bytes - input.Length];
            for (int i = 0; i < _expand; i++)
            {
                input.CopyTo(output);
                Rng.GetBytes(data);
                output.Write(data, 0, data.Length);
                input.Seek(0, SeekOrigin.Begin);
            }
        }

        public void Decode(Stream input, Stream output)
        {
            int bytes = _total/_expand;
            var votes = new int[8*bytes];
            var buffer = new byte[8*bytes];
            Array.Clear(votes, 0, votes.Length);
            for (int i = 0; i < _expand; i++)
            {
                input.Read(buffer, 0, 8*bytes);
                for (int j = 0; j < 8*bytes; j++)
                    if (((buffer[j >> 3] >> (j & 7)) & 1) != 0) votes[j]++;
                    else votes[j]--;
            }

            int index = 0;
            for (int i = 0; i < bytes; i++)
            {
                byte bits = 0;
                for (int j = 0; j < 8; j++)
                    if (votes[index++] > 0)
                        bits |= (byte) (1 << j);
                output.WriteByte(bits);
            }
        }
    }
}