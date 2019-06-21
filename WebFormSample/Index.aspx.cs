using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using WebFormSample.Lib;

namespace WebFormSample
{
    public partial class Index : MyAuthorizePage
    {
        private DataTable PageDataSource
        {
            get { return (DataTable)ViewState["PageDataSource"]; }
            set { ViewState["PageDataSource"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                for (int i = 0; i < 10; i++)
                {
                    dt.Columns.Add(new DataColumn("COL" + i));
                }

                for (int i = 0; i < 30; i++)
                {
                    DataRow dr = dt.NewRow();

                    for (int j = 0; j < 10; j++)
                    {
                        dr[j] = "data"+ i+j;
                    }
                    dt.Rows.Add(dr);
                }

                PageDataSource = dt;
                BindData();


            }
        }

        private void BindData()
        {
            gv.DataSource = PageDataSource;
            gv.DataBind();
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            BindData();

        }
    }
}