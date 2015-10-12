using DevExpress.UI.Xaml.Layout;

namespace BBSGridApp.View
{
    /// <summary>
    ///     A page that displays details for a single item within a group while allowing gestures to
    ///     flip through other items belonging to the same group.
    ///     ItemDetailPage - This page, that displays item details, is opened when an end-user clicks any item within the
    ///     GroupedItemsPage. The type of the page to be opened on an item click is specified by the ItemNavigationTargetType
    ///     property set for the DXGridView control within the GroupedItemsPage.
    /// </summary>
    public sealed partial class ItemDetailPage : DXPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
        }
    }
}