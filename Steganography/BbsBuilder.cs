using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Steganography.Options;

namespace Steganography
{
    public class BbsBuilder
    {
        private const int BitsPerByte = 8;

        public CvBitmap Pack(BbsOptions bbsOptions)
        {
//            XtraMessageBox.Show(bbsOptions.ToString());
            Debug.WriteLine(bbsOptions.ToString());

            string key = bbsOptions.Key;
            int expandSize = bbsOptions.ExpandSize;
            int codeSize = bbsOptions.EccCodeSize;
            int dataSize = bbsOptions.EccDataSize;
            int alpha = bbsOptions.Alpha;
            bool autoResize = bbsOptions.SampleAutoresize;
            bool maximumGamma = bbsOptions.MaximumGamma;
            int politicIndex = bbsOptions.PoliticIndex;
            string politicText = bbsOptions.PoliticText;
            int eccIndex = bbsOptions.EccIndex;
            int mixerIndex = bbsOptions.MixerIndex;
            int gammaIndex = bbsOptions.GammaIndex;
            int archiverIndex = bbsOptions.ArchiverIndex;
            int pixelFormatIndex = bbsOptions.PixelFormatIndex;
            CvBitmap sampleBitmap = bbsOptions.SampleBitmap;

            byte[] bytes = Encoding.Default.GetBytes(bbsOptions.RtfText);
            Debug.WriteLine(string.Join("", bytes.Select(x => x.ToString("X02"))));

            using (var input = new MemoryStream(bytes))
            {
                Debug.WriteLine("input {0}", input.Length);
                // Алгоритм сжатия данных
                using (Stream compressed = new Archiver(archiverIndex).Compress(input))
                {
                    Debug.WriteLine("compressed {0}", compressed.Length);
                    // Добавление конверта
                    using (Stream enveloped = new Envelope().Seal(compressed))
                    {
                        Debug.WriteLine("enveloped {0}", enveloped.Length);
                        // Алгоритм коррекции ошибок
                        using (Stream encoded = new Ecc(eccIndex, codeSize, dataSize).Encode(enveloped))
                        {
                            Debug.WriteLine("encoded {0}", encoded.Length);
                            // Требуемое число пикселей
                            long count = encoded.Length*expandSize*BitsPerByte;
                            var outputBitmap = new CvBitmap(sampleBitmap, count, pixelFormatIndex, autoResize);

                            // для каждого бита сообщения нужно N байт носителя
                            if (count > outputBitmap.Length)
                                throw new Exception(
                                    string.Format("Размер изображения недостаточен для сохранения данных {0}/{1}",
                                        count, outputBitmap.Length));

                            using (Stream output = new Politic(politicIndex, outputBitmap, politicText).Fill(encoded))
                            {
                                Debug.WriteLine("output {0}", output.Length);
                                using (var reader = new BinaryReader(output))
                                {
                                    byte[] data = reader.ReadBytes((int) output.Length);
                                    Debug.WriteLine(string.Join("", data.Select(x => x.ToString("X02"))));

                                    int length = outputBitmap.Length;
                                    int[] index = new Mixer(mixerIndex, key).GetIndeces(length);
                                    byte[] gamma =
                                        new Gamma(gammaIndex, key).GetGamma(maximumGamma
                                            ? ((length + BitsPerByte - 1)/BitsPerByte)
                                            : ((expandSize + BitsPerByte - 1)/BitsPerByte));
                                    byte[] colors = outputBitmap.Select(index);
                                    byte[] cw = new BbSignals(expandSize, maximumGamma).Combine(colors, data, gamma,
                                        (byte) alpha);
                                    outputBitmap.Replace(index, cw);
#if _Debug
                                    var sb = new StringBuilder();
                                    sb.AppendLine(string.Format("input {0}", input.Length));
                                    sb.AppendLine(string.Format("compressed {0}", compressed.Length));
                                    sb.AppendLine(string.Format("enveloped {0}", enveloped.Length));
                                    sb.AppendLine(string.Format("encoded {0}", encoded.Length));
                                    sb.AppendLine(string.Format("filled {0}", output.Length));
                                    XtraMessageBox.Show(sb.ToString());
#endif
                                    return bbsOptions.OutputBitmap = outputBitmap;
                                }
                            }
                        }
                    }
                }
            }
        }

        public string Unpack(BbsOptions bbsOptions)
        {
//            XtraMessageBox.Show(bbsOptions.ToString());
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
            bool maximumGamma = bbsOptions.MaximumGamma;

            CvBitmap inputBitmap = bbsOptions.InputBitmap;
            var medianBitmap = new CvBitmap(inputBitmap, filterStep);

            Debug.WriteLine(inputBitmap.Length);
            Debug.WriteLine(medianBitmap.Length);

            int length = inputBitmap.Length;
            int[] index = new Mixer(mixerIndex, key).GetIndeces(length);
            byte[] gamma = new Gamma(gammaIndex, key).GetGamma(maximumGamma
                ? ((length + BitsPerByte - 1)/BitsPerByte)
                : ((expandSize + BitsPerByte - 1)/BitsPerByte));
            byte[] cw = inputBitmap.Select(index);
            byte[] median = medianBitmap.Select(index);
            byte[] data = new BbSignals(expandSize, maximumGamma).Extract(cw, median, gamma);
            Debug.WriteLine(string.Join("", data.Select(x => x.ToString("X02"))));

            using (var input = new MemoryStream(data))
            {
                Debug.WriteLine("input {0}", input.Length);
                // Алгоритм коррекции ошибок
                using (Stream enveloped = new Ecc(eccIndex, codeSize, dataSize).Decode(input))
                {
                    Debug.WriteLine("enveloped {0}", enveloped.Length);
                    // Извлечение из конверта
                    using (Stream compressed = new Envelope().Extract(enveloped))
                    {
                        Debug.WriteLine("compressed {0}", compressed.Length);
                        // Алгоритм извлечения из сжатого архива
                        using (Stream output = new Archiver(archiverIndex).Decompress(compressed))
                        {
                            Debug.WriteLine("output {0}", output.Length);
                            using (var reader = new BinaryReader(output))
                            {
                                byte[] bytes = reader.ReadBytes((int) output.Length);
                                Debug.WriteLine(string.Join("", bytes.Select(x => x.ToString("X02"))));
                                bbsOptions.MedianBitmap = medianBitmap;
#if _Debug
                                var sb = new StringBuilder();
                                sb.AppendLine(string.Format("input {0}", input.Length));
                                sb.AppendLine(string.Format("enveloped {0}", enveloped.Length));
                                sb.AppendLine(string.Format("compressed {0}", compressed.Length));
                                sb.AppendLine(string.Format("output {0}", output.Length));
                                XtraMessageBox.Show(sb.ToString());
#endif
                                return bbsOptions.RtfText = Encoding.Default.GetString(bytes);
                            }
                        }
                    }
                }
            }
        }
    }
}