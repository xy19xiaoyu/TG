<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true" CodeBehind="frmQuestionAnser.aspx.cs" Inherits="Patentquery.SysAdmin.frmQuestionDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style=" width:80px" align="right">
                标题：
            </td>
            <td>
                <asp:Label ID="lblTitle" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                提问人：
            </td>
            <td>
                <asp:Label ID="lblUser" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                提问时间：
            </td>
            <td>
                <asp:Label ID="lblDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                问题：
            </td>
            <td>
                <asp:Label ID="lblQuestion" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td align="right" valign="top">
                回答：
            </td>
            <td>
                <asp:TextBox ID="txtAnser" runat ="server" TextMode="MultiLine" Rows="10" 
                    Width="643px"></asp:TextBox>
            </td>
        </tr>
        <tr>           
            <td colspan="2" align="center">
                <asp:Button ID="btnSubmit" runat="server" Text="提交" onclick="txtSubmit_Click" />
                <asp:Button ID="btnFanHui" runat="server" Text="返回" 
                    onclick="btnFanHui_Click"  />
            </td>
        </tr>
    </table>
</asp:Content>
