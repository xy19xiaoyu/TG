<%@ Page Title="用户审核" Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master"
    AutoEventWireup="true" CodeBehind="frmShenHe.aspx.cs" Inherits="Patentquery.SysAdmin.frmShenHe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    帐号:<asp:TextBox ID="txtZhangHao" runat="server"></asp:TextBox>姓名:<asp:TextBox ID="txtXingMing"
        runat="server"></asp:TextBox>
    <asp:Button ID="btnChaXun" runat="server" Text="查询" OnClick="btnChaXun_Click" />
    <asp:GridView ID="grvInfo" runat="server" Width="90%" AutoGenerateColumns="False"
        CellPadding="2" ForeColor="#1B2761" OnDataBound="grvInfo_DataBound" OnRowDeleting="grvInfo_RowDeleting"
        Font-Size="14px" CssClass="gridveiwcss" AllowPaging="True" OnPageIndexChanging="grvInfo_PageIndexChanging"
        PageSize="20">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="UserName" HeaderText="帐号" />
            <asp:BoundField DataField="RealName" HeaderText="姓名" />
            <asp:BoundField DataField="YongHuLeiXing" HeaderText="用户类型" />
            <asp:BoundField DataField="LianXiDianHua" HeaderText="联系电话" Visible="false" />
            <asp:BoundField DataField="ShouJi" HeaderText="手机" Visible="false" />
            <asp:BoundField DataField="TongXinDiZhi" HeaderText="通信地址" Visible="false" />
            <asp:BoundField DataField="EMail" HeaderText="邮箱地址" Visible="false" />
            <asp:BoundField DataField="RoleName" HeaderText="角色" Visible="false" />
            <asp:TemplateField HeaderText="组织机构代码证">
                <ItemTemplate>
                    <a href="frmZZJGDMZ.aspx?ID=<%# Eval("ID") %>" target="_blank">查看</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="审核">
                <ItemTemplate>
                    <a href="frmUserInfoDetails.aspx?SH=!@x@!&ID=<%# Eval("ID") %>">审核</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" HeaderText="删除" />
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:HiddenField ID="hfUserLeiXing" runat="server" />
</asp:Content>
