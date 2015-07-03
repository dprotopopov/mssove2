using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Steganography.Options
{
    /// <summary>
    ///     ����� ������������� � ��������� �������
    /// </summary>
    public class Politic
    {
        /// <summary>
        ///     �������������� �������
        /// </summary>
// ReSharper disable MemberCanBePrivate.Global
        public enum PoliticId
// ReSharper restore MemberCanBePrivate.Global
        {
            None = 0,
            Zeros,
            Random,
            Fake
        };

        private const int BitsPerByte = 8;

        /// <summary>
        ///     ������ ���������� ������ ������
        /// </summary>
        public static readonly object[] ComboBoxItems =
        {
            new ComboBoxItem<PoliticId>(PoliticId.None, "���"),
            new ComboBoxItem<PoliticId>(PoliticId.Zeros, "����"),
            new ComboBoxItem<PoliticId>(PoliticId.Random, "��������� ������"),
            new ComboBoxItem<PoliticId>(PoliticId.Fake, "�������������� ���������")
        };

        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();
        private readonly int _expandSize;
        private readonly CvBitmap _outputBitmap;
        private readonly PoliticId _politicId; // ������������� ��������
        private readonly string _politicsText;

        public Politic(int itemIndex, CvBitmap outputBitmap, int expandSize, string politicsText)
        {
            _outputBitmap = outputBitmap;
            _expandSize = expandSize;
            _politicsText = politicsText;
            _politicId = ((ComboBoxItem<PoliticId>) ComboBoxItems[itemIndex]).HiddenValue;
        }

        /// <summary>
        ///     ����� ������
        /// </summary>
        /// <param name="input">������� ����� ������</param>
        /// <param name="output">�������� ����� ������</param>
        public void Fill(Stream input, Stream output)
        {
            var length = (int) (_outputBitmap.Length/BitsPerByte/_expandSize - input.Length);
            input.CopyTo(output);
            switch (_politicId)
            {
                case PoliticId.None:
                    break;
                case PoliticId.Zeros:
                    for (int i = length; i-- > 0;)
                        output.WriteByte(0);
                    break;
                case PoliticId.Random:
                    var bytes = new byte[length];
                    Rng.GetBytes(bytes);
                    output.Write(bytes, 0, bytes.Length);
                    break;
                case PoliticId.Fake:
                    if (string.IsNullOrWhiteSpace(_politicsText)) throw new Exception("����������� ����������� �����");
                    byte[] buffer = Encoding.Default.GetBytes(_politicsText);
                    for (int i = 0; i < length; i += buffer.Length)
                        output.Write(buffer, 0, Math.Min(buffer.Length, length - i));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}