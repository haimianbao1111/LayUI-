﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="word" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>最简单打开保存Word文件</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" width:auto; height:700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="False" >
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
