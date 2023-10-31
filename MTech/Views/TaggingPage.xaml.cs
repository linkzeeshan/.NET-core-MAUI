//using Android.Service.Notification;
using BarCodeApp.ViewModels;
using CommunityToolkit.Maui.Views;
using System.Collections;

namespace BarCodeApp.Views;
public partial class TaggingPage : ContentPage
{
    ArrayList barCode = new ArrayList();
    List<string> sTags;
    string sPreviousBarCode = "";
    bool bAdded = false;
	public TaggingPage(List<string> sTagsTemp)
	{
        try
        {
            InitializeComponent();
            sTags = sTagsTemp;
            Tags.ItemsSource = sTags;
            cameraView.StopCameraAsync();
            btnCancel.IsVisible = false;
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
        }

    }
    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        //sPreviousBarCode = "";
    }

    private async void StartCamera()
    {
        try
        {
            if (cameraView.Cameras.Count > 0)
            {
                cameraView.IsVisible = true;
                cameraView.IsEnabled = true;
                
                cameraView.Camera = cameraView.Cameras.First();
                //MainThread.BeginInvokeOnMainThread(async () =>
                //{
                //await Task.Delay(1000);
                //await cameraView.StopCameraAsync();
                await cameraView.StartCameraAsync();
                btnCancel.IsVisible = true;
                //});
            }
        }
        catch(Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        } 

    }

    private async void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        try
        { 
            MainThread.BeginInvokeOnMainThread(() =>
            {
                string sBarCode = $"{args.Result[0].BarcodeFormat}: {args.Result[0].Text}";
                if (sBarCode.Contains(":"))
                {
                    int iPos = sBarCode.IndexOf(":");
                    sBarCode = sBarCode.Substring(iPos + 1).Trim();
                }
                if (sBarCode != sPreviousBarCode)
                {
                    //TaggingPage.SetBarCode(sBarCode);
                    //cameraView.StopCameraAsync();
                    //cameraView.StopCameraAsync();
                    cameraView.StopCameraAsync();
                    txtBarCodeNo.Text = sBarCode;
                    cameraView.IsVisible = false;
                    btnCancel.IsVisible = false;
                    cameraView.IsEnabled = false;
                    txtQuantity.SelectionLength = txtQuantity.Text.Length;
                    txtQuantity.Focus();
                    sPreviousBarCode = sBarCode;
                }
               // else
                //{
                 //   Task.Delay(2000);
                 //   sPreviousBarCode = "";
               // }
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }


    private async void btnAdd_Clicked(object sender, EventArgs e)
    {
        try
        {
            Add();
            txtQuantity.IsEnabled = false;
            txtQuantity.IsEnabled = true;
            if (bAdded == true)
            {
                StartCamera();
                //cameraView.IsVisible = true;
                //cameraView.StartCameraAsync();
                //Navigation.PushAsync(new BarCodeScanner());
                //Navigation.PushModalAsync(new Temp());
            }
        }
        catch(Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }       
    }
    private async void Add()
    {
        try
        {
            if (string.IsNullOrEmpty(txtBarCodeNo.Text))
            {
                await DisplayAlert("Error", "Bar Code cannot be blank", "OK");
                bAdded = false;
                return;
            }
            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                await DisplayAlert("Error", "Quantity cannot be blank", "OK");
                bAdded = false;
                return;
            }
            int qty;
            if (int.TryParse(txtQuantity.Text, out qty) == true)
            {
                if (Convert.ToInt32(txtQuantity.Text) <= 0)
                {
                    await DisplayAlert("Error", "Qauantity is not valid.", "OK");
                    bAdded = false;
                    return;
                }
            }
            else
            {
                await DisplayAlert("Error", "Qauantity is not valid.", "OK");
                bAdded = false;
                return;
            }
            Int32 Quantity = SearchBarCode(txtBarCodeNo.Text.Trim());
            //Int32 Quantity = 0;
            if (Quantity > 0)
            {
                bool answer = await DisplayAlert("Duplicate", "This Bar Code is already added with Quantity " + Quantity.ToString() + ". Do you want to add more quantity to this BarCode?", "Yes", "No");
                //bool answer = true;
                if (answer == false)
                {
                    bAdded = false;
                    return;
                }
                txtQuantity.Text = (Convert.ToInt32(txtQuantity.Text) + Quantity).ToString();
                RemoveBarCode(txtBarCodeNo.Text);
                bAdded = true;
            }
            sTags.Insert(0, txtBarCodeNo.Text + ": " + txtQuantity.Text);
            Tags.ItemsSource = "";
            Tags.ItemsSource = sTags;
            txtBarCodeNo.Text = "";
            txtQuantity.Text = "1";
            bAdded = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }


    }
    private Int32 SearchBarCode(string barCodeNo)
    {
        try
        {
            int i;
            int iPosition;
            for (i = 0; i <= sTags.Count - 1; i++)
            {
                iPosition = sTags[i].IndexOf(":");
                if (iPosition >= 0)
                {
                    if (sTags[i].ToString().Substring(0, iPosition).Trim() == barCodeNo)
                    {
                        return Convert.ToInt32(sTags[i].ToString().Substring(iPosition + 1).Trim());
                    }
                }
            }
            return 0;
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
            return 0;
        }

    }
    private async void RemoveBarCode(string barCodeNo)
    {
        try
        {
            int i;
            int iPosition;
            for (i = 0; i <= sTags.Count - 1; i++)
            {
                iPosition = sTags[i].IndexOf(":");
                if (iPosition >= 0)
                {
                    if (sTags[i].ToString().Substring(0, iPosition).Trim() == barCodeNo)
                    {
                        sTags.Remove(sTags[i].ToString());
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }


    }

    private async void btnAddOnly_Clicked(object sender, EventArgs e)
    {
        try
        {
            Add();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void btnStartScan_Clicked(object sender, EventArgs e)
    {
        try
        {
            txtQuantity.IsEnabled = false;
            txtQuantity.IsEnabled = true;
            StartCamera();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }

    }

    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        try
        {
            cameraView.IsVisible = false;
            btnCancel.IsVisible = false;
            cameraView.IsEnabled = false;
            await cameraView.StopCameraAsync();
            await cameraView.StartCameraAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private void txtQuantity_Focused(object sender, FocusEventArgs e)
    {
        Dispatcher.Dispatch(() =>
        {
            var entry = sender as Entry;
            entry.CursorPosition = 0;
            entry.SelectionLength = entry.Text == null ? 0 : entry.Text.Length;

        });
    }
    
}