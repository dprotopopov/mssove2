using System.Collections.Generic;
using BBSGridApp.DataModel;
using DevExpress.Core;
using BindableBase = DevExpress.Mvvm.BindableBase;

namespace BBSGridApp.ViewModel
{
    //A View Model for a GroupedItemsPage
    public sealed class GroupedItemsViewModel : BindableBase, ISupportSaveLoadState
    {
        private IEnumerable<SampleDataGroup> groups;

        public IEnumerable<SampleDataGroup> Groups
        {
            get { return groups; }
            private set { SetProperty(ref groups, value); }
        }

        void ISupportSaveLoadState.LoadState(object navigationParameter, PageStateStorage pageState)
        {
            Groups = SampleDataSource.GetGroups((string) navigationParameter);
        }

        void ISupportSaveLoadState.SaveState(PageStateStorage pageState)
        {
        }
    }
}