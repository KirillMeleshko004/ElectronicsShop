namespace ElectronicsShop.Views.AuthViews;

public partial class AuthorizationView : ContentPage
{
    public AuthorizationView(AuthorizationViewModel viewModel)
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