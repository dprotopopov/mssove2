using DevExpress.UI.Xaml.Layout;

namespace BBSGridApp.View
{
    /// <summary>
    ///     A page that displays an overview of a single group, including a preview of the items
    ///     within the group.
    ///     GroupDetailPage - This page, that displays group details, is opened when an end-user clicks a group header within
    ///     the GroupedItemsPage. A user can view group details on the left and items on the right.
    /// </summary>
    public sealed partial class GroupDetailPage : DXPage
    {
        public GroupDetailPage()
        {
            InitializeComponent();
        }
    }
}