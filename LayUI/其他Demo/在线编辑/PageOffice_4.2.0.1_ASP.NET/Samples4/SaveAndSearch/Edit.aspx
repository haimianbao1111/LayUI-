<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="SaveAndSearch_Edit" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <!-- PageOffice.js必须引用 -->
    <script type="text/javascript" src="../pageoffice/js/jquery.min.js"></script>
    <script type="text/javascript" src="../pageoffice/js/pageoffice.js" id="po_js_main"></script>
    <script type="text/javascript">
       var strKey =POBrowser.getArgument();
        function Save() {
            if (strKey != "") SetKeyWord(strKey, false);
            document.getElementById("PageOfficeCtrl1").WebSave();
            //document.getElementById("PageOfficeCtrl1").CustomSaveResult获取的是保存页面的返回值
            if (document.getElementById("PageOfficeCtrl1").CustomSaveResult == "ok")
                  document.getElementById("PageOfficeCtrl1").Alert("保存成功");
            else
                document.getElementById("PageOfficeCtrl1").Alert(document.getElementById("PageOfficeCtrl1").CustomSaveResult);
        }

        function SetKeyWord(key, visible) { 
           if (key=="null"||"" == key) {
                document.getElementById("PageOfficeCtrl1").Alert("关键字为空。");
                return;
            }  
             var sMac = "function myfunc()" + "\r\n"
                        + "Application.Selection.HomeKey(6) \r\n"
                        + "Application.Selection.Find.ClearFormatting \r\n"
                        + "Application.Selection.Find.Replacement.ClearFormatting \r\n"
                        + "Application.Selection.Find.Text = \"" + key + "\" \r\n"

                        + "While (Application.Selection.Find.Execute()) \r\n"
                        +  "If (" + visible + ") Then \r\n"
                        +  "Application.Selection.Range.HighlightColorIndex = 7 \r\n"
                        +  "Else \r\n"
                        +  "Application.Selection.Range.HighlightColorIndex = 0 \r\n"
                        +  "End If \r\n"
                        +  "Wend \r\n"
                        +  "Application.Selection.HomeKey(6) \r\n"
                        + "End function";
            
            document.getElementById("PageOfficeCtrl1").RunMacro("myfunc", sMac);
            
        }
    </script>

</head>
<body>
    <a href="#"  onclick="POBrowser.closeWindow();">返回列表页</a>
    <form id="form1" runat="server">
    <input id="Button1" type="button" onclick="SetKeyWord( strKey,true)" value="高亮显示关键字" />
    <input id="Button2" type="button" onclick="SetKeyWord( strKey,false)" value="取消关键字显示" />
    <div style="width: auto; height: 700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="True" Menubar="False">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
