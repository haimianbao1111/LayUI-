using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PageOfficeLinkTopic_Result : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       HttpContext context = HttpContext.Current;
        String pageStr = Request.QueryString["page"];
        if (pageStr.Equals("list"))
           
        {
            context.Response.ClearContent();
            context.Response.ContentType = "text/plain";
            context.Response.Write(Session["FileIsOpened"]);
            context.Response.End();
            context.Response.Close();

        }

        if (pageStr.Equals("open"))
        {
            Session["FileIsOpened"]=false;
         
        }

    }
}