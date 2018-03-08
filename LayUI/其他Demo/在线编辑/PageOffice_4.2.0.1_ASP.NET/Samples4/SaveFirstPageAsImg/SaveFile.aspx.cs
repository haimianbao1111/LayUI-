using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SaveFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageOffice.FileSaver fs = new PageOffice.FileSaver();
        if (fs.FileExtName.Equals(".jpg"))
        {
            fs.SaveToFile(Server.MapPath("images/") + fs.FileName);
        }
        else {
            fs.SaveToFile(Server.MapPath("doc/") + fs.FileName);
        }
        fs.CustomSaveResult="ok";
        fs.Close();
    }
}
