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
        public async Task AddProductToCartAsync(string userName, CartProduct product)
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
                CartChanged?.Invoke(this, new CartEventArgs(product));
                return;
            }

            if (cartForUser.Object.Products.Contains(product))
                cartForUser.Object.Products[cartForUser.Object.Products.IndexOf(product)].Quantity++;
            else
                cartForUser.Object.Products.Add(product);

            await _firebaseClient.Child(nameof(Cart))
                    .Child(cartForUser.Key)
                    .PatchAsync(cartForUser.Object);

            product.Quantity = cartForUser.Object.Products[cartForUser.Object.Products.IndexOf(product)].Quantity;
            CartChanged?.Invoke(this, new CartEventArgs(product));
        }
        public async Task RemoveProductFromCartAsync(string userName, CartProduct product)
        {
            product.Quantity--;

            var cartForUser = (await _firebaseClient
                .Child(nameof(Cart))
                .OrderBy(nameof(Cart.UserName))
                .EqualTo(userName)
                .OnceAsync<Cart>())
                .FirstOrDefault((FirebaseObject<Cart>)null);

            if (product.Quantity == 0)
            {
                await _firebaseClient
                .Child(nameof(Cart))
                .Child(cartForUser.Key)
                .DeleteAsync();

                CartChanged?.Invoke(this, new CartEventArgs(product));
                return;
            }

            cartForUser.Object.Products[cartForUser.Object.Products.IndexOf(product)].Quantity--;
            await _firebaseClient
                .Child(nameof(Cart))
                .Child(cartForUser.Key)
                .PatchAsync(cartForUser.Object);

            CartChanged?.Invoke(this, new CartEventArgs(product));
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
            CartChanged?.Invoke(this, new CartEventArgs(null));
        }
        
        public async Task<bool> IsProductInCartOfUser(string userName, Product product)
        {
            return (await _firebaseClient
                .Child(nameof(Cart))
                .OrderBy(nameof(Cart.UserName))
                .EqualTo(userName)
                .OnceAsync<Cart>()).Count() != 0;
        }

        public event EventHandler<CartEventArgs> CartChanged;
    }
}
