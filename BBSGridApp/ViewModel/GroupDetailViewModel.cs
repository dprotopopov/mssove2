using BBSGridApp.DataModel;
using DevExpress.Core;
using BindableBase = DevExpress.Mvvm.BindableBase;

namespace BBSGridApp.ViewModel
{
    //A View Model for a GroupDetailPage
    public sealed class GroupDetailViewModel : BindableBase, ISupportSaveLoadState
    {
        private SampleDataGroup group;

        public SampleDataGroup Group
        {
            get { return group; }
            private set { SetProperty(ref group, value); }
        }

        void ISupportSaveLoadState.LoadState(object navigationParameter, PageStateStorage storage)
        {
            var group = SampleDataSource.GetGroup((string) navigationParameter);
            Group = SampleDataSource.GetGroup((string) navigationParameter);
        }

        void ISupportSaveLoadState.SaveState(PageStateStorage storage)
        {
        }
    }
}