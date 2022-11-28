using CommunityToolkit.Maui.Behaviors;

namespace ElectronicsShop.Views;

public partial class ProductDetailsView : ContentPage
{
	public ProductDetailsView(ProductDetailsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}