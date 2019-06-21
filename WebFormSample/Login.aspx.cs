using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebFormSample.Lib;

namespace WebFormSample
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string PassLoginCheck(string JSonPostDataStr)
        {
            Dictionary<string, string> dic = JSonUtility.GetJSonDic(JSonPostDataStr);
            string UserName = dic["UserName"];
            string Password = dic["Password"];
            LoginCheckType LCK = (LoginCheckType)Enum.Parse(typeof(LoginCheckType), dic["LoginCheckType"]);

            bool IsPassCheck = false;

            switch (LCK)
            {
                case LoginCheckType.AD:
                    IsPassCheck = new LDAP().IsAuthenticated(UserName, Password);
                    break;
                case LoginCheckType.System:
                    IsPassCheck = true;
                    break;
            }

            //TODO:set login user object into session here.
            LoginInfo LoginInfoObj = new LoginInfo();
            LoginInfoObj.UserID = "A001";
            LoginInfoObj.UserName = "xiaojun";
            LoginInfoObj.Department = "IT";

            //string LoginInfoObjJSonStr = ObjectToJSon.GenerateJSon(LoginInfoObj);
            //string JSonKeyValuePair = String.Format("\"{0}\":{1}", "LoginInfoObjJSonStr", LoginInfoObjJSonStr);

            JSonStringBuilder JBuilder = new JSonStringBuilder();
            JBuilder.Begin();
            JBuilder.Add("IsPassCheck", IsPassCheck.ToString().ToUpper());
            //JBuilder.Add(JSonKeyValuePair);
            JBuilder.Add("Message", "test");
            JBuilder.End();

            string JSonStr = JBuilder.ToString();

            //# MUST # is login object into session here, action controller will check whether login by this session data.
            HttpContext.Current.Session["CurrentLoginInfo"] = LoginInfoObj;
            return JSonStr;
        }
    }
}