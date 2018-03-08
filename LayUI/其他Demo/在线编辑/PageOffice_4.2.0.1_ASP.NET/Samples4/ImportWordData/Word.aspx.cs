using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ImportWordData_Word : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.AddCustomToolButton("导入文件", "importData()", 5);
        PageOfficeCtrl1.AddCustomToolButton("提交数据", "submitData()", 1);
        PageOffice.WordWriter.WordDocument wordDoc = new PageOffice.WordWriter.WordDocument();
        PageOfficeCtrl1.SetWriter(wordDoc);
        PageOfficeCtrl1.SaveDataPage = "SaveData.aspx"; 
    }

}