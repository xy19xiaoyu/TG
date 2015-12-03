<%@ Page Title="" Language="C#" MasterPageFile="~/Master/index_new.master" AutoEventWireup="true"
    CodeBehind="frmPatDetails.aspx.cs" Inherits="Patentquery.My.frmPatDetails" ValidateRequest="false" %>

<asp:Content ID="head2" ContentPlaceHolderID="head" runat="server">
    <style>
        input[type="checkbox"] {
            margin-left: 5px;
            margin-top: 5px;
        }

        .accordion .accordion-header-selected {
            background: #0081c2;
        }

        .accordion-header-selected {
            background: #0081c2;
        }

        .accordion .accordion-header-selected .panel-title {
            color: #fff;
        }

        .easyui-accordion .panel {
            margin-bottom: 0px;
        }

            .easyui-accordion .panel .panel-title {
                font-size: 12px;
            }

        .thumbnail img {
            width: 280px;
            height: 280px;
        }

        #funTool ul li {
            padding: 5px 0 5px 0;
        }
    </style>
    <link href="../Css/lightbox.css" rel="stylesheet" type="text/css" />
    <script src="../Js/lightbox.js" type="text/javascript"></script>
    <link href="../Css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../js/AddToCO.js" type="text/javascript"></script>
    <script src="../js/syscfg.js" type="text/javascript"></script>
    <script src="../Js/DetailsPage1.js" type="text/javascript"></script>
    <script src="../Js/sysdialog.js" type="text/javascript"></script>
    <script src="../Js/StrComm.js" type="text/javascript"></script>
    <script src="../Js/Trans.js" type="text/javascript"></script>
    <script src="../Js/jquery.highlight-4.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display: none">
        <asp:Button ID="btnActiveTab" runat="server" Text="Button" OnClick="btnActiveTab_Click"
            ClientIDMode="Static" />
        <asp:HiddenField ID="hidActiveTabTi" runat="server" ClientIDMode="Static" Value="著录项目信息" />
        <asp:HiddenField ID="hidActiveTabIdx" runat="server" ClientIDMode="Static" Value="0" />
    </div>
    <!-- Tab 开始-->
    <div id="DivPatDetailTabs" class="easyui-tabs" style="min-height: 550px; _height: 550px;">
        <div title="著录项目信息" style="padding: 5px">
            <div id="tabMianXml" title="著录项目信息">
                <table width="100%" cellpadding="3" style="vertical-align: middle; border-collapse: inherit; border-spacing: 1px; border: 0"
                    cellspacing="1">
                    <tr>
                        <td class="tdtitle">发明名称：
                        </td>
                        <td class="tdvalue" colspan="3">
                            <b>
                                <asp:Literal ID="LiteralTitle" runat="server" /></b>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle">申请号：
                        </td>
                        <td class="tdvalue" id="tdApno">
                            <asp:Literal ID="LiteralApNo" runat="server" />
                        </td>
                        <td class="tdtitle">申请日：
                        </td>
                        <td class="tdvalue">
                            <asp:Literal ID="LiteralApDate" runat="server" />
                        </td>
                        <td class="tdtitle_right">国家/省市：
                        </td>
                        <td class="tdvalue">
                            <asp:Literal ID="LiteralCountryCode" runat="server" />
                        </td>
                    </tr>
                    <tr id="trFmXx">
                        <td class="tdtitle">公开号：
                        </td>
                        <td class="tdvalue">
                            <asp:Literal ID="LiteralPubNo" runat="server" />
                        </td>
                        <td class="tdtitle">公开日：
                        </td>
                        <td class="tdvalue">
                            <asp:Literal ID="LiteralPubDate" runat="server" />
                        </td>
                        <td class="tdtitle">主分类号：
                        </td>
                        <td class="tdvalue">
                            <asp:Literal ID="LiteralMainIpc" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle">授权公告号：
                        </td>
                        <td class="tdvalue">
                            <asp:Literal ID="LiteralAnnNo" runat="server" />
                        </td>
                        <td class="tdtitle">授权公告日：
                        </td>
                        <td class="tdvalue" colspan="3">
                            <asp:Literal ID="LiteralAnnDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle">申请人：
                        </td>
                        <td colspan="3" class="tdvalue">
                            <asp:Literal ID="LiteralApply" runat="server" />
                        </td>
                        <td class="tdvalue" rowspan="8" colspan="2">
                            <div id="divImgFt" class="thumbnail" style='width: 250px; height: 250px; vertical-align: middle; line-height: 250px;'>
                                <asp:Literal ID="LiteralImageFt" runat="server" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle">发明人：
                        </td>
                        <td colspan="3" class="tdvalue_colspan4">
                            <asp:Literal ID="LiteralInventor" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle">代理人：
                        </td>
                        <td colspan="3" class="tdvalue_colspan4">
                            <asp:Literal ID="LiteralAgent" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle">代理机构：
                        </td>
                        <td colspan="3" class="tdvalue_colspan4">
                            <asp:Literal ID="LiteralAgency" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle">申请人地址：
                        </td>
                        <td colspan="3" class="tdvalue_colspan4">
                            <asp:Literal ID="LiteralAdds" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle">优先权：
                        </td>
                        <td colspan="3" class="tdvalue_colspan4">
                            <asp:Literal ID="ltraPro" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle">分类号：
                        </td>
                        <td colspan="3" class="tdvalue_colspan4">
                            <asp:Literal ID="LiteralIpc" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle">摘要：
                        </td>
                        <td colspan="3" class="tdvalue_colspan4">
                            <asp:Literal ID="LiteralBrief" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtitle">主权利要求：
                        </td>
                        <td colspan="5" class="tdvalue_colspan4">
                            <asp:Label ID="LabelClaim" ClientIDMode="Static" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="ptinfo" class="easyui-accordion" data-options="multiple:true">
                <div id="autoindex" title="自动标引" style="padding: 10px;">
                    Loading......
                </div>
                <div id="divTabDes" title="说明书" closable="false" style="padding: 10px;">
                    Loading......
                </div>
                <div id="divTabClams" title="权利要求" closable="false" style="padding: 10px;">
                    Loading......
                </div>
                <div id="divTabLegal" title="法律状态" closable="false" style="padding: 10px;">
                    Loading......
                </div>
                <div id="divTabQuote" title="引文信息" closable="false" style="padding: 10px;">
                    Loading......
                </div>
            </div>
        </div>
        <div id="TabDegImgs" title="外观图形" closable="false" style="padding: 2px;">
            <a href="http://localhost:25533/imgs/banner_bj.png" rel="lightbox">
                <img src="../imgs/banner_bj.png" class="img-responsive" />
            </a>
            <div class="center">
                <asp:Literal ID="LiteralPictureList" runat="server" Text="" />
            </div>
        </div>
        <div id="DivtabPdf" title="全文PDF" closable="false" style="padding: 2px;">
            <asp:Literal ID="LiteralPdf" runat="server" Text="Loading......" EnableViewState="false"
                ViewStateMode="Disabled" />
            <asp:HiddenField ID="HiddenField1Pdf" runat="server" EnableViewState="false" ViewStateMode="Disabled"
                ClientIDMode="Static" />
        </div>
    </div>
    <!-- Tab 结束-->
    <div>
        <asp:HiddenField ID="hidAutoIndexWord" runat="server" Value="" />
    </div>
    <!-- 收藏夹 开始-->
    <div id="AddCO" style="min-width: 300px; display: none; z-index: 9999;" class="ke-dialog">
        <div style="min-height: 250px">
            <ul id="CO" class="easyui-tree" style="min-width: 300px" data-options="lines:true,checkbox:true,cascadeCheck:false" />
        </div>
        <div style="width: 318px">
            自定义标注：
            <textarea name="TextBoxNote" rows="5" cols="45" style="width: 290px" id="TextBoxNote"></textarea>
        </div>
    </div>
    <div id="DivAddNode" style="min-width: 400px; display: none; z-index: 9999;">
        <span id="dialogtitle" style="display: none"></span>
        <table>
            <tr>
                <th>收藏夹名称:
                </th>
                <td>
                    <input type="text" id="newclass" name="newclass" style="width: 280px" /><br />
                    <span id="showmsg" style="display: none; color: Red;">*请输入收藏夹名称</span>
                </td>
            </tr>
            <tr>
                <th>简介：
                </th>
                <td>
                    <textarea id="txtNodeDes" cols="200" name="txtNodeDes" rows="40" style="width: 280px; height: 100px"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <div id="DLGAddIndex" style="position: absolute; top: 200px; left: 50px; width: 200px; height: 500px; display: none;">
        <table id="pg" class="easyui-propertygrid" style="width: 200px; height: 500px;">
        </table>
        <input type="button" id="btnindex" onclick="AddIndex('cn')" value="标引" style="width: 50px; height: 20px; float: right; margin-top: 10px; margin-right: 10px;" />
    </div>
    <!-- 收藏夹 结束 -->
    <div id="funTool">
        <ul>
            <li><a id="AddToCo" href="javascript:void(0);"><span class="label label-info"><span
                class="glyphicon glyphicon-star"></span>收藏</span></a></li>
            <li>
                <asp:LinkButton ID="LinkButtonDownload" runat="server" OnClick="LinkButtonDownload_Click"
                    Text="&lt;span class='label label-info'&gt;&lt;span class='glyphicon glyphicon-save'&gt;&lt;/span&gt;下载&lt;/span&gt;" /></li>
        </ul>
    </div>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var top = $("#DivPatDetailTabs").offset().top + 20;
            var left = $("#DivPatDetailTabs").offset().left + 1008;
            $("#funTool").css("position", "fixed");
            $("#funTool").css("top", top);
            $("#funTool").css("left", left);
        });
        var hasClims = false;
        var hasDesc = false;
        var pid = getUrlParam("id");
        var type = "";
        $("#ptinfo").accordion({
            onSelect: function (title, index) {
                if ($("#ptinfo").accordion("getPanel", index).find("div.val").length > 0) return;
                switch (title) {
                    case "自动标引":                       
                        break;
                    case "权利要求":
                        type = "C";
                        break;
                    case "说明书":
                        type = "D";
                        break;
                    case "法律状态":
                        type = "L";
                        break;
                    case "引文信息":
                        type = "R";
                        break;
                }
                $.getJSON(
                   "../comm/PatentInfo.aspx?type=" + type + "&id" + pid,
                   function (json) {
                       var innerdiv = document.createElement("div");
                       if (json.ret) {
                           innerdiv.innerHTML = json.data;
                       }
                       else {
                           innerdiv.innerHTML = json.err;
                       }
                       $("#ptinfo").accordion("getPanel", index).html(innerdiv.innerHTML);
                   }
               );
            }
        });
    </script>
</asp:Content>

