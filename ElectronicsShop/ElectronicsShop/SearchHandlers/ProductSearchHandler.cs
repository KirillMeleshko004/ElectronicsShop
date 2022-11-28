using ElectronicsShop.Models;

using System.Collections.ObjectModel;

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
            Product currentProduct = (Product)item;

            await Shell.Current.GoToAsync($"{nameof(ProductDetailsView)}",
                new Dictionary<string, object>
                {
                    ["CurrentProduct"] = currentProduct
                });
        }

        protected override void OnQueryConfirmed()
        {
            if (string.IsNullOrEmpty(Query)) return;
            ObservableCollection<Product> searchResults = new((IEnumerable<Product>)this.ItemsSource);
            Shell.Current.GoToAsync($"{nameof(ProductsListView)}",
            new Dictionary<string, object>
                {
                    ["Title"] = this.Query,
                    ["Products"] = searchResults
            });

        }
        
    }
}
