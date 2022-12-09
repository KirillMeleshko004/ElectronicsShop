using ElectronicsShop.ViewModels;

namespace ElectronicsShop.Views.UserViews;

public partial class FavouritesView : ContentPage
{
    public IRefreshableAsync ViewModel { get; set; }
    public FavouritesView(FavouritesViewModel viewModel)
    {
        InitializeComponent();

        ViewModel = viewModel;
        BindingContext = viewModel;
    }
}