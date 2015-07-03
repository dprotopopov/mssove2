using System;
using Steganography.Cryptography;

namespace Steganography.Options
{
    /// <summary>
    ///     ����� ������������� � ��������� ���������� ������������� ������ (������������ ���)
    /// </summary>
    public class Mixer
    {
        /// <summary>
        ///     �������������� ���������� ������������� (������������)
        /// </summary>
// ReSharper disable MemberCanBePrivate.Global
        public enum MixerId
// ReSharper restore MemberCanBePrivate.Global
        {
            None = 0,
            Arcfour = 4
        };

        private static readonly Arcfour Arcfour = new Arcfour();

        /// <summary>
        ///     ������ ���������� ������������� (������������)
        /// </summary>
        public static readonly object[] ComboBoxItems =
        {
            new ComboBoxItem<MixerId>(MixerId.None, "���"),
            new ComboBoxItem<MixerId>(MixerId.Arcfour, "ARCFOUR")
        };

        private readonly string _key;
        private readonly MixerId _mixerId; // ������������� ��������� ������������� (������������)

        public Mixer(int itemIndex, string key)
        {
            _mixerId = ((ComboBoxItem<MixerId>) ComboBoxItems[itemIndex]).HiddenValue;
            _key = key;
        }

        public int[] GetIndeces(int n)
        {
            switch (_mixerId)
            {
                case MixerId.None:
                    return None.Identity(n);
                case MixerId.Arcfour:
                    Arcfour.SetKey(_key);
                    return Arcfour.Ksa(n);
            }
            throw new NotImplementedException();
        }
    }
}