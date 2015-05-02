using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Steganography
{
    public partial class ViewSequenceForm : Form
    {
        private readonly int _expandSize;
        private readonly string _steganographyKey;

        public ViewSequenceForm(string steganographyKey, int expandSize)
        {
            _steganographyKey = steganographyKey;
            _expandSize = expandSize;
            InitializeComponent();
            byte[] gamma = new Arcfour(steganographyKey).Prga((expandSize + 7)/8);
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