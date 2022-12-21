using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;

namespace ElectronicsShop.Services
{
    public static class ImageSourceService<T> where T : IImageURIContainer
    {
        private const string _dbURI = "https://electronicsshop-8c6b3-default-rtdb.europe-west1.firebasedatabase.app/";
        private const string _storageURI = "electronicsshop-8c6b3.appspot.com";
        private static readonly FirebaseClient _firebaseClient = new FirebaseClient(_dbURI);
        private static readonly FirebaseStorage _firebaseStorage = new FirebaseStorage(_storageURI);
        public static async Task<string> SaveImageAndGetURIAsync(FileResult file)
        {
            Stream imageToUpload = await file.OpenReadAsync();

            await _firebaseStorage
                .Child(typeof(T).Name)
                .Child($"{file.FileName}")
                .PutAsync(imageToUpload);

            return await _firebaseStorage
                .Child(typeof(T).Name)
                .Child($"{file.FileName}")
                .GetDownloadUrlAsync();
        }
        public static async Task DeleteImageAsync(string imageURI)
        {
            await _firebaseStorage
                .Child(typeof(T).Name)
                .Child(System.IO.Path.GetFileName(new Uri(imageURI).LocalPath))
                .DeleteAsync();
        }
        public static async Task<int> CountImageUsings(string imageURI)
        {
            IEnumerable<IImageURIContainer> elements = (IEnumerable<IImageURIContainer>)
                (from fbObj in await _firebaseClient.Child(typeof(T).Name)
                .OnceAsync<T>() select fbObj.Object);
            int count = 0;
            foreach (IImageURIContainer el in elements)
                if ((System.IO.Path.GetFileName(new Uri(el.ImageURI).LocalPath)) == (System.IO.Path.GetFileName(new Uri(imageURI).LocalPath))) count++;

            return count;
        }
    }
}
