using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels.UserViewModels
{
    [QueryProperty(nameof(Products), nameof(Products))]
    [QueryProperty(nameof(TotalPrice), nameof(TotalPrice))]
    public partial class CheckoutViewModel : BaseViewModel
    {
        readonly OrderService _orderService;
        readonly CartService _cartService;

        [ObservableProperty]
        ObservableCollection<CartProduct> _products; 

        [ObservableProperty]
        string _country; 
        [ObservableProperty]
        string _city; 
        [ObservableProperty]
        string _street; 
        [ObservableProperty]
        int _buildingNumber; 
        [ObservableProperty]
        int? _apartmentNumber;
        [ObservableProperty]
        int _postcode;

        [ObservableProperty]
        double totalPrice;

        public CheckoutViewModel(OrderService orderService, CartService cartService)
        {
            _orderService = orderService;

            _cartService = cartService;
        }

        [RelayCommand]
        async Task ConfirmAsync()
        {
            IsBusy = true;

            Address address = new(Country, City, Street, BuildingNumber, ApartmentNumber, Postcode);

            await _orderService.CheckoutAsync(App.UserName, Products.ToList(), address, TotalPrice);
            await _cartService.ClearCartAsync(App.UserName);

            await Shell.Current.DisplayAlert("Success!",
                $"You order now awaiting confirmation. You can follow the status update in your account page",
                "Ok");
            await Shell.Current.GoToAsync($"..");

            IsBusy = false;
        }
    }
}
