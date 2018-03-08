<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Compose.aspx.cs" Inherits="ExaminationPaper_Compose" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">

         <%=operateStr %>

    </script>
     <!-- PageOffice.js必须引用 -->
	<script type="text/javascript" src="../pageoffice/js/jquery.min.js"></script>
    <script type="text/javascript" src="../pageoffice/js/pageoffice.js" id="po_js_main"></script>
</head>
<body>
    <a href="#"  onclick="POBrowser.closeWindow();">返回列表页</a>
    <form id="form1" runat="server">
    <div style="width: 100%; height: 700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" Menubar="False" 
            CustomToolbar="False">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
