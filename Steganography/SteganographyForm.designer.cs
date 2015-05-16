namespace Steganography
{
    partial class SteganographyForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.packingPixelFormat = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.packingGamma = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.packingMixer = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.packingPoliticsNone = new System.Windows.Forms.RadioButton();
            this.packingPoliticsText = new System.Windows.Forms.TextBox();
            this.packingPoliticsFake = new System.Windows.Forms.RadioButton();
            this.packingPoliticsRandom = new System.Windows.Forms.RadioButton();
            this.packingPoliticsZero = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.packingSampleAutoresize = new System.Windows.Forms.CheckBox();
            this.packingViewSequence = new System.Windows.Forms.Button();
            this.packingLoad = new System.Windows.Forms.Button();
            this.packingSave = new System.Windows.Forms.Button();
            this.packingExpand = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.packingSample = new System.Windows.Forms.PictureBox();
            this.packingImage = new System.Windows.Forms.PictureBox();
            this.packingAlpha = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.encrypt = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.packingSource = new System.Windows.Forms.TextBox();
            this.packingKey = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.unpackingGamma = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.unpackingMixer = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.unpackingViewSequence = new System.Windows.Forms.Button();
            this.unpackingLoad = new System.Windows.Forms.Button();
            this.unpackingSave = new System.Windows.Forms.Button();
            this.unpackingExpand = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.unpackingFilter = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.unpackingImage = new System.Windows.Forms.PictureBox();
            this.decrypt = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.unpackingDest = new System.Windows.Forms.TextBox();
            this.unpackingKey = new System.Windows.Forms.TextBox();
            this.openSampleDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileOptions = new System.Windows.Forms.SaveFileDialog();
            this.openFileOptions = new System.Windows.Forms.OpenFileDialog();
            this.packingCompress = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.unpackingDecompress = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packingExpand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.packingSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.packingImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.packingAlpha)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unpackingExpand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unpackingFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unpackingImage)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1261, 738);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.packingCompress);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.packingPixelFormat);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.packingGamma);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.packingMixer);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.packingPoliticsNone);
            this.tabPage1.Controls.Add(this.packingPoliticsText);
            this.tabPage1.Controls.Add(this.packingPoliticsFake);
            this.tabPage1.Controls.Add(this.packingPoliticsRandom);
            this.tabPage1.Controls.Add(this.packingPoliticsZero);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.packingSampleAutoresize);
            this.tabPage1.Controls.Add(this.packingViewSequence);
            this.tabPage1.Controls.Add(this.packingLoad);
            this.tabPage1.Controls.Add(this.packingSave);
            this.tabPage1.Controls.Add(this.packingExpand);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.packingSample);
            this.tabPage1.Controls.Add(this.packingImage);
            this.tabPage1.Controls.Add(this.packingAlpha);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.encrypt);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.packingSource);
            this.tabPage1.Controls.Add(this.packingKey);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1253, 705);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Режим упаковки";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // packingPixelFormat
            // 
            this.packingPixelFormat.FormattingEnabled = true;
            this.packingPixelFormat.Location = new System.Drawing.Point(647, 231);
            this.packingPixelFormat.Name = "packingPixelFormat";
            this.packingPixelFormat.Size = new System.Drawing.Size(121, 28);
            this.packingPixelFormat.TabIndex = 38;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(416, 237);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(146, 20);
            this.label17.TabIndex = 37;
            this.label17.Text = "Формат пикселей";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(808, 304);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(111, 20);
            this.label16.TabIndex = 36;
            this.label16.Text = "Изображение";
            // 
            // packingGamma
            // 
            this.packingGamma.FormattingEnabled = true;
            this.packingGamma.Location = new System.Drawing.Point(254, 296);
            this.packingGamma.Name = "packingGamma";
            this.packingGamma.Size = new System.Drawing.Size(121, 28);
            this.packingGamma.TabIndex = 35;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(23, 302);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(137, 20);
            this.label14.TabIndex = 34;
            this.label14.Text = "Алгоритм гаммы";
            // 
            // packingMixer
            // 
            this.packingMixer.FormattingEnabled = true;
            this.packingMixer.Location = new System.Drawing.Point(254, 261);
            this.packingMixer.Name = "packingMixer";
            this.packingMixer.Size = new System.Drawing.Size(121, 28);
            this.packingMixer.TabIndex = 33;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 267);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(211, 20);
            this.label12.TabIndex = 32;
            this.label12.Text = "Алгоритм перемешивания";
            // 
            // packingPoliticsNone
            // 
            this.packingPoliticsNone.AutoSize = true;
            this.packingPoliticsNone.Checked = true;
            this.packingPoliticsNone.Location = new System.Drawing.Point(416, 305);
            this.packingPoliticsNone.Name = "packingPoliticsNone";
            this.packingPoliticsNone.Size = new System.Drawing.Size(166, 24);
            this.packingPoliticsNone.TabIndex = 31;
            this.packingPoliticsNone.TabStop = true;
            this.packingPoliticsNone.Text = "не обрабатывать";
            this.packingPoliticsNone.UseVisualStyleBackColor = true;
            // 
            // packingPoliticsText
            // 
            this.packingPoliticsText.Location = new System.Drawing.Point(416, 427);
            this.packingPoliticsText.Multiline = true;
            this.packingPoliticsText.Name = "packingPoliticsText";
            this.packingPoliticsText.ReadOnly = true;
            this.packingPoliticsText.Size = new System.Drawing.Size(352, 142);
            this.packingPoliticsText.TabIndex = 30;
            this.packingPoliticsText.Text = "HELLO WORLD";
            // 
            // packingPoliticsFake
            // 
            this.packingPoliticsFake.AutoSize = true;
            this.packingPoliticsFake.Location = new System.Drawing.Point(416, 395);
            this.packingPoliticsFake.Name = "packingPoliticsFake";
            this.packingPoliticsFake.Size = new System.Drawing.Size(249, 24);
            this.packingPoliticsFake.TabIndex = 29;
            this.packingPoliticsFake.Text = "альтернативное сообщение";
            this.packingPoliticsFake.UseVisualStyleBackColor = true;
            this.packingPoliticsFake.CheckedChanged += new System.EventHandler(this.packingPoliticsFake_CheckedChanged);
            // 
            // packingPoliticsRandom
            // 
            this.packingPoliticsRandom.AutoSize = true;
            this.packingPoliticsRandom.Location = new System.Drawing.Point(416, 365);
            this.packingPoliticsRandom.Name = "packingPoliticsRandom";
            this.packingPoliticsRandom.Size = new System.Drawing.Size(177, 24);
            this.packingPoliticsRandom.TabIndex = 28;
            this.packingPoliticsRandom.Text = "случайные данные";
            this.packingPoliticsRandom.UseVisualStyleBackColor = true;
            // 
            // packingPoliticsZero
            // 
            this.packingPoliticsZero.AutoSize = true;
            this.packingPoliticsZero.Location = new System.Drawing.Point(416, 335);
            this.packingPoliticsZero.Name = "packingPoliticsZero";
            this.packingPoliticsZero.Size = new System.Drawing.Size(69, 24);
            this.packingPoliticsZero.TabIndex = 27;
            this.packingPoliticsZero.Text = "нули";
            this.packingPoliticsZero.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(416, 281);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(318, 20);
            this.label11.TabIndex = 26;
            this.label11.Text = "Политика запопления лишних  пикселей";
            // 
            // packingSampleAutoresize
            // 
            this.packingSampleAutoresize.AutoSize = true;
            this.packingSampleAutoresize.Location = new System.Drawing.Point(420, 201);
            this.packingSampleAutoresize.Name = "packingSampleAutoresize";
            this.packingSampleAutoresize.Size = new System.Drawing.Size(264, 24);
            this.packingSampleAutoresize.TabIndex = 25;
            this.packingSampleAutoresize.Text = "Маштабировать изображение";
            this.packingSampleAutoresize.UseVisualStyleBackColor = true;
            // 
            // packingViewSequence
            // 
            this.packingViewSequence.Location = new System.Drawing.Point(23, 458);
            this.packingViewSequence.Name = "packingViewSequence";
            this.packingViewSequence.Size = new System.Drawing.Size(352, 33);
            this.packingViewSequence.TabIndex = 24;
            this.packingViewSequence.Text = "Посмотреть последовательность ...";
            this.packingViewSequence.UseVisualStyleBackColor = true;
            this.packingViewSequence.Click += new System.EventHandler(this.packingViewSequence_Click);
            // 
            // packingLoad
            // 
            this.packingLoad.Location = new System.Drawing.Point(23, 536);
            this.packingLoad.Name = "packingLoad";
            this.packingLoad.Size = new System.Drawing.Size(352, 33);
            this.packingLoad.TabIndex = 23;
            this.packingLoad.Text = "Загрузить параметры ...";
            this.packingLoad.UseVisualStyleBackColor = true;
            this.packingLoad.Click += new System.EventHandler(this.packingLoad_Click);
            // 
            // packingSave
            // 
            this.packingSave.Location = new System.Drawing.Point(23, 497);
            this.packingSave.Name = "packingSave";
            this.packingSave.Size = new System.Drawing.Size(352, 33);
            this.packingSave.TabIndex = 22;
            this.packingSave.Text = "Сохранить параметры ...";
            this.packingSave.UseVisualStyleBackColor = true;
            this.packingSave.Click += new System.EventHandler(this.packingSave_Click);
            // 
            // packingExpand
            // 
            this.packingExpand.Location = new System.Drawing.Point(255, 136);
            this.packingExpand.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.packingExpand.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.packingExpand.Name = "packingExpand";
            this.packingExpand.Size = new System.Drawing.Size(120, 26);
            this.packingExpand.TabIndex = 20;
            this.packingExpand.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 138);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(183, 20);
            this.label10.TabIndex = 19;
            this.label10.Text = "Избыточность (1-1000)";
            // 
            // packingSample
            // 
            this.packingSample.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.packingSample.Location = new System.Drawing.Point(416, 52);
            this.packingSample.Name = "packingSample";
            this.packingSample.Size = new System.Drawing.Size(352, 134);
            this.packingSample.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.packingSample.TabIndex = 16;
            this.packingSample.TabStop = false;
            this.packingSample.Click += new System.EventHandler(this.packingSample_Click);
            // 
            // packingImage
            // 
            this.packingImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.packingImage.Location = new System.Drawing.Point(808, 327);
            this.packingImage.Name = "packingImage";
            this.packingImage.Size = new System.Drawing.Size(397, 242);
            this.packingImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.packingImage.TabIndex = 15;
            this.packingImage.TabStop = false;
            this.packingImage.Click += new System.EventHandler(this.packingImage_Click);
            // 
            // packingAlpha
            // 
            this.packingAlpha.Location = new System.Drawing.Point(255, 168);
            this.packingAlpha.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.packingAlpha.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.packingAlpha.Name = "packingAlpha";
            this.packingAlpha.Size = new System.Drawing.Size(120, 26);
            this.packingAlpha.TabIndex = 8;
            this.packingAlpha.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(163, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "Глубина погружения";
            // 
            // encrypt
            // 
            this.encrypt.Location = new System.Drawing.Point(518, 602);
            this.encrypt.Name = "encrypt";
            this.encrypt.Size = new System.Drawing.Size(178, 54);
            this.encrypt.TabIndex = 6;
            this.encrypt.Text = "упаковываем";
            this.encrypt.UseVisualStyleBackColor = true;
            this.encrypt.Click += new System.EventHandler(this.packing_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(412, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Изображение";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(808, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Исходный текст";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ключ";
            // 
            // packingSource
            // 
            this.packingSource.Location = new System.Drawing.Point(808, 52);
            this.packingSource.Multiline = true;
            this.packingSource.Name = "packingSource";
            this.packingSource.Size = new System.Drawing.Size(397, 228);
            this.packingSource.TabIndex = 1;
            // 
            // packingKey
            // 
            this.packingKey.Location = new System.Drawing.Point(23, 52);
            this.packingKey.Multiline = true;
            this.packingKey.Name = "packingKey";
            this.packingKey.Size = new System.Drawing.Size(352, 32);
            this.packingKey.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.unpackingDecompress);
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Controls.Add(this.unpackingGamma);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.unpackingMixer);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.unpackingViewSequence);
            this.tabPage2.Controls.Add(this.unpackingLoad);
            this.tabPage2.Controls.Add(this.unpackingSave);
            this.tabPage2.Controls.Add(this.unpackingExpand);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.unpackingFilter);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.unpackingImage);
            this.tabPage2.Controls.Add(this.decrypt);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.unpackingDest);
            this.tabPage2.Controls.Add(this.unpackingKey);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1253, 705);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Режим распаковки";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(452, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(316, 238);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 38;
            this.pictureBox1.TabStop = false;
            // 
            // unpackingGamma
            // 
            this.unpackingGamma.FormattingEnabled = true;
            this.unpackingGamma.Location = new System.Drawing.Point(258, 296);
            this.unpackingGamma.Name = "unpackingGamma";
            this.unpackingGamma.Size = new System.Drawing.Size(121, 28);
            this.unpackingGamma.TabIndex = 37;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(25, 299);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(137, 20);
            this.label15.TabIndex = 36;
            this.label15.Text = "Алгоритм гаммы";
            // 
            // unpackingMixer
            // 
            this.unpackingMixer.FormattingEnabled = true;
            this.unpackingMixer.Location = new System.Drawing.Point(258, 261);
            this.unpackingMixer.Name = "unpackingMixer";
            this.unpackingMixer.Size = new System.Drawing.Size(121, 28);
            this.unpackingMixer.TabIndex = 35;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 264);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(211, 20);
            this.label13.TabIndex = 34;
            this.label13.Text = "Алгоритм перемешивания";
            // 
            // unpackingViewSequence
            // 
            this.unpackingViewSequence.Location = new System.Drawing.Point(25, 411);
            this.unpackingViewSequence.Name = "unpackingViewSequence";
            this.unpackingViewSequence.Size = new System.Drawing.Size(354, 33);
            this.unpackingViewSequence.TabIndex = 26;
            this.unpackingViewSequence.Text = "Посмотреть последовательность ...";
            this.unpackingViewSequence.UseVisualStyleBackColor = true;
            this.unpackingViewSequence.Click += new System.EventHandler(this.unpackingViewSequence_Click);
            // 
            // unpackingLoad
            // 
            this.unpackingLoad.Location = new System.Drawing.Point(25, 489);
            this.unpackingLoad.Name = "unpackingLoad";
            this.unpackingLoad.Size = new System.Drawing.Size(354, 33);
            this.unpackingLoad.TabIndex = 25;
            this.unpackingLoad.Text = "Загрузить параметры ...";
            this.unpackingLoad.UseVisualStyleBackColor = true;
            this.unpackingLoad.Click += new System.EventHandler(this.unpackingLoad_Click);
            // 
            // unpackingSave
            // 
            this.unpackingSave.Location = new System.Drawing.Point(25, 450);
            this.unpackingSave.Name = "unpackingSave";
            this.unpackingSave.Size = new System.Drawing.Size(354, 33);
            this.unpackingSave.TabIndex = 24;
            this.unpackingSave.Text = "Сохранить параметры ...";
            this.unpackingSave.UseVisualStyleBackColor = true;
            this.unpackingSave.Click += new System.EventHandler(this.unpackingSave_Click);
            // 
            // unpackingExpand
            // 
            this.unpackingExpand.Location = new System.Drawing.Point(259, 163);
            this.unpackingExpand.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.unpackingExpand.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.unpackingExpand.Name = "unpackingExpand";
            this.unpackingExpand.Size = new System.Drawing.Size(120, 26);
            this.unpackingExpand.TabIndex = 18;
            this.unpackingExpand.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(183, 20);
            this.label9.TabIndex = 17;
            this.label9.Text = "Избыточность (1-1000)";
            // 
            // unpackingFilter
            // 
            this.unpackingFilter.Location = new System.Drawing.Point(259, 129);
            this.unpackingFilter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.unpackingFilter.Name = "unpackingFilter";
            this.unpackingFilter.Size = new System.Drawing.Size(120, 26);
            this.unpackingFilter.TabIndex = 16;
            this.unpackingFilter.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(215, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "Параметр фильтра (1-100)";
            // 
            // unpackingImage
            // 
            this.unpackingImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.unpackingImage.Location = new System.Drawing.Point(806, 52);
            this.unpackingImage.Name = "unpackingImage";
            this.unpackingImage.Size = new System.Drawing.Size(399, 237);
            this.unpackingImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.unpackingImage.TabIndex = 14;
            this.unpackingImage.TabStop = false;
            this.unpackingImage.Click += new System.EventHandler(this.unpackingImage_Click);
            // 
            // decrypt
            // 
            this.decrypt.Location = new System.Drawing.Point(522, 598);
            this.decrypt.Name = "decrypt";
            this.decrypt.Size = new System.Drawing.Size(178, 54);
            this.decrypt.TabIndex = 13;
            this.decrypt.Text = "распаковываем";
            this.decrypt.UseVisualStyleBackColor = true;
            this.decrypt.Click += new System.EventHandler(this.unpacking_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(806, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Изображение";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(806, 301);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Исходный текст";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Ключ";
            // 
            // unpackingDest
            // 
            this.unpackingDest.Location = new System.Drawing.Point(810, 330);
            this.unpackingDest.Multiline = true;
            this.unpackingDest.Name = "unpackingDest";
            this.unpackingDest.ReadOnly = true;
            this.unpackingDest.Size = new System.Drawing.Size(395, 200);
            this.unpackingDest.TabIndex = 9;
            // 
            // unpackingKey
            // 
            this.unpackingKey.Location = new System.Drawing.Point(25, 51);
            this.unpackingKey.Multiline = true;
            this.unpackingKey.Name = "unpackingKey";
            this.unpackingKey.Size = new System.Drawing.Size(349, 32);
            this.unpackingKey.TabIndex = 7;
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
            // openFileOptions
            // 
            this.openFileOptions.FileName = "openFileDialog3";
            // 
            // packingCompress
            // 
            this.packingCompress.FormattingEnabled = true;
            this.packingCompress.Location = new System.Drawing.Point(254, 332);
            this.packingCompress.Name = "packingCompress";
            this.packingCompress.Size = new System.Drawing.Size(121, 28);
            this.packingCompress.TabIndex = 40;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(23, 338);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(177, 20);
            this.label18.TabIndex = 39;
            this.label18.Text = "Алгоритм компрессии";
            // 
            // unpackingDecompress
            // 
            this.unpackingDecompress.FormattingEnabled = true;
            this.unpackingDecompress.Location = new System.Drawing.Point(258, 330);
            this.unpackingDecompress.Name = "unpackingDecompress";
            this.unpackingDecompress.Size = new System.Drawing.Size(121, 28);
            this.unpackingDecompress.TabIndex = 40;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(25, 333);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(197, 20);
            this.label19.TabIndex = 39;
            this.label19.Text = "Алгоритм декомпрессии";
            // 
            // SteganographyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 738);
            this.Controls.Add(this.tabControl1);
            this.Name = "SteganographyForm";
            this.Text = "Метод ШПС";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packingExpand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.packingSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.packingImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.packingAlpha)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unpackingExpand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unpackingFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unpackingImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button encrypt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox packingSource;
        private System.Windows.Forms.TextBox packingKey;
        private System.Windows.Forms.Button decrypt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox unpackingDest;
        private System.Windows.Forms.TextBox unpackingKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox unpackingImage;
        private System.Windows.Forms.PictureBox packingImage;
        private System.Windows.Forms.NumericUpDown packingAlpha;
        private System.Windows.Forms.NumericUpDown unpackingFilter;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox packingSample;
        private System.Windows.Forms.NumericUpDown packingExpand;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown unpackingExpand;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.OpenFileDialog openSampleDialog;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.Button packingViewSequence;
        private System.Windows.Forms.Button packingLoad;
        private System.Windows.Forms.Button packingSave;
        private System.Windows.Forms.Button unpackingLoad;
        private System.Windows.Forms.Button unpackingSave;
        private System.Windows.Forms.CheckBox packingSampleAutoresize;
        private System.Windows.Forms.Button unpackingViewSequence;
        private System.Windows.Forms.TextBox packingPoliticsText;
        private System.Windows.Forms.RadioButton packingPoliticsFake;
        private System.Windows.Forms.RadioButton packingPoliticsRandom;
        private System.Windows.Forms.RadioButton packingPoliticsZero;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.SaveFileDialog saveFileOptions;
        private System.Windows.Forms.OpenFileDialog openFileOptions;
        private System.Windows.Forms.RadioButton packingPoliticsNone;
        private System.Windows.Forms.ComboBox packingMixer;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox unpackingMixer;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox packingGamma;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox unpackingGamma;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox packingPixelFormat;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox packingCompress;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox unpackingDecompress;
        private System.Windows.Forms.Label label19;
    }
}

