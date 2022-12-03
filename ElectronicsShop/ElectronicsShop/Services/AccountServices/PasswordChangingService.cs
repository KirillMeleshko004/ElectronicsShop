using Firebase.Database;
using Firebase.Database.Query;

namespace ElectronicsShop.Services.AccountServices
{
    public class PasswordChangingService
    {
        private const string _dbURI = "https://electronicsshop-8c6b3-default-rtdb.europe-west1.firebasedatabase.app/";
        private readonly FirebaseClient _firebaseClient = new FirebaseClient(_dbURI);


        public async Task<AccountInfo> ChangePasswordAsync(string login, string oldPassword, string newPassword, string repeatPassword)
        {
            string state = AuthDataChecker.CheckPasswordChangeData(oldPassword, newPassword, repeatPassword);
            if (state != AccountErrorMessages.SUCCESS) return new AccountInfo(null, null, state);

            FirebaseObject<UserData> userAccount = (await _firebaseClient
                .Child(nameof(UserData))
                .OrderBy(nameof(UserData.UserName))
                .EqualTo(login)
                .OnceAsync<UserData>())
                .FirstOrDefault((FirebaseObject<UserData>)null);

            if (userAccount.Object.Password != SHA256HashComputer.ComputeSha256Hash(oldPassword))
                return new AccountInfo(null, null, AccountErrorMessages.INCORRECT_LOGIN_OR_PASSWORD);
            userAccount.Object.Password = SHA256HashComputer.ComputeSha256Hash(newPassword);
            await _firebaseClient.Child(nameof(UserData))
                .Child(userAccount.Key)
                .PatchAsync(userAccount.Object);

            return new AccountInfo(login, userAccount.Object.Role, AccountErrorMessages.SUCCESS);
        }
    }
}
