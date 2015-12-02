<%@ Page Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true" Inherits="frmSHRight" Title="权限管理" Codebehind="frmSHRight.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="left" valign="top">
                审核人：
            </td>
            <td align="left" valign="top">
                被审核人
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <asp:ListBox ID="listUserInfo" runat="server" AutoPostBack="True" 
                    OnSelectedIndexChanged="listUserInfo_SelectedIndexChanged" Height="277px" 
                    Width="137px">
                </asp:ListBox>
            </td>
            <td align="left" valign="top">
                <asp:CheckBoxList ID="chkUserInfo" runat="server" RepeatColumns="6" 
                    RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td align="center" valign="top" colspan="2">
                <asp:Button ID="btnBaoCun" runat="server" Text="保存" onclick="btnBaoCun_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
