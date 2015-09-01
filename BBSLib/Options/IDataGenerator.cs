using System;

namespace BBSLib.Options
{
    public interface IDataGenerator : IDisposable
    {
        void GetBytes(byte[] buffer);
        void GetInts(int[] buffer);
    }
}