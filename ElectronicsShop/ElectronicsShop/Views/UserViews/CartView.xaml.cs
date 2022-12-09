using ElectronicsShop.ViewModels;

namespace ElectronicsShop.Views.UserViews;

public partial class CartView : ContentPage
{
    public IRefreshableAsync ViewModel { get; set; }
    public CartView(CartViewModel viewModel)
    {
        InitializeComponent();

        ViewModel = viewModel;
        BindingContext = viewModel;
    }
}