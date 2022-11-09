using System;
using System.Net;
using System.Text.Json;

namespace ElectronicsShop.Models
{
    public class TempServer
    {
        static string path = FileSystem.Current.AppDataDirectory;
        static string fullPath = Path.Combine(path, "UserData.json");

        static Dictionary<string, string> _accounts;
        static Dictionary<string, string> ReadAccounts()
        {
            Dictionary<string, string> accounts;
            using (FileStream fs = new(fullPath, FileMode.OpenOrCreate))
            {
                try
                {
                    accounts = JsonSerializer.Deserialize<Dictionary<string, string>>(fs);
                }
                catch (Exception)
                {
                    accounts = new Dictionary<string, string>();
                }
            }
            return accounts;
        }
        static void WriteAccounts(Dictionary<string, string> accounts)
        {
            using (FileStream fs = new(fullPath, FileMode.Create))
            {
                JsonSerializer.Serialize<Dictionary<string, string>>(fs, accounts);
            }
        }
        static TempServer()
        {
            _accounts = ReadAccounts();
        }
        public async static Task<AccountInfo> AuthorizationAsync(string login, string password)
        {
            await Task.Delay(500);
            if (login == null || password == null || login == "" || password == "") return new AccountInfo(login, 
                AccountErrorMessages.FIELDS_EMPTY);
            if (!_accounts.ContainsKey(login) || _accounts[login] != password) return new AccountInfo(login,
                AccountErrorMessages.INCORRECT_LOGIN_OR_PASSWORD);
            return new AccountInfo(login, AccountErrorMessages.SUCCESS);
        }
        public async static Task<AccountInfo> CreateAccountAsync(string login, string password, string repeatPassword)
        {
            await Task.Delay(3000);
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatPassword)) 
                return new AccountInfo(login, AccountErrorMessages.FIELDS_EMPTY);
            if (_accounts.ContainsKey(login)) return new AccountInfo(login, AccountErrorMessages.SAME_LOGIN_EXIST);
            if (password != repeatPassword) return new AccountInfo(login, AccountErrorMessages.PASSWORDS_NOT_SAME);
            _accounts.Add(login, password);
            WriteAccounts(_accounts);
            return new AccountInfo(login, AccountErrorMessages.SUCCESS);
        }
    }
}
