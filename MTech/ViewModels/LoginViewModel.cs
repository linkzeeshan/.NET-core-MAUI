using Microsoft.Extensions.Options;
using BarCodeApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BarCodeApp.ViewModels
{
    public partial class LoginViewModel : INotifyPropertyChanged
    {
        private LoginRequestModel myLoginRequestModel = new LoginRequestModel();
        public LoginRequestModel MyLoginRequestModel
        {
            get { return myLoginRequestModel; }
            set
            {
                myLoginRequestModel = value;
                OnPropertyChanged(nameof(MyLoginRequestModel));
            }
        }
        public ICommand LoginCommand {get;}
        public bool ValidUser(string UserName, string Password) 
        { 
            return false;
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(PerformLoginOperation);
        }

        private async void PerformLoginOperation(object obj)
        {
            //Perform API Operation / DB Operation
            var data = MyLoginRequestModel;

            if (string.IsNullOrEmpty(data.UserName) && string.IsNullOrEmpty(data.Password)) 
            {
                await Shell.Current.DisplayAlert("Invalid User", "Empty User Name and Password not allowed", "OK");
            }
            else if (string.IsNullOrEmpty(data.UserName))
            {
                await Shell.Current.DisplayAlert("Invalid User", "Empty User Name not allowed", "OK");

            }
            else if (string.IsNullOrEmpty(data.Password))
            {
                await Shell.Current.DisplayAlert("Invalid User", "Empty Password not allowed", "OK");

            }
            else
            {
                if (ValidUser(data.UserName, data.Password))
                {
                    Preferences.Set("UserAlreadyLoggedIn", true);

                    await Shell.Current.GoToAsync(state: "//DashBoard");

                }
                else
                {
                    await Shell.Current.DisplayAlert("Invalid User", "User Name and / or Password is not valid", "OK");
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
