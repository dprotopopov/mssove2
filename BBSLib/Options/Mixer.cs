using System;
using System.Collections.Generic;
using System.Linq;
using BBSLib.Cryptography;

namespace BBSLib.Options
{
    /// <summary>
    ///     Класс алгоритмов псевдослучайного перемешивания данных (перестановок бит)
    /// </summary>
    public class Mixer : IDataGenerator
    {
        private static object[] _comboBoxItems;

        private static readonly Arcfour Arcfour = new Arcfour();

        private readonly string _key;
        private readonly MixerId _mixerId; // Идентификатор алгоритма перемешивания (перестановок)

        public Mixer(int itemIndex, string key)
        {
            _mixerId = ((ComboBoxItem<MixerId>) ComboBoxItems[itemIndex]).HiddenValue;
            _key = key;
        }

        /// <summary>
        ///     Список алгоритмов перемешивания (перестановок)
        /// </summary>
        public static object[] ComboBoxItems
        {
            get
            {
                if (_comboBoxItems != null) return _comboBoxItems;
                var list = new List<object>(from object item in Enum.GetValues(typeof (MixerId))
                    select new ComboBoxItem<MixerId>((MixerId) item, item.ToString()));
                return _comboBoxItems = list.ToArray();
            }
        }

        public void Dispose()
        {
        }

        public void GetBytes(byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public void GetInts(int[] buffer)
        {
            switch (_mixerId)
            {
                case MixerId.None:
                    None.Identity(buffer);
                    break;
                case MixerId.Arcfour:
                    Arcfour.SetKey(_key);
                    Arcfour.Ksa(buffer);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Идентификаторы алгоритмов перемешивания (перестановок)
        /// </summary>
        private enum MixerId
        {
            None = 0,
            Arcfour = 4
        };
    }
}