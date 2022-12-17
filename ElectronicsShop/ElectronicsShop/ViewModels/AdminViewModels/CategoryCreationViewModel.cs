using System.ComponentModel;

namespace ElectronicsShop.ViewModels.AdminViewModels
{
    public partial class CategoryCreationViewModel : BaseViewModel
    {
        [ObservableProperty]
        string categoryName;
        [ObservableProperty]
        string imageUrl;


        [ObservableProperty]
        bool isNotEmpty = false;


        private FileResult _image;
        readonly CategoryService _categoryService;

        public CategoryCreationViewModel(CategoryService categoryService)
        {
            _categoryService = categoryService;

            PropertyChanged += CheckEmpty;
        }
        private void CheckEmpty(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsNotEmpty)) return;
            if (string.IsNullOrEmpty(CategoryName) ||
                string.IsNullOrEmpty(ImageUrl))
                IsNotEmpty = false;
            else IsNotEmpty = true;
        }

        [RelayCommand]
        public async Task Confirm()
        {
            bool isSuccessful = await _categoryService.AddCategory(CategoryName, _image);
            if(!isSuccessful)
            {
                await Shell.Current.DisplayAlert("Error!", "Same category may exist", "Ok");
                IsBusy = false;
                return;
            }
            await Shell.Current.DisplayAlert("Success", "Category created", "Ok");
            await Shell.Current.GoToAsync("..");
            IsBusy = false;
        }

        [RelayCommand]
        public async Task PickImage()
        {
            _image = await MediaPicker.PickPhotoAsync();
            if (_image is null) return;
            ImageUrl = _image.FullPath;
        }
    }
}
