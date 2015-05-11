using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Steganography
{
    public partial class GammaForm : Form
    {
        private const int BitsPerByte = 8;
        private readonly int _expandSize;
        private readonly int _gammaIndex;
        private readonly string _steganographyKey;

        public GammaForm(string steganographyKey, int expandSize, int gammaIndex)
        {
            _steganographyKey = steganographyKey;
            _expandSize = expandSize;
            _gammaIndex = gammaIndex;
            InitializeComponent();
            byte[] gamma = new Gamma(gammaIndex, steganographyKey).GetGamma((expandSize + BitsPerByte - 1)/BitsPerByte);
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
                MessageBox.Show(exception.Message);
            }
        }
    }
}