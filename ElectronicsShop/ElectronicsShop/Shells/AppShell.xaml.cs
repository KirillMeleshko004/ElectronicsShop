namespace ElectronicsShop.Shells;

public partial class AppShell : Shell
{
	public AppShell()
	{
        InitializeComponent();

        BindingContext = new AppShellViewModel();

        Routing.RegisterRoute(nameof(CheckoutView), typeof(CheckoutView));
        Routing.RegisterRoute(nameof(ProductsListView), typeof(ProductsListView));
        Routing.RegisterRoute(nameof(AuthorizationView), typeof(AuthorizationView));
        Routing.RegisterRoute(nameof(RegistrationView), typeof(RegistrationView));
        Routing.RegisterRoute(nameof(ProductDetailsView), typeof(ProductDetailsView));
        Routing.RegisterRoute(nameof(OrdersView), typeof(OrdersView));
        Routing.RegisterRoute(nameof(OrderDetailsView), typeof(OrderDetailsView));
    }
    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        string location = args.Current?.Location?.ToString();
        switch(location)
        {
            case $"//{nameof(CategoryView)}":
                ((CategoryView)Shell.Current.CurrentPage).ViewModel.Refresh();
                break;
            case $"//{nameof(CartView)}":
                ((CartView)Shell.Current.CurrentPage).ViewModel.Refresh();
                break;
            case $"//{nameof(AccountView)}":
                ((AccountView)Shell.Current.CurrentPage).ViewModel.Refresh();
                break;
            case $"//{nameof(FavouritesView)}":
                ((FavouritesView)Shell.Current.CurrentPage).ViewModel.Refresh();
                break;
            case $"//{nameof(FeedbackView)}":
                ((FeedbackView)Shell.Current.CurrentPage).ViewModel.Refresh();
                break;
            default: break;
        }

        base.OnNavigated(args);
    }
}
