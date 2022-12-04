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
            return (from fbObj in (
                    await _firebaseClient
                    .Child(nameof(Product))
                    .OrderByKey()
                    .OnceAsync<Product>()
                    ) select fbObj.Object).ToList<Product>();
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

            ProductChanged?.Invoke(this, new ProductEventArgs(product, ProductEventArgs.Actions.added));
        }
        public async Task DeleteProductAsync(Product product)
        {
            string prodKey = (await _firebaseClient
                .Child(nameof(Product))
                .OrderBy(nameof(Product.Id))
                .EqualTo(product.Id)
                .OnceAsync<Product>()).First().Key;

            await _firebaseClient
                .Child(nameof(Product))
                .Child(prodKey)
                .DeleteAsync(); 
            

            await _firebaseStorage.Child(System.IO.Path.GetFileName(new Uri(product.ImageString).LocalPath))
                .DeleteAsync();

            ProductChanged?.Invoke(this, new ProductEventArgs(product, ProductEventArgs.Actions.removed));
        }
        public async Task ChangeProductAsync(Product product, FileResult productImage)
        {
            if (productImage != null)
            {
                Stream imageToUpload = await productImage.OpenReadAsync();

                await _firebaseStorage
                    .Child($"{productImage.FileName}")
                    .PutAsync(imageToUpload);

                string imageURI = await _firebaseStorage
                    .Child($"{productImage.FileName}")
                    .GetDownloadUrlAsync();

                product.ImageString = imageURI;
            }

            var coll = (await _firebaseClient
                .Child(nameof(Product))
                .OrderBy(nameof(Product.Id))
                .EqualTo(product.Id)
                .OnceAsync<Product>());
            string productKey = 
                coll.First().Key;

            await _firebaseClient.Child(nameof(Product)).Child(productKey).PatchAsync(product);

            ProductChanged?.Invoke(this, new ProductEventArgs(product, ProductEventArgs.Actions.changed));
        }

        public event EventHandler<ProductEventArgs> ProductChanged;
    }
}
