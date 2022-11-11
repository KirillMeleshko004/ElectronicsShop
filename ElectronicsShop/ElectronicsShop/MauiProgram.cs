﻿using CommunityToolkit.Maui;
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
        builder.Services.AddTransient<CategoryView>();
        builder.Services.AddTransient<CategoryViewModel>();
        builder.Services.AddTransient<CartView>();
        builder.Services.AddTransient<CartViewModel>();
        builder.Services.AddTransient<ProductView>();
        builder.Services.AddTransient<ProductViewModel>();
        builder.Services.AddTransient<FilterView>();
        builder.Services.AddTransient<FilterViewModel>();

        builder.Services.AddSingleton<AuthorizationService>();
        builder.Services.AddSingleton<AccountCreationService>();
		builder.Services.AddSingleton<ProductsService>();
        builder.Services.AddSingleton<CartService>();
        builder.Services.AddSingleton<FilterService>();

        return builder.Build();
	}
}
