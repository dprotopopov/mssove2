using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace BBSGridApp.DataModel
{
    /// <summary>
    ///     Creates a collection of groups and items with hard-coded content.
    ///     SampleDataSource initializes with placeholder data rather than live production
    ///     data so that sample data is provided at both design-time and run-time.
    /// </summary>
    public sealed class SampleDataSource
    {
        private static readonly SampleDataSource _sampleDataSource = new SampleDataSource();

        public SampleDataSource()
        {
            var ITEM_CONTENT = string.Format("{0}",
                "Метод широкополосного сигнала.");


            var group1 = new SampleDataGroup("Group-1",
                "Упаковка",
                "Формула размещения данных в графическом файле",
                "Icons/256.png",
                "Формула изменения яркостей пикселей в зависимости от значения i-го бита данных x[i] и j-го бита псевдослучайной последовательности y[j]: Cw[i,j] = C[i,j] + A*(-1)^(x[i] xor y[j]), где C[i,j] - значение яркости пикселя графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j], A - значение изменения яркости (параметр метода - глубина погружения).",
                "GroupDetailPage");
            var item11 = new SampleDataItem("Group-1-Item-1",
                "Автоматическая упаковка",
                "Формула размещения данных в графическом файле",
                "Icons/star.png",
                "Формула изменения яркостей пикселей в зависимости от значения i-го бита данных x[i] и j-го бита псевдослучайной последовательности y[j]: Cw[i,j] = C[i,j] + A*(-1)^(x[i] xor y[j]), где C[i,j] - значение яркости пикселя графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j], A - значение изменения яркости (параметр метода - глубина погружения).",
                "ItemDetailPage",
                ITEM_CONTENT,
                group1);
            var item12 = new SampleDataItem("Group-1-Item-2",
                "Отправляемое сообщение",
                "Формула размещения данных в графическом файле",
                "Icons/mail.png",
                "Формула изменения яркостей пикселей в зависимости от значения i-го бита данных x[i] и j-го бита псевдослучайной последовательности y[j]: Cw[i,j] = C[i,j] + A*(-1)^(x[i] xor y[j]), где C[i,j] - значение яркости пикселя графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j], A - значение изменения яркости (параметр метода - глубина погружения).",
                "ItemDetailPage",
                ITEM_CONTENT,
                group1);
            var item13 = new SampleDataItem("Group-1-Item-3",
                "Исходное изображение",
                "Формула размещения данных в графическом файле",
                "Icons/sample.png",
                "Формула изменения яркостей пикселей в зависимости от значения i-го бита данных x[i] и j-го бита псевдослучайной последовательности y[j]: Cw[i,j] = C[i,j] + A*(-1)^(x[i] xor y[j]), где C[i,j] - значение яркости пикселя графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j], A - значение изменения яркости (параметр метода - глубина погружения).",
                "ItemDetailPage",
                ITEM_CONTENT,
                group1);
            var item14 = new SampleDataItem("Group-1-Item-4",
                "Подготовленное изображение",
                "Формула размещения данных в графическом файле",
                "Icons/image.png",
                "Формула изменения яркостей пикселей в зависимости от значения i-го бита данных x[i] и j-го бита псевдослучайной последовательности y[j]: Cw[i,j] = C[i,j] + A*(-1)^(x[i] xor y[j]), где C[i,j] - значение яркости пикселя графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j], A - значение изменения яркости (параметр метода - глубина погружения).",
                "ItemDetailPage",
                ITEM_CONTENT,
                group1);
            var item15 = new SampleDataItem("Group-1-Item-5",
                "Параметры упаковки",
                "Формула размещения данных в графическом файле",
                "Icons/settings.png",
                "Формула изменения яркостей пикселей в зависимости от значения i-го бита данных x[i] и j-го бита псевдослучайной последовательности y[j]: Cw[i,j] = C[i,j] + A*(-1)^(x[i] xor y[j]), где C[i,j] - значение яркости пикселя графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j], A - значение изменения яркости (параметр метода - глубина погружения).",
                "ItemDetailPage",
                ITEM_CONTENT,
                group1);
            group1.Items.Add(item11);
            group1.Items.Add(item12);
            group1.Items.Add(item13);
            group1.Items.Add(item14);
            group1.Items.Add(item15);
            AllGroups.Add(group1);

            var group2 = new SampleDataGroup("Group-2",
                "Распаковка",
                "Формула извлечения данных из графического файла",
                "Icons/256.png",
                "При извлечении данных из массива яркостей пикселей применяется взвешенная схема голосования с коэффициентами равными разности между средним значением яркости и яркостью голосующего пикселя. V[i]= SUM (Cw[i,j]-Mw[i,j])(-1)^y[j] if V[i]<0 then x[i] == 0 else x[i] == 1, где Cw[i,j] - значение яркости пикселя декодируемого графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j], Mw[i,j] - значение средней яркости пикселей вокруг пикселя декодируемого графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j]",
                "GroupDetailPage");
            var item21 = new SampleDataItem("Group-2-Item-1",
                "Автоматическая распаковка",
                "Формула извлечения данных из графического файла",
                "Icons/star.png",
                "При извлечении данных из массива яркостей пикселей применяется взвешенная схема голосования с коэффициентами равными разности между средним значением яркости и яркостью голосующего пикселя. V[i]= SUM (Cw[i,j]-Mw[i,j])(-1)^y[j] if V[i]<0 then x[i] == 0 else x[i] == 1, где Cw[i,j] - значение яркости пикселя декодируемого графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j], Mw[i,j] - значение средней яркости пикселей вокруг пикселя декодируемого графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j].",
                "ItemDetailPage",
                ITEM_CONTENT,
                group2);
            var item22 = new SampleDataItem("Group-2-Item-2",
                "Полученное изображение",
                "Формула извлечения данных из графического файла",
                "Icons/image.png",
                "При извлечении данных из массива яркостей пикселей применяется взвешенная схема голосования с коэффициентами равными разности между средним значением яркости и яркостью голосующего пикселя. V[i]= SUM (Cw[i,j]-Mw[i,j])(-1)^y[j] if V[i]<0 then x[i] == 0 else x[i] == 1, где Cw[i,j] - значение яркости пикселя декодируемого графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j], Mw[i,j] - значение средней яркости пикселей вокруг пикселя декодируемого графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j].",
                "ItemDetailPage",
                ITEM_CONTENT,
                group2);
            var item23 = new SampleDataItem("Group-2-Item-3",
                "Усреднённое изображение",
                "Формула извлечения данных из графического файла",
                "Icons/median.png",
                "При извлечении данных из массива яркостей пикселей применяется взвешенная схема голосования с коэффициентами равными разности между средним значением яркости и яркостью голосующего пикселя. V[i]= SUM (Cw[i,j]-Mw[i,j])(-1)^y[j] if V[i]<0 then x[i] == 0 else x[i] == 1, где Cw[i,j] - значение яркости пикселя декодируемого графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j], Mw[i,j] - значение средней яркости пикселей вокруг пикселя декодируемого графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j].",
                "ItemDetailPage",
                ITEM_CONTENT,
                group2);
            var item24 = new SampleDataItem("Group-2-Item-4",
                "Полученное сообщение",
                "Формула извлечения данных из графического файла",
                "Icons/mail.png",
                "При извлечении данных из массива яркостей пикселей применяется взвешенная схема голосования с коэффициентами равными разности между средним значением яркости и яркостью голосующего пикселя. V[i]= SUM (Cw[i,j]-Mw[i,j])(-1)^y[j] if V[i]<0 then x[i] == 0 else x[i] == 1, где Cw[i,j] - значение яркости пикселя декодируемого графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j], Mw[i,j] - значение средней яркости пикселей вокруг пикселя декодируемого графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j].",
                "ItemDetailPage",
                ITEM_CONTENT,
                group2);
            var item25 = new SampleDataItem("Group-2-Item-5",
                "Параметры распаковки",
                "Формула извлечения данных из графического файла",
                "Icons/settings.png",
                "При извлечении данных из массива яркостей пикселей применяется взвешенная схема голосования с коэффициентами равными разности между средним значением яркости и яркостью голосующего пикселя. V[i]= SUM (Cw[i,j]-Mw[i,j])(-1)^y[j] if V[i]<0 then x[i] == 0 else x[i] == 1, где Cw[i,j] - значение яркости пикселя декодируемого графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j], Mw[i,j] - значение средней яркости пикселей вокруг пикселя декодируемого графического изображения сопоставленного i-му биту данных x[i] и j-му биту псевдослучайной последовательности y[j].",
                "ItemDetailPage",
                ITEM_CONTENT,
                group2);
            group2.Items.Add(item21);
            group2.Items.Add(item22);
            group2.Items.Add(item23);
            group2.Items.Add(item24);
            group2.Items.Add(item25);
            AllGroups.Add(group2);

            var group3 = new SampleDataGroup("Group-3",
                "Настроечные параметры",
                "Метод широкополосного сигнала",
                "Icons/256.png",
                "В соответствии с методом широкополосного сигнала, каждый бит данных кодируется последовательностью изменённых яркостей пикселей в соответствии со значениями бит псевдослучайной последовательности.",
                "GroupDetailPage");
            var item31 = new SampleDataItem("Group-3-Item-1",
                "Редактирование настроечных параметров",
                "Метод широкополосного сигнала",
                "Icons/settings.png",
                "В соответствии с методом широкополосного сигнала, каждый бит данных кодируется последовательностью изменённых яркостей пикселей в соответствии со значениями бит псевдослучайной последовательности.",
                "ItemDetailPage",
                ITEM_CONTENT,
                group3);
            group3.Items.Add(item31);
            AllGroups.Add(group3);

            var group4 = new SampleDataGroup("Group-4",
                "Справка",
                "Метод широкополосного сигнала",
                "Icons/256.png",
                "В соответствии с методом широкополосного сигнала, каждый бит данных кодируется последовательностью изменённых яркостей пикселей в соответствии со значениями бит псевдослучайной последовательности.",
                "GroupDetailPage");
            group4.Items.Add(new SampleDataItem("Group-4-Item-1",
                "О программе",
                "Метод широкополосного сигнала",
                "Icons/about.png",
                "В соответствии с методом широкополосного сигнала, каждый бит данных кодируется последовательностью изменённых яркостей пикселей в соответствии со значениями бит псевдослучайной последовательности.",
                "AboutPage",
                ITEM_CONTENT,
                group4));
            group4.Items.Add(new SampleDataItem("Group-4-Item-2",
                "Описание",
                "Метод широкополосного сигнала",
                "Icons/BOOK-icon.png",
                "В соответствии с методом широкополосного сигнала, каждый бит данных кодируется последовательностью изменённых яркостей пикселей в соответствии со значениями бит псевдослучайной последовательности.",
                "ItemDetailPage",
                ITEM_CONTENT,
                group4));
            AllGroups.Add(group4);

            FlatGroup.Items.Add(item12);
            FlatGroup.Items.Add(item13);
            FlatGroup.Items.Add(item14);

            FlatGroup.Items.Add(item22);
            FlatGroup.Items.Add(item23);
            FlatGroup.Items.Add(item24);

            FlatGroup.Items.Add(item31);
        }

        public ObservableCollection<SampleDataGroup> AllGroups { get; } = new ObservableCollection<SampleDataGroup>();

        public SampleDataGroup FlatGroup { get; } = new SampleDataGroup("Items",
            "Элементы для сборки",
            "Метод широкополосного сигнала",
            "Icons/256.png",
            "В соответствии с методом широкополосного сигнала, каждый бит данных кодируется последовательностью изменённых яркостей пикселей в соответствии со значениями бит псевдослучайной последовательности.",
            "ItemDetailPage");

        public static IEnumerable<SampleDataGroup> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups"))
                throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");

            return _sampleDataSource.AllGroups;
        }

        public static SampleDataGroup GetFlatGroup() => _sampleDataSource.FlatGroup;

        public static SampleDataGroup GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches =
                _sampleDataSource.AllGroups.Where(group => group.UniqueId.Equals(uniqueId));
            return matches.Count() == 1 ? matches.First() : null;
        }

        public static SampleDataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches =
                _sampleDataSource.AllGroups.SelectMany(group => group.Items)
                    .Where(item => item.UniqueId.Equals(uniqueId));
            return matches.Count() == 1 ? matches.First() : null;
        }
    }
}