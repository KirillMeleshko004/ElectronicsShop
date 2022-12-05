using static ElectronicsShop.GlobalConst.AccountErrorMessages;
namespace ElectronicsShop.Services.AccountServices
{
    public static class AuthDataChecker
    {
        private const int MIN_LOGIN_LENGTH = 8;
        private const int MIN_PASSWORD_LENGTH = 8;

        public static string CheckRegData(string login, string password, string repeatPassword)
        {
            if (IsFieldEmpty(login, password, repeatPassword))
                return FIELDS_EMPTY;
            if (!IsLoginLengthEnough(login) || !IsPasswordLengthEnough(password))
                return LOGIN_OR_PASSWORD_TOO_SHORT;
            if (password != repeatPassword)
                return PASSWORDS_NOT_SAME;
            return SUCCESS;
        }
        public static string CheckAuthData(string login, string password)
        {
            if (IsFieldEmpty(login, password))
                return FIELDS_EMPTY;
            if (!IsLoginLengthEnough(login) || !IsPasswordLengthEnough(password))
                return LOGIN_OR_PASSWORD_TOO_SHORT;
            return SUCCESS;
        }
        public static string CheckPasswordChangeData(string oldPassword, string newPassword, string repeatPassword)
        {
            if (IsFieldEmpty(oldPassword, newPassword, repeatPassword))
                return FIELDS_EMPTY;
            if (!IsPasswordLengthEnough(oldPassword) || !IsPasswordLengthEnough(newPassword) || !IsPasswordLengthEnough(repeatPassword))
                return LOGIN_OR_PASSWORD_TOO_SHORT;
            if (newPassword != repeatPassword)
                return PASSWORDS_NOT_SAME;
            return SUCCESS;
        }
        private static bool IsLoginLengthEnough(string login)
        {
            return login.Length >= MIN_LOGIN_LENGTH;
        }
        private static bool IsPasswordLengthEnough(string password)
        {
            return password.Length >= MIN_PASSWORD_LENGTH;
        }
        private static bool IsFieldEmpty(string first, string second, string third)
        {
            return string.IsNullOrEmpty(first) || string.IsNullOrEmpty(second) || string.IsNullOrEmpty(third);
        }
        private static bool IsFieldEmpty(string first, string second)
        {
            return string.IsNullOrEmpty(first) || string.IsNullOrEmpty(second);
        }
    }
}
