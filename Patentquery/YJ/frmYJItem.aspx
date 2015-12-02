<%@ Page Title="" Language="C#" MasterPageFile="~/Master/My.master" AutoEventWireup="true"
    CodeBehind="frmYJItem.aspx.cs" Inherits="Patentquery.YJ.frmYJItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="users" class="ui-widget ui-widget-content">
        <asp:GridView ID="grvInfo" runat="server" AutoGenerateColumns="False" CellPadding="4"
            ForeColor="#333333" GridLines="None" Width="100%">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <a href="#">
                            <%# Eval("S_NAME")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
           
                <asp:BoundField DataField="CURRENTNUM" HeaderText="当前专利数" />
                <asp:BoundField DataField="CHANGENUM" HeaderText="变更数量" />
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
</asp:Content>
