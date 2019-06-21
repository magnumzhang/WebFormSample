using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormSample.Lib
{
    public enum LoginCheckType
    {
        AD,
        System
    }

    public class LoginInfo
    {
        //define class property # NOT # member here.
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Department { get; set; }
        public string RoleName { get; set; }

        public LoginInfo()
        {
            //# MUST # initialize all the property value first.
            UserID = "#";
            UserName = "#";
            Department = "#";
            RoleName = "#";
        }
    }
}