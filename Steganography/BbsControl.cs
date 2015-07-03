using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;
using DevExpress.XtraVerticalGrid.Events;
using DevExpress.XtraVerticalGrid.Rows;
using Emgu.CV.UI;
using Steganography.Options;

namespace Steganography
{
    // A delegate type for hooking up change notifications.

    public partial class BbsControl : XtraUserControl
    {
        public delegate void SelectedModeChangedEventHandler(object sender, EventArgs e);

        // An event that clients can use to be notified whenever the
        // elements of the list change.

        public enum Mode
        {
            Pack = 0,
            Unpack = 1,
            Options = 2
        }

        private readonly BitmapSteganography _bitmapSteganography = new BitmapSteganography();
        private BbsOptions _bbsOptions = new BbsOptions();
        private CvBitmap _inputBitmap;
        private CvBitmap _medianBitmap;
        private CvBitmap _outputBitmap;
        private CvBitmap _sampleBitmap;

        public BbsControl()
        {
            InitializeComponent();
            repositoryItemComboBoxArchiver.Items.AddRange(Archiver.ComboBoxItems);
            repositoryItemComboBoxGamma.Items.AddRange(Gamma.ComboBoxItems);
            repositoryItemComboBoxMixer.Items.AddRange(Mixer.ComboBoxItems);
            repositoryItemComboBoxPolitic.Items.AddRange(Politic.ComboBoxItems);
            repositoryItemComboBoxPixelFormat.Items.AddRange(CvBitmap.ComboBoxItems);
            try
            {
                Stream stream = File.Open("default.options", FileMode.Open);
                Debug.WriteLine("Reading Default Options Information");
                _bbsOptions = (BbsOptions) new BinaryFormatter().Deserialize(stream);
                stream.Close();
            }
            catch (Exception)
            {
                _bbsOptions.PixelFormatIndex = 0;
                _bbsOptions.GammaIndex = 1;
                _bbsOptions.MixerIndex = 1;
                _bbsOptions.ArchiverIndex = 0;
                _bbsOptions.ExpandSize = 64;
                _bbsOptions.Alpha = 10;
                _bbsOptions.SampleAutoresize = false;
                _bbsOptions.PoliticIndex = 0;
                _bbsOptions.PoliticText = "FAKE";
                _bbsOptions.Key = "WELCOME";
                _bbsOptions.FilterStep = 3;
                _bbsOptions.IndexToObject();
            }

            _sampleBitmap = _bbsOptions.SampleBitmap;
            _inputBitmap = _bbsOptions.InputBitmap;
            _outputBitmap = _bbsOptions.OutputBitmap;
            _medianBitmap = _bbsOptions.MedianBitmap;


            propertyGridControlPack.DefaultEditors.AddRange(new[]
            {
                new DefaultEditor(typeof (bool), repositoryItemCheckEditBoolean),
                new DefaultEditor(typeof (int), repositoryItemSpinEditNumber),
                new DefaultEditor(typeof (string), repositoryItemTextEditString)
            });
            propertyGridControlPack.RepositoryItems.AddRange(new RepositoryItem[]
            {
                repositoryItemComboBoxMixer,
                repositoryItemComboBoxGamma,
                repositoryItemComboBoxArchiver,
                repositoryItemComboBoxPolitic,
                repositoryItemComboBoxPixelFormat,
                repositoryItemMemoEditPoliticText,
                repositoryItemCheckEditBoolean,
                repositoryItemSpinEditNumber,
                repositoryItemTextEditString
            });
            propertyGridControlUnpack.DefaultEditors.AddRange(new[]
            {
                new DefaultEditor(typeof (bool), repositoryItemCheckEditBoolean),
                new DefaultEditor(typeof (int), repositoryItemSpinEditNumber),
                new DefaultEditor(typeof (string), repositoryItemTextEditString)
            });
            propertyGridControlUnpack.RepositoryItems.AddRange(new RepositoryItem[]
            {
                repositoryItemComboBoxMixer,
                repositoryItemComboBoxGamma,
                repositoryItemComboBoxArchiver,
                repositoryItemComboBoxPolitic,
                repositoryItemComboBoxPixelFormat,
                repositoryItemMemoEditPoliticText,
                repositoryItemCheckEditBoolean,
                repositoryItemSpinEditNumber,
                repositoryItemTextEditString
            });

            ArchiverComboBoxItem1.Properties.RowEdit = ArchiverComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
            MixerComboBoxItem1.Properties.RowEdit = MixerComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
            GammaComboBoxItem1.Properties.RowEdit = GammaComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
            PoliticComboBoxItem1.Properties.RowEdit = PoliticComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
            PoliticText1.Properties.RowEdit = PoliticText.Properties.RowEdit.Clone() as RepositoryItem;
            PixelFormatComboBoxItem1.Properties.RowEdit =
                PixelFormatComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;

            ArchiverComboBoxItem2.Properties.RowEdit = ArchiverComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
            MixerComboBoxItem2.Properties.RowEdit = MixerComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
            GammaComboBoxItem2.Properties.RowEdit = GammaComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;

            propertyGridControlOptions.SelectedObject = _bbsOptions;
            propertyGridControlPack.SelectedObject = _bbsOptions;
            propertyGridControlUnpack.SelectedObject = _bbsOptions;

            openSampleDialog.Filter =
                openImageDialog.Filter =
                    saveImageDialog.Filter =
                        @"Bitmap Images (*.bmp)|*.bmp|All Files (*.*)|*.*";
            saveFileDialog.Filter =
                openFileDialog.Filter =
                    @"Rich Text Files (*.rtf)|*.rtf|All Files (*.*)|*.*";
        }

        public Mode SelectedMode
        {
            get { return (Mode) xtraTabControl1.SelectedTabPageIndex; }
            set { xtraTabControl1.SelectedTabPageIndex = (int) value; }
        }

        public bool TabsVisible
        {
// ReSharper disable UnusedMember.Global
            get { return xtraTabControl1.ShowTabHeader == DefaultBoolean.True; }
// ReSharper restore UnusedMember.Global
            set { xtraTabControl1.ShowTabHeader = (value || DesignMode) ? DefaultBoolean.True : DefaultBoolean.False; }
        }


        public event SelectedModeChangedEventHandler SelectedModeChanged;

        public void Execute()
        {
            switch (SelectedMode)
            {
                case Mode.Pack:
                    try
                    {
                        if (packingSample.Image == null) throw new Exception("Нет изображения");
                        _bbsOptions.IndexToObject();
                        _bbsOptions.RtfText = packFile.RtfText;
                        _bbsOptions.SampleBitmap = _sampleBitmap;
                        _bitmapSteganography.Pack(_bbsOptions);
                        _outputBitmap = _bbsOptions.OutputBitmap;
                        packingImage.Image = _outputBitmap.GetBitmap();
                    }
                    catch (Exception exception)
                    {
                        XtraMessageBox.Show(exception.Message);
                    }
                    break;
                case Mode.Unpack:
                    try
                    {
                        if (unpackImage.Image == null) throw new Exception("Нет изображения");

                        _bbsOptions.IndexToObject();
                        _bbsOptions.InputBitmap = _inputBitmap;
                        _bitmapSteganography.Unpack(_bbsOptions);
                        _outputBitmap = _bbsOptions.OutputBitmap;
                        _medianBitmap = _bbsOptions.MedianBitmap;
                        unpackFile.RtfText = _bbsOptions.RtfText;
                        pictureBox1.Image = _medianBitmap.GetBitmap();
                    }
                    catch (Exception exception)
                    {
                        XtraMessageBox.Show(exception.Message);
                    }
                    break;
            }
        }

        public void ViewSequence()
        {
            new GammaForm(_bbsOptions.Key, _bbsOptions.ExpandSize, _bbsOptions.GammaIndex)
                .ShowDialog();
        }

        private void packSample_Click(object sender, EventArgs e)
        {
            PackOpenImage();
        }

        public void PackOpenImage()
        {
            try
            {
                if (openSampleDialog.ShowDialog() != DialogResult.OK) return;
                _sampleBitmap = new CvBitmap(openSampleDialog.FileName);
                packingSample.Image = _sampleBitmap.GetBitmap();
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
        }

        private void packImage_Click(object sender, EventArgs e)
        {
            PackSaveImage();
        }

        public void PackSaveImage()
        {
            try
            {
                if (_outputBitmap == null) throw new Exception("Нет изображения");
                if (saveImageDialog.ShowDialog() != DialogResult.OK) return;
                _outputBitmap.Save(saveImageDialog.FileName);
                Debug.WriteLine(_outputBitmap.Length);
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
        }

        private void unpackImage_Click(object sender, EventArgs e)
        {
            UnpackOpenImage();
        }

        public void UnpackOpenImage()
        {
            try
            {
                if (openImageDialog.ShowDialog() != DialogResult.OK) return;
                _inputBitmap = new CvBitmap(openImageDialog.FileName);
                unpackImage.Image = _inputBitmap.GetBitmap();
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
        }


        public void UnpackSaveFile()
        {
            try
            {
                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
                using (StreamWriter writer = File.CreateText(saveFileDialog.FileName))
                    writer.Write(unpackFile.RtfText);
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
        }

        public void PackOpenFile()
        {
            try
            {
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                using (StreamReader reader = File.OpenText(openFileDialog.FileName))
                    packFile.RtfText = reader.ReadToEnd();
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
        }

        public void OptionsSave()
        {
            if (saveOptionsDialog.ShowDialog() != DialogResult.OK) return;
            if (SelectedMode == Mode.Options) propertyGridControlOptions.UpdateFocusedRecord();
            if (SelectedMode == Mode.Pack) propertyGridControlPack.UpdateFocusedRecord();
            if (SelectedMode == Mode.Unpack) propertyGridControlUnpack.UpdateFocusedRecord();
            using (Stream stream = File.Open(saveOptionsDialog.FileName, FileMode.Create))
                new BinaryFormatter().Serialize(stream, _bbsOptions);
        }

        public void OptionsLoad()
        {
            if (openOptionsDialog.ShowDialog() != DialogResult.OK) return;
            using (Stream stream = File.Open(openOptionsDialog.FileName, FileMode.Open))
                _bbsOptions = (BbsOptions) new BinaryFormatter().Deserialize(stream);
            propertyGridControlOptions.Refresh();
            propertyGridControlPack.Refresh();
            propertyGridControlUnpack.Refresh();
        }

        private void SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            propertyGridControlOptions.Refresh();
            propertyGridControlPack.Refresh();
            propertyGridControlUnpack.Refresh();
            if (SelectedModeChanged == null) return;
            SelectedModeChanged(this, e);
        }

        private void SelectedItemChanged(object sender, EventArgs e)
        {
            if (SelectedMode == Mode.Options) propertyGridControlOptions.UpdateFocusedRecord();
            if (SelectedMode == Mode.Pack) propertyGridControlPack.UpdateFocusedRecord();
            if (SelectedMode == Mode.Unpack) propertyGridControlUnpack.UpdateFocusedRecord();
            _bbsOptions.ObjectToIndex();
        }

        private void unpackFile_DoubleClick(object sender, EventArgs e)
        {
            UnpackSaveFile();
        }

        private void packFile_DoubleClick(object sender, EventArgs e)
        {
            PackOpenFile();
        }

        public void ShowCipherImage()
        {
            try
            {
                using (var imageViewer = new ImageViewer(_outputBitmap.Image, "Cipher Image"))
                    imageViewer.ShowDialog();
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
        }

        public void UnpackFileShow()
        {
            try
            {
                var richEditForm = new RichTextEditor {RtfText = unpackFile.RtfText};
                if (richEditForm.ShowDialog() != DialogResult.OK) return;
                unpackFile.RtfText = richEditForm.RtfText;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
        }

        private void CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (SelectedMode == Mode.Options) propertyGridControlOptions.UpdateFocusedRecord();
            if (SelectedMode == Mode.Pack) propertyGridControlPack.UpdateFocusedRecord();
            if (SelectedMode == Mode.Unpack) propertyGridControlUnpack.UpdateFocusedRecord();
            _bbsOptions.ObjectToIndex();
        }

        public void ShowSampleImage()
        {
            try
            {
                using (var imageViewer = new ImageViewer(_sampleBitmap.Image, "Sample Image"))
                    imageViewer.ShowDialog();
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
        }

        public void PackFileShow()
        {
            try
            {
                var richEditForm = new RichTextEditor {RtfText = packFile.RtfText};
                if (richEditForm.ShowDialog() != DialogResult.OK) return;
                packFile.RtfText = richEditForm.RtfText;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
        }

        public void ShowInputImage()
        {
            try
            {
                using (var imageViewer = new ImageViewer(_inputBitmap.Image, "Input Image"))
                    imageViewer.ShowDialog();
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
        }

        public void ShowMedianImage()
        {
            try
            {
                using (var imageViewer = new ImageViewer(_medianBitmap.Image, "Median Image"))
                    imageViewer.ShowDialog();
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
        }
    }
}