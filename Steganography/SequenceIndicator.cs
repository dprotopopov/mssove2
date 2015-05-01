using System.IO;
using System.Security.Cryptography;

namespace Steganography
{
    public class SequenceIndicator
    {
        private const int Size = 256/8;
        private readonly Comparer _comparer = new Comparer();
        private readonly SHA256 _sha256 = SHA256.Create();

        public void Envelop(Stream input, Stream enveloped)
        {
            byte[] indicator = _sha256.ComputeHash(input);
            input.Seek(0, SeekOrigin.Begin);
            enveloped.Write(indicator, 0, indicator.Length);
            input.CopyTo(enveloped);
            enveloped.Write(indicator, 0, indicator.Length);
        }

        public void Extract(Stream enveloped, Stream output)
        {
            var hash = new byte[Size];
            var shift = new byte[Size];
            enveloped.Read(hash, 0, Size);
            enveloped.Read(shift, 0, Size);
            while (_comparer.Compare(hash, shift) != 0)
            {
                output.WriteByte(shift[0]);
                for (int i = 1; i < Size; i++) shift[i - 1] = shift[i];
                int b = enveloped.ReadByte();
                if (b < 0) break;
                shift[Size - 1] = (byte) b;
            }
        }
    }
}