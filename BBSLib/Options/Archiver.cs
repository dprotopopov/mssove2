using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace BBSLib.Options
{
    /// <summary>
    ///     Класс применяемых в программе алгоритмов сжатия данных
    /// </summary>
    public class Archiver : IStreamTransform
    {
        private static object[] _comboBoxItems; // Список значений для комбо-бокса
        private readonly ArchiverId _archiverId; // Идентификатор выбранного алгоритма

        public Archiver(int itemIndex)
        {
            _archiverId = ((ComboBoxItem<ArchiverId>) ComboBoxItems[itemIndex]).HiddenValue;
        }

        /// <summary>
        ///     Список алгоритмов сжатия данных
        /// </summary>
        public static object[] ComboBoxItems
        {
            get
            {
                if (_comboBoxItems != null) return _comboBoxItems;
                var list = new List<object>(from object item in Enum.GetValues(typeof (ArchiverId))
                    select new ComboBoxItem<ArchiverId>((ArchiverId) item, item.ToString()));
                return _comboBoxItems = list.ToArray();
            }
        }

        /// <summary>
        ///     Вызов метода компрессии для текущего выбранного алгоритма сжатия данных
        /// </summary>
        /// <param name="input">Входной поток данных</param>
        /// <param name="output">Выходной поток данных</param>
        public void Forward(Stream input, Stream output)
        {
            switch (_archiverId)
            {
                case ArchiverId.None:
                    None.Forward(input, output);
                    break;
                case ArchiverId.Deflate:
                    using (var compessionStream = new DeflateStream(output, CompressionMode.Compress, true))
                        input.CopyTo(compessionStream);
                    break;
                case ArchiverId.GZip:
                    using (var compessionStream = new GZipStream(output, CompressionMode.Compress, true))
                        input.CopyTo(compessionStream);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Вызов метода декомпрессии для текущего выбранного алгоритма сжатия данных
        /// </summary>
        /// <param name="input">Входной поток данных</param>
        /// <param name="output">Выходной поток данных</param>
        public void Backward(Stream input, Stream output)
        {
            switch (_archiverId)
            {
                case ArchiverId.None:
                    None.Backward(input, output);
                    break;
                case ArchiverId.Deflate:
                    using (var decompessionStream = new DeflateStream(input, CompressionMode.Decompress, true))
                        decompessionStream.CopyTo(output);
                    break;
                case ArchiverId.GZip:
                    using (var decompessionStream = new GZipStream(input, CompressionMode.Decompress, true))
                        decompessionStream.CopyTo(output);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        ///     Идентификаторы алгоритмов сжатия данных
        /// </summary>
        private enum ArchiverId
        {
            None = 0,
            Deflate,
            GZip
        };
    }
}