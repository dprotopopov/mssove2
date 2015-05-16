using System;

namespace Steganography
{
    public class Mixer
    {
        private static readonly Arcfour Arcfour = new Arcfour();

        public static readonly object[] ComboBoxItems =
        {
            new ComboBoxItem<MixerId>(MixerId.None, "Нет"),
            new ComboBoxItem<MixerId>(MixerId.Arcfour, "ARCFOUR")
        };

        private readonly string _key;
        private readonly MixerId _mixerId;

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


        private enum MixerId
        {
            None = 0,
            Arcfour = 4
        };
    }
}