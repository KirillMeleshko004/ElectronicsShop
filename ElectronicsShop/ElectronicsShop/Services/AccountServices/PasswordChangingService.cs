namespace ElectronicsShop.Services.AccountServices
{
    public class PasswordChangingService
    {
        public async Task<AccountInfo> ChangePasswordAsync(string login, string oldPassword, string newPassword, string repeatPassword)
        {
            return await TempServer.ChangePasswordAsync(login, oldPassword, newPassword, repeatPassword);
        }
    }
}
