namespace ElectronicsShop.Services
{
    public class LoginCheckService
    {
        public async Task<AccountInfo> CheckLoginAsync(string login)
        {
            return await TempServer.CheckLoginAsync(login);
        }
    }
}
