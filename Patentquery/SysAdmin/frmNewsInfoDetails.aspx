<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true" CodeBehind="frmNewsInfoDetails.aspx.cs" Inherits="Patentquery.SysAdmin.frmNewsInfoDetails" %>
<%@Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/ckeditor/ckeditor.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <table width="100%">
        <tr>
            <td style="width:100px">新闻标题：
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" Width="699px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:100px">新闻摘要：
            </td>
            <td>
                <asp:TextBox ID="txtSummary" runat="server" Width="707px" Height="65px" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:100px">
                新闻内容：
            </td>
            <td>
                 <CKEditor:CKEditorControl ID="txtA" BasePath="~/ckeditor" runat="server"></CKEditor:CKEditorControl>
            </td>
            
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnSubmit" runat="server" Text="提交" onclick="btnSubmit_Click" 
                    Width="69px" />
                    <asp:Button ID="btnfanhui" runat="server" Text="返回" 
                    Width="69px" onclick="btnfanhui_Click" />
            </td>
        </tr>
   </table>
</asp:Content>
