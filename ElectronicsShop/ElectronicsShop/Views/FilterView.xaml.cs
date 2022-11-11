namespace ElectronicsShop.Views;

public partial class FilterView : ContentPage
{
	public FilterView(FilterViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}