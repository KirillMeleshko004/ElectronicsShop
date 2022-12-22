using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ElectronicsShop.ViewModels.AdminViewModels
{
    public partial class CategoryChangingViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<string> _categories;


        [ObservableProperty]
        string _categoryName;
        [ObservableProperty]
        string _prevSelection;

        [ObservableProperty]
        string _imageURI;


        [ObservableProperty]
        bool _isChanged = false;

        private FileResult _image = null;
        private readonly CategoryService _categoryService;

        private Category _selection;

        public CategoryChangingViewModel(CategoryService categoryService)
        {
            _categoryService = categoryService;


            GetCategories();

            PropertyChanged += SelectionChanged;
            PropertyChanged += FieldChanged;
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
            if (e.PropertyName != nameof(PrevSelection)) return;

            CategoryName = PrevSelection;
            _selection = await _categoryService.GetCategoryInfo(CategoryName);
            ImageURI = _selection.ImageURI;
            PrevSelection = CategoryName;
        }
        void FieldChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(CategoryName) && e.PropertyName != nameof(ImageURI)) return;
            if (_selection is null) return;
            IsChanged = (CategoryName != _selection.CategoryName || ImageURI != _selection.ImageURI);
        }

        [RelayCommand]
        public async Task PickImage()
        {
            _image = await MediaPicker.PickPhotoAsync();
            if (_image is null) return;
            ImageURI = _image.FullPath;
        }


        [RelayCommand]
        public async Task Confirm()
        {
            IsBusy = true;
            try
            {
                await _categoryService.ChangeCategory(PrevSelection, CategoryName, _image);
                await Shell.Current.DisplayAlert("Success", "Category changed", "Ok");
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
