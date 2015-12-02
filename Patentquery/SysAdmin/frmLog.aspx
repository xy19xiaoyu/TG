<%@ Page Title="日志管理" Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true"
    CodeBehind="frmLog.aspx.cs" Inherits="Patentquery.SysAdmin.frmLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
        <script language="javascript" type="text/javascript" src="../JS/jDatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />    <br />今日访问量：<asp:Label ID="lblToDay" runat="server" Text="Label"></asp:Label> 总访问量：<asp:Label ID="lblZong" runat="server" Text="Label"></asp:Label>
    <br />用户类型：<asp:DropDownList ID="ddlYongHuLX" runat="server" 
        AutoPostBack="True" onselectedindexchanged="ddlYongHuLX_SelectedIndexChanged">
        <asp:ListItem>请选择</asp:ListItem>
         <asp:ListItem>游客</asp:ListItem>
        <asp:ListItem>个人 </asp:ListItem>
        <asp:ListItem>企业</asp:ListItem>
        <asp:ListItem>政府</asp:ListItem>
        <asp:ListItem>系统管理  </asp:ListItem>
    </asp:DropDownList>用户名：<asp:DropDownList ID="ddlUserName" runat="server">
    </asp:DropDownList>
    操作时间:<asp:TextBox ID="txtDateStart" CssClass="Wdate" onClick="var d5222=$dp.$('ctl00_ContentPlaceHolder1_txtDateEnd');WdatePicker({onpicked:function(){d5222.focus();},maxDate:'#F{$dp.$D(\'ctl00_ContentPlaceHolder1_txtDateEnd\')}'})"
        runat="server" Width="120px"></asp:TextBox>到<asp:TextBox
        ID="txtDateEnd" runat="server" CssClass="Wdate" onFocus="WdatePicker({minDate:'#F{$dp.$D(\'ctl00_ContentPlaceHolder1_txtDateStart\')}'})"
        Width="120px"></asp:TextBox>
    <asp:Button ID="btnChaXun" runat="server" Text="查询" OnClick="btnChaXun_Click" />
    <asp:GridView ID="grvInfo" runat="server" Width="90%" AutoGenerateColumns="False"
        CellPadding="2" ForeColor="#1B2761" Font-Size="14px" 
        CssClass="gridveiwcss" AllowPaging="True" 
        onpageindexchanging="grvInfo_PageIndexChanging" PageSize="20">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="IP" HeaderText="IP" />
            <asp:BoundField DataField="UserName" HeaderText="用户" />
            <asp:BoundField DataField="LanMu" HeaderText="访问模块" />
            <asp:BoundField DataField="ShiJian" HeaderText="登录时间" />
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
