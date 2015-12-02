<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Index.master" AutoEventWireup="true"
    CodeBehind="frmTreeQuery.aspx.cs" Inherits="Patentquery.My.frmTreeQuery" %>

<asp:Content ID="headxy" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <link href="../css/themes/default/default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/TreeQuery.js"></script>
    <script src="../Js/Trans.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/plugins/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../jquery-easyui-1.8.0/plugins/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- IPC 检索-->
    <div id="left" style="width: 225px;">
        <div id="pinlieft">
            <div id="left_title" class="left_ti">
                &nbsp;
                <select id="selectTree" onchange="ShowTree()">
                    <option value="IPC" selected="selected">IPC分类</option>
                    <option value="ADM">外观设计分类</option>
                    <option value="ARE">国民经济分类</option>
                </select>&nbsp;
            </div>
            <div class="left_content2" style="min-height: 580px;">
                <div id="divIPC" style="min-height: 580px">
                    <ul id="IPC" class="easyui-tree" data-options="lines:true" />
                </div>
                <div id="divADM" style="min-height: 580px; display: none;">
                    <ul id="ADM" class="easyui-tree" data-options="lines:true" />
                </div>
                <div id="divARE" style="min-height: 580px; display: none">
                    <ul id="ARE" class="easyui-tree" data-options="lines:true" />
                </div>
            </div>
        </div>
    </div>
    <div id="right" style="min-height: 652px; width: 768px;">
        <div id="showlist">
            <div class="spbutton" id="divSSC">
                <ul>
                    <li>
                        <div class="commandLeft" id="ZLXMECJSGL">
                            <span class="btn" onclick="ShowTabSearch('重新检索')">重新检索</span> <span class="btn" onclick="ShowTabSearch('过滤检索')">
                                过滤检索</span> <span class="btn" onclick="ShowTabSearch('二次检索')">二次检索</span>
                        </div>
                        <div class="commandRight" style="margin-left: 10px;">
                            <span class="btn" onclick="showsp()">显示检索式</span>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="page">
                <div class="pageleft">
                    <span class="wentu"><a href="javascript:void(0);" onclick="return ShowPL('0');">文图</a></span>
                    <span class="liebiao"><a href="javascript:void(0);" onclick="return ShowPL('1');">列表</a></span>
                </div>
                <div id="pagetop" class="easyui-pagination" style="width: 650px; float: left" data-options="total:0,showRefresh: false,displayMsg: '从 [{from}] 到 [{to}] 共[{total}]条记录'">
                </div>
            </div>
            <div class="fun" style="border-top-width: 0px;">
                <input type="checkbox" onclick="SelectAll(this)" />全选&nbsp;&nbsp; <a href="javascript:void(0);"
                    onclick="showAddCO()">
                    <img src='/Images/bigshoucang_bj.png' alt='收藏' />收藏</a>&nbsp; <a id="PLXZ" href="javascript:void(0);"
                        onclick="showExportCFG()">
                        <img src='/Images/bigdaochu_bj.png' alt='导出' />导出</a>&nbsp; <a href="javascript:void(0);"
                            onclick="GOTOST();">
                            <img src='/Images/bigfenxi_bj.png' alt='分析' />分析</a>&nbsp; <a id="a_adddata" href="javascript:void(0);"
                                onclick="ShowAddToZT()">
                                <img src="/Images/bigjiaru_bj.png" alt='加入专题库'></img><span class="spaddtozt">加入专题库</span></a>
            </div>
            <div id="divlist" class="content">
            </div>
            <div class="page">
                <div class="pageleft">
                    <span class="wentu"><a href="javascript:void(0);" onclick="return ShowPL('0');">文图</a></span>
                    <span class="liebiao"><a href="javascript:void(0);" onclick="return ShowPL('1');">列表</a></span>
                </div>
                <div id="pagebom" class="easyui-pagination" style="width: 650px; float: left" data-options="total:0,showRefresh: false,displayMsg: '从 [{from}] 到 [{to}] 共[{total}]条记录'">
                </div>
            </div>
        </div>
        <div id="divshowhelp" style="display: none">
            <div class="boxInner" style="min-height: 610px; padding: 20px;">
                <ul>
                    <li>1.请点击右边<b>分类导航</b>下边的下拉框选择分类类型。</li>
                    <li>2.选择分类导航的节点，点击节点的<b>中文</b>或<b>英文</b>浏览该分类下的专利，点击<b>节点名称</b>显示技术分类的简介。</li>
                </ul>
            </div>
        </div>
        <div class="box" id="divnodata" style="display: none">
            <div class="boxInner" style="min-height: 610px; padding: 20px;">
                <ul>
                    <li>此分类下您还没有专利！</li>
                </ul>
            </div>
        </div>
        <div id="showspdes" style="display: none">
            <div id="spdes" class="boxInner" style="min-height: 610px; padding: 20px;">
            </div>
        </div>
    </div>
    <!-- 表格检索 开始 -->
    <div id="tabSearch" style="width: 690px; display: none;">
        <span id="DseachTitle" style="display: none">编辑检索式</span>
        <div id="cndivQueryTable" class="ztdivQueryTable" style="overflow: inherit; border-width: 0px;">
            <dl>
                <dt id="dtTableCnTI">发明名称（TI）：</dt>
                <dd id="ddTableCnTI">
                    <input name="TxtCn1" type="text" id="TxtCn1" class="textBox" lang="TI" />
                </dd>
                <dt id="dtTableCnAB">摘要（AB）：</dt>
                <dd id="ddTableCnAB">
                    <input name="TxtCn2" type="text" id="TxtCn2" class="textBox" lang="AB" />
                </dd>
                <dt id="dtTableCnCL">主权利要求（CL）：</dt>
                <dd id="ddTableCnCL">
                    <input name="TxtCn3" type="text" id="TxtCn3" class="textBox" lang="CL" />
                </dd>
                <dt id="dtTableCnTX">关键词（TX）：</dt>
                <dd id="ddTableCnTX">
                    <input name="TxtCn4" type="text" id="TxtCn4" class="textBox" lang="TX" />
                </dd>
                <dt id="dtTableCnPA">申请人（PA）：</dt>
                <dd id="ddTableCnPA">
                    <input name="TxtCn5" type="text" id="TxtCn5" class="textBox" lang="PA" />
                </dd>
                <dt id="dtTableCnIC">分类号（IC）：</dt>
                <dd id="ddTableCnIC">
                    <input name="TxtCn6" type="text" id="TxtCn6" class="textBox" lang="IC" />
                </dd>
                <dt id="dtTableCnAN">申请号（AN）：</dt>
                <dd id="ddTableCnAN">
                    <input name="TxtCn7" type="text" id="TxtCn7" class="textBox" lang="AN" />
                </dd>
                <dt id="dtTableCnAD">申请日（AD）：</dt>
                <dd id="ddTableCnAD">
                    <input name="TxtCn8" type="text" id="TxtCn8" class="textBox" lang="AD" />
                </dd>
                <dt id="dtTableCnPN">公开号（PN）：</dt>
                <dd id="ddTableCnPN">
                    <input name="TxtCn9" type="text" id="TxtCn9" class="textBox" lang="PN" />
                </dd>
                <dt id="dtTableCnPD">公开日（PD）：</dt>
                <dd id="ddTableCnPD">
                    <input name="TxtCn10" type="text" id="TxtCn10" class="textBox" lang="PD" />
                </dd>
                <dt id="dtTableCnGN">公告号（GN）：</dt>
                <dd id="ddTableCnGN">
                    <input name="TxtCn11" type="text" id="TxtCn11" class="textBox" lang="GN" />
                </dd>
                <dt id="dtTableCnGD">公告日（GD）：</dt>
                <dd id="ddTableCnGD">
                    <input name="TxtCn12" type="text" id="TxtCn12" class="textBox" lang="GD" />
                </dd>
                <dt id="dtTableCnPR">优先权号（PR）：</dt>
                <dd id="ddTableCnPR">
                    <input name="TxtCn13" type="text" id="TxtCn13" class="textBox" lang="PR" />
                </dd>
                <dt id="dtTableCnIN">发明人（IN）：</dt>
                <dd id="ddTableCnIN">
                    <input name="TxtCn14" type="text" id="TxtCn14" class="textBox" lang="IN" />
                </dd>
                <dt id="dtTableCnCT">范畴分类（CT）：</dt>
                <dd id="ddTableCnCT">
                    <input name="TxtCn15" type="text" id="TxtCn15" class="textBox" lang="CT" />
                </dd>
                <dt id="dtTableCnDZ">申请人地址（DZ）：</dt>
                <dd id="ddTableCnDZ">
                    <input name="TxtCn16" type="text" id="TxtCn16" class="textBox" lang="DZ" />
                </dd>
                <dt id="dtTableCnCO">国省代码（CO）：</dt>
                <dd id="ddTableCnCO">
                    <input name="TxtCn17" type="text" id="TxtCn17" class="textBox" lang="CO" />
                </dd>
                <dt id="dtTableCnAG">代理机构（AG）：</dt>
                <dd id="ddTableCnAG">
                    <input name="TxtCn18" type="text" id="TxtCn18" class="textBox" lang="AG" />
                </dd>
                <dt id="dtTableCnAT">代理人（AT）：</dt>
                <dd id="ddTableCnAT">
                    <input name="TxtCn19" type="text" id="TxtCn19" class="textBox" lang="AT" />
                </dd>
                <dt id="dtTableCnMC">主分类号（MC）：</dt>
                <dd id="ddTableCnMC">
                    <input name="TxtCn20" type="text" id="TxtCn20" class="textBox" lang="MC" />
                </dd>
            </dl>
        </div>
        <div id="endivQueryTable" class="ztdivQueryTable" style="overflow: inherit; display: none;">
            <dl>
                <dt id="dtTableEnTI">发明名称（TI）：</dt>
                <dd id="ddTableEnTI">
                    <input name="TxtEn1" type="text" id="TxtEn1" class="textBox" lang="TI" />
                </dd>
                <dt id="dtTableEnAB">摘要（AB）：</dt>
                <dd id="ddTableEnAB">
                    <input name="TxtEn2" type="text" id="TxtEn2" class="textBox" lang="AB" />
                </dd>
                <dt id="dtTableEnAN">申请号（AN）：</dt>
                <dd id="ddTableEnAN">
                    <input name="TxtEn3" type="text" id="TxtEn3" class="textBox" lang="AN" />
                </dd>
                <dt id="dtTableEnAD">申请日（AD）：</dt>
                <dd id="ddTableEnAD">
                    <input name="TxtEn4" type="text" id="TxtEn4" class="textBox" lang="AD" />
                </dd>
                <dt id="dtTableEnPN">公开(公告)号（PN）：</dt>
                <dd id="ddTableEnPN">
                    <input name="TxtEn5" type="text" id="TxtEn5" class="textBox" lang="PN" />
                </dd>
                <dt id="dtTableEnPD">公开(公告)日（PD）：</dt>
                <dd id="ddTableEnPD">
                    <input name="TxtEn6" type="text" id="TxtEn6" class="textBox" lang="PD" />
                </dd>
                <dt id="dtTableEnPA">申请人（PA）：</dt>
                <dd id="ddTableEnPA">
                    <input name="TxtEn7" type="text" id="TxtEn7" class="textBox" lang="PA" />
                </dd>
                <dt id="dtTableEnIN">发明人（IN）：</dt>
                <dd id="ddTableEnIN">
                    <input name="TxtEn8" type="text" id="TxtEn8" class="textBox" lang="IN" />
                </dd>
                <dt id="dtTableEnIC">分类号（IC）：</dt>
                <dd id="ddTableEnIC">
                    <input name="TxtEn9" type="text" id="TxtEn9" class="textBox" lang="IC" />
                </dd>
                <dt id="dtTableEnPR">优先权号（PR）：</dt>
                <dd id="ddTableEnPR">
                    <input name="TxtEn10" type="text" id="TxtEn10" class="textBox" lang="PR" />
                </dd>
                <dt id="dtTableEnCT">引用文献（CT）：</dt>
                <dd id="ddTableEnCT">
                    <input name="TxtEn11" type="text" id="TxtEn11" class="textBox" lang="CT" />
                </dd>
                <dt id="dtTableEnEC">欧洲分类（EC）：</dt>
                <dd id="ddTableEnEC">
                    <input name="TxtEn12" type="text" id="TxtEn12" class="textBox" lang="EC" />
                </dd>
                <dt id="dtTableEnMC">主分类号（MC）：</dt>
                <dd id="ddTableEnMC">
                    <input name="TxtEn13" type="text" id="TxtEn13" class="textBox" lang="MC" />
                </dd>
                <dt id="dttmp1">&nbsp;</dt>
                <dd id="dttmp2" style="width: 154px; height: 30px">
                </dd>
            </dl>
        </div>
        <textarea name="TxtSearch" rows="2" cols="20" id="TxtSearch" style="height: 60px;
            width: 470px; display: none;"></textarea>
        <span id="LabValidationResult"></span>
        <select id="SlctLogicSymbol" style="display: none">
            <option value="+">+(或)</option>
            <option value="-">-(非)</option>
            <option selected="selected" value="*">*(与)</option>
        </select>
    </div>
    <!-- 表格检索 结束-->
    <!-- 收藏夹 开始-->
    <div id="AddCO" style="min-width: 300px; display: none;" class="ke-dialog">
        <div style="min-height: 250px">
            <ul id="CO" class="easyui-tree" style="min-width: 300px" data-options="lines:true,checkbox:true,cascadeCheck:false" />
        </div>
        <hr />
        <div style="width: 318px">
            自定义标注：
            <textarea name="TextBoxNote" rows="5" cols="45" style="width: 290px" id="TextBoxNote"></textarea>
        </div>
    </div>
    <div id="DivAddNode" style="min-width: 400px; display: none;">
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
    <!-- 同类专利-->
    <div id="sm" style="width: 520px; height: 300px; display: none;">
        <iframe id="ism" name="ism" scrolling="no" frameborder="0" src="" style="width: 100%;
            height: 100%;"></iframe>
    </div>
    <!-- 同类专利-->
    <div id="cart">
        <div class="header">
            专利对比</div>
        <div id="cartItems" class="body">
        </div>
        <div class="body center">
            <span class="btnSmall" onclick="clearCart()">清空</span> <span class="btnSmall" onclick="closeCart()">
                关闭</span> <span class="btnSmall" onclick="submitCart()">确定</span>
        </div>
        <div class="bottom">
        </div>
    </div>
    <!-- 添加到专题库 开始-->
    <div id="AddToZT" style="min-width: 230px; display: none;">
        <div style="min-height: 200px; min-width: 230px">
            <div id="ztlist" style="padding-bottom: 10px; display: none;">
                <asp:DropDownList ID="zttype" runat="server" ClientIDMode="Static" onchange="return chanageTree();"
                    Style="min-width: 180px;">
                </asp:DropDownList>
            </div>
            <ul id="AddZT" class="easyui-tree" style="min-width: 230px;" url="" data-options="lines:true,checkbox:true,cascadeCheck:false,onlyLeafCheck:true" />
        </div>
    </div>
    <!-- 添加到专题库 结束-->
    <!-- IPC 检索-->
    <div id="ipcSearch" style="width: 600px; min-height: 500px; display: none;">
        <table cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <select id="ipcs">
                        <option value="IPC" selected="selected">IPC分类</option>
                        <option value="ADM">外观设计分类</option>
                        <option value="ARE">国民经济分类</option>
                    </select>
                </td>
                <td>
                    <input type="text" id="IPCInput" name="IPCInput" style="width: 300px" />
                    <input type="button" id="Search" value="检索" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <input type="radio" id="rdkey" name="stype" checked="checked" /><label for="rdkey">按关键字查找</label>
                    <input type="radio" id="rdipc" name="stype" value="" /><label for="rdipc">按分类号查找</label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="divIPCSearchReturn">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <!-- 添加到专题库 结束-->
    <iframe id="ifile" name="ifile" frameborder="0" src="" style="width: 0; height: 0;">
    </iframe>
    <input type="hidden" id="tname" name="tname" value="zt" />
    <input type="hidden" id="hidthid" name="hidthid" value="1" />
    <input type="hidden" id="hidNodeId" name="hidNodeId" value="1" />
    <input type="hidden" id="hidNodeName" name="hidNodeName" value="1" />
    <input type="hidden" id="hidCpicids" name="hidCpicids" value="," />
    <input type="hidden" id="hidtype" name="hidtype" value="CN" />
    <input type="hidden" id="hidPageIndex" name="hidPageIndex" value="1" />
    <input type="hidden" id="hidSourceType" name="hidSourceType" value="FI" />
    <input type="hidden" id="hidShowType" name="hidShowType" value="0" />
    <input type="hidden" id="hidpagesize" name="hidpagesize" value="10" />
    <input type="hidden" id="hidQuery" name="hidQuery" value="" />
    <input type="hidden" id="hidSearchNo" name="hidSearchNo" value="0" />
    <input type="hidden" id="hiditemcount" name="hiditemcount" value="0" />
    <asp:HiddenField ID="rightlist" runat="server" ClientIDMode="Static" Value="," />
    <asp:HiddenField ID="yonghuleixing" runat="server" ClientIDMode="Static" Value="个人" />
    <%--隐藏变量，存储代理人提示信息--%>
    <input type="hidden" id="hfValue" name="hfValue" runat="server" clientidmode="Static" />
    <input type="hidden" id="hfValueCountryCode" name="hfValueCountryCode" runat="server"
        clientidmode="Static" />
    <input type="hidden" id="hfSelEntrances" name="hfSelEntrances" runat="server" clientidmode="Static" />
    <script language="javascript" type="text/javascript">
        $(function () {
            var data = $("#hfValue").val().split(";");
            var dataCo = $('#hfValueCountryCode').val().split(";");
            var option = {
                max: 12,    //列表里的条目数
                minChars: 0,    //自动完成激活之前填入的最小字符
                width: 400,     //提示的宽度，溢出隐藏
                scrollHeight: 300,   //提示的高度，溢出显示滚动条
                matchContains: true,    //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                autoFill: false    //自动填充

            };
            var option1 = {
                max: 12,    //列表里的条目数
                minChars: 0,    //自动完成激活之前填入的最小字符
                width: 200,     //提示的宽度，溢出隐藏
                scrollHeight: 300,   //提示的高度，溢出显示滚动条
                matchContains: true,    //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                autoFill: false    //自动填充

            };
            $('#TxtCn18').autocomplete(data, option).result(function (event, data, formatted) {
                $('#TxtCn18').val(data.toString().substring(1, 6));

            });

            $('#TxtCn17').autocomplete(dataCo, option1).result(function (event, dataCo, formatted) {
                $('#TxtCn17').val(dataCo.toString().substring(1, 3));

            });
        });
    </script>
</asp:Content>
