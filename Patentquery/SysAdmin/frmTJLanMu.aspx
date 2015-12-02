<%@ Page Title="厦漳泉科技基础资源服务平台-专利平台后台[栏目访问题统计]" Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master"
    AutoEventWireup="true" CodeBehind="frmTJLanMu.aspx.cs" Inherits="Patentquery.SysAdmin.frmTJLanMu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript" src="../JS/jDatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <br />
    <div style="text-align: left; padding-left:35px">
        操作时间:<asp:TextBox ID="txtDateStart" CssClass="Wdate" onClick="var d5222=$dp.$('ctl00_ContentPlaceHolder1_txtDateEnd');WdatePicker({onpicked:function(){d5222.focus();},maxDate:'#F{$dp.$D(\'ctl00_ContentPlaceHolder1_txtDateEnd\')}'})"
            runat="server"></asp:TextBox>到<asp:TextBox ID="txtDateEnd" runat="server" CssClass="Wdate"
                onFocus="WdatePicker({minDate:'#F{$dp.$D(\'ctl00_ContentPlaceHolder1_txtDateStart\')}'})"></asp:TextBox>
        <asp:Button ID="btnChaXun" runat="server" Text="查询" OnClick="btnChaXun_Click" />
    </div>
    <asp:GridView ID="grvInfo" runat="server" Width="90%" AutoGenerateColumns="False"
        CellPadding="2" ForeColor="#1B2761" Font-Size="14px" CssClass="gridveiwcss">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="LanMu" HeaderText="栏目" />
            <asp:BoundField DataField="ShuLiang" HeaderText="访问量" />
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
