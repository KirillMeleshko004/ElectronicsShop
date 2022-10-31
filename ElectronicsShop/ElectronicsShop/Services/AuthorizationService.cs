namespace ElectronicsShop.Services
{
    public class AuthorizationService
    {
        public async Task<bool> DoAuthorizationAsync(string login, string password)
        {
            return await TempServer.AuthorizationAsync(login, password);
        }
    }
}
