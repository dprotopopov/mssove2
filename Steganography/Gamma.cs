using System;

namespace Steganography
{
    public class Gamma
    {
        private static readonly Arcfour Arcfour = new Arcfour();

        public static readonly object[] ComboBoxItems =
        {
            new ComboBoxItem<GammaId>(GammaId.None, "Нет"),
            new ComboBoxItem<GammaId>(GammaId.Arcfour, "ARCFOUR")
        };

        private readonly string _key;
        private readonly GammaId _mixerId;

        public Gamma(int itemIndex, string key)
        {
            _mixerId = ((ComboBoxItem<GammaId>) ComboBoxItems[itemIndex]).HiddenValue;
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

        private enum GammaId
        {
            None = 0,
            Arcfour = 4
        };
    }
}