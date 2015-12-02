<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true"
    CodeBehind="frmSetMailSvs.aspx.cs" Inherits="Patentquery.SysAdmin.frmSetMailSvs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div style="text-align: center">
        <table style="width: 100%;">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td align="right">
                    &nbsp;用户名：
                </td>
                <td align="left">
                    <asp:TextBox ID="txbMailUser" runat="server"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td align="right">
                    密 码：
                </td>
                <td align="left">
                    <asp:TextBox ID="txbMailPwd" runat="server" TextMode="Password"></asp:TextBox>
                    &nbsp;<label for="chkShowPwd"><input id="chkShowPwd" type="checkbox" />明码显示</label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="left">
                    <asp:Button ID="Button1" runat="server" Text="确定" />
                    <asp:Button ID="Button2" runat="server" Text="取消" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
