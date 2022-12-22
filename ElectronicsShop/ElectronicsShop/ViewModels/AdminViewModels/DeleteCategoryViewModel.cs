using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ElectronicsShop.ViewModels.AdminViewModels
{
    public partial class DeleteCategoryViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<string> _categories;


        [ObservableProperty]
        string _categoryName;

        [ObservableProperty]
        string _imageURI;


        [ObservableProperty]
        bool _isSelected = false;

        private readonly CategoryService _categoryService;

        public DeleteCategoryViewModel(CategoryService categoryService)
        {
            _categoryService = categoryService;


            GetCategories();

            PropertyChanged += SelectionChanged;
        }

        public async void GetCategories()
        {
            IsBusy = true;
            try
            {
                Categories = (from category in await _categoryService.GetCategories() select category.CategoryName)?.ToObservableCollection();
            }
            catch
            {
                ConnectionErrorView.ShowErrorMessage();
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void SelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            IsBusy = true;
            try
            {
                if (e.PropertyName != nameof(CategoryName)) return;
                ImageURI = (await _categoryService.GetCategoryInfo(CategoryName)).ImageURI;
                IsSelected = true;
            }
            catch
            {
                ConnectionErrorView.ShowErrorMessage();
            }
            finally
            {
                IsBusy = false;
            }
        }


        [RelayCommand]
        public async Task Delete()
        {
            IsBusy = true;
            try
            {
                await _categoryService.RemoveCategory(await _categoryService.GetCategoryInfo(CategoryName));
                await Shell.Current.DisplayAlert("Success", "Category deleted", "Ok");
                await Shell.Current.GoToAsync("..");
            }
            catch
            {
                ConnectionErrorView.ShowErrorMessage();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
