using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using BBSLib;
using DevExpress.XtraEditors;

namespace BBSApp
{
    internal static class Program
    {
        private const string DefaultOptionsFileName = "default.options";

        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var args = Environment.GetCommandLineArgs();
            var optionsFileName = DefaultOptionsFileName;

            var sampleImageFileName = string.Empty;
            var inputImageFileName = string.Empty;
            var textFileName = string.Empty;
            var politicTextFileName = string.Empty;
            var selectedMode = BbsControl.Mode.Welcome;

            try
            {
                Debug.WriteLine("Process commant line");
                for (var i = 1; i < args.Length; i++)
                    switch (args[i].ToLower())
                    {
                        case "-options":
                            optionsFileName = args[++i];
                            break;
                        case "-text":
                            textFileName = args[++i];
                            break;
                        case "-politictext":
                            politicTextFileName = args[++i];
                            break;
                        case "-sample":
                            selectedMode = BbsControl.Mode.Pack;
                            sampleImageFileName = args[++i];
                            break;
                        case "-input":
                            selectedMode = BbsControl.Mode.Unpack;
                            inputImageFileName = args[++i];
                            break;
                        default:
                            selectedMode = BbsControl.Mode.Unpack;
                            inputImageFileName = args[i];
                            break;
                    }

                var bbsOptions = new BbsOptions();

                try
                {
                    Debug.WriteLine("Reading Options Information");
                    using (Stream stream = File.Open(optionsFileName, FileMode.Open))
                        bbsOptions = (BbsOptions) new BinaryFormatter().Deserialize(stream);
                }
                catch (Exception)
                {
                }

                bbsOptions.RtfText = (string.IsNullOrWhiteSpace(textFileName)) 
                    ? bbsOptions.RtfText 
                    : File.ReadAllText(textFileName);
                bbsOptions.PoliticText = (string.IsNullOrWhiteSpace(politicTextFileName))
                    ? bbsOptions.PoliticText
                    : File.ReadAllText(politicTextFileName);
                bbsOptions.SampleBitmap = (string.IsNullOrWhiteSpace(sampleImageFileName))
                    ? bbsOptions.SampleBitmap
                    : new CvBitmap(sampleImageFileName);
                bbsOptions.InputBitmap = (string.IsNullOrWhiteSpace(inputImageFileName))
                    ? bbsOptions.InputBitmap
                    : new CvBitmap(inputImageFileName);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(bbsOptions, selectedMode));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
    }
}