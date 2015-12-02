<%@ Page Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true"
    Inherits="frmRoleInfoDetails" Title="角色管理" CodeBehind="frmRoleInfoDetails.aspx.cs"
    MaintainScrollPositionOnPostback="true" %>

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
    <asp:HiddenField ID="hfUserLeiXing" runat="server" />
    <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="left" valign="top">
                角色名称：
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txtProName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                权限：
            </td>
            <td align="left" valign="top">
                <asp:CheckBoxList ID="chkRight" runat="server">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
            </td>
            <td align="left" valign="top">
                <asp:TreeView ID="tTV" runat="server" ImageSet="Contacts" NodeIndent="10" ShowCheckBoxes="All"
                    OnTreeNodeCheckChanged="tTV_TreeNodeCheckChanged1" onclick="TreeViewCheckBox_Click(event)">
                    <ParentNodeStyle Font-Bold="True" ForeColor="#5555DD" />
                    <HoverNodeStyle Font-Underline="False" />
                    <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
                    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                        NodeSpacing="0px" VerticalPadding="0px" />
                </asp:TreeView>
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
