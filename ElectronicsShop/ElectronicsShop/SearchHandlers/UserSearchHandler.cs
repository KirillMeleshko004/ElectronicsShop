using System.Collections.ObjectModel;

namespace ElectronicsShop.SearchHandlers
{
    public class UserSearchHandler : SearchHandler
    {
        public static readonly BindableProperty UsersProperty = BindableProperty.Create(
            nameof(Users),
            typeof(ObservableCollection<User>),
            typeof(ProductSearchHandler),
            propertyChanged: ProductsChanged);
        public ObservableCollection<User> Users
        {
            get => (ObservableCollection<User>)GetValue(UsersProperty);
            set => SetValue(UsersProperty, value);
        }
        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            if (Users is null) return;
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrEmpty(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = (from user in Users
                               where user.UID.ToString().ToLower().Contains(newValue.ToLower())
                               select user).ToList();
            }
        }
        protected override async void OnItemSelected(object item)
        {
            User currentUser = (User)item;

            await Shell.Current.GoToAsync($"{nameof(UserDetailsView)}",
                new Dictionary<string, object>
                {
                    ["User"] = currentUser
                });
        }

        static void ProductsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            UserSearchHandler searchHandler = (UserSearchHandler)bindable;
            searchHandler.Users = (ObservableCollection<User>)newValue;
        }

    }
}