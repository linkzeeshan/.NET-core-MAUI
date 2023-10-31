namespace BarCodeApp.Views;

public partial class Settings : ContentPage
{
    public Settings()
    {
        InitializeComponent();
        string Name = Preferences.Get("Name", "unknown");
        string BranchName = Preferences.Get("BranchName", "unknown");
        string Email = Preferences.Get("Email", "unknown");
        string ServerName = Preferences.Get("ServerName", "unknown");
        string DataBase = Preferences.Get("DatabaseName", "unknown");
        string GeneratedCode = Preferences.Get("GeneratedCode", "unknown");
        string AppCode = Preferences.Get("AppCode", "unknown");
        if (Name != "unknown")
        {
            txtName.Text = Name;
        }
        if (Email != "unknown")
        {
            txtEmail.Text = Email;
        }
        if (BranchName != "unknown")
        {
            txtBranchName.Text = BranchName;
        }
        if (ServerName != "unknown")
        {
            txtServerName.Text = ServerName;
        }
        if (DataBase != "unknown")
        {
            txtDatabaseName.Text = DataBase;
        }
        if (GeneratedCode != "unknown")
        {
            txtGeneratedCode.Text = GeneratedCode;
        }
        if (AppCode != "unknown")
        {
            txtAppCode.Text = AppCode;
        }

    }

    private void btnSave_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtName.Text))
        {
            DisplayAlert("Blank Name", "Please provide a Name", "OK");
            return;
        }
        if (string.IsNullOrEmpty(txtEmail.Text))
        {
            DisplayAlert("Blank Email Address", "Please provide an Email Address", "OK");
            return;
        }
        else
        {
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                DisplayAlert("Invalid Email Address", "Provided Email Address is not valid", "OK");
                return;
            }
        }
        try
        {
            Preferences.Set("Name", txtName.Text);
            Preferences.Set("BranchName", txtBranchName.Text);
            Preferences.Set("ServerName", txtServerName.Text);
            Preferences.Set("DatabaseName", txtDatabaseName.Text);
            Preferences.Set("Email", txtEmail.Text);
            if (txtAppCode.Text != "")
                Preferences.Set("AppCode", txtAppCode.Text);
            if (txtGeneratedCode.Text != "")
                Preferences.Set("GeneratedCode", txtGeneratedCode.Text);

            DisplayAlert("Saved", "Successfully Saved", "OK");
        }
        catch (Exception e1)
        {
            DisplayAlert("Error", e1.Message, "OK");
        }
    }
}