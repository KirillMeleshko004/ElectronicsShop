using ElectronicsShop.ViewModels;

namespace ElectronicsShop.Views.AdminViews;

public partial class AdminUsersView : ContentPage
{
    public IRefreshableAsync ViewModel { get; set; }
    public AdminUsersView(AdminUsersViewModel viewModel)
	{
		InitializeComponent();
		ViewModel = viewModel;
		BindingContext = viewModel;
	}
}