using ElectronicsShop.Services.AccountServices;
using System.Diagnostics;

namespace ElectronicsShop.ViewModels.UserViewModels
{
    public partial class AddressViewModel : BaseViewModel
    {
        public List<string> Countries { get; } = AddressService.GetCountries().ToList<string>();

        readonly AddressService _addressService;


        [ObservableProperty]
        string errorMessage;

        [ObservableProperty]
        string _country;
        [ObservableProperty]
        string _city;
        [ObservableProperty]
        string _street;
        [ObservableProperty]
        int? _buildingNumber;
        [ObservableProperty]
        int? _apartmentNumber;
        [ObservableProperty]
        string _postalcode;

        public AddressViewModel(AddressService addressService)
        {
            _addressService = addressService;
        }

        [RelayCommand]
        async Task ValidateAddress()
        {
            IsBusy = true;

            try
            {
                ErrorMessage = await _addressService.ValidateAddressAsync(Country, City, Street, BuildingNumber, Postalcode);
                if (ErrorMessage != AddressErrorMessages.SUCCESS)
                {
                    IsFailed = true;
                    IsBusy = false;
                    return;
                }
                IsFailed = false;
                await GoBack(_addressService.GetFormattedAddress(Country, City, Street,
                    BuildingNumber, ApartmentNumber, Postalcode));
            }
            catch
            {
                ConnectionErrorView.ShowErrorMessage();
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task SuggestPostalCode()
        {
            IsBusy = true;
            try
            {
                Postalcode = await _addressService.GetPostcode(Country, City, Street, BuildingNumber);
            }
            catch
            {
                ConnectionErrorView.ShowErrorMessage();
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        [ObservableProperty]
        bool isNotEmpty = false;

        async Task GoBack(string address)
        {
            await Shell.Current.GoToAsync($"..",
                new Dictionary<string, object>
                {
                    ["Address"] = address
                });
        }
    }
}
