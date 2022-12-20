namespace ElectronicsShop.Views.AdminViews;

public partial class DeleteCategoryView : ContentPage
{
	public DeleteCategoryView(DeleteCategoryViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
    }
    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (((Picker)sender).SelectedItem is not null)
        {
            categoryFields.IsVisible = true;
        }
        else
        {
            categoryFields.IsVisible = false;
        }
    }
}