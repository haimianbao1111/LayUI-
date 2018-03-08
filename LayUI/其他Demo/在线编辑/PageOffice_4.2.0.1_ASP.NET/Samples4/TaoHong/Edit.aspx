﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Edit" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="images/csstg.css" rel="stylesheet" type="text/css" />
    <!-- PageOffice.js必须引用 -->
	<script type="text/javascript" src="../pageoffice/js/jquery.min.js"></script>
    <script type="text/javascript" src="../pageoffice/js/pageoffice.js" id="po_js_main"></script>
</head>
<body>
    <form id="form2">
    <div id="header">
        <div style="float: left; margin-left: 20px;">
            <img src="images/logo.jpg" height="30" /></div>
        <ul>
            <li><a target="_blank" href="http://www.zhuozhengsoft.com">卓正网站</a></li>
            <li><a target="_blank" href="http://www.zhuozhengsoft.com/poask/index.asp">客户问吧</a></li>
            <li><a href="#">在线帮助</a></li>
            <li><a target="_blank" href="http://www.zhuozhengsoft.com/contact-us.html">联系我们</a></li>
        </ul>
    </div>
    <div id="content">
        <div id="textcontent" style="width: 1000px; height: 800px;">
        <div class="flow4">
					<a href="#" onclick="POBrowser.closeWindow();"> <img alt="返回" src="images/return.gif"
							border="0" />文件列表</a>
					<span style="width: 100px;"> </span><strong>文档主题：</strong>
					<span style="color: Red;">测试文件</span>
		</div>
              <script type="text/javascript">
        function Save() {
            document.getElementById("PageOfficeCtrl1").WebSave();
        }

        //全屏/还原
        function IsFullScreen() {
            document.getElementById("PageOfficeCtrl1").FullScreen = !document.getElementById("PageOfficeCtrl1").FullScreen;
        }

    </script>
            <!--**************   卓正 PageOffice组件 ************************-->
            <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
            </po:PageOfficeCtrl>
        </div>
    </div>
    <div id="footer">
        <hr width="1000" />
        <div>
            Copyright (c) 2012 北京卓正志远软件有限公司</div>
    </div>
    </form>
</body>
</html>
