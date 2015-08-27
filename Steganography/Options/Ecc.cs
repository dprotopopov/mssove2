using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ZXing.Common.ReedSolomon;

namespace Steganography.Options
{
    /// <summary>
    ///     Error and erasure detection and correction
    /// </summary>
    public class Ecc
    {
        /// <summary>
        ///     ������ ���������� ��������� ������
        /// </summary>
        public static readonly object[] ComboBoxItems =
        {
            new ComboBoxItem<EccId>(EccId.None, "���"),
            new ComboBoxItem<EccId>(EccId.ReedSolomon, "Reed-Solomon")
        };

        private readonly int _eccCodeSize;
        private readonly int _eccDataSize;
        private readonly EccId _eccId; // ������������� ��������� ��������� ������

        public Ecc(int itemIndex, int eccCodeSize, int eccDataSize)
        {
            _eccCodeSize = eccCodeSize;
            _eccDataSize = eccDataSize;
            _eccId = ((ComboBoxItem<EccId>) ComboBoxItems[itemIndex]).HiddenValue;
        }

        /// <summary>
        ///     ����� ������ ����������� ��� �������� ���������� ��������� ��������� ������
        /// </summary>
        /// <param name="input">������� ����� ������</param>
        /// <returns>�������� ����� ������</returns>
        public Stream Encode(Stream input)
        {
            int codeSize = _eccCodeSize;
            int dataSize = _eccDataSize;
            Debug.Assert(codeSize > dataSize);
            Stream output = new MemoryStream();
            switch (_eccId)
            {
                case EccId.None:
                    None.Encode(input, output);
                    break;
                case EccId.ReedSolomon:
                    GenericGF gf = GenericGF.QR_CODE_FIELD_256;
                    var encoder = new ReedSolomonEncoder(gf);
                    int twoS = Math.Abs(codeSize - dataSize);
                    var buffer = new byte[Math.Max(codeSize, dataSize)];
                    while (input.Read(buffer, 0, dataSize) > 0)
                    {
                        int[] array = buffer.Select(x => (int) x).ToArray();
                        encoder.encode(array, twoS);
                        buffer = array.Select(x => (byte) x).ToArray();
                        output.Write(buffer, 0, codeSize);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            output.Seek(0, SeekOrigin.Begin);
            return output;
        }

        /// <summary>
        ///     ����� ������ ������������� ��� �������� ���������� ��������� ��������� ������
        /// </summary>
        /// <param name="input">������� ����� ������</param>
        /// <returns>�������� ����� ������</returns>
        public Stream Decode(Stream input)
        {
            int codeSize = _eccCodeSize;
            int dataSize = _eccDataSize;
            Debug.Assert(codeSize > dataSize);
            Stream output = new MemoryStream();
            switch (_eccId)
            {
                case EccId.None:
                    None.Decode(input, output);
                    break;
                case EccId.ReedSolomon:
                    GenericGF gf = GenericGF.QR_CODE_FIELD_256;
                    var decoder = new ReedSolomonDecoder(gf);
                    int twoS = Math.Abs(codeSize - dataSize);
                    var buffer = new byte[Math.Max(codeSize, dataSize)];
                    while (input.Read(buffer, 0, codeSize) > 0)
                    {
                        int[] array = buffer.Select(x => (int) x).ToArray();
                        decoder.decode(array, twoS);
                        buffer = array.Select(x => (byte) x).ToArray();
                        output.Write(buffer, 0, dataSize);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            output.Seek(0, SeekOrigin.Begin);
            return output;
        }

        /// <summary>
        ///     �������������� ���������� ��������� ������
        /// </summary>
        private enum EccId
        {
            None = 0,
            ReedSolomon
        };
    }
}