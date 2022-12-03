namespace ElectronicsShop.Models
{
    public class AccountInfo
    {
        public string Login { get; set; }
        public string Role { get; set; }
        public string ErrorMessage { get; set; }
        public AccountInfo(string login, string role, string error)
        {
            Login = login;
            Role = role;
            ErrorMessage = error;
        }
    }
}
