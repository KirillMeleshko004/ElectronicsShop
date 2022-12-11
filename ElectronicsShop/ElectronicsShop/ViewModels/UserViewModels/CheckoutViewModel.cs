using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Geocoding;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
        double totalPrice;

        [ObservableProperty]
        bool isNotEmpty = false;

        public CheckoutViewModel(OrderService orderService, CartService cartService)
        {
            _orderService = orderService;

            _cartService = cartService;
        }

        private void CheckEmpty(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsNotEmpty)) return;
            if (String.IsNullOrEmpty(Email) ||
                String.IsNullOrEmpty(Address))
                IsNotEmpty = false;
            else IsNotEmpty = true;
        }
        [RelayCommand]
        async Task EnterAddress()
        {
            await Shell.Current.GoToAsync($"{nameof(AddressView)}");
        }


        [RelayCommand]
        async Task ConfirmAsync()
        {
            IsBusy = true;

            //await _orderService.CheckoutAsync(App.UserName, 
            //    Products.ToList(), 
            //    await _addressService.GetFormattedAddressAsync(Country, City, Street, (int)BuildingNumber),
            //    TotalPrice);
            await _cartService.ClearCartAsync(App.UserName);

            await Shell.Current.DisplayAlert("Success!",
                $"You order now awaiting confirmation. You can follow the status update in your account page",
                "Ok");
            await Shell.Current.GoToAsync($"..");

            IsBusy = false;
        }
    }
}
