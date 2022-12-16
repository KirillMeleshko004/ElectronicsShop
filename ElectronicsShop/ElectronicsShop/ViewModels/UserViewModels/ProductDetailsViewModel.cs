using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;

namespace ElectronicsShop.ViewModels.UserViewModels
{
    [QueryProperty(nameof(CurrentProduct), nameof(CurrentProduct))]
    public partial class ProductDetailsViewModel : BaseViewModel
    {
        readonly CartService _cartService;
        readonly FavouritesService _favouritesService;

        [ObservableProperty]
        Product _currentProduct;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NotInCart))]
        bool _inCart = false;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotFavouriteForUser))]
        bool _isFavouriteForUser = false;
        public bool IsNotFavouriteForUser => !_isFavouriteForUser;
        public bool NotInCart => !_inCart;

        public ProductDetailsViewModel(CartService cartService, FavouritesService favouritesService)
        {
            _cartService = cartService;
            _favouritesService = favouritesService;

            PropertyChanged += ProductPropertyChanged;
        }
        public async void ProductPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(CurrentProduct)) return;
            InCart = await _cartService.IsProductInCartOfUser(App.UserName, CurrentProduct);
            IsFavouriteForUser = await _favouritesService.IsProductFavouriteForUserAsync(App.UserName, CurrentProduct);
        }

        [RelayCommand]
        async Task AddToCart(Product product)
        {
            IsBusy = true;
            await _cartService.AddProductToCartAsync(App.UserName, new CartProduct(product) { Quantity = 0 });


            string text = "Added to cart";
            await Toast.Make(text, ToastDuration.Short).Show();

            InCart = true;
            IsBusy = false;
        }

        [RelayCommand]
        async Task RemoveFromCart(Product product)
        {
            IsBusy = true;
            await _cartService.FullRemoveProductAsync(App.UserName, (product)); 


            string text = "Removed from cart";
            await Toast.Make(text, ToastDuration.Short).Show();

            InCart = false;
            IsBusy = false;
        }

        [RelayCommand]
        async Task AddToFavourites()
        {
            IsBusy = true;
            if (IsFavouriteForUser)
                await _favouritesService.DeleteFromFavouritesAsync(App.UserName, CurrentProduct);
            else await _favouritesService.SetFavouriteAsync(App.UserName, CurrentProduct);
            IsFavouriteForUser = !IsFavouriteForUser;

            string message;
            if (IsFavouriteForUser)
                message = "Added to favourites";
            else message = "Removed from favourites";
            await Toast.Make(message, ToastDuration.Short).Show();
            IsBusy = false;
        }
    }
}
