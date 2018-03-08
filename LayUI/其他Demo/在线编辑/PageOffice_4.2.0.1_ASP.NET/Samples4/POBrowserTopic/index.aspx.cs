using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class POBrowserTopic_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string userName="张三";
        Session["userName"] = userName;
    }
}
