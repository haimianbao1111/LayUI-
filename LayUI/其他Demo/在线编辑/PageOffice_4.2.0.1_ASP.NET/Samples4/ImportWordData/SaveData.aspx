<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaveData.aspx.cs" Inherits="SubmitWord_SaveFilePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<div class="errTopArea" style="TEXT-ALIGN:left">[提示标题：这是一个开发人员可自定义的对话框]</div>
		<div class="errMainArea" id="error163">
			<div class="errTxtArea" style="HEIGHT:150px; TEXT-ALIGN:left">
				<b class="txt_title">
					<ul>
					<%=ErrorMsg%>
					</ul>					
				</b>				
			</div>
		</div>
</body>
</html>
