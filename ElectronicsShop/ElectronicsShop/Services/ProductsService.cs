using Firebase.Database;
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
        }
    }
}
