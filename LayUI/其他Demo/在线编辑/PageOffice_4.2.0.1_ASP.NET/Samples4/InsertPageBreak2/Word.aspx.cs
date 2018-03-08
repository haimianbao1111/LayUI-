using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InsertPageBreak_Word : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOffice.WordWriter.WordDocument wordDocument = new PageOffice.WordWriter.WordDocument();
        //在文档末尾插入分页符，并且在新的页中创建新的数据区域插入另一篇文档
        PageOffice.WordWriter.DataRegion mydr1 = wordDocument.CreateDataRegion("PO_first", PageOffice.WordWriter.DataRegionInsertType.After , "[end]");
        mydr1.SelectEnd ();
        wordDocument.InsertPageBreak();//插入分页符

        PageOffice.WordWriter.DataRegion mydr2= wordDocument.CreateDataRegion("PO_second", PageOffice.WordWriter.DataRegionInsertType.After, "[end]");
         mydr2.Value = "[word]doc/test2.doc[/word]";

        // 设置PageOffice组件服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.AddCustomToolButton("保存","Save()",1);
        PageOfficeCtrl1.SetWriter(wordDocument);
        //设置保存页面
        PageOfficeCtrl1.SaveFilePage="SaveFile.aspx";
        // 打开文档
        PageOfficeCtrl1.WebOpen("doc/test1.doc", PageOffice.OpenModeType.docNormalEdit, "Tom");
    }
}
