﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RunMacro_Word : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        TextBox1.Text = "Function myFunc1() \r\n"
                         +"myFunc1 = \"123\"  \r\n"
                         + "End Function";
        // 设置PageOffice组件服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        // 设置保存文件页面
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";
        // 打开文档
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docNormalEdit, "Tom");
    }
}
