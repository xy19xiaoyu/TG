<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Index.master" AutoEventWireup="true"
    Inherits="Trans_TransMain" CodeBehind="TransMain.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Css/B_SeachPage.css" rel="stylesheet" type="text/css" />
    <link href="../Jquery-ui-1.10.3/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Jquery-ui-1.10.3/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.accordion.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../Js/Translation.js" type="text/javascript"></script>
    <script>
        $(function () {
            $("#DivTabs").tabs();
        });       
    </script>
    <div>
        <div style="padding-top: 5px; filter: alpha(Opacity=0); -moz-opacity: 0; opacity: 0;">
        </div>
        <div id="DivTabs" style="width: 98%px;">
            <ul id="ulPatTabs">
                <li><a href="#divEn2Cn">英汉翻译</a></li>
                <li><a href="#divCn2En">汉英翻译</a></li>
            </ul>
            <!-- 英译汉 开始 -->
            <br />
            <div style="display: none">
                &nbsp; &nbsp;<input id="Cprs1" type="radio" name="TranEngine" checked="checked" value="cprs" /><strong>CPRS翻译</strong>
                &nbsp; &nbsp;<input id="Google1" type="radio" name="TranEngine" value="google" /><strong>Google翻译</strong>
                &nbsp; &nbsp;
            </div>
            <div id="divEn2Cn" title="英汉翻译" style="padding: 10px">
                <table width="100%" style="border-collapse: collapse;">
                    <tr>
                        <td colspan="2">
                            请输入英文:
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtInputContent1" runat="server" TextMode="MultiLine" Rows="10"
                                Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%">
                            <div id="loading11" style="display: none">
                                <img src="../Images/gifLoading.gif" alt="" id="loading1" /></div>
                            &nbsp;
                        </td>
                        <td align="right">
                            <span class="button" onclick="translationCustomServices('2')">CPRS翻译</span> &nbsp;<span class="button"
                                onclick="translationCustomServices('2','gl')">Google翻译</span> &nbsp;<span class="button" onclick="translationCustomServices('2','bd')">百度翻译
                                </span> &nbsp;<span class="button" onclick="translationCustomServices('2','yd')">有道翻译</span> &nbsp;<span class="button"
                                    onclick="clearInputContent('2')">清空 </span>&nbsp;&nbsp; &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtTranslateContent1" runat="server" TextMode="MultiLine" Rows="10"
                                Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- 英译汉 结束 -->
            <!-- 汉译英 开始 -->
            <div id="divCn2En" title="汉英翻译" style="padding: 10px">
                <table width="100%" style="border-collapse: collapse;">
                    <tr>
                        <td colspan="2">
                            请输入中文(可以输入3000个汉字,包括标点符号和空格回车):
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtInputContent" runat="server" TextMode="MultiLine" Rows="10" Width="98%"
                                MaxLength="1000"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%">
                            <div id="loading22" style="display: none">
                                <img src="../Images/gifLoading.gif" alt="" id="loading2" />
                            </div>
                            &nbsp;
                        </td>
                        <td align="right">
                            <span class="button" onclick="translationCustomServices('1')">CPRS翻译</span> &nbsp;<span class="button"
                                onclick="translationCustomServices('1','gl')">Google翻译</span> &nbsp;<span class="button" onclick="translationCustomServices('1','bd')">百度翻译
                                </span> &nbsp;<span class="button" onclick="translationCustomServices('1','yd')">有道翻译</span> &nbsp;<span class="button"
                                    onclick="clearInputContent('1')">清空 </span>&nbsp;&nbsp; &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtTranslateContent" runat="server" TextMode="MultiLine" Rows="10"
                                Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- 汉译英 结束 -->
        </div>
        <div style="margin: 10px 0; display: none">
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="$('#dlg').dialog('open')">
                翻译</a>
            <div id="dlg" class="easyui-dialog" title="实时翻译" data-options="iconCls:'icon-tip'"
                style="width: 300px; height: 200px; padding: 10px" closed="true">
                <table style="width: 100%;" border="0" cellpadding="2" cellspacing="0">
                    <tr>
                        <td colspan="2">
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            <input id="rdbC2E" type="radio" value="1" checked="checked" name="TransType" />中译英&nbsp;&nbsp;
                            <input id="rdbE2C" type="radio" value="2" name="TransType" />英译中
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;原&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;词:
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="txtWord" runat="server" MaxLength="15"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;翻译结果:
                        </td>
                        <td>
                            &nbsp;<asp:TextBox ID="lbTransResult" runat="server" MaxLength="15"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <a onclick="TranslateTool()" id="atrans">翻&nbsp;译</a>
                            <img id="loading" src="../Images/loading04.gif" alt="正在翻译" title="正在翻译.." />
                            <br />
                            <span id="showMes"></span>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
