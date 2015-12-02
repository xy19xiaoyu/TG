document.write("<script src=\"../js/AjaxDoPatSearch.js\"></script>");
document.write("<script src=\"../js/Export1.js\"></script>");
document.write("<script src=\"../js/page.js\"></script>");
document.write("<script src=\"../js/FormatHTML.js\"></script>");
document.write("<script src=\"../js/FormatTable.js\"></script>");
document.write("<script src=\"../js/errorTips.js\"></script>");
document.write("<script src=\"../js/AddToZT.js\"></script>");
document.write("<script src=\"../js/Cart.js\"></script>");
document.write("<script src=\"../js/RegImgZoom.js\"></script>");
document.write("<script src=\"../js/AddToCo.js\"></script>");
document.write("<script src=\"../js/right.js\"></script>");
var table;
$(document).ready(function () {
    //
    $('#showlist').hide();
    $('#divnodata').hide();
    $('#showspdes').hide();
    $('#divshowhelp').show();
    $('#divzt').bind('contextmenu', function (e) {
        e.preventDefault();
        $('#maddzt').menu('show', { left: e.pageX, top: e.pageY });
    });
    ShowTree();
    regPatentListSelectPage();
    $('#divtop .datagrid-header').remove();
    $("div#divtop [class='datagrid-wrap panel-body panel-body-noheader']").css("border-width", "0");
    ShowHotTop();
});


function ShowTree() {
    var strurl = "../comm/UserCollects.aspx";
    $('#CO').tree({
        url: strurl,
        onExpand: function () {
            addhover('CO');
        }, onLoadSuccess: function (node, data) {
            addhover('CO');
        }, onContextMenu: function (e, node) {
            e.preventDefault();
            $(this).tree('select', node.target);
            $('#mm').menu('show', { left: e.pageX, top: e.pageY });

        }
    });
}
//鼠标悬浮是添加中文+英文，离开移除
function addhover(treename) {

    var $divary = $("#" + treename).find("div.tree-node");
    //showMessage($divary.length);
    $divary.each(function (index) {

        var html = $divary.eq(index).html();
        var nodeid = $divary.eq(index).attr("node-id");
        var node = $("#" + treename).tree('find', nodeid);
        if (node) {
            //
            if (node.attributes) {
                var li = parseInt(node.attributes.live);

                //添加的元素
                addstr = '<span id="showlist' + treename + nodeid + '" style="display:none"><img id="showcn' + treename + nodeid + '" src="../Images/cn1.png" border="0" />&nbsp;<img id="showdocdb' + treename + nodeid + '" src="../Images/en1.png" border="0" /></span><span class="tree-title"';
                if ($("#showlist" + nodeid).length > 0) {

                }
                else {
                    html = html.replace('<span class="tree-title"', addstr);
                    html = html.replace('<SPAN class=tree-title', addstr);
                    //添加
                    $divary.eq(index).html(html);
                    //注册onclick事件
                    $("#showcn" + treename + nodeid).click(function () {
                        if (node.text == "未标引") {

                            show('cn', $("#" + treename).tree("getParent", node.target).id);
                        }
                        else {
                            show('cn', nodeid);
                        }

                    });
                    //注册onclick事件
                    $("#showdocdb" + treename + nodeid).click(function () {
                        if (node.text == "未标引") {
                            show('en', $("#" + treename).tree("getParent", node.target).id);
                        }
                        else {
                            show('en', nodeid);
                        }
                    });

                }


                //鼠标移上
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
                    var des = node.attributes.des;
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



function show(type, nodeid) {
    if ($('#hidtype').val() != type && $('#hidNodeId').val() != nodeid) {
        $('#hidCpicids').val(',');
    }
    $('#hidNodeId').val(nodeid);
    $('#hidtype').val(type);
    $("#hidQuery").val('');
    ShowTable(type, nodeid, 1, 10, '1', 'all')
}

function ShowTable(type, nodeid, pageindex, pagesize, showtype, filter) {
   
    $('#showlist').show();
    $('#divnodata').hide();
    $('#divshowhelp').hide();
    $("#showspdes").hide();
    $('#hidPageIndex').val(pageindex);
    $('#hidpagesize').val(pagesize);
    // 显示进度条
    showProcess();
    var Query = $("#hidQuery").val();
    $('#list').datagrid('loading')
    $.ajax({
        type: "POST",
        url: "../comm/UserCollects.aspx/GetPageList",
        data: "{'Type': '" + type + "', 'NodeId':'" + nodeid + "', 'pageindex':'" + pageindex + "', 'rows':'" + pagesize + "','filter':'" + filter + "','Query':'" + HTMLEncode(Query) + "'}",
        timeout: 30000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            showMessage("错误", msg);
            closeProcess()
            return;
        },
        success: function (msg) {
            //
            var data = $.parseJSON(msg.d);
            FormatTable(type, data, pageindex, pagesize, showtype, filter);

        }
    });
}

function FormatTable(type, data, pageindex, pagesize, showtype, filter) {
    
    table = new zlTable("divlist", type, data);
    table.ShowCOTable(showtype);
    if (data.total == "0" && $("#hidQuery").val() == "" && filter == "all") {
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
    if (showtype == "1") {
        $('.thumbnail').hide();
        $('.details').css("width", "750px");
        $('.notebase').css("width", "750px");
    }
    closeProcess();

}

function ShowPL(showtype) {
      if (table != null) {
          if (showtype == "0") {
              table.ShowCOTable(showtype);
              SetChecked();
          }
          else {
              $('.thumbnail').hide();
              $('.details').css("width", "750px");
              $('.notebase').css("width", "750px");
          }
    }
    else {
        ShowPatentList(showtype, '', '')
        $('.thumbnail').hide();
        $('.details').css("width", "750px");
        $('.notebase').css("width", "750px");
    }
    $('#hidShowType').val(showtype);
}
function ShowPatentList(showtype, pageindex, pagesize) {
    if (pageindex == "") {
        pageindex = $('#hidPageIndex').val();
    }
    if (showtype == "") {
        showtype = $('#hidShowType').val();
    }

    filter = $('#ftop').find("option:selected").val();

    if (pagesize == "") {
        pagesize = $('#hidpagesize').val();
    }
    var type = $('#hidtype').val();
    var nodeid = $('#hidNodeId').val();

    ShowTable(type, nodeid, pageindex, pagesize, showtype, filter);
}

function Filter(obj) {
    //
    var filter = $(obj).find("option:selected").val();
    var pagesize = $('#hidpagesize').val();
    var pageindex = $('#hidPageIndex').val();
    var type = $('#hidtype').val();
    var showtype = $('#hidShowType').val();
    var nodeid = $('#hidNodeId').val();
    ShowTable(type, nodeid, pageindex, pagesize, showtype, filter);
}


function ShowHotTop() {
    $('#tbhot').datagrid('loading')
    $.ajax({
        type: "POST",
        url: "../comm/UserCollects.aspx/GetHot",
        data: "{'top': '10'}",
        timeout: 30000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            //showMessage("错误", msg);
            return;
        },
        success: function (msg) {
            //
            var data = $.parseJSON(msg.d);
            var listview = $.extend({}, $.fn.datagrid.defaults.view, {
                renderRow: function (target, fields, frozen, rowIndex, rowData) {
                    var cc = [];
                    //
                    if (!frozen) {
                        cc.push('<td class="hot">');
                        debugger;
                        cc.push(FormatCoTitle(rowData.type, rowData.title, rowData.Number, rowData.zhuanLiLeiXing, rowData.count, rowData.Pid));
                        cc.push('</td>'); ;
                    }


                    return cc.join('');
                }
            });
            $('#tbhot').datagrid({
                view: listview
            });
            $('#tbhot').datagrid({
                onLoadSuccess: function (data) {
                    if (data.total == 0) {
                        //给用户一个没有数据的提示 
                    }
                    else {

                        $('#divtop .datagrid-header').remove();
                        $("div#divtop [class='datagrid-wrap panel-body panel-body-noheader']").css("border-width", "0");
                    }
                }
            });
            $('#tbhot').datagrid('loadData', { "total": data.total, "rows": data.rows });
        }
    });
}

function FormatCoTitle(type, strTitle, strANX, zhuanLiLeiXing, count, cpic) {
    debugger;
    var strReturn = "";
    if (type.toUpperCase() == "CN") {
        if (zhuanLiLeiXing == "3")//外观设计专利
        {
            strReturn = "<a href=\"../my/frmPatDetails.aspx?Id=" + strANX + "&xy=" + cpic + "\" target=\"_blank\">" + strTitle + "<span>" + count + "</span></a>";
        }
        else {
            strReturn = "<a href=\"../my/frmPatDetails.aspx?Id=" + strANX + "&xy=" + cpic + "\" target=\"_blank\">" + strTitle + "<span>" + count + "</span></a>";
        }

    }
    else {
        strReturn = "<a href=\"../my/EnPatentDetails.aspx?Id=" + strANX + "&xy=" + cpic + "\" target=\"_blank\">" + strTitle + "<span>" + count + "</span></a>";
    }
    return strReturn;
}
function SearchNote() {
    //
    var Query = $('#TextBoxKeyword').val();
    $("#hidQuery").val(Query);
    ShowPatentList("", "1", "");
}

function DelCo() {

    var obj = arguments[0];
    var NodeId = $('#hidNodeId').val();
    var CpicIds = "";
    if (obj == null) {
        CpicIds = $('#hidCpicids').val();
    }
    else {
        CpicIds = obj;
    }
    art.dialog({
        title: '确认删除',
        content: '<img src="../js/artDialog/skins/icons/warning.png"/>您确定取消收藏所选数据吗？',
        button: [
          {
              value: '取消',
              callback: function () { }

          },
            {
                value: '确定',
                callback: function () {
                    $.ajax({
                        type: "POST",
                        url: "../comm/UserCollects.aspx/DelToCO",
                        data: "{'pids':'" + CpicIds + "','nodids':'" + NodeId + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var res = msg.d;
                            if (res == "succ") {
                                //
                                var ids = CpicIds.split(',');
                                for (var i = 0; i < ids.length; i++) {
                                    $('#hidCpicids').val($('#hidCpicids').val().toString().replace(',' + ids[i] + ',', ','));
                                    if ($('#td' + ids[i]).length > 0) {
                                        $('#td' + ids[i]).remove();
                                        //删除JS数据；

                                        for (var j = 0; j < table.data.rows.length; j++) {
                                            if (table.data.rows[j].CPIC == ids[i]) {
                                                if (j == 0) {
                                                    table.data.rows.shift();
                                                }
                                                else {
                                                    table.data.rows.splice(j - 1, 1);
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                showMessage("取消收藏成功");
                            } else {
                                showMessage("取消收藏失败");
                            }

                        }
                    });
                },
                focus: true
            }
            ]

    });
}

function EditCo(pid) {
    $('#txtNote').val($('#Note' + pid).html());
    art.dialog({
        title: '修改标注',
        content: document.getElementById('EditNote'),
        lock: true,
        id: 'dgEditNote',
        button: [
                {
                    value: '确定',
                    callback: function () {

                        var nodeid = $('#hidNodeId').val();
                        var note = $('#txtNote').val();
                        if (note.trim() == "") {
                            showMessage("请输入标注内容！")
                            return false;
                        }
                        note = HTMLEncode(note);

                        var myDialog = showProcess();
                        $.ajax({
                            type: "POST",
                            url: "../comm/UserCollects.aspx/EditNote",
                            data: "{'nodeid':'" + nodeid + "','pid':'" + pid + "','note':'" + decodeURIComponent(note) + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {

                                var res = msg.d;
                                if (res == "succ") {
                                    $('#Note' + pid).html(HTMLEncode(note));
                                    $('#NoteDate' + pid).html(getdate);
                                    myDialog.close();
                                    art.dialog.get('dgEditNote').close();
                                } else {
                                    showMessage("修改失败！");
                                }
                                myDialog.close();
                            }
                        });
                    },
                    focus: false
                }
            ]
    });
}

function getdate() {
    var date = new Date(); //日期对象
    var now = "";
    now = date.getFullYear() + "-"; //读英文就行了
    now = now + (date.getMonth() + 1) + "-"; //取月的时候取的是当前月-1如果想取当前月+1就可以了
    now = now + date.getDate() + " ";
    now = now + date.getHours() + ":";
    now = now + date.getMinutes() + ":";
    now = now + date.getSeconds() + "";
    return now;
}
function GOTOST() {
    var db = $("#hidtype").val();
    var type  = "CO";
    var id = $('#hidNodeId').val();
    window.open("../comm/autost.aspx?type=" + type + "&db=" + db + "&id=" + id);
}