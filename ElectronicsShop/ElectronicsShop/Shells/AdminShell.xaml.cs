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
    }
}