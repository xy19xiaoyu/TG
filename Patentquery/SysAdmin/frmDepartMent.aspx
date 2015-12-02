<%@ Page Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true" Inherits="frmDepartMent" Title="无标题页" Codebehind="frmDepartMent.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="left" valign="top">
                部门名称： 
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txtDepartMent" runat="server"></asp:TextBox>
            </td>
        </tr>
     
        <tr>
            <td align="left" valign="top">
            </td>
            <td align="left" valign="top">
                <asp:Button ID="btnQueDing" runat="server" Text="保存" 
                    onclick="btnQueDing_Click" />
            </td>
        </tr>
        
        <tr>
            <td align="left" valign="top" colspan="2">
                
                <asp:GridView ID="grvDepartMent" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="400px">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField DataField="DepartMent" HeaderText="部门名称" />
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                
            </td>
        </tr>
    </table>
</asp:Content>

