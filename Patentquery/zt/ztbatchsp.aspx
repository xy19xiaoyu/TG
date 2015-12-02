<%@ Page Title="" Language="C#" MasterPageFile="~/Master/zt.master" AutoEventWireup="true"
    CodeBehind="ztbatchsp.aspx.cs" Inherits="Patentquery.zt.ztbatchsp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <link href="../css/themes/default/default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/ztbatchsp.js"></script>
    <script src="../js/CheckSPCN.js"></script>
    <script src="../js/CheckSPEN.js"></script>
    <script src="../Js/SearchCommon.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/page.js"></script>
    <div class="command">
        <b>选择专题库：</b><asp:DropDownList ID="ztnamelist" runat="server" ClientIDMode="Static">
        </asp:DropDownList>
        <span class="btn" onclick="ShowSPTable();">确定</span>
    </div>
    <!-- 检索式列表 开始 -->
    <div style="margin: 0 auto; width: 990px; padding: 1px;">
        <div class="easyui-tabs" style="width: 988px;">
            <div id="cnsp" title="中国专利" style="min-height: 560px; width: 100%; padding: 10px;">
                <!-- 概览列表  开始  -->
                <table id="TabcnSP" class="easyui-datagrid" style="width: 960px; height: 560px;"
                    data-options="singleSelect:true,collapsible:true,toolbar:'#divtool'">
                    <thead>
                        <tr>
                            <th data-options="field:'NodeName',width:120">
                                节点名称
                            </th>
                            <th data-options="field:'SPNum',width:60">
                                检索编号
                            </th>
                            <th data-options="field:'sp',width:500">
                                检索式
                            </th>
                            <th data-options="field:'Hit',width:80,align:'right'">
                                命中篇数
                            </th>
                            <th data-options="field:'st',width:50,align:'right'">
                                状态
                            </th>
                            <th data-options="field:'UpdateDate',width:120,align:'right'">
                                操作时间
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
            <div id="ensp" title="世界专利" style="min-height: 560px; width: 100%; padding: 10px;">
                <!-- 概览列表  开始  -->
                <table id="TabenSP" class="easyui-datagrid" style="width: 960px; height: 560px;"
                    data-options="singleSelect:true,collapsible:true,toolbar:'#divtoolen'">
                    <thead>
                        <tr>
                            <th data-options="field:'NodeName',width:120">
                                节点名称
                            </th>
                            <th data-options="field:'SPNum',width:60">
                                检索编号
                            </th>
                            <th data-options="field:'sp',width:500">
                                检索式
                            </th>
                            <th data-options="field:'Hit',width:80,align:'right'">
                                命中篇数
                            </th>
                            <th data-options="field:'st',width:50,align:'right'">
                                状态
                            </th>
                            <th data-options="field:'UpdateDate',width:120,align:'right'">
                                操作时间
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </div>
    <!-- 添加检索式工具栏 开始-->
    <div id="divtool" style="padding: 5px; height: auto">
        <div style="margin-bottom: 5px">
            <a id="cnzt_addsp" href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-add"
                onclick="return ShowTabSearch('添加检索式','cn');" plain="true">添加检索式</a>
        </div>
    </div>
    <div id="divtoolen" style="padding: 5px; height: auto">
        <div style="margin-bottom: 5px">
            <a id="enzt_addsp" href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-add"
                onclick="return ShowTabSearch('添加检索式','en');" plain="true">添加检索式</a> 
        </div>
    </div>
    <!-- 添加检索式工具栏 结束-->
    <!-- 表格检索 开始 -->
    <div id="tabSearch" style="width: 740px; display: none;">
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
                <dt id="dtTableEnPN">公开号（PN）：</dt>
                <dd id="ddTableEnPN">
                    <input name="TxtEn5" type="text" id="TxtEn5" class="textBox" lang="PN" />
                </dd>
                <dt id="dtTableEnPD">公开日（PD）：</dt>
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
    <!-- 右键菜单 结束-->
    <div id="BoundToZT" class="easyui-menu" style="width: 120px;">
        <div onclick="ShowAddToZT();" data-options="iconCls:'icon-add'">
            绑定至导航</div>
        <div onclick="RemoveBindToZT('cn');" data-options="iconCls:'icon-remove'">
            取消绑定</div>
        <div class="menu-sep"></div>
        <div onclick="ShowTabSearch('修改检索式','cn');" data-options="iconCls:'icon-add'">
            修改检索式
        </div>
        <div class="menu-sep"></div>
        <div onclick="deleteSP('cn');" data-options="iconCls:'icon-remove'">
            删除检索式</div>
    </div>
    <!-- 右键菜单 结束-->
     <!-- 右键菜单 结束-->
    <div id="EnBoundToZT" class="easyui-menu" style="width: 120px;">
        <div onclick="ShowAddToZT();" data-options="iconCls:'icon-add'">
            绑定至导航</div>
         <div onclick="RemoveBindToZT('en');" data-options="iconCls:'icon-remove'">
            取消绑定</div>
        <div class="menu-sep"></div>
        <div onclick="ShowTabSearch('修改检索式','en');" data-options="iconCls:'icon-add'">
            修改检索式
        </div>
        <div class="menu-sep"></div>
        <div onclick="deleteSP('en');" data-options="iconCls:'icon-remove'">
            删除检索式</div>
    </div>
    <!-- 右键菜单 结束-->
    <div id="AddToZT" style="min-width: 200px; display: none;">
        <div style="min-height: 300px">
            <ul id="AddZT" class="easyui-tree" style="min-width: 200px" data-options="lines:true,checkbox:true,cascadeCheck:false" />
        </div>
    </div>
    <!-- 设置分类 结束 -->
    <input type="hidden" id="hidtype" name="hidtype" value="1" />
    <input type="hidden" id="hidspid" name="rowIndex" value="0" />    
    <input type="hidden" id="tableindex" name="tableindex" value="0" />
</asp:Content>
