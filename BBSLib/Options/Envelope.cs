using System;
using System.IO;

namespace BBSLib.Options
{
    /// <summary>
    ///     Класс для добавления информации о размере и положении в информационном потоке участка с передаваемыми данными,
    ///     для возможности последующего обнаружения и извлечения из этого потока участка с переданными данными
    /// </summary>
    public class Envelope : IStreamTransform
    {
        /// <summary>
        ///     Вызов метода добавления информации о размере и положении в информационном потоке участка с передаваемыми данными
        /// </summary>
        /// <param name="input">Входной поток данных</param>
        /// <param name="output">Выходной поток данных</param>
        public void Forward(Stream input, Stream output)
        {
            var count = (int) input.Length;
            var length = new byte[4];
            Array.Copy(BitConverter.GetBytes(count), length, 4);
            output.Write(length, 0, 4);
            input.CopyTo(output);
        }

        /// <summary>
        ///     Вызов метода обнаружения и извлечения из информационного потока участка с переданными данными
        /// </summary>
        /// <param name="input">Входной поток данных</param>
        /// <param name="output">Выходной поток данных</param>
        public void Backward(Stream input, Stream output)
        {
            var length = new byte[4];
            input.Read(length, 0, length.Length);
            Int32 count = BitConverter.ToInt32(length, 0) & 0x7FFFFFFF;
            input.CopyTo(output);
            if (output.Length > count)
                output.SetLength(count);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
    }
}