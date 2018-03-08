﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class CreateWord_OpenWord : System.Web.UI.Page
{
    string fileName = "";
    string subject = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       // Context.Response.Charset = "UTF8";
        fileName = Request.QueryString["fileName"].Trim();
         subject = Request.QueryString["subject"];
        Literal_Subject.Text = Server.UrlDecode(Request.QueryString["subject"]);

    }

    protected void PageOfficeCtrl1_Load(object sender, EventArgs e)
    {
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save", 1);
        PageOfficeCtrl1.WebOpen(Server.MapPath("doc/") + fileName, PageOffice.OpenModeType.docNormalEdit, "somebody");
    }
}
