using System.ComponentModel;

namespace ElectronicsShop.ViewModels.AdminViewModels
{
    public partial class CategoryCreationViewModel : BaseViewModel
    {
        [ObservableProperty]
        string categoryName;
        [ObservableProperty]
        string imageURI;


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
                string.IsNullOrEmpty(ImageURI))
                IsNotEmpty = false;
            else IsNotEmpty = true;
        }

        [RelayCommand]
        public async Task Confirm()
        {
            IsBusy = true;

            if (!NetworkCheckerService.CheckConnection())
            {
                NetworkCheckerService.ShowNewtworkErrorMessage();
                IsBusy = false;
                return;
            }

            bool isSuccessful = await _categoryService.AddCategory(CategoryName, _image);
            if (!isSuccessful)
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
            ImageURI = _image.FullPath;
        }
    }
}
