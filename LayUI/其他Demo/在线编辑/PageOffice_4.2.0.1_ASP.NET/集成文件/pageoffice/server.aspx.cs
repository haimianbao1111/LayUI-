// 重要提示：本页代码是卓正PageOffice开发平台的系统保留代码，请勿修改。
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace pageoffice
{
    public partial class server : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 重要提示：本页代码是卓正PageOffice开发平台的系统保留代码，请勿修改。
            PageOffice.POServer.Server ServerObj;
            // 如果出现“未将对象引用设置到对象的实例”的调试信息，请检查您在Web服务器上是否正确安装了卓正PageOffice开发平台的Setup.exe安装程序。
            ServerObj = new PageOffice.POServer.Server();
            ServerObj.DBConnectionString = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=|DataDirectory|seal.mdb";
            ServerObj.Run();

            string strOutPut = "欢迎使用 PageOffice 产品\r\n";
            String strRegInfo = "";
            if (ServerObj.SerialNumber != "")
            {
                strRegInfo = "注册码：" + ServerObj.SerialNumber + "<br>" + "版 本：" + ServerObj.VersionInfo + "<br>" + "用 户：" + ServerObj.Organization;
                if (ServerObj.TrialEndTime != "")
                    strRegInfo = strRegInfo + "<br>" + "此产品是<span style=\"color:Red;\">试用版</span>，试用结束时间是 " + ServerObj.TrialEndTime + "。";
                strRegInfo = strRegInfo + "<br>PageOffice Server Version: " + ServerObj.ServerVersion;
            }
            else
            {
                strRegInfo = "<span style=\"color:Red;\">此产品尚未注册激活。</span>";
            }
            strOutPut = strOutPut + "PageOffice 注册信息：" + strRegInfo + "\r\n";
            strOutPut = strOutPut + "PageOffice 运行信息：" + ServerObj.GetStatusLog() + "\r\n";
            //出于安全考虑，您只能在Web服务器上通过localhost方式来查看本页显示的PageOffice系统信息或者到临时文件夹里查看poserver_log.txt文件。
            string ServerIP = Request.ServerVariables["HTTP_HOST"].ToLower();
            if ((ServerIP.StartsWith("localhost")) || (ServerIP.StartsWith("127.0.0.1")))
            {
                LabelReg.Text = strRegInfo;
                LabelLog.Text = ServerObj.GetStatusLog();
            }
            else
            {
                try
                {
                    FileStream fs = new FileStream(Path.GetTempPath() + "poserver_log.txt", FileMode.Create);//D:\...\pageoffice No last "\"
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(strOutPut);
                    sw.Flush();
                    sw.Close();
                    fs.Close();

                    LabelReg.Text = "PageOffice 注册信息已生成，请到Web服务器临时文件夹里查看poserver_log.txt文件。" + "<br>PageOffice Server Version: " + ServerObj.ServerVersion;
                    LabelLog.Text = ServerObj.GetStatusLog();
                    //string tempFile = Path.GetTempPath();//如果您找不到临时文件夹位置，请打开这里的注释
                    //Response.Write(tempFile);
                }
                catch
                {
                    LabelReg.Text = "无法生成poserver_log.txt文件，可能是临时文件夹权限不够。出于安全考虑，您只能在Web服务器上通过localhost方式来查看本页显示的PageOffice系统信息。" + "<br>PageOffice Server Version: " + ServerObj.ServerVersion;
                    LabelLog.Text = ServerObj.GetStatusLog();
                }
            }
        }
    }
}
