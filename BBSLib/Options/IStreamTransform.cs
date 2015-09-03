using System;
using System.IO;

namespace BBSLib.Options
{
    /// <summary>
    ///     Abstrtract interface of data stream transformation class
    /// </summary>
    public interface IStreamTransform : IDisposable
    {
        /// <summary>
        ///     Обработчик потока данных
        ///     Прямая обработка данных
        /// </summary>
        /// <param name="input">Входной поток данных</param>
        /// <param name="output">Выходной поток данных</param>
        void Forward(Stream input, Stream output);

        /// <summary>
        ///     Обработчик потока данных
        ///     Обратная обработка данных
        /// </summary>
        /// <param name="input">Входной поток данных</param>
        /// <param name="output">Выходной поток данных</param>
        void Backward(Stream input, Stream output);
    }
}