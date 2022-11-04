namespace ElectronicsShop.Views;

public partial class CategoriesView : ContentPage
{
	public CategoriesView(CategoriesViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}