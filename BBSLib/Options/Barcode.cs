﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using ZXing;

namespace BBSLib.Options
{
    public class Barcode : IDisposable
    {
        private static object[] _comboBoxItems;
        private readonly int _barcodeId; // Идентификатор алгоритма сжатия данных
        private Bitmap _bitmap;

        public Barcode(int itemIndex)
        {
            switch (itemIndex)
            {
                case 0:
                    _barcodeId = 0;
                    break;
                default:
                    _barcodeId = (int) ((ComboBoxItem<BarcodeFormat>) ComboBoxItems[itemIndex]).HiddenValue;
                    break;
            }
        }

        public Barcode(Bitmap bitmap)
        {
            _bitmap = bitmap;
        }

        /// <summary>
        ///     Список алгоритмов баркода
        /// </summary>
        public static object[] ComboBoxItems
        {
            get
            {
                if (_comboBoxItems != null) return _comboBoxItems;
                var list = new List<object>
                {
                    new ComboBoxItem<BarcodeFormat>(0, "Нет")
                };
                list.AddRange(
                    (from object item in Enum.GetValues(typeof (BarcodeFormat))
                        select new ComboBoxItem<BarcodeFormat>((BarcodeFormat) item, item.ToString())));
                return _comboBoxItems = list.ToArray();
            }
        }

        [Description(@"Избыточность")]
        [Category(@"Значения")]
        public int ExpandSize { get; set; }

        [Description(@"Алгоритм компрессии")]
        [Category(@"Алгоритмы")]
        public int ArchiverIndex { get; set; }

        [Description(@"Алгоритм перемешивания")]
        [Category(@"Алгоритмы")]
        public int MixerIndex { get; set; }

        [Description(@"Алгоритм гаммы")]
        [Category(@"Алгоритмы")]
        public int GammaIndex { get; set; }

        [Description(@"Алгоритм коррекции ошибки")]
        [Category(@"Алгоритмы")]
        public int EccIndex { get; set; }

        [Description(@"Длина кода")]
        [Category(@"Алгоритм коррекции ошибки")]
        public int EccCodeSize { get; set; }

        [Description(@"Длина данных")]
        [Category(@"Алгоритм коррекции ошибки")]
        public int EccDataSize { get; set; }

        [Description(@"Использовать гамму максимальной длины")]
        [Category(@"Значения")]
        public bool MaximumGamma { get; set; }

        [Description(@"Ключ")]
        [Category(@"Значения")]
        public string Key { get; set; }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("ExpandSize {0}", ExpandSize));
            sb.AppendLine(string.Format("EccCodeSize {0}", EccCodeSize));
            sb.AppendLine(string.Format("EccDataSize {0}", EccDataSize));
            sb.AppendLine(string.Format("Key {0}", Key));
            sb.AppendLine(string.Format("MaximumGamma {0}", MaximumGamma));
            sb.AppendLine(string.Format("EccIndex {0}", EccIndex));
            sb.AppendLine(string.Format("MixerIndex {0}", MixerIndex));
            sb.AppendLine(string.Format("GammaIndex {0}", GammaIndex));
            sb.AppendLine(string.Format("ArchiverIndex {0}", ArchiverIndex));
            return sb.ToString();
        }

        public Bitmap Encode()
        {
            int count = Marshal.SizeOf(typeof (BinaryData));
            var binaryData = new BinaryData
            {
                ArchiverIndex = (byte) ArchiverIndex,
                MixerIndex = (byte) MixerIndex,
                GammaIndex = (byte) GammaIndex,
                EccIndex = (byte) EccIndex,
                ExpandSize = (byte) ExpandSize,
                EccCodeSize = (byte) EccCodeSize,
                EccDataSize = (byte) EccDataSize,
                MaximumGamma = (byte) (MaximumGamma ? 0 : -1),
            };
            byte[] keyBytes = Encoding.Default.GetBytes(Key);
            var binaryBytes = new byte[count];
            IntPtr ptr = Marshal.AllocHGlobal(binaryBytes.Length);
            Marshal.StructureToPtr(binaryData, ptr, true);
            Marshal.Copy(ptr, binaryBytes, 0x0, binaryBytes.Length);
            Marshal.FreeHGlobal(ptr);
            string text = Convert.ToBase64String(binaryBytes.Concat(keyBytes).ToArray());
            switch (_barcodeId)
            {
                case 0:
                    throw new ArgumentNullException();
                default:
                    var writer = new BarcodeWriter
                    {
                        Format = (BarcodeFormat) _barcodeId,
                    };
                    Bitmap result = writer.Write(text);
                    using (var image = new Image<Gray, Byte>(result))
                        return _bitmap = image.Bitmap;
            }
        }

        public string Decode()
        {
            using (var image = new Image<Gray, Byte>(_bitmap))
            {
                using (Image<Gray, byte> bw = image.Convert(b => (byte) ((b < 128) ? 0 : 255)))
                {
                    var reader = new BarcodeReader();
                    Result result = reader.Decode(bw.Bitmap);
                    if (result == null) throw new Exception("Баркод не распознан");
                    byte[] bytes = Convert.FromBase64String(result.Text);
                    int count = Marshal.SizeOf(typeof (BinaryData));
                    GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                    var binaryData =
                        (BinaryData) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof (BinaryData));
                    handle.Free();
                    ArchiverIndex = binaryData.ArchiverIndex;
                    GammaIndex = binaryData.GammaIndex;
                    MixerIndex = binaryData.MixerIndex;
                    EccIndex = binaryData.EccIndex;
                    ExpandSize = binaryData.ExpandSize;
                    EccCodeSize = binaryData.EccCodeSize;
                    EccDataSize = binaryData.EccDataSize;
                    MaximumGamma = (binaryData.MaximumGamma == 0);
                    Key = Encoding.Default.GetString(bytes.ToList().GetRange(count, bytes.Length - count).ToArray());
                    return ToString();
                }
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct BinaryData
        {
            [FieldOffset(0)] public byte ArchiverIndex;
            [FieldOffset(1)] public byte MixerIndex;
            [FieldOffset(2)] public byte GammaIndex;
            [FieldOffset(3)] public byte EccIndex;
            [FieldOffset(4)] public byte ExpandSize;
            [FieldOffset(5)] public byte EccCodeSize;
            [FieldOffset(6)] public byte EccDataSize;
            [FieldOffset(7)] public byte MaximumGamma;
        };
    }
}