using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BBSLib.Options
{
    /// <summary>
    ///     Класс реализованных в программе политик
    /// </summary>
    public class Politic : IStreamTransform
    {
        /// <summary>
        ///     Идентификаторы политик
        /// </summary>
        private enum PoliticId
        {
            None,
            Zeros,
            RandomData,
            FakeText
        };

        private const int BitsPerByte = 8; // Количество битов в байте
        private static object[] _comboBoxItems;

        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();
        private readonly CvBitmap _bitmap;
        private readonly int _expandSize;
        private readonly PoliticId _politicId; // Идентификатор политики
        private readonly string _politicsText;

        public Politic(int itemIndex, string politicsText, int expandSize, CvBitmap bitmap)
        {
            _bitmap = bitmap;
            _politicsText = politicsText;
            _expandSize = expandSize;
            _politicId = ((ComboBoxItem<PoliticId>) ComboBoxItems[itemIndex]).HiddenValue;
        }

        /// <summary>
        ///     Список алгоритмов сжатия данных
        /// </summary>
        public static object[] ComboBoxItems
        {
            get
            {
                if (_comboBoxItems != null) return _comboBoxItems;
                var list = new List<object>(from object item in Enum.GetValues(typeof (PoliticId))
                    select new ComboBoxItem<PoliticId>((PoliticId) item, item.ToString()));
                return _comboBoxItems = list.ToArray();
            }
        }

        public void Dispose()
        {
        }

        /// <summary>
        ///     Вызов метода
        /// </summary>
        /// <param name="input">Входной поток данных</param>
        /// <param name="output">Выходной поток данных</param>
        public void Forward(Stream input, Stream output)
        {
            var length = (int) (_bitmap.Length/BitsPerByte/_expandSize - input.Length);
            input.CopyTo(output);
            switch (_politicId)
            {
                case PoliticId.None:
                    break;
                case PoliticId.Zeros:
                    var zeros = new byte[length];
                    Array.Clear(zeros, 0, zeros.Length);
                    output.Write(zeros, 0, zeros.Length);
                    break;
                case PoliticId.RandomData:
                    var bytes = new byte[length];
                    Rng.GetBytes(bytes);
                    output.Write(bytes, 0, bytes.Length);
                    break;
                case PoliticId.FakeText:
                    if (string.IsNullOrWhiteSpace(_politicsText)) throw new Exception("Отсутствует заполняющий текст");
                    byte[] buffer = Encoding.Default.GetBytes(_politicsText);
                    for (int i = 0; i < length; i += buffer.Length)
                        output.Write(buffer, 0, Math.Min(buffer.Length, length - i));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void Backward(Stream input, Stream output)
        {
            throw new NotImplementedException();
        }
    }
}