using CommunityToolkit.Maui;
using ElectronicsShop.Services;
using ElectronicsShop.ViewModels;
using ElectronicsShop.Views;

namespace ElectronicsShop;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiApp<App>().UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddTransient<AuthorizationView>();
		builder.Services.AddTransient<AuthorizationViewModel>();
		builder.Services.AddTransient<RegistrationView>();
		builder.Services.AddTransient<RegistrationViewModel>();
		builder.Services.AddTransient<ShopMainView>();
        builder.Services.AddTransient<ShopMainViewModel>();

		builder.Services.AddSingleton<Account>();

        builder.Services.AddSingleton<AuthorizationService>();
        builder.Services.AddSingleton<AccountCreationService>();
        return builder.Build();
	}
}
