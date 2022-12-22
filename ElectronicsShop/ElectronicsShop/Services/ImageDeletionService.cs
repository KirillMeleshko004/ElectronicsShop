using Firebase.Database;

namespace ElectronicsShop.Services
{
    public class ImageDeletionService
    {
        public static async Task<bool> ShouldDelete(Category category)
        {
            var favTask = IsImageInFavourites(category.ImageURI);
            var cartTask = IsImageInCart(category.ImageURI);
            var prodTask = ImageSourceService<Category>.CountImageUsings(category.ImageURI);

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
                foreach(var product in favourites.Products)
                {
                    if ((System.IO.Path.GetFileName(new Uri(product.ImageURI).LocalPath)) ==
                            (System.IO.Path.GetFileName(new Uri(imageURI).LocalPath)))
                        return true;
                }
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
                foreach (var product in cart.Products)
                {
                    if ((System.IO.Path.GetFileName(new Uri(product.ImageURI).LocalPath)) ==
                            (System.IO.Path.GetFileName(new Uri(imageURI).LocalPath)))
                        return true;
                }
            }
            return false;
        }
        public static async Task<bool> ShouldDelete(Product product)
        {
            var favTask = IsProductInFavourites(product);
            var cartTask = IsProductInCart(product);
            var prodTask = ImageSourceService<Product>.CountImageUsings(product.ImageURI);

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
        private static async Task<bool> IsProductInFavourites(Product product)
        {
            IEnumerable<UserData> users = await DataSourceService<UserData>.GetDataAsync();
            foreach (User user in users)
            {
                var favourites = await DataSourceService<FavouritesService.Favourites>
                    .GetDataAsync(user.UserName, nameof(FavouritesService.Favourites.UserName));
                if (favourites is null || favourites.Products is null) continue;
                if (favourites.Products.Contains(product))
                    return true;
            }
            return false;
        }
        private static async Task<bool> IsProductInCart(Product product)
        {
            IEnumerable<UserData> users = await DataSourceService<UserData>.GetDataAsync();
            foreach (User user in users)
            {
                var cart = await DataSourceService<Cart>
                    .GetDataAsync(user.UserName, nameof(Cart.UserName));
                if (cart is null || cart.Products is null) continue;
                if (cart.Products.Contains(new CartProduct(product)))
                    return true;
            }
            return false;
        }
    }
}
