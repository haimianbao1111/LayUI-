<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="ImportWordData_Word" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>导入文件并提交数据</title>
    <script type="text/javascript">
              //导入文件
              function importData() {
                 document.getElementById("PageOfficeCtrl1").WordImportDialog();
                }
              //提交数据
              function submitData() {
                 document.getElementById("PageOfficeCtrl1").WebSave();

               }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="color:Red">请导入“/Samples4/ImportWordData”下的ImportWord.doc文档查看演示效果。</div>
    <div style="width: auto; height: 700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" Menubar="False">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
