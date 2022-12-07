using Firebase.Database;
using Firebase.Database.Query;

namespace ElectronicsShop.Services
{
    public class CartService
    {
        private const string _dbURI = "https://electronicsshop-8c6b3-default-rtdb.europe-west1.firebasedatabase.app/";
        private readonly FirebaseClient _firebaseClient = new FirebaseClient(_dbURI);

        public async Task<List<CartProduct>> GetCartForUserAsync(string userName)
        {
            Cart cart = (await _firebaseClient
                .Child(nameof(Cart))
                .OrderBy(nameof(Cart.UserName))
                .EqualTo(userName)
                .OnceAsync<Cart>())
                .FirstOrDefault((FirebaseObject<Cart>)null)?
                .Object;

            if (cart is null) return new List<CartProduct>();
            
            return cart.Products;
        }
        public async Task<CartProduct> AddProductToCartAsync(string userName, CartProduct product)
        {
            product.Quantity++;

            var cartForUser = (await _firebaseClient
                .Child(nameof(Cart))
                .OrderBy(nameof(Cart.UserName))
                .EqualTo(userName)
                .OnceAsync<Cart>())
                .FirstOrDefault((FirebaseObject<Cart>)null);

            if (cartForUser is null)
            {
                await _firebaseClient.Child(nameof(Cart)).PostAsync(new Cart
                {
                    UserName = userName,
                    Products = new List<CartProduct> { product },
                });
                return product;
            }

            if(cartForUser.Object.Products is null)
                cartForUser.Object.Products = new List<CartProduct>();

            if (cartForUser.Object.Products.Contains(product))
                cartForUser.Object.Products[cartForUser.Object.Products.IndexOf(product)].Quantity++;
            else
                cartForUser.Object.Products.Add(product);

            await _firebaseClient.Child(nameof(Cart))
                    .Child(cartForUser.Key)
                    .PatchAsync(cartForUser.Object);

            return product;
        }
        public async Task FullRemoveProductAsync(string userName, Product product)
        {
            var cartForUser = (await _firebaseClient
               .Child(nameof(Cart))
               .OrderBy(nameof(Cart.UserName))
               .EqualTo(userName)
               .OnceAsync<Cart>())
               .FirstOrDefault((FirebaseObject<Cart>)null);

            if(cartForUser is not null)
            {
                cartForUser.Object.Products.Remove(new CartProduct(product));

                await _firebaseClient
                    .Child(nameof(Cart))
                    .Child(cartForUser.Key)
                    .PatchAsync(cartForUser.Object);
            }
        }
        public async Task<CartProduct> RemoveProductFromCartAsync(string userName, CartProduct product)
        {
            product.Quantity--;

            var cartForUser = (await _firebaseClient
                .Child(nameof(Cart))
                .OrderBy(nameof(Cart.UserName))
                .EqualTo(userName)
                .OnceAsync<Cart>())
                .FirstOrDefault((FirebaseObject<Cart>)null);

            if (product.Quantity == 0)
                cartForUser.Object.Products.Remove(product);
            else
                cartForUser.Object.Products[cartForUser.Object.Products.IndexOf(product)].Quantity--;

            await _firebaseClient
                .Child(nameof(Cart))
                .Child(cartForUser.Key)
                .PatchAsync(cartForUser.Object);

            return product;
        }
        public async Task ClearCartAsync(string userName)
        {
            var cartForUser = (await _firebaseClient
                .Child(nameof(Cart))
                .OrderBy(nameof(Cart.UserName))
                .EqualTo(userName)
                .OnceAsync<Cart>())
                .FirstOrDefault((FirebaseObject<Cart>)null);
            await _firebaseClient
                .Child(nameof(Cart))
                .Child(cartForUser.Key)
                .DeleteAsync();
        }
        
        public async Task<bool> IsProductInCartOfUser(string userName, Product product)
        {
            var cart = (await _firebaseClient
                .Child(nameof(Cart))
                .OrderBy(nameof(Cart.UserName))
                .EqualTo(userName)
                .OnceAsync<Cart>())
                .FirstOrDefault((FirebaseObject<Cart>)null); ;
            if (cart is null || cart.Object.Products is null) return false;
            return (from cartProduct in cart.Object.Products where cartProduct.Id == product.Id select cartProduct).Any();
        }
    }
}
