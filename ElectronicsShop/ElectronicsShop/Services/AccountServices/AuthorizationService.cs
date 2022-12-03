using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;

namespace ElectronicsShop.Services.AccountServices
{
    public class AuthorizationService
    {
        private const string _dbURI = "https://electronicsshop-8c6b3-default-rtdb.europe-west1.firebasedatabase.app/";
        private readonly FirebaseClient _firebaseClient = new FirebaseClient(_dbURI);

        public async Task<AccountInfo> AuthUserAsync(string login, string password)
        {
            string state = AuthDataChecker.CheckAuthData(login, password);
            if (state != AccountErrorMessages.SUCCESS) return new AccountInfo(null, null, state);

            FirebaseObject<UserData> userAccount = (await _firebaseClient
                .Child(nameof(UserData))
                .OrderBy(nameof(UserData.UserName))
                .EqualTo(login)
                .OnceAsync<UserData>())
                .FirstOrDefault((FirebaseObject<UserData>)null);
            if (userAccount is null || userAccount.Object.Password != SHA256HashComputer.ComputeSha256Hash(password))
                return new AccountInfo(null, null, AccountErrorMessages.INCORRECT_LOGIN_OR_PASSWORD);

            return new AccountInfo(login, userAccount.Object.Role, AccountErrorMessages.SUCCESS);
        }
    }
}
