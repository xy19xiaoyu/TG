<%@ Page Language="C#" MasterPageFile="~/Master/index.Master" AutoEventWireup="true"
    Title="专利预警--厦漳泉科技基础资源服务平台-专利平台" CodeBehind="JZDSYJ.aspx.cs" Inherits="Patentquery.YJ.JZDSYJ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="zlyj.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <%--<script type="text/javascript" src="../jquery-easyui-1.8.0/jquery.easyui.min.js"></script>
    <script src="../jquery-easyui-1.8.0/datagrid-detailview.js" type="text/javascript"></script>--%>
    <script src="../Js/cnExpertSearch.js" type="text/javascript"></script>
    <link href="../Css/AJAX.css" rel="stylesheet" type="text/css" />
    <script src="../jquery-easyui-1.8.0/plugins/jQuery.niceTitle.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Js/JScript.js"></script>
    <link href="../Jquery-ui-1.10.3/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Jquery-ui-1.10.3/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.accordion.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/datagrid-detailview.js" type="text/javascript"></script>
    <style>
        dd div
        {
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="left-min">
        <div id="left_title" class="left_ti_left">
            <span class="spleft" onclick="changetitle('CN')">中国专利预警</span><span class="spright"
                onclick="changetitle('EN')">世界专利预警</span>
        </div>
        <div class="left_content1">
            <div id="lstcn">
                <ul>
                    <li>
                        <div id="jzds" class="jzdsselected" onclick="showRight(this)" onmouseover="setselected(this)"
                            onmouseout="unsetselected(this)" />
                    </li>
                    <li>
                        <div id="jsdx" class="jsdx" onclick="showRight(this)" onmouseover="setselected(this)"
                            onmouseout="unsetselected(this)" />
                    </li>
                    <li>
                        <div id="fmrdx" class="fmrdx" onclick="showRight(this)" onmouseover="setselected(this)"
                            onmouseout="unsetselected(this)" />
                    </li>
                    <li>
                        <div id="qyfb" class="qyfb" onclick="showRight(this)" onmouseover="setselected(this)"
                            onmouseout="unsetselected(this)" />
                    </li>
                    <li>
                        <div id="gwlh" class="gwlh" onclick="showRight(this)" onmouseover="setselected(this)"
                            onmouseout="unsetselected(this)" />
                    </li>
                    <li>
                        <div id="gaoji" class="gaoji" onclick="showRight(this)" onmouseover="setselected(this)"
                            onmouseout="unsetselected(this)" />
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <%--  <div id="content_mid" style="cursor: pointer;" onclick="changeleft()">
        <img id="leftctr" src="../imgs/2left.png" style="border-width: 0px; cursor: pointer;"
            alt="隐藏" title="隐藏" />
    </div>--%>
    <div id="right" class="rightmin">
        <div id="toptabs">
            <ul id="ultabs">
                <li><a id="touru" href="1" style="display: none;">专利投入跟踪预警</a></li>
                <li><a id="chengguo" href="2" style="display: none;">专利成果跟踪预警</a></li>
                <li><a id="shichangzhongxin" href="3" style="display: none;">市场重心跟踪预警</a></li>
                <li><a id="jishuzhongxin" href="4" style="display: none;">技术重心跟踪预警</a></li>
                <li><a id="shenqingren" href="5" style="display: none;">申请人跟踪预警</a></li>
                <li><a id="yanfaren" href="6" style="display: none;">研发人才跟踪预警</a></li>
                <li><a id="zhiliang" href="7" style="display: none;">专利质量跟踪预警</a></li>
                <li><a id="shouming" href="8" style="display: none;">专利寿命跟踪预警</a></li>
                <li><a id="laihua" href="9" style="display: none;">来华专利预警</a></li>
                <li><a id="zidingyi" href="23" style="display: none;">自定义预警</a></li>
            </ul>
        </div>
        <div class="right_top">
            <select id="ddlctype">
            </select>
            <input type="text" id="txtSearch" />
            <span class="button" onclick="showMianTable()">查询</span> <span class="button" onclick="showAddyjDG()">
                增加</span> <span class="button" onclick="delyj()">删除</span>
            <%--<span id="btnhot" class="button"
                    onclick="showhot();">热点预警</span>--%>
            <span style="margin-left: 20px; float: right; line-height: 30px" id="dgPostion">&nbsp;</span>
        </div>
        <div class="right_content">
            <table id="tableyj" class="easyui-datagrid" style="width: 780px; height: 490px;"
                data-options="singleSelect:false,collapsible:true,pagination:true,pageList:[15,30,35,50]">
                <thead>
                    <tr>
                        <%--<th data-options="field:'ck',checkbox:true">
                        </th>
                        <th data-options="field:'ALIAS',width:80">
                            预警名称
                        </th>
                        <th data-options="field:'S_NAME',width:100">
                            预警项
                        </th>
                        <th data-options="field:'C_DATE',width:80,align:'center',formatter:function(value,rowData,rowIndex){return ChangeDateToString(value, rowData, rowIndex,0)}">
                            设置日期
                        </th>
                        <th data-options="field:'CURRENTNUM',width:50,align:'center',formatter:function(value,rowData,rowIndex){return formatlist(value, rowData, rowIndex,0)}">
                            <label >当前数</label>
                        </th>
                        <th data-options="field:'CHANGENUM',width:50,align:'center',formatter:function(value,rowData,rowIndex){return formatlist(value, rowData, rowIndex,1)}">
                            变更数
                        </th>
                        <th data-options="field:'BEIZHU',width:110,align:'left'">
                            备注
                        </th>
                        <th data-options="field:'C_Id',width:135,align:'center',formatter:function(value,rowData,rowIndex){return formathit(value, rowData, rowIndex)}">
                            操作
                        </th>
                        <th data-options="field:'C_Id_status',width:100,align:'center',formatter:function(value,rowData,rowIndex){return formatstatus(value, rowData, rowIndex)}">
                            状态
                        </th>--%>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <div id="addyjdiv" style="display: none; height: auto;">
        <dl>
            <div id="divfanwei" style="display: none">
                <dt><span>预警范围：</span>
                    <select id="ddlfanwei" runat="server">
                    </select></dt>
            </div>
            <dt><span id="dgti1">预警名称：</span></dt>
            <dd>
                <input type="text" id="itxtname" /></dd>
            <dt><span id="dgti2"></span>
                <img id="TiShi" src="../images/img/home-08.jpg" alt="提示:高级预警" style="cursor: pointer;"
                    title="<strong>提示</strong><br/>     &nbsp;&nbsp;发明名称（TI）、摘要（AB）<br/>     &nbsp;&nbsp;主权利要求（CL）、关键词（TX）<br/>     &nbsp;&nbsp;申请人（PA）、分类号（IC）<br/>     &nbsp;&nbsp;申请号（AN）、申请日（AD）<br/>     &nbsp;&nbsp;公开号（PN）、公开日（PD）<br/>     &nbsp;&nbsp;公告号（GN）、公告日（GD）<br/>     &nbsp;&nbsp;优先权号（PR）、发明人（IN）<br/>     &nbsp;&nbsp;范畴分类（CT）、申请人地址（DZ）<br/>     &nbsp;&nbsp;国省代码（CO）、代理机构（AG）<br/>     &nbsp;&nbsp;代理人（AT）、主分类号（MC）<br/>     &nbsp;&nbsp;权利要求（CS）、说明书（DS）" /></dt>
            <dd>
                <!--onkeyup="findColors();" -->
                <div id="sheng" style="display: none">
                    <select id="ddlSheng" runat="server">
                    </select></div>
                <div id="GuoJia" style="display: none">
                    <select id="ddlGuoJia" runat="server">
                    </select></div>
                <div id="ShiJie" style="display: none">
                    <select id="ddlShiJie" runat="server">
                    </select></div>
                <div id="divkeyword">
                    <input type="text" id="txtKeyWord" onkeyup="findColors();" />
                </div>
                <div id="divzhuanti">
                    <input id="cc" method='get' value="请选择专题库" style="width: 250px" />
                </div>
                <input type="button" value="添加" onclick="additem();"  style="margin-left:2px;" />
                <div id="popup">
                    <ul id="colors_ul">
                    </ul>
                </div>
            </dd>
            <dt><span id="dgti3"></span></dt>
            <dd>
                <textarea id="txtkeys" rows="3"></textarea>
            </dd>
            <div id="dgti" style="display: none">
                <dt><span id="dgti4"></span>
                    <div id="shichang" style="display: none">
                        <select id="ddlshichang" runat="server">
                        </select></div>
                    <div id="ShiJie1" style="display: none">
                        <select id="ddlShiJie1" runat="server">
                        </select></div>
                </dt>
                <input type="text" id="txtKeyWord1" onkeyup="findTopColors();" />
                <input type="button" value="添加" onclick="addtopitem();" style="margin-left:2px;" />
                <dt><span id="dgti5"></span></dt>
                <dd>
                    <textarea id="txtkeys1" rows="3"></textarea>
                </dd>
            </div>
            <dt><span>备注:</span></dt>
            <dd>
                <textarea id="txtare" rows="3"></textarea></dd>
            <dt><span>预警周期:</span>
                <select id="ddlyjdate" onchange="yjdate()">
                    <option value="1">每月</option>
                    <option value="12">每年</option>
                    <%--<option value="4">四周</option>--%>
                </select>
            </dt>
            <dt><span>预警状态:</span>
                <select id="ddlyjStatus">
                    <option value="1">启动</option>
                    <option value="2">停止</option>
                    <%--<option value="4">四周</option>--%>
                </select></dt>
            <dt><span>下次预警时间：</span><span id="yjdate"></span></dt>
        </dl>
    </div>
    <div id="yjhot" style="display: none">
        <table id="hot" class="easyui-datagrid" style="width: 300px; height: 200px;" data-options="singleSelect:true,collapsible:true">
        </table>
    </div>
    <script>

        $(document).ready(function () {
            $('#cc').combotree({
                url: 'getzhuantitree.aspx',
                required: true
            });
            $("#cc").combotree('tree').tree("collapseAll");
            $("input,img").niceTitle({ showLink: false }); //要排除一些例外的元素，例如可以用a:not([class='nono'])来排除calss为"nono"的a元素

        });
        $("#toptabs").tabs(0);
    </script>
    <asp:HiddenField ID="zhuantiID" runat="server" />
    <asp:HiddenField ID="hdzhiliang" runat="server" />
</asp:Content>
