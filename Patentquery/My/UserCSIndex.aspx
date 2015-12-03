<%@ Page Title="" Language="C#" MasterPageFile="~/Master/index.master" AutoEventWireup="true"
    CodeBehind="UserCSIndex.aspx.cs" Inherits=" Patentquery.My.UserCSIndex" %>

<asp:Content ID="he1" ContentPlaceHolderID="head" runat="server">
    <script src="../Js/bootstrap.min.js" type="text/javascript"></script>
    <link href="../Js/artDialog/skins/blue.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <link href="../Css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../js/CSIndex.js" type="text/javascript"></script>
    <script src="../Js/HotTop.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/datagrid-detailview.js" type="text/javascript"></script>
    <style>
        .panel
        {
            margin-bottom: 0px;
            overflow: hidden;
            font-size: 12px;
            text-align: left;
        }
        .panel-title
        {
            font-size: 12px;
            font-weight: bold;
            color: #0E2D5F;
            height: 16px;
            line-height: 16px;
        }
        .panel-header
        {
            padding: 5px;
            position: relative;
        }
        .panel-body
        {
            padding: 0px;
        }
        ul
        {
            padding: 0px;
        }
        ul li
        {
            list-style-type: none;
            padding: 2px;
        }
        .addon_width
        {
            width: 80px;
        }
       
        
        .form-control
        {
            height: 20px;
        }
        .datagrid-row-selected .datagrid-cell a
        {
            color: White;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="left" style="width: 225px;">
        <div id="pinlieft">
            <div id="left_title " class="left_ti" style="text-align: center;">
                &nbsp;<span>用户中心</span>&nbsp;
            </div>
            <div class="left_content2" style="padding: 0px; width: 220px; min-height: 505px">
                <div id="leftmue" class="easyui-accordion " style="width: 220px; height: 505px;">
                   <div title=">>>系统后台管理" data-options="selected:false" url="../sysadmin/"></div>
                    <div title="个人资料" data-options="selected:false" url="EditUser.aspx"></div>
                    <div title="检索式管理" data-options="selected:false" url="frmQueryMag.aspx"></div>
                    <div title="标引管理" data-options="selected:false" url="UserCSIndex.aspx"></div>
                    <div title="我的收藏夹" data-options="selected:false" url="frmCollectList.aspx" style="overflow: auto; padding: 10px; min-height: 250px;">
                        <div id="divzt" style="min-height: 250px">
                            <ul id="CO" class="easyui-tree" data-options="lines:true" />
                        </div>
                    </div>
                    <div title="热门收藏" data-options="selected:true" url="" style="overflow: inherit; min-height: 250px;">
                        <div id="divtop" style="min-height: 250px; width: 202px; padding: 10px;">
                            <table id="tbhot" class="easyui-datagrid" style="width: 210px; overflow: visible"
                                data-options="singleSelect:true,collapsible:true">
                                <thead>
                                    <tr>
                                        <th data-options="field:'Title',width:210"></th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="right" style="width: 768px; min-height: 550px;">
        <div style="padding: 0 5px 0px 5px;">
            <table id="CSIndexs" class="easyui-datagrid" style="width: 760px; height: 540px;
                overflow: visible" data-options="singleSelect:true,collapsible:true,toolbar:'#tb',rownumbers:true">
                <thead>
                    <tr>
                        <th data-options="field:'itemname',width:160">
                            标引项
                        </th>
                        <th data-options="field:'itemvalues',width:445">
                            标引值
                        </th>
                        <th data-options="field:'_operate',width:120,align:'center',formatter:formatOper">
                            操作
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
        <div id="tb" style="padding: 5px; height: auto">
            <div class="btn-group" role="group" aria-label="...">
                <button type="button" class="btn btn-default btn_Add_CSIndex">
                    添加标引项</button>
                <button type="button" class="btn btn-default btn_edit_CSIndex">
                    编辑标引项</button>
                <button type="button" class="btn btn-default btn_Del_CSIndex">
                    删除标引项</button>
                <div class="input-group">
                    <span class="input-group-addon">标引项</span>
                    <input id="itemname" type="text" class="form-control" placeholder="请输入搜索条件" />
                    <span class="input-group-btn">
                        <button id="btnSearch" class="btn btn-default" type="button">
                            搜索</button>
                    </span>
                </div>
            </div>
        </div>
        <div id="dlgCSIndex" class="dlg_Role" style="display: none;">
            <ul>
                <li>
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon addon_right addon_width" style="font-weight: bold">标引项</span>
                        <input type="text" id="txtitemname" class="form-control form-control_width" placeholder="标引项"
                            style="width: 375px;" />
                    </div>
                </li>
                <li>
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon addon_right addon_width">标引值1</span>
                        <input type="text" id="txtvalue1" class="form-control form-control_width" placeholder="标引值1"
                            style="width: 375px;" />
                    </div>
                </li>
                <li>
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon addon_right addon_width">标引值2</span>
                        <input type="text" id="txtvalue2" class="form-control form-control_width" placeholder="标引值2"
                            style="width: 375px;" />
                    </div>
                </li>
                <li>
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon addon_right addon_width">标引值3</span>
                        <input type="text" id="txtvalue3" class="form-control form-control_width" placeholder="标引值3"
                            style="width: 375px;" />
                    </div>
                </li>
                <li>
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon addon_right addon_width">标引值4</span>
                        <input type="text" id="txtvalue4" class="form-control form-control_width" placeholder="标引值4"
                            style="width: 375px;" />
                    </div>
                </li>
                <li>
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon addon_right addon_width">标引值5</span>
                        <input type="text" id="txtvalue5" class="form-control form-control_width" placeholder="标引值5"
                            style="width: 375px;" />
                    </div>
                </li>
            </ul>
        </div>
        <!-- 右键菜单 结束-->
        <div id="meuCSIndex" class="easyui-menu" style="width: 120px;">
            <div class="btn_edit_CSIndex" data-options="iconCls:'icon-add'">
                编辑标引项
            </div>
            <div class="btn_Del_CSIndex" data-options="iconCls:'icon-remove'">
                删除标引项</div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $(".accordion-header").each(function () {

                var url = $(this).parent().find("div.accordion-body").attr("url");
                if (url == null || url == "" || url == "undefined") {
                    return;
                }
                //如果有Url  panel不展开，取消Musedown 时间注册 
                $(this).parent().unbind();
                $(this).parent().bind("mousedown", function (e) {
                    console.log(e.button);
                    if (e.button == 0) {

                        var url = $(this).find("div.accordion-body").attr("url");
                        if (url == null || url == "" || url == "undefined") {
                            return;
                        }
                        $(this).parent().unbind();
                        location.href = url;
                    }
                });
            });
        });
    </script>
</asp:Content>
