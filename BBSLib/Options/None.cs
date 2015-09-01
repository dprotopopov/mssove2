using System;
using System.IO;
using System.Linq;

namespace BBSLib.Options
{
    /// <summary>
    ///      ласс аглоритмов-пустышек
    /// </summary>
    public  class None
    {

        public static void Identity(int[] buffer)
        {
            int len = buffer.Length;
            Array.Copy(Enumerable.Range(0, len).ToArray(),buffer,len);
        }

        public static void Zero(int[] buffer)
        {
            Array.Clear(buffer, 0, buffer.Length);
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