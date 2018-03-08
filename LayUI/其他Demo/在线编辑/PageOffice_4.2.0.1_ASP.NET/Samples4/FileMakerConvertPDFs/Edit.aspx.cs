using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FileMakerConvertPDFs_Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String id = Request.QueryString["id"].Trim();
        String filePath = "";
        if ("1".Equals(id))
        {
            filePath = Server.MapPath("doc/PageOffice产品简介.doc");
        }
        if ("2".Equals(id))
        {
            filePath = Server.MapPath("doc/Pageoffice客户端安装步骤.doc");
        }
        if ("3".Equals(id))
        {
            filePath = Server.MapPath("doc/PageOffice的应用领域.doc");
        }
        if ("4".Equals(id))
        {
            filePath = Server.MapPath("doc/PageOffice产品对客户端环境要求.doc");
        }


          // 设置PageOffice组件服务页面
         PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "/pageoffice/server.aspx";
          // 设置保存文件页面
         PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx";
          //添加自定义的保存按钮
         PageOfficeCtrl1.AddCustomToolButton("保存","Save()",1);
          // 打开文档
         PageOfficeCtrl1.WebOpen(filePath,PageOffice.OpenModeType.docNormalEdit,"Tom");

    }
}