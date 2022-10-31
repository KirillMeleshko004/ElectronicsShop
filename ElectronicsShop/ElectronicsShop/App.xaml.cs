using ElectronicsShop.Views;

namespace ElectronicsShop;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}