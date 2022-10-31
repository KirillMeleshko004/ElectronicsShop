namespace ElectronicsShop.Services
{
    public class LoginCheckService
    {
        public async Task<bool> CheckLogin(string login)
        {
            return await TempServer.CheckLogin(login);
        }
    }
}
