<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PDF.aspx.cs" Inherits="PDF" %>
<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>在线打开PDF文件</title>
    <style>
#main{
	width:1030px;
	height:900px;
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
    <!-- PageOffice.js必须引用 -->
	<script type="text/javascript" src="../pageoffice/js/jquery.min.js"></script>
    <script type="text/javascript" src="../pageoffice/js/pageoffice.js" id="po_js_main"></script>
</head>
<body style="overflow:hidden;">
    <form id="form1" runat="server">
    <!--**************   卓正 PageOffice 客户端代码开始    ************************-->
	<script language="javascript" type="text/javascript">
	    function AfterDocumentOpened() {
	        //alert(document.getElementById("PDFCtrl1").Caption);
	    }
	    function Print() {
	        document.getElementById("PDFCtrl1").ShowDialog(4);
	    }
	    function SwitchFullScreen() {
	        document.getElementById("PDFCtrl1").FullScreen = !document.getElementById("PDFCtrl1").FullScreen;
	    }
	    function SwitchBKMK() {
	        document.getElementById("PDFCtrl1").BookmarksVisible = !document.getElementById("PDFCtrl1").BookmarksVisible;
	    }
	    function SetPageReal() {
	        document.getElementById("PDFCtrl1").SetPageFit(1);
	    }
	    function SetPageFit() {
	        document.getElementById("PDFCtrl1").SetPageFit(2);
	    }
	    function SetPageWidth() {
	        document.getElementById("PDFCtrl1").SetPageFit(3);
	    }
	    function ZoomIn() {
	        document.getElementById("PDFCtrl1").ZoomIn();
	    }
	    function ZoomOut() {
	        document.getElementById("PDFCtrl1").ZoomOut();
	    }
	    function FirstPage() {
	        document.getElementById("PDFCtrl1").GoToFirstPage();
	    }
	    function PreviousPage() {
	        document.getElementById("PDFCtrl1").GoToPreviousPage();
	    }
	    function NextPage() {
	        document.getElementById("PDFCtrl1").GoToNextPage();
	    }
	    function LastPage() {
	        document.getElementById("PDFCtrl1").GoToLastPage();
	    }
	    function RotateRight() {
	        document.getElementById("PDFCtrl1").RotateRight();
	    }
	    function RotateLeft() {
	        document.getElementById("PDFCtrl1").RotateLeft();
	    }
	    function Close() {
	        POBrowser.closeWindow();
	    }
	</script>
    <!--**************   卓正 PageOffice 客户端代码结束    ************************-->
 <div id="main">
<div id="shut"><img src="../js/close.png"  onclick="Close()" title="关闭" /></div>
<div id="content"  style="height:850px;width:1028px;overflow:hidden;">
        <po:PDFCtrl ID="PDFCtrl1" runat="server" onload="PDFCtrl1_Load" 
            Theme="Office2007">
        </po:PDFCtrl>
    </div>
     </div>
    </form>
</body>
</html>
