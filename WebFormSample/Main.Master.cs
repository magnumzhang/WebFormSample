using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using WebFormSample.Lib;

namespace WebFormSample
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected LoginInfo CurrentLoginInfo
        {
            get { return (LoginInfo)Session["CurrentLoginInfo"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUserID.Text = CurrentLoginInfo.UserID;
                lblUserName.Text = CurrentLoginInfo.UserName;
                lblDepartment.Text = CurrentLoginInfo.Department;

               LoadMenu();
            }
        }

        private void LoadMenu()
        {
            MenuDataCollection MDC = new MenuDataCollection();
            StringBuilder myHTMLBuilder = new StringBuilder();
            myHTMLBuilder.Append("<ul class=\"navbar-nav navbar-sidenav\" id=\"exampleAccordion\">");
                        
            foreach (MenuItemBase TopMenuItem in MDC.RootMenuList)
            {
                myHTMLBuilder.AppendLine("<li class=\"nav-item\" data-toggle=\"tooltip\" data-placement=\"right\">");
                myHTMLBuilder.AppendFormat("<a class=\"nav-link nav-link-collapse collapsed\" data-toggle=\"collapse\" href=\"#{0}\" data-parent=\"#exampleAccordion\">", TopMenuItem.MenuID);
                myHTMLBuilder.AppendFormat("<span class=\"nav-link-text\">{0}</span>", TopMenuItem.MenuText);
                myHTMLBuilder.AppendLine("</a>");

                List<MenuItemBase> SecondLevelMenuItemList = (from t in MDC.MenuList where t.ParentID == TopMenuItem.MenuID select t).ToList();

                myHTMLBuilder.AppendFormat("<ul class=\"sidenav-second-level collapse\" id=\"{0}\">", TopMenuItem.MenuID);
                foreach (MenuItemBase SecondLevelMenuItem in SecondLevelMenuItemList)
                {
                    List<MenuItemBase> ThirdLevelMenuItemList = (from t in MDC.MenuList where t.ParentID == SecondLevelMenuItem.MenuID select t).ToList();
                    if (ThirdLevelMenuItemList.Count == 0)
                    {
                        myHTMLBuilder.AppendLine("<li>");

                        if (SecondLevelMenuItem is PageMenuItem)
                        {
                            myHTMLBuilder.AppendFormat("<a href=\"#\">{0}</a>", SecondLevelMenuItem.MenuText);
                        }
                        else
                        {
                            //不支持MVC action controller的菜单链接。
                            //ActionMenuItem myActionMenuItem = (ActionMenuItem)SecondLevelMenuItem;
                            //Html.ActionLink(myActionMenuItem.MenuText, myActionMenuItem.ActionName, myActionMenuItem.ControllerName);
                        }

                        myHTMLBuilder.AppendLine("</li>");
                    }
                    else
                    {
                        myHTMLBuilder.AppendLine("<li>");
                        myHTMLBuilder.AppendFormat("<a class=\"nav-link-collapse collapsed\" data-toggle=\"collapse\" href=\"#{0}\">{1}</a>", SecondLevelMenuItem.MenuID, SecondLevelMenuItem.MenuText);
                        myHTMLBuilder.AppendFormat("<ul class=\"sidenav-third-level collapse\" id=\"{0}\">", SecondLevelMenuItem.MenuID);

                        foreach (MenuItemBase ThirdLevelMenuItem in ThirdLevelMenuItemList)
                        {
                            myHTMLBuilder.AppendLine("<li>");

                            if (ThirdLevelMenuItem is PageMenuItem)
                            {
                                myHTMLBuilder.AppendFormat("<a href=\"#\">{0}</a>", ThirdLevelMenuItem.MenuText);
                            }
                            else
                            {
                                //不支持MVC action controller的菜单链接。
                                //ActionMenuItem myActionMenuItem = (ActionMenuItem)ThirdLevelMenuItem;
                                //Html.ActionLink(myActionMenuItem.MenuText, myActionMenuItem.ActionName, myActionMenuItem.ControllerName);
                            }

                            myHTMLBuilder.AppendLine("</li>");
                        }
                        myHTMLBuilder.AppendLine("</ul>");
                        myHTMLBuilder.AppendLine("</li>");
                    }
                }
                myHTMLBuilder.AppendLine("</ul>");
                myHTMLBuilder.AppendLine("</li>");
               
            }

            myHTMLBuilder.AppendLine("</ul>");

            string myHTMLStr = myHTMLBuilder.ToString();
            ltMenu.Text = myHTMLStr;
        }
    }
}