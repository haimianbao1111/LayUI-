<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Excel.aspx.cs" Inherits="word" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
  <head>
    <title>最简单的打开保存Excel文件</title>
     <!-- PageOffice.js必须引用 -->
	<script type="text/javascript" src="../pageoffice/js/jquery.min.js"></script>
    <script type="text/javascript" src="../pageoffice/js/pageoffice.js" id="po_js_main"></script>
 <style>
#main{
	width:750px;
	height:800px;
	border:#83b3d9 2px solid;
	background:#f2f7fb;
	
}
#shut{
	width:45px;
	height:30px;
	float:right;
	margin-right:-1px;
}
#shut:hover{
	}
</style>
</head >
<body style="overflow:hidden" >
    <!-- PageOffice.js必须引用 -->
	<script type="text/javascript" src="../pageoffice/js/jquery.min.js"></script>
    <script type="text/javascript" src="../pageoffice/js/pageoffice.js" id="po_js_main"></script>
       <script type="text/javascript">
          function Save() {
              document.getElementById("PageOfficeCtrl1").WebSave();
        }
           function Close() {
               POBrowser.closeWindow();
        }
	</script>
<div id="main">
<div id="shut"><img src="../js/close.png"  onclick="Close()" title="关闭" /></div>
<div id="content"  style="height:750px;width:750px;overflow:hidden;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="False" 
            onload="PageOfficeCtrl1_Load">
        </po:PageOfficeCtrl>
    </div>
  
</div>
</body>
</html>
