namespace ElectronicsShop.Views;

public partial class AccountView : ContentPage
{
	public AccountView(AccountViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
		changePasswordFields.IsVisible = !changePasswordFields.IsVisible;
		oldPasswordEntry.Text = String.Empty;
        newPasswordEntry.Text = String.Empty;
        repeatPasswordEntry.Text = String.Empty;
    }
}