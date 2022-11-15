namespace ElectronicsShop.Views;

public partial class ProductsListView : ContentPage
{
    public ProductsListView(ProductsListViewModel viewModel)
	{
        InitializeComponent();

		BindingContext = viewModel;
    }
}