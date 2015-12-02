<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Patentquery.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="panxy" runat="server" Visible="false" Height="366px" 
            Width="700px">
            <fieldset style="width: 700px;">
                <legend>数据源</legend>数据源 :<asp:DropDownList ID="ddlDBType" runat="server">
                    <asp:ListItem Value="0">CN</asp:ListItem>
                    <asp:ListItem Value="1">DOCDB</asp:ListItem>
                    <asp:ListItem Value="2">DWPI</asp:ListItem>
                </asp:DropDownList>
            </fieldset>
            <br />
            <fieldset style="width: 700px;">
                <legend>连接池操作</legend>&nbsp;
                <asp:Button ID="Button6" runat="server" Height="23px" OnClick="Button2_Click" Text="登录" />
                &nbsp;
                <asp:Button ID="Button5" runat="server" Height="23px" OnClick="Button4_Click" Text="注销" />
                &nbsp;<asp:Button ID="btnGrow" runat="server" OnClick="btnGrow_Click" Text="增长连接池" />
                &nbsp;
                </fieldset>
            <br />
            <fieldset style="width: 700px; height: 107px;">
                <legend>检索测试</legend>请输入检索式：<asp:TextBox ID="txtSearchPattern" runat="server" 
                    Width="350px"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="检索" 
                    Height="23px" Width="58px" />
                    <asp:Button ID="Button7" runat="server" OnClick="Button7_Click1" 
                    Height="23px" Width="58px"  Text="检索" />
                    <asp:Button ID="Button8" runat="server" Height="23px" 
                    OnClick="Button8_Click" Text="新引擎重新连接" Width="104px" />
                    <br />
                <br />
                    <asp:Label ID="lblmessage0" runat="server">检索结果:</asp:Label>
                <asp:Label ID="lblmessage" runat="server"></asp:Label>
            </fieldset>
            <br />
            <fieldset style="width: 700px;">
                <legend>连接池信息 </legend>
                <asp:Label ID="txtshow" runat="server" Height="139px" Width="533px" />
            </fieldset>
            <br />
        </asp:Panel>
        <asp:Panel ID="Panel1" runat="server" Visible="true" Height="42px" Width="555px">
            请输入密码：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Button" Height="23px" />
        </asp:Panel>
    </div>
    </form>
</body>
</html>
