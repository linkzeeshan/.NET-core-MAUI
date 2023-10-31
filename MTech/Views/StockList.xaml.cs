//using Microsoft.UI.Composition.Interactions;
//using HealthKit;
using System.Data;
using System.Formats.Tar;
using System.Net;
using System.Net.Mail;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
//using Java.Security;

namespace BarCodeApp.Views;

public partial class StockList : ContentPage
{
    List<string> sTags;
    string FileNameWOPath = "";
    public StockList(List<string> sTagsTem)
    {
        int i;
        int row = 1;
        InitializeComponent();
        sTags = sTagsTem;
        string sBarCode;
        string sQuantity;
        int iPosition;
        

        gList.Add(new BoxView
        {
            Color = Colors.White,

        }, 0, 0
        );
        gList.Add(new Label
        {
            Text = "Bar Code",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        }, 0, 0
        );
        gList.Add(new BoxView
        {
            Color = Colors.White
        }, 1, 0);

        gList.Add(new Label
        {
            Text = "Quantity",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        }, 1, 0);


        for (i = sTags.Count - 1; i >= 0; i--)
        {
            gList.RowDefinitions.Add(new RowDefinition(20));

            iPosition = sTags[i].IndexOf(":");
            if (iPosition > -1)
            {
                sBarCode = sTags[i].Substring(0, iPosition);
                sQuantity = sTags[i].Substring(iPosition + 1);

                gList.Add(new BoxView
                {
                    Color = Colors.White,

                }, 0, row
                    );
                gList.Add(new Label
                {
                    Text = sBarCode,
                    TextColor = Colors.Black,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                }, 0, row
                );
                gList.Add(new BoxView
                {
                    Color = Colors.White
                }, 1, row);

                gList.Add(new Label
                {
                    Text = sQuantity,
                    TextColor = Colors.Black,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                }, 1, row);
                row++;
            }
        }

    }
    //public async Task CheckForPermissions()
    //{
    //    var status_write = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
    //    var status_read = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

    //    if (Permissions.ShouldShowRationale<Permissions.StorageWrite>())
    //    {
    //        // just testing
    //    }

    //    if (Permissions.ShouldShowRationale<Permissions.StorageRead>())
    //    {
    //        // just testing
    //    }

    //    status_write = await Permissions.RequestAsync<Permissions.StorageWrite>();
    //    status_read = await Permissions.RequestAsync<Permissions.StorageRead>();

    //    if (status_write != PermissionStatus.Granted)
    //    {
    //        // still not granted
    //    }

    //    // forward user to settings so they can grant permission
    //    AppInfo.Current.ShowSettingsUI();
    //}

    private async void btnExportToText_Clicked(object sender, EventArgs e)
    {
        //await CheckForPermissions();
        FileNameWOPath = "BarCode_" + DateTime.Now.ToString().Replace(" ","_").Replace("/","_").Replace(":","_") + ".txt";

        try
        {
            //var folder = await FolderPicker.PickAsync(default);
            //var folder = FolderPicker.Default.PickAsync(default);
            //string sFolder = folder.ToString();

            //string sFolder = $"Name: {folder.Name} Path: {folder.Path}";
            

            //await DisplayAlert("Folder", sFolder, "OK");
            string targetFileName = WriteTextToFile(FileNameWOPath);
            await DisplayAlert("Text Export", "Successfully exported to " + targetFileName, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }


    public string WriteTextToFile(string targetFileName)
    {
        string txtFile = "";

        for (int i = sTags.Count - 1; i >= 0; i--)
        {

            txtFile += sTags[i].ToString() + "\r\n";
        }

        try
        {
            string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, targetFileName);
            using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
            using StreamWriter streamWriter = new StreamWriter(outputStream);
            streamWriter.WriteAsync(txtFile);
            return targetFile;
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
            return "";
        }
    }

    private async void btnSendEmail_Clicked(object sender, EventArgs e)
    {
        try
        {
            FileNameWOPath = "BarCode_" + DateTime.Now.ToString().Replace(" ", "_").Replace("/", "_").Replace(":", "_") + ".txt";
            string targetFileName = WriteTextToFile(FileNameWOPath);
            //await Navigation.PushAsync(new Email(sTags, targetFileName, FileNameWOPath));
            if (Microsoft.Maui.ApplicationModel.Communication.Email.Default.IsComposeSupported)
            {

                string subject = "Stock Take Txt!";
                string body = "Stock Take Txt.";

                string sEmail = Preferences.Get("Email", "unknown");

                string[] recipients = new[] { sEmail, sEmail };

                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    BodyFormat = EmailBodyFormat.PlainText,
                    To = new List<string>(recipients)
                };
                message.Attachments.Add(new EmailAttachment(targetFileName));
                await Microsoft.Maui.ApplicationModel.Communication.Email.Default.ComposeAsync(message);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }


    }
}