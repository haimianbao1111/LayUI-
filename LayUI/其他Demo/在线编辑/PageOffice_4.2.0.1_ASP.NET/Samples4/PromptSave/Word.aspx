<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Word.aspx.cs" Inherits="PromptSave_Word" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--PageOffice.js和jquery.min.js文件一定要引用-->
     <script type="text/javascript" src="../pageoffice/js/jquery.min.js"></script>
     <script type="text/javascript" src="../pageoffice/js/pageoffice.js" id="po_js_main"></script>
</head>
<body>
    <script type="text/javascript">
        function Save() {
            document.getElementById("PageOfficeCtrl1").WebSave();
        }
        function CloseFile() {
            POBrowser.closeWindow();
        }
        
        function BeforeBrowserClosed(){    
            if (document.getElementById("PageOfficeCtrl1").IsDirty){
                if(confirm("提示：文档已被修改，是否继续关闭放弃保存 ？"))
                {
                    return  true;
                    
                }else{
                
                    return  false;
                }
	         	
            }
        }
        
    </script>
    
    <form id="form1" runat="server">
    <div style=" width:auto; height:700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
