using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Steganography.Cryptography;

namespace Steganography.Options
{
    /// <summary>
    ///     Класс реализованных в программе алгоритмов формирования гаммы
    /// </summary>
    public class Gamma
    {
        /// <summary>
        ///     Идентификаторы алгоритмов формирования гаммы
        /// </summary>
// ReSharper disable MemberCanBePrivate.Global
        public enum GammaId
// ReSharper restore MemberCanBePrivate.Global
        {
            None = 0,
            Aes = 1,
            Des = 2,
            TripleDes = 3,
            Arcfour = 4,
        };

        private const byte FillByte = 0xAA;
        private const int BitsPerByte = 8;
        private static readonly Arcfour Arcfour = new Arcfour();

        /// <summary>
        ///     Список алгоритмов формирования гаммы
        /// </summary>
        public static readonly object[] ComboBoxItems =
        {
            new ComboBoxItem<GammaId>(GammaId.None, "Нет"),
            new ComboBoxItem<GammaId>(GammaId.Aes, "AES"),
            new ComboBoxItem<GammaId>(GammaId.Des, "DES"),
            new ComboBoxItem<GammaId>(GammaId.TripleDes, "TripleDES"),
            new ComboBoxItem<GammaId>(GammaId.Arcfour, "ARCFOUR")
        };

        private readonly GammaId _gammaId; // Идентификатор алгоритма формирования гаммы
        private readonly string _key;

        public Gamma(int itemIndex, string key)
        {
            _gammaId = ((ComboBoxItem<GammaId>) ComboBoxItems[itemIndex]).HiddenValue;
            _key = key;
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Не ликвидировать объекты несколько раз")]
        public byte[] GetGamma(int n)
        {
            switch (_gammaId)
            {
                case GammaId.None:
                    return None.Zero(n);
                case GammaId.Aes:
                    using (Aes aes = Aes.Create())
                    {
                        Arcfour.SetKey(_key);
                        if (aes != null)
                        {
                            aes.Key = Arcfour.Prga((aes.KeySize + BitsPerByte - 1)/BitsPerByte);
                            aes.IV = Arcfour.Prga((aes.BlockSize + BitsPerByte - 1)/BitsPerByte);
                            aes.Mode = CipherMode.CBC;
                            aes.Padding = PaddingMode.Zeros;

                            // Create a decrytor to perform the stream transform.
// ReSharper disable AssignNullToNotNullAttribute
                            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
// ReSharper restore AssignNullToNotNullAttribute

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
// ReSharper disable EmptyGeneralCatchClause
                                    catch
// ReSharper restore EmptyGeneralCatchClause
                                    {
                                    }
                                }
                                // Return the encrypted bytes from the memory stream. 
                                return memoryStream.ToArray();
                            }
                        }
                    }
                    break;
                case GammaId.Des:
                    using (var serviceProvider = new DESCryptoServiceProvider())
                    {
                        Arcfour.SetKey(_key);
                        serviceProvider.Key =
                            Arcfour.Prga((serviceProvider.KeySize + BitsPerByte - 1)/BitsPerByte);
                        serviceProvider.IV =
                            Arcfour.Prga((serviceProvider.BlockSize + BitsPerByte - 1)/BitsPerByte);
                        serviceProvider.Mode = CipherMode.CBC;
                        serviceProvider.Padding = PaddingMode.Zeros;

                        // Create a decrytor to perform the stream transform.
// ReSharper disable AssignNullToNotNullAttribute
                        ICryptoTransform encryptor = serviceProvider.CreateEncryptor(serviceProvider.Key,
// ReSharper restore AssignNullToNotNullAttribute
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
// ReSharper disable EmptyGeneralCatchClause
                                catch
// ReSharper restore EmptyGeneralCatchClause
                                {
                                }
                            }
                            // Return the encrypted bytes from the memory stream. 
                            return memoryStream.ToArray();
                        }
                    }
                case GammaId.TripleDes:
                    using (var serviceProvider = new TripleDESCryptoServiceProvider())
                    {
                        Arcfour.SetKey(_key);
                        serviceProvider.Key =
                            Arcfour.Prga((serviceProvider.KeySize + BitsPerByte - 1)/BitsPerByte);
                        serviceProvider.IV =
                            Arcfour.Prga((serviceProvider.BlockSize + BitsPerByte - 1)/BitsPerByte);
                        serviceProvider.Mode = CipherMode.CBC;
                        serviceProvider.Padding = PaddingMode.Zeros;

                        // Create a decrytor to perform the stream transform.
// ReSharper disable AssignNullToNotNullAttribute
                        ICryptoTransform encryptor = serviceProvider.CreateEncryptor(serviceProvider.Key,
// ReSharper restore AssignNullToNotNullAttribute
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
// ReSharper disable EmptyGeneralCatchClause
                                catch
// ReSharper restore EmptyGeneralCatchClause
                                {
                                }
                            }
                            // Return the encrypted bytes from the memory stream. 
                            return memoryStream.ToArray();
                        }
                    }
                case GammaId.Arcfour:
                    Arcfour.SetKey(_key);
                    return Arcfour.Prga(n);
            }
            throw new NotImplementedException();
        }
    }
}