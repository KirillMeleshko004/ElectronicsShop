using ElectronicsShop.Models;

namespace ElectronicsShop.Services
{
    public class ProductsService
    {
        public async Task<List<Product>> GetProductsAsync()
        {
            return (await DataSourceService<Product>.GetDataAsync())?.ToList<Product>();
        }
        public async Task AddProductAsync(Product product, FileResult productImage)
        {
            product.ImageURI = await ImageSourceService<Product>.SaveImageAndGetURIAsync(productImage);
            var products = await GetProductsAsync() ?? new List<Product>();
            int newId = products.Count == 0 ? 1 : (from prod in products select prod.Id).Max() + 1;
            product.Id = newId;

            await DataSourceService<Product>.SaveDataAsync(product);

            ProductChanged?.Invoke(this, new ProductEventArgs(product, ProductEventArgs.Actions.added));
        }
        public async Task DeleteProductAsync(Product product)
        {
            await DataSourceService<Product>.DeleteElementsAsync(product.Id, nameof(Product.Id));

            if (await ImageDeletionService.ShouldDelete(product))
                await ImageSourceService<Product>.DeleteImageAsync(product.ImageURI);

            await DataSourceService<Product>.DeleteElementsAsync(product.Id, nameof(Product.Id));

            ProductChanged?.Invoke(this, new ProductEventArgs(product, ProductEventArgs.Actions.removed));
        }
        public async Task ChangeProductAsync(Product product, FileResult productImage)
        {
            if (productImage != null)
                product.ImageURI = await ImageSourceService<Product>.SaveImageAndGetURIAsync(productImage);

            await DataSourceService<Product>.AlterSingleElementAsync(product, product.Id, nameof(Product.Id));

            ProductChanged?.Invoke(this, new ProductEventArgs(product, ProductEventArgs.Actions.changed));
        }

        public event EventHandler<ProductEventArgs> ProductChanged;
    }
}
