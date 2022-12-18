﻿namespace ElectronicsShop.Services
{
    public class ProductsService
    {
        public async Task<List<Product>> GetProductsAsync()
        {
            return (await DataSourceService<Product>.GetDataAsync()).ToList<Product>();
        }
        public async Task AddProductAsync(Product product, FileResult productImage)
        {
            product.ImageString = await DataSourceService<Product>.SaveFileAndGetURIAsync(productImage);
            var products = await GetProductsAsync();
            int newId = (from prod in products select prod.Id)?.Max() + 1 ?? 1;
            product.Id = newId;

            await DataSourceService<Product>.SaveDataAsync(product);

            ProductChanged?.Invoke(this, new ProductEventArgs(product, ProductEventArgs.Actions.added));
        }
        public async Task DeleteProductAsync(Product product)
        {
            await DataSourceService<Product>.DeleteFileAsync(product.ImageString);

            await DataSourceService<Product>.DeleteElementAsync(product.Id, nameof(Product.Id));

            ProductChanged?.Invoke(this, new ProductEventArgs(product, ProductEventArgs.Actions.removed));
        }
        public async Task ChangeProductAsync(Product product, FileResult productImage)
        {
            if (productImage != null)
                product.ImageString = await DataSourceService<Product>.SaveFileAndGetURIAsync(productImage);

            await DataSourceService<Product>.AlterSingleElementAsync(product, product.Id, nameof(Product.Id));

            ProductChanged?.Invoke(this, new ProductEventArgs(product, ProductEventArgs.Actions.changed));
        }

        public event EventHandler<ProductEventArgs> ProductChanged;
    }
}
