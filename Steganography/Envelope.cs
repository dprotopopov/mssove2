using System;
using System.IO;

namespace Steganography
{
    public class Envelope
    {
        public Stream Seal(Stream input)
        {
            Stream output = new MemoryStream();
            var count = (int) input.Length;
            var length = new byte[4];
            Array.Copy(BitConverter.GetBytes(count), length, 4);
            output.Write(length, 0, 4);
            input.CopyTo(output);
            output.Seek(0, SeekOrigin.Begin);
            return output;
        }

        public Stream Extract(Stream input)
        {
            Stream output = new MemoryStream();
            var length = new byte[4];
            input.Read(length, 0, length.Length);
            Int32 count = BitConverter.ToInt32(length, 0);
            for (long i = 0; i < count; i++) output.WriteByte((byte) input.ReadByte());
            output.Seek(0, SeekOrigin.Begin);
            return output;
        }
    }
}