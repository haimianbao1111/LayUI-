using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PageOfficeLinkTopic_Result2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String text=Request.QueryString["text"];
        Session["text"] = text;
    }
}