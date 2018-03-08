﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        //获取后台添加的书签名称字符串
        function getBkNames() {
            var bkNames = document.getElementById("PageOfficeCtrl1").DataRegionList.DefineNames;
            return bkNames;
        }

        //获取后台添加的书签文本字符串
        function getBkConts() {
            var bkConts = document.getElementById("PageOfficeCtrl1").DataRegionList.DefineCaptions;
            return bkConts;
        }

        //定位书签
        function locateBK(bkName) {
            var drlist = document.getElementById("PageOfficeCtrl1").DataRegionList;
            drlist.GetDataRegionByName(bkName).Locate();
            document.getElementById("PageOfficeCtrl1").Activate();
            window.focus();
            
        }

        //添加书签
        function addBookMark(param) {
            var tmpArr = param.split("=");
            var bkName = tmpArr[0];
            var content = tmpArr[1];
            var drlist = document.getElementById("PageOfficeCtrl1").DataRegionList;
            drlist.Refresh();
            try {
                document.getElementById("PageOfficeCtrl1").Document.Application.Selection.Collapse(0);
                drlist.Add(bkName, content);
                return "true";
            } catch (e) {
                return "false";
            }
        }
        //删除书签
        function delBookMark(bkName) {
            var drlist = document.getElementById("PageOfficeCtrl1").DataRegionList;
            try {
                drlist.Delete(bkName);
                return "true";
            } catch (e) {
                return "false";
            }
        }

        //遍历当前Word中已存在的书签名称
        function checkBkNames() {
            var drlist = document.getElementById("PageOfficeCtrl1").DataRegionList;
            drlist.Refresh();
            var bkName = "";
            var bkNames = "";
            for (var i = 0; i < drlist.Count; i++) {
                bkName = drlist.Item(i).Name;
                bkNames += bkName.substr(3) + ",";
            }
            return bkNames.substr(0, bkNames.length - 1);
        }

        //遍历当前Word中已存在的书签文本
        function checkBkConts() {
            var drlist = document.getElementById("PageOfficeCtrl1").DataRegionList;
            drlist.Refresh();
            var bkCont = "";
            var bkConts = "";
            for (var i = 0; i < drlist.Count; i++) {
                bkCont = drlist.Item(i).Value;
                bkConts += bkCont + ",";
            }
            return bkConts.substr(0, bkConts.length - 1);
        }
    </script>
    
    <script type="text/javascript">
        function Save() {
            document.getElementById("PageOfficeCtrl1").WebSave();
        }
        function ShowDefineDataRegions() {
            document.getElementById("PageOfficeCtrl1").ShowHtmlModelessDialog("DataRegionDlg.htm", "parameter=xx", "left=300px;top=390px;width=520px;height=410px;frame:no;");

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 1000px; height: 800px;">
        <%--<button onclick="my();">
            管理书签</button>--%>
        <%--<input id="Button1" type="button" value="button" onclick="delBookMark('PO_use')" />--%>
        <%-- <input type="button"  value="定位" onclick="locateBK('')"/>
        <input id="Button1" type="button" value="button" onclick="addBookMark('','')" />--%>
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="True" Menubar="False">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
