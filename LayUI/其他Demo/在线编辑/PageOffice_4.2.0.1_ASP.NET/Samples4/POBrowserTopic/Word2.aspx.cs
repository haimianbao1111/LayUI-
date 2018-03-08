using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class POBrowserTopic_Word2 : System.Web.UI.Page
{
    public string userName;
    public string id;

    protected void Page_Load(object sender, EventArgs e)
    {
        //获取index.aspx页面传递过来参数的值
        userName = Session["userName"].ToString();
        //获取index.aspx用？传递过来的id的值
        id = Request.QueryString["id"];

        // 设置PageOffice组件服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        // 设置保存文件页面
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";

        PageOfficeCtrl1.AddCustomToolButton("保存", "Save()", 1);
 
        // 打开文档
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docNormalEdit, "Tom");
    }
}
