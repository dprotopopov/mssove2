using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using DevExpress.UI.Xaml.Layout;

namespace BBSGridApp.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DXPage1 : DXPage
    {
        public DXPage1()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     https://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh700391.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            // Open a text file.
            var open =
                new FileOpenPicker();
            open.SuggestedStartLocation =
                PickerLocationId.DocumentsLibrary;
            open.FileTypeFilter.Add(".rtf");

            var file = await open.PickSingleFileAsync();

            if (file != null)
            {
                var randAccStream =
                    await file.OpenAsync(FileAccessMode.Read);

                // Load the file into the Document property of the RichEditBox.
                inputText.Document.LoadFromStream(TextSetOptions.FormatRtf, randAccStream);
            }
        }

        /// <summary>
        ///     https://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh700391.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (((ApplicationView.Value != ApplicationViewState.Snapped) ||
                 ApplicationView.TryUnsnap()))
            {
                var savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;

                // Dropdown of file types the user can save the file as
                savePicker.FileTypeChoices.Add("Rich Text", new List<string> {".rtf"});

                // Default file name if the user does not type one in or select a file to replace
                savePicker.SuggestedFileName = "New Document";

                var file = await savePicker.PickSaveFileAsync();
                if (file != null)
                {
                    // Prevent updates to the remote version of the file until we 
                    // finish making changes and call CompleteUpdatesAsync.
                    CachedFileManager.DeferUpdates(file);
                    // write to file
                    var randAccStream =
                        await file.OpenAsync(FileAccessMode.ReadWrite);

                    inputText.Document.SaveToStream(TextGetOptions.FormatRtf, randAccStream);

                    // Let Windows know that we're finished changing the file so the 
                    // other app can update the remote version of the file.
                    var status = await CachedFileManager.CompleteUpdatesAsync(file);
                    if (status != FileUpdateStatus.Complete)
                    {
                        var errorBox =
                            new MessageDialog("File " + file.Name + " couldn't be saved.");
                        await errorBox.ShowAsync();
                    }
                }
            }
        }
    }
}