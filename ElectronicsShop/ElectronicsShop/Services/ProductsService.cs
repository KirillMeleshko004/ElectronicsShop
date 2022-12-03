using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;

namespace ElectronicsShop.Services
{
    public class ProductsService
    {
        private const string _dbURI = "https://electronicsshop-8c6b3-default-rtdb.europe-west1.firebasedatabase.app/";
        private const string _storageURI = "electronicsshop-8c6b3.appspot.com";
        private readonly FirebaseClient _firebaseClient = new FirebaseClient(_dbURI);
        private readonly FirebaseStorage _firebaseStorage = new FirebaseStorage(_storageURI);
        public async Task<List<Product>> GetProductsAsync()
        {
            return await TempServer.GetProductsAsync();
        }
        public async Task AddProductAsync(Product product, FileResult productImage)
        {
            Stream imageToUpload = await productImage.OpenReadAsync();

            await _firebaseStorage
                .Child($"{productImage.FileName}")
                .PutAsync(imageToUpload);

            string imageURI = await _firebaseStorage
                .Child($"{productImage.FileName}")
                .GetDownloadUrlAsync();

            int count = (await _firebaseClient
               .Child(nameof(Product))
               .OnceAsync<UserData>()).Count;

            product.ImageString = imageURI;
            product.Id = count + 1;

            await _firebaseClient.Child(nameof(Product)).PostAsync(product);
        }
        public async Task DeleteProductAsync(Product product)
        {
            await _firebaseClient
                .Child(nameof(Product))
                .Child(nameof(Product.Id))
                .OrderByKey()
                .EqualTo(product.Id)
                .DeleteAsync();

            await _firebaseStorage.Child(product.ImageString)
                .DeleteAsync();
        }
    }
}
