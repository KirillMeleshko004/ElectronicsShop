namespace ElectronicsShop.Services.AccountServices
{
    public class AuthorizationService
    { 
        public async Task<AccountInfo> AuthUserAsync(string login, string password)
        {
            string state = AuthDataChecker.CheckAuthData(login, password);
            if (state != AccountErrorMessages.SUCCESS) return new AccountInfo(null, null, state);

            UserData userAccount = await DataSourceService<UserData>.GetDataAsync(login, nameof(UserData.UserName));

            if (userAccount is null || userAccount.Password != SHA256HashComputer.ComputeSha256Hash(password))
                return new AccountInfo(null, null, AccountErrorMessages.INCORRECT_LOGIN_OR_PASSWORD);

            return new AccountInfo(login, userAccount.Role, AccountErrorMessages.SUCCESS);
        }
    }
}
