namespace ElectronicsShop.Services
{
    public class FavouritesService
    {
        public async Task<List<Product>> GetFavouritesForUserAsync(string userName)
        {
            return await TempServer.GetFavouritesForUserAsync(userName);
        }
        public async Task SetFavouriteAsync(string userName, int productId)
        {
            await TempServer.SetFavouriteAsync(userName, productId);
        }
        public async Task DeleteFromFavouritesAsync(string userName, int productId)
        {
            await TempServer.DeleteFromFavouritesAsync(userName, productId);
        }
        public async Task<bool> IsProductFavouriteForUserAsync(string userName, int productId)
        {
            return await TempServer.IsProductFavouriteForUserAsync(userName, productId);
        }
    }
}
