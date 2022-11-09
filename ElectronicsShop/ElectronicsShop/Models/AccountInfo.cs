namespace ElectronicsShop.Models
{
    public class AccountInfo
    {
        public string Login { get; set; }
        public string ErrorMessage { get; set; }
        public AccountInfo(string login, string error)
        {
            Login = login;
            ErrorMessage = error;
        }
    }
}
