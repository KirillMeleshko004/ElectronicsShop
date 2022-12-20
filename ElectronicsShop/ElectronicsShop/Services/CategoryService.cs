namespace ElectronicsShop.Services
{
    public class CategoryService
    {
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await DataSourceService<Category>.GetDataAsync();
        }
        public async Task<Category> GetCategoryInfo(string categoryName)
        {
            return await DataSourceService<Category>.GetDataAsync(categoryName, nameof(Category.CategoryName));
        }
        public async Task<bool> AddCategory(string name, FileResult categoryImage)
        {
            if ((from categ in (await DataSourceService<Category>.GetDataAsync()) where categ.CategoryName == name select categ).Any())
                return false;

            Category category = new()
            {
                ImageURI = await ImageSourceService<Category>.SaveImageAndGetURIAsync(categoryImage),
                CategoryName = name
            };

            await DataSourceService<Category>.SaveDataAsync(category);
            CategoryChanged?.Invoke(this, new EventArgs());

            return true;
        }
        public async Task ChangeCategory(string oldName, string newName, FileResult newCategoryImage)
        {
            string newImageURI = null;
            if (newCategoryImage != null)
                newImageURI = await ImageSourceService<Category>.SaveImageAndGetURIAsync(newCategoryImage);

            Category oldCategory = await DataSourceService<Category>.GetDataAsync(oldName, nameof(Category.CategoryName));

            if (oldName != newName)
                oldCategory.CategoryName = newName;
            if (newImageURI != null)
            {
                if (await ImageSourceService<Category>.CountImageUsings(oldCategory.ImageURI) == 1)
                    await ImageSourceService<Category>.DeleteImageAsync(oldCategory.ImageURI);
                oldCategory.ImageURI = newImageURI;
            }

            await DataSourceService<Category>.AlterSingleElementAsync(oldCategory, oldName, nameof(Category.CategoryName));
            CategoryChanged?.Invoke(this, new EventArgs());
        }
        public async Task RemoveCategory(Category category)
        {
            await DataSourceService<Product>.DeleteElementAsync(category.CategoryName, nameof(Product.ProductCategory));
            if (await ImageSourceService<Category>.CountImageUsings(category.ImageURI) == 1)
                await ImageSourceService<Category>.DeleteImageAsync(category.ImageURI);
            await DataSourceService<Category>.DeleteElementAsync(category.CategoryName, nameof(Category.CategoryName));
            CategoryChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler CategoryChanged;
    }
}
