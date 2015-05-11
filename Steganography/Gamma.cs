using System;

namespace Steganography
{
    public class Gamma
    {
        private static readonly Arcfour Arcfour = new Arcfour();

        public static readonly object[] ComboBoxItems =
        {
            new ComboBoxItem(GammaId.None, "Нет"),
            new ComboBoxItem(GammaId.Arcfour, "ARCFOUR")
        };

        private readonly string _key;
        private readonly GammaId _mixerId;

        public Gamma(int itemIndex, string key)
        {
            _mixerId = ((ComboBoxItem) ComboBoxItems[itemIndex]).HiddenValue;
            _key = key;
        }

        public byte[] GetGamma(int n)
        {
            switch (_mixerId)
            {
                case GammaId.None:
                    return None.Zero(n);
                case GammaId.Arcfour:
                    Arcfour.SetKey(_key);
                    return Arcfour.Prga(n);
            }
            throw new NotImplementedException();
        }

        private class ComboBoxItem
        {
            private readonly string _displayValue;
            private readonly GammaId _hiddenValue;

            //Constructor
            public ComboBoxItem(GammaId h, string d)
            {
                _displayValue = d;
                _hiddenValue = h;
            }

            //Accessor
            public GammaId HiddenValue
            {
                get { return _hiddenValue; }
            }

            //Override ToString method
            public override string ToString()
            {
                return _displayValue;
            }
        }

        private enum GammaId
        {
            None = 0,
            Arcfour = 4
        };
    }
}