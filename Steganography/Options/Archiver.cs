using System;
using System.IO;
using System.IO.Compression;

namespace Steganography.Options
{
    /// <summary>
    ///     ����� ������������� � ��������� ���������� ������ ������
    /// </summary>
    public class Archiver
    {
        /// <summary>
        ///     ������ ���������� ������ ������
        /// </summary>
        public static readonly object[] ComboBoxItems =
        {
            new ComboBoxItem<ArchiverId>(ArchiverId.None, "���"),
            new ComboBoxItem<ArchiverId>(ArchiverId.Deflate, "DEFLATE"),
            new ComboBoxItem<ArchiverId>(ArchiverId.GZip, "GZIP")
        };

        private readonly ArchiverId _archiverId; // ������������� ��������� ������ ������

        public Archiver(int itemIndex)
        {
            _archiverId = ((ComboBoxItem<ArchiverId>) ComboBoxItems[itemIndex]).HiddenValue;
        }

        /// <summary>
        ///     ����� ������ ������ ��� �������� ���������� ��������� ������ ������
        /// </summary>
        /// <param name="input">������� ����� ������</param>
        /// <param name="output">�������� ����� ������</param>
        public void Compress(Stream input, Stream output)
        {
            switch (_archiverId)
            {
                case ArchiverId.None:
                    None.Compress(input, output);
                    break;
                case ArchiverId.Deflate:
                    using (var compessionStream = new DeflateStream(output, CompressionMode.Compress, true))
                        input.CopyTo(compessionStream);
                    break;
                case ArchiverId.GZip:
                    using (var compessionStream = new GZipStream(output, CompressionMode.Compress, true))
                        input.CopyTo(compessionStream);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     ����� ������ ���������� ��� �������� ���������� ��������� ������ ������
        /// </summary>
        /// <param name="input">������� ����� ������</param>
        /// <param name="output">�������� ����� ������</param>
        public void Decompress(Stream input, Stream output)
        {
            switch (_archiverId)
            {
                case ArchiverId.None:
                    None.Decompress(input, output);
                    break;
                case ArchiverId.Deflate:
                    using (var decompessionStream = new DeflateStream(input, CompressionMode.Decompress, true))
                        decompessionStream.CopyTo(output);
                    break;
                case ArchiverId.GZip:
                    using (var decompessionStream = new GZipStream(input, CompressionMode.Decompress, true))
                        decompessionStream.CopyTo(output);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     �������������� ���������� ������ ������
        /// </summary>
        private enum ArchiverId
        {
            None = 0,
            Deflate,
            GZip
        };
    }
}