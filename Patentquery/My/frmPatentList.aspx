<%@ Page Title="" Language="C#" MasterPageFile="~/Master/index.master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="frmPatentList.aspx.cs" Inherits="Patentquery.My.frmPatentList" %>

<asp:Content ID="head1" runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <script src="../jquery-easyui-1.8.0/jquery.query-2.1.7.js" type="text/javascript"></script>
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery.easyui.min.js"></script>
    <script src="../jquery-easyui-1.8.0/plugins/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../jquery-easyui-1.8.0/plugins/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="../js/patentlist.js"></script>
    <script type="text/javascript" src="../js/page.js"></script>
    <script src="../Js/SearchCommon.js" type="text/javascript"></script>
    <script src="../Js/DropDown.js" type="text/javascript"></script>
    <link href="../Css/dropdown.css" rel="stylesheet" type="text/css" />
    <script src="../Js/autocomplete.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-color: White; width: 990px; margin: 0 auto;">
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
                <span class="lianglan"><a href="javascript:void(0);" onclick="return ShowPL('2');">两栏</a></span>
                <div id="sort" class="dropdown-sort" tabindex="1">
                    <span style="padding-left: 5px">排序</span>
                    <ul class="dropdown" tabindex="1">
                        <li val="PD|DESC"><a href="#">公开/公告日↓</a></li>
                        <li val="PD|AESC"><a href="#">公开/公告日↑</a></li>
                        <li val="AD|DESC"><a href="#">申请日↓</a></li>
                        <li val="AD|AESC"><a href="#">申请日↑</a></li>
                    </ul>
                </div>
                <%--<span id="SPPD" class="DESC"><a href="javascript:void(0);" onclick="return Orderby('SPPD');">
                    公开公告日</a></span> <span id="SPAD"  class="AESC"><a href="javascript:void(0);"
                        onclick="return Orderby('SPAD');">申请日</a></span>--%>
            </div>
            <div id="pagetop" class="easyui-pagination" style="width: 700px; float: left" data-options="total:0,showRefresh: false,displayMsg: '当前显示从 [{from}] 到 [{to}] 共[{total}]条记录'">
            </div>
        </div>
        <div style="margin-top: 4px; display: none;">
            <select id="sorttop" style="float: left" class="pagination-page-list" onchange="return Orderby(this)">
                <option value="PD|DESC">按公开/公告日降序</option>
                <option value="PD|AESC">按公开/公告日升序</option>
                <option value="AD|DESC">按申请日降序</option>
                <option value="AD|AESC">按申请日升序</option>
            </select>
        </div>
        <div class="fun" style="border-top-width: 0px;">
            <label>
                <input type="checkbox" onclick="SelectAll(this)" />全选</label>&nbsp;&nbsp; <a id="ADD2CO"
                    href="javascript:void(0);" onclick="showAddCO()">
                    <img src='/Images/bigshoucang_bj.png' alt='收藏' />收藏</a>&nbsp; <a id="PLXZ" href="javascript:void(0);"
                        onclick="showExportCFG()">
                        <img src='/Images/bigdaochu_bj.png' alt='导出' />导出选中</a>&nbsp; <a id="GJPLXZ" href="javascript:void(0);"
                            onclick="showExportCFG1()">
                            <img src='/Images/bigdaochu_bj.png' alt='导出' />批量导出</a> &nbsp; <a href="javascript:void(0);"
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
                <span class="lianglan"><a href="javascript:void(0);" onclick="return ShowPL('2');">两栏</a></span>
                <div id="sort1" class="dropdown-sort" tabindex="1">
                    <span style="padding-left: 5px">排序</span>
                    <ul class="dropdown" tabindex="1">
                        <li val="PD|DESC"><a href="#">公开/公告日↓</a></li>
                        <li val="PD|AESC"><a href="#">公开/公告日↑</a></li>
                        <li val="AD|DESC"><a href="#">申请日↓</a></li>
                        <li val="AD|AESC"><a href="#">申请日↑</a></li>
                    </ul>
                </div>
                <%--<span id="SPPD1" class="DESC"><a href="javascript:void(0);" onclick="return Orderby('SPPD');">
                    公开公告日</a></span> <span id="SPAD1"  class="AESC"><a href="javascript:void(0);"
                        onclick="return Orderby('SPAD');">申请日</a></span>--%>
            </div>
            <div id="pagebom" class="easyui-pagination" style="width: 700px; float: left" data-options="total:0,showRefresh: false,displayMsg: '当前显示从 [{from}] 到 [{to}] 共[{total}]条记录'">
            </div>
        </div>
        <div style="margin-top: 4px; display: none">
            <select id="sortbom" style="float: left" class="pagination-page-list" onchange="return Orderby(this)">
                <option value="PD|DESC">按公开/公告日降序</option>
                <option value="PD|AESC">按公开/公告日升序</option>
                <option value="AD|DESC">按申请日降序</option>
                <option value="AD|AESC">按申请日升序</option>
            </select>
        </div>
    </div>
    <!-- 表格检索 开始 -->
    <div id="tabSearch" style="width: 690px; display: none;">
        <span id="DseachTitle" style="display: none">编辑检索式</span>
        <div id="cndivQueryTable" class="ztdivQueryTable" style="overflow: inherit; border-width: 0px;">
            <dl>
                <dt class="typecounty">专利类型：</dt>
                <dd class="typecounty" style="width: 500px; height: 30px;">
                    <input type="checkbox" checked="checked" id="chkAll" onclick="SelectAll1('cndivQueryTable',this);" />
                    <label for="chkAll">
                        全选</label>
                    <input id="fm" checked="checked" name="checkbox" type="checkbox" value="FM" /><label
                        for="fm">发明[FM]</label>
                    <input id="xx" checked="checked" name="checkbox" type="checkbox" value="XX" /><label
                        for="xx">实用新型[XX]</label>
                    <input id="wg" checked="checked" name="checkbox" type="checkbox" value="WG" /><label
                        for="wg">外观[WG]</label></dd>
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
                <dt id="dtTbCS">权利要求(CS):</dt>
                <dd>
                    <input name="TxtCn21" type="text" id="TxtCn21" lang="CS" class="textBox" title="" />
                </dd>
                <dt id="dtTbDS">说明书(DS):</dt>
                <dd>
                    <input name="TxtCn22" type="text" id="TxtCn22" lang="DS" class="textBox" title="" />
                </dd>
            </dl>
        </div>
        <div id="endivQueryTable" class="ztdivQueryTable" style="overflow: inherit; display: none;">
            <dl>
                <dt class="typecounty">国家：</dt>
                <dd class="typecounty" style="width: 500px; height: 50px;">
                    <input id="ckCN" type="checkbox" name="enckbox" checked="checked" value="CN" /><label
                        for="ckCN">中国</label>
                    <input id="ckUS" type="checkbox" name="enckbox" checked="checked" value="US" /><label
                        for="ckUS">美国</label>
                    <input id="ckDE" type="checkbox" name="enckbox" checked="checked" value="DE" /><label
                        for="ckDE">德国</label>
                    <input id="ckJP" type="checkbox" name="enckbox" checked="checked" value="JP" /><label
                        for="ckJP">日本</label>
                    <input id="ckGB" type="checkbox" name="enckbox" checked="checked" value="GB" /><label
                        for="ckGB">英国</label>
                    <input id="ckFR" type="checkbox" name="enckbox" checked="checked" value="FR" /><label
                        for="ckFR">法国</label>
                    <input id="ckKR" type="checkbox" name="enckbox" checked="checked" value="KR" /><label
                        for="ckKR">韩国</label>
                    <input id="ckRU" type="checkbox" name="enckbox" checked="checked" value="RU" /><label
                        for="ckRU">俄罗斯</label>
                    <input id="ckCH" type="checkbox" name="enckbox" checked="checked" value="CH" /><label
                        for="ckCH">瑞士</label>
                    <input id="ckEP" type="checkbox" name="enckbox" checked="checked" value="EP" /><label
                        for="ckEP">EPO</label>
                    <input id="ckWO" type="checkbox" name="enckbox" checked="checked" value="WO" /><label
                        for="ckWO">WIPO</label>
                    <label>
                        <input id="ckELSE" name="enckbox" type="checkbox" checked="checked" value="ELSE" />其他国家及地区</label>
                    <input type="checkbox" checked="checked" id="enckall" onclick="SelectAll1('endivQueryTable',this);" /><label
                        for="enckall">全选</label>
                </dd>
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
        <div id="divSPText" class="ztdivQueryTable" style="padding-top: 0px; border-width: 0px;
            overflow: inherit;">
            <dl style="margin-top: 0px">
                <dt id="dt1">检索式：</dt>
                <dd>
                    <textarea name="TxtSearch" rows="2" cols="20" id="TxtSearch" style="height: 60px;
                        width: 470px;"></textarea>
                    <span id="Span1"></span>
                </dd>
            </dl>
        </div>
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
    <div id="cart" style="display: none">
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
    <iframe id="ifile" name="ifile" frameborder="0" src="" style="width: 0; height: 0;">
    </iframe>
    <input type="hidden" id="hidCpicids" name="hidCpicids" value="," />
    <input type="hidden" id="hidSourceType" name="hidSourceType" value="FI" />
    <input type="hidden" id="hidShowType" name="hidShowType" value="0" />
    <input type="hidden" id="hidpagesize" name="hidpagesize" value="10" />
    <input type="hidden" id="hidPageIndex" name="hidPageIndex" value="1" />
    <input type="hidden" id="hidItemCount" name="hidItemCount" value="0" />
    <input type="hidden" id="hidtype" name="hidtype" value="CN" />
    <asp:HiddenField ID="rightlist" runat="server" ClientIDMode="Static" Value="," />
    <asp:HiddenField ID="yonghuleixing" runat="server" ClientIDMode="Static" Value="个人" />
    <script type="text/javascript">
        $(document).ready(function () {
            var db = requestUrl('db');
            InitCommon(db);
            var itemcount = requestUrl('Nm');
            var SearchNo = requestUrl('No');
            var kw = requestUrl('kw');
            from = requestUrl('from');
            if (from == null || from == "") {
                from = "FI";
            }
            if (from != "FI") {
                $("#divSSC").hide();
            }
            var pagesize = $('#hidpagesize').val();
            ShowTable(db, SearchNo, from, 1, pagesize, itemcount, '0');
            regPatentListSelectPage();
            ddsort = new DropDown($('#sort'), null, Orderby);
            ddsort.setText("公开/公告日↓");
            ddsort1 = new DropDown($('#sort1'), null, Orderby);
            ddsort1.setText("公开/公告日↓");
            var patternUrl = requestUrl("Query").replace("#", "");
            patternUrl = decodeURIComponent(patternUrl).Trim();
            var leixing = getEndFlag(patternUrl);
            if (leixing == null || leixing == "") {
                $(".typecounty").show();
            }
            else {
                $(".typecounty").hide();
            }


        });
    </script>
</asp:Content>
