using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    public partial class FavouritesViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<Product> _favourites;

        FavouritesService _favouritesService;
        public FavouritesViewModel(FavouritesService favouritesService)
        {
            _favouritesService = favouritesService;

            Refresh();
        }
        public async void Refresh()
        {
            Favourites = (await _favouritesService.GetFavouritesForUserAsync(App.UserAccount.UserName)).ToObservableCollection<Product>();
        }
    }
}
