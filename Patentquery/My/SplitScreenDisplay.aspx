<%@ Page Language="C#" MasterPageFile="~/Master/Index.master" AutoEventWireup="true"
    Inherits="My_SplitScreenDisplay" Title="" CodeBehind="SplitScreenDisplay.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Css/B_SeachPage.css" rel="stylesheet" type="text/css" />
    <link href="../Jquery-ui-1.10.3/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Jquery-ui-1.10.3/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
//            $("#leftFrame").load(function () {
//                var mainHeight = $(this).contents().find("body").height();
//                $(this).height(mainHeight);
//            });
            $("#rightFrame").load(function () {
                $(this).contents().find(".commandLeft").css("float", "right");
            });
        });
    </script>
    <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true"
        runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="1">
        <ProgressTemplate>
            <div class="loading">
                <img src="/Images/gifLoading.gif" alt="" /></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td style="border-right: #6595d6 2px solid;" width="50%">
                        <asp:Literal ID="LiteralLeft" runat="server" Text="Loading......" />
                        <%--<iframe id="frame_content" src="SplitScreenDisplay.aspx?Id=4BBA3BCA9GGE9FCA3ACA9BIA9FBBFIFA9DIF9FGE9AGC5CCA&type=CN" scrolling="no" frameborder="0" width="100%" onload="this.height=this.contentWindow.document.documentElement.scrollHeight"></iframe>--%> 
                    </td>
                    <td style="border-left: #6595d6 2px solid;" width="50%">
                        <asp:Literal ID="LiteralRight" runat="server" Text="Loading......" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        //设置updatepanel回调客户端JS
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            // re-bind your jquery events here
            //////////////////////////////////////// 
            //隐藏遮照层
            try {
            } catch (e) {
            }
        }
    );
    </script>
</asp:Content>
