using CommunityToolkit.Maui;

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
        builder.Services.AddTransient<AdminProductsView>();
        builder.Services.AddTransient<AdminProductsViewModel>();
        builder.Services.AddTransient<ProductCreationView>();
        builder.Services.AddTransient<ProductCreationViewModel>();
        builder.Services.AddTransient<ProductChangingView>();
        builder.Services.AddTransient<ProductChangingViewModel>();
        builder.Services.AddTransient<AddressView>();
        builder.Services.AddTransient<AddressViewModel>();
        builder.Services.AddTransient<CategoryCreationView>();
        builder.Services.AddTransient<CategoryCreationViewModel>();
        builder.Services.AddTransient<AdminUsersView>();
        builder.Services.AddTransient<AdminUsersViewModel>();
        builder.Services.AddTransient<AdminOrdersView>();
        builder.Services.AddTransient<AdminOrdersViewModel>();
        builder.Services.AddTransient<AdminOrderDetailsView>();
        builder.Services.AddTransient<AdminOrderDetailsViewModel>();

        builder.Services.AddSingleton<AuthorizationService>();
        builder.Services.AddSingleton<AccountCreationService>();
        builder.Services.AddSingleton<PasswordChangingService>();
        builder.Services.AddSingleton<ProductsService>();
        builder.Services.AddSingleton<CartService>();
        builder.Services.AddSingleton<OrderService>();
        builder.Services.AddSingleton<FavouritesService>();
        builder.Services.AddSingleton<AddressService>();
        builder.Services.AddSingleton<CategoryService>();

        return builder.Build();
    }
}
