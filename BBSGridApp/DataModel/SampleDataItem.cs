namespace BBSGridApp.DataModel
{
    /// <summary>
    ///     Generic item data model.
    /// </summary>
    public class SampleDataItem : SampleDataCommon
    {
        private string _content = string.Empty;

        private SampleDataGroup _group;

        public SampleDataItem(string uniqueId, string title, string subtitle, string imagePath, string description,
            string navigationTarget,
            string content, SampleDataGroup group)
            : base(uniqueId, title, subtitle, imagePath, description, navigationTarget)
        {
            _content = content;
            _group = group;
        }

        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        public SampleDataGroup Group
        {
            get { return _group; }
            set { SetProperty(ref _group, value); }
        }
    }
}