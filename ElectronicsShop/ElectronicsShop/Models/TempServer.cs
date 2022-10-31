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
            accounts = ReadAccounts();
        }
        public async static Task<bool> AuthorizationAsync(string login, string password)
        {
            await Task.Delay(3000);
            if (login == null || password == null) return false;
            if (!accounts.ContainsKey(login)) return false;
            if (accounts[login]!=password) return false;
            return true;
        }
        public async static Task<bool> CheckLogin(string login)
        {
            await Task.Delay(3000);
            if (login == null) return false;
            return accounts.ContainsKey(login) ? false : true;
        }
        public async static Task<bool> CreateAccount(string login, string password)
        {
            await Task.Delay(3000);
            accounts.Add(login, password);
            WriteAccounts(accounts);
            return true;
        }
    }
}
