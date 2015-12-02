<%@ Page Title="" Language="C#" MasterPageFile="~/Master/index.master" AutoEventWireup="true"
    CodeBehind="frmCnTbSearch.aspx.cs" Inherits="Patentquery.My.frmCnTbSearch" ValidateRequest="false" %>

<asp:Content ID="hed" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/B_SeachPage.css" rel="stylesheet" type="text/css" />
    <link href="../Css/B_cprs2010.css" rel="stylesheet" type="text/css" />
    <link href="../jquery-easyui-1.8.0/plugins/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
    <link href="../css/themes/default/default.css" rel="stylesheet" type="text/css" />
    <script src="../Js/cnTableSearch.js" type="text/javascript"></script>
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
    <script src="../Js/autocomplete.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="mytips" style="border-right: gray 1px solid; border-top: gray 1px solid;
        z-index: 2; display: none; left: 0px; border-left: gray 1px solid; border-bottom: gray 1px solid;
        position: absolute; text-align: center; font-size: 15px; background-color: #ffffff;
        vertical-align: middle;">
    </div>
    <div class="div_Content_xiwl home2_con">
        <div class="home_sel">
            <div class="home_sel1" style="width: 400px;">
                <label>
                    <input type="radio" name="RadioGroup1" value="中国专利" id="Radio1" checked="checked" />
                    中国专利</label>
                <label>
                    <input type="radio" name="RadioGroup1" value="世界专利" id="RadioGroup1_1" onclick="javascript:window.location='frmWdTbSearch.aspx'" />
                    世界专利</label>
                <label id="labSetTable" runat="server">
                    <a href="javascript:;" onclick="OpenSetTable();" style="cursor: hand; color: Black;">
                        &nbsp;&nbsp;&nbsp;&nbsp;配置表格项</a></label>
            </div>
        </div>
        <div id="divQueryTable" class="home2_table div_xiwl" style="overflow: auto;">
            <div class="home3_set" style="width: 98%; padding: 10px 0px 15px 0px;">
                <ul style="float: left; padding-left: 40px;">
                    <li style="font-weight: bold;">
                        <label for="rdAll">
                            <input id="rdAll" type="radio" checked="checked" name="LegDbType" title="全库" value="" />全库：</label></li>
                    <li>
                        <label for="rdYX">
                            <input id="rdYX" type="radio" name="LegDbType" title="有效库" value="@YX" />有效库
                        </label>
                    </li>
                    <li>
                        <label for="rdSX">
                            <input id="rdSX" type="radio" name="LegDbType" title="失效库" value="@SX" />失效库</label></li></ul>
                <ul style="float: right">
                    <li style="font-weight: bold;">
                        <label>
                            <input type="checkbox" checked="checked" id="chkAll" onclick="Select();" />全选：</label></li>
                    <li>
                        <label>
                            <input name="checkbox" checked="checked" type="checkbox" value="DI" />发明公开【DI】</label></li>
                    <li>
                        <label>
                            <input name="checkbox" checked="checked" type="checkbox" value="UM" />实用新型【UM】</label></li>
                    <li>
                        <label>
                            <input name="checkbox" checked="checked" type="checkbox" value="DP" />外观设计【DP】</label></li>
                    <li>
                        <label>
                            <input name="checkbox" type="checkbox" value="AI" />发明授权【AI】</label></li>
                </ul>
            </div>
            <div>
                <dl>
                    <dt id="dtTbTI">发明名称(TI):</dt>
                    <dd>
                        <asp:TextBox ID="Txb1" runat="server" lang="Ti" onblur="validateBib(this)" title="<p><strong>发明名称</strong><br/>   例:自行车</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:发明名称" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;1、已知内容包含计算机，应键入：计算机<br/>   &nbsp;&nbsp;2、已知内容包含计算机和系统，应键入：计算机*系统 <br/>   &nbsp;&nbsp;3、已知内容包含计算机或控制板，应键入：计算机+控制板<br/>   &nbsp;&nbsp;4、已知内容包含计算机，不包含键盘，应键入：计算机-键盘<br/>   &nbsp;&nbsp;5、已知内容包含计算机，不包含应用和系统，应键入：计算机-（应用*系统）" /></dd>
                    <dt id="dtTbAB">摘要(AB):</dt>
                    <dd>
                        <asp:TextBox ID="Txb2" runat="server" lang="Ab" onblur="validateBib(this)" title="<p><strong>文摘</strong><br/>   例:外喷放热气</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:摘要" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;1、已知内容包含计算机，应键入：计算机<br/>   &nbsp;&nbsp;2、已知内容包含计算机和系统，应键入：计算机*系统<br/>   &nbsp;&nbsp;3、已知内容包含计算机或控制板，应键入：计算机+控制板<br/>   &nbsp;&nbsp;4、已知内容包含计算机，不包含键盘，应键入：计算机-键盘<br/>   &nbsp;&nbsp;5、已知内容包含计算机，不包含应用和系统，应键入：计算机-（应用*系统）" /></dd>
                    <dt id="dtTbCL">主权利要求(CL):</dt>
                    <dd>
                        <asp:TextBox ID="Txb3" runat="server" lang="Cl" onblur="validateBib(this)" title="<p><strong>主权利要求</strong><br/>   例:加煤系统</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:主权利要求" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;1、已知内容包含计算机，应键入：计算机<br/>   &nbsp;&nbsp;2、已知内容包含计算机和系统，应键入：计算机*系统<br/>   &nbsp;&nbsp;3、已知内容包含计算机或控制板，应键入：计算机+控制板<br/>   &nbsp;&nbsp;4、已知内容包含计算机，不包含键盘，应键入：计算机-键盘<br/>   &nbsp;&nbsp;5、已知内容包含计算机，不包含应用和系统，应键入：计算机-（应用*系统）" /></dd>
                    <dt id="dtTbTX">关键词(TX):</dt>
                    <dd>
                        <asp:TextBox ID="Txb4" runat="server" lang="Tx" onblur="validateBib(this)" title="<p><strong>关键词</strong><br/>   例:计算机</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:关键词" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;1、已知内容包含计算机，应键入：计算机 <br/>   &nbsp;&nbsp;2、已知内容包含计算机和系统，应键入：计算机*系统<br/>   &nbsp;&nbsp;3、已知内容包含计算机或控制板，应键入：计算机+控制板<br/>   &nbsp;&nbsp;4、已知内容包含计算机，不包含键盘，应键入：计算机-键盘<br/>   &nbsp;&nbsp;5、已知内容包含计算机，不包含应用和系统，应键入：计算机-（应用*系统）" /></dd>
                    <dt id="dtTbPA">申请人(PA):</dt>
                    <dd>
                        <asp:TextBox ID="Txb5" runat="server" lang="Pa" onblur="validateBib(this)" title="<p><strong>申请人</strong><br/>   例:张三</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:申请人" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;1、已知完整发明（申请）人姓名，应键入：袁隆平 <br/>   &nbsp;&nbsp;2、已知发明（申请）人的一半姓名，应键入：袁隆 <br/>   &nbsp;&nbsp;3、已知发明（申请）人姓名包含袁隆平和王光烈，应键入：袁隆平*王光烈<br/>   &nbsp;&nbsp;4、已知发明（申请）人姓名包含袁隆平或王光烈，应键入：袁隆平+王光烈<br/>   &nbsp;&nbsp;5、已知发明（申请）人姓名包含袁隆平，不包含王光烈，应键入：袁隆平-王光烈<br/>   6、已知发明（申请）人姓名包含袁隆平，不包含王光烈和赵旭日，应键入：袁隆平-（王光烈*赵旭日）" /></dd>
                    <dt id="dtTbIC">分类号(IC):</dt>
                    <dd>
                        <asp:TextBox ID="Txb6" runat="server" lang="Ic" onblur="validateBib(this)" title="<p><strong>IPC 分类</strong><br/>   例:A01B</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:分类号" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;1、已知完整IPC号是A47J 27/66，应键入：A47J02766</font><br/>   &nbsp;&nbsp;2、已知IPC号前五位是A47J 2，应键入：A47J02<br/>    &nbsp;&nbsp;3、已知IPC号包含A47J或A01B，应键入：A47J+ A01B" /></dd>
                    <dt id="dt1">权利人(PO):</dt>
                    <dd>
                        <asp:TextBox ID="TxbPo" runat="server" lang="Po" onblur="validateBib(this)" title="<p><strong>权利人</strong><br/>   例:张三</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:申请人" style="cursor: pointer;" title="" /></dd>
                    <dt id="dtTbAN">申请号(AN):</dt>
                    <dd>
                        <asp:TextBox ID="Txb7" runat="server" lang="An" onblur="validateBib(this)" title="<p><strong>申请号</strong><br/>   例:200820028064</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:申请号" style="cursor: pointer;" title=" <strong>应用示例</strong></br>申请号检索输入4—12位(不带校验位),早期的8位申请号输入2—8位(不带校验位),如需输入校验位,其格式为[.?或?]。<br/>   申请号为8位的专利，系统会自动转换为12位显示。例如：85107482，显示检索结果198510007482。<br/>     &nbsp;&nbsp;1、已知完整申请号，应键入：200820028064<br/>   &nbsp;&nbsp;2、已知申请号前五位，应键入：20082 <br/>    &nbsp;&nbsp;3、已知申请号包含200820028064或200890100326，应键入：200820028064+200890100326 " /></dd>
                    <dt id="dtTbAD">申请日(AD):</dt>
                    <dd>
                        <asp:TextBox ID="Txb8" runat="server" lang="Ad" onblur="validateBib(this)" title="<p><strong>申请日</strong><br/>   例:20030102</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:申请日" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;1、已知完整日期，应键入：19990205 <br/>   &nbsp;&nbsp;2、已知月份，应键入：199902<br/>    &nbsp;&nbsp;3、已知年份，应键入：1999<br/>    &nbsp;&nbsp;4、已知时间的连续范围，应键入：20060202>20090101<br/>   &nbsp;&nbsp;5、已知时间范围包含2008年或2009年，应键入：2008+2009" /></dd>
                    <dt id="dtTbPN">公开号(PN):</dt>
                    <dd>
                        <asp:TextBox ID="Txb9" runat="server" lang="Pn" onblur="validateBib(this)" title="<p><strong>公开号</strong><br/>   例:1240461</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:公开号" style="cursor: pointer;" title=" <strong>应用示例</strong></br>公开公告号检索必须输入2—9位。<br/>    1、已知完整公开（公告）号，应键入：101969536<br/>   &nbsp;&nbsp;2、已知公开（公告）号前五位，应键入：10196<br/>    &nbsp;&nbsp;3、已知公开（公告）号包含101969536或202139867U，应键入：101969536+202139867U" /></dd>
                    <dt id="dtTbPD">公开日(PD):</dt>
                    <dd>
                        <asp:TextBox ID="Txb10" runat="server" lang="Pd" onblur="validateBib(this)" title="<p><strong>公开日</strong><br/>   例:20030102</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:公开日" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;1、已知完整日期，应键入：19990205 <br/>   &nbsp;&nbsp;2、已知月份，应键入：199902<br/>    &nbsp;&nbsp;3、已知年份，应键入：1999<br/>    &nbsp;&nbsp;4、已知时间的连续范围，应键入：应键入：20060202>20090101<br/>   &nbsp;&nbsp;5、已知时间范围包含2008年或2009年，应键入：2008+2009" /></dd>
                    <dt id="dtTbGN">公告号(GN):</dt>
                    <dd>
                        <asp:TextBox ID="Txb11" runat="server" lang="Gn" onblur="validateBib(this)" title="<p><strong>公告号</strong><br/>   例:1088075</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:公告号" style="cursor: pointer;" title=" <strong>应用示例</strong></br>公开公告号检索必须输入2—9位。<br/>   1、已知完整公开（公告）号，应键入：101969536<br/>   &nbsp;&nbsp;2、已知公开（公告）号前五位，应键入：10196<br/>    &nbsp;&nbsp;3、已知公开（公告）号包含101969536或202139867U，应键入：101969536+202139867U" /></dd>
                    <dt id="dtTbGD">公告日(GD):</dt>
                    <dd>
                        <asp:TextBox ID="Txb12" runat="server" lang="Gd" onblur="validateBib(this)" title="<p><strong>公告日</strong><br/>   例:20030102</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:公告日" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;1、已知完整日期，应键入：19990205 <br/>   &nbsp;&nbsp;2、已知月份，应键入：199902 <br/>    &nbsp;&nbsp;3、已知年份，应键入：1999 <br/>    &nbsp;&nbsp;4、已知时间的连续范围，应键入：应键入：20060202>20090101 <br/>   &nbsp;&nbsp;5、已知时间范围包含2008年或2009年，应键入：2008+2009" /></dd>
                    <dt id="dtTbPR">优先权号(PR):</dt>
                    <dd>
                        <asp:TextBox ID="Txb13" runat="server" lang="Pr" onblur="validateBib(this)" title="<p><strong>优先权号</strong><br/>   例:CN20011342625   </p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:优先权号" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;1、已知完整优先权号，应键入：EP86113792 | &nbsp;&nbsp;2、已知优先权号前五位，应键入：EP861 <br/>   &nbsp;&nbsp;3、已知优先权号包含EP86113792或EP200800988，应键入：EP86113792+ EP200800988" /></dd>
                    <dt id="dtTbIN">发明人(IN):</dt>
                    <dd>
                        <asp:TextBox ID="Txb14" runat="server" lang="In" onblur="validateBib(this)" title="<p><strong>发明人</strong><br/>   例:张三</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:发明人" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;1、已知完整发明（申请）人姓名，应键入：袁隆平 | 2、已知发明（申请）人的一半姓名，应键入：袁隆 | 3、已知发明（申请）人姓名包含袁隆平和王光烈，应键入：袁隆平*王光烈 | 4、已知发明（申请）人姓名包含袁隆平或王光烈，应键入：袁隆平+王光烈 | 5、已知发明（申请）人姓名包含袁隆平，不包含王光烈，应键入：袁隆平-王光烈 | 6、已知发明（申请）人姓名包含袁隆平，不包含王光烈和赵旭日，应键入：袁隆平-（王光烈*赵旭日）" /></dd>
                    <dt id="dtTbCT">范畴分类(CT):</dt>
                    <dd>
                        <asp:TextBox ID="Txb15" runat="server" lang="Ct" onblur="validateBib(this)" title="<p><strong>范畴分类</strong><br/>   例:21B</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:范畴分类" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;已知范畴分类号是23F，应键入：23F" /></dd>
                    <dt id="dtTbDZ">申请人地址(DZ):</dt>
                    <dd>
                        <asp:TextBox ID="Txb16" runat="server" lang="Dz" onblur="validateBib(this)" title="<p><strong>申请人地址</strong><br/>   例:北京</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:发明名称" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;1、已知完整申请人地址，应键入：北京市海淀区中关村 | 2、已知申请人的一半地址，应键入：北京市海淀区 | 3、已知申请人地址包含北京市或上海市，应键入：北京市+上海市" /></dd>
                    <dt id="dtTbCO">国省代码(CO):</dt>
                    <dd>
                        <input type="text" id="Txb17" name1="pattern" lang="Co" title="<p><strong>国省代码</strong><br/>   例:95或AD</p>"
                            runat="server" clientidmode="Static" />
                        <img src="../images/img/home-08.jpg" alt="提示:国省代码" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;已知专利所在国省为中国北京，应键入：11" /></dd>
                    <dt id="dtTbAG">代理机构(AG):</dt>
                    <dd>
                        <input type="text" id="Txb18" name1="pattern" lang="Ag" title="<p><strong>代理机构</strong><br/>   例:31244</p>"
                            runat="server" clientidmode="Static" />
                        <img src="../images/img/home-08.jpg" alt="提示:代理机构" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;已知专利的代理机构代码为11296，应键入：11296" /></dd>
                    <dt id="dtTbMC">主分类号(MC):</dt>
                    <dd>
                        <input type="text" id="Txb19" name1="pattern" lang="MC" title="<p><strong>主分类号</strong><br/>   例:A01B</p>"
                            runat="server" clientidmode="Static" />
                        <img src="../images/img/home-08.jpg" alt="提示:主分类号" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;1、已知完整IPC号是A47J 27/66，应键入：A47J02766</font><br/>   &nbsp;&nbsp;2、已知IPC号前五位是A47J 2，应键入：A47J02<br/>    &nbsp;&nbsp;3、已知IPC号包含A47J或A01B，应键入：A47J+ A01B" /></dd>
                    <dt id="dtTbAT">代理人(AT):</dt>
                    <dd>
                        <input type="text" id="Txb20" name1="pattern" lang="AT" title="<p><strong>代理人</strong><br/>   例:张三</p>"
                            runat="server" clientidmode="Static" />
                        <img src="../images/img/home-08.jpg" alt="提示:代理人" style="cursor: pointer;" title="<strong>应用示例</strong><br/>     &nbsp;&nbsp;1、已知完整发明（申请）人姓名，应键入：袁隆平 | 2、已知发明（申请）人的一半姓名，应键入：袁隆 | 3、已知发明（申请）人姓名包含袁隆平和王光烈，应键入：袁隆平*王光烈 | 4、已知发明（申请）人姓名包含袁隆平或王光烈，应键入：袁隆平+王光烈 | 5、已知发明（申请）人姓名包含袁隆平，不包含王光烈，应键入：袁隆平-王光烈 | 6、已知发明（申请）人姓名包含袁隆平，不包含王光烈和赵旭日，应键入：袁隆平-（王光烈*赵旭日）" /></dd>
                    <dt id="dtTbCS">权利要求(CS):</dt>
                    <dd>
                        <input type="text" id="Txb21" name1="pattern" lang="CS" title="" runat="server" clientidmode="Static" />
                        <img src="../images/img/home-08.jpg" alt="提示:权利要求" style="cursor: pointer;" title="" /></dd>
                    <dt id="dtTbDS">说明书(DS):</dt>
                    <dd>
                        <input type="text" id="Txb22" name1="pattern" lang="DS" title="" runat="server" clientidmode="Static" />
                        <img src="../images/img/home-08.jpg" alt="提示:说明书" style="cursor: pointer;" title="" /></dd>
                    <dt id="dt2">审查员引用文献(CC):</dt>
                    <dd>
                        <asp:TextBox ID="TxbCC" runat="server" lang="Cc" onblur="validateBib(this)" title="<p><strong>引用文献</strong><br/>  例:CN200312</p>"
                            ClientIDMode="Static"></asp:TextBox>
                        <img src="../images/img/home-08.jpg" alt="提示:申请人" style="cursor: pointer;" title="" /></dd>
                </dl>
            </div>
            <div class="home2_tat2">
                <a href="javascript:;" onclick="cnClearSearch()">
                    <img id="Img3" alt="清空检索式" src="../NewImg/bt5.jpg" style="cursor: hand;" title="清空检索式" /></a>
                <a href="javascript:;" onclick="cnGenerateSearch();" style="cursor: hand;">
                    <%--生成检索式--%><img id="Img2" alt="生成检索式" src="../NewImg/bt4.jpg" style="cursor: hand;"
                        title="生成检索式" /></a> <a href="javascript:;">
                            <img id="BtnSearch" alt="检索" src="../NewImg/bt6.jpg" style="cursor: hand;" title="专利检索"
                                onclick="DoTableSearchNew('Cn','0')" /></a></div>
        </div>
        <!--table-->
        <div class="home2_down div_xiwl">
            <div>
                命令行检索 &nbsp;<font style="color: Red">[示例:计算机/TI+A01B/IC 或 (计算机/TI+A01B/IC)@LX=DI,UM,@YX]</font></div>
            <table>
                <tr>
                    <td>
                        <textarea lang="MD" name="" cols="" id="TxtSearch" rows="" class="home2_input"></textarea>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td>
                        <a href="javascript:;">
                            <img id="imgBtnSearch2" alt="检索" src="../images/btnQuery.png" style="cursor: hand;
                                height: 87px;" title="命令行检索" onclick="DoTableSearchNew('Cn','1')" /></a>
                    </td>
                </tr>
            </table>
            <div class="home2_d1">
                <li><a href="javascript:;" value="*" name="*" onclick="addcommand(this)">
                    <img src="../images/img/home-09.jpg" title="<strong>应用示例</strong><br/>  A*B:同时包含A和B" /></a></li>
                <li><a href="javascript:;" value="+" name="+" onclick="addcommand(this)">
                    <img src="../images/img/home-10.jpg" title="<strong>应用示例</strong><br/> A+B:包含A或者B" /></a></li>
                <li><a href="javascript:;" value="-" name="-" onclick="addcommand(this)">
                    <img src="../images/img/home-11.jpg" title="<strong>应用示例</strong><br/>  A-B:包含A且不包含B" /></a></li>
                <li><a href="javascript:;" value="(" name="(" onclick="addcommand(this)">
                    <img src="../images/img/home-12.jpg" title="<strong>应用示例</strong><br/>  （）:括号内的内容优先计算" /></a></li>
                <li><a href="javascript:;" value=")" name=")" onclick="addcommand(this)">
                    <img src="../images/img/home-13.jpg" title="<strong>应用示例</strong><br/> （）:括号内的内容优先计算" /></a></li>
            </div>
            <div class="home2_d2">
                <li></li>
                <li style="padding-top: 6px; padding-left: 3px;"></li>
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
                                <input type="checkbox" id="checkBoxSelectAllSetTable" onclick="selectAllSetTable('cn')" />全选/清空
                            </div>
                            <ul id="ulCn">
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnTI" />发明名称（TI）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnAB" />摘要（AB）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnCL" />主权利要求（CL）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnTX" />关键词（TX）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnPA" />申请人（PA）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnIC" />分类号（IC）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnAN" />申请号（AN）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnPO" />权利人（PO）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnAD" />申请日（AD）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnPN" />公开号（PN）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnPD" />公开日（PD）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnGN" />公告号（GN）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnGD" />公告日（GD）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnPR" />优先权号（PR）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnIN" />发明人（IN）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnCT" />范畴分类（CT）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnDZ" />申请人地址（DZ）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnCO" />国省代码（CO）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnAG" />代理机构（AG）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnAT" />代理人（AT）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnMC" />主分类号（MC）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnCS" />权利要求（CS）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnDS" />说明书（DS）</li>
                                <li>
                                    <input type="checkbox" name="checkBoxSetCn" value="TableCnCC" />审查员引用文献（CC）</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="ke-dialog-footer">
                    <span id="spanLoading" style="display: none;">
                        <img src="http://localhost:47902/Images/loading2.gif" style="height: 20px; width: 20px" />正在应用设置...</span>
                    <span id="spanErr" style="color: Red; display: none;">应用设置失败!</span> <span class="ke-button-common ke-button-outer ke-dialog-yes"
                        title="应用">
                        <input id="addnode" class="ke-button-common ke-button" type="button" onclick="return ApplaySetTable('cn');"
                            value="应用" />
                    </span><span class="ke-button-common ke-button-outer ke-dialog-no" title="取消">
                        <input class="ke-button-common ke-button" type="button" onclick="CloseSetTab();"
                            value="取消" /></span>
                </div>
            </div>
        </div>
    </div>
    <%--隐藏变量，存储代理人提示信息--%>
    <asp:HiddenField ID="hfValue" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfValueCountryCode" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfSelEntrances" runat="server" ClientIDMode="Static" />
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            InitPageData('cn');
        });

    </script>
</asp:Content>
