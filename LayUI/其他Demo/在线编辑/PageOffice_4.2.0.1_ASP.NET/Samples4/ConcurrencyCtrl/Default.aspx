﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ConcurrencyCtrl_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>演示：并发控制</title>
<script type="text/javascript" src="../pageoffice/js/jquery.min.js"></script>
<script type="text/javascript" src="../pageoffice/js/pageoffice.js" id="po_js_main"></script>
</head>
<body style=" font-size:12px;">
    <form id="form1" runat="server">
    <div style=" border:solid 1px RoyalBlue; width:500px; text-align:center;  height:200px; margin:100px auto;">
        <div style=" margin-top:50px; height:170px; ">
             <span style=" color:Red;">操作说明：</span><span>首先点击“张三打开文件”会在弹出窗口中打开Word文档（不要关闭窗口），<br />再点击“李四
打开文件”，看并发控制效果。</span><br /><br />
        1：<a href="javascript:POBrowser.openWindow('Word.aspx?userid=1' , 'width=1200px;height=800px;');" >张三打开文件</a><br /><br />
        2：<a href="javascript:POBrowser.openWindow('Word.aspx?userid=2' , 'width=1200px;height=800px;');" >李四打开文件</a>
        </div>
        <div style=" color:Gray;">Copyright &copy 2013 北京卓正志远软件有限公司 </div>
    </div>
    </form>
</body>
</html>
