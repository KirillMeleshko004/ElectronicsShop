namespace ElectronicsShop.Views.UserViews;

public partial class CategoryView : ContentPage
{
    public CategoryViewModel ViewModel { get; set; }
    public CategoryView(CategoryViewModel viewModel)
    {
        InitializeComponent();

        ViewModel = viewModel;
        BindingContext = ViewModel;
    }
}