using System;
using System.IO;

namespace BBSLib.Options
{
    public interface IStreamTransform : IDisposable
    {
        void Forward(Stream input, Stream output);
        void Backward(Stream input, Stream output);
    }
}