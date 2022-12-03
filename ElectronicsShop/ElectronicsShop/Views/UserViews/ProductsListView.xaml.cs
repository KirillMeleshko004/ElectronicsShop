namespace ElectronicsShop.Views.UserViews;
public partial class ProductsListView : ContentPage
{
    public ProductsListView(ProductsListViewModel viewModel)
	{
        InitializeComponent();

		BindingContext = viewModel;
    }
}