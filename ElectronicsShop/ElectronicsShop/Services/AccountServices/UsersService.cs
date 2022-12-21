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
        }
        public async Task DeleteAccount(string userName)
        {
            await DataSourceService<UserData>.DeleteElementsAsync(userName, nameof(UserData.UserName));
        }
    }
}
