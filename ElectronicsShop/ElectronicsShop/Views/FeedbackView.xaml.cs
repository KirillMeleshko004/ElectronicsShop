namespace ElectronicsShop.Views;

public partial class FeedbackView : ContentPage
{
    public FeedbackViewModel ViewModel { get; set; }
    public FeedbackView(FeedbackViewModel viewModel)
	{
		InitializeComponent();

		ViewModel = viewModel;
		BindingContext = ViewModel;
	}
}