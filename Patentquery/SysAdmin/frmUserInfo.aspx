<%@ Page Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true"
    Inherits="frmUserInfo" Title="用户管理" CodeBehind="frmUserInfo.aspx.cs" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    &nbsp;&nbsp; 帐号:<asp:TextBox ID="txtZhangHao" runat="server"></asp:TextBox>&nbsp;
    姓名:<asp:TextBox ID="txtXingMing" runat="server"></asp:TextBox>&nbsp; 
    <asp:Label ID="Label1" runat="server" Text="用户类型:"></asp:Label>
    <asp:DropDownList
        ID="ddlYongHuLeiXing" runat="server">
        <asp:ListItem>全部</asp:ListItem>
        <asp:ListItem>系统管理</asp:ListItem>
        <asp:ListItem>企业</asp:ListItem>
        <asp:ListItem>政府</asp:ListItem>
        <asp:ListItem>个人</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="btnChaXun" runat="server" Text="查询" OnClick="btnChaXun_Click" />
    &nbsp;<asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click">新增</asp:LinkButton>
    <asp:GridView ID="grvInfo" runat="server" Width="90%" AutoGenerateColumns="False"
        CellPadding="2" ForeColor="#1B2761" OnDataBound="grvInfo_DataBound" OnRowDeleting="grvInfo_RowDeleting"
        Font-Size="14px" CssClass="gridveiwcss" AllowPaging="True" 
        onpageindexchanging="grvInfo_PageIndexChanging" PageSize="20">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="UserName" HeaderText="帐号" />
            <asp:BoundField DataField="RealName" HeaderText="姓名" />
            <asp:BoundField DataField="YongHuLeiXing" HeaderText="用户类型">
                <HeaderStyle Width="60px" />
                <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="LianXiDianHua" HeaderText="联系电话" Visible="false" />
            <asp:BoundField DataField="ShouJi" HeaderText="手机" Visible="false" />
            <asp:BoundField DataField="TongXinDiZhi" HeaderText="通信地址" Visible="false" />
            <asp:BoundField DataField="EMail" HeaderText="邮箱地址" Visible="false" />
            <asp:BoundField DataField="RoleName" HeaderText="角色" />

  <asp:TemplateField HeaderText="组织机构代码证">
                <ItemTemplate>
                    <a href="frmZZJGDMZ.aspx?ID=<%# Eval("ID") %>" target="_blank">查看</a>
                </ItemTemplate>
            </asp:TemplateField>

            


            <asp:TemplateField>
                <ItemTemplate>
                    <a href="frmUserInfoDetails.aspx?ID=<%# Eval("ID") %>">修改</a>
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
    <asp:HiddenField ID="hfUserLeiXing" runat="server" />
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
