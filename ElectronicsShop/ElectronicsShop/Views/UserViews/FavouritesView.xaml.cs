namespace ElectronicsShop.Views.UserViews;

public partial class FavouritesView : ContentPage
{
	public FavouritesViewModel ViewModel { get; set; }
	public FavouritesView(FavouritesViewModel viewModel)
	{
		InitializeComponent();

		ViewModel = viewModel;
		BindingContext = ViewModel;
	}
}