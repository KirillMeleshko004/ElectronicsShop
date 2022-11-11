namespace ElectronicsShop.Views;

public partial class CategoryView : ContentPage
{
	public CategoryView(CategoryViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}