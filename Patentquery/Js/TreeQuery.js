document.write("<script src=\"../js/AjaxDoPatSearch.js\"></script>");
document.write("<script src=\"../js/Export.js\"></script>");
document.write("<script src=\"../js/page.js\"></script>");
document.write("<script src=\"../js/FormatHTML.js\"></script>");
document.write("<script src=\"../js/FormatTable.js\"></script>");
document.write("<script src=\"../js/AddToCO.js\"></script>");
document.write("<script src=\"../js/AddToZT.js\"></script>");
document.write("<script src=\"../js/Cart.js\"></script>");
document.write("<script src=\"../js/CheckSPCN.js\"></script>");
document.write("<script src=\"../js/CheckSPEN.js\"></script>");
document.write("<script src=\"../js/RegImgZoom.js\"></script>");
document.write("<script src=\"../js/right.js\"></script>");

var table;
$(document).ready(function () {

    ShowTree("IPC");
    regPatentListSelectPage();
});

function ShowTree(type) {
    //
    if (type == null) {
        type = $("#selectTree").find("option:selected").val();
    }
    $('#showlist').hide();
    $('#divnodata').hide();
    $('#showspdes').hide();
    $('#divshowhelp').show();

    $('#divIPC').hide();
    $('#divADM').hide();
    $('#divARE').hide();
    $('#div' + type).show();

    var strurl = "../comm/sysTreeNodes.aspx?clstype=" + type;
    $('#' + type).tree({
        url: strurl,
        onExpand: function () {
            addhover(type);
        }, onLoadSuccess: function (node, data) {
            addhover(type);
        }
    });
}

//鼠标悬浮是添加中文+英文，离开移除
function addhover(treename) {

    var $divary = $("#" + treename).find("div.tree-node");
    //alert($divary.length);
    $divary.each(function (index) {

        var html = $divary.eq(index).html();
        var nodeid = $divary.eq(index).attr("node-id");
        var node = $("#" + treename).tree('find', nodeid);
        if (node) {
            //
            if (node.attributes) {
                var li = parseInt(node.attributes.live);

                //添加的元素
                var addstr;
                if (treename != "ADM") {
                    addstr = '<span id="showlist' + treename + nodeid + '" style="display:none"><img id="showcn' + treename + nodeid + '" src="../Images/cn1.png" border="0" />&nbsp;<img id="showdocdb' + treename + nodeid + '" src="../Images/en1.png" border="0" /></span><span class="tree-title"';
                    //addstr = '<span id="showlist' + treename + nodeid + '" style="display:none;height:18px;"><img src="../Images/cn1.png" /><a href="javascript:void(0);" id="showcn' + treename + nodeid + '" class="cn">&nbsp;&nbsp;</a>&nbsp;<a href="javascript:void(0);" id="showdocdb' + treename + nodeid + '"  class="en">&nbsp;&nbsp;</a></span><span class="tree-title"';
                }
                else {
                    addstr = '<span id="showlist' + treename + nodeid + '" style="display:none"><a href="javascript:void(0);" id="showcn' + treename + nodeid + '" class="cn">&nbsp;&nbsp;</a></span><span class="tree-title"';
                }
                if ($("#showlist" + treename + nodeid).length > 0) {

                }
                else {
                    if (node.attributes.des != "" && node.attributes.live != "0") {
                        html = html.replace('<span class="tree-title"', addstr);
                        html = html.replace('<SPAN class=tree-title', addstr);
                        //添加
                        $divary.eq(index).html(html);
                        //注册onclick事件
                        $("#showcn" + treename + nodeid).unbind();
                        $("#showcn" + treename + nodeid).click(function () {
                            show('cn', node.attributes.des);
                        });
                        //注册onclick事件
                        $("#showdocdb" + treename + nodeid).unbind();
                        $("#showdocdb" + treename + nodeid).click(function () {
                            show('en', node.attributes.des);
                        });

                        //鼠标移上
                        $divary.eq(index).unbind();
                        $divary.eq(index).mouseenter(function () {
                            var nodeid = $divary.eq(index).attr("node-id");
                            $("#showlist" + treename + nodeid).show();
                        });
                        //鼠标移走
                        $divary.eq(index).mouseleave(function () {
                            var nodeid = $divary.eq(index).attr("node-id");
                            $("#showlist" + treename + nodeid).hide();
                        });
                    }

                }


            }

            //}
        }

    });
    //为原来的Nodename添加click事件
    var $titleary = $("#" + treename).find("span.tree-title");
    $titleary.each(function (index) {
        $titleary.eq(index).click(function () {
            var nodeid = $titleary.eq(index).parent().attr("node-id");
            var node = $("#" + treename).tree('find', nodeid);
            if (node) {
                //
                if (node.attributes) {
                    $('#showlist').hide();
                    $('#divnodata').hide();
                    $('#divshowhelp').hide();
                    $("#showspdes").show();
                    var des = node.text;
                    if (des == "") {
                        des = "<b>暂无简介...</b>";
                    }
                    else {
                        des = "<b>简介</b>：</br>" + des
                    }
                    $("#spdes").html(des);
                }
            }
        });
    });
}

function show(type, ipcs) {
    ipcs = ipcs.replace("-", "").replace("-", "");
    var ary = ipcs.split(";");
    var QueryContent = "";
    for (var i = 0; i < ary.length; i++) {
        var tmpipc = ary[i];
        if (tmpipc.trim() == "") continue;
        QueryContent = QueryContent + "(" + formatipc(type, tmpipc) + "/IC)+";
    }
    var query = "F XX " + QueryContent.substring(0, QueryContent.length - 1);
    //
    DoIPCSearch("", query, type, "0", "");
}
function formatipc(type, ipc) {
    if (type.toUpperCase() == "CN") return ipc;
    var split = ipc.split('/');
    if (split.length == 2)//如果IPC中包含/
    {
        var start4 = split[0].substr(0, 4); //开始4位不变
        var mid3 = split[0].substring(4).Trim().replace(/0/g, '');
        var last4 = split[1].Trim();
        return start4 + mid3 + "/" + last4;
    }
    else {
        return ipc;
    }
}
function DoIPCSearch(strkw, strQuery, dbType, _searchSrc, errTip) {
    //
    dbType = dbType.toUpperCase();
    var dg = showProcess();
    $.ajax({
        type: "POST",
        url: "/my/SmartQuery.aspx/DoPatSearch",
        data: "{'strSearchQuery':'" + encodeURIComponent(strQuery) + "', '_strSdbType':'" + dbType + "','_sDoSrc':'" + _searchSrc + "'}",
        timeout: 30000,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //
            // 撤销进度条
            dg.close();
            //$("#masklayer").css("display", "none");
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "检索超时，请稍后再试！"
            }
            showError(msg);
            return;
        },
        success: function (msg) {
            //
            strMsg = msg.d;
            if (strMsg[0].indexOf("<hit") != -1) {
                var num = strMsg[2]; // 检索结果数
                var sNo = strMsg[1];

                if ((num == "0" || num == 0) && _searchSrc != "2") {
                    // 撤销进度条
                    dg.close();
                    zeroResultError(dbType);
                    return;
                }
                $("#hidQuery").val(strQuery);
                $('#hidtype').val(dbType);
                $('#hiditemcount').val(num);
                $('#hidSearchNo').val(sNo);
                var tb = art.dialog.get('DGTableSearch');
                if (tb != null) tb.close();
                ShowPatentList("", 1, 10, "");
            }
            else {
                // 撤销进度条
                dg.close();
                showError(strMsg);
            }

        }
    });
}
function ShowPL(showtype) {
    if (table != null) {
        if (showtype == "0") {
            table.ShowTreeQueryTable(showtype);
            SetChecked();
            $('#hidShowType').val(showtype);
            $('.details').css("width", "580px");
            $('.notebase').css("width", "580px");
        }
        else {
            $('.thumbnail').hide();
            $('.details').css("width", "750px");
            $('.notebase').css("width", "750px");
        }
    }
    else {
        ShowPatentList(showtype, '', '', '')
        $('.thumbnail').show();
        $('.details').css("width", "580px");
        $('.notebase').css("width", "580px");
    }
    
}
function ShowPatentList(showtype, pageindex, pagesize, sort) {
    //
    var db = $('#hidtype').val();
    var itemcount = $('#hiditemcount').val();
    var SearchNo = $('#hidSearchNo').val();

    if (pageindex == "") {
        pageindex = $('#hidPageIndex').val();
    }
    if (showtype == "") {
        showtype = $('#hidShowType').val();
    }
    if (pagesize == "") {
        pagesize = $('#hidpagesize').val();
    }
    if (sort == "") {
        //sort = $('#sorttop').find("option:selected").val();
    }
    // $('#sorttop').val(sort);
    // $('#sortbom').val(sort);
    ShowTable(db, SearchNo, 'FI', pageindex, pagesize, itemcount, showtype, sort);
}
function ShowTable(type, nodeid, sourcetype, pageindex, pagesize, itemcount, showtype, sort) {
    //
    $('#showlist').show();
    $('#divnodata').hide();
    $('#divshowhelp').hide();
    $("#showspdes").hide();
    // 显示进度条
    showProcess();
    $('#list').datagrid('loading')
    $.ajax({
        type: "POST",
        url: "../comm/GetList.aspx/GetPageList",
        data: "{'Type': '" + type + "', 'NodeId':'" + nodeid + "', 'SourceType': '" + sourcetype + "', 'ItemCount':'" + itemcount + "', 'pageindex':'" + pageindex + "', 'rows':'" + pagesize + "','Sort':'" + sort + "'}",
        timeout: 30000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "请求数据超时，请稍后再试！";
            }
            showMessage("错误", msg);
            closeProcess();
            return;
        },
        success: function (msg) {
            $('#hidPageIndex').val(pageindex);
            $('#hidpagesize').val(pagesize);
            $('#hidShowType').val(showtype);
            $('#hidItemCount').val(itemcount);
            var data = $.parseJSON(msg.d);
            FormatTable(type, data, pageindex, pagesize, showtype);
        }
    });
}
function FormatTable(type, data, pageindex, pagesize, showtype) {
    table = new zlTable("divlist", type, data);
    table.ShowTreeQueryTable(showtype);
    if (data.total == 0) {
        //给用户一个没有数据的提示  
        $('#showlist').hide();
        $('#divnodata').show();
        $('#divshowhelp').hide();
        $("#showspdes").hide();
    }
    else {
        SetPage(data.total, pagesize, pageindex);
        SetChecked();
        $('#left').css('height', $('#divlist').css('height'));
        $("#pinlieft").pin();
    }
    closeProcess();
}
//展示表格检索
function ShowTabSearch(title) {

    var type = $('#hidtype').val();
    if (type.toUpperCase() == 'CN') {
        $('#endivQueryTable').hide();
        $('#cndivQueryTable').show();
        cnClearSearch();

    }
    else {
        $('#cndivQueryTable').hide();
        $('#endivQueryTable').show();
        docdbClearSearch();
    }
    TableSearchDialog(title);
    $("#DseachTitle").html(title);
}

//SSearch

function SSearch() {
    //
    var type = $('#hidtype').val();
    var Query = $("#hidQuery").val();
    var title = $('#DseachTitle').html();

    setViewResult(" "); // 清空验证域
    var strQuery;
    strQuery = $("#TxtSearch").val();
    if (type.toUpperCase() == "CN") {
        if (strQuery == "") {
            strQuery = getTableSearchQueryCN().Trim();
        }
        var pattern = validateLogicSearchQueryCN(strQuery);
        pattern = pattern.Trim();
        if (pattern.substring(0, 5) == "Error") {
            showError(pattern);
            return;
        }
        else {
            $("#TxtSearch").val(pattern);
            strQuery = pattern;
        }
        if (pattern == "") {
            alert("请输入检索条件");
            return;
        }

    }
    else {
        if (strQuery == "") {
            strQuery = getTableSearchQueryEN().Trim();
        }
        var pattern = validateLogicSearchQueryEN(strQuery);
        pattern = pattern.Trim();
        if (pattern.substring(0, 5) == "Error") {
            showError(pattern);
            return;
        }
        else {
            $("#TxtSearch").val(pattern);
            strQuery = pattern;
        }
        if (pattern == "") {
            alert("请输入检索条件");
            return;
        }
    }
    var newquery = "";
    var operator = "*";
    switch (title) {
        case "重新检索":
            operator = "";
            break;
        case "过滤检索":
            operator = "-";
            break;
        case "二次检索":
            operator = "*";
            break;
    }
    //
    if (operator != "") {
        if (strQuery.indexOf("F XX ") >= 0) {
            strQuery = strQuery.replace("F YY ", "").replace("F XX ", "");
        }
        var patternUrl = $("#hidQuery").val();
        patternUrl = decodeURIComponent(patternUrl).Trim();
        patternUrl = "(" + patternUrl.replace("F YY ", "").replace("F XX ", "").replace("#", "") + ")";
        strQuery = "(" + strQuery.replace("F YY ", "").replace("F XX ", "") + ")";
        newquery = "F XX " + patternUrl + operator + strQuery.replace(/\+/g, "@");

    } else {
        newquery = strQuery;
    }
    $("#TxtSearch").val('');
    DoIPCSearch("", newquery, type.toUpperCase(), "0", "")

}
function GOTOST() {
    var db = $("#hidtype").val();
    var type = db;
    var id = $('#hidSearchNo').val();
    window.open("../comm/autost.aspx?type=" + type + "&db=" + db + "&id=" + id);
}