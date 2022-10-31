namespace ElectronicsShop.Services
{
    public class AccountCreationService
    {
        public async Task<bool> CreateAccount(string login, string password, string repeatPassword)
        {
            if (password != repeatPassword) return false;
            return await TempServer.CreateAccount(login, password);
        }
    }
}
