using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class POBrowserTopic_Word3 : System.Web.UI.Page
{
    public string txt;
    protected void Page_Load(object sender, EventArgs e)
    {
        txt = Session["txt"].ToString();

        // 设置PageOffice组件服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        // 设置保存文件页面
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";

        PageOfficeCtrl1.AddCustomToolButton("保存并关闭", "Save()", 1);

        // 打开文档
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docNormalEdit, "Tom");
    }
}
