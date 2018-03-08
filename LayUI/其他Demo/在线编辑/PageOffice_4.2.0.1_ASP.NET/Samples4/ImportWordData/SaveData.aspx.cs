using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using PageOffice.WordReader;
public partial class SubmitWord_SaveFilePage : System.Web.UI.Page
{
    public String ErrorMsg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string docID = Request.QueryString["id"];
        string connstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|demo.mdb";
        OleDbConnection conn = new OleDbConnection(connstring);
        conn.Open();

        //-----------  PageOffice 服务器端编程开始  -------------------//

        PageOffice.WordReader.WordDocument doc = new WordDocument();
        String sName = doc.OpenDataRegion("PO_name").Value;
        String sDept = doc.OpenDataRegion("PO_dept").Value;
        String sCause = doc.OpenDataRegion("PO_cause").Value;
        String sNum = doc.OpenDataRegion("PO_num").Value;
        String sDate = doc.OpenDataRegion("PO_date").Value;
        if (sName.Equals(""))
        {
            ErrorMsg = ErrorMsg + "<li>申请人</li>";
        }
        if (sDept.Equals(""))
        {
            ErrorMsg = ErrorMsg + "<li>部门名称</li>";
        }
        if (sCause.Equals(""))
        {
            ErrorMsg = ErrorMsg + "<li>请假原因</li>";


        }
        if (sDate.Equals(""))
        {
            ErrorMsg = ErrorMsg + "<li>日期</li>";
        }

        try
        {
            if (sNum != "")
            {
                if (Int32.Parse(sNum) < 0)
                {
                    ErrorMsg = ErrorMsg + "<li>请假天数不能是负数</li>";
                }
            }
            else
            {
                ErrorMsg = ErrorMsg + "<li>请假天数</li>";
            }
        }
        catch (Exception Ex)
        {
            ErrorMsg = ErrorMsg + "<li><font color=red>注意：</font>请假天数必须是数字</li>";
        }

        if (ErrorMsg == "") {
		String sql =  "insert into leaveRecord(Name,Dept,Cause,Num,SubmitTime) values('"
              + sName + "','" + sDept + "','" + sCause + "'," + sNum + ",'"+sDate+"')";
        OleDbCommand cmd = new OleDbCommand(sql, conn);
        OleDbDataReader reader = cmd.ExecuteReader();
	    Response.Write("提交的数据为：<br/>");
		Response.Write("姓名："+sName+"<br/>");
		Response.Write("部门："+sDept+"<br/>");
		Response.Write("原因："+sCause+"<br/>");
		Response.Write("天数："+sNum+"<br/>");
        Response.Write("日期：" + sDate + "<br/>");
        doc.ShowPage(578, 380);
        
	} else {
	ErrorMsg = "<div style='color:#FF0000;'>请修改以下信息：</div> "
				+ ErrorMsg;
    doc.ShowPage(578, 380);
	}
        doc.Close();
	    conn.Close();
	//-----------  PageOffice 服务器端编程结束  -------------------//
        
    }
}
