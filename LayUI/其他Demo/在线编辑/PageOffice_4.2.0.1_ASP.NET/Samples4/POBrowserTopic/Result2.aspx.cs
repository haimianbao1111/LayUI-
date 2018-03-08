using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class POBrowserTopic_Result2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string paramValue = Request["param"].ToString();
        Session["txt"] = paramValue;
    }
}
