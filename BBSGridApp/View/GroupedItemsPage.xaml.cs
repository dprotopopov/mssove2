using DevExpress.UI.Xaml.Layout;

namespace BBSGridApp.View
{
    /// <summary>
    ///     A page that displays a grouped collection of items.
    ///     GroupedItemsPage - This is the startup page that displays groups and their items. In all application states, except
    ///     the Snapped state, data is presented using a horizontally scrollable DXGridView control. In the Snapped application
    ///     state, the data is presented using the ListView control. Check the attached
    ///     LayoutAwareDecorator.ViewStateVisibility property set for the ListView control. It ensures the ListView's
    ///     visibility in the Snapped state.
    /// </summary>
    public sealed partial class GroupedItemsPage : DXPage
    {
        public GroupedItemsPage()
        {
            InitializeComponent();
        }
    }
}