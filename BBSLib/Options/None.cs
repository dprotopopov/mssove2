using System.IO;
using System.Linq;

namespace BBSLib.Options
{
    /// <summary>
    ///      ласс аглоритмов-пустышек
    /// </summary>
    public  class None
    {

        public static int[] Identity(int len)
        {
            return Enumerable.Range(0, len).ToArray();
        }

        public static byte[] Zero(int len)
        {
            return Enumerable.Repeat<byte>(0, len).ToArray();
        }

        public static void Forward(Stream input, Stream output)
        {
            input.CopyTo(output);
        }

        public static void Backward(Stream input, Stream output)
        {
            input.CopyTo(output);
        }
    }
}