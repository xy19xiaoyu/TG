<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" 
    CodeBehind="frmOpinionMng.aspx.cs" Inherits="Patentquery.SysAdmin.frmOpinionMng" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript" src="../JS/jDatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: left">
        提交时间:<asp:TextBox ID="txtDateStart" CssClass="Wdate" onClick="var d5222=$dp.$('ctl00_ContentPlaceHolder1_txtDateEnd');WdatePicker({onpicked:function(){d5222.focus();},maxDate:'#F{$dp.$D(\'ctl00_ContentPlaceHolder1_txtDateEnd\')}'})"
            runat="server"></asp:TextBox>到<asp:TextBox ID="txtDateEnd" runat="server" CssClass="Wdate"
                onFocus="WdatePicker({minDate:'#F{$dp.$D(\'ctl00_ContentPlaceHolder1_txtDateStart\')}'})"></asp:TextBox>
        <asp:Button ID="btnChaXun" runat="server" Text="查询" OnClick="btnChaXun_Click" />
    </div>
    <div>
        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="1" bgcolor="#cccccc">
            <tr height="30" bgcolor="#e1ecf3">
                <td style="width: 50%; text-align: center">
                    <strong>意见标题</strong>
                </td>
                <td style="width: 17%; text-align: center">
                    <strong>提交人</strong>
                </td>
                <td style="width: 17%; text-align: center">
                    <strong>提交时间</strong>
                </td>
                <td style="width: 10%; text-align: center">
                    <strong>操作</strong>
                </td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="False"
            Width="100%" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
            OnRowDeleting="GridView1_RowDeleting" PageSize="15" DataKeyNames="ID">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <table bgcolor="#cccccc" border="0" cellpadding="0" cellspacing="1" style="width: 100%;">
                            <tr bgcolor="#FFFFDD" height="30">
                                <td style="width: 50%; text-align: left">
                                    <asp:Label ID="LabBT" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title")%>'></asp:Label>
                                </td>
                                <td style="width: 17%; text-align: center">
                                    <asp:Label ID="labTJR" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UName")%>'></asp:Label>
                                </td>
                                <td style="width: 17%; text-align: center">
                                    <asp:Label ID="labTJSJ" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TJDate")%>'></asp:Label>
                                </td>
                                <td style="width: 10%; text-align: center">
                                    <asp:LinkButton ID="lnkBtnDel" runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('是否确定要删除？');"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr bgcolor="#FFFFFF" height="30">
                                <td colspan="4" style="text-align: left">
                                    <b>意见内容:</b>
                                    <asp:Label ID="lblFileDoc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LYTxt")%>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
        </asp:GridView>        
    </div>
    <div style="text-align:right">
        <asp:Label ID="labExlFile" runat="server" Text="下载" Visible="False"></asp:Label>
        <asp:Button ID="Button1" runat="server" 
            Text="导出EXCEL" onclick="Button1_Click" />&nbsp;&nbsp; </div>
</asp:Content>
