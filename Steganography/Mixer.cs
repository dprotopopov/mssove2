using System;

namespace Steganography
{
    public class Mixer
    {
        private static readonly Arcfour Arcfour = new Arcfour();
        private static readonly None None = new None();

        public static readonly object[] ComboBoxItems =
        {
            new ComboBoxItem(MixerId.None, "Нет"),
            new ComboBoxItem(MixerId.Arcfour, "ARCFOUR")
        };

        private readonly string _key;
        private readonly MixerId _mixerId;

        public Mixer(int itemIndex, string key)
        {
            _mixerId = ((ComboBoxItem) ComboBoxItems[itemIndex]).HiddenValue;
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

        private class ComboBoxItem
        {
            private readonly string _displayValue;
            private readonly MixerId _hiddenValue;

            //Constructor
            public ComboBoxItem(MixerId h, string d)
            {
                _displayValue = d;
                _hiddenValue = h;
            }

            //Accessor
            public MixerId HiddenValue
            {
                get { return _hiddenValue; }
            }

            //Override ToString method
            public override string ToString()
            {
                return _displayValue;
            }
        }

        private enum MixerId
        {
            None = 0,
            Arcfour = 4
        };
    }
}