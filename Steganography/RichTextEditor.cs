using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace Steganography
{
    public partial class RichTextEditor : RibbonForm
    {
        public RichTextEditor()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        public string RtfText
        {
            get { return richEditControl1.RtfText; }
            set { richEditControl1.RtfText = value; }
        }

        private void Close_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void Apply_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}