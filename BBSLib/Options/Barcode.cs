using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using ZXing;

namespace BBSLib.Options
{
    /// <summary>
    ///     Класс применяемых в программе алгоритмов формирования баркода
    ///     Кодируемые и декодированные данные передаются через аттрибуты класса
    /// </summary>
    public class Barcode : IDisposable
    {
        private const int BitsPerByte = 8; // Количество битов в байте
        private static object[] _comboBoxItems; // Список значений для комбо-бокса
        private readonly int _barcodeId; // Идентификатор выбранного алгоритма формирования баркода
        private Bitmap _bitmap; // Изображение содержащее баркод

        /// <summary>
        ///     Конструктор для работы в режиме формирования баркода
        /// </summary>
        /// <param name="itemIndex">Идентификатор выбранного алгоритма формирования баркода</param>
        public Barcode(int itemIndex)
        {
            switch (itemIndex)
            {
                case 0:
                    _barcodeId = 0;
                    break;
                default:
                    _barcodeId = (int) ((ComboBoxItem<BarcodeFormat>) ComboBoxItems[itemIndex]).HiddenValue;
                    break;
            }
        }

        /// <summary>
        ///     Конструктор для работы в режиме декодирования баркода
        /// </summary>
        /// <param name="bitmap">Изображение содержащее баркод</param>
        public Barcode(Bitmap bitmap)
        {
            _bitmap = bitmap;
        }

        /// <summary>
        ///     Список алгоритмов баркода
        /// </summary>
        public static object[] ComboBoxItems
        {
            get
            {
                if (_comboBoxItems != null) return _comboBoxItems;
                var list = new List<object>
                {
                    new ComboBoxItem<BarcodeFormat>(0, "Нет")
                };
                list.AddRange(
                    (from object item in Enum.GetValues(typeof (BarcodeFormat))
                        select new ComboBoxItem<BarcodeFormat>((BarcodeFormat) item, item.ToString())));
                return _comboBoxItems = list.ToArray();
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("MaximumGamma {0}", MaximumGamma));
            sb.AppendLine(string.Format("DhtMode {0}", DhtMode));
            sb.AppendLine(string.Format("ExpandSize {0}", ExpandSize));
            sb.AppendLine(string.Format("EccCodeSize {0}", EccCodeSize));
            sb.AppendLine(string.Format("EccDataSize {0}", EccDataSize));
            sb.AppendLine(string.Format("Key {0}", Key));
            sb.AppendLine(string.Format("EccIndex {0}", EccIndex));
            sb.AppendLine(string.Format("MixerIndex {0}", MixerIndex));
            sb.AppendLine(string.Format("GammaIndex {0}", GammaIndex));
            sb.AppendLine(string.Format("ArchiverIndex {0}", ArchiverIndex));
            return sb.ToString();
        }

        /// <summary>
        ///     Формирования баркода выбранным алгоритмом
        ///     Кодируемые и декодированные данные передаются через аттрибуты класса
        /// </summary>
        public Bitmap Encode()
        {
            var flags = new byte[1];
            var bitArray = new BitArray(BitsPerByte*flags.Length);
            bitArray[0] = DhtMode;
            bitArray[1] = AutoAlpha;
            bitArray[2] = MaximumGamma;
            bitArray.CopyTo(flags, 0);

            var sizeOfBinaryData = Marshal.SizeOf(typeof (BinaryData));
            var binaryData = new BinaryData
            {
                ArchiverIndex = (byte) ArchiverIndex,
                MixerIndex = (byte) MixerIndex,
                GammaIndex = (byte) GammaIndex,
                EccIndex = (byte) EccIndex,
                ExpandSize = (byte) ExpandSize,
                EccCodeSize = (byte) EccCodeSize,
                EccDataSize = (byte) EccDataSize,
                Flags = flags[0]
            };
            var keyBytes = Encoding.GetEncoding(0).GetBytes(Key);
            var binaryBytes = new byte[sizeOfBinaryData];
            var ptr = Marshal.AllocHGlobal(binaryBytes.Length);
            Marshal.StructureToPtr(binaryData, ptr, true);
            Marshal.Copy(ptr, binaryBytes, 0x0, binaryBytes.Length);
            Marshal.FreeHGlobal(ptr);
            var text = Convert.ToBase64String(binaryBytes.Concat(keyBytes).ToArray());
            switch (_barcodeId)
            {
                case 0:
                    throw new ArgumentNullException();
                default:
                    var writer = new BarcodeWriter
                    {
                        Format = (BarcodeFormat) _barcodeId
                    };
                    var result = writer.Write(text);
                    using (var image = new Image<Gray, byte>(result))
                        return _bitmap = image.Bitmap;
            }
        }

        /// <summary>
        ///     Декодирование баркода
        ///     Кодируемые и декодированные данные передаются через аттрибуты класса
        /// </summary>
        public string Decode()
        {
            using (var image = new Image<Gray, byte>(_bitmap))
            {
                using (var bw = image.Convert(b => (byte) ((b < 128) ? 0 : 255)))
                {
                    var reader = new BarcodeReader();
                    var result = reader.Decode(bw.Bitmap);
                    if (result == null) throw new Exception("Баркод не распознан");
                    var bytes = Convert.FromBase64String(result.Text);
                    var sizeOfBinaryData = Marshal.SizeOf(typeof (BinaryData));
                    var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                    var binaryData =
                        (BinaryData) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof (BinaryData));
                    handle.Free();
                    ArchiverIndex = binaryData.ArchiverIndex;
                    GammaIndex = binaryData.GammaIndex;
                    MixerIndex = binaryData.MixerIndex;
                    EccIndex = binaryData.EccIndex;
                    ExpandSize = binaryData.ExpandSize;
                    EccCodeSize = binaryData.EccCodeSize;
                    EccDataSize = binaryData.EccDataSize;

                    var flags = new byte[1];
                    flags[0] = binaryData.Flags;

                    var bitArray = new BitArray(flags);
                    DhtMode = bitArray[0];
                    AutoAlpha = bitArray[1];
                    MaximumGamma = bitArray[2];

                    Key =
                        Encoding.GetEncoding(0)
                            .GetString(
                                bytes.ToList().GetRange(sizeOfBinaryData, bytes.Length - sizeOfBinaryData).ToArray());
                    return ToString();
                }
            }
        }

        /// <summary>
        ///     Структура для упаковки в баркоде двоичных данных
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        private struct BinaryData
        {
            [FieldOffset(0)] public byte ArchiverIndex;
            [FieldOffset(1)] public byte MixerIndex;
            [FieldOffset(2)] public byte GammaIndex;
            [FieldOffset(3)] public byte EccIndex;
            [FieldOffset(4)] public byte ExpandSize;
            [FieldOffset(5)] public byte EccCodeSize;
            [FieldOffset(6)] public byte EccDataSize;
            [FieldOffset(7)] public byte Flags;
        };

        #region Кодируемые и декодированные данные

        /// <summary>
        ///     Подбирать глубину погружения
        /// </summary>
        [Description(@"Подбирать глубину погружения")]
        [Category(@"Значения")]
        public bool AutoAlpha { get; set; }

        /// <summary>
        ///     Использовать DHT образ как контейнер данных
        /// </summary>
        [Description(@"Использовать DHT образ как контейнер данных")]
        [Category(@"Значения")]
        public bool DhtMode { get; set; }

        /// <summary>
        ///     Избыточность
        /// </summary>
        [Description(@"Избыточность")]
        [Category(@"Значения")]
        public int ExpandSize { get; set; }

        [Description(@"Алгоритм компрессии")]
        [Category(@"Алгоритмы")]
        public int ArchiverIndex { get; set; }

        /// <summary>
        ///     Алгоритм псевдослучайного перемешивания данных (перестановок бит)
        /// </summary>
        [Description(@"Алгоритм псевдослучайного перемешивания данных (перестановок бит)")]
        [Category(@"Алгоритмы")]
        public int MixerIndex { get; set; }

        /// <summary>
        ///     Алгоритм формирования псевдослучайной последовательности
        /// </summary>
        [Description(@"Алгоритм формирования псевдослучайной последовательности")]
        [Category(@"Алгоритмы")]
        public int GammaIndex { get; set; }

        /// <summary>
        ///     Алгоритм коррекции ошибки
        /// </summary>
        [Description(@"Алгоритм коррекции ошибки")]
        [Category(@"Алгоритмы")]
        public int EccIndex { get; set; }

        /// <summary>
        ///     Алгоритм коррекции ошибки
        ///     Длина кода
        /// </summary>
        [Description(@"Длина кода")]
        [Category(@"Алгоритм коррекции ошибки")]
        public int EccCodeSize { get; set; }

        /// <summary>
        ///     Алгоритм коррекции ошибки
        ///     Длина данных
        /// </summary>
        [Description(@"Длина данных")]
        [Category(@"Алгоритм коррекции ошибки")]
        public int EccDataSize { get; set; }

        /// <summary>
        ///     Использовать псевдослучайную последовательность максимальной длины
        ///     То есть каждому биту данных будет соответствовать своя псевдослучайная последовательность
        /// </summary>
        [Description(@"Использовать псевдослучайную последовательность максимальной длины")]
        [Category(@"Значения")]
        public bool MaximumGamma { get; set; }

        /// <summary>
        ///     Стеганографический ключ
        /// </summary>
        [Description(@"Стеганографический ключ")]
        [Category(@"Значения")]
        public string Key { get; set; }

        #endregion
    }
}