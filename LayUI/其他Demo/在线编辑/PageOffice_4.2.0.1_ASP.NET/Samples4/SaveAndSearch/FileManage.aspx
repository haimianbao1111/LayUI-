<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileManage.aspx.cs" Inherits="SaveAndSearch_FileManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function onColor(td) {
            td.style.backgroundColor = '#D7FFEE';
        }
        function offColor(td) {
            td.style.backgroundColor = '';
        }
        function getFocus() {
            var str = document.getElementById("Input_KeyWord").value;
            if (str == "请输入关键字") {
                document.getElementById("Input_KeyWord").value = "";
            }

        }
        function lostFocus() {
            var str = document.getElementById("Input_KeyWord").value;
            if (str.length <= 0) {
                document.getElementById("Input_KeyWord").value = "请输入关键字";
            }
        }
        function copyKeyToInput(key) {
            document.getElementById("Input_KeyWord").value = key;
        }

    </script>
    <!-- PageOffice.js必须引用 -->
    <script type="text/javascript" src="../pageoffice/js/jquery.min.js"></script>
    <script type="text/javascript" src="../pageoffice/js/pageoffice.js" id="po_js_main"></script>
</head>
<body>
    <!--header-->
    <div class="zz-headBox br-5 clearfix">
        <div class="zz-head mc">
            <!--logo-->
            <div class="logo fl">
                <a href="#">
                    <img src="images/logo.png" alt="" /></a></div>
            <!--logo end-->
            <ul class="head-rightUl fr">
                <li><a href="#">卓正网站</a></li>
                <li><a href="#">客户问吧</a></li>
                <li class="bor-0"><a href="#">联系我们</a></li>
            </ul>
        </div>
    </div>
    <!--header end-->
    <!--content-->
    <div class="zz-content mc clearfix pd-28">
        <div class="demo mc">
            <h2 class="fs-16">
                PageOffice 实现Word文档的在线编辑保存和全文关键字搜索</h2>
        </div>
        <div class="demo mc">
            <h3 class="fs-12">
                搜索文件</h3>
            <form id="form1" runat="server">
            <table class="text" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td style="font-size: 9pt" align="left">
                        通过文档内容中的关键字搜索文档&nbsp;&nbsp;&nbsp;
                    </td>
                    <td align="center">
                        <input name="Input_KeyWord" id="Input_KeyWord" type="text" onfocus="getFocus()" onblur="lostFocus()"
                            class="boder" style="width: 180px;" value="请输入关键字" />
                    </td>
                    <td style="width: 221px;">
                        &nbsp;
                        <asp:Button ID="btnSearch" runat="server" Text="搜索文档" Width="86px" 
                            onclick="btnSearch_Click" />
                    </td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td colspan="2">&nbsp;<span style="color:Gray;">热门搜索：</span> 
                    <a href="#" style="color:#00217d;" onclick="copyKeyToInput('网站');">网站</a>、
                    <a href="#" style="color:#00217d;" onclick="copyKeyToInput('软件');">软件</a>、
                    <a href="#" style="color:#00217d;" onclick="copyKeyToInput('字体');">字体</a></td>
                </tr>
            </table>
            </form>
        </div>
        <div class="zz-talbeBox mc">
            <h2 class="fs-12">
                文档列表</h2>
            <table class="zz-talbe">
                <thead>
                    <tr>
                        <th width="22%">
                            文档名称
                        </th>
                        <th width="5%" style=" text-align:center;">
                            操作
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <%=strHtmls %>
                </tbody>
            </table>
        </div>
    </div>
    <!--content end-->
    <!--footer-->
    <div class="login-footer clearfix">
        Copyright &copy 2013 北京卓正志远软件有限公司</div>
    <!--footer end-->
</body>
</html>
