<%@ Page Title="" Language="C#" MasterPageFile="~/Master/index.master" AutoEventWireup="true"
    CodeBehind="frmWdTbSearch.aspx.cs" Inherits="Patentquery.My.frmWdTbSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../jquery-easyui-1.8.0/plugins/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
    <link href="../css/themes/default/default.css" rel="stylesheet" type="text/css" />
    <link href="../Css/B_SeachPage.css" rel="stylesheet" type="text/css" />
    <link href="../Css/B_cprs2010.css" rel="stylesheet" type="text/css" />
    <script src="../Js/docdbTableSearch.js" type="text/javascript"></script>
    <script src="../Js/TableSearchPage.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
    <script src="../jquery-easyui-1.8.0/jquery.query-2.1.7.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/plugins/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/plugins/jQuery.niceTitle.js" type="text/javascript"></script>
    <script src="../Js/errorTips.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/cprs/jQuery.cprs.Core.js" type="text/javascript"></script>
    <script src="../Js/progressbar.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/easydialog.min.js"></script>
    <script src="../Js/CN_EntrancesTip.js" type="text/javascript"></script>
    <div class="div_Content_xiwl home2_con">
        <div class="home_sel">
            <div class="home_sel1" style="width: 400px;">
                <label>
                    <input type="radio" name="RadioGroup1" value="中国专利" id="Radio1" onclick="javascript:window.location='frmcnTbSearch.aspx'" />
                    中国专利</label>
                <label>
                    <input type="radio" name="RadioGroup1" value="世界专利" checked="checked" id="RadioGroup1_1" />
                    世界专利</label>
                <label id="labSetTable" runat="server">
                    <a href="javascript:;" onclick="OpenSetTable();" style="cursor: hand; color: Black;">
                        &nbsp;&nbsp;&nbsp;&nbsp;配置表格项</a></label>
            </div>
        </div>
        <!--btn-->
        <%--<div class="div_xiwl home2_table">--%>
        <div id="divQueryTable" class="home2_table div_xiwl" style="overflow: auto;">
            <table width="98%" border="0" cellspacing="7" cellpadding="0">
                <tr>
                    <td width="146" height="35">
                        <div class="home2_tat">
                            <input type="checkbox" id="chkAll" onclick="Select();" />全选：</div>
                    </td>
                    <td colspan="6">
                        <div class="home3_set">
                            <ul>
                                <li>
                                    <label><input name="checkbox" type="checkbox" value="CN" />中国[CN]</label></li>
                                <li>
                                    <label><input name="checkbox" type="checkbox" value="US" />美国[US]</label></li>
                                <li>
                                    <label><input name="checkbox" type="checkbox" value="DE" />德国[DE]</label></li>
                                <li>
                                    <label><input name="checkbox" type="checkbox" value="JP" />日本[JP]</label></li>
                                <li>
                                    <label><input name="checkbox" type="checkbox" value="GB" />英国[GB]</label></li>
                                <li>
                                    <label><input name="checkbox" type="checkbox" value="FR" />法国[FR]</label></li>
                                <li>
                                    <label><input name="checkbox" type="checkbox" value="KR" />韩国[KR]</label></li>
                                <li>
                                    <label><input name="checkbox" type="checkbox" value="RU" />俄罗斯[RU]</label></li>
                                <li>
                                    <label><input name="checkbox" type="checkbox" value="CH" />瑞士[CH]</label></li>
                                <li>
                                    <label><input name="checkbox" type="checkbox" value="EP" />EPO[EP]&nbsp;</label></li>
                                <li>
                                    <label><input name="checkbox" type="checkbox" value="WO" />WIPO[WO]</label></li>
                                <li style="display: ">
                                    <label><input name="checkbox" type="checkbox" value="ELSE" />其他国家及地区[OT]&nbsp;<img style="display: none"
                                        src="../images/img/home-20.jpg" /></label></li>
                            </ul>
                        </div>
                    </td>
                </tr>
            </table>
            <dl>
                <dt id="dtTbTI">发明名称(TI)：</dt>
                <dd>
                    <asp:TextBox class="searchinput" ID="Txt1" runat="server" lang="Ti" onblur="validateBib(this)"
                        title="<p><strong>发明名称</strong>|例:computer</p>" ClientIDMode="Static"></asp:TextBox>
                    <img src="../images/img/home-08.jpg" title=" <strong>应用实例</strong> | 1、已知内容包含computer，应键入：computer | 2、已知内容包含computer和system，应键入：computer*system | 3、已知内容包含computer或keyboard，应键入：computer+keyboard | 4、已知内容包含computer，不包含keyboard，应键入：computer - keyboard | 5、已知内容包含computer，不包含application和system，应键入：computer -（application*system） | 注：1、使用前方一直符号“$”时，可能会获得其他不相干结果；例如：com$包含computer及comprise等英文单词。" />
                </dd>
                <dt id="dtTbAB">摘要(AB)：</dt>
                <dd>
                    <asp:TextBox class="searchinput" ID="Txt2" runat="server" lang="Ab" onblur="validateBib(this)"
                        title="<p><strong>文摘</strong>|例:computer</p>" ClientIDMode="Static"></asp:TextBox>
                    <img src="../images/img/home-08.jpg" title=" <strong>应用实例</strong> | 1、已知内容包含computer，应键入：computer | 2、已知内容包含computer和system，应键入：computer*system | 3、已知内容包含computer或keyboard，应键入：computer+keyboard | 4、已知内容包含computer，不包含keyboard，应键入：computer - keyboard | 5、已知内容包含computer，不包含application和system，应键入：computer -（application*system） | 注：1、使用前方一直符号“$”时，可能会获得其他不相干结果；例如：com$包含computer及comprise等英文单词。" />
                </dd>
                <dt id="dtTbAN">申请号(AN):</dt>
                <dd>
                    <asp:TextBox class="searchinput" ID="Txt3" runat="server" lang="An" onblur="validateBib(this)"
                        title="<p><strong>申请号</strong>|例:GB9818481A</p>" ClientIDMode="Static"></asp:TextBox>
                    <img src="../images/img/home-08.jpg" title="<strong>应用实例</strong> | 1、已知完整申请号，应键入：GB19170014070 | 2、已知申请号前五位，应键入：GB19170 | 3、已知申请号包含GB19170014070或GB19170013562，应键入：GB19170014070+ GB19170013562 | 注：1、申请号检索必须输入国别代码 " /></dd>
                <dt id="dtTbAD">申请日(AD):</dt>
                <dd>
                    <asp:TextBox class="searchinput" ID="Txt4" runat="server" lang="Ad" onblur="validateBib(this)"
                        title="<p><strong>申请日</strong><br/>格式:(YYYY 或 YYYYMM 或 YYYYMMDD)<br/>例:20021201</p>"
                        ClientIDMode="Static"></asp:TextBox>
                    <img src="../images/img/home-08.jpg" title="<strong>应用实例</strong> | 1、已知完整日期，应键入：19990205 | 2、已知月份，应键入：199902 | 3、已知年份，应键入：1999 | 4、已知时间的连续范围(不大于5年)，应键入：20060202>20090101 | 5、已知时间范围包含2008年或2009年，应键入：2008+2009 | 注：申请日/公开日检索日期格式为YYYY或YYYYMM或YYYYMMDD" /></dd>
                <dt id="dtTbPN">公开/公告号(PN):</dt>
                <dd>
                    <asp:TextBox class="searchinput" ID="Txt5" runat="server" lang="Pn" onblur="validateBib(this)"
                        title="<p><strong>公开号</strong><br/>例:GB9818481D</p>" ClientIDMode="Static"></asp:TextBox>
                    <img src="../images/img/home-08.jpg" title="<strong>应用实例</strong> | 1、已知完整公开号，应键入：GB1529429A | 2、已知公开号前五位，应键入：GB15294 | 3、已知公开号包含GB1529429A或GB107791A，应键入：GB1529429A+GB107791A | 注：公开号检索必须输入国别代码" /></dd>
                <dt id="dtTbPD">公开/公告日(PD):</dt>
                <dd>
                    <asp:TextBox class="searchinput" ID="Txt6" runat="server" lang="Pd" onblur="validateBib(this)"
                        title="<p><strong>公开日</strong><br/>格式:(YYYY 或 YYYYMM 或 YYYYMMDD)<br/>例:2003</p>"
                        ClientIDMode="Static"></asp:TextBox>
                    <img src="../images/img/home-08.jpg" title="<strong>应用实例</strong> | 1、已知完整日期，应键入：19990205 | 2、已知月份，应键入：199902 | 3、已知年份，应键入：1999 | 4、已知时间的连续范围，应键入：20060202>20090101 | 5、已知时间范围包含2008年或2009年，应键入：2008+2009 | 注：申请日/公开日检索日期格式为YYYY或YYYYMM或YYYYMMDD" /></dd>
                <dt id="dtTbPA">申请人(PA):</dt>
                <dd>
                    <asp:TextBox class="searchinput" ID="Txt7" runat="server" lang="Pa" onblur="validateBib(this)"
                        title="<p><strong>申请人</strong><br/>例:tom</p>" ClientIDMode="Static"></asp:TextBox>
                    <img src="../images/img/home-08.jpg" title="<strong>应用实例</strong> | 1、已知完整申请人姓名，应键入：YOUNG BAE SOHN  | 2、已知发明（申请）人的一半姓名，应键入：tho$ | 3、已知发明（申请）人姓名包含thomas和yoshiki，应键入：thomas*yoshiki | 4、已知发明（申请）人姓名包含thomas或yoshiki，应键入：thomas+yoshiki | 5、已知发明（申请）人姓名包含thomas，不包含yoshiki，应键入：thomas - yoshiki | 6、已知发明（申请）人姓名包含thomas，不包含yoshiki和kathrine，应键入：thomas-（yoshiki*kathrine） | 注：1、使用前方一直符号“$”时，可能会获得其他不相干结果；例如：tho$包含thomas及thomason等英文单词。" /></dd>
                <dt id="dtTbIN">发明人(IN):</dt>
                <dd>
                    <asp:TextBox class="searchinput" ID="Txt8" runat="server" lang="In" onblur="validateBib(this)"
                        title="<p><strong>发明人</strong><br/>例:tom</p>" ClientIDMode="Static"></asp:TextBox>
                    <img src="../images/img/home-08.jpg" title="<strong>应用实例</strong> | 1、已知完整发明人姓名YOUNG-BAE SOHN ，应键入：YOUNG BAE SOHN  | 2、已知发明（申请）人的一半姓名，应键入：tho$ | 3、已知发明（申请）人姓名包含thomas和yoshiki，应键入：thomas*yoshiki | 4、已知发明（申请）人姓名包含thomas或yoshiki，应键入：thomas+yoshiki | 5、已知发明（申请）人姓名包含thomas，不包含yoshiki，应键入：thomas - yoshiki | 6、已知发明（申请）人姓名包含thomas，不包含yoshiki和kathrine，应键入：thomas-（yoshiki*kathrine） | 注：1、使用前方一直符号“$”时，可能会获得其他不相干结果；例如：tho$包含thomas及thomason等英文单词。" /></dd>
                <dt id="dtTbIC">分类号(IC):</dt>
                <dd>
                    <asp:TextBox class="searchinput" ID="Txt9" runat="server" lang="Ic" onblur="validateBib(this)"
                        title="<p><strong>IPC 分类</strong><br/>例:A01B 或 A47J27/66</p>" ClientIDMode="Static"></asp:TextBox>
                    <img src="../images/img/home-08.jpg" title=" <strong>应用实例</strong> | 1、已知完整IPC号是A47J 27/66，应键入：A47J27/66 | 2、已知IPC号前五位是A47J 2，应键入：A47J2 | 3、已知IPC号包含A47J或A01B，应键入：A47J+A01B " /></dd>
                <dt id="dtTbMC">主分类号(MC):</dt>
                <dd>
                    <asp:TextBox class="searchinput" ID="Txt13" runat="server" lang="Mc" onblur="validateBib(this)"
                        title="<p><strong>主分类号</strong><br/>例:A01B 或 A47J27/66</p>" ClientIDMode="Static"></asp:TextBox>
                    <img src="../images/img/home-08.jpg" title=" <strong>应用实例</strong>| 1、已知完整IPC号是A47J 27/66，应键入：A47J27/66 | 2、已知IPC号前五位是A47J 2，应键入：A47J2 | 3、已知IPC号包含A47J或A01B，应键入：A47J+A01B " /></dd>
                <dt id="dtTbPR">优先权号(PR):</dt>
                <dd>
                    <asp:TextBox class="searchinput" ID="Txt10" runat="server" lang="Pr" onblur="validateBib(this)"
                        title="<p><strong>优先权号</strong><br/>例:CN20020149</p>" ClientIDMode="Static"></asp:TextBox>
                    <img src="../images/img/home-08.jpg" title="<strong>应用实例</strong> | 1、已知完整优先权号，应键入：EP86113792 | 2、已知优先权号前五位，应键入：EP861 | 3、已知优先权号包含EP86113792或EP200800988，应键入：EP86113792+ EP200800988 | 注：1、优先权号检索必须输入国别代码 " /></dd>
                <dt id="dtTbCT">引用文献(CT):</dt>
                <dd>
                    <asp:TextBox class="searchinput" ID="Txt11" runat="server" lang="Ct" onblur="validateBib(this)"
                        title="<p><strong>引用文献</strong><br/>例:CN20020149</p>" ClientIDMode="Static"></asp:TextBox>
                    <img src="../images/img/home-08.jpg" title="<strong>应用实例</strong> | 1、已知完整文献号，应键入：US4925053A | 2、已知文献号前五位，应键入：US492 | 3、已知文献号包含US4925053A或EP200800988，应键入：US4925053A+ EP200800988 " /></dd>
                <dt id="dtTbEC">欧洲分类(EC):</dt>
                <dd>
                    <asp:TextBox class="searchinput" ID="Txt12" runat="server" lang="Ec" onblur="validateBib(this)"
                        title="<p><strong>欧洲分类</strong><br/>例:B60R21</p>" ClientIDMode="Static"></asp:TextBox>
                    <img src="../images/img/home-08.jpg" title="<strong>应用实例</strong> | 1、已知完整ECLA号是A47J 27，应键入：A47J27 | 2、已知ECLA号前五位是A47J 2，应键入：A47J02 | 3、已知ECLA号包含A47J或A01B，应键入：A47J+A01B " /></dd>
            </dl>
            <div class="home2_tat2">
                <a href="javascript:;" onclick="docdbClearSearch()">
                    <img id="Img3" alt="清空检索式" src="../NewImg/bt5.jpg" style="cursor: hand;" title="生成检索式" /></a>
                <a href="javascript:;" onclick="docdbGenerateSearch()">
                    <img id="Img2" alt="生成检索式" src="../NewImg/bt4.jpg" style="cursor: hand;" title="生成检索式" /></a>
                <a href="javascript:;" id="BtnSearch" onclick="DoTableSearchNew('WD','0')">
                    <img src="../NewImg/bt6.jpg" title="专利检索" /></a></div>
        </div>
        <!--table-->
        <div class="home2_down div_xiwl">
            <div>
                命令行检索 &nbsp;<font style="color: Red">[示例:compute/TI+A01B/IC 或 (compute/TI+A01B/IC)@CO=US,CN]</font></div>
            <table>
                <tr>
                    <td>
                        <textarea name="" lang="MD" cols="" id="TxtSearch" rows="" class="home2_input" title="示例:compute/TI+A01B/IC"></textarea>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td>
                        <a href="javascript:;">
                            <img id="imgBtnSearch2" alt="检索" src="../images/btnQuery.png" style="cursor: hand;
                                height: 87px;" title="命令行检索" onclick="DoTableSearchNew('WD','1')" /></a>
                    </td>
                </tr>
            </table>
            <div class="home3_d1">
                <li><a href="javascript:;" value="*" name="*" onclick="addcommand(this)">
                    <img src="../images/img/home-09.jpg" title="应用示例：<br/>  A*B:同时包含A和B" /></a></li>
                <li><a href="javascript:;" value="+" name="+" onclick="addcommand(this)">
                    <img src="../images/img/home-10.jpg" title="应用示例：<br/> A+B:包含A或者B" /></a></li>
                <li><a href="javascript:;" value="-" name="-" onclick="addcommand(this)">
                    <img src="../images/img/home-11.jpg" title="应用示例：<br/>  A-B:包含A且不包含B" /></a></li>
                <li><a href="javascript:;" value="(" name="(" onclick="addcommand(this)">
                    <img src="../images/img/home-12.jpg" title="应用示例：<br/>  （）:括号内的内容优先计算" /></a></li>
                <li><a href="javascript:;" value=")" name=")" onclick="addcommand(this)">
                    <img src="../images/img/home-13.jpg" title="应用示例：<br/> （）:括号内的内容优先计算" /></a></li>
                <li><a href="javascript:;" value="$" name="$" onclick="addcommand(this)">
                    <img src="../images/img/home-14.jpg" title="应用示例：<br/>  A$:所有前缀包含A的单词" /></a></li>
                <li><a href="javascript:;">
                    <img value="ADJ" name="<ADJn>" onclick="addcommand(this)" src="../images/img/home-15.jpg"
                        title="应用示例：<br/> A&nbsp;ADJn&nbsp;B:A和B之间有0-n个词，且A和B前后顺序不能变化；<br/>n属于(0-9)" /></a></li>
                <li><a href="javascript:;">
                    <img value="NEAR" name="<NEARn>" onclick="addcommand(this)" src="../images/img/home-16.jpg"
                        title="应用示例：<br/>  A&nbsp;NEARn&nbsp;B:A和B之间有0-n个词，且A和B前后顺序可以变化；<br/>n属于(0-9)" /></a></li>
                <li><a href="javascript:;">
                    <img value="SKIP" name="<SKIPn>" onclick="addcommand(this)" src="../images/img/home-17.jpg"
                        title="应用示例：<br/>  ASKIPn&nbsp;B:A和B之间只能有n个词，词序不能变化；<br/>n属于(0-9)" /></a></li>
            </div>
            <div class="home3_d2">
                <li></li>
                <li style="padding-top: 6px; padding-left: 6px;">
                    <%--<a href="javascript:;" onclick="docdbClearSearch()">
                    清空检索式</a>--%></li>
            </div>
            <div id="ResultBlock" style="display: none; font-size: 20px; text-align: left;">
                <hr />
                <span id="LabResult" style="height: 30px">命中5篇专利。<a href="cn/PatentGeneralList.aspx?No=001">查看</a></span>
                <span id="LabValidationResult"></span>
                <select id="SlctLogicSymbol" size="1" style="text-align: left; height: 25px; font-size: medium"
                    name="D1">
                    <option value="-">-(非)</option>
                    <option selected="selected" value="*">*(与)</option>
                    <option value="+">+(或)</option>
                </select>
            </div>
        </div>
    </div>
    <div id="DivAddNode" style="width: 600px; height: 230px; display: none;" class="ke-dialog">
        <div class="ke-dialog-content">
            <div class="ke-dialog-header">
                <b><span id="dialogtitle">配置表格项</span></b><span class="ke-dialog-icon-close" title="关闭"
                    onclick="easyDialog.close();"></span>
            </div>
            <div id="divSetTable1" style="width: 600px;">
                <div class="ke-dialog-body">
                    <div style="margin: 10px;">
                        <div id="divSetTable" style="z-index: 1000;">
                            <div class="right">
                                <input type="checkbox" id="checkBoxSelectAllSetTable" onclick="selectAllSetTable('en')" />全选/清空
                            </div>
                            <ul id="ulEn">
                                <li>
                                    <input type="checkbox" name="checkBoxSetEn" value="TableEnTI" />发明名称（TI）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetEn" value="TableEnAB" />摘要（AB）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetEn" value="TableEnAN" />申请号（AN）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetEn" value="TableEnAD" />申请日（AD）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetEn" value="TableEnPN" />公开/公告号（PN）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetEn" value="TableEnPD" />公开/公告日（PD）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetEn" value="TableEnPA" />申请人（PA）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetEn" value="TableEnIN" />发明人（IN）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetEn" value="TableEnIC" />分类号（IC）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetEn" value="TableEnMC" />主分类号（MC）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetEn" value="TableEnPR" />优先权号（PR）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetEn" value="TableEnCT" />引用文献（CT）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetEn" value="TableEnEC" />欧洲分类（EC）</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="ke-dialog-footer">
                    <span id="spanLoading" style="display: none;">
                        <img src="http://localhost:47902/Images/loading2.gif" style="height: 20px; width: 20px" />正在应用设置...</span>
                    <span id="spanErr" style="color: Red; display: none;">应用设置失败!</span> <span class="ke-button-common ke-button-outer ke-dialog-yes"
                        title="应用">
                        <input id="addnode" class="ke-button-common ke-button" type="button" onclick="return ApplaySetTable('en');"
                            value="应用" />
                    </span><span class="ke-button-common ke-button-outer ke-dialog-no" title="取消">
                        <input class="ke-button-common ke-button" type="button" onclick="CloseSetTab();"
                            value="取消" /></span>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfSelEntrances" runat="server" ClientIDMode="Static" />
    <script type="text/javascript">
        $(document).ready(function () {
            InitPageData('en');
        });  
            
    </script>
</asp:Content>
