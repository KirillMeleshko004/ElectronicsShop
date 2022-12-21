namespace ElectronicsShop.Views.AuthViews;

public partial class RegistrationView : ContentPage
{
	public RegistrationView(RegistrationViewModel viewModel)
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