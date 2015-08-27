using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using Steganography.Options;

namespace Steganography
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
            Alpha = (int) info.GetValue("Alpha", typeof (int));
            ExpandSize = (int) info.GetValue("ExpandSize", typeof (int));
            EccCodeSize = (int) info.GetValue("EccCodeSize", typeof (int));
            EccDataSize = (int) info.GetValue("EccDataSize", typeof (int));
            FilterStep = (int) info.GetValue("FilterStep", typeof (int));
            BarcodeIndex = (int) info.GetValue("BarcodeIndex", typeof (int));
            ExtractBarcode = (bool) info.GetValue("ExtractBarcode", typeof (bool));
            Key = (string) info.GetValue("Key", typeof (string));
            SampleAutoresize = (bool) info.GetValue("SampleAutoresize", typeof (bool));
            MaximumGamma = (bool) info.GetValue("MaximumGamma", typeof (bool));
            PoliticIndex = (int) info.GetValue("PoliticIndex", typeof (int));
            PoliticText = (string) info.GetValue("PoliticText", typeof (string));
            EccIndex = (int) info.GetValue("EccIndex", typeof (int));
            MixerIndex = (int) info.GetValue("MixerIndex", typeof (int));
            GammaIndex = (int) info.GetValue("GammaIndex", typeof (int));
            ArchiverIndex = (int) info.GetValue("ArchiverIndex", typeof (int));
            PixelFormatIndex = (int) info.GetValue("PixelFormatIndex", typeof (int));
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

        [Browsable(false)]
        public CvBitmap MedianBitmap { get; set; }

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

        [Browsable(false)]
        public CvBitmap InputBitmap { get; set; }

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

        [Browsable(false)]
        public CvBitmap OutputBitmap { get; set; }

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
        public bool SampleAutoresize { get; set; }

        [Description(@"Использовать гамму максимальной длины")]
        [Category(@"Значения")]
        public bool MaximumGamma { get; set; }

        [Browsable(false)]
        public CvBitmap SampleBitmap { get; set; }

        [Description(@"Ключ")]
        [Category(@"Значения")]
        public string Key { get; set; }

        [Browsable(false)]
        public string RtfText { get; set; }

        // Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            ObjectToIndex();
            //You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"
            info.AddValue("Alpha", Alpha);
            info.AddValue("ExpandSize", ExpandSize);
            info.AddValue("EccCodeSize", EccCodeSize);
            info.AddValue("EccDataSize", EccDataSize);
            info.AddValue("FilterStep", FilterStep);
            info.AddValue("BarcodeIndex", BarcodeIndex);
            info.AddValue("ExtractBarcode", ExtractBarcode);
            info.AddValue("Key", Key);
            info.AddValue("SampleAutoresize", SampleAutoresize);
            info.AddValue("MaximumGamma", MaximumGamma);
            info.AddValue("PoliticIndex", PoliticIndex);
            info.AddValue("PoliticText", PoliticText);
            info.AddValue("EccIndex", EccIndex);
            info.AddValue("MixerIndex", MixerIndex);
            info.AddValue("GammaIndex", GammaIndex);
            info.AddValue("ArchiverIndex", ArchiverIndex);
            info.AddValue("PixelFormatIndex", PixelFormatIndex);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Alpha {0}", Alpha));
            sb.AppendLine(string.Format("ExpandSize {0}", ExpandSize));
            sb.AppendLine(string.Format("EccCodeSize {0}", EccCodeSize));
            sb.AppendLine(string.Format("EccDataSize {0}", EccDataSize));
            sb.AppendLine(string.Format("FilterStep {0}", FilterStep));
            sb.AppendLine(string.Format("BarcodeIndex {0}", BarcodeIndex));
            sb.AppendLine(string.Format("ExtractBarcode {0}", ExtractBarcode));
            sb.AppendLine(string.Format("Key {0}", Key));
            sb.AppendLine(string.Format("SampleAutoresize {0}", SampleAutoresize));
            sb.AppendLine(string.Format("MaximumGamma {0}", MaximumGamma));
            sb.AppendLine(string.Format("SampleAutoresize {0}", SampleAutoresize));
            sb.AppendLine(string.Format("PoliticIndex {0}", PoliticIndex));
            sb.AppendLine(string.Format("PoliticText {0}", PoliticText));
            sb.AppendLine(string.Format("EccIndex {0}", EccIndex));
            sb.AppendLine(string.Format("MixerIndex {0}", MixerIndex));
            sb.AppendLine(string.Format("GammaIndex {0}", GammaIndex));
            sb.AppendLine(string.Format("ArchiverIndex {0}", ArchiverIndex));
            sb.AppendLine(string.Format("PixelFormatIndex {0}", PixelFormatIndex));
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
            for (int i = 0; i < Ecc.ComboBoxItems.Length; i++)
                if (EccComboBoxItem.Equals(Ecc.ComboBoxItems[i]))
                {
                    EccIndex = i;
                    break;
                }
            for (int i = 0; i < Mixer.ComboBoxItems.Length; i++)
                if (MixerComboBoxItem.Equals(Mixer.ComboBoxItems[i]))
                {
                    MixerIndex = i;
                    break;
                }
            for (int i = 0; i < Gamma.ComboBoxItems.Length; i++)
                if (GammaComboBoxItem.Equals(Gamma.ComboBoxItems[i]))
                {
                    GammaIndex = i;
                    break;
                }
            for (int i = 0; i < Archiver.ComboBoxItems.Length; i++)
                if (ArchiverComboBoxItem.Equals(Archiver.ComboBoxItems[i]))
                {
                    ArchiverIndex = i;
                    break;
                }
            for (int i = 0; i < Politic.ComboBoxItems.Length; i++)
                if (PoliticComboBoxItem.Equals(Politic.ComboBoxItems[i]))
                {
                    PoliticIndex = i;
                    break;
                }
            for (int i = 0; i < CvBitmap.ComboBoxItems.Length; i++)
                if (PixelFormatComboBoxItem.Equals(CvBitmap.ComboBoxItems[i]))
                {
                    PixelFormatIndex = i;
                    break;
                }
            for (int i = 0; i < Barcode.ComboBoxItems.Length; i++)
                if (BarcodeComboBoxItem.Equals(Barcode.ComboBoxItems[i]))
                {
                    BarcodeIndex = i;
                    break;
                }
        }
    }
}