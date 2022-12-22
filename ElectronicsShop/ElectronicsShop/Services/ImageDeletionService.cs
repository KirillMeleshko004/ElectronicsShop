using Firebase.Database;
using static ElectronicsShop.Services.FavouritesService;

namespace ElectronicsShop.Services
{
    public class ImageDeletionService
    {
        public static async Task<bool> ShouldDelete<T>(string imageURI) where T : IImageURIContainer
        {
            var favTask = IsImageInFavourites(imageURI);
            var cartTask = IsImageInCart(imageURI);
            var prodTask = ImageSourceService<T>.CountImageUsings(imageURI);

            var Tasks = new List<Task> { favTask, cartTask, prodTask };

            while (Tasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(Tasks);
                if (finishedTask == favTask)
                {
                    if (favTask.Result)
                    {
                        return false;
                    }
                }
                else if (finishedTask == cartTask)
                {
                    if (cartTask.Result)
                    {
                        return false;
                    }
                }
                else if (finishedTask == prodTask)
                {
                    if (prodTask.Result != 0)
                    {
                        return false;
                    }
                }
                Tasks.Remove(finishedTask);
            }
            return true;
        }
        private static async Task<bool> IsImageInFavourites(string imageURI)
        {
            IEnumerable<UserData> users = await DataSourceService<UserData>.GetDataAsync();
            foreach (User user in users)
            {
                var favourites = await DataSourceService<FavouritesService.Favourites>
                    .GetDataAsync(user.UserName, nameof(FavouritesService.Favourites.UserName));
                if (favourites is null || favourites.Products is null) continue;
                if ((from product in favourites.Products where product.ImageURI == imageURI select product).Any())
                    return true;
            }
            return false;
        }
        private static async Task<bool> IsImageInCart(string imageURI)
        {
            IEnumerable<UserData> users = await DataSourceService<UserData>.GetDataAsync();
            foreach (User user in users)
            {
                var cart = await DataSourceService<Cart>
                    .GetDataAsync(user.UserName, nameof(Cart.UserName));
                if (cart is null || cart.Products is null) continue;
                if ((from product in cart.Products where product.ImageURI == imageURI select product).Any())
                    return true;
            }
            return false;
        }
    }
}
