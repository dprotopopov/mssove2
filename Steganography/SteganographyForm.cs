using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Steganography
{
    public partial class SteganographyForm : Form
    {
        private const string BitmapImagesFilter = "Bitmap Images|*.bmp";
        private readonly BitmapSteganography _bitmapSteganography = new BitmapSteganography();
        private SteganographyBitmap _inputBitmap;
        private SteganographyOptions _options = new SteganographyOptions();
        private SteganographyBitmap _outputBitmap;
        private SteganographyBitmap _sampleBitmap;

        public SteganographyForm()
        {
            InitializeComponent();
            packingMixer.Items.AddRange(Mixer.ComboBoxItems);
            packingGamma.Items.AddRange(Gamma.ComboBoxItems);
            packingPixelFormat.Items.AddRange(SteganographyBitmap.ComboBoxItems);
            unpackingMixer.Items.AddRange(Mixer.ComboBoxItems);
            unpackingGamma.Items.AddRange(Gamma.ComboBoxItems);
            try
            {
                Stream stream = File.Open("default.options", FileMode.Open);
                Debug.WriteLine("Reading Default Options Information");
                _options = (SteganographyOptions) new BinaryFormatter().Deserialize(stream);
                stream.Close();
            }
            catch (Exception)
            {
                _options.PixelFormatIndex = 0;
                _options.GammaIndex = 1;
                _options.MixerIndex = 1;
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

            SetPackingOption();
            SetUnpackingOption();
        }

        private void SetPackingOption()
        {
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
            packingMixer.SelectedIndex = _options.MixerIndex;
            packingGamma.SelectedIndex = _options.GammaIndex;
            packingPixelFormat.SelectedIndex = _options.PixelFormatIndex;
            _sampleBitmap = _options.SampleBitmap;
            _inputBitmap = _options.InputBitmap;
            _outputBitmap = _options.OutputBitmap;
        }

        private void SetUnpackingOption()
        {
            unpackingKey.Text = _options.SteganographyKey;
            unpackingExpand.Value = _options.ExpandSize;
            unpackingFilter.Value = _options.FilterStep;
            unpackingDecompress.Checked = _options.Compress;
            unpackingMixer.SelectedIndex = _options.MixerIndex;
            unpackingGamma.SelectedIndex = _options.GammaIndex;
            _sampleBitmap = _options.SampleBitmap;
            _inputBitmap = _options.InputBitmap;
            _outputBitmap = _options.OutputBitmap;
        }

        private void GetPackingOption()
        {
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
            _options.MixerIndex = packingMixer.SelectedIndex;
            _options.GammaIndex = packingGamma.SelectedIndex;
            _options.PixelFormatIndex = packingPixelFormat.SelectedIndex;
            _options.SampleBitmap = _sampleBitmap;
            _options.InputBitmap = _inputBitmap;
            _options.OutputBitmap = _outputBitmap;
        }

        private void GetUnpackingOption()
        {
            _options.SteganographyKey = unpackingKey.Text;
            _options.ExpandSize = (int) unpackingExpand.Value;
            _options.FilterStep = (int) unpackingFilter.Value;
            _options.Compress = unpackingDecompress.Checked;
            _options.MixerIndex = unpackingMixer.SelectedIndex;
            _options.GammaIndex = unpackingGamma.SelectedIndex;
            _options.SampleBitmap = _sampleBitmap;
            _options.InputBitmap = _inputBitmap;
            _options.OutputBitmap = _outputBitmap;
        }

        private void packing_Click(object sender, EventArgs e)
        {
            try
            {
                if (packingSample.Image == null) throw new Exception("Нет изображения");
                GetPackingOption();
                _outputBitmap = _bitmapSteganography.Packing(_options);
                packingImage.Image = _outputBitmap.GetBitmap();
                SetUnpackingOption();
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
                pictureBox1.Image = _options.BlurBitmap.GetBitmap();
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
                openSampleDialog.Filter = BitmapImagesFilter;
                if (openSampleDialog.ShowDialog() != DialogResult.OK) return;
                _sampleBitmap = new SteganographyBitmap(openSampleDialog.FileName);
                packingSample.Image = _sampleBitmap.GetBitmap();
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
                saveImageDialog.Filter = BitmapImagesFilter;
                if (_outputBitmap == null) throw new Exception("Нет изображения");
                if (saveImageDialog.ShowDialog() != DialogResult.OK) return;
                _outputBitmap.Save(saveImageDialog.FileName);
                Debug.WriteLine(_outputBitmap.Length);
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
                openImageDialog.Filter = BitmapImagesFilter;
                if (openImageDialog.ShowDialog() != DialogResult.OK) return;
                _inputBitmap = new SteganographyBitmap(openImageDialog.FileName);
                unpackingImage.Image = _inputBitmap.GetBitmap();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void packingViewSequence_Click(object sender, EventArgs e)
        {
            new GammaForm(packingKey.Text, (int) packingExpand.Value, packingGamma.SelectedIndex).ShowDialog();
        }

        private void unpackingViewSequence_Click(object sender, EventArgs e)
        {
            new GammaForm(packingKey.Text, (int) packingExpand.Value, unpackingGamma.SelectedIndex).ShowDialog();
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
            SetPackingOption();
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
            SetUnpackingOption();
        }

        private void packingPoliticsFake_CheckedChanged(object sender, EventArgs e)
        {
            packingPoliticsText.ReadOnly = !packingPoliticsFake.Checked;
        }
    }
}