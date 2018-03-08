using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class POBrowser_Word : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 设置PageOffice组件服务页面
        /**
          * 如果访问项目的时候浏览器地址栏中的地址都不带后缀（例如"http://localhost:3306/Samples/Deafult"），则这里给ServerPage赋值的时候也不能带后缀，
          * 直接就是“ PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server";”，否则会报“error：0”的错误
          */
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        //添加自定义保存按钮
        PageOfficeCtrl1.AddCustomToolButton("保存", "Save()", 1);
        // 设置保存文件页面
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";
        // 打开文档
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docNormalEdit, "Tom");
        
    }
}
