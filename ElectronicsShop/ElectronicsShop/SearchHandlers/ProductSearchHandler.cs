using ElectronicsShop.Views;

namespace ElectronicsShop.SearchHandlers
{
    public class ProductSearchHandler : SearchHandler
    {
        public IList<Product> Products { get; set; }
        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if(string.IsNullOrEmpty(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = (from product in Products where product.ProductName.ToLower().Contains(newValue.ToLower()) 
                               select product).ToList();
            }
        }
        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            Product currentProduct = (Product)item;

            await Shell.Current.GoToAsync($"{nameof(ProductView)}",
                new Dictionary<string, object>
                {
                    ["CurrentProduct"] = currentProduct
                });
        }
    }
}
