using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using BBSLib;
using BBSLib.Options;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;
using DevExpress.XtraVerticalGrid.Events;
using DevExpress.XtraVerticalGrid.Rows;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;

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

        private const string DefaultOptionsFileName = "default.options";

        private readonly BbsBuilder _bbsBuilder = new BbsBuilder();
        private BbsOptions _bbsOptions = new BbsOptions();
        private CvBitmap _inputBitmap;
        private CvBitmap _medianBitmap;
        private CvBitmap _outputBitmap;
        private CvBitmap _sampleBitmap;

        public BbsControl()
        {
            InitializeComponent();
            repositoryItemComboBoxArchiver.Items.AddRange(Archiver.ComboBoxItems);
            repositoryItemComboBoxEcc.Items.AddRange(Ecc.ComboBoxItems);
            repositoryItemComboBoxGamma.Items.AddRange(Gamma.ComboBoxItems);
            repositoryItemComboBoxMixer.Items.AddRange(Mixer.ComboBoxItems);
            repositoryItemComboBoxPolitic.Items.AddRange(Politic.ComboBoxItems);
            repositoryItemComboBoxPixelFormat.Items.AddRange(CvBitmap.ComboBoxItems);
            repositoryItemComboBoxBarcode.Items.AddRange(Barcode.ComboBoxItems);
            try
            {
                Debug.WriteLine("Reading Default Options Information");
                using (Stream stream = File.Open(DefaultOptionsFileName, FileMode.Open))
                    _bbsOptions = (BbsOptions) new BinaryFormatter().Deserialize(stream);
            }
            catch (Exception)
            {
                _bbsOptions.PixelFormatIndex = 0;
                _bbsOptions.PoliticIndex = 0;
                _bbsOptions.EccIndex = 1;
                _bbsOptions.GammaIndex = 1;
                _bbsOptions.MixerIndex = 1;
                _bbsOptions.ArchiverIndex = 1;
                _bbsOptions.BarcodeIndex = 1;

                _bbsOptions.EccCodeSize = 127;
                _bbsOptions.EccDataSize = 63;
                _bbsOptions.ExpandSize = 31;
                _bbsOptions.Alpha = 15;
                _bbsOptions.FilterStep = 7;

                _bbsOptions.AutoResize = true;
                _bbsOptions.AutoAlpha = true;
                _bbsOptions.ExtractBarcode = true;
                _bbsOptions.MaximumGamma = true;

                _bbsOptions.PoliticText = "FAKE";
                _bbsOptions.Key = "WELCOME";

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
                repositoryItemComboBoxBarcode,
                repositoryItemComboBoxMixer,
                repositoryItemComboBoxGamma,
                repositoryItemComboBoxEcc,
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
                repositoryItemComboBoxBarcode,
                repositoryItemComboBoxMixer,
                repositoryItemComboBoxGamma,
                repositoryItemComboBoxEcc,
                repositoryItemComboBoxArchiver,
                repositoryItemComboBoxPolitic,
                repositoryItemComboBoxPixelFormat,
                repositoryItemMemoEditPoliticText,
                repositoryItemCheckEditBoolean,
                repositoryItemSpinEditNumber,
                repositoryItemTextEditString
            });

            ArchiverComboBoxItem1.Properties.RowEdit = ArchiverComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
            EccComboBoxItem1.Properties.RowEdit = EccComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
            MixerComboBoxItem1.Properties.RowEdit = MixerComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
            GammaComboBoxItem1.Properties.RowEdit = GammaComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
            BarcodeComboBoxItem1.Properties.RowEdit = BarcodeComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
            PoliticComboBoxItem1.Properties.RowEdit = PoliticComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
            PoliticText1.Properties.RowEdit = PoliticText.Properties.RowEdit.Clone() as RepositoryItem;
            PixelFormatComboBoxItem1.Properties.RowEdit =
                PixelFormatComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;


            ArchiverComboBoxItem2.Properties.RowEdit = ArchiverComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
            EccComboBoxItem2.Properties.RowEdit = EccComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
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

        public bool Execute()
        {
            switch (SelectedMode)
            {
                case Mode.Pack:
                    try
                    {
                        if (packingSample.Image == null) throw new Exception("Нет изображения");
                        _bbsOptions.ObjectToIndex();
                        _bbsOptions.RtfText = packFile.RtfText;
                        _bbsOptions.SampleBitmap = _sampleBitmap;
                        _outputBitmap = _bbsBuilder.Pack(_bbsOptions);
                        packingImage.Image = _outputBitmap.GetBitmap();
                        return true;
                    }
                    catch (Exception exception)
                    {
                        XtraMessageBox.Show(exception.Message);
                        return false;
                    }
                case Mode.Unpack:
                    try
                    {
                        if (unpackImage.Image == null) throw new Exception("Нет изображения");
                        _bbsOptions.ObjectToIndex();
                        _bbsOptions.InputBitmap = _inputBitmap;
                        unpackFile.RtfText = _bbsBuilder.Unpack(_bbsOptions);
                        _medianBitmap = _bbsOptions.MedianBitmap;
                        pictureBox1.Image = _medianBitmap.GetBitmap();
                        return true;
                    }
                    catch (Exception exception)
                    {
                        XtraMessageBox.Show(exception.Message);
                        return false;
                    }
            }
            throw new NotImplementedException();
        }

        public bool ViewSequence()
        {
            _bbsOptions.ObjectToIndex();
            using (var form = new GammaForm(_bbsOptions.Key, _bbsOptions.EccCodeSize, _bbsOptions.GammaIndex))
                form.ShowDialog();
            return true;
        }

        private void packSample_Click(object sender, EventArgs e)
        {
            PackOpenImage();
        }

        public bool PackOpenImage()
        {
            try
            {
                if (openSampleDialog.ShowDialog() == DialogResult.OK)
                {
                    _sampleBitmap = new CvBitmap(openSampleDialog.FileName);
                    packingSample.Image = _sampleBitmap.GetBitmap();
                    return true;
                }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }

        private void packImage_Click(object sender, EventArgs e)
        {
            PackSaveImage();
        }

        public bool PackSaveImage()
        {
            try
            {
                if (_outputBitmap == null) throw new Exception("Нет изображения");
                if (saveImageDialog.ShowDialog() == DialogResult.OK)
                {
                    _outputBitmap.Save(saveImageDialog.FileName);
                    Debug.WriteLine(_outputBitmap.ToString());
                    return true;
                }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }

        private void unpackImage_Click(object sender, EventArgs e)
        {
            UnpackOpenImage();
        }

        public bool UnpackOpenImage()
        {
            try
            {
                if (openImageDialog.ShowDialog() == DialogResult.OK)
                {
                    _inputBitmap = new CvBitmap(openImageDialog.FileName);
                    unpackImage.Image = _inputBitmap.GetBitmap();
                    return true;
                }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }


        public bool UnpackSaveFile()
        {
            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = File.CreateText(saveFileDialog.FileName))
                        writer.Write(unpackFile.RtfText);
                    return true;
                }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }

        public bool PackOpenFile()
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader reader = File.OpenText(openFileDialog.FileName))
                        packFile.RtfText = reader.ReadToEnd();
                    return true;
                }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }

        public bool OptionsSave()
        {
            if (saveOptionsDialog.ShowDialog() != DialogResult.OK) return false;
            if (SelectedMode == Mode.Options) propertyGridControlOptions.UpdateFocusedRecord();
            if (SelectedMode == Mode.Pack) propertyGridControlPack.UpdateFocusedRecord();
            if (SelectedMode == Mode.Unpack) propertyGridControlUnpack.UpdateFocusedRecord();
            _bbsOptions.ObjectToIndex();
            using (Stream stream = File.Open(saveOptionsDialog.FileName, FileMode.Create))
                new BinaryFormatter().Serialize(stream, _bbsOptions);
            return true;
        }

        public bool OptionsLoad()
        {
            if (openOptionsDialog.ShowDialog() != DialogResult.OK) return false;
            using (Stream stream = File.Open(openOptionsDialog.FileName, FileMode.Open))
                _bbsOptions = (BbsOptions) new BinaryFormatter().Deserialize(stream);
            _bbsOptions.IndexToObject();
            propertyGridControlOptions.SelectedObject = _bbsOptions;
            propertyGridControlPack.SelectedObject = _bbsOptions;
            propertyGridControlUnpack.SelectedObject = _bbsOptions;
            return true;
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
        }

        private void unpackFile_DoubleClick(object sender, EventArgs e)
        {
            UnpackSaveFile();
        }

        private void packFile_DoubleClick(object sender, EventArgs e)
        {
            PackOpenFile();
        }

        public bool ShowCipherImage()
        {
            try
            {
                using (var imageViewer = new ImageViewer(_outputBitmap.Image, "Cipher Image"))
                    imageViewer.ShowDialog();
                return true;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }

        public bool ShowBarcodeImage()
        {
            try
            {
                _bbsOptions.ObjectToIndex();
                using (var barcode = new Barcode(_bbsOptions.BarcodeIndex)
                {
                    ArchiverIndex = _bbsOptions.ArchiverIndex,
                    EccIndex = _bbsOptions.EccIndex,
                    MixerIndex = _bbsOptions.MixerIndex,
                    GammaIndex = _bbsOptions.GammaIndex,
                    ExpandSize = _bbsOptions.ExpandSize,
                    EccCodeSize = _bbsOptions.EccCodeSize,
                    EccDataSize = _bbsOptions.EccDataSize,
                    MaximumGamma = _bbsOptions.MaximumGamma,
                    Key = _bbsOptions.Key,
                })
                using (var image = new Image<Gray, Byte>(barcode.Encode()))
                using (var imageViewer = new ImageViewer(image, "Barcode Image"))
                    imageViewer.ShowDialog();
                return true;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }

        public bool UnpackFileShow()
        {
            try
            {
                using (var richEditForm = new RichTextEditor {RtfText = unpackFile.RtfText})
                    if (richEditForm.ShowDialog() == DialogResult.OK)
                    {
                        unpackFile.RtfText = richEditForm.RtfText;
                        return true;
                    }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }

        private void CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (SelectedMode == Mode.Options) propertyGridControlOptions.UpdateFocusedRecord();
            if (SelectedMode == Mode.Pack) propertyGridControlPack.UpdateFocusedRecord();
            if (SelectedMode == Mode.Unpack) propertyGridControlUnpack.UpdateFocusedRecord();
        }

        public bool ShowSampleImage()
        {
            try
            {
                using (var imageViewer = new ImageViewer(_sampleBitmap.Image, "Sample Image"))
                    imageViewer.ShowDialog();
                return true;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }

        public bool PackFileShow()
        {
            try
            {
                using (var richEditForm = new RichTextEditor {RtfText = packFile.RtfText})
                    if (richEditForm.ShowDialog() == DialogResult.OK)
                    {
                        packFile.RtfText = richEditForm.RtfText;
                        return true;
                    }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }

        public bool ShowInputImage()
        {
            try
            {
                using (var imageViewer = new ImageViewer(_inputBitmap.Image, "Input Image"))
                    imageViewer.ShowDialog();
                return true;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }

        public bool ShowMedianImage()
        {
            try
            {
                using (var imageViewer = new ImageViewer(_medianBitmap.Image, "Median Image"))
                    imageViewer.ShowDialog();
                return true;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }

        public bool ShowBarcodeText()
        {
            try
            {
                using (var barcode = new Barcode(_inputBitmap.Image.Bitmap))
                    XtraMessageBox.Show(barcode.Decode());
                return true;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }
    }
}