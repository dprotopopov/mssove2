﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using BBSLib.Options;
using FFTTools;

namespace BBSLib
{
    /// <summary>
    ///     Класс применения метода широкополосного сигнала для передачи данных через графический файл
    ///     В соответствии с методом широкополосного сигнала, каждый бит данных кодируется последовательностью изменённых
    ///     яркостей пикселей в соответствии со значениями бит псевдослучайной последовательности.
    ///     При извлечении данных из массива яркостей пикселей применяется взвешенная схема голосования с коэффициентами
    ///     равными разности между средним значением яркости и яркостью голосующего пикселя.
    ///     Метод широкополосного сигнала предполагает возможность выработки у отправляемой и принимающих сторон одинаковой
    ///     псевдослучайной последовательности или, по крайней мере, псевдослучайных последовательностей со статистическими
    ///     характеристиками эквивалентными равным.
    ///     В дополнение к самому методу широкополосного сигнала данные могут быть сжаты алгоритмом компрессии данных,
    ///     добавлены коды исправления ошибок, последовательности бит могут размещаться в изображении не последовательно, а в
    ///     соответствии с выбранным алгоритмом.
    ///     В данной реализации для выработки псевдослучайной последовательности и для выработки индексов для внедрения бит
    ///     данных в изображение используются алгоритмы с использованием данных, называемых стеганографическим ключём.
    ///     При передаче данных стеганографический ключ передаётся вместе с данными.
    ///     Хотя для выработки псевдослучайной последовательности и для выработки индексов используются криптографические
    ///     алгоритмы как наиболее алгоритмы с наиболее изученными статистическими свойствами,
    ///     метод широкополосного сигнала НЕ ЯВЛЯЕТСЯ ШИФРОВАНИЕМ,
    ///     а является одним из медотов скрытной передачи информации,
    ///     то есть СТЕГАНОГРАФИЧЕСКИМ методом.
    /// </summary>
    public class BbsBuilder
    {
        private const int BitsPerByte = 8; // Количество битов в байте
        private const double F = 128;

        /// <summary>
        ///     Применение метода широкополосного сигнала
        ///     Внедрение данных в графический файл
        /// </summary>
        /// <param name="bbsOptions">Параметры алгоритма включая исходные данные</param>
        /// <returns>Графический файл с внедрёнными данными</returns>
        public static CvBitmap Pack(BbsOptions bbsOptions)
        {
            Debug.WriteLine(bbsOptions.ToString());

            string key = bbsOptions.Key;
            int expandSize = bbsOptions.ExpandSize;
            int codeSize = bbsOptions.EccCodeSize;
            int dataSize = bbsOptions.EccDataSize;
            double alpha = bbsOptions.Alpha;
            double betta = 0;
            bool dhtMode = bbsOptions.DhtMode;
            bool autoAlpha = bbsOptions.AutoAlpha;
            bool autoResize = bbsOptions.AutoResize;
            bool maximumGamma = bbsOptions.MaximumGamma;
            int politicIndex = bbsOptions.PoliticIndex;
            string politicText = bbsOptions.PoliticText;
            int eccIndex = bbsOptions.EccIndex;
            int mixerIndex = bbsOptions.MixerIndex;
            int gammaIndex = bbsOptions.GammaIndex;
            int archiverIndex = bbsOptions.ArchiverIndex;
            int barcodeIndex = bbsOptions.BarcodeIndex;
            int zipperIndex = bbsOptions.ZipperIndex;
            int filterStep = bbsOptions.FilterStep;

            CvBitmap sampleBitmap = bbsOptions.SampleBitmap;

            Size minSize = sampleBitmap.Size;
            CvBitmap barcodeBitmap = null;

            try
            {
                using (var barcode = new Barcode(barcodeIndex)
                {
                    // indeces
                    ArchiverIndex = archiverIndex,
                    EccIndex = eccIndex,
                    MixerIndex = mixerIndex,
                    GammaIndex = gammaIndex,
                    // ints
                    ExpandSize = expandSize,
                    EccCodeSize = codeSize,
                    EccDataSize = dataSize,
                    // bools
                    DhtMode = dhtMode,
                    AutoAlpha = autoAlpha,
                    MaximumGamma = maximumGamma,
                    // strings
                    Key = key,
                })
                {
                    // Формирование баркода с параметрами для используемых алгоритмов
                    barcodeBitmap = new CvBitmap(barcode.Encode());
                    Size barcodeSize = barcodeBitmap.Size;
                    minSize.Width = Math.Max(minSize.Width, 4*barcodeSize.Width);
                    minSize.Height = Math.Max(minSize.Height, 4*barcodeSize.Height);
                }
            }
            catch (ArgumentNullException exception)
            {
            }


            byte[] bytes = Encoding.Default.GetBytes(bbsOptions.RtfText);
            Debug.WriteLine(string.Join("", bytes.Select(x => x.ToString("X02"))));

            //     В дополнение к самому методу широкополосного сигнала данные могут быть сжаты алгоритмом компрессии данных,
            //     добавлены коды исправления ошибок, последовательности бит могут размещаться в изображении не последовательно, а в
            //     соответствии с выбранным алгоритмом.
            IStreamTransform[] streamTransforms =
            {
                new Archiver(archiverIndex), // Алгоритм сжатия данных
                new Envelope(), // Добавление конверта
                new Ecc(eccIndex, codeSize, dataSize) // Алгоритм коррекции ошибок
            };
            var input = new MemoryStream(bytes);
            Debug.WriteLine("input {0}", input.Length);
            foreach (IStreamTransform transform in streamTransforms)
            {
                using (MemoryStream prev = input)
                    transform.Forward(prev, input = new MemoryStream());
                input.Seek(0, SeekOrigin.Begin);
                Debug.WriteLine("{0} {1}", transform, input.Length);
            }


            // для каждого бита сообщения нужно N пикселей носителя
            long inputLength = input.Length; // Количество байт передаваемых данных
            long requiredLength = inputLength*expandSize*BitsPerByte; // Требуемое число пикселей
            Size sampleSize = sampleBitmap.Size;
            long sampleLength = sampleBitmap.Length;
            double ratio = Math.Sqrt(1 + (double) requiredLength/sampleLength);
            ratio = Math.Max(ratio, (double) minSize.Width/sampleSize.Width);
            ratio = Math.Max(ratio, (double) minSize.Height/sampleSize.Height);
            minSize.Width = (int) Math.Max(minSize.Width, Math.Ceiling(ratio*sampleSize.Width));
            minSize.Height = (int) Math.Max(minSize.Height, Math.Ceiling(ratio*sampleSize.Height));

            CvBitmap bitmap;
            CvBitmap median;

            using (var stretchBuilder = new StretchBuilder(minSize))
                bitmap = new CvBitmap(sampleBitmap, stretchBuilder, autoResize);


            long length = bitmap.Length;
            Size size = bitmap.Size;

            if (requiredLength > length)
                throw new Exception(
                    string.Format("Размер изображения недостаточен для сохранения данных {0}/{1}",
                        requiredLength, sampleLength));

            if (minSize.Width > size.Width || minSize.Height > size.Height)
                throw new Exception(
                    string.Format(
                        "Размер изображения недостаточен для сохранения данных {0}x{1}/{2}x{3}",
                        size.Width, size.Height, minSize.Width, minSize.Height));

            // Применение политики обработки неиспользуемых пикселей
            using (IStreamTransform streamTransform = new Politic(politicIndex, politicText, expandSize, bitmap))
            using (MemoryStream prev = input)
                streamTransform.Forward(prev, input = new MemoryStream());

            input.Seek(0, SeekOrigin.Begin);
            Debug.WriteLine("input {0}", input.Length);

            // Внедрения в передаваемое изображение баркода с настроечными параметрами для используемых алгоритмов
            if (barcodeBitmap != null) bitmap.DrawCopyright(barcodeBitmap);

            using (var builder = new BlurBuilder(filterStep))
                median = new CvBitmap(bitmap, builder);

            using (var reader = new BinaryReader(input))
            {
                byte[] data = reader.ReadBytes((int) input.Length);
                Debug.WriteLine(string.Join("", data.Select(x => x.ToString("X02"))));

                var index = new int[length];
                var colors = new double[length];
                var medianColors = new double[length];

                var gamma = new byte[maximumGamma
                    ? ((length + BitsPerByte - 1)/BitsPerByte)
                    : ((expandSize + BitsPerByte - 1)/BitsPerByte)];

                using (var builder = new Mixer(mixerIndex, key))
                    builder.GetInts(index);
                using (var builder = new Gamma(gammaIndex, key))
                    builder.GetBytes(gamma);

                IDataContainer bitmapDataContainer = dhtMode ? new HartleyOfBitmap(bitmap) : (IDataContainer) bitmap;
                bitmapDataContainer.Select(index, colors);

                IDataContainer medianDataContainer = dhtMode
                    ? new HartleyOfBitmap(median)
                    : (IDataContainer) median;
                medianDataContainer.Select(index, medianColors);

                Debug.Assert(dhtMode || colors.All(x => x >= 0));
                Debug.Assert(dhtMode || medianColors.All(x => x >= 0));

                // Рассчёт весов пикселей
                var middle = medianColors.Average();
                var weight = medianColors.Select(x => F/(F + Math.Abs(x - middle))).ToArray();
                var average = weight.Average();
                weight = weight.Select(x => x/average).ToArray();

                if (autoAlpha)
                {
                    // Вычисление параметров из характеристик отправляемых данных

                    double[] delta = colors.Zip(medianColors, (x, y) => (x - y)).Zip(weight, (x, y) => x*y).ToArray();

                    double e1 = delta.Average(x => x);
                    double e2 = delta.Average(x => x*x);

                    bbsOptions.Alpha = (int) Math.Ceiling(alpha = Math.Sqrt(e2 - e1*e1));

                    // Использование псевдослучайной последовательности с характеристиками приближенными к равновероятной, для кодирования
                    // данных, позволяет сохранить среднюю яркость пикселей у исходного графического изображения и у изображения,
                    // содержащего внедрённые данные
                    // Однако при прямом и дословном применении алгоритма средняя яркость пикселей могла бы иметь смещение относительно средней яркости у исходного изображения
                    // Поэтому производим статистическую оценку такого смещения и вводим её в качестве компенсирующего слагаемого в алгоритм

                    // Вычисление количества единиц в исходных данных и псевдослучайной последовательности
                    double trueData = BitCounter.Count(data);
                    double trueGamma = BitCounter.Count(gamma);

                    // Вычисление количества нулей в исходных данных и псевдослучайной последовательности
                    double falseData = (long) data.Length*BitsPerByte - trueData;
                    double falseGamma = (long) gamma.Length*BitsPerByte - trueGamma;

                    // Вычисление оценки количества единиц и нулей при смешивании исходных данных и псевдослучайной последовательности
                    double trueCount = trueGamma*falseData + falseGamma*trueData;
                    double falseCount = trueGamma*trueData + falseGamma*falseData;

                    betta = ((falseCount - trueCount)*alpha/(trueCount + falseCount));

                    Debug.WriteLine("alpha = {0} betta = {1}", alpha, betta);
                }

                using (var bbSignals = new BbSignals(expandSize, maximumGamma))
                {
                    var delta = new double[colors.Length];
                    bbSignals.Combine(delta, data, gamma, alpha, betta);
                    delta = delta.Zip(weight, (x, y) => x*y).ToArray();
                    colors = colors.Zip(delta, (x, y) => (x + y)).ToArray();
                }

                bitmapDataContainer.Replace(index, colors);
                return bbsOptions.OutputBitmap = bitmap;
            }
        }

        /// <summary>
        ///     Применение метода широкополосного сигнала
        ///     Извлечение данных из графического файла
        /// </summary>
        /// <param name="bbsOptions">Параметры алгоритма включая графический файл с внедрёнными данными</param>
        /// <returns>Извлечённые данные</returns>
        public static string Unpack(BbsOptions bbsOptions)
        {
            Debug.WriteLine(bbsOptions.ToString());

            string key = bbsOptions.Key;
            int expandSize = bbsOptions.ExpandSize;
            int codeSize = bbsOptions.EccCodeSize;
            int dataSize = bbsOptions.EccDataSize;
            int filterStep = bbsOptions.FilterStep;
            int eccIndex = bbsOptions.EccIndex;
            int mixerIndex = bbsOptions.MixerIndex;
            int gammaIndex = bbsOptions.GammaIndex;
            int archiverIndex = bbsOptions.ArchiverIndex;
            int zipperIndex = bbsOptions.ZipperIndex;
            double alpha = bbsOptions.Alpha;
            double betta = 0;
            bool dhtMode = bbsOptions.DhtMode;
            bool autoAlpha = bbsOptions.AutoAlpha;
            bool maximumGamma = bbsOptions.MaximumGamma;
            bool extractBarcode = bbsOptions.ExtractBarcode;

            CvBitmap bitmap = bbsOptions.InputBitmap;

            if (extractBarcode)
                using (var barcode = new Barcode(bitmap.Bitmap))
                {
                    // Извлечение параметров из внедрённого в изображение баркода
                    // и использование этих извлечённых параметров для используемых алгоритмов
                    barcode.Decode();
                    archiverIndex = barcode.ArchiverIndex;
                    eccIndex = barcode.EccIndex;
                    mixerIndex = barcode.MixerIndex;
                    gammaIndex = barcode.GammaIndex;
                    // ints
                    expandSize = barcode.ExpandSize;
                    codeSize = barcode.EccCodeSize;
                    dataSize = barcode.EccDataSize;
                    // bools
                    dhtMode = barcode.DhtMode;
                    autoAlpha = barcode.AutoAlpha;
                    maximumGamma = barcode.MaximumGamma;
                    // strings
                    key = barcode.Key;

                    Debug.WriteLine(barcode.ToString());
                }

            using (var builder = new BlurBuilder(filterStep))
                bbsOptions.MedianBitmap = new CvBitmap(bitmap, builder);
            CvBitmap median = bbsOptions.MedianBitmap;

            long length = bitmap.Length;

            var index = new int[length];
            var colors = new double[length];
            var medianColors = new double[length];
            var data = new byte[length/expandSize/BitsPerByte];

            using (var builder = new Mixer(mixerIndex, key))
                builder.GetInts(index);
            var gamma = new byte[maximumGamma
                ? ((length + BitsPerByte - 1)/BitsPerByte)
                : ((expandSize + BitsPerByte - 1)/BitsPerByte)];
            using (var builder = new Gamma(gammaIndex, key))
                builder.GetBytes(gamma);

            IDataContainer bitmapDataContainer = dhtMode ? new HartleyOfBitmap(bitmap) : (IDataContainer) bitmap;
            bitmapDataContainer.Select(index, colors);

            IDataContainer medianDataContainer = dhtMode ? new HartleyOfBitmap(median) : (IDataContainer) median;
            medianDataContainer.Select(index, medianColors);

            Debug.Assert(dhtMode || colors.All(x => x >= 0));
            Debug.Assert(dhtMode || medianColors.All(x => x >= 0));

            // Рассчёт весов пикселей
            var middle = medianColors.Average();
            var weight = medianColors.Select(x => F / (F + Math.Abs(x - middle))).ToArray();
            var average = weight.Average();
            weight = weight.Select(x => x / average).ToArray();

            double[] delta = colors.Zip(medianColors, (x, y) => (x - y)).Zip(weight, (x, y) => x * y).ToArray();

            if (autoAlpha)
            {
                // Вычисление параметров из характеристик полученных данных

                double e1 = delta.Average(x => x);
                double e2 = delta.Average(x => x*x);
                betta = e1;
                bbsOptions.Alpha = (int) Math.Ceiling(alpha = Math.Sqrt(e2 - e1*e1));

                Debug.WriteLine("alpha = {0} betta = {1}", alpha, betta);
            }

            using (var bbSignals = new BbSignals(expandSize, maximumGamma))
                bbSignals.Extract(delta, data, gamma, alpha, betta);

            Debug.WriteLine(string.Join("", data.Select(x => x.ToString("X02"))));

            //     В дополнение к самому методу широкополосного сигнала данные могут быть сжаты алгоритмом компрессии данных,
            //     добавлены коды исправления ошибок, последовательности бит могут размещаться в изображении не последовательно, а в
            //     соответствии с выбранным алгоритмом.
            IStreamTransform[] streamTransforms =
            {
                new Ecc(eccIndex, codeSize, dataSize), // Алгоритм коррекции ошибок
                new Envelope(), // Извлечение из конверта
                new Archiver(archiverIndex) // Алгоритм извлечения из сжатого архива
            };

            var input = new MemoryStream(data);
            Debug.WriteLine("input {0}", input.Length);
            foreach (IStreamTransform transform in streamTransforms)
            {
                using (MemoryStream prev = input)
                    transform.Backward(prev, input = new MemoryStream());
                input.Seek(0, SeekOrigin.Begin);
                Debug.WriteLine("{0} {1}", transform, input.Length);
            }
            using (var reader = new BinaryReader(input))
            {
                byte[] bytes = reader.ReadBytes((int) input.Length);
                Debug.WriteLine(string.Join("", bytes.Select(x => x.ToString("X02"))));
                return bbsOptions.RtfText = Encoding.Default.GetString(bytes);
            }
        }
    }
}