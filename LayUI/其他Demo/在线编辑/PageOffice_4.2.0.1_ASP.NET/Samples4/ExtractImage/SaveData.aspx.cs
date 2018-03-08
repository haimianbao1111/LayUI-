using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExtractImage_SaveData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOffice.WordReader.WordDocument wordDoc = new PageOffice.WordReader.WordDocument();
        PageOffice.WordReader.DataRegion dataRegion1 = wordDoc.OpenDataRegion("PO_image");
        //将提取的图片保存到服务器上，图片的名称为:a.jpg
        dataRegion1.OpenShape(1).SaveAsJPG(Server.MapPath("doc/a.jpg"));
        wordDoc.CustomSaveResult = "保存成功,文件保存到：" + Server.MapPath("doc/a.jpg");
        wordDoc.Close();
    }
}