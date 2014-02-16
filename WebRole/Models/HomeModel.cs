using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Screen.Vc.WebRole.Models
{
    public class HomeModel
    {
        public HomeModel()
        {
            Register = new RegisterModel();
            Login = new LoginModel();
        }
        public RegisterModel Register { get; set; }
        public LoginModel Login { get; set; }

    }

}