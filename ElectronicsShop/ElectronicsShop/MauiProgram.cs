using CommunityToolkit.Maui;
using ElectronicsShop.Services.AccountServices;
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
		builder.Services.AddTransient<ProductsListView>();
		builder.Services.AddTransient<ProductsListViewModel>();
        builder.Services.AddTransient<CategoryView>();
        builder.Services.AddTransient<CategoryViewModel>();
        builder.Services.AddTransient<CartView>();
        builder.Services.AddTransient<CartViewModel>();
        builder.Services.AddTransient<ProductDetailsView>();
        builder.Services.AddTransient<ProductDetailsViewModel>();
        builder.Services.AddTransient<AccountView>();
        builder.Services.AddTransient<AccountViewModel>();
        builder.Services.AddTransient<OrdersView>();
        builder.Services.AddTransient<OrdersViewModel>();
        builder.Services.AddTransient<CheckoutView>();
        builder.Services.AddTransient<CheckoutViewModel>();
        builder.Services.AddTransient<OrderDetailsView>();
        builder.Services.AddTransient<OrderDetailsViewModel>();
        builder.Services.AddTransient<FavouritesView>();
        builder.Services.AddTransient<FavouritesViewModel>();
        builder.Services.AddTransient<FeedbackView>();
        builder.Services.AddTransient<FeedbackViewModel>();

        builder.Services.AddSingleton<AuthorizationService>();
        builder.Services.AddSingleton<AccountCreationService>();
        builder.Services.AddSingleton<PasswordChangingService>();
        builder.Services.AddSingleton<ProductsService>();
        builder.Services.AddSingleton<CartService>();
        builder.Services.AddSingleton<OrderService>();
        builder.Services.AddSingleton<FavouritesService>();

        return builder.Build();
	}
}
