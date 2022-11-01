using ElectronicsShop.Views;

namespace ElectronicsShop;

public partial class AppShell : Shell
{
	public AppShell()
	{
        InitializeComponent();

        BindingContext = new AppShellViewModel();

        Routing.RegisterRoute(nameof(ShopMainView), typeof(ShopMainView));
        Routing.RegisterRoute(nameof(AuthorizationView), typeof(AuthorizationView));
        Routing.RegisterRoute(nameof(RegistrationView), typeof(RegistrationView));
    }
}
