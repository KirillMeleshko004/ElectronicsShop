using ElectronicsShop.ViewModels;

namespace ElectronicsShop.Views.UserViews;

public partial class CategoryView : ContentPage
{
    public IRefreshableAsync ViewModel { get; set; }
    public CategoryView(CategoryViewModel viewModel)
    {
        InitializeComponent();

        ViewModel = viewModel;
        BindingContext = viewModel;
    }
}