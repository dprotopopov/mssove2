using System;
using System.ComponentModel;
using System.Runtime.Serialization;
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
            FilterStep = (int) info.GetValue("FilterStep", typeof (int));
            Key = (string) info.GetValue("Key", typeof (string));
            SampleAutoresize = (bool) info.GetValue("SampleAutoresize", typeof (bool));
            MaximumGamma = (bool) info.GetValue("MaximumGamma", typeof (bool));
            PoliticIndex = (int) info.GetValue("PoliticIndex", typeof (int));
            PoliticText = (string) info.GetValue("PoliticText", typeof (string));
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
// ReSharper disable MemberCanBePrivate.Global
        public object ArchiverComboBoxItem { get; set; }

// ReSharper restore MemberCanBePrivate.Global

        [Browsable(false)]
        [Description(@"Алгоритм компрессии")]
        [Category(@"Алгоритмы")]
        public int ArchiverIndex { get; set; }

        [Browsable(false)]
        public CvBitmap MedianBitmap { get; set; }

        [Description(@"Избыточность")]
        [Category(@"Значения")]
        public int ExpandSize { get; set; }

        [Description(@"Параметр фильтра")]
        public int FilterStep { get; set; }

        [Description(@"Алгоритм гаммы")]
        [Category(@"Алгоритмы")]
// ReSharper disable MemberCanBePrivate.Global
        public object GammaComboBoxItem { get; set; }

// ReSharper restore MemberCanBePrivate.Global

        [Browsable(false)]
        [Description(@"Алгоритм гаммы")]
        [Category(@"Алгоритмы")]
        public int GammaIndex { get; set; }

        [Browsable(false)]
        public CvBitmap InputBitmap { get; set; }

        [Description(@"Алгоритм перемешивания")]
        [Category(@"Алгоритмы")]
// ReSharper disable MemberCanBePrivate.Global
        public object MixerComboBoxItem { get; set; }

// ReSharper restore MemberCanBePrivate.Global

        [Browsable(false)]
        [Description(@"Алгоритм перемешивания")]
        [Category(@"Алгоритмы")]
        public int MixerIndex { get; set; }

        [Browsable(false)]
        public CvBitmap OutputBitmap { get; set; }

        [Description(@"Формат пикселей")]
        [Category(@"Значения")]
// ReSharper disable MemberCanBePrivate.Global
        public object PixelFormatComboBoxItem { get; set; }

// ReSharper restore MemberCanBePrivate.Global

        [Browsable(false)]
        [Description(@"Формат пикселей")]
        [Category(@"Значения")]
        public int PixelFormatIndex { get; set; }

        [Description(@"Политика заполения лишних пикселей")]
        [Category(@"Политика заполения лишних пикселей")]
// ReSharper disable MemberCanBePrivate.Global
        public object PoliticComboBoxItem { get; set; }

// ReSharper restore MemberCanBePrivate.Global

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
// ReSharper disable MemberCanBePrivate.Global
        public bool MaximumGamma { get; set; }

// ReSharper restore MemberCanBePrivate.Global

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
            info.AddValue("FilterStep", FilterStep);
            info.AddValue("Key", Key);
            info.AddValue("SampleAutoresize", SampleAutoresize);
            info.AddValue("MaximumGamma", MaximumGamma);
            info.AddValue("PoliticIndex", PoliticIndex);
            info.AddValue("PoliticText", PoliticText);
            info.AddValue("MixerIndex", MixerIndex);
            info.AddValue("GammaIndex", GammaIndex);
            info.AddValue("ArchiverIndex", ArchiverIndex);
            info.AddValue("PixelFormatIndex", PixelFormatIndex);
        }

        public void IndexToObject()
        {
            MixerComboBoxItem = Mixer.ComboBoxItems[MixerIndex];
            GammaComboBoxItem = Gamma.ComboBoxItems[GammaIndex];
            ArchiverComboBoxItem = Archiver.ComboBoxItems[ArchiverIndex];
            PoliticComboBoxItem = Politic.ComboBoxItems[PoliticIndex];
            PixelFormatComboBoxItem = CvBitmap.ComboBoxItems[PixelFormatIndex];
        }

        public void ObjectToIndex()
        {
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
        }
    }
}