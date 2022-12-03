using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    public partial class FavouritesViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<Product> _favourites;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmpty))]
        bool isEmpty = true;
        public bool IsNotEmpty => !isEmpty;

        readonly FavouritesService _favouritesService;
        public FavouritesViewModel(FavouritesService favouritesService)
        {
            _favouritesService = favouritesService;

            Refresh();
        }
        public async void Refresh()
        {
            Title = $"{App.UserAccount.UserName}'s favourites";
            Favourites = (await _favouritesService.GetFavouritesForUserAsync(App.UserAccount.UserName)).ToObservableCollection<Product>();
            IsEmpty = Favourites.Count == 0;
        }

        [RelayCommand]
        async Task GoToProduct(Product currentProduct)
        {
            await Shell.Current.GoToAsync($"{nameof(ProductDetailsView)}",
                new Dictionary<string, object>
                {
                    ["CurrentProduct"] = currentProduct
                });
        }

        [RelayCommand]
        async Task Unfavourite(Product product)
        {
            if (IsBusy) return;
            IsBusy = true;
            await _favouritesService.DeleteFromFavouritesAsync(App.UserAccount.UserName, product.Id);
            Favourites = (await _favouritesService.GetFavouritesForUserAsync(App.UserAccount.UserName)).ToObservableCollection<Product>();
            IsBusy = false;
        }
    }
}
