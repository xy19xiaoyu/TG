<%@ Page Title="" Language="C#" MasterPageFile="~/Master/index.master" AutoEventWireup="true"
    CodeBehind="frmCollectList.aspx.cs" Inherits="Patentquery.My.frmCollectList" %>

<asp:Content ID="he1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <link href="../css/themes/default/default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/UserCollect.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="left" style="width: 225px;">
        <div id="pinlieft">
            <div id="left_title " class="left_ti" style="text-align: center;">
                &nbsp;<span>用户中心</span>&nbsp;
            </div>
            <div class="left_content2" style="padding: 0px; width: 220px; min-height: 505px">
                <div id="leftmue" class="easyui-accordion " style="width: 220px; height: 505px;">
                    <div title=">>>系统后台管理" url="../sysadmin/"></div>
                    <div title="个人资料" url="EditUser.aspx"></div>
                    <div title="检索式管理" url="frmQueryMag.aspx"></div>
                    <div title="标引管理" url="UserCSIndex.aspx"></div>
                    <div title="我的收藏夹" url="" data-options="selected:true" style="overflow: auto; padding: 10px; min-height: 250px;">
                        <div id="divzt" style="min-height: 250px">
                            <ul id="CO" class="easyui-tree" data-options="lines:true" />
                        </div>
                    </div>
                    <div title="热门收藏" url="" style="overflow: inherit; min-height: 250px;">
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
    <div id="right" style="width: 768px; min-height: 542px;">
        <div id="showlist">
            <div class="spbutton" id="divSSC" style="padding: 5px 5px 0px 5px;">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="vertical-align: middle">
                            <select id="ftop" onchange="Filter(this)" style="*margin-top: 10px;">
                                <option value="all">全部</option>
                                <option value="yes">标注</option>
                                <option value="no">未标注</option>
                            </select>
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        <td style="vertical-align: middle">
                            <span style="font-size: 18px">标注内容&nbsp;&nbsp;</span>
                        </td>
                        <td style="vertical-align: middle">
                            <input name="TextBoxKeyword" type="text" id="TextBoxKeyword" class="smallTextBox">
                        </td>
                        <td style="vertical-align: middle">
                            <span class="button" onclick="SearchNote()">搜索</span>
                        </td>
                    </tr>
                </table>
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
                <input type="checkbox" onclick="SelectAll(this)" />
                全选 <a href="javascript:void(0);" onclick="DelCo()">
                    <img src='/Images/smallquxiaoshou_bj.PNG' alt='取消收藏' />取消收藏</a> <a id="PLXZ" href="javascript:void(0);"
                        onclick="showExportCFG()">
                        <img src='/Images/bigdaochu_bj.png' alt='' />导出</a> <a href="javascript:void(0);"
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
        <div id="divshowhelp" style="display: none;">
            <div class="boxInner" style="min-height: 522px; padding: 10px">
                <ul>
                    <li>1.请点击右边<b>我的收藏夹</b>下边的方框中<b>收藏目录</b>或者<b>空白处</b>管理您的收藏夹。</li>
                    <li>2.选择收藏夹的节点，点击节点的<b>中文</b>或<b>英文</b>浏览您收藏的专利，点击<b>节点名称</b>显示节点的简介。</li>
                </ul>
            </div>
        </div>
        <div class="box" id="divnodata" style="display: none">
            <div class="boxInner" style="min-height: 522px; padding: 10px">
                <ul>
                    <li>此节点下您还没有收藏专利！请从专利检索结果的列表中页面或者专利详细信息页面进行收藏专利！<br />
                        现在就去<a href="SmartQuery.aspx" target="_blank" style="color: Red"><b>检索</b></a></li>
                </ul>
            </div>
        </div>
        <div id="showspdes" style="display: none">
            <div id="spdes" class="boxInner" style="height: 522px; padding: 10px">
            </div>
        </div>
    </div>
    <!-- 右键菜单 开始-->
    <div id="mm" class="easyui-menu" style="width: 120px;">
        <div onclick="appendco('CO','添加收藏夹')" data-options="iconCls:'icon-add'">
            添加收藏夹
        </div>
        <div onclick="editco('CO','修改收藏夹')" data-options="iconCls:'icon-edit'">
            修改收藏夹
        </div>
        <div class="menu-sep">
        </div>
        <div onclick="RemoveNodeco('CO') " data-options="iconCls:'icon-remove'">
            删除
        </div>
    </div>
    <div id="maddzt" class="easyui-menu" style="width: 120px;">
        <div onclick="appendco('CO','添加收藏夹 ')" data-options="iconCls:'icon-add'">
            添加收藏夹
        </div>
    </div>
    <div id="DivAddNode" style="min-width: 400px; display: none;">
        <span id="dialogtitle" style="display: none"></span>
        <table>
            <tr>
                <th>收藏夹名称:
                </th>
                <td>
                    <input type="text" id="newclass" name="newclass" style="width: 280px" /><br />
                    <span id="showmsg" style="display: none; color: Red;">*请输入收藏夹名称</span>
                </td>
            </tr>
            <tr>
                <th>简介：
                </th>
                <td>
                    <textarea id="txtNodeDes" cols="200" name="txtNodeDes" rows="40" style="width: 280px; height: 100px"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <!-- 右键菜单 结束-->
    <!--修改标注-->
    <div id="EditNote" style="min-width: 400px; display: none;">
        <table>
            <tr>
                <th>标注：
                </th>
                <td>
                    <textarea id="txtNote" cols="200" name="txtNodeDes" rows="40" style="width: 340px; height: 100px"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <!-- 修改标注结束 -->
    <!-- 同类专利-->
    <div id="sm" style="width: 520px; height: 300px; display: none;">
        <iframe id="ism" name="ism" scrolling="no" frameborder="0" src="" style="width: 100%; height: 100%;"></iframe>
    </div>
    <!-- 同类专利-->
    <div id="cart">
        <div class="header">
            专利对比
        </div>
        <div id="cartItems" class="body">
        </div>
        <div class="body center">
            <span class="btnSmall" onclick="clearCart()">清空</span> <span class="btnSmall" onclick="closeCart()">关闭</span> <span class="btnSmall" onclick="submitCart()">确定</span>
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
    <iframe id="ifile" name="ifile" frameborder="0" src="" style="width: 0; height: 0;"></iframe>
    <input type="hidden" id="tname" name="tname" value="zt" />
    <input type="hidden" id="hidthid" name="hidthid" value="1" />
    <input type="hidden" id="hidNodeId" name="hidNodeId" value="1" />
    <input type="hidden" id="hidNodeName" name="hidNodeName" value="1" />
    <input type="hidden" id="hidCpicids" name="hidCpicids" value="," />
    <input type="hidden" id="hidtype" name="hidtype" value="1" />
    <input type="hidden" id="hidPageIndex" name="hidPageIndex" value="1" />
    <input type="hidden" id="hidSourceType" name="hidSourceType" value="db" />
    <input type="hidden" id="hidShowType" name="hidShowType" value="all" />
    <input type="hidden" id="hidpagesize" name="hidpagesize" value="10" />
    <input type="hidden" id="hidQuery" name="hidQuery" value="" />
    <asp:HiddenField ID="rightlist" runat="server" ClientIDMode="Static" Value="," />
    <asp:HiddenField ID="yonghuleixing" runat="server" ClientIDMode="Static" Value="个人" />

    <script type="text/javascript">
        $(function () {
            setTimeout(initleftmenu, 500);
        });
        function initleftmenu() {
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
        }
    </script>
</asp:Content>
