<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true" CodeBehind="frmNewsInfo.aspx.cs" Inherits="Patentquery.SysAdmin.frmNewsInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td align="center">
                <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click">新增</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvNewsInfo" runat="server" Width="90%" AutoGenerateColumns="False"
                CellPadding="2" ForeColor="#1B2761"
                Font-Size="14px" CssClass="gridveiwcss" AllowPaging="True" PageSize="20" 
                onpageindexchanging="gvNewsInfo_PageIndexChanging" 
                onrowdeleting="gvNewsInfo_RowDeleting" >
                <Columns>
                    <asp:BoundField DataField="NID" HeaderText="ID">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TITLE" HeaderText="标题">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CREATEDATE" HeaderText="发布时间">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            </td>
        </tr>
    </table>
    
</asp:Content>
