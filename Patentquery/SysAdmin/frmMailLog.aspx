<%@ Page Title="邮件管理" Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true"
    CodeBehind="frmMailLog.aspx.cs" Inherits="Patentquery.SysAdmin.frmMailLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Js/dateTime.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    发送时间:<asp:TextBox ID="txtDateStart" onfocus="javaScript:calendar();" runat="server"></asp:TextBox>到<asp:TextBox
        ID="txtDateEnd" runat="server" onfocus="javaScript:calendar();"></asp:TextBox>收件人:<asp:TextBox
            ID="txtShouJianRen" runat="server"></asp:TextBox>
    <asp:Button ID="btnChaXun" runat="server" Text="查询" OnClick="btnChaXun_Click" />
    <asp:GridView ID="grvInfo" runat="server" Width="90%" AutoGenerateColumns="False"
        CellPadding="2" ForeColor="#1B2761" Font-Size="14px" 
        CssClass="gridveiwcss" AllowPaging="True" 
        onpageindexchanging="grvInfo_PageIndexChanging" PageSize="20">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
          
            <asp:BoundField DataField="ShouJianRen" HeaderText="收件人" />
            <asp:BoundField DataField="YouJianMingCheng" HeaderText="邮件名称" />
            <asp:BoundField DataField="ZhuanLiQuYu" HeaderText="专利区域" />
            <asp:BoundField DataField="FaSongShiJian" HeaderText="发送时间" />
            <asp:BoundField DataField="FaSongZhuangTai" HeaderText="发送状态" />
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <br />
</asp:Content>
