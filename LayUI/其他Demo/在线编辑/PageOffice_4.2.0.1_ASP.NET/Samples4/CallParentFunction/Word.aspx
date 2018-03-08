<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="CallParentFunction_Word" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--PageOffice.js和jquery.min.js文件一定要引用-->
     <script type="text/javascript" src="../pageoffice/js/jquery.min.js"></script>
     <script type="text/javascript" src="../pageoffice/js/pageoffice.js" id="po_js_main"></script>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        function Close() {
            POBrowser.closeWindow();
        }
        function increaseCount(value) {
            var sResult = POBrowser.callParentJs("updateCount(" + value + ");");
            document.getElementById("PageOfficeCtrl1").Alert("现在父窗口Count的值为：" + sResult);
        }
        function increaseCountAndClose(value) {
            var sResult = POBrowser.callParentJs("updateCount(" + value + ");");
            POBrowser.closeWindow();
        }
    </script>
 
    <input type="button" value="设置父窗口Count的值加1" onclick="increaseCount(1);" />
	<input type="button" value="设置父窗口Count的值加5，并关闭窗口" onclick="increaseCountAndClose(5);" /></br>
    <div style=" width:auto; height:700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
