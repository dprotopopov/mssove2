using System;
using System.Runtime.Serialization;

namespace Steganography
{
    /// <summary>
    ///     http://www.codeproject.com/Articles/1789/Object-Serialization-using-C
    /// </summary>
    [Serializable]
    public class SteganographyOptions : ISerializable //derive your class from ISerializable
    {
        public int Alpha;
        public int ArchiverIndex;
        public SteganographyBitmap BlurBitmap;
        public int ExpandSize;
        public int FilterStep;
        public int GammaIndex;
        public SteganographyBitmap InputBitmap;
        public int MixerIndex;
        public SteganographyBitmap OutputBitmap;
        public int PixelFormatIndex;
        public bool PoliticsFake;
        public bool PoliticsNone;
        public bool PoliticsRandom;
        public string PoliticsText;
        public bool PoliticsZero;
        public bool SampleAutoresize;
        public SteganographyBitmap SampleBitmap;
        public string SteganographyKey;
        public string Text;

        public SteganographyOptions()
        {
        }

        // Deserialization constructor.
        public SteganographyOptions(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            Alpha = (int) info.GetValue("Alpha", typeof (int));
            ExpandSize = (int) info.GetValue("ExpandSize", typeof (int));
            FilterStep = (int) info.GetValue("FilterStep", typeof (int));
            SteganographyKey = (string) info.GetValue("SteganographyKey", typeof (string));
            Text = (string) info.GetValue("Text", typeof (string));
            SampleAutoresize = (bool) info.GetValue("SampleAutoresize", typeof (bool));
            PoliticsNone = (bool) info.GetValue("PoliticsNone", typeof (bool));
            PoliticsZero = (bool) info.GetValue("PoliticsZero", typeof (bool));
            PoliticsRandom = (bool) info.GetValue("PoliticsRandom", typeof (bool));
            PoliticsFake = (bool) info.GetValue("PoliticsFake", typeof (bool));
            PoliticsText = (string) info.GetValue("PoliticsText", typeof (string));
            MixerIndex = (int) info.GetValue("MixerIndex", typeof (int));
            GammaIndex = (int) info.GetValue("GammaIndex", typeof (int));
            ArchiverIndex = (int) info.GetValue("ArchiverIndex", typeof (int));
            PixelFormatIndex = (int) info.GetValue("PixelFormatIndex", typeof (int));
        }

        // Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            //You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"
            info.AddValue("Alpha", Alpha);
            info.AddValue("ExpandSize", ExpandSize);
            info.AddValue("FilterStep", FilterStep);
            info.AddValue("SteganographyKey", SteganographyKey);
            info.AddValue("Text", Text);
            info.AddValue("SampleAutoresize", SampleAutoresize);
            info.AddValue("PoliticsNone", PoliticsNone);
            info.AddValue("PoliticsZero", PoliticsZero);
            info.AddValue("PoliticsRandom", PoliticsRandom);
            info.AddValue("PoliticsFake", PoliticsFake);
            info.AddValue("PoliticsText", PoliticsText);
            info.AddValue("MixerIndex", MixerIndex);
            info.AddValue("GammaIndex", GammaIndex);
            info.AddValue("ArchiverIndex", ArchiverIndex);
            info.AddValue("PixelFormatIndex", PixelFormatIndex);
        }
    }
}