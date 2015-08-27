using System.ComponentModel;

namespace Steganography.Options
{
    public class Barcode
    {
        /// <summary>
        ///     ������ ���������� �������
        /// </summary>
        public static readonly object[] ComboBoxItems =
        {
            new ComboBoxItem<BarcodeId>(BarcodeId.None, "���")
        };

        private readonly BarcodeId _barcodeId; // ������������� ��������� ������ ������

        public Barcode(int itemIndex)
        {
            _barcodeId = ((ComboBoxItem<BarcodeId>) ComboBoxItems[itemIndex]).HiddenValue;
        }

        [Description(@"�������� ����������")]
        [Category(@"���������")]
        public int ArchiverIndex { get; set; }

        [Description(@"������������")]
        [Category(@"��������")]
        public int ExpandSize { get; set; }

        [Description(@"����� ����")]
        [Category(@"�������� ��������� ������")]
        public int EccCodeSize { get; set; }

        [Description(@"����� ������")]
        [Category(@"�������� ��������� ������")]
        public int EccDataSize { get; set; }

        [Description(@"�������� ��������� ������")]
        [Category(@"���������")]
        public int EccIndex { get; set; }

        [Description(@"�������� �������������")]
        [Category(@"���������")]
        public int MixerIndex { get; set; }

        [Description(@"������������ ����� ������������ �����")]
        [Category(@"��������")]
        public bool MaximumGamma { get; set; }

        [Description(@"�������� �����")]
        [Category(@"���������")]
        public int GammaIndex { get; set; }

        [Description(@"����")]
        [Category(@"��������")]
        public string Key { get; set; }

        [Description(@"�������� �������")]
        [Category(@"��������")]
        public int FilterStep { get; set; }


        /// <summary>
        ///     �������������� ���������� �������
        /// </summary>
        private enum BarcodeId
        {
            None = 0,
            Qr
        };
    }
}