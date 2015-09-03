using System;
using System.IO;
using System.Linq;

namespace BBSLib.Options
{
    /// <summary>
    ///      Класс алгоритмов-пустышек
    /// </summary>
    public static class None
    {
        /// <summary>
        /// Заполнение массива элементов индексами этих элементов
        /// </summary>
        /// <param name="buffer"></param>
        public static void Identity(int[] buffer)
        {
            int len = buffer.Length;
            Array.Copy(Enumerable.Range(0, len).ToArray(),buffer,len);
        }

        /// <summary>
        /// Заполнение массива элементов нулями
        /// </summary>
        /// <param name="buffer"></param>
        public static void Zero(int[] buffer)
        {
            Array.Clear(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Простейший обработчик потока данных
        /// Прямая обработка данных
        /// Копирование данных из входного потока в выходной поток
        /// </summary>
        /// <param name="input">Входной поток данных</param>
        /// <param name="output">Выходной поток данных</param>
        public static void Forward(Stream input, Stream output)
        {
            input.CopyTo(output);
        }

        /// <summary>
        /// Простейший обработчик потока данных
        /// Обратная обработка данных
        /// Копирование данных из входного потока в выходной поток
        /// </summary>
        /// <param name="input">Входной поток данных</param>
        /// <param name="output">Выходной поток данных</param>
        public static void Backward(Stream input, Stream output)
        {
            input.CopyTo(output);
        }
    }
}