namespace ElectronicsShop.Views.AdminViews;

public partial class CategoryCreationView : ContentPage
{
	public CategoryCreationView(CategoryCreationViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}