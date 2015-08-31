using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace Steganography
{
    /// <summary>
    ///     Редактор текста расширенного формата
    /// </summary>
    public partial class RichTextEditor : RibbonForm
    {
        public RichTextEditor()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        ///     Содержимое тексового редактора
        /// </summary>
        public string RtfText
        {
            get { return richEditControl1.RtfText; }
            set { richEditControl1.RtfText = value; }
        }

        /// <summary>
        ///     Обработчик события
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     Обработчик события
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Apply_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}