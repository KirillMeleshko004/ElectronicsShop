namespace ElectronicsShop.Views.UserViews;

public partial class CheckoutView : ContentPage
{
    public CheckoutView(CheckoutViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

    }
}