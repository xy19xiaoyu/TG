<%@ Page Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true" Inherits="frmRightInfoDetails" Title="角色管理" Codebehind="frmRightInfoDetails.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function TreeViewCheckBox_Click(e) {
            if (window.event == null)
                o = e.target;
            else
                o = window.event.srcElement;
            if (o.tagName == "INPUT" && o.type == "checkbox") {
                __doPostBack("", "");
            }
        }   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="left" valign="top">
                权限代码：
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txtRightCode" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                权限名称：
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txtRightName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">上层目录：
            </td>
            <td align="left" valign="top">
                <asp:DropDownList ID="ddlUp" runat="server">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>   <tr>
            <td align="left" valign="top">显示顺序：
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txtShunXu" runat="server">0</asp:TextBox>
                &nbsp;
            </td>
        </tr>  <tr>
            <td align="left" valign="top">是否显示：
            </td>
            <td align="left" valign="top">
                <asp:CheckBox ID="chkXianShi" runat="server" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
            </td>
            <td align="left" valign="top">
                <asp:Button ID="btnQueDing" runat="server" Text="保存" OnClick="btnQueDing_Click" />
                <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="返回" />
            </td>
        </tr>
    </table>
</asp:Content>
