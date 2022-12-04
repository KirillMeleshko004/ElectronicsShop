namespace ElectronicsShop.Views.AdminViews;

public partial class ProductChangingView : ContentPage
{
	public ProductChangingView(ProductChangingViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}