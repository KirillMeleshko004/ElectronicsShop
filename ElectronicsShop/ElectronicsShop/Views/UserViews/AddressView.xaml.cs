namespace ElectronicsShop.Views.UserViews;

public partial class AddressView : ContentPage
{
	public AddressView(AddressViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }

	private void Picker_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (((Picker)sender).SelectedItem is not null)
        {
			addressFields.IsVisible = true;
        }
		else
		{
			addressFields.IsVisible = false;
			cityEntry.Text = null;
			streetEntry.Text = null;
			buildingEntry.Text = null;
			apartmentEntry.Text = null;
			postalcodeEntry.Text = null;
		}
	}
}