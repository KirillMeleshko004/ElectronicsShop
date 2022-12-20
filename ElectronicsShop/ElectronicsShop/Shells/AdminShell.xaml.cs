namespace ElectronicsShop.Shells;

public partial class AdminShell : Shell
{
	public AdminShell()
	{
		InitializeComponent();

		BindingContext = new AdminShellViewModel();

		Routing.RegisterRoute(nameof(ProductCreationView), typeof(ProductCreationView));
		Routing.RegisterRoute(nameof(ProductChangingView), typeof(ProductChangingView));
        Routing.RegisterRoute(nameof(CategoryCreationView), typeof(CategoryCreationView));
        Routing.RegisterRoute(nameof(AdminOrderDetailsView), typeof(AdminOrderDetailsView));
        Routing.RegisterRoute(nameof(UserDetailsView), typeof(UserDetailsView));
        Routing.RegisterRoute(nameof(CategoryChangingView), typeof(CategoryChangingView));
        Routing.RegisterRoute(nameof(DeleteCategoryView), typeof(DeleteCategoryView));
    }
    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        string location = args.Current?.Location?.ToString();
        switch (location)
        {
            case $"//{nameof(AdminProductsView)}":
                ((AdminProductsView)Shell.Current.CurrentPage).ViewModel.RefreshAsync();
                break;
            case $"//{nameof(AdminUsersView)}":
                ((AdminUsersView)Shell.Current.CurrentPage).ViewModel.RefreshAsync();
                break;
            case $"//{nameof(AdminOrdersView)}":
                ((AdminOrdersView)Shell.Current.CurrentPage).ViewModel.RefreshAsync();
                break;
            default: break;
        }

        base.OnNavigated(args);
    }
}