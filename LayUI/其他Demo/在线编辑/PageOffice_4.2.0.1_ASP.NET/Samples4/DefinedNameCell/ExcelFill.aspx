﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExcelFill.aspx.cs" Inherits="SubmitExcel_SubmitExcel" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function Save() {
            document.getElementById("PageOfficeCtrl1").WebSave();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    A1、B1单元格的数据是使用后台程序填充进去的，请查看ExcelFill.aspx.cs的代码。
    <div style="width: 1000px; height: 800px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
