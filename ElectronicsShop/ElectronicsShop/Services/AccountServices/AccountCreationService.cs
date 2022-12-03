using Firebase.Database;
using Firebase.Database.Query;

namespace ElectronicsShop.Services.AccountServices
{
    public class AccountCreationService
    {
        private const string _dbURI = "https://electronicsshop-8c6b3-default-rtdb.europe-west1.firebasedatabase.app/";
        private readonly FirebaseClient _firebaseClient = new FirebaseClient(_dbURI);

        public async Task<AccountInfo> RegisterUserAsync(string login, string password, string repeatPassword)
        {
            string state = AuthDataChecker.CheckRegData(login, password, repeatPassword);
            if (state != AccountErrorMessages.SUCCESS) return new AccountInfo(null, null, state);

            IReadOnlyCollection<FirebaseObject<UserData>> accounts = await _firebaseClient
                .Child(nameof(UserData))
                .OnceAsync<UserData>();
            if ((from acc in accounts where acc.Object.UserName == login select acc).Any())
                return new AccountInfo(null, null, AccountErrorMessages.SAME_LOGIN_EXIST);
            string role = accounts.Any() ? Roles.User : Roles.Admin;
            await _firebaseClient.Child(nameof(UserData)).PostAsync(new UserData
            {
                UID = accounts.Count + 1,
                UserName = login,
                Password = SHA256HashComputer.ComputeSha256Hash(password),
                Role = role
            });

            return new AccountInfo(login, role, AccountErrorMessages.SUCCESS);
        }
    }
}
