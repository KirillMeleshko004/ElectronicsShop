namespace ElectronicsShop.Services
{
    public class FavouritesService
    {
        public async Task<List<Product>> GetFavouritesForUserAsync(string userName)
        {
            return (await DataSourceService<Favourites>.GetDataAsync(userName, nameof(Favourites.UserName)))?.Products;
        }
        public async Task SetFavouriteAsync(string userName, Product product)
        {
            Favourites favourites = await DataSourceService<Favourites>.GetDataAsync(userName, nameof(Favourites.UserName));

            favourites ??= new Favourites { UserName = userName, Products = new()};
            favourites.Products ??= new();
            favourites.Products.Add(product);

            await DataSourceService<Favourites>.AlterSingleElementAsync(favourites, userName, nameof(Favourites.UserName));
        }
        public async Task DeleteFromFavouritesAsync(string userName, Product product)
        {
            Favourites favourites = await DataSourceService<Favourites>.GetDataAsync(userName, nameof(Favourites.UserName));
            favourites.Products.Remove(product);
            await DataSourceService<Favourites>.AlterSingleElementAsync(favourites, userName, nameof(Favourites.UserName));
        }
        public async Task<bool> IsProductFavouriteForUserAsync(string userName, Product product)
        {
            Favourites favourites = await DataSourceService<Favourites>.GetDataAsync(userName, nameof(Favourites.UserName));
            return favourites?.Products is not null && favourites.Products.Contains(product);
        }
        public record Favourites
        {
            public string UserName { get; set; }
            public List<Product> Products { get; set; }
        }
    }
}
