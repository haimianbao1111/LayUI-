<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Excel.aspx.cs" Inherits="_Excel" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入Excel文件并提交数据</title>
    <script type="text/javascript">
        function importData() {
            document.getElementById("PageOfficeCtrl1").ExcelImportDialog();
        }

        function submitData() {
            document.getElementById("PageOfficeCtrl1").WebSave();
//            if (document.getElementById("PageOfficeCtrl1").CustomSaveResult == "ok")//
//                alert("数据提交成功！");
//            else
//                alert("数据提交失败！");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="color:Red">请导入“/Samples4/ImportExcelData”下的ImportExcel.xls文档查看演示效果。</div>
    <div style=" width:auto; height:600px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
