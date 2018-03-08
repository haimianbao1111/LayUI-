<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendParameters.aspx.cs" Inherits="SendParameters_SendParameters" %>

<%@ Register Assembly="PageOffice, Version=4.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        function Save() {
            document.getElementById("PageOfficeCtrl1").WebSave();
        }
        function SetFullScreen() {
            document.getElementById("PageOfficeCtrl1").FullScreen = !document.getElementById("PageOfficeCtrl1").FullScreen;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="font-size: 14px;">
    <div style="border:1px solid black;">PageOffice给保存页面传值的三种方式：</br>
             <span style="color: Red;">1.通过设置保存页面的url中的?给保存页面传递参数：</span></br>
              &nbsp;&nbsp;&nbsp;例: PageOfficeCtrl1.SaveFilePage = "SaveFile.aspx?id=1";</br>
              &nbsp;&nbsp;&nbsp;保存页面获取参数的方法：</br>
              &nbsp;&nbsp;&nbsp;string value=Request.QueryString["id"];</br>
    
              <span style="color: Red;">2.通过input隐藏域给保存页面传递参数：</span></br>
               &nbsp;&nbsp;&nbsp;例:<xmp> <input id="Hidden1" name="age" type="hidden" value="25" /></xmp></br>
               &nbsp;&nbsp;&nbsp;保存页面获取参数的方法：</br>
               &nbsp;&nbsp;&nbsp;string age=fs.GetFormField("age")</br>
               &nbsp;&nbsp;&nbsp;注意：获取Form控件传递过来的参数值，fs.GetFormField("参数名")方法中的参数名是当前控件的“name”属性而不是id，更多详细代码请查看SaveFile.aspx。<br>

              <span style="color: Red;">3.通过Form控件给保存页面传递参数(这里的Form控件包括输入框、下拉框、单选框、复选框、TextArea等类型的控件)：</span></br>
               &nbsp;&nbsp;&nbsp;例:<xmp> <input id="Text1" name="userName" type="text" value="张三" /></xmp></br>
               &nbsp;&nbsp;&nbsp;保存页面获取参数的方法：</br>
               &nbsp;&nbsp;&nbsp;string name=fs.GetFormField("userName");</br>
               &nbsp;&nbsp;&nbsp;注意：获取Form控件传递过来的参数值，fs.GetFormField("参数名")方法中的参数名是当前控件的“name”属性而不是id，更多详细代码请查看SaveFile.aspx。<br>
    </div>
        <span style="color: Red;">更新人员信息：</span><br />
        <input id="Hidden1" name="age" type="hidden" value="25" />
        <span style="color: Red;">姓名：</span><input id="Text1" name="userName" type="text" value="张三" /><br />
        <span style="color: Red;">性别：</span><select id="Select1" name="selSex">
            <option value="男">男</option>
            <option value="女">女</option>
        </select>
    </div>
    <!--PageOfficeCtrl控件-->
    <div style="width: auto; height: 700px;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" CustomToolbar="True" Menubar="True">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
