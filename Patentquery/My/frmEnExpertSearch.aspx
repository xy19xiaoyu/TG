<%@ Page Title="" Language="C#" MasterPageFile="~/Master/index.master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="frmEnExpertSearch.aspx.cs" Inherits="Patentquery.My.frmEnExpertSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<link href="../Css/cprs2010.css" rel="stylesheet" type="text/css" />--%>
    <link href="../Css/B_SeachPage.css" rel="stylesheet" type="text/css" />
    <link href="../Css/B_cprs2010.css" rel="stylesheet" type="text/css" />
    <link href="../jquery-easyui-1.8.0/plugins/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
    <script src="../Js/docdbExpertSearch.js" type="text/javascript"></script>
    <script src="../Js/ExpertSearchPage.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script src="../jquery-easyui-1.8.0/jquery.query-2.1.7.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/plugins/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/plugins/jQuery.niceTitle.js" type="text/javascript"></script>
    <script src="../Js/errorTips.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/cprs/jQuery.cprs.Core.js" type="text/javascript"></script>
    <script src="../Js/progressbar.js" type="text/javascript"></script>
    <script src="../Js/CN_EntrancesTip.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="../js/file-uploader/fileuploader.css" />
    <script type="text/javascript" language="javascript" src="../js/file-uploader/fileuploader.min.js"></script>
    <script type="text/javascript" language="javascript">
        $(function () {
            new qq.FileUploader({
                element: document.getElementById('upload_avatar'),
                action: "../zt/ZtFileUpLoad.ashx",
                multiple: false,
                disableDefaultDropzone: true,
                allowedExtensions: ['txt', 'ini'],
                uploadButtonText: '导入外部号单...',
                onComplete: function (id, fileName, json) {
                    if (json.success) {
                        //$("#crop_tmp_avatar").val(json.tmp_avatar);
                        //$("#crop_container").show();
                        //$("#crop_preview").html('<img style="width: 130px; height: 90px;" src="../ZtHeadImg/' + json.tmp_avatar + '">');
                        //$("#crop_preview").attr("title", json.tmp_avatar);
                        //alert(json.description);
                        DoPatSearch("", tmp_avatar, "EN", "2", "");
                    }
                    else {
                        alert(json.description);
                    }
                }
            });
        });      
    </script>
    <div id="mytips" style="border-right: gray 1px solid; border-top: gray 1px solid;
        z-index: 2; display: none; left: 0px; border-left: gray 1px solid; border-bottom: gray 1px solid;
        position: absolute; text-align: center; font-size: 15px; background-color: #ffffff;
        vertical-align: middle;">
    </div>
    <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true"
        runat="server">
    </asp:ScriptManager>
    <div class="div_Content_xiwl home2_con">
        <div class="home_sel">
            <div class="home_sel1">
                <label>
                    <input type="radio" name="RadioGroup1" value="中国专利" id="Radio1" onclick="javascript:window.location='frmCnExpertSearch.aspx'" />
                    中国专利</label>
                <label>
                    <input type="radio" name="RadioGroup1" value="世界专利" checked="checked" id="RadioGroup1_1" />
                    世界专利</label>
            </div>
        </div>
        <!--btn-->
        <div class="div_xiwl home2_table">
            <div class="home4_5 div_xiwl">
                <li id="Lab1" lang="An" value="申请号(AN)" style="cursor: pointer;" onclick="addSearchEntrance(this.lang)"
                    title="<p><strong>申请号</strong><br/>例:GB9818481A/AN</p>">申请号(AN)</li>
                <li id="Lab2" lang="Ad" value="申请日(AD)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>申请日</strong><br/>格式:(YYYY 或 YYYYMM 或 YYYYMMDD)<br/>例:20021201/AD</p>">
                    申请日(AD)</li>
                <li id="Lab3" lang="Pn" value="公开/公告号(PN)" onclick="addSearchEntrance(this.lang)"
                    style="cursor: pointer;" title="<p><strong>公开号</strong><br/>例:GB9818481D0/PN</p>">
                    公开/公告号(PN)</li>
                <li id="Lab4" lang="Pd" value="公开/公告日(PD)" onclick="addSearchEntrance(this.lang)"
                    style="cursor: pointer;" title="<p><strong>公开日</strong><br/>格式:(YYYY 或 YYYYMM 或 YYYYMMDD)<br/>例:2003/PD 或 20030101/PD</p>">
                    公开/公告日(PD)</li>
                <li id="Lab5" lang="Ic" value="分类号(IC)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>IPC 分类</strong><br/>例:A01B/IC 或 G06F17/30/IC</p>">分类号(IC)</li>
                <li id="Lab20" lang="Mc" value="主分类号(MC)" onclick="addSearchEntrance(this.lang)"
                    style="cursor: pointer;" title="<p><strong>IPC 分类</strong><br/>例:A01B</p>">主分类号(MC)</li>
                <li id="Lab6" lang="Pr" value="优先权号(PR)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>优先权号</strong><br/>例:CN20020149/PR</p>">优先权号(PR)</li>
                <li id="Lab7" lang="In" value="发明人(IN)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>发明人</strong><br/>例:tom/IN</p>">发明人(IN)</li>
                <li id="Lab8" lang="Pa" value="申请人(PA)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>申请人</strong><br/>例:tom/PA</p>">申请人(PA)</li>
                <li id="Lab18" lang="Ti" value="发明名称(TI)" onclick="addSearchEntrance(this.lang)"
                    style="cursor: pointer;" title="<p><strong>发明名称</strong><br/>例:computer/TI</p>">
                    发明名称(TI)</li>
                <li id="Lab9" lang="Ab" value="摘要(AB)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>文摘</strong><br/>例:computer/AB</p>">摘要(AB)</li>
                <li id="Lab19" lang="Ct" value="引用文献(CT)" onclick="addSearchEntrance(this.lang)"
                    style="cursor: pointer;" title="<p><strong>引用文献</strong><br/>例:GB9818481D0A/CT</p>">
                    引用文献(CT)</li>
                <li id="Lab10" lang="Ec" value="欧洲分类(EC)" onclick="addSearchEntrance(this.lang)"
                    style="cursor: pointer;" title="<p><strong>欧洲分类</strong><br/>例:B60R21/EC</p>">欧洲分类(EC)</li>
                <%--<li id="Lab11" lang="Ro" value="国别(RO)" onmousemove="seashowtip(this.name,1,150,event)"
                        onmouseout="seashowtip(this.name,0,150,event)" onclick="addSearchEntrance(this.lang)"
                        style="cursor: hand;" name="<p><strong>国别</strong><br/>例:CN</p>">
                        国别(RO)</li>--%>
            </div>
            <!--home4_1-->
            <div class=" home4_r ">
                <div runat="server" id="docdbSearchHisTable" class="home4_6 div_xiwl" clientidmode="Static">
                    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="docdbSearchHistoryGrid" runat="server" BorderWidth="1px" Width="98%"
                                CssClass="ExperGrd" AutoGenerateColumns="False" GridLines="Horizontal" RowStyle-Wrap="False">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <table id='tab<%#DataBinder.Eval(Container.DataItem,"DbID")%>' style="text-align: left;">
                                                <tr>
                                                    <%--复选框--%>
                                                    <td>
                                                        <input id="Checkbox1" style="white-space: nowrap;" name="SearchHisCheckBox" type="checkbox"
                                                            lang='<%#DataBinder.Eval(Container.DataItem,"DbID")%>' runat="server" />
                                                    </td>
                                                    <%--查看按钮--%>
                                                    <td>
                                                        &nbsp; <span style="white-space: nowrap; text-align: left; color: #0066CC; text-decoration: underline;
                                                            cursor: pointer;" onclick="viewHis('<%#DataBinder.Eval(Container.DataItem,"HyperLink") %>')">
                                                            查看 </span>
                                                    </td>
                                                    <%--检索编号--%>
                                                    <td>
                                                        &nbsp; <span id="Span1" style="white-space: nowrap; background-color: Transparent;
                                                            border: none; font-size: 15px; font-weight: bold;" runat="server">(<%#DataBinder.Eval(Container.DataItem, "SearchNum")%>)
                                                        </span>
                                                    </td>
                                                    <%--日期--%>
                                                    <td>
                                                        &nbsp; <span id="Span2" style="white-space: nowrap; background-color: Transparent;
                                                            border: none; width: 90px; font-size: 15px; font-style: italic;" runat="server">
                                                            <%#DataBinder.Eval(Container.DataItem, "SearchDate")%></span>
                                                    </td>
                                                    <%--检索式--%>
                                                    <td width="350" title='<%#DataBinder.Eval(Container.DataItem, "SearchFormula")%>'>
                                                        &nbsp; <span id="Span3" style="text-align: left; border: none; font-size: 15px; white-space: nowrap;"
                                                            runat="server">
                                                            <%#DataBinder.Eval(Container.DataItem, "SearchFormula")%>
                                                        </span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Height="15px"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle Font-Bold="True" Font-Size="15px" HorizontalAlign="Left" CssClass="PagerCss" />
                                <RowStyle Height="35px" Wrap="False" />
                            </asp:GridView>
                            <span style="visibility: hidden;">
                                <asp:Button ID="BtnSearchHisUpdate" runat="server" Text="更新历史" OnClick="BtnSearchHisUpdate_Click"
                                    ClientIDMode="Static" Height="1px" />
                            </span>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="docdbSearchHistoryGrid" />
                            <asp:AsyncPostBackTrigger ControlID="BtnSearchHisUpdate" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="home4_1">
                    <div class="home4_2">
                        <label>
                            <input type="checkbox" id="select" name="chk" onclick="SelectAll();" />全选</label>
                    </div>
                    <div class="home4_3">
                        <li><a href="javascript:;" onclick="DeleteSelected()">
                            <img src="../NewImg/bt7.jpg" title="删除所选检索历史记录" /></a></li>
                        <li><a href="javascript:;" style="">
                            <asp:ImageButton ID="btnExportSearchHis" runat="server" OnClick="LinkButtonExport_Click"
                                OnClientClick="return checkHasSelect()" ImageUrl="~/NewImg/import.jpg" ToolTip="导出检索历史记录" />
                        </a></li>
                        <li style="display: none">
                            <div id="upload_avatar">
                            </div>
                        </li>
                    </div>
                </div>
            </div>
            <!--concent-->
            <div id="BibliographicValidation" style="font-style: italic; color: #FF0000">
                <div id="LabValidationResult">
                </div>
            </div>
        </div>
        <!--table-->
        <div class="home2_down div_xiwl">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="SearchTextBox" class="home2_input" onfocus="setViewResult('')" runat="server"
                            title="示例:compute/TI+A01B/IC<br/>提示:可在检索式尾部添加@CO=CN,US对国别进行限定<br/>如:compute/TI+A01B/IC @CO=US,CN<br/>表示只需要检索美国和中国专利"
                            TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td>
                        <a href="javascript:;" id="BtnSearch" onclick="DoExpertSearchNew('En','1');">
                            <img alt="检索" src="../images/btnQuery.png" style="cursor: hand; height: 87px;" title="命令行检索" /></a>
                    </td>
                </tr>
            </table>
            <div class="home4_d1">
                <li><a href="javascript:;" value="*" name="*" onclick="addcommand(this)">
                    <img src="../images/img/home-09.jpg" title="应用示例：&lt;br/>  A*B:同时包含A和B" /></a></li>
                <li><a href="javascript:;" value="+" name="+" onclick="addcommand(this)">
                    <img src="../images/img/home-10.jpg" title="应用示例：&lt;br/> A+B:包含A或者B" /></a></li>
                <li><a href="javascript:;" value="-" name="-" onclick="addcommand(this)">
                    <img src="../images/img/home-11.jpg" title="应用示例：&lt;br/>  A-B:包含A且不包含B" /></a></li>
                <li><a href="javascript:;" value="(" name="(" onclick="addcommand(this)">
                    <img src="../images/img/home-12.jpg" title="应用示例：&lt;br/>  （）:括号内的内容优先计算" /></a></li>
                <li><a href="javascript:;" value=")" name=")" onclick="addcommand(this)">
                    <img src="../images/img/home-13.jpg" title="应用示例：&lt;br/> （）:括号内的内容优先计算" /></a></li>
                <li><a href="javascript:;" value="$" name="$" onclick="addcommand(this)">
                    <img src="../images/img/home-14.jpg" title="应用示例：&lt;br/>  A$:所有前缀包含A的单词" /></a></li>
                <li><a href="javascript:;">
                    <img value="ADJ" name="&lt;ADJn>" onclick="addcommand(this)" src="../images/img/home-15.jpg"
                        title="应用示例：&lt;br/> A&nbsp;ADJn&nbsp;B:A和B之间有0-n个词，且A和B前后顺序不能变化；&lt;br/>n属于(0-9)" /></a></li>
                <li><a href="javascript:;">
                    <img value="NEAR" name="&lt;NEARn>" onclick="addcommand(this)" src="../images/img/home-16.jpg"
                        title="应用示例：&lt;br/>  A&nbsp;NEARn&nbsp;B:A和B之间有0-n个词，且A和B前后顺序可以变化；&lt;br/>n属于(0-9)" /></a></li>
                <li><a href="javascript:;">
                    <img value="SKIP" name="&lt;SKIPn>" onclick="addcommand(this)" src="../images/img/home-17.jpg"
                        title="应用示例：&lt;br/>  A&nbsp;SKIPn&nbsp;B:A和B之间只能有n个词，词序不能变化；&lt;br/>n属于(0-9)" /></a></li>
            </div>
            <div class="home2_d2">
                <li></li>
                <li><a href="javascript:;" onclick="ClearSearch()">
                    <img id="Img3" alt="清空检索式" src="../NewImg/bt5.jpg" style="cursor: hand;" title="清空检索式" /></a></li>
            </div>
        </div>
    </div>
</asp:Content>
