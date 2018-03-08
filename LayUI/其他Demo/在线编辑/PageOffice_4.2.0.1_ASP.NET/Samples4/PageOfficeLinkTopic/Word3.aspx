<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word3.aspx.cs" Inherits="PageOfficeLinkTopic_Word3" %><%@ Register TagPrefix="po" Namespace="PageOffice" Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>主页面传递参数到子页面2</title>
       <script type="text/javascript"
			src="js/jquery-1.6.min.js"></script>
</head>
<body>
 <script type="text/javascript">
     function Save() {
         document.getElementById("PageOfficeCtrl1").WebSave();
         //关闭PageOfficeLink弹出的窗口
         window.external.close();
     }
  

    </script>
              获取index.aspx中第一个文本框中的值：</br>
			  代码：String text=Session["text"].ToString();</br>
			  输出：<%=text %></br></br>
    <form id="form1" runat="server">
    <div style=" width:auto; height:700px;">
     <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl> 

    </div>
    </form>
</body>
</html>
