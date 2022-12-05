namespace ElectronicsShop.Shells;

public partial class AuthShell : Shell
{
    public AuthShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(AuthorizationView), typeof(AuthorizationView));
        Routing.RegisterRoute(nameof(RegistrationView), typeof(RegistrationView));
    }
}