using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Steganography
{
    public partial class SteganographyForm : Form
    {
        private readonly BitmapSteganography _bitmapSteganography = new BitmapSteganography();
        private SteganographyOptions _options = new SteganographyOptions();

        public SteganographyForm()
        {
            InitializeComponent();
            try
            {
                Stream stream = File.Open("default.options", FileMode.Open);
                Debug.WriteLine("Reading Default Options Information");
                _options = (SteganographyOptions) new BinaryFormatter().Deserialize(stream);
                stream.Close();
            }
            catch (Exception)
            {
                _options.ExpandSize = 64;
                _options.Alpha = 10;
                _options.Compress = false;
                _options.SampleAutoresize = false;
                _options.PoliticsNone = true;
                _options.PoliticsZero = false;
                _options.PoliticsRandom = false;
                _options.PoliticsFake = false;
                _options.PoliticsText = "FAKE";
                _options.SteganographyKey = "WELCOME";
                _options.FilterStep = 3;
            }
            
            ApplyPackingOption();
            ApplyUnpackingOption();
        }

        private void ApplyPackingOption()
        {
            packingSample.Image = _options.ImageSample;
            packingKey.Text = _options.SteganographyKey;
            packingExpand.Value = _options.ExpandSize;
            packingAlpha.Value = _options.Alpha;
            packingCompress.Checked = _options.Compress;
            packingSource.Text = _options.Text;
            packingSampleAutoresize.Checked = _options.SampleAutoresize;
            packingPoliticsNone.Checked = _options.PoliticsNone;
            packingPoliticsZero.Checked = _options.PoliticsZero;
            packingPoliticsRandom.Checked = _options.PoliticsRandom;
            packingPoliticsFake.Checked = _options.PoliticsFake;
            packingPoliticsText.Text = _options.PoliticsText;
        }

        private void ApplyUnpackingOption()
        {
            unpackingImage.Image = _options.Bitmap;
            unpackingKey.Text = _options.SteganographyKey;
            unpackingExpand.Value = _options.ExpandSize;
            unpackingFilter.Value = _options.FilterStep;
            unpackingDecompress.Checked = _options.Compress;
        }

        private void GetPackingOption()
        {
            _options.ImageSample = packingSample.Image;
            _options.SteganographyKey = packingKey.Text;
            _options.ExpandSize = (int) packingExpand.Value;
            _options.Alpha = (int) packingAlpha.Value;
            _options.Compress = packingCompress.Checked;
            _options.Text = packingSource.Text;
            _options.SampleAutoresize = packingSampleAutoresize.Checked;
            _options.PoliticsNone = packingPoliticsNone.Checked;
            _options.PoliticsZero = packingPoliticsZero.Checked;
            _options.PoliticsRandom = packingPoliticsRandom.Checked;
            _options.PoliticsFake = packingPoliticsFake.Checked;
            _options.PoliticsText = packingPoliticsText.Text;
        }

        private void GetUnpackingOption()
        {
            _options.Bitmap = (Bitmap) unpackingImage.Image;
            _options.SteganographyKey = unpackingKey.Text;
            _options.ExpandSize = (int) unpackingExpand.Value;
            _options.FilterStep = (int) unpackingFilter.Value;
            _options.Compress = unpackingDecompress.Checked;
        }

        private void packing_Click(object sender, EventArgs e)
        {
            try
            {
                if (packingSample.Image == null) throw new Exception("Нет изображения");
                GetPackingOption();
                packingImage.Image = _bitmapSteganography.Packing(_options);
                ApplyUnpackingOption();
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

                GetUnpackingOption();
                unpackingDest.Text = _bitmapSteganography.Unpacking(_options);
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
                if (openSampleDialog.ShowDialog() != DialogResult.OK) return;
                Image image = Image.FromFile(openSampleDialog.FileName);
                if (image.PixelFormat != PixelFormat.Format32bppArgb)
                    if (MessageBox.Show(@"Формат файла отличен от BMP32ARGB",
                        @"Предупреждение",
                        MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        return;
                packingSample.Image = image;
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
                if (saveImageDialog.ShowDialog() != DialogResult.OK) return;
                packingImage.Image.Save(saveImageDialog.FileName);
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
                if (openImageDialog.ShowDialog() != DialogResult.OK) return;
                var bitmap = new Bitmap(Image.FromFile(openImageDialog.FileName));
                Debug.WriteLine(bitmap.PixelFormat);
                if (bitmap.PixelFormat != PixelFormat.Format32bppArgb) throw new Exception("Неизвестный формат");
                unpackingImage.Image = bitmap;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void packingViewSequence_Click(object sender, EventArgs e)
        {
            new ViewSequenceForm(packingKey.Text, (int) packingExpand.Value).ShowDialog();
        }

        private void unpackingViewSequence_Click(object sender, EventArgs e)
        {
            new ViewSequenceForm(packingKey.Text, (int) packingExpand.Value).ShowDialog();
        }

        private void packingSave_Click(object sender, EventArgs e)
        {
            if (saveFileOptions.ShowDialog() != DialogResult.OK) return;
            //Open the file written above and read values from it.
            GetPackingOption();
            Stream stream = File.Open(saveFileOptions.FileName, FileMode.Create);
            Debug.WriteLine("Writing Options Information");
            new BinaryFormatter().Serialize(stream, _options);
            stream.Close();
        }

        private void packingLoad_Click(object sender, EventArgs e)
        {
            if (openFileOptions.ShowDialog() != DialogResult.OK) return;
            //Open the file written above and read values from it.
            Stream stream = File.Open(openFileOptions.FileName, FileMode.Open);
            Debug.WriteLine("Reading Options Information");
            _options = (SteganographyOptions) new BinaryFormatter().Deserialize(stream);
            stream.Close();
            ApplyPackingOption();
        }

        private void unpackingSave_Click(object sender, EventArgs e)
        {
            if (saveFileOptions.ShowDialog() != DialogResult.OK) return;
            GetUnpackingOption();
            // Open a file and serialize the object into it in binary format.
            // EmployeeInfo.osl is the file that we are creating. 
            // Note:- you can give any extension you want for your file
            // If you use custom extensions, then the user will now 
            //   that the file is associated with your program.
            Stream stream = File.Open(saveFileOptions.FileName, FileMode.Create);
            Debug.WriteLine("Writing Options Information");
            new BinaryFormatter().Serialize(stream, _options);
            stream.Close();
        }

        private void unpackingLoad_Click(object sender, EventArgs e)
        {
            if (openFileOptions.ShowDialog() != DialogResult.OK) return;
            //Open the file written above and read values from it.
            Stream stream = File.Open(openFileOptions.FileName, FileMode.Open);
            Debug.WriteLine("Reading Options Information");
            _options = (SteganographyOptions) new BinaryFormatter().Deserialize(stream);
            stream.Close();
            ApplyUnpackingOption();
        }

        private void packingPoliticsFake_CheckedChanged(object sender, EventArgs e)
        {
            packingPoliticsText.ReadOnly = !packingPoliticsFake.Checked;
        }
    }
}