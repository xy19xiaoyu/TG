<%@ Page Title="" Language="C#" MasterPageFile="~/Master/index.master" AutoEventWireup="true"
    CodeBehind="frmcnExpertSearch.aspx.cs" Inherits="Patentquery.My.frmcnExpertSearch"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <link href="../Css/cprs2010.css" rel="stylesheet" type="text/css" />--%>
    <link href="../Css/B_SeachPage.css" rel="stylesheet" type="text/css" />
    <link href="../Css/B_cprs2010.css" rel="stylesheet" type="text/css" />
    <link href="../jquery-easyui-1.8.0/plugins/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
    <script src="../Js/cnExpertSearch.js" type="text/javascript"></script>
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
                        DoPatSearch("", json.tmp_avatar, "CN", "2", "");
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
                    <input type="radio" name="RadioGroup1" value="中国专利" id="Radio1" checked="checked" />
                    中国专利</label>
                <label>
                    <input type="radio" name="RadioGroup1" value="世界专利" id="RadioGroup1_1" onclick="javascript:window.location='frmEnExpertSearch.aspx'" />
                    世界专利</label>
            </div>
        </div>
        <!--btn-->
        <div class="div_xiwl home2_table">
            <div class="home4_5 div_xiwl">
                <li id="Lab1" lang="An" value="申请号(AN)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>申请号</strong><br/>例:200820028064/AN</p>">申请号(AN)</li>
                <li id="Lab2" lang="Ad" value="申请日(AD)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>申请日</strong><br/>例:20030102/AD</p>">申请日(AD)</li>
                <li id="Lab3" lang="Pn" value="公开号(PN)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>公开号</strong><br/>例:1240461/PN</p>">公开号(PN)</li>
                <li id="Lab4" lang="Pd" value="公开日(PD)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>公开日</strong><br/>例:20030102/PD</p>">公开日(PD)</li>
                <li id="Lab5" lang="Gn" value="公告号(GN)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>公告号</strong><br/>例:1088075/GN</p>">公告号(GN)</li>
                <li id="Lab6" lang="Gd" value="公告日(GD)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>公告日</strong><br/>例:20030102/GD</p>">公告日(GD)</li>
                <li id="Lab7" lang="Ic" value="分类号(IC)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>分类号</strong><br/><b>IPC分类号:</b>大组部分7位,不足7位的须在大类后补0,/去除,/后为4位,不足4位的尾部补0(如:A01N 47/36=A01N0473600) <br/><b>例:</b>A01B/IC 或 A01N0473600/IC <br/><b>外观分类:</b>去除-,不足11位的尾部补0(如:09-05-B0038=0905B003800)</p>">
                    分类号(IC)</li>
                <li id="Lab17" lang="Mc" value="主分类号(MC)" onclick="addSearchEntrance(this.lang)"
                    style="cursor: pointer;" title="<p><strong>IPC 分类</strong><br/>例:A01B/MC</p>">主分类号(MC)</li>
                <li id="Lab6" lang="Ct" value="范畴分类(CT)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>范畴分类</strong><br/>例:21B/CT</p>">范畴分类(CT)</li>
                <li id="Lab18" lang="Pr" value="优先权号(PR)" onclick="addSearchEntrance(this.lang)"
                    style="cursor: pointer;" title="<p><strong>优先权号</strong><br/>例:CN20011342625   </p>">
                    优先权号(PR)</li>
                <li id="Li2" lang="Cc" value="审查员引用文献(CC)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="">审查员引用文献(CC)</li>
                <li id="Lab9" lang="Co" value="国省代码(CO)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>国省代码</strong><br/>例:95/CO或US/CO</p>">国省代码(CO)</li>
                <li id="Lab19" lang="In" value="发明人(IN)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>发明人</strong><br/>例:张三/IN</p>">发明人(IN)</li>
                <li id="Lab10" lang="Pa" value="申请人(PA)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>申请人</strong><br/>例:张三/PA</p>">申请人(PA)</li>
                <li id="Li1" lang="Po" value="权利人(PO)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="">权利人(PO)</li>
                <li id="Lab11" lang="TX" value="关键词(TX)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>关键词</strong><br/>例:计算机/TX</p>">关键词(TX)</li>
                <li id="Lab12" lang="Ti" value="发明名称(TI)" onclick="addSearchEntrance(this.lang)"
                    style="cursor: pointer;" title="<p><strong>发明名称</strong><br/>例:自行车/TI</p>">发明名称(TI)</li>
                <li id="Lab13" lang="Ag" value="代理机构代码(AG)" style="cursor: pointer;" onclick="addSearchEntrance(this.lang)"
                    title="<p><strong>代理机构</strong><br/>例:31244/AG</p>">代理机构代码(AG)</li>
                <li id="Lab18" lang="At" value="代理人(AT)" onclick="addSearchEntrance(this.lang)" style="cursor: pointer;"
                    title="<p><strong>发明人</strong><br/>例:张三/AT</p>">代理人(AT)</li>
                <li id="Lab14" lang="Dz" value="申请人地址(DZ)" style="cursor: pointer;" onclick="addSearchEntrance(this.lang)"
                    title="<p><strong>申请人地址</strong><br/>例:北京/DZ</p>">申请人地址(DZ)</li>
                <li id="Lab15" lang="Ab" value="摘要(AB)" style="cursor: pointer;" onclick="addSearchEntrance(this.lang)"
                    title="<p><strong>文摘</strong><br/>例:外喷放热气/AB</p>">摘要(AB)</li>
                <li id="Lab16" lang="Cl" value="主权利要求(CL)" style="cursor: pointer;" onclick="addSearchEntrance(this.lang)"
                    title="<p><strong>主权利要求</strong><br/>例:加煤系统/CL</p>">主权利要求(CL)</li>
                <li id="Lab17" lang="CS" value="权利要求(CS)" style="cursor: pointer;" onclick="addSearchEntrance(this.lang)"
                    title="<p><strong>权利要求</strong><br/>例:外喷放热气/CS</p>">权利要求(CS)</li>
                <li id="Lab18" lang="DS" value="说明书(DS)" style="cursor: pointer;" onclick="addSearchEntrance(this.lang)"
                    title="<p><strong>说明书</strong><br/>例:加煤系统/CL</p>">说明书(DS)</li>
            </div>
            <!--home4_1-->
            <div class=" home4_r ">
                <div runat="server" id="cnSearchHisTable" class="home4_6 div_xiwl" clientidmode="Static">
                    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="cnSearchHistoryGrid" runat="server" BorderWidth="1px" Width="98%"
                                CssClass="ExperGrd" AutoGenerateColumns="False" GridLines="Horizontal" RowStyle-Wrap="False">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <table id='tab<%#DataBinder.Eval(Container.DataItem,"DbID")%>' style="text-align: left;">
                                                <tr>
                                                    <%--复选框--%>
                                                    <td>
                                                        <input style="white-space: nowrap;" name="SearchHisCheckBox" type="checkbox" lang='<%#DataBinder.Eval(Container.DataItem,"DbID")%>'
                                                            runat="server" />
                                                    </td>
                                                    <%--查看按钮--%>
                                                    <td>
                                                        &nbsp; <span style="white-space: nowrap; text-align: left; color: #0066CC; text-decoration: underline;
                                                            cursor: pointer;" onclick="viewHis('<%#DataBinder.Eval(Container.DataItem,"HyperLink") %>')">
                                                            查看 </span>
                                                    </td>
                                                    <%--检索编号--%>
                                                    <td>
                                                        &nbsp; <span style="white-space: nowrap; background-color: Transparent; border: none;
                                                            font-size: 15px; font-weight: bold;" runat="server">(<%#DataBinder.Eval(Container.DataItem, "SearchNum")%>)
                                                        </span>
                                                    </td>
                                                    <%--日期--%>
                                                    <td>
                                                        &nbsp; <span style="white-space: nowrap; background-color: Transparent; border: none;
                                                            width: 90px; font-size: 15px; font-style: italic;" runat="server">
                                                            <%#DataBinder.Eval(Container.DataItem, "SearchDate")%></span>
                                                    </td>
                                                    <%--检索式--%>
                                                    <td width="350" title='<%#DataBinder.Eval(Container.DataItem, "SearchFormula")%>'>
                                                        &nbsp; <span style="text-align: left; border: none; font-size: 15px; white-space: nowrap;"
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
                            <asp:AsyncPostBackTrigger ControlID="cnSearchHistoryGrid" />
                            <asp:AsyncPostBackTrigger ControlID="BtnSearchHisUpdate" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="home4_1">
                    <div class="home4_2">
                        <%--<asp:RadioButtonList Width="120px" ID="select" runat="server" RepeatDirection="Horizontal"
                        onclick="Select()">
                        <asp:ListItem Text="全选" Value="全选">全选</asp:ListItem>
                        <asp:ListItem Text="清空" Value="清空">取消</asp:ListItem>
                    </asp:RadioButtonList>--%>
                        <label>
                            <input type="checkbox" id="select" name="chk" onclick="SelectAll();" />全选
                        </label>
                    </div>
                    <div class="home4_3">
                        <li><a href="javascript:;" onclick="DeleteSelected()">
                            <img src="../NewImg/bt7.jpg" title="删除所选检索历史记录" /></a></li>
                        <li><a href="javascript:;" style="display: ;">
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
        <%--<div class="home4_down div_xiwl">--%>
        <div class="home2_down div_xiwl">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="SearchTextBox" class="home2_input" onfocus="setViewResult('')" runat="server"
                            TextMode="MultiLine" ClientIDMode="Static" title="提示:可在检索式尾部添加@LX=FM,XX,WG对专利类型进行限定<br/>如:A01/IC+2012/AD @LX=FM,XX<br/>表示只需要检索发明和实用新型专利"></asp:TextBox>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td>
                        <a href="javascript:;">
                            <img id="BtnSearch" alt="检索" src="../images/btnQuery.png" style="cursor: hand; height: 87px;"
                                title="命令行检索" onclick="DoExpertSearchNew('Cn','1')" /></a>
                    </td>
                </tr>
            </table>
            <div class=" home4_d1">
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
            </div>
            <div class="home2_d2 ">
                <li></li>
                <li><a href="javascript:;" onclick="ClearSearch()">
                    <img id="Img3" alt="清空检索式" src="../NewImg/bt5.jpg" style="cursor: hand;" title="清空检索式" /></a>
            </div>
        </div>
    </div>
</asp:Content>
