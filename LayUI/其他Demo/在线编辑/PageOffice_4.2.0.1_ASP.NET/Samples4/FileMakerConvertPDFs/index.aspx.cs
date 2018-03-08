using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FileMakerConvertPDFs_index : System.Web.UI.Page
{
    public String url = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        url = Server.MapPath("doc/");
    }
}