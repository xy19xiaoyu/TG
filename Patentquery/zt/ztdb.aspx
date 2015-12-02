<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Index.master" AutoEventWireup="true"
    CodeBehind="ztdb.aspx.cs" Inherits="TH_ztdb" %>

<asp:Content ID="he1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <link href="../css/themes/default/default.css" rel="stylesheet" type="text/css" />
    <link href='../stars/jquery.rating.css' type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery.easyui.min.js"></script>
    <script src='../stars/jquery.MetaData.js' type="text/javascript" language="javascript"></script>
    <script src='../stars/jquery.rating.js' type="text/javascript" language="javascript"></script>
    <script src='../js/th.js' type="text/javascript" language="javascript"></script>
    <script src='../js/Trans.js' type="text/javascript" language="javascript"></script>
    <script src="../jquery-easyui-1.8.0/plugins/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../jquery-easyui-1.8.0/plugins/jquery.autocomplete.css" rel="stylesheet"
        type="text/css" />
    <script src="../Js/DropDown.js" type="text/javascript"></script>
    <link href="../Css/dropdown.css" rel="stylesheet" type="text/css" />
    <script src="../Js/autocomplete.js" type="text/javascript"></script>
    <script src="../Js/SearchCommon.js" type="text/javascript"></script>
    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- IPC 检索-->
    <div id="left" style="width: 225px;">
        <div id="pinlieft">
            <div id="left_title" class="left_ti">
                <div id="dd" class="dropdown-zt" tabindex="1">
                    <span></span>
                    <ul class="dropdown" tabindex="1">
                        <li val='up'><a href="#">更新</a></li>
                    </ul>
                </div>
            </div>
            <div class="left_content_zt" style="min-height: 493px;">
                <div id="divzt" style="min-height: 470px; overflow-x: auto">
                    <ul id="zt" class="easyui-tree" data-options="lines:true" />
                </div>
                <div id="divup" style="min-height: 470px; display: none; overflow-x: auto">
                    <ul id="up" class="easyui-tree" data-options="lines:true" />
                </div>
                <div id="divqy" style="min-height: 470px; display: none; overflow-x: auto">
                    <ul id="qy" class="easyui-tree" data-options="lines:true" />
                </div>
            </div>
        </div>
    </div>
    <div id="right" style="width: 768px;">
        <div id="showlist" style="display: none">
            <div class="spbutton" id="divSSC">
                <ul>
                    <li>
                        <div class="commandLeft" id="ZLXMECJSGL">
                            <span class="btn" onclick="ShowTabSearch('过滤检索','')">过滤检索</span> <span class="btn"
                                onclick="ShowTabSearch('二次检索','')">二次检索</span>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="page">
                <div class="pageleft">
                    <span class="wentu"><a href="javascript:void(0);" onclick="return ShowPL('0');">文图</a></span>
                    <span class="liebiao"><a href="javascript:void(0);" onclick="return ShowPL('1');">列表</a></span>
                    <div id="sort" class="dropdown-sort" tabindex="1">
                        <span style="padding-left: 5px">排序</span>
                        <ul class="dropdown" tabindex="1">
                            <li val="PD|DESC"><a href="#">公开/公告日↓</a></li>
                            <li val="PD|AESC"><a href="#">公开/公告日↑</a></li>
                            <li val="AD|DESC"><a href="#">申请日↓</a></li>
                            <li val="AD|AESC"><a href="#">申请日↑</a></li>
                            <li val="STAR"><a href="#">核心专利↓</a></li>
                        </ul>
                    </div>
                </div>
                <div id="pagetop" class="easyui-pagination" style="width: 525px; float: left" data-options="total:0,showRefresh: false,displayMsg:'从 [{from}] 到 [{to}] 共[{total}]条'">
                </div>
            </div>
            <div class="fun" style="border-top-width: 0px;">
                <label><input type="checkbox" onclick="SelectAll(this)" />全选</label>&nbsp;&nbsp; <a id="ADD2CO"
                    href="javascript:void(0);" onclick="showAddCO()">
                    <img src='/Images/bigshoucang_bj.png' alt='收藏' />收藏</a>&nbsp; <a id="PLXZ" href="javascript:void(0);"
                        onclick="showExportCFG()">
                        <img src='/Images/bigdaochu_bj.png' alt='导出' />导出选中</a>&nbsp; <a id="GJPLXZ" href="javascript:void(0);"
                            onclick="showExportCFG1()">
                            <img src='/Images/bigdaochu_bj.png' alt='导出' />批量导出</a>&nbsp;<a href="javascript:void(0);"
                                onclick="GOTOST();">
                                <img src='/Images/bigfenxi_bj.png' alt='分析' />分析</a>&nbsp; <a id="a_eddata" href="javascript:void(0);"
                                    onclick="ShowBatchMoveToZT()">
                                    <img src='/Images/smallzhuanyi_bj.png' alt='移动到' />移动到</a> <a id="a_deldata" href="javascript:void(0);"
                                        onclick="DelToZT('')">
                                        <img src='/Images/smallshanchu_bj.png' alt='删除' />删除</a>
            </div>
            <div id="divlist" class="content">
            </div>
            <div class="page">
                <div class="pageleft">
                    <span class="wentu"><a href="javascript:void(0);" onclick="return ShowPL('0');">文图</a></span>
                    <span class="liebiao"><a href="javascript:void(0);" onclick="return ShowPL('1');">列表</a></span>
                    <div id="sort1" class="dropdown-sort" tabindex="1">
                        <span style="padding-left: 5px">排序</span>
                        <ul class="dropdown" tabindex="1">
                            <li val="PD|DESC"><a href="#">公开/公告日↓</a></li>
                            <li val="PD|AESC"><a href="#">公开/公告日↑</a></li>
                            <li val="AD|DESC"><a href="#">申请日↓</a></li>
                            <li val="AD|AESC"><a href="#">申请日↑</a></li>
                            <li val="STAR"><a href="#">核心专利↓</a></li>
                        </ul>
                    </div>
                </div>
                <div id="pagebom" class="easyui-pagination" style="width: 525px; float: left" data-options="total:0,showRefresh: false,displayMsg:
    '从 [{from}] 到 [{to}] 共[{total}]条记录'">
                </div>
            </div>
        </div>
        <div id="showsp" style="display: none">
            <!-- 添加检索式工具栏 开始-->
            <div id="divtool" style="padding: 5px; height: auto">
                <div style="margin-bottom: 5px">
                    <a id="cnzt_addsp" href="javascript:void(0);" data-options="disabled:true" class="easyui-linkbutton"
                        iconcls="icon-add" onclick="return ShowTabSearch('添加检索式','cn');" plain="true">添加检索式</a>
                    <a id="cnzt_edsp" href="javascript:void(0);" data-options="disabled:true" class="easyui-linkbutton"
                        iconcls="icon-edit" onclick="return ShowTabSearch('修改检索式','cn')" plain="true">修改检索式</a>
                    <a id="cnzt_delsp" href="javascript:void(0);" data-options="disabled:true" class="easyui-linkbutton"
                        iconcls="icon-remove" onclick="return deleteSP('cn');" plain="true">删除检索式</a>
                </div>
            </div>
            <div id="divtoolen" style="padding: 5px; height: auto">
                <div style="margin-bottom: 5px">
                    <a id="enzt_addsp" href="javascript:void(0);" data-options="disabled:true" class="easyui-linkbutton"
                        iconcls="icon-add" onclick="return ShowTabSearch('添加检索式','en');" plain="true">添加检索式</a>
                    <a id="enzt_edsp" href="javascript:void(0);" data-options="disabled:true" class="easyui-linkbutton"
                        iconcls="icon-edit" onclick="return ShowTabSearch('修改检索式','en')" plain="true">修改检索式</a>
                    <a id="enzt_delsp" href="javascript:void(0);" data-options="disabled:true" class="easyui-linkbutton"
                        iconcls="icon-remove" onclick="return deleteSP('en');" plain="true">删除检索式</a>                </div>
            </div>
            <!-- 添加检索式工具栏 结束-->
            <!-- 检索式列表
    开始 -->
            <div class="easyui-tabs" style="width: 760px; margin: 5px auto;">
                <div id="cnsp" title="中国专利" style="min-height: 460px; padding: 10px">
                    <!-- 概览列表 开始 -->
                    <table id="TabcnSP" class="easyui-datagrid" style="width: 740px; height: 465px;"
                        data-options="rownumbers:true,singleSelect:true,collapsible:true,toolbar:'#divtool'">
                        <thead>
                            <tr>
                                <th data-options="field:'sp',width:470">
                                    检索式
                                </th>
                                <th data-options="field:'Hit',width:80,align:'right'">
                                    命中篇数
                                </th>
                                <th data-options="field:'UpdateDate',width:140,align:'right'">
                                    操作时间
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
                <div id="ensp" title="世界专利" style="min-height: 460px; padding: 10px">
                    <!-- 概览列表 开始 -->
                    <table id="TabenSP" class="easyui-datagrid" style="width: 740px; height: 465px;"
                        data-options="rownumbers:true,singleSelect:true,collapsible:true,toolbar:'#divtoolen'">
                        <thead>
                            <tr>
                                <th data-options="field:'sp',width:470">
                                    检索式
                                </th>
                                <th data-options="field:'Hit',width:80,align:'right'">
                                    命中篇数
                                </th>
                                <th data-options="field:'UpdateDate',width:140,align:'right'">
                                    操作时间
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
            <!-- 检索式列表 结束-->
        </div>
        <div id="showspdes" style="display: none">
            <div id="spdes" class="boxInner" style="height: 510px; padding: 20px;">
            </div>
        </div>
        <div id="divshowhelp">
            <div class="boxInner" style="min-height: 510px; padding: 20px;">
                <ul>
                    <li>1.请右键点击右边<b>我的专题库</b>下边的方框中<b>专题库目录</b>或者<b>空白处</b>管理您的专题库。</li>
                    <li>2.选择专题库的节点，点击节点的<b>中文</b>或<b>英文</b>浏览您专题库的专利，点击<b>节点名称</b>显示节点的简介。</li>
                </ul>
            </div>
        </div>
        <div class="box" id="divnodata" style="display: none">
            <div class="boxInner" style="min-height: 510px; padding: 20px;">
                <ul>
                    <li>此节点下还没有专利！</li>
                    <li>1.查看子节点数据</li>
                    <li>2.请从专利检索结果的列表中页面或者专利详细信息页面进行添加！ 现在就去<a href="../my/SmartQuery.aspx" target="_blank"
                        style="color: Red"><b>检索</b></a></li>
                    <li>3.右键点击节点编辑数据，通过检索式进行数据添加</li>
                </ul>
            </div>
        </div>
        <div id="divnoupdata" style="display: none">
            <div class="boxInner" style="min-height: 510px; padding: 20px;">
                <ul>
                    <li>此节点下还没有更新的专利数据！</li>
                </ul>
            </div>
        </div>
    </div>
    <!-- 添加节点开始 -->
    <div id="DivAddNode" style="width: 480px; height: 150px; display: none;">
        <b><span id="dialogtitle" style="display: none"></span></b>
        <div style="padding-left: 10px">
            <table>
                <tr>
                    <th>
                        名称:
                    </th>
                    <td>
                        <input type="text" id="newclass" name="newclass" style="width: 400px" /><br />
                        <span id="showmsg" style="display: none; color: Red;">*请输入名称</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        描述：
                    </th>
                    <td>
                        <textarea id="txtNodeDes" cols="200" name="txtNodeDes" rows="40" style="width: 400px;
                            height: 100px"></textarea>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!-- 添加节点 结束-->
    <!-- 右键菜单 开始-->
    <div id="mm" class="easyui-menu" style="width: 120px;">
        <div onclick="append('zt','添加分类')" data-options="iconCls:'icon-add',disabled:'true'">
            添加分类</div>
        <div onclick="edit('zt','修改分类')" data-options="iconCls:'icon-edit',disabled:'true'">
            修改名称
        </div>
        <div id="m_delData" onclick="showtbsp('zt')" data-options="iconCls:'icon-edit',disabled:'true'">
            修改数据</div>
        <div class="menu-sep">
        </div>
        <div onclick="RemoveNode('zt') " data-options="iconCls:'icon-remove',disabled:'true'">
            删除</div>
    </div>
    <!-- 右键菜单 结束-->
    <div id="AddCA" class="easyui-menu" style="width: 120px;">
        <div onclick="append('ca','添加竞争对手')" data-options="iconCls:'icon-add',disabled:'true'">
            添加竞争对手</div>
    </div>
    <!-- 右键菜单 结束-->
    <div id="addzt" class="easyui-menu" style="width: 120px;">
        <div onclick="append('zt','添加主分类')" data-options="iconCls:'icon-add',disabled:'true'">
            添加主分类</div>
    </div>
    <div id="addqy" class="easyui-menu" style="width: 120px;">
        <div onclick="append('qy','添加主分类')" data-options="iconCls:'icon-add',disabled:'true'">
            添加主分类</div>
    </div>
    <!-- 右键菜单 开始-->
    <div id="mmca" class="easyui-menu" style="width: 120px;">
        <div onclick="append('ca','添加子竞争对手')" data-options="iconCls:'icon-add',disabled:'true'">
            添加子竞争对手</div>
        <div onclick="edit('ca','修改竞争对手名称')" data-options="iconCls:'icon-edit',disabled:'true'">
            修改竞争对手名称
        </div>
        <div onclick="showtbsp('ca')" data-options="iconCls:'icon-edit',disabled:'true'">
            修改检索式</div>
        <div class="menu-sep">
        </div>
        <div onclick="RemoveNode('ca') " data-options="iconCls:'icon-remove',disabled:'true'">
            删除</div>
    </div>
    <!-- 右键菜单 结束-->
    <!-- 右键菜单 开始-->
    <div id="mmqy" class="easyui-menu" style="width: 120px;">
        <div onclick="append('qy','添加分类')" data-options="iconCls:'icon-add',disabled:'true'">
            添加分类</div>
        <div onclick="edit('qy','修改分类')" data-options="iconCls:'icon-edit',disabled:'true'">
            修改名称
        </div>
        <div id="Div2" onclick="showtbsp('qy')" data-options="iconCls:'icon-edit',disabled:'true'">
            修改数据</div>
        <div class="menu-sep">
        </div>
        <div onclick="RemoveNode('qy') " data-options="iconCls:'icon-remove',disabled:'true'">
            删除</div>
    </div>
    <!-- 右键菜单 结束-->
    <!-- Tree CN END -->
    <!-- 设置分类 开始-->
    <div id="SetZT" style="min-width: 200px; display: none;">
        <div style="min-height: 300px">
            <ul id="setZT" class="easyui-tree" style="min-width: 200px" data-options="lines:true,checkbox:true,cascadeCheck:false,onlyLeafCheck:true" />
        </div>
    </div>
    <!-- 设置分类 结束 -->
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
    <!-- 收藏夹 开始-->
    <div id="AddCO" style="width: 300px; display: none;" class="ke-dialog">
        <div style="min-height: 250px">
            <ul id="CO" class="easyui-tree" style="width: 300px" data-options="lines:true,checkbox:true,cascadeCheck:false" />
        </div>
        <hr />
        <div style="width: 318px">
            自定义标注：
            <textarea name="TextBoxNote" rows="5" cols="45" style="width: 290px" id="TextBoxNote"></textarea>
        </div>
    </div>
    <!-- 收藏夹 结束 -->
    <div id="sm" style="width: 520px; height: 300px; display: none;">
        <iframe id="ism" name="ism" scrolling="no" frameborder="0" src="" style="width: 100%;
            height: 100%;"></iframe>
    </div>
    <!-- 表格检索 开始 -->
    <div id="tabSearch" style="width: 690px; display: none;">
        <span id="DseachTitle" style="display: none">编辑检索式</span>
        <div id="cndivQueryTable" class="ztdivQueryTable" style="overflow: inherit; border-width: 0px;">
            <dl>
                <dt>专利类型：</dt>
                <dd style="width: 500px; height: 30px;">
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
                    <input type="text" id="TxtCn21" lang="CS" class="textBox" title="" />
                </dd>
                <dt id="dtTbDS">说明书(DS):</dt>
                <dd>
                    <input type="text" id="TxtCn22" lang="DS" class="textBox" title="" />
                </dd>
            </dl>
        </div>
        <div id="endivQueryTable" class="ztdivQueryTable" style="overflow: inherit; display: none;">
            <dl>
                <dt>国家：</dt>
                <dd style="width: 500px; height: 50px;">
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
                    <span id="LabValidationResult"></span>
                </dd>
            </dl>
        </div>
        <select id="SlctLogicSymbol" style="display: none">
            <option value="+">+(或)</option>
            <option value="-">-(非)</option>
            <option selected="selected" value="*">*(与)</option>
        </select>
    </div>
    <!-- 表格检索 结束-->
    <iframe id="ifile" name="ifile" frameborder="0" src="" style="width: 0; height: 0;">
    </iframe>
    <input type="hidden" id="tname" name="tname" value="zt" />
    <asp:HiddenField ID="hidthid" runat="server" ClientIDMode="Static" Value="1" />
    <asp:HiddenField ID="yonghuleixing" runat="server" ClientIDMode="Static" Value="1" />    
    <input type="hidden" id="hidNodeId" name="hidNodeId" value="1" />
    <input type="hidden" id="hidNodeName" name="hidNodeName" value="1" />
    <input type="hidden" id="hidCpicids" name="hidCpicids" value="," />
    <input type="hidden" id="hidtype" name="hidtype" value="1" />
    <input type="hidden" id="hidPageIndex" name="hidPageIndex" value="1" />
    <input type="hidden" id="hidSourceType" name="hidSourceType" value="db" />
    <input type="hidden" id="hidShowType" name="hidShowType" value="0" />
    <input type="hidden" id="hidpagesize" name="hidpagesize" value="10" />
    <input type="hidden" id="hidItemCount" name="hidItemCount" value="10" />
    <asp:HiddenField ID="rightlist" runat="server" ClientIDMode="Static" Value="," />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#showlist").hide();
            $("#showsp").hide(); $("#showspdes").show(); $('#divca').hide(); $('#divup').hide();
            Loadzttype();
        }); 
    </script>
</asp:Content>
