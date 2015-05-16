using System.IO;
using System.Linq;

namespace Steganography
{
    public static class None
    {
        public static int[] Identity(int len)
        {
            return Enumerable.Range(0, len).ToArray();
        }

        public static byte[] Zero(int len)
        {
            return Enumerable.Repeat<byte>(0, len).ToArray();
        }

        public static void Compress(Stream input, Stream output)
        {
            input.CopyTo(output);
        }

        public static void Decompress(Stream input, Stream output)
        {
            input.CopyTo(output);
        }
    }
}