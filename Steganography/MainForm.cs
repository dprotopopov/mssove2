using System;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraNavBar;

namespace Steganography
{
    /// <summary>
    ///     List of 3rd Party .NET UI & Reporting Components
    ///     http://www.codeproject.com/Reference/788434/List-of-rd-Party-NET-UI-Reporting-Components
    /// </summary>
    public partial class MainForm : RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
            InitSkinGallery();
            mainControl.TabsVisible = false;
            ribbonPageCategoryPack.Visible = true;
            ribbonPageCategoryUnpack.Visible = false;
            ribbonPageCategoryOptions.Visible = false;
            ribbonControl.SelectedPage = ribbonPagePack;
            mainControl.SelectedMode = BbsControl.Mode.Pack;
        }

        private void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(skinRibbonGalleryBarItem1, true);
        }


        private void About_ItemClick(object sender, ItemClickEventArgs e)
        {
            var about = new AboutBox();
            about.ShowDialog();
        }

        private void Pack_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.SelectedMode = BbsControl.Mode.Pack;
            ribbonControl.SelectedPage = ribbonPagePack;
        }

        private void Unpack_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.SelectedMode = BbsControl.Mode.Unpack;
            ribbonControl.SelectedPage = ribbonPageUnpack;
        }

        private void Options_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.SelectedMode = BbsControl.Mode.Options;
            ribbonControl.SelectedPage = ribbonPageOptions;
        }

        private void Execute_ItemClick(object sender, ItemClickEventArgs e)
        {
            switch (mainControl.SelectedMode)
            {
                case BbsControl.Mode.Pack:
                    bool b = mainControl.PackFileShow() &&
                             mainControl.PackOpenImage() &&
                             mainControl.Execute() &&
                             mainControl.ShowCipherImage();
                    break;
                case BbsControl.Mode.Unpack:
                    bool b1 = mainControl.UnpackOpenImage() &&
                              mainControl.Execute() &&
                              mainControl.UnpackFileShow();
                    break;
            }
        }

        private void Pack_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            mainControl.SelectedMode = BbsControl.Mode.Pack;
            ribbonControl.SelectedPage = ribbonPagePack;
        }

        private void Unpack_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            mainControl.SelectedMode = BbsControl.Mode.Unpack;
            ribbonControl.SelectedPage = ribbonPageUnpack;
        }

        private void Options_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            mainControl.SelectedMode = BbsControl.Mode.Options;
            ribbonControl.SelectedPage = ribbonPageOptions;
        }

        private void Exit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void PackingOpenImage_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.PackOpenImage();
        }

        private void PackingSaveImage_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.PackSaveImage();
        }

        private void UnpackingOpenImage_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.UnpackOpenImage();
        }

        private void SelectedModeChanged(object sender, EventArgs e)
        {
            ribbonPageCategoryPack.Visible = mainControl.SelectedMode == BbsControl.Mode.Pack;
            ribbonPageCategoryUnpack.Visible = mainControl.SelectedMode == BbsControl.Mode.Unpack;
            ribbonPageCategoryOptions.Visible = mainControl.SelectedMode == BbsControl.Mode.Options;
            switch (mainControl.SelectedMode)
            {
                case BbsControl.Mode.Pack:
                    ribbonControl.SelectedPage = ribbonPagePack;
                    break;
                case BbsControl.Mode.Unpack:
                    ribbonControl.SelectedPage = ribbonPageUnpack;
                    break;
                case BbsControl.Mode.Options:
                    ribbonControl.SelectedPage = ribbonPageOptions;
                    break;
            }
        }

        private void OptionsLoad_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.OptionsLoad();
        }

        private void OptionsSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.OptionsSave();
        }

        private void ViewSequence_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.ViewSequence();
        }

        private void PackingOpenFile_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.PackOpenFile();
        }

        private void UnpackingSaveFile_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.UnpackSaveFile();
        }

        private void Internet_ItemClick(object sender, ItemClickEventArgs e)
        {
            // http://stackoverflow.com/questions/4580263/how-to-open-in-default-browser-in-c-sharp
            Process.Start("http://google.com");
        }

        private void ShowCipherImage_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.ShowCipherImage();
        }

        private void UnpackFileShow_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.UnpackFileShow();
        }

        private void ShowSampleImage_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.ShowSampleImage();
        }

        private void PackFileShow_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.PackFileShow();
        }

        private void ShowInputImage_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.ShowInputImage();
        }

        private void ShowMedianImage_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.ShowMedianImage();
        }

        private void Barcode_ItemClick(object sender, ItemClickEventArgs e)
        {
            switch (mainControl.SelectedMode)
            {
                case BbsControl.Mode.Pack:
                    mainControl.ShowBarcodeImage();
                    break;
                case BbsControl.Mode.Unpack:
                    mainControl.ShowBarcodeText();
                    break;
            }
        }

        private void Erase_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.Erase();
        }

        private void PackingSendImage_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.PackSendImage();
        }

        private void UnpackingSendFile_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.UnpackSendFile();
        }

        private void Check_ItemClick(object sender, ItemClickEventArgs e)
        {
            mainControl.Check();
        }
    }
}