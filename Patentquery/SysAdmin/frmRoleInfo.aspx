<%@ Page Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true" Inherits="frmRoleInfo" Title="角色管理" Codebehind="frmRoleInfo.aspx.cs" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    角色：<asp:TextBox ID="txtJueSe" runat="server"></asp:TextBox> <asp:Button ID="btnChaXun" runat="server" 
        Text="查询" onclick="btnChaXun_Click" />
    <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click">新增</asp:LinkButton>
    <asp:GridView ID="grvInfo" runat="server" Width="90%" AutoGenerateColumns="False"
        CellPadding="2" ForeColor="#1b2761" OnDataBound="grvInfo_DataBound" OnRowDeleting="grvInfo_RowDeleting"
        Font-Size="14px" CssClass="gridveiwcss">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="RoleName" HeaderText="角色名称">
                <ItemStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="PageDes" HeaderText="权限" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="frmRoleInfoDetails.aspx?ID=<%# Eval("ID") %>">修改</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:HiddenField ID="hfUserLeiXing" runat="server" />
</asp:Content>
