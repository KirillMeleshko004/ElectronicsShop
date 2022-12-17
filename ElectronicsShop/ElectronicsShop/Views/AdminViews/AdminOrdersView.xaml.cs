using ElectronicsShop.ViewModels;

namespace ElectronicsShop.Views.AdminViews;

public partial class AdminOrdersView : ContentPage
{
    public IRefreshableAsync ViewModel { get; set; }
    public AdminOrdersView(AdminOrdersViewModel viewModel)
	{
		InitializeComponent();
        ViewModel = viewModel;
        BindingContext = viewModel;
    }
}