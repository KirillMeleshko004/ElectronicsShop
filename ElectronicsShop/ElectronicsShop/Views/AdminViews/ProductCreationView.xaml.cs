namespace ElectronicsShop.Views.AdminViews;

public partial class ProductCreationView : ContentPage
{
	public ProductCreationView(ProductCreationViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}