using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Steganography
{
    public partial class SteganographyForm : Form
    {
        private readonly BitmapSteganography _bitmapSteganography = new BitmapSteganography();

        public SteganographyForm()
        {
            InitializeComponent();
        }

        private void packing_Click(object sender, EventArgs e)
        {
            try
            {
                if (packingSample.Image == null) throw new Exception("Нет изображения");
                packingImage.Image = _bitmapSteganography.Packing(
                    new Bitmap(packingSample.Image),
                    packingKey.Text,
                    (int) packingExpand.Value,
                    (int) packingAlpha.Value,
                    packingCompress.Checked,
                    packingSource.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void unpacking_Click(object sender, EventArgs e)
        {
            try
            {
                if (unpackingImage.Image == null) throw new Exception("Нет изображения");
                unpackingDest.Text = _bitmapSteganography.Unpacking(
                    (Bitmap) unpackingImage.Image,
                    unpackingKey.Text,
                    (int) unpackingExpand.Value,
                    (int) unpackingFilter.Value,
                    umpackingDecompress.Checked);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void packingSample_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
                packingSample.Image = Image.FromFile(openFileDialog1.FileName);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void packingImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (packingImage.Image == null) throw new Exception("Нет изображения");
                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
                packingImage.Image.Save(saveFileDialog1.FileName);
                Debug.WriteLine(packingImage.Image.PixelFormat);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void unpackingImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog2.ShowDialog() != DialogResult.OK) return;
                var bitmap = new Bitmap(Image.FromFile(openFileDialog2.FileName));
                Debug.WriteLine(bitmap.PixelFormat);
                if (bitmap.PixelFormat != PixelFormat.Format32bppArgb) throw new Exception("Неизвестный формат");
                unpackingImage.Image = bitmap;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}