using System.ComponentModel;

namespace Steganography.Options
{
    public class Barcode
    {
        /// <summary>
        ///     Список алгоритмов баркода
        /// </summary>
        public static readonly object[] ComboBoxItems =
        {
            new ComboBoxItem<BarcodeId>(BarcodeId.None, "Нет")
        };

        private readonly BarcodeId _barcodeId; // Идентификатор алгоритма сжатия данных

        public Barcode(int itemIndex)
        {
            _barcodeId = ((ComboBoxItem<BarcodeId>) ComboBoxItems[itemIndex]).HiddenValue;
        }

        [Description(@"Алгоритм компрессии")]
        [Category(@"Алгоритмы")]
        public int ArchiverIndex { get; set; }

        [Description(@"Избыточность")]
        [Category(@"Значения")]
        public int ExpandSize { get; set; }

        [Description(@"Длина кода")]
        [Category(@"Алгоритм коррекции ошибки")]
        public int EccCodeSize { get; set; }

        [Description(@"Длина данных")]
        [Category(@"Алгоритм коррекции ошибки")]
        public int EccDataSize { get; set; }

        [Description(@"Алгоритм коррекции ошибки")]
        [Category(@"Алгоритмы")]
        public int EccIndex { get; set; }

        [Description(@"Алгоритм перемешивания")]
        [Category(@"Алгоритмы")]
        public int MixerIndex { get; set; }

        [Description(@"Использовать гамму максимальной длины")]
        [Category(@"Значения")]
        public bool MaximumGamma { get; set; }

        [Description(@"Алгоритм гаммы")]
        [Category(@"Алгоритмы")]
        public int GammaIndex { get; set; }

        [Description(@"Ключ")]
        [Category(@"Значения")]
        public string Key { get; set; }

        [Description(@"Параметр фильтра")]
        [Category(@"Значения")]
        public int FilterStep { get; set; }


        /// <summary>
        ///     Идентификаторы алгоритмов баркода
        /// </summary>
        private enum BarcodeId
        {
            None = 0,
            Qr
        };
    }
}