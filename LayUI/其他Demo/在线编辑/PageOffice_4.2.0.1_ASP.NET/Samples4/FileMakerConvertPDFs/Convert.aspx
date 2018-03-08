<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Convert.aspx.cs" Inherits="FileMakerConvertPDFs_Convert" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script language="javascript" type="text/javascript">
	         function OnProgressComplete() {
		            window.parent.convert(); //调用父页面的js函数
	        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <po:FileMakerCtrl ID="FileMakerCtrl1" runat="server">
    </po:FileMakerCtrl>
    <div>
    
    </div>
    </form>
</body>
</html>
