using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeApp.Models
{
    public partial class LoginRequestModel : Component
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public LoginRequestModel()
        {
            
        }

    }
}
