using System;
using System.Net;
using System.Text.Json;

namespace ElectronicsShop.Models
{
    public class TempServer
    {
        static string path = FileSystem.Current.AppDataDirectory;
        static string fullPath = Path.Combine(path, "UserData.json");

        static Dictionary<string, string> accounts;
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
            using (FileStream fs = new(fullPath, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize<Dictionary<string, string>>(fs, accounts);
            }
        }
        static TempServer()
        {
            WriteAccounts(accounts);
            accounts = ReadAccounts();
        }
        public async static Task<AccountInfo> AuthorizationAsync(string login, string password)
        {
            await Task.Delay(3000);
            if (login == null || password == null || login == "" || password == "") return new AccountInfo(login, AccountInfo.Errors.EmptyField);
            if (!accounts.ContainsKey(login) || accounts[login] != password) return new AccountInfo(login, AccountInfo.Errors.IncorrectLoginOrPassword);
            return new AccountInfo(login, AccountInfo.Errors.NoErrors);
        }
        public async static Task<AccountInfo> CreateAccountAsync(string login, string password, string repeatPassword)
        {
            await Task.Delay(3000);
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatPassword)) 
                return new AccountInfo(login, AccountInfo.Errors.EmptyField);
            if (accounts.ContainsKey(login)) new AccountInfo(login, AccountInfo.Errors.SameLoginExist);
            if (password != repeatPassword) return new AccountInfo(login, AccountInfo.Errors.PasswordsNotSame);
            accounts.Add(login, password);
            WriteAccounts(accounts);
            return new AccountInfo(login, AccountInfo.Errors.NoErrors);
        }
    }
}
