using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExtractImage_Word : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 设置PageOffice组件服务页面
        PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
        //添加自定义的保存按钮
        PageOfficeCtrl1.AddCustomToolButton("保存图片", "Save", 1);
        PageOffice.WordWriter.WordDocument wordDoc = new PageOffice.WordWriter.WordDocument();
        PageOffice.WordWriter.DataRegion dataRegion1 = wordDoc.OpenDataRegion("PO_image");
        dataRegion1.Editing = true;//放图片的数据区域是可以编辑的，其它部分不可编辑
        PageOfficeCtrl1.SetWriter(wordDoc);
        //设置数据提交的保存页面
        PageOfficeCtrl1.SaveDataPage = "SaveData.aspx";
        // 打开文档
        PageOfficeCtrl1.WebOpen("doc/test.doc", PageOffice.OpenModeType.docSubmitForm, "Tom");
    }
}