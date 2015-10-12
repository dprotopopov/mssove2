using BBSGridApp.DataModel;
using DevExpress.Core;
using BindableBase = DevExpress.Mvvm.BindableBase;

namespace BBSGridApp.ViewModel
{
    //A View Model for an ItemDetailPage
    public sealed class ItemDetailViewModel : BindableBase, ISupportSaveLoadState
    {
        private SampleDataGroup _flatGroup = SampleDataSource.GetFlatGroup();
        private SampleDataItem selectedItem;

        public SampleDataItem SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }

        public SampleDataGroup FlatGroup
        {
            get { return _flatGroup; }
            private set { SetProperty(ref _flatGroup, value); }
        }

        void ISupportSaveLoadState.LoadState(object navigationParameter, PageStateStorage pageState)
        {
            var item = SampleDataSource.GetItem(pageState.GetParameter("SelectedItem", (string) navigationParameter));
            SelectedItem = item;
        }

        void ISupportSaveLoadState.SaveState(PageStateStorage pageState)
        {
            pageState.SaveParameter("SelectedItem", SelectedItem.UniqueId);
        }
    }
}