using ElectronicsShop.ViewModels;

namespace ElectronicsShop.Views.AdminViews;

public partial class AdminProductsView : ContentPage
{
    public IRefreshableAsync ViewModel { get; set; }
    public AdminProductsView(AdminProductsViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        BindingContext = viewModel;
    }
}