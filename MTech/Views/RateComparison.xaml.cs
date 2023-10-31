namespace BarCodeApp.Views;

public partial class RateComparison : ContentPage
{
	public RateComparison()
	{
		InitializeComponent();
	}

    private void btnScanRateComparison_Clicked(object sender, EventArgs e)
    {

    }

    private void btnSearchRateComparison_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Is Search button Pressed?", "Yes it is pressed", "ok");
    }

    private void btnPrint_Clicked(object sender, EventArgs e)
    {
        DisplayAlert(" Is Print button Pressed?", " YES it is pressed", "ok");
    }
}