<%@ Page Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true" Inherits="frmRightInfo" Title="权限信息" Codebehind="frmRightInfo.aspx.cs" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />上层目录:<asp:DropDownList ID="ddlUp" runat="server">
    </asp:DropDownList>
    权限名称:<asp:TextBox ID="txtRightName" runat="server" Width="140px"></asp:TextBox>权限/URL:<asp:TextBox 
        ID="txtRightCode" runat="server" Width="140px"></asp:TextBox>
    <asp:Button
        ID="btnChaXun" runat="server" Text="查询" onclick="btnChaXun_Click" />  <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click">新增</asp:LinkButton>
    <asp:GridView ID="grvInfo" runat="server" Width="90%" AutoGenerateColumns="False"
        CellPadding="2" ForeColor="#1B2761" OnDataBound="grvInfo_DataBound" OnRowDeleting="grvInfo_RowDeleting"
        Font-Size="14px" CssClass="gridveiwcss">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="PageDes" HeaderText="权限名称" />
            <asp:BoundField DataField="PageName" HeaderText="权限/URL" />
            <asp:BoundField DataField="NodeLevel" HeaderText="上层目录" />
            <asp:BoundField DataField="XianShiFlag" HeaderText="是否显示" />
            <asp:BoundField DataField="XianShiShunXu" HeaderText="显示顺序" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="frmRIghtInfoDetails.aspx?ID=<%# Eval("ID") %>">修改</a>
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
  
</asp:Content>
