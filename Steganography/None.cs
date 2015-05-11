using System.Linq;

namespace Steganography
{
    public class None
    {
        public static int[] Identity(int n)
        {
            return Enumerable.Range(0, n).ToArray();
        }

        public static byte[] Zero(int n)
        {
            return Enumerable.Repeat<byte>(0, n).ToArray();
        }
    }
}