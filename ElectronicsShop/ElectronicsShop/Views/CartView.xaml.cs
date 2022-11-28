namespace ElectronicsShop.Views;

public partial class CartView : ContentPage
{
	public CartViewModel ViewModel { get; set; }
	public CartView(CartViewModel viewModel)
	{
		InitializeComponent();

        ViewModel = viewModel;
		BindingContext = ViewModel;
	}
}