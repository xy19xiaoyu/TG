<%@ Page Language="C#" AutoEventWireup="true" Inherits="Default" CodeBehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>后台管理系统</title>
    <link href="css/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="lgcontainer">
        <div style="height: 466px;">
            &nbsp;
        </div>
        <div style="height: 10px; width: 700px; float: left;">
            &nbsp;
        </div>
        <div style="float: left;">
            <div>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="lgbtcss"></asp:TextBox>
            </div>
            <div style="height: 46px;">
            </div>
            <div>
                <asp:TextBox ID="txtPWD" runat="server" TextMode="Password" CssClass="lgbtcss"></asp:TextBox>
            </div>
            <div style="height: 26px;">
            </div>
            <div>
                <asp:Button ID="btnLogin" runat="server" Text="登录" OnClick="btnLogin_Click" Width="70px" />&nbsp;&nbsp;&nbsp;
                <a href="../My/SmartQuery.aspx">返回前台</a></div>
        </div>
    </div>
    </form>
</body>
</html>
