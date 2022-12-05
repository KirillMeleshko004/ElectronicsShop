namespace ElectronicsShop.Views.UserViews;

public partial class ProductDetailsView : ContentPage
{
    public ProductDetailsView(ProductDetailsViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}