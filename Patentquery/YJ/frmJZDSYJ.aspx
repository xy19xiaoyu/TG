<%@ Page Title="" Language="C#" MasterPageFile="~/Master/st.master" AutoEventWireup="true"
    CodeBehind="frmJZDSYJ.aspx.cs" Inherits="Patentquery.YJ.frmJZDSYJ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td valign="top" align="left" colspan="2">
                <asp:LinkButton ID="btnCN" runat="server" OnClick="btnCN_Click">中国专利预警</asp:LinkButton>
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="btnEN" runat="server" OnClick="btnEN_Click">世界专利预警</asp:LinkButton>
            </td>
        </tr>       
        <tr>
            <td valign="top" align="left">
            </td>
            <td valign="top" align="left">
                <asp:DropDownList ID="ddlKeyWord" runat="server">
                </asp:DropDownList>
                <asp:TextBox ID="txtKeyWord" runat="server"></asp:TextBox>
                <asp:Button ID="btnChaXun" runat="server" OnClick="btnChaXun_Click" Text="查询" />
                <asp:Button ID="btnAdd" runat="server" Text="新增" OnClick="btnAdd_Click" />
                
                <asp:Button ID="btnDel" runat="server" Text="删除" OnClick="btnDel_Click" />
                <asp:Button ID="btnReDian" runat="server" Text="热点预警" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
                <asp:LinkButton ID="btnJZDS" runat="server" OnClick="btnJZDS_Click">竞争对手动向预警</asp:LinkButton>
                <br />
                <asp:LinkButton ID="btnTDJS" runat="server" OnClick="btnTDJS_Click">特定技术动向预警</asp:LinkButton><br />
                <asp:LinkButton ID="btnFaMingRenDX" runat="server" OnClick="btnFaMingRenDX_Click">发明人动向预警</asp:LinkButton><br />
                <asp:LinkButton ID="btnZhuanLiQuYuFB" runat="server" OnClick="btnZhuanLiQuYuFB_Click">专利区域分布预警</asp:LinkButton><br />
                <asp:LinkButton ID="btnLaiHuaZhuanLi" runat="server" OnClick="btnLaiHuaZhuanLi_Click">来华专利布局预警</asp:LinkButton><br />
                <asp:LinkButton ID="btnGaoJi" runat="server" OnClick="btnGaoJi_Click">高级订制预警</asp:LinkButton>
            </td>
            <td valign="top" align="left">
                <div id="users-contain" class="ui-widget">
                    <table id="users" class="ui-widget ui-widget-content">
                        <asp:GridView ID="grvInfo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None" Width="100%">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkXuanZe" runat="server" ToolTip='<%# Eval("C_ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="预警名称">
                                    <ItemTemplate>
                                        <a href="frmYJItem.aspx?CID=<%# Eval("C_ID") %>" target="_blank">
                                            <%# Eval("ALIAS")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="C_DATE" HeaderText="预警设置日期" />
                                <asp:TemplateField HeaderText="当前专利数">
                                    <ItemTemplate>
                                        <a href="frmGaiYao.aspx?WID=<%# Eval("W_ID") %>" target="_blank">
                                            <%# Eval("CURRENTNUM")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="变更数量">
                                    <ItemTemplate>
                                        <a href="frmGaiYao.aspx?WID=<%# Eval("W_ID") %>" target="_blank">
                                            <%# Eval("CHANGENUM")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BEIZHU" HeaderText="备注" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <%--          手动更新调用   PatentWarnning.YJini.SearchYJini(C_ID);C_ID 为当前行的ID   --%>
                                        <a href="#">手动更新</a> <a href="frmYJHis.aspx?CID=<%# Eval("C_ID") %>" target="_blank">
                                            预警历史</a> <a href="frmAdd.aspx?CID=<%# Eval("C_ID") %>">修改</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
