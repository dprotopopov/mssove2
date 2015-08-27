using DevExpress.XtraRichEdit;

namespace Steganography
{
    partial class BbsControl
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BbsControl));
            this.openSampleDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveOptionsDialog = new System.Windows.Forms.SaveFileDialog();
            this.openOptionsDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.packingSample = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.packFile = new DevExpress.XtraRichEdit.RichEditControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.packingImage = new DevExpress.XtraEditors.PictureEdit();
            this.propertyGridControlPack = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.category1 = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.MixerComboBoxItem1 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.ArchiverComboBoxItem1 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.GammaComboBoxItem1 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.EccComboBoxItem1 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.EccCodeSize1 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.EccDataSize1 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.categoryPack = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.SampleAutoresize1 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.Key1 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.Alpha1 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.PixelFormatComboBoxItem1 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.MaximumGamma1 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.ExpandSize1 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.BarcodeComboBoxItem1 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.PoliticComboBoxItem1 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.PoliticText1 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox1 = new DevExpress.XtraEditors.PictureEdit();
            this.unpackImage = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.unpackFile = new DevExpress.XtraRichEdit.RichEditControl();
            this.propertyGridControlUnpack = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.category2 = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.MixerComboBoxItem2 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.ArchiverComboBoxItem2 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.GammaComboBoxItem2 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.EccComboBoxItem2 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.EccCodeSize2 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.EccDataSize2 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.categoryUnpack = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.Key2 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.FilterStep2 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.MaximumGamma2 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.ExpandSize2 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.ExtractBarcode2 = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabPageOptions = new DevExpress.XtraTab.XtraTabPage();
            this.propertyGridControlOptions = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.repositoryItemCheckEditBoolean = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemSpinEditNumber = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repositoryItemTextEditString = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemComboBoxMixer = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxGamma = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxArchiver = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxPolitic = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxPixelFormat = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemMemoEditPoliticText = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemComboBoxEcc = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxBarcode = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.category = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.MixerComboBoxItem = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.ArchiverComboBoxItem = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.GammaComboBoxItem = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.EccComboBoxItem = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.EccCodeSize = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.EccDataSize = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.categoryPackUnpack = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.SampleAutoresize = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.Key = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.FilterStep = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.Alpha = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.PixelFormatComboBoxItem = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.MaximumGamma = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.ExpandSize = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.BarcodeComboBoxItem = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.ExtractBarcode = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.PoliticComboBoxItem = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.PoliticText = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.PixelFormatIndex = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packingSample.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.packingImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlPack)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unpackImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlUnpack)).BeginInit();
            this.xtraTabPageOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlOptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditBoolean)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditString)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxMixer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxGamma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxArchiver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxPolitic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxPixelFormat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEditPoliticText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxEcc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxBarcode)).BeginInit();
            this.SuspendLayout();
            // 
            // openSampleDialog
            // 
            this.openSampleDialog.DefaultExt = "jpg";
            // 
            // saveImageDialog
            // 
            this.saveImageDialog.DefaultExt = "bmp";
            // 
            // openImageDialog
            // 
            this.openImageDialog.DefaultExt = "bmp";
            // 
            // saveOptionsDialog
            // 
            this.saveOptionsDialog.DefaultExt = "options";
            this.saveOptionsDialog.Filter = "Options (*.options)|*.options";
            // 
            // openOptionsDialog
            // 
            this.openOptionsDialog.DefaultExt = "options";
            this.openOptionsDialog.Filter = "Options (*.options)|*.options";
            // 
            // groupControl1
            // 
            this.groupControl1.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.groupControl1.Controls.Add(this.xtraTabControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(1235, 746);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "layoutControlGroup1";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 2);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1231, 742);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPageOptions});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.SelectedPageChanged);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.tableLayoutPanel1);
            this.xtraTabPage1.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage1.Image")));
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1225, 695);
            this.xtraTabPage1.Text = "Режим упаковки";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.99998F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00002F));
            this.tableLayoutPanel1.Controls.Add(this.labelControl3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelControl7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.packingSample, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelControl10, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.packFile, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelControl8, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.packingImage, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.propertyGridControlPack, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.56589F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.43411F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1225, 695);
            this.tableLayoutPanel1.TabIndex = 52;
            // 
            // labelControl7
            // 
            this.labelControl7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelControl7.Location = new System.Drawing.Point(3, 437);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(62, 19);
            this.labelControl7.TabIndex = 47;
            this.labelControl7.Text = "Образец";
            // 
            // packingSample
            // 
            this.packingSample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packingSample.Location = new System.Drawing.Point(3, 462);
            this.packingSample.Name = "packingSample";
            this.packingSample.Properties.ShowMenu = false;
            this.packingSample.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.packingSample.Size = new System.Drawing.Size(606, 230);
            this.packingSample.TabIndex = 16;
            this.packingSample.Click += new System.EventHandler(this.packSample_Click);
            this.packingSample.DoubleClick += new System.EventHandler(this.packSample_Click);
            // 
            // labelControl10
            // 
            this.labelControl10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelControl10.Location = new System.Drawing.Point(615, 3);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(84, 19);
            this.labelControl10.TabIndex = 50;
            this.labelControl10.Text = "Сообщение";
            // 
            // packFile
            // 
            this.packFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packFile.EnableToolTips = true;
            this.packFile.Location = new System.Drawing.Point(615, 28);
            this.packFile.Name = "packFile";
            this.packFile.Size = new System.Drawing.Size(607, 403);
            this.packFile.TabIndex = 1;
            this.packFile.DoubleClick += new System.EventHandler(this.packFile_DoubleClick);
            // 
            // labelControl8
            // 
            this.labelControl8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelControl8.Location = new System.Drawing.Point(615, 437);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(69, 19);
            this.labelControl8.TabIndex = 48;
            this.labelControl8.Text = "Картинка";
            // 
            // packingImage
            // 
            this.packingImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packingImage.Location = new System.Drawing.Point(615, 462);
            this.packingImage.Name = "packingImage";
            this.packingImage.Properties.ShowMenu = false;
            this.packingImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.packingImage.Size = new System.Drawing.Size(607, 230);
            this.packingImage.TabIndex = 15;
            this.packingImage.Click += new System.EventHandler(this.packImage_Click);
            // 
            // propertyGridControlPack
            // 
            this.propertyGridControlPack.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.propertyGridControlPack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControlPack.Location = new System.Drawing.Point(3, 28);
            this.propertyGridControlPack.Name = "propertyGridControlPack";
            this.propertyGridControlPack.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.category1,
            this.categoryPack});
            this.propertyGridControlPack.Size = new System.Drawing.Size(606, 403);
            this.propertyGridControlPack.TabIndex = 52;
            this.propertyGridControlPack.CellValueChanged += new DevExpress.XtraVerticalGrid.Events.CellValueChangedEventHandler(this.CellValueChanged);
            // 
            // category1
            // 
            this.category1.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.MixerComboBoxItem1,
            this.ArchiverComboBoxItem1,
            this.GammaComboBoxItem1,
            this.EccComboBoxItem1});
            this.category1.Name = "category1";
            this.category1.Properties.Caption = "Алгоритмы";
            // 
            // MixerComboBoxItem1
            // 
            this.MixerComboBoxItem1.Height = 23;
            this.MixerComboBoxItem1.Name = "MixerComboBoxItem1";
            this.MixerComboBoxItem1.Properties.Caption = "Алгоритм перемешивания";
            this.MixerComboBoxItem1.Properties.FieldName = "MixerComboBoxItem";
            // 
            // ArchiverComboBoxItem1
            // 
            this.ArchiverComboBoxItem1.Name = "ArchiverComboBoxItem1";
            this.ArchiverComboBoxItem1.Properties.Caption = "Алгоритм компрессии";
            this.ArchiverComboBoxItem1.Properties.FieldName = "ArchiverComboBoxItem";
            // 
            // GammaComboBoxItem1
            // 
            this.GammaComboBoxItem1.Name = "GammaComboBoxItem1";
            this.GammaComboBoxItem1.Properties.Caption = "Алгоритм гаммы";
            this.GammaComboBoxItem1.Properties.FieldName = "GammaComboBoxItem";
            // 
            // EccComboBoxItem1
            // 
            this.EccComboBoxItem1.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.EccCodeSize1,
            this.EccDataSize1});
            this.EccComboBoxItem1.IsChildRowsLoaded = true;
            this.EccComboBoxItem1.Name = "EccComboBoxItem1";
            this.EccComboBoxItem1.Properties.Caption = "Алгоритм коррекции ошибок";
            this.EccComboBoxItem1.Properties.FieldName = "EccComboBoxItem";
            // 
            // EccCodeSize1
            // 
            this.EccCodeSize1.Name = "EccCodeSize1";
            this.EccCodeSize1.Properties.Caption = "Длина кода";
            this.EccCodeSize1.Properties.FieldName = "EccCodeSize";
            // 
            // EccDataSize1
            // 
            this.EccDataSize1.Name = "EccDataSize1";
            this.EccDataSize1.Properties.Caption = "Длина данных";
            this.EccDataSize1.Properties.FieldName = "EccDataSize";
            // 
            // categoryPack
            // 
            this.categoryPack.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.SampleAutoresize1,
            this.Key1,
            this.Alpha1,
            this.PixelFormatComboBoxItem1,
            this.MaximumGamma1,
            this.ExpandSize1,
            this.BarcodeComboBoxItem1,
            this.PoliticComboBoxItem1});
            this.categoryPack.Name = "categoryPack";
            this.categoryPack.Properties.Caption = "Значения";
            // 
            // SampleAutoresize1
            // 
            this.SampleAutoresize1.Name = "SampleAutoresize1";
            this.SampleAutoresize1.Properties.Caption = "Маштабировать изображение";
            this.SampleAutoresize1.Properties.FieldName = "SampleAutoresize";
            // 
            // Key1
            // 
            this.Key1.Name = "Key1";
            this.Key1.Properties.Caption = "Ключ";
            this.Key1.Properties.FieldName = "Key";
            // 
            // Alpha1
            // 
            this.Alpha1.Name = "Alpha1";
            this.Alpha1.Properties.Caption = "Глубина погружения";
            this.Alpha1.Properties.FieldName = "Alpha";
            // 
            // PixelFormatComboBoxItem1
            // 
            this.PixelFormatComboBoxItem1.Name = "PixelFormatComboBoxItem1";
            this.PixelFormatComboBoxItem1.Properties.Caption = "Формат пикселей";
            this.PixelFormatComboBoxItem1.Properties.FieldName = "PixelFormatComboBoxItem";
            // 
            // MaximumGamma1
            // 
            this.MaximumGamma1.Name = "MaximumGamma1";
            this.MaximumGamma1.Properties.Caption = "Использовать гамму максимальной длины";
            this.MaximumGamma1.Properties.FieldName = "MaximumGamma";
            // 
            // ExpandSize1
            // 
            this.ExpandSize1.Name = "ExpandSize1";
            this.ExpandSize1.Properties.Caption = "Избыточность";
            this.ExpandSize1.Properties.FieldName = "ExpandSize";
            // 
            // BarcodeComboBoxItem1
            // 
            this.BarcodeComboBoxItem1.Name = "BarcodeComboBoxItem1";
            this.BarcodeComboBoxItem1.Properties.Caption = "Встраиваемый баркод";
            this.BarcodeComboBoxItem1.Properties.FieldName = "BarcodeComboBoxItem";
            // 
            // PoliticComboBoxItem1
            // 
            this.PoliticComboBoxItem1.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.PoliticText1});
            this.PoliticComboBoxItem1.IsChildRowsLoaded = true;
            this.PoliticComboBoxItem1.Name = "PoliticComboBoxItem1";
            this.PoliticComboBoxItem1.Properties.Caption = "Политика заполения лишних пикселей";
            this.PoliticComboBoxItem1.Properties.FieldName = "PoliticComboBoxItem";
            // 
            // PoliticText1
            // 
            this.PoliticText1.Name = "PoliticText1";
            this.PoliticText1.Properties.Caption = "Альтернативное сообщение";
            this.PoliticText1.Properties.FieldName = "PoliticText";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.tableLayoutPanel2);
            this.xtraTabPage2.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPage2.Image")));
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1225, 695);
            this.xtraTabPage2.Text = "Режим распаковки";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.99998F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00002F));
            this.tableLayoutPanel2.Controls.Add(this.labelControl2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelControl17, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox1, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.unpackImage, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelControl18, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.unpackFile, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.propertyGridControlUnpack, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelControl1, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.03101F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.96899F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1225, 695);
            this.tableLayoutPanel2.TabIndex = 48;
            // 
            // labelControl17
            // 
            this.labelControl17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelControl17.Location = new System.Drawing.Point(3, 441);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(69, 19);
            this.labelControl17.TabIndex = 45;
            this.labelControl17.Text = "Картинка";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(615, 466);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Properties.ShowMenu = false;
            this.pictureBox1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureBox1.Size = new System.Drawing.Size(607, 226);
            this.pictureBox1.TabIndex = 38;
            // 
            // unpackImage
            // 
            this.unpackImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unpackImage.Location = new System.Drawing.Point(3, 466);
            this.unpackImage.Name = "unpackImage";
            this.unpackImage.Properties.ShowMenu = false;
            this.unpackImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.unpackImage.Size = new System.Drawing.Size(606, 226);
            this.unpackImage.TabIndex = 14;
            this.unpackImage.Click += new System.EventHandler(this.unpackImage_Click);
            // 
            // labelControl18
            // 
            this.labelControl18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelControl18.Location = new System.Drawing.Point(615, 3);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(84, 19);
            this.labelControl18.TabIndex = 46;
            this.labelControl18.Text = "Сообщение";
            // 
            // unpackFile
            // 
            this.unpackFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unpackFile.EnableToolTips = true;
            this.unpackFile.Location = new System.Drawing.Point(615, 28);
            this.unpackFile.Name = "unpackFile";
            this.unpackFile.ReadOnly = true;
            this.unpackFile.Size = new System.Drawing.Size(607, 407);
            this.unpackFile.TabIndex = 9;
            this.unpackFile.DoubleClick += new System.EventHandler(this.unpackFile_DoubleClick);
            // 
            // propertyGridControlUnpack
            // 
            this.propertyGridControlUnpack.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.propertyGridControlUnpack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControlUnpack.Location = new System.Drawing.Point(3, 28);
            this.propertyGridControlUnpack.Name = "propertyGridControlUnpack";
            this.propertyGridControlUnpack.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.category2,
            this.categoryUnpack});
            this.propertyGridControlUnpack.Size = new System.Drawing.Size(606, 407);
            this.propertyGridControlUnpack.TabIndex = 48;
            this.propertyGridControlUnpack.CellValueChanged += new DevExpress.XtraVerticalGrid.Events.CellValueChangedEventHandler(this.CellValueChanged);
            // 
            // category2
            // 
            this.category2.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.MixerComboBoxItem2,
            this.ArchiverComboBoxItem2,
            this.GammaComboBoxItem2,
            this.EccComboBoxItem2});
            this.category2.Name = "category2";
            this.category2.Properties.Caption = "Алгоритмы";
            // 
            // MixerComboBoxItem2
            // 
            this.MixerComboBoxItem2.Name = "MixerComboBoxItem2";
            this.MixerComboBoxItem2.Properties.Caption = "Алгоритм перемешивания";
            this.MixerComboBoxItem2.Properties.FieldName = "MixerComboBoxItem";
            // 
            // ArchiverComboBoxItem2
            // 
            this.ArchiverComboBoxItem2.Name = "ArchiverComboBoxItem2";
            this.ArchiverComboBoxItem2.Properties.Caption = "Алгоритм компрессии";
            this.ArchiverComboBoxItem2.Properties.FieldName = "ArchiverComboBoxItem";
            // 
            // GammaComboBoxItem2
            // 
            this.GammaComboBoxItem2.Name = "GammaComboBoxItem2";
            this.GammaComboBoxItem2.Properties.Caption = "Алгоритм гаммы";
            this.GammaComboBoxItem2.Properties.FieldName = "GammaComboBoxItem";
            // 
            // EccComboBoxItem2
            // 
            this.EccComboBoxItem2.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.EccCodeSize2,
            this.EccDataSize2});
            this.EccComboBoxItem2.IsChildRowsLoaded = true;
            this.EccComboBoxItem2.Name = "EccComboBoxItem2";
            this.EccComboBoxItem2.Properties.Caption = "Алгоритм коррекции ошибок";
            this.EccComboBoxItem2.Properties.FieldName = "EccComboBoxItem";
            // 
            // EccCodeSize2
            // 
            this.EccCodeSize2.Name = "EccCodeSize2";
            this.EccCodeSize2.Properties.Caption = "Длина кода";
            this.EccCodeSize2.Properties.FieldName = "EccCodeSize";
            // 
            // EccDataSize2
            // 
            this.EccDataSize2.Name = "EccDataSize2";
            this.EccDataSize2.Properties.Caption = "Длина данных";
            this.EccDataSize2.Properties.FieldName = "EccDataSize";
            // 
            // categoryUnpack
            // 
            this.categoryUnpack.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.Key2,
            this.FilterStep2,
            this.MaximumGamma2,
            this.ExpandSize2,
            this.ExtractBarcode2});
            this.categoryUnpack.Name = "categoryUnpack";
            this.categoryUnpack.Properties.Caption = "Значения";
            // 
            // Key2
            // 
            this.Key2.Name = "Key2";
            this.Key2.Properties.Caption = "Ключ";
            this.Key2.Properties.FieldName = "Key";
            // 
            // FilterStep2
            // 
            this.FilterStep2.Height = 22;
            this.FilterStep2.Name = "FilterStep2";
            this.FilterStep2.Properties.Caption = "Параметр фильтра";
            this.FilterStep2.Properties.FieldName = "FilterStep";
            // 
            // MaximumGamma2
            // 
            this.MaximumGamma2.Name = "MaximumGamma2";
            this.MaximumGamma2.Properties.Caption = "Использовать гамму максимальной длины";
            this.MaximumGamma2.Properties.FieldName = "MaximumGamma";
            // 
            // ExpandSize2
            // 
            this.ExpandSize2.Name = "ExpandSize2";
            this.ExpandSize2.Properties.Caption = "Избыточность";
            this.ExpandSize2.Properties.FieldName = "ExpandSize";
            // 
            // ExtractBarcode2
            // 
            this.ExtractBarcode2.Name = "ExtractBarcode2";
            this.ExtractBarcode2.Properties.Caption = "Импорт параметров из баркода";
            this.ExtractBarcode2.Properties.FieldName = "ExtractBarcode";
            // 
            // labelControl1
            // 
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelControl1.Location = new System.Drawing.Point(615, 441);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 19);
            this.labelControl1.TabIndex = 49;
            this.labelControl1.Text = "Среднее";
            // 
            // xtraTabPageOptions
            // 
            this.xtraTabPageOptions.Controls.Add(this.propertyGridControlOptions);
            this.xtraTabPageOptions.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabPageOptions.Image")));
            this.xtraTabPageOptions.Name = "xtraTabPageOptions";
            this.xtraTabPageOptions.Size = new System.Drawing.Size(1225, 695);
            this.xtraTabPageOptions.Text = "Режим параметров";
            // 
            // propertyGridControlOptions
            // 
            this.propertyGridControlOptions.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.propertyGridControlOptions.DefaultEditors.AddRange(new DevExpress.XtraVerticalGrid.Rows.DefaultEditor[] {
            new DevExpress.XtraVerticalGrid.Rows.DefaultEditor(typeof(bool), this.repositoryItemCheckEditBoolean),
            new DevExpress.XtraVerticalGrid.Rows.DefaultEditor(typeof(int), this.repositoryItemSpinEditNumber),
            new DevExpress.XtraVerticalGrid.Rows.DefaultEditor(typeof(string), this.repositoryItemTextEditString)});
            this.propertyGridControlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControlOptions.Location = new System.Drawing.Point(0, 0);
            this.propertyGridControlOptions.Name = "propertyGridControlOptions";
            this.propertyGridControlOptions.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBoxMixer,
            this.repositoryItemComboBoxGamma,
            this.repositoryItemComboBoxArchiver,
            this.repositoryItemComboBoxPolitic,
            this.repositoryItemComboBoxPixelFormat,
            this.repositoryItemMemoEditPoliticText,
            this.repositoryItemCheckEditBoolean,
            this.repositoryItemSpinEditNumber,
            this.repositoryItemTextEditString,
            this.repositoryItemComboBoxEcc,
            this.repositoryItemComboBoxBarcode});
            this.propertyGridControlOptions.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.category,
            this.categoryPackUnpack});
            this.propertyGridControlOptions.Size = new System.Drawing.Size(1225, 695);
            this.propertyGridControlOptions.TabIndex = 0;
            this.propertyGridControlOptions.CellValueChanged += new DevExpress.XtraVerticalGrid.Events.CellValueChangedEventHandler(this.CellValueChanged);
            // 
            // repositoryItemCheckEditBoolean
            // 
            this.repositoryItemCheckEditBoolean.AutoHeight = false;
            this.repositoryItemCheckEditBoolean.Name = "repositoryItemCheckEditBoolean";
            // 
            // repositoryItemSpinEditNumber
            // 
            this.repositoryItemSpinEditNumber.AutoHeight = false;
            this.repositoryItemSpinEditNumber.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEditNumber.IsFloatValue = false;
            this.repositoryItemSpinEditNumber.Mask.EditMask = "N00";
            this.repositoryItemSpinEditNumber.MaxValue = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.repositoryItemSpinEditNumber.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.repositoryItemSpinEditNumber.Name = "repositoryItemSpinEditNumber";
            this.repositoryItemSpinEditNumber.ValueChanged += new System.EventHandler(this.SelectedItemChanged);
            // 
            // repositoryItemTextEditString
            // 
            this.repositoryItemTextEditString.AutoHeight = false;
            this.repositoryItemTextEditString.Name = "repositoryItemTextEditString";
            this.repositoryItemTextEditString.Modified += new System.EventHandler(this.SelectedItemChanged);
            // 
            // repositoryItemComboBoxMixer
            // 
            this.repositoryItemComboBoxMixer.AutoHeight = false;
            this.repositoryItemComboBoxMixer.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxMixer.Name = "repositoryItemComboBoxMixer";
            this.repositoryItemComboBoxMixer.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBoxMixer.SelectedIndexChanged += new System.EventHandler(this.SelectedItemChanged);
            // 
            // repositoryItemComboBoxGamma
            // 
            this.repositoryItemComboBoxGamma.AutoHeight = false;
            this.repositoryItemComboBoxGamma.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxGamma.Name = "repositoryItemComboBoxGamma";
            this.repositoryItemComboBoxGamma.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBoxGamma.SelectedIndexChanged += new System.EventHandler(this.SelectedItemChanged);
            // 
            // repositoryItemComboBoxArchiver
            // 
            this.repositoryItemComboBoxArchiver.AutoHeight = false;
            this.repositoryItemComboBoxArchiver.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxArchiver.Name = "repositoryItemComboBoxArchiver";
            this.repositoryItemComboBoxArchiver.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBoxArchiver.SelectedIndexChanged += new System.EventHandler(this.SelectedItemChanged);
            // 
            // repositoryItemComboBoxPolitic
            // 
            this.repositoryItemComboBoxPolitic.AutoHeight = false;
            this.repositoryItemComboBoxPolitic.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxPolitic.Name = "repositoryItemComboBoxPolitic";
            this.repositoryItemComboBoxPolitic.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBoxPolitic.SelectedIndexChanged += new System.EventHandler(this.SelectedItemChanged);
            // 
            // repositoryItemComboBoxPixelFormat
            // 
            this.repositoryItemComboBoxPixelFormat.AutoHeight = false;
            this.repositoryItemComboBoxPixelFormat.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxPixelFormat.Name = "repositoryItemComboBoxPixelFormat";
            this.repositoryItemComboBoxPixelFormat.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBoxPixelFormat.SelectedIndexChanged += new System.EventHandler(this.SelectedItemChanged);
            // 
            // repositoryItemMemoEditPoliticText
            // 
            this.repositoryItemMemoEditPoliticText.Name = "repositoryItemMemoEditPoliticText";
            this.repositoryItemMemoEditPoliticText.EditValueChanged += new System.EventHandler(this.SelectedItemChanged);
            // 
            // repositoryItemComboBoxEcc
            // 
            this.repositoryItemComboBoxEcc.AutoHeight = false;
            this.repositoryItemComboBoxEcc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxEcc.Name = "repositoryItemComboBoxEcc";
            // 
            // repositoryItemComboBoxBarcode
            // 
            this.repositoryItemComboBoxBarcode.AutoHeight = false;
            this.repositoryItemComboBoxBarcode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxBarcode.Name = "repositoryItemComboBoxBarcode";
            // 
            // category
            // 
            this.category.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.MixerComboBoxItem,
            this.ArchiverComboBoxItem,
            this.GammaComboBoxItem,
            this.EccComboBoxItem});
            this.category.Name = "category";
            this.category.Properties.Caption = "Алгоритмы";
            // 
            // MixerComboBoxItem
            // 
            this.MixerComboBoxItem.Name = "MixerComboBoxItem";
            this.MixerComboBoxItem.Properties.Caption = "Алгоритм перемешивания";
            this.MixerComboBoxItem.Properties.FieldName = "MixerComboBoxItem";
            this.MixerComboBoxItem.Properties.RowEdit = this.repositoryItemComboBoxMixer;
            // 
            // ArchiverComboBoxItem
            // 
            this.ArchiverComboBoxItem.Name = "ArchiverComboBoxItem";
            this.ArchiverComboBoxItem.Properties.Caption = "Алгоритм компрессии";
            this.ArchiverComboBoxItem.Properties.FieldName = "ArchiverComboBoxItem";
            this.ArchiverComboBoxItem.Properties.RowEdit = this.repositoryItemComboBoxArchiver;
            // 
            // GammaComboBoxItem
            // 
            this.GammaComboBoxItem.Expanded = false;
            this.GammaComboBoxItem.Name = "GammaComboBoxItem";
            this.GammaComboBoxItem.Properties.Caption = "Алгоритм гаммы";
            this.GammaComboBoxItem.Properties.FieldName = "GammaComboBoxItem";
            this.GammaComboBoxItem.Properties.RowEdit = this.repositoryItemComboBoxGamma;
            // 
            // EccComboBoxItem
            // 
            this.EccComboBoxItem.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.EccCodeSize,
            this.EccDataSize});
            this.EccComboBoxItem.IsChildRowsLoaded = true;
            this.EccComboBoxItem.Name = "EccComboBoxItem";
            this.EccComboBoxItem.Properties.Caption = "Алгоритм коррекции ошибок";
            this.EccComboBoxItem.Properties.FieldName = "EccComboBoxItem";
            this.EccComboBoxItem.Properties.RowEdit = this.repositoryItemComboBoxEcc;
            // 
            // EccCodeSize
            // 
            this.EccCodeSize.Name = "EccCodeSize";
            this.EccCodeSize.Properties.Caption = "Длина кода";
            this.EccCodeSize.Properties.FieldName = "EccCodeSize";
            this.EccCodeSize.Properties.RowEdit = this.repositoryItemSpinEditNumber;
            // 
            // EccDataSize
            // 
            this.EccDataSize.Name = "EccDataSize";
            this.EccDataSize.Properties.Caption = "Длина данных";
            this.EccDataSize.Properties.FieldName = "EccDataSize";
            this.EccDataSize.Properties.RowEdit = this.repositoryItemSpinEditNumber;
            // 
            // categoryPackUnpack
            // 
            this.categoryPackUnpack.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.SampleAutoresize,
            this.Key,
            this.FilterStep,
            this.Alpha,
            this.PixelFormatComboBoxItem,
            this.MaximumGamma,
            this.ExpandSize,
            this.BarcodeComboBoxItem,
            this.ExtractBarcode,
            this.PoliticComboBoxItem});
            this.categoryPackUnpack.Height = 23;
            this.categoryPackUnpack.Name = "categoryPackUnpack";
            this.categoryPackUnpack.Properties.Caption = "Значения";
            // 
            // SampleAutoresize
            // 
            this.SampleAutoresize.Name = "SampleAutoresize";
            this.SampleAutoresize.Properties.Caption = "Маштабировать изображение";
            this.SampleAutoresize.Properties.FieldName = "SampleAutoresize";
            this.SampleAutoresize.Properties.RowEdit = this.repositoryItemCheckEditBoolean;
            // 
            // Key
            // 
            this.Key.Name = "Key";
            this.Key.Properties.Caption = "Ключ";
            this.Key.Properties.FieldName = "Key";
            // 
            // FilterStep
            // 
            this.FilterStep.Name = "FilterStep";
            this.FilterStep.Properties.Caption = "Параметр фильтра";
            this.FilterStep.Properties.FieldName = "FilterStep";
            this.FilterStep.Properties.RowEdit = this.repositoryItemSpinEditNumber;
            // 
            // Alpha
            // 
            this.Alpha.Name = "Alpha";
            this.Alpha.Properties.Caption = "Глубина погружения";
            this.Alpha.Properties.FieldName = "Alpha";
            this.Alpha.Properties.RowEdit = this.repositoryItemSpinEditNumber;
            // 
            // PixelFormatComboBoxItem
            // 
            this.PixelFormatComboBoxItem.Name = "PixelFormatComboBoxItem";
            this.PixelFormatComboBoxItem.Properties.Caption = "Формат пикселей";
            this.PixelFormatComboBoxItem.Properties.FieldName = "PixelFormatComboBoxItem";
            this.PixelFormatComboBoxItem.Properties.RowEdit = this.repositoryItemComboBoxPixelFormat;
            // 
            // MaximumGamma
            // 
            this.MaximumGamma.Name = "MaximumGamma";
            this.MaximumGamma.Properties.Caption = "Использовать гамму максимальной длины";
            this.MaximumGamma.Properties.FieldName = "MaximumGamma";
            this.MaximumGamma.Properties.RowEdit = this.repositoryItemCheckEditBoolean;
            // 
            // ExpandSize
            // 
            this.ExpandSize.Name = "ExpandSize";
            this.ExpandSize.Properties.Caption = "Избыточность";
            this.ExpandSize.Properties.FieldName = "ExpandSize";
            // 
            // BarcodeComboBoxItem
            // 
            this.BarcodeComboBoxItem.Name = "BarcodeComboBoxItem";
            this.BarcodeComboBoxItem.Properties.Caption = "Встраиваемый баркод";
            this.BarcodeComboBoxItem.Properties.FieldName = "BarcodeComboBoxItem";
            this.BarcodeComboBoxItem.Properties.RowEdit = this.repositoryItemComboBoxBarcode;
            // 
            // ExtractBarcode
            // 
            this.ExtractBarcode.Name = "ExtractBarcode";
            this.ExtractBarcode.Properties.Caption = "Импорт параметров из баркода";
            this.ExtractBarcode.Properties.FieldName = "ExtractBarcode";
            this.ExtractBarcode.Properties.RowEdit = this.repositoryItemCheckEditBoolean;
            // 
            // PoliticComboBoxItem
            // 
            this.PoliticComboBoxItem.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.PoliticText});
            this.PoliticComboBoxItem.IsChildRowsLoaded = true;
            this.PoliticComboBoxItem.Name = "PoliticComboBoxItem";
            this.PoliticComboBoxItem.Properties.Caption = "Политика заполения лишних пикселей";
            this.PoliticComboBoxItem.Properties.FieldName = "PoliticComboBoxItem";
            this.PoliticComboBoxItem.Properties.RowEdit = this.repositoryItemComboBoxPolitic;
            // 
            // PoliticText
            // 
            this.PoliticText.IsChildRowsLoaded = true;
            this.PoliticText.Name = "PoliticText";
            this.PoliticText.Properties.Caption = "Альтернативное сообщение";
            this.PoliticText.Properties.FieldName = "PoliticText";
            this.PoliticText.Properties.RowEdit = this.repositoryItemMemoEditPoliticText;
            // 
            // PixelFormatIndex
            // 
            this.PixelFormatIndex.Name = "PixelFormatIndex";
            this.PixelFormatIndex.Properties.Caption = "Формат пикселей";
            this.PixelFormatIndex.Properties.FieldName = "PixelFormatComboBoxItem";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // labelControl2
            // 
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelControl2.Location = new System.Drawing.Point(3, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(82, 19);
            this.labelControl2.TabIndex = 50;
            this.labelControl2.Text = "Параметры";
            // 
            // labelControl3
            // 
            this.labelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelControl3.Location = new System.Drawing.Point(3, 3);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(82, 19);
            this.labelControl3.TabIndex = 53;
            this.labelControl3.Text = "Параметры";
            // 
            // BbsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "BbsControl";
            this.Size = new System.Drawing.Size(1235, 746);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packingSample.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.packingImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlPack)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unpackImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlUnpack)).EndInit();
            this.xtraTabPageOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControlOptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditBoolean)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditString)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxMixer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxGamma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxArchiver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxPolitic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxPixelFormat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEditPoliticText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxEcc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxBarcode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openSampleDialog;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.OpenFileDialog openOptionsDialog;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        private System.Windows.Forms.SaveFileDialog saveOptionsDialog;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageOptions;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow PixelFormatIndex;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow PoliticComboBoxItem;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow PoliticText;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow category;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryPackUnpack;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow SampleAutoresize;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow Key;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow EccCodeSize;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow FilterStep;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow Alpha;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow PixelFormatComboBoxItem;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.PictureEdit packingSample;
        private RichEditControl packFile;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.PictureEdit packingImage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow MixerComboBoxItem;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow ArchiverComboBoxItem;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow GammaComboBoxItem;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.PictureEdit pictureBox1;
        private DevExpress.XtraEditors.PictureEdit unpackImage;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private RichEditControl unpackFile;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControlOptions;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditBoolean;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditNumber;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEditString;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxMixer;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxGamma;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxArchiver;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxPolitic;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxPixelFormat;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEditPoliticText;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControlUnpack;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControlPack;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow category1;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow MixerComboBoxItem1;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow ArchiverComboBoxItem1;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow GammaComboBoxItem1;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryPack;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow SampleAutoresize1;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow Key1;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow EccCodeSize1;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow Alpha1;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow PixelFormatComboBoxItem1;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow PoliticComboBoxItem1;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow PoliticText1;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow category2;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow MixerComboBoxItem2;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow ArchiverComboBoxItem2;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow GammaComboBoxItem2;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryUnpack;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow Key2;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow EccCodeSize2;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow FilterStep2;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow MaximumGamma1;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow MaximumGamma2;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow MaximumGamma;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow EccComboBoxItem1;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow EccComboBoxItem2;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow EccComboBoxItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxEcc;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow EccDataSize;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow EccDataSize1;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow EccDataSize2;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow ExpandSize;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow ExpandSize1;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow ExpandSize2;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxBarcode;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow BarcodeComboBoxItem;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow BarcodeComboBoxItem1;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow ExtractBarcode;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow ExtractBarcode2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}
