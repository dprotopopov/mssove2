using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using BBSLib.Cryptography;

namespace BBSLib.Options
{
    /// <summary>
    ///     Класс алгоритмов формирования псевдослучайной последовательности
    /// </summary>
    public class Gamma : IDataGenerator
    {
        private const byte FillByte = 0xAA; // Байт для инициализации массивов
        private const int BitsPerByte = 8; // Количество битов в байте
        private static object[] _comboBoxItems;
        private static readonly Arcfour Arcfour = new Arcfour();

        private readonly GammaId _gammaId; // Идентификатор алгоритма формирования гаммы
        private readonly string _key;

        public Gamma(int itemIndex, string key)
        {
            _gammaId = ((ComboBoxItem<GammaId>) ComboBoxItems[itemIndex]).HiddenValue;
            _key = key;
        }

        /// <summary>
        ///     Список алгоритмов формирования гаммы
        /// </summary>
        public static object[] ComboBoxItems
        {
            get
            {
                if (_comboBoxItems != null) return _comboBoxItems;
                var list = new List<object>(from object item in Enum.GetValues(typeof (GammaId))
                    select new ComboBoxItem<GammaId>((GammaId) item, item.ToString()));
                return _comboBoxItems = list.ToArray();
            }
        }

        public void Dispose()
        {
        }


        public void GetBytes(byte[] buffer)
        {
            int n = buffer.Length;
            switch (_gammaId)
            {
                case GammaId.None:
                    Array.Clear(buffer, 0, n);
                    break;
                case GammaId.Aes:
                    using (Aes aes = Aes.Create())
                    {
                        Arcfour.SetKey(_key);
                        if (aes != null)
                        {
                            aes.Key = new byte[(aes.KeySize + BitsPerByte - 1)/BitsPerByte];
                            aes.IV = new byte[(aes.BlockSize + BitsPerByte - 1)/BitsPerByte];
                            Arcfour.Prga(aes.Key);
                            Arcfour.Prga(aes.IV);
                            aes.Mode = CipherMode.CBC;
                            aes.Padding = PaddingMode.Zeros;

                            // Create a decrytor to perform the stream transform.
                            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                            // Create the streams used for encryption. 
                            using (var memoryStream = new MemoryStream())
                            {
                                using (
                                    var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
                                    )
                                {
                                    try
                                    {
                                        //Write all data to the stream.
                                        cryptoStream.Write(Enumerable.Repeat(FillByte, n).ToArray(), 0, n);
                                        cryptoStream.FlushFinalBlock();
                                    }
                                    catch
                                    {
                                    }
                                }
                                // Return the encrypted bytes from the memory stream. 
                                Array.Copy(memoryStream.ToArray(), buffer, n);
                            }
                        }
                    }
                    break;
                case GammaId.Des:
                    using (var serviceProvider = new DESCryptoServiceProvider())
                    {
                        Arcfour.SetKey(_key);
                        serviceProvider.Key = new byte[(serviceProvider.KeySize + BitsPerByte - 1)/BitsPerByte];
                        serviceProvider.IV = new byte[(serviceProvider.BlockSize + BitsPerByte - 1)/BitsPerByte];
                        Arcfour.Prga(serviceProvider.Key);
                        Arcfour.Prga(serviceProvider.IV);
                        serviceProvider.Mode = CipherMode.CBC;
                        serviceProvider.Padding = PaddingMode.Zeros;

                        // Create a decrytor to perform the stream transform.
                        ICryptoTransform encryptor = serviceProvider.CreateEncryptor(serviceProvider.Key,
                            serviceProvider.IV);

                        // Create the streams used for encryption. 
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                try
                                {
                                    //Write all data to the stream.
                                    cryptoStream.Write(Enumerable.Repeat(FillByte, n).ToArray(), 0, n);
                                    cryptoStream.FlushFinalBlock();
                                }
                                catch
                                {
                                }
                            }
                            // Return the encrypted bytes from the memory stream. 
                            Array.Copy(memoryStream.ToArray(), buffer, n);
                        }
                    }
                    break;
                case GammaId.TripleDes:
                    using (var serviceProvider = new TripleDESCryptoServiceProvider())
                    {
                        Arcfour.SetKey(_key);
                        serviceProvider.Key = new byte[(serviceProvider.KeySize + BitsPerByte - 1)/BitsPerByte];
                        serviceProvider.IV = new byte[(serviceProvider.BlockSize + BitsPerByte - 1)/BitsPerByte];
                        Arcfour.Prga(serviceProvider.Key);
                        Arcfour.Prga(serviceProvider.IV);
                        serviceProvider.Mode = CipherMode.CBC;
                        serviceProvider.Padding = PaddingMode.Zeros;

                        // Create a decrytor to perform the stream transform.
                        ICryptoTransform encryptor = serviceProvider.CreateEncryptor(serviceProvider.Key,
                            serviceProvider.IV);

                        // Create the streams used for encryption. 
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                try
                                {
                                    //Write all data to the stream.
                                    cryptoStream.Write(Enumerable.Repeat(FillByte, n).ToArray(), 0, n);
                                    cryptoStream.FlushFinalBlock();
                                }
                                catch
                                {
                                }
                            }
                            // Return the encrypted bytes from the memory stream. 
                            Array.Copy(memoryStream.ToArray(), buffer, n);
                        }
                    }
                    break;
                case GammaId.Arcfour:
                    Arcfour.SetKey(_key);
                    Arcfour.Prga(buffer);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void GetInts(int[] buffer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Идентификаторы алгоритмов формирования гаммы
        /// </summary>
        private enum GammaId
        {
            None = 0,
            Aes = 1,
            Des = 2,
            TripleDes = 3,
            Arcfour = 4,
        };
    }
}