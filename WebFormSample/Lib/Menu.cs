using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormSample.Lib
{
    public class MenuItemBase
    {
        public string MenuID;
        public string MenuText;
        public string MenuValue;
        public string ParentID;
    }

    public class PageMenuItem : MenuItemBase
    {
        public string NavigateURL;

        public PageMenuItem(string ID, string text, string value, string parentID, string navigateURL)
        {
            this.MenuID = ID;
            this.MenuText = text;
            this.MenuValue = value;
            this.ParentID = parentID;
            this.NavigateURL = navigateURL;
        }
    }

    public class ActionMenuItem : MenuItemBase
    {
        public string ActionName;
        public string ControllerName;

        public ActionMenuItem(string ID, string text, string value, string parentID, string actionName, string controllerName)
        {
            this.MenuID = ID;
            this.MenuText = text;
            this.MenuValue = value;
            this.ParentID = parentID;
            this.ActionName = actionName;
            this.ControllerName = controllerName;
        }
    }

    public class MenuDataCollection
    {
        public List<MenuItemBase> MenuList;
        public List<MenuItemBase> RootMenuList;
        public List<MenuItemBase> ChildMenuList;

        public MenuDataCollection()
        {
            //目前对于系统菜单，至顶向下，最多支持三级菜单。
            MenuList = new List<MenuItemBase>();
            RootMenuList = new List<MenuItemBase>();
            ChildMenuList = new List<MenuItemBase>();

            //TODO:load your menu here.

            MenuList.Add(new PageMenuItem("PageMenuID001", "TopMenuText001", "TopMenuValue001", "0", "#"));
            MenuList.Add(new PageMenuItem("PageMenuID002", "TopMenuText002", "TopMenuValue002", "0", "#"));

            MenuList.Add(new PageMenuItem("PageMenuID002_A", "SubMenuTextA", "SubMenuValueA", "PageMenuID002", "#"));
            MenuList.Add(new PageMenuItem("PageMenuID002_B", "SubMenuTextB", "SubMenuValueB", "PageMenuID002", "#"));
            MenuList.Add(new PageMenuItem("PageMenuID002_C", "SubMenuTextC", "SubMenuValueC", "PageMenuID002", "#"));

            MenuList.Add(new PageMenuItem("PageMenuID002_C_1", "SubMenuTextC-Sub111", "SubMenuValueC-Sub111", "PageMenuID002_C", "#"));
            MenuList.Add(new PageMenuItem("PageMenuID002_C_2", "SubMenuTextC-Sub222", "SubMenuValueC-Sub222", "PageMenuID002_C", "#"));

            //MenuList.Add(new ActionMenuItem("ActionMenuID003", "TopMenuText003", "TopMenuValue003", "0", "Action1", "Controller1"));

            //MenuList.Add(new ActionMenuItem("ActionMenuID003_A", "SubMenuTextAA", "SubMenuValueAA", "ActionMenuID003", "Action2", "Controller2"));
            //MenuList.Add(new ActionMenuItem("ActionMenuID003_B", "SubMenuTextBB", "SubMenuValueBb", "ActionMenuID003", "Action2", "Controller2"));

            //MenuList.Add(new ActionMenuItem("ActionMenuID004", "TopMenuText004", "TopMenuValue004", "0", "Action2", "Controller2"));



            RootMenuList = (from t in MenuList where t.ParentID == "0" select t).ToList();
            ChildMenuList = (from t in MenuList where t.ParentID != "0" select t).ToList();
        }
    }
}