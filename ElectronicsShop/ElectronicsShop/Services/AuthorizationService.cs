namespace ElectronicsShop.Services
{
    public class AuthorizationService
    {
        public async Task<AccountInfo> DoAuthorizationAsync(string login, string password)
        {
            return await TempServer.AuthorizationAsync(login, password);
        }
    }
}
