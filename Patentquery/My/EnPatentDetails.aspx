<%@ Page Language="C#" MasterPageFile="~/Master/index.master" AutoEventWireup="true"
    Inherits="My_EnPatentDetails" Title="" CodeBehind="EnPatentDetails.aspx.cs" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        input[type="checkbox"]
        {
            margin-left: 5px;
            margin-top: 5px;
        }
    </style>
    <link href="../Css/B_SeachPage.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <link href="../Jquery-ui-1.10.3/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Jquery-ui-1.10.3/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.accordion.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Js/imageZoom.js"></script>
    <script src="../js/AddToCO.js" type="text/javascript"></script>
    <script src="../js/syscfg.js" type="text/javascript"></script>
    <script src="../Js/DetailsPage.js" type="text/javascript"></script>
    <script src="../Js/sysdialog.js" type="text/javascript"></script>
    <script src="../Js/StrComm.js" type="text/javascript"></script>
    <script src="../Js/Trans.js" type="text/javascript"></script>
    <script src="../Js/AddToIndex.js" type="text/javascript"></script>
    <script>
        //        $(function () {
        //            $("#tabs").tabs();
        //        });
        $(function () {
            IniteTabs(false, "EN");
            InitGL();
        });
    </script>
    <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true"
        runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="command">
                <div class="commandRight">
                    <a id="splitScreenDispaly" href="javascript:void(0);">
                        <img src='/Images/smallduibi_bj.png' alt='' />分屏显示</a> <a id="AddToCo" href="javascript:void(0);">
                            <img src='/Images/start.png' alt='' />
                            收藏</a>
                    <asp:LinkButton ID="LinkButtonDownload" Text="<img src='/Images/iconDownload.png' alt=''/> 下载"
                        OnClick="LinkButtonDownload_Click" runat="server" />
                </div>
            </div>
            <div style="display: none">
                <asp:Button ID="btnActiveTab" runat="server" Text="Button" OnClick="btnActiveTab_Click"
                    ClientIDMode="Static" />
                <asp:HiddenField ID="hidActiveTabTi" runat="server" ClientIDMode="Static" Value="著录项目信息" />
                <asp:HiddenField ID="hidActiveTabIdx" runat="server" ClientIDMode="Static" Value="0" />
            </div>
            <!-- Tab 开始-->
            <div id="DivPatDetailTabs" style="min-height: 550px; _height: 550px;">
                <ul id="ulPatTabs">
                    <li><a href="#tabMianXml">著录项目信息</a></li>
                    <li><a href="#DivtabPdf">全文PDF</a></li>
                    <li><a href="#divTabDes">说明书</a></li>
                    <li><a href="#divTabClams">权利要求</a></li>
                    <li><a href="#divTabLegal">法律状态</a></li>
                    <li><a href="#divTabQuote">引文信息</a></li>
                </ul>
                <div id="tabMianXml" title="著录项目信息" style="padding: 10px;">
                    &nbsp;&nbsp; <b>
                        <asp:Literal ID="LiteralTitle" runat="server" /></b>
                    <table width="100%" cellpadding="3" border="0" style="word-wrap: break-word; word-break: break-all;">
                        <tr>
                            <td style="width: 125px; background-color: #dbe0e6; text-align: right; vertical-align: middle;">
                                公开/公告号：
                            </td>
                            <td style="width: 450px; text-align: left; vertical-align: middle;">
                                <asp:Literal ID="LiteralPubNo" runat="server" />
                            </td>
                            <td style="background-color: #dbe0e6; width: 125px; text-align: right; vertical-align: middle;">
                                申请号：
                            </td>
                            <td style="vertical-align: middle; word-break: break-all; width: 300px;">
                                <asp:Literal ID="LiteralApNo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 125px; background-color: #dbe0e6; text-align: right; vertical-align: middle;">
                                公开/公告日：
                            </td>
                            <td style="width: 450px; text-align: left; vertical-align: middle;">
                                <asp:Literal ID="LiteralPubDate" runat="server" />
                            </td>
                            <td style="background-color: #dbe0e6; width: 125px; text-align: right; vertical-align: middle;">
                                申请日：
                            </td>
                            <td style="vertical-align: middle; word-break: break-all; width: 300px;">
                                <asp:Literal ID="LiteralApDate" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 125px; background-color: #dbe0e6; text-align: right; vertical-align: middle;">
                                申请人：
                            </td>
                            <td style="width: 450px; text-align: left; vertical-align: middle;">
                                <asp:Literal ID="LiteralApply" runat="server" />
                            </td>
                            <td style="background-color: #dbe0e6; width: 125px; text-align: right; vertical-align: middle;">
                                发明人：
                            </td>
                            <td style="vertical-align: middle; word-break: break-all; width: 300px;">
                                <asp:Literal ID="LiteralInventor" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 125px; background-color: #dbe0e6; text-align: right; vertical-align: middle;">
                                优先权：
                            </td>
                            <td colspan="2" style="vertical-align: middle; word-break: break-all; width: 570px;">
                                <asp:Literal ID="LiteralPri" runat="server" />
                            </td>
                            <td rowspan="5">
                                <div id="divImgFt" class="center" style='border: 1px solid #CCC; padding: 2px; width: 300px;
                                    height: 300px; vertical-align: middle; line-height: 300px;'>
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
                        <tr>
                            <td style="width: 125px; background-color: #dbe0e6; text-align: right; vertical-align: middle;">
                                国际分类：
                            </td>
                            <td colspan="2" style="vertical-align: middle; word-break: break-all; width: 570px;">
                                <asp:Literal ID="LiteralIpc" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 125px; background-color: #dbe0e6; text-align: right; vertical-align: middle;">
                                欧洲分类：
                            </td>
                            <td colspan="2" style="vertical-align: middle; word-break: break-all; width: 570px;">
                                <asp:Literal ID="litEcla" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 125px; background-color: #dbe0e6; text-align: right; vertical-align: middle;">
                                引用文献：
                            </td>
                            <td colspan="2" style="vertical-align: middle; word-break: break-all; width: 570px;">
                                <asp:Literal ID="litRef" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="vertical-align: middle; word-break: keep-all; width: 725px;">
                                <b>摘 &nbsp;要：</b>
                                <asp:Literal ID="litAbs" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <div id="divAccordion">
                        <h3>
                            同族专利
                        </h3>
                        <div class="Aunderline">
                            <asp:Literal ID="LiteralSimilar" runat="server" />
                        </div>
                        <h3 style="display: none">
                            专利家族摘要
                        </h3>
                        <div style="display: none">
                            <asp:Literal ID="litFmyAbs" runat="server" />
                        </div>
                        <h3 id="h3Note" class="open">
                            自定义标注</h3>
                        <div id="divSetBz">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="98%"
                                DataKeyNames="CollectId" OnRowUpdating="GridView1_RowUpdating">
                                <EmptyDataTemplate>
                                    暂无标注信息!
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <table style="border-right-width: 0px; background-color: #cccccc; width: 100%; border-top-width: 0px;
                                                text-align: center; border-bottom-width: 0px; border-left-width: 0px" id="Table1"
                                                border="0" cellspacing="1" cellpadding="4">
                                                <tr style="height: 30px">
                                                    <td style="background-color: #e1ecf3; width: 10%; text-align: right;">
                                                        收藏夹：
                                                    </td>
                                                    <td style="background-color: white; width: 18%" align="left">
                                                        <%--<%# DataBinder.Eval(Container.DataItem, "AppNo")%>--%>
                                                        <%# DataBinder.Eval(Container.DataItem, "floder")%>
                                                    </td>
                                                    <td style="background-color: #e1ecf3; width: 18%" align="right">
                                                        标注时间：
                                                    </td>
                                                    <td style="background-color: white; width: 120px" align="left">
                                                        <%# DataBinder.Eval(Container.DataItem, "NoteDate")%>
                                                    </td>
                                                </tr>
                                                <tr style="background-color: white; height: 40px" align="left">
                                                    <td colspan="4">
                                                        <table width="100%">
                                                            <tr>
                                                                <td width="100%">
                                                                    <asp:TextBox ID="txbNote" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Note")%>'
                                                                        TextMode="MultiLine" Rows="3" Width="99%"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="bntUp" runat="server" Text="修改" Height="50px" CommandName="update" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div id="DivtabPdf" title="全文PDF" closable="false" style="padding: 2px;">
                    <asp:Literal ID="LiteralPdf" runat="server" Text="Loading......" EnableViewState="false"
                        ViewStateMode="Disabled" />
                    <asp:HiddenField ID="HiddenField1Pdf" runat="server" EnableViewState="false" ViewStateMode="Disabled"
                        ClientIDMode="Static" />
                </div>
                <div id="divTabDes" title="说明书" closable="false" style="padding: 10px;">
                    <asp:Literal ID="LiteralBook" Visible="true" runat="server" Text="Loading......" />
                </div>
                <div id="divTabClams" title="权利要求" icon="icon-reload" closable="false" style="padding: 10px;">
                    <asp:Literal ID="LiteralRights" Visible="true" runat="server" Text="Loading......" />
                </div>
                <div id="divTabLegal" title="法律状态" closable="false" style="padding: 10px;">
                    <asp:Literal ID="LiteralLeagl" Visible="true" runat="server" Text="Loading......" />
                </div>
                <div id="divTabQuote" title="引文信息" closable="false" style="padding: 10px;">
                    <p>
                        <strong>申请人引用:</strong></p>
                    <asp:Literal ID="LiteralQuote_1" Visible="true" runat="server" Text="Loading......" />
                    <hr align="center" width="100%" size="1" style="margin: 10px 0;" />
                    <p>
                        <strong>非申请人引用:</strong></p>
                    <asp:Literal ID="LiteralQuote_2" Visible="true" runat="server" Text="Loading......" />
                </div>
            </div>
            <!-- Tab 结束-->
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="LinkButtonDownload" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- 收藏夹 开始-->
    <div id="AddCO" style="min-width: 300px; display: none; z-index: 9999;" class="ke-dialog">
        <div style="min-height: 250px">
            <ul id="CO" class="easyui-tree" style="min-width: 300px" data-options="lines:true,checkbox:true,cascadeCheck:false" />
        </div>
        <hr></hr>
        <div style="width: 318px">
            自定义标注：
            <textarea name="TextBoxNote" rows="5" cols="45" style="width: 290px" id="TextBoxNote"></textarea>
        </div>
    </div>
    <div id="DivAddNode" style="min-width: 400px; display: none; z-index: 9999;">
        <span id="dialogtitle" style="display: none"></span>
        <table>
            <tr>
                <th>
                    收藏夹名称:
                </th>
                <td>
                    <input type="text" id="newclass" name="newclass" style="width: 280px" /><br />
                    <span id="showmsg" style="display: none; color: Red;">*请输入收藏夹名称</span>
                </td>
            </tr>
            <tr>
                <th>
                    简介：
                </th>
                <td>
                    <textarea id="txtNodeDes" cols="200" name="txtNodeDes" rows="40" style="width: 280px;
                        height: 100px"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <!-- 收藏夹 结束 -->
    <div id="DLGAddIndex" style="position: absolute; top: 200px; left: 50px; width: 200px;
        height: 500px; display: none;">
        <table id="pg" class="easyui-propertygrid" style="width: 200px; height: 500px;">
        </table>
        <input type="button" id="btnindex" onclick="AddIndex('en')" value="标引" style="width: 50px;
            height: 20px; float: right; margin-top: 10px; margin-right: 10px;" />
    </div>
    <!-- 同族专利-->
    <div id="sm" style="width: 520px; height: 300px; display: none;">
        <iframe id="ism" name="ism" scrolling="no" frameborder="0" src="" style="width: 100%;
            height: 100%;"></iframe>
    </div>
    <!-- 同族专利结束-->
    <script type="text/javascript" language="javascript">
        //设置updatepanel回调客户端JS
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            // re-bind your jquery events here
            //////////////////////////////////////// 
            //隐藏遮照层
            try {
                IniteTabs(true, "EN");
            } catch (e) {
            }
        }
    );
    </script>
</asp:Content>
