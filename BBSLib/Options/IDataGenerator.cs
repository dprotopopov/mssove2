using System;

namespace BBSLib.Options
{
    /// <summary>
    ///     Abstract interface of data generator class
    /// </summary>
    public interface IDataGenerator : IDisposable
    {
        void GetBytes(byte[] buffer);
        void GetInts(int[] buffer);
    }
}