using ElectronicsShop.Views;
using System.Security.AccessControl;

namespace ElectronicsShop;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(RegistrationViewLoginPage), typeof(RegistrationViewLoginPage));
	}
}
