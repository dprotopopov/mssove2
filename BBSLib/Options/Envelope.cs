using System;
using System.IO;

namespace BBSLib.Options
{
    public class Envelope : IStreamTransform
    {
        public void Forward(Stream input, Stream output)
        {
            var count = (int) input.Length;
            var length = new byte[4];
            Array.Copy(BitConverter.GetBytes(count), length, 4);
            output.Write(length, 0, 4);
            input.CopyTo(output);
        }

        public void Backward(Stream input, Stream output)
        {
            var length = new byte[4];
            input.Read(length, 0, length.Length);
            Int32 count = BitConverter.ToInt32(length, 0) & 0x7FFFFFFF;
            input.CopyTo(output);
            if (output.Length > count)
                output.SetLength(count);
        }

        public void Dispose()
        {
        }
    }
}