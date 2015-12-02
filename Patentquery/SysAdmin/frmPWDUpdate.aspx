<%@ Page Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true"
    Inherits="frmPWDUpdate" Title="厦漳泉科技基础资源服务平台-专利平台后台[修改密码]" CodeBehind="frmPWDUpdate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    用 户 名：<asp:Label ID="lblUserName" runat="server" Text=""></asp:Label><br />
    密&nbsp;&nbsp;&nbsp;&nbsp;码：<asp:TextBox ID="txtPWD" runat="server"></asp:TextBox><br />
    真实姓名：<asp:TextBox ID="txtRealName" runat="server"></asp:TextBox><br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnQueDing" runat="server" OnClick="btnQueDing_Click" Text="确 定" />
</asp:Content>
