using System;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using DevExpress.Mvvm;

namespace BBSGridApp.DataModel
{
    /// <summary>
    ///     Base class for <see cref="SampleDataItem" /> and <see cref="SampleDataGroup" /> that
    ///     defines properties common to both.
    /// </summary>
    [WebHostHidden]
    public abstract class SampleDataCommon : BindableBase
    {
        private static readonly Uri _baseUri = new Uri("ms-appx:///");
        private string _description = string.Empty;
        private ImageSource _image;
        private string _imagePath;
        private string _navigationTarget = string.Empty;
        private string _subtitle = string.Empty;
        private string _title = string.Empty;

        private string _uniqueId = string.Empty;

        public SampleDataCommon(string uniqueId, string title, string subtitle, string imagePath, string description,
            string navigationTarget)
        {
            _uniqueId = uniqueId;
            _title = title;
            _subtitle = subtitle;
            _description = description;
            _navigationTarget = navigationTarget;
            _imagePath = imagePath;
        }

        public string UniqueId
        {
            get { return _uniqueId; }
            set { SetProperty(ref _uniqueId, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string Subtitle
        {
            get { return _subtitle; }
            set { SetProperty(ref _subtitle, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public string NavigationTarget
        {
            get { return _navigationTarget; }
            set { SetProperty(ref _navigationTarget, value); }
        }

        public ImageSource Image
        {
            get
            {
                if (_image == null && _imagePath != null)
                {
                    _image = new BitmapImage(new Uri(_baseUri, _imagePath));
                }
                return _image;
            }

            set
            {
                _imagePath = null;
                SetProperty(ref _image, value);
            }
        }

        public void SetImage(string path)
        {
            _image = null;
            _imagePath = path;
            RaisePropertiesChanged("Image");
        }

        public override string ToString() => Title;
    }
}