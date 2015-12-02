<%@ Page Title="" Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true"
    CodeBehind="HelpFileMng.aspx.cs" Inherits="Patentquery.SysAdmin.HelpFileMng" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript" src="../Jquery-ui-1.10.3/jquery-1.9.1.js"></script>
    <link type="text/css" rel="stylesheet" href="../js/file-uploader/fileuploader.css" />
    <script type="text/javascript" language="javascript" src="../js/file-uploader/fileuploader.min.js"></script>
    <script type="text/javascript" language="javascript">
        var g_oJCrop = null;
        $(function () {
            new qq.FileUploader({
                element: document.getElementById('upload_avatar'),
                action: "../zt/ZtFileUpLoad.ashx",
                multiple: false,
                //autoUpload: false,
                disableDefaultDropzone: true,
                allowedExtensions: ['pdf', 'doc', 'docx', 'wmv', 'txt'],
                uploadButtonText: '选择附件...',
                onComplete: function (id, fileName, json) {
                    if (json.success) {
                        if (g_oJCrop != null) g_oJCrop.destroy();
                        //$("#crop_tmp_avatar").val(json.tmp_avatar);
                        //$("#crop_container").show();
                        //$("#crop_preview").html('<img style="width: 130px; height: 90px;" src="../ZtHeadImg/' + json.tmp_avatar + '">');
                        //$("#crop_preview").attr("title", json.tmp_avatar);
                        $("#hdiFileName").attr("value", json.tmp_avatar);
                        alert('上传成功');
                    }
                    else {
                        $("#hdiFileName").attr("value", "");
                        alert(json.description);
                    }
                }
            });
        });
            
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="3" border="0" cellspacing="0">
        <tr>
            <td align="left" valign="top" colspan="5">
                <asp:GridView ID="grvInfo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="None" Width="100%" DataKeyNames="ID" 
                    onrowdeleting="grv_RowDeleting">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:HyperLinkField HeaderText="标题" DataNavigateUrlFields="fileName" DataNavigateUrlFormatString="../ZtHeadImg/{0}"
                            DataTextField="helpTitle" Target="_blank" />
                        <asp:BoundField DataField="UploadDate" HeaderText="上传时间">
                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDel" runat="server" CommandName="delete">删除</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
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
        </tr>
        <tr>
            <td valign="top" class="style2">
                帮助文件：
            </td>
            <td colspan="4">
                <div id="upload_avatar">
                </div>
            </td>
        </tr>
        <tr>
            <td valign="top" class="style2">
                标题：
            </td>
            <td align="left" valign="top" colspan="3">
                <asp:TextBox ID="txtHelpTitle" runat="server" Width="370px"></asp:TextBox>
                <asp:Button ID="btnBaoCun" runat="server" OnClick="btnBaoCun_Click" Text="保存" Width="74px" />
            </td>
            <td align="left" valign="top" class="style4">
                <asp:HiddenField ID="hdiFileName" runat="server" ClientIDMode="Static" />
            </td>
        </tr>
    </table>
</asp:Content>
