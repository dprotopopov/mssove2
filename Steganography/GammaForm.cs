using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Steganography.Options;

namespace Steganography
{
    public partial class GammaForm : XtraForm
    {
        private const int BitsPerByte = 8;
        // ReSharper disable NotAccessedField.Local
        private readonly int _expandSize;
// ReSharper restore NotAccessedField.Local
// ReSharper disable NotAccessedField.Local
        private readonly int _gammaIndex;
// ReSharper restore NotAccessedField.Local
// ReSharper disable NotAccessedField.Local
        private readonly string _steganographyKey;
// ReSharper restore NotAccessedField.Local

        public GammaForm(string steganographyKey, int expandSize, int gammaIndex)
        {
            _steganographyKey = steganographyKey;
            _expandSize = expandSize;
            _gammaIndex = gammaIndex;
            InitializeComponent();
            byte[] gamma = new Gamma(gammaIndex, steganographyKey).GetGamma((expandSize + BitsPerByte - 1) / BitsPerByte);
            textBox1.Text = string.Join("", gamma.ToArray().Select(x => x.ToString("X02")));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
                File.WriteAllText(saveFileDialog1.FileName, textBox1.Text);
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
        }
    }
}