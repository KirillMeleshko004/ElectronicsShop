namespace ElectronicsShop.Views.UserViews;

public partial class ProductDetailsView : ContentPage
{
    public ProductDetailsView(ProductDetailsViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Frame frame = (Frame)sender;
        Animations.FrameAnimation.AnimateFrame(frame);
    }
}