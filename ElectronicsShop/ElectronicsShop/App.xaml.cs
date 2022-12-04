namespace ElectronicsShop;

public partial class App : Application
{
	public static string UserName { get; set; }
    public App()
	{
		InitializeComponent();

		MainPage = new Shells.AuthShell();
	}
}