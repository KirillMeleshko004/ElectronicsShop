using AndroidX.Lifecycle;

namespace ElectronicsShop.Views;

public partial class AccountView : ContentPage
{
    public AccountViewModel ViewModel { get; set; }
    public AccountView(AccountViewModel viewModel)
	{
		InitializeComponent();

        ViewModel = viewModel;
        BindingContext = ViewModel;
    }

	private void Button_Clicked(object sender, EventArgs e)
	{
		changePasswordFields.IsVisible = !changePasswordFields.IsVisible;
		oldPasswordEntry.Text = String.Empty;
        newPasswordEntry.Text = String.Empty;
        repeatPasswordEntry.Text = String.Empty;
    }
}