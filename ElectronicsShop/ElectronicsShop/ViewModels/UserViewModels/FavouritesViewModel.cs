using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels.UserViewModels
{
    public partial class FavouritesViewModel : BaseViewModel, IRefreshableAsync
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

            RefreshAsync();
        }
        public async void RefreshAsync()
        {
            Title = $"{App.UserName}'s favourites";
            Favourites = (await _favouritesService.GetFavouritesForUserAsync(App.UserName))?.ToObservableCollection<Product>();
            
            IsEmpty = Favourites is null ? true : Favourites.Count == 0;
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
            await _favouritesService.DeleteFromFavouritesAsync(App.UserName, product);
            Favourites = (await _favouritesService.GetFavouritesForUserAsync(App.UserName))?.ToObservableCollection<Product>();
            IsEmpty = Favourites is null ? true : Favourites.Count == 0;
            IsBusy = false;
        }
    }
}
