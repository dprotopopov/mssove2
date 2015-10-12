using System;

namespace BBSLib
{
    /// <summary>
    ///     Интерфейс для работы с контейнером для передачи данных
    /// </summary>
    public interface IDataContainer : IDisposable
    {
        /// <summary>
        ///     Извлечение из контейнера значений яркостей пикселей, в соответствии с указанными индексами
        /// </summary>
        /// <param name="index">Массив индексов пикселей</param>
        /// <param name="colors">Массив извлечённых значений яркостей пикселей</param>
        void Select(int[] index, double[] colors);

        /// <summary>
        ///     Замена в контейнере, в соответствии с указанными индексами, значений яркостей пикселей
        /// </summary>
        /// <param name="index">Массив индексов пикселей</param>
        /// <param name="colors">Массив извлечённых значений яркостей пикселей</param>
        void Replace(int[] index, double[] colors);

        /// <summary>
        ///     Вычисление статистических характеристик контейнера
        /// </summary>
        /// <param name="average">Средняя яркость пикселей</param>
        /// <param name="delta">Дисперсия яркости пикселей</param>
        void AverageAndDelta(out double average, out double delta);
    }
}