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
            if (!NetworkCheckerService.CheckConnection())
            {
                NetworkCheckerService.ShowNewtworkErrorMessage();
                return;
            }
            Categories = (from category in await _categoryService.GetCategories() select category.CategoryName)?.ToObservableCollection();
        }

        async void SelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!NetworkCheckerService.CheckConnection())
            {
                NetworkCheckerService.ShowNewtworkErrorMessage();
                return;
            }
            ImageURI = (await _categoryService.GetCategoryInfo(CategoryName)).ImageURI;
            IsSelected = true;
        }


        [RelayCommand]
        public async Task Delete()
        {
            if (!NetworkCheckerService.CheckConnection())
            {
                NetworkCheckerService.ShowNewtworkErrorMessage();
                return;
            }

            await _categoryService.RemoveCategory(await _categoryService.GetCategoryInfo(CategoryName));
            await Shell.Current.DisplayAlert("Success", "Category deleted", "Ok");
            await Shell.Current.GoToAsync("..");
        }
    }
}
