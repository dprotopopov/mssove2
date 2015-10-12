using System;
using System.Collections.Generic;
using System.Linq;

namespace BBSLib.Options
{
    /// <summary>
    ///     Класс реализованных в программе алгоритмов компандера/экспандера
    ///     Экспандер расширяет диапазон значений
    ///     Компандер сужает диапазон значений
    /// </summary>
    public class Zipper : IDisposable
    {
        private static object[] _comboBoxItems; // Список значений для комбо-бокса
        private readonly double _expect;
        private readonly double _sigma;

        private readonly ZipperId _zipperId; // Идентификатор выбранного алгоритма арифметики сложения/вычитания

        public Zipper(int itemIndex, double expect = 0, double sigma = 1)
        {
            _zipperId = ((ComboBoxItem<ZipperId>) ComboBoxItems[itemIndex]).HiddenValue;
            _expect = expect;
            _sigma = sigma;
        }

        /// <summary>
        ///     Список политик
        /// </summary>
        public static object[] ComboBoxItems
        {
            get
            {
                if (_comboBoxItems != null) return _comboBoxItems;
                var list = new List<object>(from object item in Enum.GetValues(typeof (ZipperId))
                    select new ComboBoxItem<ZipperId>((ZipperId) item, item.ToString()));
                return _comboBoxItems = list.ToArray();
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        ///     Вызов метода алгоритма компандера
        /// </summary>
        /// <param name="delta">Массив изменений значений яркостей пикселей</param>
        public void Compact(double[] delta)
        {
            var index = 0;
            switch (_zipperId)
            {
                case ZipperId.Linear:
                    break;
                case ZipperId.Quadratic:
                    foreach (var value in delta)
                        delta[index++] = _expect +
                                         _sigma*Math.Sign(value - _expect)*
                                         Math.Sqrt(Math.Abs(value - _expect)/_sigma);
                    break;
                case ZipperId.Cubic:
                    foreach (var value in delta)
                        delta[index++] = _expect +
                                         _sigma*Math.Sign(value - _expect)*
                                         Math.Pow(Math.Abs(value - _expect)/_sigma, 1.0/3);
                    break;
                case ZipperId.Exponential:
                    foreach (var value in delta)
                        delta[index++] = _expect +
                                         _sigma*Math.Sign(value - _expect)*
                                         Math.Log(1 + (Math.Abs(value - _expect)/_sigma));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Вызов метода алгоритма экспандера
        /// </summary>
        /// <param name="delta">Массив изменений значений яркостей пикселей</param>
        public void Expand(double[] delta)
        {
            var index = 0;
            switch (_zipperId)
            {
                case ZipperId.Linear:
                    break;
                case ZipperId.Quadratic:
                    foreach (var value in delta)
                        delta[index++] = _expect +
                                         _sigma*Math.Sign(value - _expect)*
                                         Square((value - _expect)/_sigma);
                    break;
                case ZipperId.Cubic:
                    foreach (var value in delta)
                        delta[index++] = _expect +
                                         _sigma*Math.Sign(value - _expect)*
                                         Math.Pow(Math.Abs(value - _expect)/_sigma, 3);
                    break;
                case ZipperId.Exponential:
                    foreach (var value in delta)
                        delta[index++] = _expect +
                                         _sigma*Math.Sign(value - _expect)*
                                         (Math.Exp(Math.Abs(value - _expect)/_sigma) - 1.0);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private static double Square(double x)
        {
            return x*x;
        }

        /// <summary>
        ///     Идентификаторы алгоритмов арифметики сложения/вычитания
        /// </summary>
        private enum ZipperId
        {
            Linear,
            Quadratic,
            Cubic,
            Exponential
        };
    }
}