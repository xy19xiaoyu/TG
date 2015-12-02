<%@ Page Title="IP配置" Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master"
    AutoEventWireup="true" CodeBehind="frmIP.aspx.cs" Inherits="Patentquery.SysAdmin.frmIP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 186px;
        }
        .style2
        {
            width: 122px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td class="style1">
                    IP:<asp:TextBox ID="txtIP" runat="server"></asp:TextBox>
                </td>
                <td class="style2">
                    <asp:RadioButtonList ID="rbtList" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">允许</asp:ListItem>
                        <asp:ListItem Value="1">拒绝</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="新增" />              
                </td>
                <td style="text-align: right"> 
                    <asp:CheckBox ID="ckbIpSet" runat="server" Text="开启IP访问限制" 
                        oncheckedchanged="ckbIpSet_CheckedChanged" AutoPostBack="True" />   &nbsp;&nbsp;
                    </td>
            </tr>
        </table>
    </div>
    <p>
        <asp:GridView ID="grvInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CellPadding="4" ForeColor="#333333" GridLines="None" OnDataBound="grvInfo_DataBound"
            OnPageIndexChanging="grvInfo_PageIndexChanging" OnRowDeleting="grvInfo_RowDeleting"
            Width="100%">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:BoundField DataField="IP" HeaderText="IP" />
                <asp:TemplateField HeaderText="标记">
                    <ItemTemplate>
                        <%# Eval("flag").ToString().Replace("1", "拒绝").Replace("0", "允许")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField HeaderText="删除" ShowDeleteButton="True" />
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
    </p>
</asp:Content>
