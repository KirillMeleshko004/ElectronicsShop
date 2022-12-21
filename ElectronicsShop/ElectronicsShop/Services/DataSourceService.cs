using Firebase.Database;
using Firebase.Database.Query;

namespace ElectronicsShop.Services
{
    public static class DataSourceService <T>
    {
        private const string _dbURI = "https://electronicsshop-8c6b3-default-rtdb.europe-west1.firebasedatabase.app/";
        private static readonly FirebaseClient _firebaseClient = new FirebaseClient(_dbURI);

        public static async Task<IEnumerable<T>> GetDataAsync()
        {
            return (from FBObj in (await _firebaseClient
                    .Child(typeof(T).Name)
                    .OrderByKey()
                    .OnceAsync<T>())
                        select FBObj.Object)?.AsEnumerable<T>();
        }
        public static async Task<T> GetDataAsync(int identityValue, string identityName)
        {
            var data = await _firebaseClient
                .Child(typeof(T).Name)
                .OrderBy(identityName)
                .EqualTo(identityValue)
                .OnceAsync<T>();
            return data.Any() ? data.First().Object : default;
        }
        public static async Task<T> GetDataAsync(string identityValue, string identityName)
        {
            var data = await _firebaseClient
                .Child(typeof(T).Name)
                .OrderBy(identityName)
                .EqualTo(identityValue)
                .OnceAsync<T>();
            return data.Any() ? data.First().Object : default;
        }
        public static async Task<IEnumerable<T>> GetMultipleDataAsync(int identityValue, string identityName)
        {
            return (from FBObj in (await _firebaseClient
                .Child(typeof(T).Name)
                .OrderBy(identityName)
                .EqualTo(identityValue)
                .OnceAsync<T>())
                    select FBObj.Object)?.AsEnumerable<T>();
        }
        public static async Task<IEnumerable<T>> GetMultipleDataAsync(string identityValue, string identityName)
        {
            return (from FBObj in (await _firebaseClient
                .Child(typeof(T).Name)
                .OrderBy(identityName)
                .EqualTo(identityValue)
                .OnceAsync<T>()) select FBObj.Object)?.AsEnumerable<T>();
        }
        public static async Task SaveDataAsync(T data)
        {
            await _firebaseClient
                .Child(data.GetType().Name)
                .PostAsync(data);
        }
        public static async Task DeleteElementsAsync(int identityValue, string identityName)
        {
            var FBObj = (await _firebaseClient
                .Child(typeof(T).Name)
                .OrderBy(identityName)
                .EqualTo(identityValue)
                .OnceAsync<T>());
            foreach(var item in FBObj)
                await _firebaseClient
                    .Child(typeof(T).Name)
                    .Child(item.Key)
                    .DeleteAsync();
        }
        public static async Task DeleteElementsAsync(string identityValue, string identityName)
        {
            var FBObj = (await _firebaseClient
                .Child(typeof(T).Name)
                .OrderBy(identityName)
                .EqualTo(identityValue)
                .OnceAsync<T>());
            foreach (var item in FBObj)
                await _firebaseClient
                    .Child(typeof(T).Name)
                    .Child(item.Key)
                    .DeleteAsync();
        }
        public static async Task AlterSingleElementAsync(T data, int identityValue, string identityName)
        {
            var FBObj = (await _firebaseClient
                .Child(data.GetType().Name)
                .OrderBy(identityName)
                .EqualTo(identityValue)
                .OnceAsync<T>()).First();

            await _firebaseClient
                .Child(data.GetType().Name)
                .Child(FBObj.Key)
                .PatchAsync(data);
        }
        public static async Task AlterSingleElementAsync(T data, string identityValue, string identityName)
        {
            var FBObj = (await _firebaseClient
                .Child(data.GetType().Name)
                .OrderBy(identityName)
                .EqualTo(identityValue)
                .OnceAsync<T>());
            if(FBObj.Count is 0)
            {
                await SaveDataAsync(data);
                return;
            }
            await _firebaseClient
                .Child(data.GetType().Name)
                .Child(FBObj.First().Key)
                .PatchAsync(data);
        }
    }
}
