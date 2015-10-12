using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ZXing.Common.ReedSolomon;

namespace BBSLib.Options
{
    /// <summary>
    ///     Класс применяемых в программе алгоритмов коррекции ошибок
    /// </summary>
    public class Ecc : IStreamTransform
    {
        private static readonly GenericGF Gf256 = GenericGF.QR_CODE_FIELD_256;
        private static readonly ReedSolomonEncoder RsEncoder = new ReedSolomonEncoder(Gf256);
        private static readonly ReedSolomonDecoder RsDecoder = new ReedSolomonDecoder(Gf256);
        private static object[] _comboBoxItems; // Список значений для комбо-бокса

        private readonly int _codeSize;
        private readonly int _dataSize;
        private readonly EccId _eccId; // Идентификатор выбранного алгоритма коррекции ошибок

        public Ecc(int itemIndex, int codeSize, int dataSize)
        {
            _codeSize = codeSize;
            _dataSize = dataSize;
            _eccId = ((ComboBoxItem<EccId>) ComboBoxItems[itemIndex]).HiddenValue;
        }

        /// <summary>
        ///     Список алгоритмов коррекции ошибок
        /// </summary>
        public static object[] ComboBoxItems
        {
            get
            {
                if (_comboBoxItems != null) return _comboBoxItems;
                var list = new List<object>(from object item in Enum.GetValues(typeof (EccId))
                    select new ComboBoxItem<EccId>((EccId) item, item.ToString()));
                return _comboBoxItems = list.ToArray();
            }
        }


        /// <summary>
        ///     Вызов метода кодирования для текущего выбранного алгоритма коррекции ошибок
        /// </summary>
        /// <param name="input">Входной поток данных</param>
        /// <param name="output">Выходной поток данных</param>
        public void Forward(Stream input, Stream output)
        {
            var codeSize = _codeSize;
            var dataSize = _dataSize;
            var twoS = Math.Abs(codeSize - dataSize);
            Debug.Assert(codeSize > dataSize);
            switch (_eccId)
            {
                case EccId.None:
                    None.Forward(input, output);
                    break;
                case EccId.ReedSolomon:
                    var buffer = new byte[Math.Max(codeSize, dataSize)];
                    while (input.Read(buffer, 0, dataSize) > 0)
                    {
                        var array = buffer.Select(x => (int) x).ToArray();
                        RsEncoder.encode(array, twoS);
                        output.Write(array.Select(x => (byte) x).ToArray(), 0, codeSize);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Вызов метода декодирования для текущего выбранного алгоритма коррекции ошибок
        /// </summary>
        /// <param name="input">Входной поток данных</param>
        /// <param name="output">Выходной поток данных</param>
        public void Backward(Stream input, Stream output)
        {
            var codeSize = _codeSize;
            var dataSize = _dataSize;
            var twoS = Math.Abs(codeSize - dataSize);
            Debug.Assert(codeSize > dataSize);
            switch (_eccId)
            {
                case EccId.None:
                    None.Backward(input, output);
                    break;
                case EccId.ReedSolomon:
                    var buffer = new byte[Math.Max(codeSize, dataSize)];
                    while (input.Read(buffer, 0, codeSize) > 0)
                    {
                        var array = buffer.Select(x => (int) x).ToArray();
                        RsDecoder.decode(array, twoS);
                        output.Write(array.Select(x => (byte) x).ToArray(), 0, dataSize);
                    }
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
        ///     Идентификаторы алгоритмов коррекции ошибок
        /// </summary>
        private enum EccId
        {
            None = 0,
            ReedSolomon
        };
    }
}