
using System.Drawing;
using System;
using System.Media;
using Microsoft.Maui.Controls;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;

namespace BarCodeApp.Views;

public partial class DashBoardPage : ContentPage
{
    List<string> sTags = Global.sTags;
    public DashBoardPage()
    {
        //bool bValidApp = false;
        InitializeComponent();
        try
        {
            if (Preferences.Get("GeneratedCode", "Unknown").ToString() == "Unknown")
            {
                var x = RandomNumberGenerator.GetInt32(1000000, 9999999);
                Preferences.Set("GeneratedCode", x.ToString());
            }
            if (Preferences.Get("AppCode", "Unknown") == "Unknown")
            {
                int GeneratedCode = Convert.ToInt32(Preferences.Get("Geneated", "0"));
                Global.iValidCode = Global.ValidCode(GeneratedCode);
                SendMail(GeneratedCode, Global.iValidCode);

                DisplayAlert("Invalide Code", "Please go to settings and input hte App Code provided by MTech.", "OK");
            }
            else
            {
                int GeneratedCode = Convert.ToInt32(Preferences.Get("GeneratedCode", "0"));
                Global.iValidCode = Global.ValidCode(GeneratedCode);
                int AppCode = Convert.ToInt32(Preferences.Get("AppCode", "0"));
                if (Global.iValidCode != AppCode)
                {
                    SendMail(GeneratedCode, Global.iValidCode);
                    DisplayAlert("Not Valid ", "This App is not Valid", "OK");
                }
            }
        }

        catch (Exception ex)
        {

            DisplayAlert("Error", ex.Message, "OK");
        }
    }

    //To be continue from here
    private void SendMail(int key1, int key2)
    {
        try
        {
            string fromMail = "infomaxersalert@gmail.com";
            String fromPassword = "yqhekrjityknxmew";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "key for" + key1.ToString();
            message.To.Add(new MailAddress("calltariq@gmail.com"));
            message.To.Add(new MailAddress("mtech.ksa@gmail.com"));
            message.To.Add(new MailAddress("naveed.mtech@gmail.com"));

            message.Body = "Please send this key to the client . KEY: " + key2.ToString();

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,

            };
            smtpClient.Send(message);
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
        }
    }

    protected override void OnAppearing()
    {
        RefreshObjects();
    }
    private void RefreshObjects()
    {
        int AppCode = Convert.ToInt32(Preferences.Get("AppCode", "0"));
        lblCompanyName.Text = Preferences.Get("Name", "");
        lblBrachName.Text = Preferences.Get("BranchName", "");
        //lblServerName.Text = Preferences.Get("ServerName", "");
        //lblDatabaseName.Text = Preferences.Get("DatabaseName", "");

        if (Global.iValidCode == AppCode)
        {
            btnContinueStock.IsEnabled = true;
           btnNewStock.IsEnabled = true;
            btnStockTakeList.IsEnabled = true;

        }
        else
        {
            btnContinueStock.IsEnabled = false;
            btnNewStock.IsEnabled = false;
            btnStockTakeList.IsEnabled = false;
        }
    }

    //private void ToolbarItem_Clicked(object sender, EventArgs e)
    //{
    //    DisplayAlert("button Pressed", "nice job", "ok");
    //}

    private async void btnNewStock_Clicked(object sender, EventArgs e)
    {
        if (sTags.Count > 0)
        {
            bool answer = await DisplayAlert("Warning,", " You have already taken stock. Are you sure to take new stock?", "yes", " No");
            if (answer == false)
            {
                return;
            }
            sTags.Clear();
        }
        await Navigation.PushAsync(new TaggingPage(sTags));

    }

    private async void btnContinueStock_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TaggingPage(sTags));
    }



    private async void btnStockTakeList_Clicked(object sender, EventArgs e)
    {
        //DisplayAlert("  StockList button Pressed", "nice job", "ok");
        await Navigation.PushAsync(new StockList(sTags));


    }



    private async void btnSetting_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Settings());
    }

   

    private void btnPriceChecking_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RateComparison());
    }

  
    //private async void btnSetting_Clicked(object sender, EventArgs e)
    //{
    //    //DisplayAlert(" Settingt button Pressed", "nice job", "ok");
    //    await Navigation.PushAsync(new TaggingPage(sTags));


    //}
}