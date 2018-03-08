<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word3.aspx.cs" Inherits="POBrowserTopic_Word3" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
   <title>主页面传递参数到子页面</title>
       <!-- PageOffice.js文件一定要引用 -->
       <script type="text/javascript" src="../pageoffice/js/jquery.min.js"></script>
       <script type="text/javascript" src="../pageoffice/js/pageoffice.js" id="po_js_main"></script>
       <script type="text/javascript">
          function Save() {
              document.getElementById("PageOfficeCtrl1").WebSave();
               POBrowser.closeWindow();
        }
         </script>
</head>
<body>

              获取index.aspx中第一个文本框中的值：</br>
			  代码：txt=Session["txt"];</br>
			  输出：<%=txt %></br></br>
    <form id="form1" runat="server" >
    <div style=" width:auto; height:700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>