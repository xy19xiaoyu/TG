<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoCollects.aspx.cs" Inherits="Patentquery.Comm.AutoCollects" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="98%"
            DataKeyNames="CollectId" OnRowUpdating="GridView1_RowUpdating">
            <EmptyDataTemplate>
                暂无标注信息!
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <table style="border-right-width: 0px; background-color: #cccccc; width: 100%; border-top-width: 0px;
                            text-align: center; border-bottom-width: 0px; border-left-width: 0px" id="Table1"
                            border="0" cellspacing="1" cellpadding="4">
                            <tr style="height: 30px">
                                <td style="background-color: #e1ecf3; width: 10%; text-align: right;">
                                    收藏夹：
                                </td>
                                <td style="background-color: white; width: 18%" align="left">
                                    <%--<%# DataBinder.Eval(Container.DataItem, "AppNo")%>--%>
                                    <%# DataBinder.Eval(Container.DataItem, "floder")%>
                                </td>
                                <td style="background-color: #e1ecf3; width: 18%" align="right">
                                    标注时间：
                                </td>
                                <td style="background-color: white; width: 120px" align="left">
                                    <%# DataBinder.Eval(Container.DataItem, "NoteDate")%>
                                </td>
                            </tr>
                            <tr style="background-color: white; height: 40px" align="left">
                                <td colspan="4">
                                    <table width="100%">
                                        <tr>
                                            <td width="100%">
                                                <asp:TextBox ID="txbNote" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Note")%>'
                                                    TextMode="MultiLine" Rows="3" Width="99%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="bntUp" runat="server" Text="修改" Height="50px" CommandName="update" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
