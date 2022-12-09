using ElectronicsShop.ViewModels;

namespace ElectronicsShop.Views.UserViews;

public partial class FeedbackView : ContentPage
{
    public IRefreshableAsync ViewModel { get; set; }
    public FeedbackView(FeedbackViewModel viewModel)
    {
        InitializeComponent();

        ViewModel = viewModel;
        BindingContext = viewModel;
    }
}