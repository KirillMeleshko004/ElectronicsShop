namespace ElectronicsShop.Views.UserViews;

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