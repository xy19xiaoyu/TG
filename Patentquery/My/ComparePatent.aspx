<%@ Page Language="C#" MasterPageFile="~/Master/Index.master" AutoEventWireup="true"
    Inherits="My_ComparePatent" Title="" CodeBehind="ComparePatent.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Css/B_SeachPage.css" rel="stylesheet" type="text/css" />
    <link href="../Jquery-ui-1.10.3/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Jquery-ui-1.10.3/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.accordion.js" type="text/javascript"></script>
    <script src="../Js/ComparePage.js" type="text/javascript"></script>
    <script src="../Js/Common.js" type="text/javascript"></script>
    <script>
        $(function () {
            IniteClaims();
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
            <div class="command" style="float: none">
                <div class="commandLeft">
                    专利A：
                    <asp:DropDownList ID="DropDownListPatentA" OnSelectedIndexChanged="DropDownListPatentA_SelectedIndexChanged"
                        AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="compareRight">
                    专利B：
                    <asp:DropDownList ID="DropDownListPatentB" OnSelectedIndexChanged="DropDownListPatentB_SelectedIndexChanged"
                        AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="box">
                <div class="boxTL">
                </div>
                <div class="boxTop">
                </div>
                <div class="boxTR">
                </div>
                <div class="boxOutter">
                    <div class="boxInner">
                        <div id="xiangmu">
                            <div>
                                <table cellspacing="1" class="compare">
                                    <tr>
                                        <th colspan="4" style="text-align: center; border-right: #6595d6 1px solid;">
                                            <asp:Literal ID="LiteralTitleA" runat="server" />
                                        </th>
                                        <th colspan="4" style="text-align: center;">
                                            <asp:Literal ID="LiteralTitleB" runat="server" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th width="10%">
                                            申请日：
                                        </th>
                                        <td width="15%">
                                            <asp:Literal ID="LiteralApDateA" runat="server" />
                                        </td>
                                        <th width="10%">
                                            申请号：
                                        </th>
                                        <td width="15%" style="border-right: #6595d6 1px solid;">
                                            <asp:Literal ID="LiteralApNoA" runat="server" />
                                        </td>
                                        <th width="10%">
                                            申请日：
                                        </th>
                                        <td width="15%">
                                            <asp:Literal ID="LiteralApDateB" runat="server" />
                                        </td>
                                        <th width="10%">
                                            申请号：
                                        </th>
                                        <td width="15%">
                                            <asp:Literal ID="LiteralApNoB" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            发明人：
                                        </th>
                                        <td>
                                            <asp:Literal ID="LiteralInventorA" runat="server" />
                                        </td>
                                        <th>
                                            申请人：
                                        </th>
                                        <td style="border-right: #6595d6 1px solid;">
                                            <asp:Literal ID="LiteralApplyA" runat="server" />
                                        </td>
                                        <th>
                                            发明人：
                                        </th>
                                        <td>
                                            <asp:Literal ID="LiteralInventorB" runat="server" />
                                        </td>
                                        <th>
                                            申请人：
                                        </th>
                                        <td>
                                            <asp:Literal ID="LiteralApplyB" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            国家/省市：
                                        </th>
                                        <td>
                                            <asp:Literal ID="LiteralCountryCodeA" runat="server" />
                                        </td>
                                        <th>
                                            申请人地址：
                                        </th>
                                        <td style="border-right: #6595d6 1px solid;">
                                            <asp:Literal ID="LiteralAddsA" runat="server" />
                                        </td>
                                        <th>
                                            国家/省市：
                                        </th>
                                        <td>
                                            <asp:Literal ID="LiteralCountryCodeB" runat="server" />
                                        </td>
                                        <th>
                                            申请人地址：
                                        </th>
                                        <td>
                                            <asp:Literal ID="LiteralAddsB" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            公开号：
                                        </th>
                                        <td>
                                            <asp:Literal ID="LiteralPubNoA" runat="server" />
                                        </td>
                                        <th>
                                            公开日：
                                        </th>
                                        <td style="border-right: #6595d6 1px solid;">
                                            <asp:Literal ID="LiteralPubDateA" runat="server" />
                                        </td>
                                        <th>
                                            公开号：
                                        </th>
                                        <td>
                                            <asp:Literal ID="LiteralPubNoB" runat="server" />
                                        </td>
                                        <th>
                                            公开日：
                                        </th>
                                        <td>
                                            <asp:Literal ID="LiteralPubDateB" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="gonggao" runat="server">
                                        <th>
                                            授权公告号：
                                        </th>
                                        <td>
                                            <asp:Literal ID="LiteralAnnNoA" runat="server" />
                                        </td>
                                        <th>
                                            授权公告日：
                                        </th>
                                        <td style="border-right: #6595d6 1px solid;">
                                            <asp:Literal ID="LiteralAnnDateA" runat="server" />
                                        </td>
                                        <th>
                                            授权公告号：
                                        </th>
                                        <td>
                                            <asp:Literal ID="LiteralAnnNoB" runat="server" />
                                        </td>
                                        <th>
                                            授权公告日：
                                        </th>
                                        <td>
                                            <asp:Literal ID="LiteralAnnDateB" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            优先权：
                                        </th>
                                        <td colspan="3">
                                            <asp:Literal ID="litYSQ_A" runat="server" />
                                        </td>
                                        <th>
                                            优先权：
                                        </th>
                                        <td colspan="3">
                                            <asp:Literal ID="litYSQ_B" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div id="accordion">
                            <h3>
                                摘要信息
                            </h3>
                            <div>
                                <table cellspacing="1" class="compare">
                                    <tr>
                                        <td style="border-right: #6595d6 1px solid;" width="50%">
                                            <asp:Literal ID="LiteralBriefA" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Literal ID="LiteralBriefB" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <h3>
                                摘要附图
                            </h3>
                            <div>
                                <table cellspacing="1" class="compare">
                                    <tr>
                                        <td width="50%" style="border-right: #6595d6 1px solid;">
                                            <asp:Image ID="ImageFtA" Width="300" Height="200" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Image ID="ImageFtB" Width="300" Height="200" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <h3>
                                法律状态
                            </h3>
                            <div>
                                <table cellspacing="1" class="compare">
                                    <tr>
                                        <td width="50%" style="border-right: #6595d6 1px solid;">
                                            <asp:Literal ID="litFlzt_A" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Literal ID="litFlzt_B" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <h3 id="hclaim">
                                权利要求书</h3>
                            <div id="divclaim">
                                <asp:HiddenField ID="hidClaims" runat="server" Value="" />
                                <div id="imgloading">
                                    <img src="../Images/loading04.gif" />数据加载中.....</div>
                                <table id="tableClaims" style="display: none;" cellspacing="1" class="compare">
                                    <tr>
                                        <td width="50%" style="border-right: #6595d6 1px solid;">
                                            <div id="claim1">
                                            </div>
                                        </td>
                                        <td>
                                            <div id="claim2">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                              <h3 id="hDes">
                                说明书</h3>
                            <div id="divDes">
                                <asp:HiddenField ID="hidDes" runat="server" Value="" />
                                <div id="divImglogingDes">
                                    <img src="../Images/loading04.gif" />数据加载中.....</div>
                                <table id="tabDes" style="display: none;" cellspacing="1" class="compare">
                                    <tr>
                                        <td width="50%" style="border-right: #6595d6 1px solid;">
                                            <div id="divDes1">
                                            </div>
                                        </td>
                                        <td>
                                            <div id="divDes2">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="boxBL">
                </div>
                <div class="boxBottom">
                </div>
                <div class="boxBR">
                </div>
            </div>
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
                //IniteTabs(true);
                $("#accordion").accordion();
            } catch (e) {
            }
        }
    );
    </script>
</asp:Content>
