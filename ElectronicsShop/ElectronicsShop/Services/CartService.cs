namespace ElectronicsShop.Services
{
    public class CartService
    {
        public async Task<List<CartProduct>> GetCartForUserAsync(string userName)
        {
            Cart cart = await DataSourceService<Cart>.GetDataAsync(userName, nameof(Cart.UserName));
            return cart is null ? new List<CartProduct>() : cart.Products;
        }
        public async Task<CartProduct> AddProductToCartAsync(string userName, CartProduct product)
        {
            product.Quantity++;

            Cart cart = await DataSourceService<Cart>.GetDataAsync(userName, nameof(Cart.UserName));

            if (cart is null)
            {
                await DataSourceService<Cart>.SaveDataAsync(new Cart
                {
                    UserName = userName,
                    Products = new List<CartProduct> { product },
                });

                return product;
            }

            cart.Products ??= new List<CartProduct>();

            if (cart.Products.Contains(product))
                cart.Products[cart.Products.IndexOf(product)].Quantity++;
            else
                cart.Products.Add(product);

            await DataSourceService<Cart>.AlterSingleElementAsync(cart, userName, nameof(Cart.UserName));

            return product;
        }
        public async Task FullRemoveProductAsync(string userName, Product product)
        {
            Cart cart = await DataSourceService<Cart>.GetDataAsync(userName, nameof(Cart.UserName));

            if (cart is not null)
            {
                cart.Products.Remove(new CartProduct(product));
                await DataSourceService<Cart>.AlterSingleElementAsync(cart, userName, nameof(Cart.UserName));
            }
        }
        public async Task<CartProduct> RemoveProductFromCartAsync(string userName, CartProduct product)
        {
            product.Quantity--;

            Cart cart = await DataSourceService<Cart>.GetDataAsync(userName, nameof(Cart.UserName));

            if (product.Quantity is 0)
                cart.Products.Remove(product);
            else
                cart.Products[cart.Products.IndexOf(product)].Quantity--;

            await DataSourceService<Cart>.AlterSingleElementAsync(cart, userName, nameof(Cart.UserName));

            return product;
        }
        public async Task ClearCartAsync(string userName)
        {
            await DataSourceService<Cart>.DeleteElementAsync(userName, nameof(Cart.UserName));
        }
        
        public async Task<bool> IsProductInCartOfUser(string userName, Product product)
        {
            Cart cart = await DataSourceService<Cart>.GetDataAsync(userName, nameof(Cart.UserName));

            if (cart is null || cart.Products is null) return false;
            return (from cartProduct in cart.Products where cartProduct.Id == product.Id select cartProduct).Any();
        }
    }
}
