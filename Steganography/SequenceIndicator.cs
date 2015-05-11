using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Steganography
{
    public class SequenceIndicator
    {
        private static readonly SHA256 HashFn = SHA256.Create();

        private static int HashSize
        {
            get { return HashFn.ComputeHash(Encoding.Default.GetBytes(string.Empty)).Length; }
        }

        public void Envelop(Stream input, Stream enveloped)
        {
            byte[] eof = HashFn.ComputeHash(input);
            input.Seek(0, SeekOrigin.Begin);
            enveloped.Write(eof, 0, eof.Length);
            input.CopyTo(enveloped);
            enveloped.Write(eof, 0, eof.Length);
        }

        public void Extract(Stream enveloped, Stream output)
        {
            var shift = new byte[HashSize];
            var buffer = new byte[HashSize];
            var eof = new byte[HashSize];
            enveloped.Read(eof, 0, eof.Length);
            int count = enveloped.Read(shift, 0, shift.Length);
            while (count == shift.Length && !eof.SequenceEqual(shift))
            {
                output.WriteByte(shift[0]);
                Array.Copy(shift, 1, buffer, 0, --count);
                byte[] bytes = buffer;
                buffer = shift;
                shift = bytes;
                int b = enveloped.ReadByte();
                if (b >= 0) shift[count++] = (byte) b;
            }
            if (count < shift.Length) output.Write(shift, 0, count);
        }
    }
}