<%@ Page Language="C#" MasterPageFile="~/Master/Index.master" AutoEventWireup="true"
    Inherits="My_LawQueryResult" Title="" CodeBehind="LawQueryResult.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Css/B_cprs2010.css" rel="stylesheet" type="text/css" />
    <link href="../Css/B_My.css" rel="stylesheet" type="text/css" />
    <div class="div_Content_xiwl home2_con">
        <div class="">
            <div class="">
                <h2>
                    多案法律状态检索结果</h2>
                <hr />
                <div class="center">
                    <asp:DropDownList ID="ddlPagCount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPagCount_SelectedIndexChanged">
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinkButton ID="btnShouYe" Text="|<" OnClick="btnShouYe_Click" CssClass="btnTiny"
                        runat="server" />
                    <asp:LinkButton ID="btnQian" Text="<" OnClick="btnQian_Click" CssClass="btnTiny"
                        runat="server" />
                    <asp:TextBox ID="txtPagIndex" runat="server" OnTextChanged="txtPagIndex_TextChanged"
                        Width="30px"></asp:TextBox>
                    <asp:LinkButton ID="btnHou" Text=">" OnClick="btnHou_Click" CssClass="btnTiny" runat="server" />
                    <asp:LinkButton ID="btnMoYe" Text=">|" OnClick="btnMoYe_Click" CssClass="btnTiny"
                        runat="server" />
                    共<asp:Label ID="lblCountItem" runat="server"></asp:Label>条 共<asp:Label ID="lblCount"
                        runat="server"></asp:Label>页 第<asp:Label ID="lblPagIndex" runat="server"></asp:Label>页
                </div>
                <asp:GridView ID="GridView1" AutoGenerateColumns="False" Width="100%" GridLines="None"
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
                                        <%# SearchInterface.XmPatentComm.Format_LegalStatusDetailInfo(Eval("Detail").ToString())%></div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <table>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfType" runat="server" />
</asp:Content>
