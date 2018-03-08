<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word2.aspx.cs" Inherits="PageOfficeLinkTopic_Word2" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>主页面传递参数到子页面1</title>
</head>
<body>
 <script type="text/javascript">
     function Save() {
         document.getElementById("PageOfficeCtrl1").WebSave();
     }
  

    </script>
    <form id="form1" runat="server">
    (1)获取index.aspx用session传递过来的userName的值：</br>
			    &nbsp;&nbsp;&nbsp;代码： String userName = Session["userName"].ToString();</br>
			    &nbsp;&nbsp;&nbsp;输出：UserName=<%=userName %></br></br>
		
     (2)获取index.aspx用？传递过来的id的值：</br>
			   &nbsp;&nbsp;&nbsp;代码：String id = Request.QueryString["id"];</br>
			   &nbsp;&nbsp;&nbsp;输出：id=<%=id %></br>
    <div style=" width:auto; height:700px;">
		<po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl> 
    </div>
    </form>
</body>
</html>

