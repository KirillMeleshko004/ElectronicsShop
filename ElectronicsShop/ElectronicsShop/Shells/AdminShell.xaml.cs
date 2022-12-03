namespace ElectronicsShop.Shells;

public partial class AdminShell : Shell
{
	public AdminShell()
	{
		InitializeComponent();

		BindingContext = new AdminShellViewModel();

        Routing.RegisterRoute(nameof(ProductCreationView), typeof(ProductCreationView));
    }
}