<%@ Page Title="厦漳泉科技基础资源服务平台-专利平台后台[法律状态配置]" Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true"
    CodeBehind="LegalUrlCfg.aspx.cs" Inherits="Patentquery.SysAdmin.LegalUrlCfg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <style type="text/css">
        .style2
    {
        text-align: right;
    }
    .style3
    {
        width: 160px;
    }
    .style4
    {
        width: 244px;
    }
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="3" border="0" cellspacing="0">
           <tr>
            <td align="left" valign="top" colspan="5">
                <asp:GridView ID="grvInfo" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="CO" HeaderText="国别" />
                        <asp:BoundField DataField="DES" HeaderText="描述" />
                        <asp:BoundField DataField="LegUrl" HeaderText="网址" />
                        <asp:TemplateField>
                          
                            <ItemTemplate>
                                <asp:LinkButton ID="btnXuanZe" runat="server" onclick="btnXuanZe_Click">修改</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                              <asp:LinkButton ID="btnDel" runat="server" onclick="btnDel_Click"  >删除</asp:LinkButton>
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
            </td>
        </tr>  <tr>
            <td valign="top" class="style2" >
                国别：
            </td>
            <td align="left" valign="top" class="style3">
                  <asp:TextBox ID="txtGuoBie" runat="server"></asp:TextBox>
            </td>
            <td align="left" valign="top"  style="text-align: right">
                描述：
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txtMiaoShu" runat="server" Width="190px"></asp:TextBox>
            </td>
            <td align="left" valign="top" class="style4">
                &nbsp;</td>
        </tr>
   
        <tr>
            <td valign="top" class="style2" >
                网址：
            </td>
            <td align="left" valign="top" colspan="3">
                <asp:TextBox ID="txtWangZhi" runat="server" Width="370px"></asp:TextBox>
                <asp:Button ID="btnBaoCun" runat="server" onclick="btnBaoCun_Click" Text="保存" />
            </td>
            <td align="left" valign="top" class="style4">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
