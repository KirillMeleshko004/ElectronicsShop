using System.Collections.ObjectModel;

namespace ElectronicsShop.SearchHandlers
{
    public class ProductSearchHandler : SearchHandler
    {
        //public IEnumerable<Product> Products { get; set; }
        public static readonly BindableProperty ProductsProperty = BindableProperty.Create(
            nameof(Products),
            typeof(ObservableCollection<Product>),
            typeof(ProductSearchHandler),
            propertyChanged: ProductsChanged);
        public ObservableCollection<Product> Products
        {
            get => (ObservableCollection<Product>)GetValue(ProductsProperty);
            set => SetValue(ProductsProperty, value);
        }
        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            if (Products is null) return;
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrEmpty(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = (from product in Products
                               where product.ProductName.ToLower().Contains(newValue.ToLower())
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

        static void ProductsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ProductSearchHandler searchHandler = (ProductSearchHandler)bindable;
            searchHandler.Products = (ObservableCollection<Product>)newValue;
        }

    }
}
