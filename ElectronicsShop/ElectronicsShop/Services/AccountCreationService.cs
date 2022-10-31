namespace ElectronicsShop.Services
{
    public class AccountCreationService
    {
        public async Task<AccountInfo> CreateAccountAsync(string login, string password, string repeatPassword)
        {
            return await TempServer.CreateAccountAsync(login, password, repeatPassword);
        }
    }
}
