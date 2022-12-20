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
            Categories = (from category in await _categoryService.GetCategories() select category.CategoryName)?.ToObservableCollection();
        }

        async void SelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(CategoryName)) return;
            ImageURI = (await _categoryService.GetCategoryInfo(CategoryName)).ImageURI;
            IsSelected = true;
        }


        [RelayCommand]
        public async Task Delete()
        {
            IsBusy = true;
            await _categoryService.RemoveCategory(await _categoryService.GetCategoryInfo(CategoryName));
            await Shell.Current.DisplayAlert("Success", "Category changed", "Ok");
            await Shell.Current.GoToAsync("..");
            IsBusy = false;
        }
    }
}
