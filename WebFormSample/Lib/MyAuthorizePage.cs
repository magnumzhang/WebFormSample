using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebFormSample.Lib;

namespace WebFormSample.Lib
{
    public abstract class MyAuthorizePage : System.Web.UI.Page
    {
        protected LoginInfo CurrentLoginInfo
        {
            get { return (LoginInfo)Session["CurrentLoginInfo"]; }
        }

        protected bool IsLogin { get { return Session["CurrentLoginInfo"] != null; } }

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);

            if (!IsPostBack)
            {
                if (!IsLogin)
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
    }
}