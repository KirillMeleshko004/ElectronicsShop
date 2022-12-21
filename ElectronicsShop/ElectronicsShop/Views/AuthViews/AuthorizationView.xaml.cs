using Microsoft.Maui.Platform;

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
    private void EntryCompleted(object sender, EventArgs e)
    {
        if (((Entry)sender).Focus())
        {
            ((Entry)sender).Unfocus();
            this.Unfocus();
        }
#if ANDROID
        if (Platform.CurrentActivity.CurrentFocus != null)
            Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
#endif
    }
}