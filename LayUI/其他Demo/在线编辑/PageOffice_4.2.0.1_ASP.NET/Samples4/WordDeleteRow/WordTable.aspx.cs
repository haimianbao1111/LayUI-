using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PageOffice.WordWriter;

public partial class WordDeleteRow_WordTable : System.Web.UI.Page
    
{
    public String FilePath = null;
    public void Page_Load(object sender, EventArgs e)
    {
        FilePath = Request.ApplicationPath + "WordDeleteRow/doc";
        WordDocument doc = new WordDocument();
        PageOffice.WordWriter.Table table1 = doc.OpenDataRegion("PO_table").OpenTable(1);
        Cell cell = table1.OpenCellRC(2,1);
        //删除坐标为(2,1)的单元格所在行
        table1.RemoveRowAt(cell);
        PageOfficeCtrl1.CustomToolbar = false;
        PageOfficeCtrl1.SetWriter(doc);
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        PageOfficeCtrl1.WebOpen("doc/test_table.doc", PageOffice.OpenModeType.docNormalEdit, "张佚名");
    }
}
