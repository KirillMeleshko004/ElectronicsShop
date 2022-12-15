using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels.UserViewModels
{
    [QueryProperty(nameof(Products), nameof(Products))]
    [QueryProperty(nameof(TotalPrice), nameof(TotalPrice))]
    [QueryProperty(nameof(Address), nameof(Address))]
    public partial class CheckoutViewModel : BaseViewModel
    {
        readonly OrderService _orderService;
        readonly CartService _cartService;

        [ObservableProperty]
        ObservableCollection<CartProduct> _products;

        [ObservableProperty]
        string _email;
        [ObservableProperty]
        string _address;

        [ObservableProperty]
        double _totalPrice;

        public CheckoutViewModel(OrderService orderService, CartService cartService)
        {
            _orderService = orderService;

            _cartService = cartService;
        }

        [RelayCommand]
        async Task EnterAddressAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(AddressView)}");
        }

        [RelayCommand]
        async Task ConfirmAsync()
        {
            IsBusy = true;

            if (string.IsNullOrEmpty(Address))
            {
                await ShowErrorToast("Incorrect address");
                IsBusy = false;
                return;
            }
            if (string.IsNullOrEmpty(Email) || !Order.IsEmail(Email))
            {
                await ShowErrorToast("Incorrect email");
                IsBusy = false;
                return;
            }


            await _orderService.CheckoutAsync(App.UserName,
                Products.ToList(),
                Address,
                Email,
                TotalPrice);
            await _cartService.ClearCartAsync(App.UserName);

            await Shell.Current.DisplayAlert("Success!",
                $"You order now awaiting confirmation. You can follow the status update in your account page",
                "Ok");
            await Shell.Current.GoToAsync($"..");

            IsBusy = false;
        }
        private async Task ShowErrorToast(string message)
        {
            await Toast.Make(message, ToastDuration.Long).Show();
        }
    }
}
