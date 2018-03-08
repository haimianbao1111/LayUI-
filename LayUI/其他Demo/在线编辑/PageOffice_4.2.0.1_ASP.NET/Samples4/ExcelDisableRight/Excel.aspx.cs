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
        // 设置PageOffice组件服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";

        PageOffice.ExcelWriter.Workbook workBook = new PageOffice.ExcelWriter.Workbook();
        workBook.DisableSheetRightClick = true;//禁止当前工作表的鼠标右键
        //workBook.DisableSheetDoubleClick = true;//禁止当前工作表的鼠标双击
        PageOfficeCtrl1.SetWriter(workBook);

        // 设置保存文件页面
        PageOfficeCtrl1.AddCustomToolButton("保存", "SaveFile()",1);
        PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";
        // 打开文档
        PageOfficeCtrl1.WebOpen("doc/test.xls", PageOffice.OpenModeType.xlsNormalEdit, "Tom");
    }
}
