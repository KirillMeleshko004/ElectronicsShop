namespace ElectronicsShop.Services
{
    public class FavouritesService
    {
        public async Task<List<Product>> GetFavouritesForUserAsync(string userName)
        {
            await Task.Delay(100);
            return null;
        }
    }
}
