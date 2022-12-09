namespace ElectronicsShop.Services.AccountServices
{
    public class PasswordChangingService
    {
        public async Task<AccountInfo> ChangePasswordAsync(string login, string oldPassword, string newPassword, string repeatPassword)
        {
            string state = AuthDataChecker.CheckPasswordChangeData(oldPassword, newPassword, repeatPassword);
            if (state != AccountErrorMessages.SUCCESS) return new AccountInfo(null, null, state);

            UserData userAccount = await DataSourceService<UserData>.GetDataAsync(login, nameof(UserData.UserName));

            if (userAccount is null || userAccount.Password != SHA256HashComputer.ComputeSha256Hash(oldPassword))
                return new AccountInfo(null, null, AccountErrorMessages.INCORRECT_OLD_PASSWORD);

            userAccount.Password = SHA256HashComputer.ComputeSha256Hash(newPassword);

            await DataSourceService<UserData>.AlterSingleElementAsync(userAccount, login, nameof(UserData.UserName));

            return new AccountInfo(login, userAccount.Role, AccountErrorMessages.SUCCESS);
        }
    }
}
