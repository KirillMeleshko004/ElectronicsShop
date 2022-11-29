namespace ElectronicsShop.Services
{
    public class FavouritesService
    {
        public async Task<List<Product>> GetFavouritesForUserAsync(string userName)
        {
            return await TempServer.GetFavouritesForUserAsync(userName);
        }
        public async static void SetFavouriteAsync(string userName, int productId)
        {
            await TempServer.SetFavouriteAsync(userName, productId);
        }
        public async Task<bool> IsProductFavouriteForUserAsync(string userName, int productId)
        {
            return await TempServer.IsProductFavouriteForUserAsync(userName, productId);
        }
    }
}
