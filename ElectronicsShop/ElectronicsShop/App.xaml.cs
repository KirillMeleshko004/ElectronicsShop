using ElectronicsShop.Views;

namespace ElectronicsShop;

public partial class App : Application
{
	public static Account UserAccount { get; set; }
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}