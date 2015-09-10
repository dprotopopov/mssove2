using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using BBSLib.Options;

namespace BBSLib
{
    /// <summary>
    ///     Класс параметров, аргументов и результатов выполнения программы.
    ///     Поддерживает сохранение и восстановление параметров в-из файл(а) сериализации для их повторного использования.
    ///     http://www.codeproject.com/Articles/1789/Object-Serialization-using-C
    /// </summary>
    [Serializable]
    public sealed class BbsOptions : ISerializable, IDisposable
    {
        public BbsOptions()
        {
            // indeces
            EccIndex = 1;
            GammaIndex = 1;
            MixerIndex = 1;
            ArchiverIndex = 1;
            BarcodeIndex = 1;
            PoliticIndex = 0;
            ZipperIndex = 0;

            // ints
            EccCodeSize = 255;
            EccDataSize = 224;
            ExpandSize = 63;
            Alpha = 1;
            FilterStep = 1;
            // bools
            DhtMode = true;
            AutoResize = true;
            AutoAlpha = true;
            ExtractBarcode = true;
            MaximumGamma = true;
            // strings
            Key = "WELCOME";
            PoliticText =
                "Lorem ipsum dolor sit amet, his ea quod tation, ne sit mazim concludaturque, graece tincidunt pro ei. Vero diceret iracundia pro ea, ne eripuit mandamus mea, an usu nisl liber theophrastus. Partem mollis nostrud eam no. Duis partiendo no pro. Cu eum quot luptatum probatus. Ex per labitur incorrupte inciderint, sit sint nonumy melius ea.\n" +
                "Tollit soleat torquatos qui eu. Cu mutat debitis legendos per. Nemore partiendo ne mei. At ridens eruditi efficiantur his.\n" +
                "Petentium abhorreant definitiones mea ex. Dolore necessitatibus ad vim. No agam ubique efficiendi qui, has at dico dissentiet. Cu sed dicam omnesque, oratio ridens eum ne. Ea adolescens definiebas mel, cum eu debitis veritus. Mei purto essent cu.\n" +
                "Perfecto complectitur no vel, ex oblique offendit quo. Ad eos viris scripta postulant. Dolorem volumus eam id. Dicant consectetuer consequuntur et vim, ad sed saepe impedit. Vim ei error tibique. Vitae admodum est eu, mundi eligendi evertitur an vix. Pri doming dicunt repudiandae an, debitis inimicus has no.\n" +
                "Vidisse torquatos ius te, his ei nibh ornatus moderatius. Eu qui aperiam omittam albucius, at pro vivendo scriptorem. Has natum volumus suavitate eu, mazim consulatu imperdiet an nam. Id mei idque aliquid, ad cetero suavitate quo. Vel soluta ridens invenire id.";

            IndexToObject();
        }

        // Deserialization constructor.
        private BbsOptions(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            ArchiverIndex = (int) info.GetValue("ArchiverIndex", typeof (int));
            BarcodeIndex = (int) info.GetValue("BarcodeIndex", typeof (int));
            EccIndex = (int) info.GetValue("EccIndex", typeof (int));
            GammaIndex = (int) info.GetValue("GammaIndex", typeof (int));
            MixerIndex = (int) info.GetValue("MixerIndex", typeof (int));
            PoliticIndex = (int) info.GetValue("PoliticIndex", typeof (int));
            ZipperIndex = (int) info.GetValue("ZipperIndex", typeof (int));

            Alpha = (int) info.GetValue("Alpha", typeof (int));
            ExpandSize = (int) info.GetValue("ExpandSize", typeof (int));
            EccCodeSize = (int) info.GetValue("EccCodeSize", typeof (int));
            EccDataSize = (int) info.GetValue("EccDataSize", typeof (int));
            FilterStep = (int) info.GetValue("FilterStep", typeof (int));

            DhtMode = (bool) info.GetValue("DhtMode", typeof (bool));
            AutoAlpha = (bool) info.GetValue("AutoAlpha", typeof (bool));
            AutoResize = (bool) info.GetValue("AutoResize", typeof (bool));
            ExtractBarcode = (bool) info.GetValue("ExtractBarcode", typeof (bool));
            MaximumGamma = (bool) info.GetValue("MaximumGamma", typeof (bool));

            Key = (string) info.GetValue("Key", typeof (string));
            PoliticText = (string) info.GetValue("PoliticText", typeof (string));
            IndexToObject();
        }

        #region Элементы для работы с параметрами через комбо-боксы

        [Description(@"Алгоритм компрессии")]
        [Category(@"Алгоритмы")]
        public object ArchiverComboBoxItem { get; set; }

        [Description(@"Встраиваемый баркод")]
        [Category(@"Значения")]
        public object BarcodeComboBoxItem { get; set; }

        [Description(@"Алгоритм коррекции ошибки")]
        [Category(@"Алгоритмы")]
        public object EccComboBoxItem { get; set; }

        [Description(@"Алгоритм гаммы")]
        [Category(@"Алгоритмы")]
        public object GammaComboBoxItem { get; set; }

        [Description(@"Извлекать баркод")]
        [Category(@"Значения")]
        public bool ExtractBarcode { get; set; }

        [Description(@"Алгоритм перемешивания")]
        [Category(@"Алгоритмы")]
        public object MixerComboBoxItem { get; set; }

        [Description(@"Политика заполения лишних пикселей")]
        [Category(@"Политика заполения лишних пикселей")]
        public object PoliticComboBoxItem { get; set; }

        [Description(@"Алгоритм арифметики сложения/вычитания")]
        [Category(@"Алгоритмы")]
        public object ZipperComboBoxItem { get; set; }

        #endregion

        /// <summary>
        ///     Глубина погружения
        /// </summary>
        [Description(@"Глубина погружения")]
        [Category(@"Значения")]
        public int Alpha { get; set; }

        /// <summary>
        ///     Алгоритм арифметики сложения/вычитания
        /// </summary>
        [Browsable(false)]
        [Description(@"Алгоритм арифметики сложения/вычитания")]
        [Category(@"Алгоритмы")]
        public int ZipperIndex { get; set; }

        /// <summary>
        ///     Алгоритм компрессии
        /// </summary>
        [Browsable(false)]
        [Description(@"Алгоритм компрессии")]
        [Category(@"Алгоритмы")]
        public int ArchiverIndex { get; set; }

        /// <summary>
        ///     Избыточность
        /// </summary>
        [Description(@"Избыточность")]
        [Category(@"Значения")]
        public int ExpandSize { get; set; }

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
        ///     Параметр фильтра размытия изображения
        /// </summary>
        [Description(@"Параметр фильтра размытия изображения")]
        [Category(@"Значения")]
        public int FilterStep { get; set; }

        /// <summary>
        ///     Встраиваемый баркод
        /// </summary>
        [Browsable(false)]
        [Description(@"Встраиваемый баркод")]
        [Category(@"Значения")]
        public int BarcodeIndex { get; set; }

        /// <summary>
        ///     Алгоритм формирования псевдослучайной последовательности
        /// </summary>
        [Browsable(false)]
        [Description(@"Алгоритм формирования псевдослучайной последовательности")]
        [Category(@"Алгоритмы")]
        public int GammaIndex { get; set; }

        /// <summary>
        ///     Алгоритм коррекции ошибки
        /// </summary>
        [Browsable(false)]
        [Description(@"Алгоритм коррекции ошибки")]
        [Category(@"Алгоритмы")]
        public int EccIndex { get; set; }

        /// <summary>
        ///     Алгоритм псевдослучайного перемешивания данных (перестановок бит)
        /// </summary>
        [Browsable(false)]
        [Description(@"Алгоритм псевдослучайного перемешивания данных (перестановок бит)")]
        [Category(@"Алгоритмы")]
        public int MixerIndex { get; set; }

        /// <summary>
        ///     Политика заполения лишних пикселей
        /// </summary>
        [Browsable(false)]
        [Description(@"Политика заполения лишних пикселей")]
        [Category(@"Политика заполения лишних пикселей")]
        public int PoliticIndex { get; set; }

        /// <summary>
        ///     Политика заполения лишних пикселей
        ///     Альтернативное сообщение
        /// </summary>
        [Description(@"Альтернативное сообщение")]
        [Category(@"Политика заполения лишних пикселей")]
        public string PoliticText { get; set; }

        /// <summary>
        ///     Маштабировать исходное изображение при необходимости
        /// </summary>
        [Description(@"Маштабировать исходное изображение при необходимости")]
        [Category(@"Значения")]
        public bool AutoResize { get; set; }

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

        #region Элементы исходных данных и результатов работы программы

        [Browsable(false)]
        public string RtfText { get; set; }

        [Browsable(false)]
        public CvBitmap InputBitmap { get; set; }

        [Browsable(false)]
        public CvBitmap OutputBitmap { get; set; }

        [Browsable(false)]
        public CvBitmap SampleBitmap { get; set; }

        [Browsable(false)]
        public CvBitmap MedianBitmap { get; set; }

        #endregion

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        // Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            ObjectToIndex();
            // You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"
            info.AddValue("ArchiverIndex", ArchiverIndex);
            info.AddValue("BarcodeIndex", BarcodeIndex);
            info.AddValue("EccIndex", EccIndex);
            info.AddValue("GammaIndex", GammaIndex);
            info.AddValue("MixerIndex", MixerIndex);
            info.AddValue("PoliticIndex", PoliticIndex);
            info.AddValue("ZipperIndex", ZipperIndex);

            info.AddValue("Alpha", Alpha);
            info.AddValue("ExpandSize", ExpandSize);
            info.AddValue("EccCodeSize", EccCodeSize);
            info.AddValue("EccDataSize", EccDataSize);
            info.AddValue("FilterStep", FilterStep);

            info.AddValue("MaximumGamma", MaximumGamma);
            info.AddValue("ExtractBarcode", ExtractBarcode);
            info.AddValue("AutoResize", AutoResize);
            info.AddValue("AutoAlpha", AutoAlpha);
            info.AddValue("DhtMode", DhtMode);

            info.AddValue("Key", Key);
            info.AddValue("PoliticText", PoliticText);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("ArchiverIndex {0}", ArchiverIndex));
            sb.AppendLine(string.Format("BarcodeIndex {0}", BarcodeIndex));
            sb.AppendLine(string.Format("GammaIndex {0}", GammaIndex));
            sb.AppendLine(string.Format("EccIndex {0}", EccIndex));
            sb.AppendLine(string.Format("MixerIndex {0}", MixerIndex));
            sb.AppendLine(string.Format("PoliticIndex {0}", PoliticIndex));
            sb.AppendLine(string.Format("ZipperIndex {0}", ZipperIndex));

            sb.AppendLine(string.Format("Alpha {0}", Alpha));
            sb.AppendLine(string.Format("ExpandSize {0}", ExpandSize));
            sb.AppendLine(string.Format("EccCodeSize {0}", EccCodeSize));
            sb.AppendLine(string.Format("EccDataSize {0}", EccDataSize));
            sb.AppendLine(string.Format("FilterStep {0}", FilterStep));

            sb.AppendLine(string.Format("DhtMode {0}", DhtMode));
            sb.AppendLine(string.Format("AutoAlpha {0}", AutoAlpha));
            sb.AppendLine(string.Format("AutoResize {0}", AutoResize));
            sb.AppendLine(string.Format("ExtractBarcode {0}", ExtractBarcode));
            sb.AppendLine(string.Format("MaximumGamma {0}", MaximumGamma));

            sb.AppendLine(string.Format("Key {0}", Key));
            sb.AppendLine(string.Format("PoliticText {0}", PoliticText));
            return sb.ToString();
        }

        /// <summary>
        ///     Заполнение значений элементов для комбо-боксов текущими значениями параметров
        /// </summary>
        public void IndexToObject()
        {
            EccComboBoxItem = Ecc.ComboBoxItems[EccIndex];
            MixerComboBoxItem = Mixer.ComboBoxItems[MixerIndex];
            GammaComboBoxItem = Gamma.ComboBoxItems[GammaIndex];
            ArchiverComboBoxItem = Archiver.ComboBoxItems[ArchiverIndex];
            PoliticComboBoxItem = Politic.ComboBoxItems[PoliticIndex];
            BarcodeComboBoxItem = Barcode.ComboBoxItems[BarcodeIndex];
            ZipperComboBoxItem = Zipper.ComboBoxItems[ZipperIndex];
        }

        /// <summary>
        ///     Получение значений параметров из текущих значений элементов для комбо-боксов
        /// </summary>
        public void ObjectToIndex()
        {
            EccIndex = Array.IndexOf(Ecc.ComboBoxItems, EccComboBoxItem);
            MixerIndex = Array.IndexOf(Mixer.ComboBoxItems, MixerComboBoxItem);
            GammaIndex = Array.IndexOf(Gamma.ComboBoxItems, GammaComboBoxItem);
            ArchiverIndex = Array.IndexOf(Archiver.ComboBoxItems, ArchiverComboBoxItem);
            PoliticIndex = Array.IndexOf(Politic.ComboBoxItems, PoliticComboBoxItem);
            BarcodeIndex = Array.IndexOf(Barcode.ComboBoxItems, BarcodeComboBoxItem);
            ZipperIndex = Array.IndexOf(Zipper.ComboBoxItems, ZipperComboBoxItem);
        }
    }
}