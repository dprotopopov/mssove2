using System.IO;
using System.Reflection;
using DevExpress.XtraEditors;

namespace BBSApp
{
    /// <summary>
    ///     Форма с информацией о программе
    /// </summary>
    partial class AboutBox : XtraForm
    {
        public AboutBox()
        {
            InitializeComponent();
            Text = string.Format("О программе {0}", AssemblyTitle);
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = string.Format("Версия {0}", AssemblyVersion);
            labelCopyright.Text = AssemblyCopyright;
            labelCompanyName.Text = AssemblyCompany;
            textBoxDescription.Text = AssemblyDescription;
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        #region Методы доступа к атрибутам сборки

        private string AssemblyTitle
        {
            get
            {
                var attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute) attributes[0];
                    return titleAttribute.Title != ""
                        ? titleAttribute.Title
                        : Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        private string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        private string AssemblyDescription
        {
            get
            {
                var attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyDescriptionAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute) attributes[0]).Description;
            }
        }

        private string AssemblyProduct
        {
            get
            {
                var attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyProductAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyProductAttribute) attributes[0]).Product;
            }
        }

        private string AssemblyCopyright
        {
            get
            {
                var attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute) attributes[0]).Copyright;
            }
        }

        private string AssemblyCompany
        {
            get
            {
                var attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCompanyAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute) attributes[0]).Company;
            }
        }

        #endregion
    }
}