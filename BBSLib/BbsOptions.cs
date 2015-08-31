using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using BBSLib.Options;

namespace BBSLib
{
    /// <summary>
    ///     http://www.codeproject.com/Articles/1789/Object-Serialization-using-C
    /// </summary>
    [Serializable]
    public sealed class BbsOptions : ISerializable //derive your class from ISerializable
    {
        public BbsOptions()
        {
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
            PixelFormatIndex = (int) info.GetValue("PixelFormatIndex", typeof (int));
            PoliticIndex = (int) info.GetValue("PoliticIndex", typeof (int));

            Alpha = (int) info.GetValue("Alpha", typeof (int));
            ExpandSize = (int) info.GetValue("ExpandSize", typeof (int));
            EccCodeSize = (int) info.GetValue("EccCodeSize", typeof (int));
            EccDataSize = (int) info.GetValue("EccDataSize", typeof (int));
            FilterStep = (int) info.GetValue("FilterStep", typeof (int));

            ExtractBarcode = (bool) info.GetValue("ExtractBarcode", typeof (bool));
            AutoResize = (bool) info.GetValue("AutoResize", typeof (bool));
            AutoAlpha = (bool) info.GetValue("AutoAlpha", typeof (bool));
            MaximumGamma = (bool) info.GetValue("MaximumGamma", typeof (bool));

            Key = (string) info.GetValue("Key", typeof (string));
            PoliticText = (string) info.GetValue("PoliticText", typeof (string));
            IndexToObject();
        }

        [Description(@"Глубина погружения")]
        [Category(@"Значения")]
        public int Alpha { get; set; }

        [Description(@"Алгоритм компрессии")]
        [Category(@"Алгоритмы")]
        public object ArchiverComboBoxItem { get; set; }

        [Browsable(false)]
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

        [Description(@"Параметр фильтра")]
        [Category(@"Значения")]
        public int FilterStep { get; set; }

        [Browsable(false)]
        [Description(@"Встраиваемый баркод")]
        [Category(@"Значения")]
        public int BarcodeIndex { get; set; }

        [Description(@"Извлекать баркод")]
        [Category(@"Значения")]
        public bool ExtractBarcode { get; set; }

        [Description(@"Встраиваемый баркод")]
        [Category(@"Значения")]
        public object BarcodeComboBoxItem { get; set; }

        [Description(@"Алгоритм гаммы")]
        [Category(@"Алгоритмы")]
        public object GammaComboBoxItem { get; set; }

        [Browsable(false)]
        [Description(@"Алгоритм гаммы")]
        [Category(@"Алгоритмы")]
        public int GammaIndex { get; set; }

        [Description(@"Алгоритм коррекции ошибки")]
        [Category(@"Алгоритмы")]
        // ReSharper disable MemberCanBePrivate.Global
        public object EccComboBoxItem { get; set; }

        [Browsable(false)]
        [Description(@"Алгоритм коррекции ошибки")]
        [Category(@"Алгоритмы")]
        public int EccIndex { get; set; }

        [Description(@"Алгоритм перемешивания")]
        [Category(@"Алгоритмы")]
        public object MixerComboBoxItem { get; set; }

        [Browsable(false)]
        [Description(@"Алгоритм перемешивания")]
        [Category(@"Алгоритмы")]
        public int MixerIndex { get; set; }

        [Description(@"Формат пикселей")]
        [Category(@"Значения")]
        public object PixelFormatComboBoxItem { get; set; }

        [Browsable(false)]
        [Description(@"Формат пикселей")]
        [Category(@"Значения")]
        public int PixelFormatIndex { get; set; }

        [Description(@"Политика заполения лишних пикселей")]
        [Category(@"Политика заполения лишних пикселей")]
        public object PoliticComboBoxItem { get; set; }

        [Browsable(false)]
        [Description(@"Политика заполения лишних пикселей")]
        [Category(@"Политика заполения лишних пикселей")]
        public int PoliticIndex { get; set; }

        [Description(@"Альтернативное сообщение")]
        [Category(@"Политика заполения лишних пикселей")]
        public string PoliticText { get; set; }

        [Description(@"Маштабировать изображение")]
        [Category(@"Значения")]
        public bool AutoResize { get; set; }

        [Description(@"Подбирать глубину погружения")]
        [Category(@"Значения")]
        public bool AutoAlpha { get; set; }

        [Description(@"Использовать гамму максимальной длины")]
        [Category(@"Значения")]
        public bool MaximumGamma { get; set; }

        [Description(@"Ключ")]
        [Category(@"Значения")]
        public string Key { get; set; }

        #region

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

        // Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            ObjectToIndex();
            //You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"
            info.AddValue("ArchiverIndex", ArchiverIndex);
            info.AddValue("BarcodeIndex", BarcodeIndex);
            info.AddValue("EccIndex", EccIndex);
            info.AddValue("GammaIndex", GammaIndex);
            info.AddValue("MixerIndex", MixerIndex);
            info.AddValue("PixelFormatIndex", PixelFormatIndex);
            info.AddValue("PoliticIndex", PoliticIndex);

            info.AddValue("Alpha", Alpha);
            info.AddValue("ExpandSize", ExpandSize);
            info.AddValue("EccCodeSize", EccCodeSize);
            info.AddValue("EccDataSize", EccDataSize);
            info.AddValue("FilterStep", FilterStep);

            info.AddValue("MaximumGamma", MaximumGamma);
            info.AddValue("ExtractBarcode", ExtractBarcode);
            info.AddValue("AutoResize", AutoResize);
            info.AddValue("AutoAlpha", AutoAlpha);

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
            sb.AppendLine(string.Format("PixelFormatIndex {0}", PixelFormatIndex));
            sb.AppendLine(string.Format("PoliticIndex {0}", PoliticIndex));

            sb.AppendLine(string.Format("Alpha {0}", Alpha));
            sb.AppendLine(string.Format("ExpandSize {0}", ExpandSize));
            sb.AppendLine(string.Format("EccCodeSize {0}", EccCodeSize));
            sb.AppendLine(string.Format("EccDataSize {0}", EccDataSize));
            sb.AppendLine(string.Format("FilterStep {0}", FilterStep));

            sb.AppendLine(string.Format("ExtractBarcode {0}", ExtractBarcode));
            sb.AppendLine(string.Format("AutoResize {0}", AutoResize));
            sb.AppendLine(string.Format("AutoAlpha {0}", AutoAlpha));
            sb.AppendLine(string.Format("MaximumGamma {0}", MaximumGamma));

            sb.AppendLine(string.Format("Key {0}", Key));
            sb.AppendLine(string.Format("PoliticText {0}", PoliticText));
            return sb.ToString();
        }

        public void IndexToObject()
        {
            EccComboBoxItem = Ecc.ComboBoxItems[EccIndex];
            MixerComboBoxItem = Mixer.ComboBoxItems[MixerIndex];
            GammaComboBoxItem = Gamma.ComboBoxItems[GammaIndex];
            ArchiverComboBoxItem = Archiver.ComboBoxItems[ArchiverIndex];
            PoliticComboBoxItem = Politic.ComboBoxItems[PoliticIndex];
            PixelFormatComboBoxItem = CvBitmap.ComboBoxItems[PixelFormatIndex];
            BarcodeComboBoxItem = Barcode.ComboBoxItems[BarcodeIndex];
        }

        public void ObjectToIndex()
        {
            EccIndex = Array.IndexOf(Ecc.ComboBoxItems, EccComboBoxItem);
            MixerIndex = Array.IndexOf(Mixer.ComboBoxItems, MixerComboBoxItem);
            GammaIndex = Array.IndexOf(Gamma.ComboBoxItems, GammaComboBoxItem);
            ArchiverIndex = Array.IndexOf(Archiver.ComboBoxItems, ArchiverComboBoxItem);
            PoliticIndex = Array.IndexOf(Politic.ComboBoxItems, PoliticComboBoxItem);
            PixelFormatIndex = Array.IndexOf(CvBitmap.ComboBoxItems, PixelFormatComboBoxItem);
            BarcodeIndex = Array.IndexOf(Barcode.ComboBoxItems, BarcodeComboBoxItem);
        }
    }
}