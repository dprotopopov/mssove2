using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using BBSApp.SendFileTo;
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

namespace BBSApp
{
    // A delegate type for hooking up change notifications.

    public partial class BbsControl : XtraUserControl
    {
        public delegate void SelectedModeChangedEventHandler(object sender, EventArgs e);

        // An event that clients can use to be notified whenever the
        // elements of the list change.

        public enum Mode
        {
            Welcome = 0,
            Pack = 1,
            Unpack = 2,
            Options = 3
        }

        private const int BitsPerByte = 8; // Количество битов в байте

        private BbsOptions _bbsOptions;
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
            repositoryItemComboBoxBarcode.Items.AddRange(Barcode.ComboBoxItems);


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

            ArchiverComboBoxItem2.Properties.RowEdit = ArchiverComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
            EccComboBoxItem2.Properties.RowEdit = EccComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
            MixerComboBoxItem2.Properties.RowEdit = MixerComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;
            GammaComboBoxItem2.Properties.RowEdit = GammaComboBoxItem.Properties.RowEdit.Clone() as RepositoryItem;


            openSampleDialog.Filter =
                openImageDialog.Filter =
                    saveImageDialog.Filter =
                        @"Bitmap Images (*.bmp)|*.bmp|All Files (*.*)|*.*";
            saveFileDialog.Filter =
                openFileDialog.Filter =
                    @"Rich Text Files (*.rtf)|*.rtf|All Files (*.*)|*.*";
        }

        public BbsOptions BbsOptions
        {
            get { return _bbsOptions; }
            set{
                propertyGridControlUnpack.SelectedObject =
                    propertyGridControlPack.SelectedObject =
                        propertyGridControlOptions.SelectedObject = 
                            _bbsOptions = value;
                _sampleBitmap = _bbsOptions.SampleBitmap;
                _inputBitmap = _bbsOptions.InputBitmap;
                _outputBitmap = _bbsOptions.OutputBitmap;
                _medianBitmap = _bbsOptions.MedianBitmap;
            }
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
                        BbsOptions.ObjectToIndex();
                        BbsOptions.RtfText = packFile.RtfText;
                        BbsOptions.SampleBitmap = _sampleBitmap;
                        _outputBitmap = BbsBuilder.Pack(BbsOptions);
                        packingImage.Image = _outputBitmap.Bitmap;
                        propertyGridControlOptions.Refresh();
                        propertyGridControlPack.Refresh();
                        propertyGridControlUnpack.Refresh();
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
                        BbsOptions.ObjectToIndex();
                        BbsOptions.InputBitmap = _inputBitmap;
                        unpackFile.RtfText = BbsBuilder.Unpack(BbsOptions);
                        _medianBitmap = BbsOptions.MedianBitmap;
                        unpackMedian.Image = _medianBitmap.Bitmap;
                        propertyGridControlOptions.Refresh();
                        propertyGridControlPack.Refresh();
                        propertyGridControlUnpack.Refresh();
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

        public bool ShowGamma()
        {
            BbsOptions.ObjectToIndex();
            var bytes = new byte[(BbsOptions.ExpandSize + BitsPerByte - 1)/BitsPerByte];
            using (var gamma = new Gamma(BbsOptions.GammaIndex, BbsOptions.Key))
                gamma.GetBytes(bytes);
            XtraMessageBox.Show(
                string.Join("", bytes.ToArray().Select(x => x.ToString("X02"))),
                "Псевдослучайная последовательность");
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
                    packingSample.Image = _sampleBitmap.Bitmap;
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
                    unpackImage.Image = _inputBitmap.Bitmap;
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
                if (string.IsNullOrWhiteSpace(unpackFile.RtfText)) throw new Exception("Нет текста");
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var writer = File.CreateText(saveFileDialog.FileName))
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
                    using (var reader = File.OpenText(openFileDialog.FileName))
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
            BbsOptions.ObjectToIndex();
            using (Stream stream = File.Open(saveOptionsDialog.FileName, FileMode.Create))
                new BinaryFormatter().Serialize(stream, BbsOptions);
            return true;
        }

        public bool OptionsLoad()
        {
            if (openOptionsDialog.ShowDialog() != DialogResult.OK) return false;
            using (Stream stream = File.Open(openOptionsDialog.FileName, FileMode.Open))
                BbsOptions = (BbsOptions) new BinaryFormatter().Deserialize(stream);
            BbsOptions.IndexToObject();
            propertyGridControlOptions.SelectedObject = BbsOptions;
            propertyGridControlPack.SelectedObject = BbsOptions;
            propertyGridControlUnpack.SelectedObject = BbsOptions;
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
                if (_outputBitmap == null) throw new Exception("Нет изображения");
                using (var imageViewer = new ImageViewer(_outputBitmap, "Отправляемое изображение"))
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
                BbsOptions.ObjectToIndex();
                using (var barcode = new Barcode(BbsOptions.BarcodeIndex)
                {
                    ArchiverIndex = BbsOptions.ArchiverIndex,
                    EccIndex = BbsOptions.EccIndex,
                    MixerIndex = BbsOptions.MixerIndex,
                    GammaIndex = BbsOptions.GammaIndex,
                    ExpandSize = BbsOptions.ExpandSize,
                    EccCodeSize = BbsOptions.EccCodeSize,
                    EccDataSize = BbsOptions.EccDataSize,
                    MaximumGamma = BbsOptions.MaximumGamma,
                    Key = BbsOptions.Key
                })
                using (var image = new Image<Gray, byte>(barcode.Encode()))
                using (var imageViewer = new ImageViewer(image, "Баркод"))
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
                using (var richEditForm = new RichTextEditor
                {
                    RtfText = unpackFile.RtfText,
                    Text = @"Полученное сообщение"
                })
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
                if (_sampleBitmap == null) throw new Exception("Нет изображения");
                using (var imageViewer = new ImageViewer(_sampleBitmap, "Исходное изображение"))
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
                using (var richEditForm = new RichTextEditor
                {
                    RtfText = packFile.RtfText,
                    Text = @"Отправляемое сообщение"
                })
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
                if (_inputBitmap == null) throw new Exception("Нет изображения");
                using (var imageViewer = new ImageViewer(_inputBitmap, "Полученное изображение"))
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
                if (_medianBitmap == null) throw new Exception("Нет изображения");
                using (var imageViewer = new ImageViewer(_medianBitmap, "Размытое изображение"))
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
                using (var barcode = new Barcode(_inputBitmap.Bitmap))
                    XtraMessageBox.Show(barcode.Decode(), "Баркод");
                return true;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }

        public bool Erase()
        {
            try
            {
                switch (SelectedMode)
                {
                    case Mode.Pack:
                        packFile.RtfText = string.Empty;
                        packingSample.Image = null;
                        packingImage.Image = null;
                        _sampleBitmap = null;
                        _outputBitmap = null;
                        return true;
                    case Mode.Unpack:
                        unpackFile.RtfText = string.Empty;
                        unpackImage.Image = null;
                        unpackMedian.Image = null;
                        _inputBitmap = null;
                        _medianBitmap = null;
                        return true;
                }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }

        public bool PackSendImage()
        {
            try
            {
                if (_outputBitmap == null) throw new Exception("Нет изображения");
                var fileName = Path.GetTempPath() + Guid.NewGuid() + ".bmp";
                _outputBitmap.Save(fileName);
                var mapi = new Mapi();
                mapi.AddAttachment(fileName);
                mapi.SendMailPopup("", "");
                File.Delete(fileName);
                return true;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }

        public bool UnpackSendFile()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(unpackFile.RtfText)) throw new Exception("Нет текста");
                var fileName = Path.GetTempPath() + Guid.NewGuid() + ".rtf";
                using (var writer = File.CreateText(fileName))
                    writer.Write(unpackFile.RtfText);
                var mapi = new Mapi();
                mapi.AddAttachment(fileName);
                mapi.SendMailPopup("", "");
                File.Delete(fileName);
                return true;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }

        /// <summary>
        ///     Отправка письма на почтовый ящик C# mail send
        /// </summary>
        /// <param name="smtpServer">Имя SMTP-сервера</param>
        /// <param name="from">Адрес отправителя</param>
        /// <param name="password">пароль к почтовому ящику отправителя</param>
        /// <param name="mailto">Адрес получателя</param>
        /// <param name="caption">Тема письма</param>
        /// <param name="message">Сообщение</param>
        /// <param name="attachFile">Присоединенный файл</param>
        public static void SendMail(string smtpServer, string from, string password,
            string mailto, string caption, string message, string attachFile = null)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(from);
                    mail.To.Add(new MailAddress(mailto));
                    mail.Subject = caption;
                    mail.Body = message;
                    if (!string.IsNullOrEmpty(attachFile))
                        mail.Attachments.Add(new Attachment(attachFile));
                    using (var client = new SmtpClient())
                    {
                        client.Host = smtpServer;
                        client.Port = 587;
                        client.EnableSsl = true;
                        client.Credentials = new NetworkCredential(from.Split('@')[0], password);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Send(mail);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public bool Check()
        {
            try
            {
                switch (SelectedMode)
                {
                    case Mode.Pack:
                        SelectedMode = Mode.Unpack;
                        BbsOptions.ObjectToIndex();
                        unpackFile.RtfText = string.Empty;
                        unpackImage.Image = null;
                        unpackMedian.Image = null;
                        _inputBitmap = null;
                        _medianBitmap = null;
                        BbsOptions.InputBitmap = _inputBitmap = _outputBitmap;
                        unpackImage.Image = _inputBitmap.Bitmap;
                        unpackFile.RtfText = BbsBuilder.Unpack(BbsOptions);
                        _medianBitmap = BbsOptions.MedianBitmap;
                        unpackMedian.Image = _medianBitmap.Bitmap;
                        var check = string.Compare(unpackFile.RtfText, packFile.RtfText, StringComparison.Ordinal) == 0;
                        XtraMessageBox.Show(check ? "Проверка пройдена" : "Проверка не пройдена", "Проверка");
                        return check;
                }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }

        public bool CheckSum()
        {
            var sha256 = SHA256.Create();
            try
            {
                switch (SelectedMode)
                {
                    case Mode.Pack:
                        using (var stream = new MemoryStream(Encoding.GetEncoding(0).GetBytes(packFile.RtfText)))
                            XtraMessageBox.Show(
                                string.Join("",
                                    sha256.ComputeHash(stream).ToArray().Select(b => string.Format("{0:X2}", b))),
                                "Контрольная сумма");
                        return true;
                    case Mode.Unpack:
                        using (var stream = new MemoryStream(Encoding.GetEncoding(0).GetBytes(unpackFile.RtfText)))
                            XtraMessageBox.Show(
                                string.Join("",
                                    sha256.ComputeHash(stream).ToArray().Select(b => string.Format("{0:X2}", b))),
                                "Контрольная сумма");
                        return true;
                }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.Message);
            }
            return false;
        }
    }
}