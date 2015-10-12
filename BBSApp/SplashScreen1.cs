using System;
using DevExpress.XtraSplashScreen;

namespace BBSApp
{
    public partial class SplashScreen1 : SplashScreen
    {
// ReSharper disable UnusedMember.Global
        public enum SplashScreenCommand
// ReSharper restore UnusedMember.Global
        {
        }

        public SplashScreen1()
        {
            InitializeComponent();
        }

        #region Overrides

// ReSharper disable RedundantOverridenMember
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

// ReSharper restore RedundantOverridenMember

        #endregion
    }
}