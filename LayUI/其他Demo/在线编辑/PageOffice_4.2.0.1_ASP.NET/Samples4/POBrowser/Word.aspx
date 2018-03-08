<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="POBrowser_Word" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"> 
<html xmlns="http://www.w3.org/1999/xhtml"> 
  <head>
   <title>最简单的打开保存Word文件</title>
</head>
<body>
    <script type="text/javascript">
          function Save() {
             document.getElementById("PageOfficeCtrl1").WebSave();
             
           }
    </script>
    <div style="position:absolute;left:5px;top:10px; width:1100px; height:100px;"><img src="../images/oa1.png" /></div>
    <div style="position:absolute;left:5px;top:66px;width:1175px; height:670px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl>
    </div>

</body>
</html>
