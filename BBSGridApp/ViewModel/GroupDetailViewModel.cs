using BBSGridApp.DataModel;
using DevExpress.Core;
using DevExpress.UI.Xaml.Layout;

namespace BBSGridApp.ViewModel
{
    //A View Model for a GroupDetailPage
    public class GroupDetailViewModel : DevExpress.Mvvm.BindableBase, ISupportSaveLoadState
    {
        SampleDataGroup group;
        public GroupDetailViewModel() { }
        public SampleDataGroup Group
        {
            get { return group; }
            private set { SetProperty<SampleDataGroup>(ref group, value); }
        }

        void ISupportSaveLoadState.LoadState(object navigationParameter, PageStateStorage storage)
        {
            SampleDataGroup group = SampleDataSource.GetGroup((string)navigationParameter);
            Group = SampleDataSource.GetGroup((string)navigationParameter);
        }

        void ISupportSaveLoadState.SaveState(PageStateStorage storage)
        {
        }
    }
}
