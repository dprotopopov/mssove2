using BBSGridApp.DataModel;
using DevExpress.Core;
using DevExpress.UI.Xaml.Layout;

namespace BBSGridApp.ViewModel
{
    //A View Model for an ItemDetailPage
    public class ItemDetailViewModel : DevExpress.Mvvm.BindableBase, ISupportSaveLoadState
    {
        SampleDataItem selectedItem;
        SampleDataGroup group;
        public ItemDetailViewModel() { }

        public SampleDataItem SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty<SampleDataItem>(ref selectedItem, value); }
        }

        public SampleDataGroup Group
        {
            get { return group; }
            private set { SetProperty<SampleDataGroup>(ref group, value); }
        }

        void ISupportSaveLoadState.LoadState(object navigationParameter, PageStateStorage pageState)
        {
            SampleDataItem item = SampleDataSource.GetItem(pageState.GetParameter("SelectedItem", (string)navigationParameter));
            Group = item.Group;
            SelectedItem = item;
        }

        void ISupportSaveLoadState.SaveState(PageStateStorage pageState)
        {
            pageState.SaveParameter("SelectedItem", SelectedItem.UniqueId);
        }
    }
}
