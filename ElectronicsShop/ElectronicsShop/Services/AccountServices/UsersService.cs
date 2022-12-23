namespace ElectronicsShop.Services.AccountServices
{
    public class UsersService
    {
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return (from userData in await DataSourceService<UserData>.GetDataAsync()
                    select new User
                    {
                        Role = userData.Role,
                        UID = userData.UID,
                        UserName = userData.UserName,
                    });
        }
        public async Task ChangeRole(string userName, string role)
        {
            UserData user = await DataSourceService<UserData>.GetDataAsync(userName, nameof(User.UserName));
            user.Role = role;
            await DataSourceService<UserData>.AlterSingleElementAsync(user, userName, nameof(User.UserName));
            if(role == Roles.Admin)
            {
                await DataSourceService<Cart>.DeleteElementsAsync(userName, nameof(Cart.UserName));
                await DataSourceService<FavouritesService.Favourites>.DeleteElementsAsync(userName, nameof(FavouritesService.Favourites.UserName));
            }
        }
        public async Task DeleteAccount(string userName)
        {
            await DataSourceService<UserData>.DeleteElementsAsync(userName, nameof(UserData.UserName));
            await DataSourceService<Cart>.DeleteElementsAsync(userName, nameof(Cart.UserName));
            await DataSourceService<FavouritesService.Favourites>.DeleteElementsAsync(userName, nameof(FavouritesService.Favourites.UserName));
        }
    }
}
