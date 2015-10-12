namespace BBSLib
{
    internal class ComboBoxItem<T>
    {
        private readonly string _displayValue;
        private readonly T _hiddenValue;

        //Constructor
        public ComboBoxItem(T h, string d)
        {
            _displayValue = d;
            _hiddenValue = h;
        }

        //Accessor
        public T HiddenValue => _hiddenValue;

        //Override ToString method
        public override string ToString() => _displayValue;
    }
}