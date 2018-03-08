using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PageOffice.WordWriter;

public partial class Word : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void PageOfficeCtrl1_Load(object sender, EventArgs e)
    {
    
        // 设置PageOffice组件服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";

        PageOffice.WordWriter.WordDocument doc = new PageOffice.WordWriter.WordDocument();
        doc.DisableWindowRightClick = true; //禁止word鼠标右键
        //doc.DisableWindowDoubleClick = true;//禁止word鼠标双击
        PageOfficeCtrl1.SetWriter(doc);

        PageOfficeCtrl1.AddCustomToolButton("保存","Save()",1);
        // 设置保存文件页面
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";
        // 打开文档
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docNormalEdit, "Tom");
    }
}
