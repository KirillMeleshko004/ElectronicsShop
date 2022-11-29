using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;

namespace ElectronicsShop.ViewModels
{
    public partial class FavouritesViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<Product> _favourites;

        public FavouritesViewModel()
        {
            _favourites = App.UserAccount.Favourites.ToObservableCollection<Product>();
        }
        public void Refresh()
        {
            _favourites = App.UserAccount.Favourites.ToObservableCollection<Product>();
        }
    }
}
