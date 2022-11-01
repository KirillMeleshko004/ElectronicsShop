namespace ElectronicsShop.Models
{
    public class Account
    {
        public string Login { get; set; }

        public Account(string login)
        {
            Login = login;
        }
        public Account() { }
    }
}
