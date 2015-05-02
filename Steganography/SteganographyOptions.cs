using System;
using System.Drawing;
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
        public Bitmap Bitmap;
        public bool Compress;
        public int ExpandSize;
        public int FilterStep;
        public Image ImageSample;
        public bool PoliticsFake;
        public bool PoliticsNone;
        public bool PoliticsRandom;
        public string PoliticsText;
        public bool PoliticsZero;
        public bool SampleAutoresize;
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
            Bitmap = (Bitmap) info.GetValue("Bitmap", typeof (Bitmap));
            Compress = (bool) info.GetValue("Compress", typeof (bool));
            ExpandSize = (int) info.GetValue("ExpandSize", typeof (int));
            FilterStep = (int) info.GetValue("FilterStep", typeof (int));
            ImageSample = (Image) info.GetValue("ImageSample", typeof (Image));
            SteganographyKey = (string) info.GetValue("SteganographyKey", typeof (string));
            Text = (string) info.GetValue("Text", typeof (string));
            SampleAutoresize = (bool) info.GetValue("SampleAutoresize", typeof (bool));
            PoliticsNone = (bool) info.GetValue("PoliticsNone", typeof (bool));
            PoliticsZero = (bool) info.GetValue("PoliticsZero", typeof (bool));
            PoliticsRandom = (bool) info.GetValue("PoliticsRandom", typeof (bool));
            PoliticsFake = (bool) info.GetValue("PoliticsFake", typeof (bool));
            PoliticsText = (string) info.GetValue("PoliticsText", typeof (string));
        }

        // Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            //You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"
            info.AddValue("Alpha", Alpha);
            info.AddValue("Bitmap", Bitmap);
            info.AddValue("Compress", Compress);
            info.AddValue("ExpandSize", ExpandSize);
            info.AddValue("FilterStep", FilterStep);
            info.AddValue("ImageSample", ImageSample);
            info.AddValue("SteganographyKey", SteganographyKey);
            info.AddValue("Text", Text);
            info.AddValue("SampleAutoresize", SampleAutoresize);
            info.AddValue("PoliticsNone", PoliticsNone);
            info.AddValue("PoliticsZero", PoliticsZero);
            info.AddValue("PoliticsRandom", PoliticsRandom);
            info.AddValue("PoliticsFake", PoliticsFake);
            info.AddValue("PoliticsText", PoliticsText);
        }
    }
}