using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PageOfficeLinkTopic_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["userName"]="张三";
        Session["FileIsOpened"] = false;
    }
}