<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ppt.aspx.cs" Inherits="word" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
 <head>
<style>
#main{
	width:1040px;
	height:890px;
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
<body style="margin:0; padding:0;border:0px; overflow:hidden" scroll="no">
    <!-- PageOffice.js必须引用 -->
	<script type="text/javascript" src="../pageoffice/js/jquery.min.js"></script>
    <script type="text/javascript" src="../pageoffice/js/pageoffice.js" id="po_js_main"></script>
       <script type="text/javascript">
           function SaveFile() {
              document.getElementById("PageOfficeCtrl1").WebSave();
        }
           function Close() {
               POBrowser.closeWindow();
        }
        
	</script>
<div id="main">
<div id="shut"><img src="../js/close.png"  onclick="Close()" title="关闭" /></div>
<div id="content"  style="height:850px;width:1036px;overflow:hidden;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" 
            onload="PageOfficeCtrl1_Load">
        </po:PageOfficeCtrl>
    </div>
  
</div>
</body>
</html>
