using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class word : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void PageOfficeCtrl1_Load(object sender, EventArgs e)
    {
        PageOffice.ExcelWriter.Workbook wb = new PageOffice.ExcelWriter.Workbook();
        PageOffice.ExcelWriter.Sheet sheet1 = wb.OpenSheet("Sheet1");
        //设置当工作表只读时，是否允许用户手动调整行列。
        sheet1.AllowAdjustRC=true;
        // 设置PageOffice组件服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.AddCustomToolButton("保存","Save()",1);
        // 设置保存文件页面
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";
        PageOfficeCtrl1.SetWriter(wb);
        // 打开文档
        PageOfficeCtrl1.WebOpen("doc/test.xls", PageOffice.OpenModeType.xlsReadOnly, "Tom");
    }
}
