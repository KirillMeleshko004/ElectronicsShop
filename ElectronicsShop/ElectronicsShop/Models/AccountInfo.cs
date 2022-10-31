namespace ElectronicsShop.Models
{
    public class AccountInfo
    {
        private static List<String> errorMessageList = new()
        {
            "Incorrect login or password!",
            "Same login already exist!",
            "Connection error!",
            "Some fields are empty!",
            "Passords are not the same!",
            "Success!"
        };
        public string Login { get; set; }
        public string ErrorMessage { get; set; }
        public Errors Error { get; set; }
        public enum Errors
        {
            IncorrectLoginOrPassword = 0,
            SameLoginExist = 1,
            ConnectionError = 2,
            EmptyField = 3,
            PasswordsNotSame = 4,
            NoErrors = 5
        }
        public AccountInfo(string login, Errors error)
        {
            Login = login;
            Error = error;
            ErrorMessage = errorMessageList[(int)error];
        }
    }
}
