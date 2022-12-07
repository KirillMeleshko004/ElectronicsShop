using static ElectronicsShop.GlobalConst.AccountErrorMessages;
namespace ElectronicsShop.Services.AccountServices
{
    public static class AuthDataChecker
    {
        private const int MIN_LOGIN_LENGTH = 8;
        private const int MIN_PASSWORD_LENGTH = 8;

        public static string CheckRegData(string login, string password, string repeatPassword)
        {
            if (AreFieldsEmpty(login, password, repeatPassword))
                return FIELDS_EMPTY;
            if (!IsLoginLengthEnough(login) || !IsPasswordLengthEnough(password))
                return LOGIN_OR_PASSWORD_TOO_SHORT;
            if (password != repeatPassword)
                return PASSWORDS_NOT_SAME;
            return SUCCESS;
        }
        public static string CheckAuthData(string login, string password)
        {
            if (AreFieldsEmpty(login, password))
                return FIELDS_EMPTY;
            if (!IsLoginLengthEnough(login) || !IsPasswordLengthEnough(password))
                return LOGIN_OR_PASSWORD_TOO_SHORT;
            return SUCCESS;
        }
        public static string CheckPasswordChangeData(string oldPassword, string newPassword, string repeatPassword)
        {
            if (AreFieldsEmpty(oldPassword, newPassword, repeatPassword))
                return FIELDS_EMPTY;
            if (!IsPasswordLengthEnough(oldPassword) || !IsPasswordLengthEnough(newPassword) || !IsPasswordLengthEnough(repeatPassword))
                return LOGIN_OR_PASSWORD_TOO_SHORT;
            if (newPassword != repeatPassword)
                return PASSWORDS_NOT_SAME;
            return SUCCESS;
        }
        public static bool AreFieldsEmpty(params string[] strings)
        {
            foreach (string str in strings)
                if (string.IsNullOrEmpty(str))
                    return true;
            return false;
        }
        private static bool IsLoginLengthEnough(string login)
        {
            return login.Length >= MIN_LOGIN_LENGTH;
        }
        private static bool IsPasswordLengthEnough(string password)
        {
            return password.Length >= MIN_PASSWORD_LENGTH;
        }
    }
}
