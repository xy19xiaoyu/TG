<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLawInfo.aspx.cs" Inherits="Patentquery.My.frmLawInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .gridView
        {
            background-color: #FFFFFF;
            z-index: 1;
        }
        table tr td
        {
            vertical-align: top;
        }
        .gridViewItem
        {
            position: relative;
            padding: 5px;
            z-index: 0;
        }
        .gridView .title
        {
            clear: both;
            color: #FFFFFF;
            background-color: #3B96DF;
            padding: 5px;
        }
        .gridView .title a
        {
            color: #FFFFFF;
        }
        .gridView .title a
        {
            color: #FFFFFF;
        }
        .gridView a
        {
            color: #0000FF;
            text-decoration: underline;
        }
        .gridView .note
        {
            clear: both;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" AutoGenerateColumns="False" Width="100%" GridLines="None" ShowHeader="false"
            CssClass="gridView" EmptyDataRowStyle-CssClass="empty" runat="server">
            <EmptyDataRowStyle CssClass="empty"></EmptyDataRowStyle>
            <EmptyDataTemplate>
                <img src="/Images/iconImportant.png" alt="" />
                暂无法律状态数据
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="gridViewItem">
                            <div class="title">
                                <b>申请号</b>： <a href='frm2Details.aspx?id=<%# Eval("SHENQINGH")%>' target="_blank">
                                    <%# Eval("SHENQINGH")%></a>
                            </div>
                            <div class="note">
                                <b>法律状态公告日</b>：<%# SearchInterface.XmPatentComm.FormatDateVlue(Eval("LegalDate").ToString())%><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    法律状态</b>：<%# Eval("LegalStatus")%><br />
                                <b>法律状态信息</b>：<%# Eval("LegalStatusInfo")%><br />
                                <%# Eval("Detail").ToString().Replace("|", "<BR>")%></div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
