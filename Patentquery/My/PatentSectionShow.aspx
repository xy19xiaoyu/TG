<%@ Page Language="C#" AutoEventWireup="true" Inherits="My_PatentSectionShow" CodeBehind="PatentSectionShow.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Css/B_SeachPage.css" rel="stylesheet" type="text/css" />
    <link href="../Jquery-ui-1.10.3/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Jquery-ui-1.10.3/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Js/Common.js" type="text/javascript"></script>
    <script src="../Js/DetailsPage.js" type="text/javascript"></script>
    <script src="/Js/imageZoom.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            background: none repeat scroll #D4EAFD;
            padding-top: 7px;
        }
        .initHidden
        {
            display: none;
        }
        .boxTop_
        {
            float:left;
            display:block;
            height:6px;
            background-repeat:repeat-x;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            //$("#divMain, #divClaim, #divDes, #divAbs, #divBrief, #divLegal, #divQuote").hide();
            var selectItem = ['#divMain', '#divClaim', '#divDes', '#divAbs', '#divBrief', '#divLegal', '#divQuote'];
            var select = requestUrl('select') == '' ? 0 : requestUrl('select');
            $(selectItem[select]).show();

            $("#<%=DropDownListPatentPart.ClientID %>").bind("keyup change", function () {
                //alert($(this).val());
                $("#divMain, #divAbs, #divBrief, #divDes, #divClaim, #divLegal, #divQuote").not(":hidden").hide();
                var divName = $(this).val();
                if (divName == 'divClaim') {
//                    if ($("[id$='hidClaims']").val() != "") {
//                        var data = $("[id$='hidClaims']").val();
//                        $("#claim1").html(data);
//                        return;
//                    }
                    if ($("[id$='hidClaims']").val() == "") {
                        $.ajax({
                            type: "POST",
                            url: "CompareClaims.aspx/getSingleClaims",
                            data: "{'Id':'" + requestUrl('Id') + "','type':'" + requestUrl('type') + "'}",
                            timeout: 30000,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                var msg = XMLHttpRequest.responseHTML;
                                if (textStatus == "timeout") {
                                    msg = "检索超时，请稍后再试！";
                                }
                                $("[id$='divClaim']").html("未加载权利要求内容!")

                                $("[id$='hidClaims']").val("未加载权利要求内容!")
                            },
                            success: function (msg) {
                                $("#tableClaims").show();
                                $("#imgloading").hide();
                                $("#claim1").html(msg.d);
                                $("[id$='hidClaims']").val(msg.d);
                            }
                        });
                    }
                } else if (divName == 'divDes') {
//                    if ($("[id$='hidDes']").val() != "") {
//                        var data = $("[id$='hidDes']").val();
//                        $("#divDes1").html(data);
//                        return;
//                    }
                    if ($("[id$='hidDes']").val() == "") {
                        $.ajax({
                            type: "POST",
                            url: "CompareClaims.aspx/getSingleDes",
                            data: "{'Id':'" + requestUrl('Id') + "','type':'" + requestUrl('type') + "'}",
                            timeout: 30000,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                var msg = XMLHttpRequest.responseHTML;
                                if (textStatus == "timeout") {
                                    msg = "检索超时，请稍后再试！";
                                }
                                $("[id$='divDes']").html("未加载权利要求内容!")

                                $("[id$='hidDes']").val("未加载权利要求内容!")
                            },
                            success: function (msg) {
                                $("#tabDes").show();
                                $("#divImglogingDes").hide();
                                $("#divDes1").html(msg.d);
                                $("[id$='hidDes']").val(msg.d);
                            }
                        });
                    }
                }

                $("#" + divName).show();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="command" style="float: none">
            <div class="commandLeft">
                专利部分：
                <asp:DropDownList ID="DropDownListPatentPart" runat="server">
                </asp:DropDownList>
            </div>
        </div>
        <div class="box">
            <div class="boxTop_">
            </div>
            <div class="boxOutter">
                <div class="boxInner">
                    <div id="divMain" class="initHidden">
                        <div>
                            <table cellspacing="1" class="compare">
                                <tr>
                                    <th colspan="4" style="text-align: center; border-right: #6595d6 1px solid;">
                                        <asp:Literal ID="LiteralTitleA" runat="server" />
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
                                </tr>
                                <tr>
                                    <th>
                                        优先权：
                                    </th>
                                    <td colspan="3">
                                        <asp:Literal ID="litYSQ_A" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div id="divAbs" class="initHidden">
                        <table cellspacing="1" class="compare">
                            <tr>
                                <td style="border-right: #6595d6 1px solid;" width="100%">
                                    <asp:Literal ID="LiteralAbs" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divBrief" class="initHidden">
                        <table cellspacing="1" class="compare">
                            <tr>
                                <td style="border-right: #6595d6 1px solid;" width="100%">
                                    <%--<asp:Image ID="LiteralBrief" Width="300" Height="200" runat="server" />--%>
                                    <div id="divImgFt" class="center" style='border: 1px solid #CCC; padding: 2px; width: 300px;
                                    height: 300px; vertical-align: middle; line-height: 300px; margin: 0 auto;'>
                                        <asp:Literal ID="LiteralImageFt" runat="server" />
                                    </div>
                                    <div id="divFtZoom" style="display: none; text-align: center;">
                                        <img src="/Images/iconRotate.png" width="24" height="24" onclick="imgRoll();" alt="旋转" />
                                        <img src="/Images/iconReverseHorizontal.png" width="24" height="24" onclick="imgReverse('H');"
                                            alt="水平翻转" />
                                        <img src="/Images/iconReverseVertical.png" width="24" height="24" onclick="imgReverse('V');"
                                            alt="垂直翻转" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divDes" class="initHidden">
                        <asp:HiddenField ID="hidDes" runat="server" Value="" />
                        <div id="divImglogingDes">
                            <img src="../Images/loading04.gif" />数据加载中.....</div>
                        <table id="tabDes" style="display: none;" cellspacing="1" class="compare">
                            <tr>
                                <td style="border-right: #6595d6 1px solid;" width="100%">
                                    <div id="divDes1">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divClaim" class="initHidden">
                        <asp:HiddenField ID="hidClaims" runat="server" Value="" />
                        <div id="imgloading">
                            <img src="../Images/loading04.gif" />数据加载中.....</div>
                        <table id="tableClaims" style="display: none;" cellspacing="1" class="compare">
                            <tr>
                                <td style="border-right: #6595d6 1px solid;" width="100%">
                                    <div id="claim1">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divLegal" class="initHidden">
                        <table cellspacing="1" class="compare">
                            <tr>
                                <td style="border-right: #6595d6 1px solid;" width="100%">
                                    <asp:Literal ID="litFlzt_A" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divQuote" class="initHidden">
                        <table cellspacing="1" class="compare">
                            <tr>
                                <td style="border-right: #6595d6 1px solid;" width="100%">
                                    <asp:Literal ID="LiteralQuote" runat="server" Text="Loading......" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
