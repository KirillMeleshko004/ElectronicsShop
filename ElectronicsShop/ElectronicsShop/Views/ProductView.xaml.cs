using CommunityToolkit.Maui.Behaviors;

namespace ElectronicsShop.Views;

public partial class ProductView : ContentPage
{
	public ProductView(ProductViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}