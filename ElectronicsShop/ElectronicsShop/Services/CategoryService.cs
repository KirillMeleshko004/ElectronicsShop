namespace ElectronicsShop.Services
{
    public class CategoryService
    {
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await DataSourceService<Category>.GetDataAsync();
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
        public async Task RemoveCategory(string name)
        {
            await DataSourceService<Category>.DeleteElementAsync(name, nameof(Category.CategoryName));
        }

        public event EventHandler CategoryChanged;
    }
}
