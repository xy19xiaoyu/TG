<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true" CodeBehind="frmQuestionLst.aspx.cs" Inherits="Patentquery.SysAdmin.frmQuestionLst" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%">
        <tr>
            <td align="center">
                状态：<asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlStatus_SelectedIndexChanged">
                    <asp:ListItem Value="0">未回复</asp:ListItem>
                    <asp:ListItem Value="1">已回复</asp:ListItem>
                </asp:DropDownList>                
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvNewsInfo" runat="server" Width="90%" AutoGenerateColumns="False"
                CellPadding="2" ForeColor="#1B2761"
                Font-Size="14px" CssClass="gridveiwcss" AllowPaging="True" PageSize="20" 
                onpageindexchanging="gvNewsInfo_PageIndexChanging" 
                    onrowdatabound="gvNewsInfo_RowDataBound" >
                <Columns>
                    <asp:BoundField DataField="QID" HeaderText="ID">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TITLE" HeaderText="标题">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CREATEDATE" HeaderText="提问时间">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Status" HeaderText="状态">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
        
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">操作</asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle BorderStyle="None" HorizontalAlign="Center" 
                            VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
        
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
