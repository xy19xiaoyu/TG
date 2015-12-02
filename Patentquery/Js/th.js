document.write("<script src=\"../js/FormatHTML.js\"></script>");
document.write("<script src=\"../js/FormatTable.js\"></script>");
document.write("<script src=\"../js/Export.js\"></script>");
document.write("<script src=\"../js/AjaxDoPatSearch.js\"></script>");
document.write("<script src=\"../js/page.js\"></script>");
document.write("<script src=\"../js/AddToCO.js\"></script>");
document.write("<script src=\"../js/thsp.js\"></script>");
document.write("<script src=\"../js/thtree.js\"></script>");
document.write("<script src=\"../js/Cart.js\"></script>");
document.write("<script src=\"../js/CheckSPCN.js\"></script>");
document.write("<script src=\"../js/CheckSPEN.js\"></script>");
document.write("<script src=\"../js/RegImgZoom.js\"></script>");
document.write("<script src=\"../js/right.js\"></script>");
var table;
var isSSearch;
//二次检索
//参数1.检索式sp, 节点id nodeid
function SSearch(title, type) {

    var strQuery = "";
    var nodeid = $('#hidNodeId').val();   
    getztsp();
    strQuery = $("#TxtSearch").val();
    if (strQuery == "") return;

    var stype = "1";
    if ($("#DseachTitle").html() == "二次检索") {
        stype = 1;
    }
    else {
        stype = 2;
    }
    showProcess();

    $.ajax({
        type: "POST",
        url: "../comm/SSearch.aspx/tableSearch",
        data: "{'strSearchQuery':'" + strQuery + "','NodeId':'" + nodeid + "','type':'" + type + "','stype':'" + stype + "'}",
        timeout: 30000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "检索超时，请稍后再试！";
            }
            closeProcess();
            showMessage("错误", msg);
            return;
        },
        success: function (msg) {

            if (msg.d == "请求错误") {
                closeProcess();
                showMessage("请求错误");
                return;
            }
            else if (msg.d != "failed") {
                if (msg.d == "0") {
                    closeProcess();
                    showMessage("检索结果为零");
                    return;
                }
                closeProcess('DGtabSearch');
                var showtype = $('#hidShowType').val();
                ShowTable(type, "998", "FI", "1", $('#hidpagesize').val(), msg.d, showtype, strSort);
            }
            else {
                closeProcess();
                showMessage(msg.d);
            }
        }
    });
}
function ShowPL(showtype) {
    if (table != null) {
        if (showtype == "0") {
            table.ShowZTTable(showtype);
            SetChecked();
            $('#hidShowType').val(showtype);
            readystars();
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
        ShowPatentList(showtype, '', '')
        $('.thumbnail').show();
        $('.details').css("width", "580px");
        $('.notebase').css("width", "580px");
    }
    $('#hidShowType').val(showtype);

}
function ShowPatentList(showtype, pageindex, pagesize) {

    var type = $('#hidtype').val();
    var nodeid = $('#hidNodeId').val();
    var sourcetype = $('#hidSourceType').val();
    if (pageindex == "") {
        pageindex = $('#hidPageIndex').val();
    }

    if (pagesize == "") {
        pagesize = $('#hidpagesize').val();
    }
    if (showtype == "") {
        showtype = $('#hidShowType').val();
    }
    var itemcount = $('#hidItemCount').val(); ;

    ShowTable(type, nodeid, sourcetype, pageindex, pagesize, itemcount, showtype, strSort);
}



//显示移动到树 type = zt|qy
function ShowMoveToZT(id) {
    var type = requestUrl('type');
    if (type == "") type = "QY";
    var ztid = $('#hidthid').val();
    $('#setZT').tree({
        url: '../comm/getNodes.aspx?clstype=' + type + '&ztid=' + ztid + '&fileter=true'
    });
    MoveZTDialog('SetZT', '设置分类', id);
}
function ShowBatchMoveToZT() {
    if ($('#hidCpicids').val() == ',') {
        showMessage('提示', '请选择专利');
        return;
    }
    else {
        id = $('#hidCpicids').val();
        var type = requestUrl('type');
        if (type == "") type = "QY";
        var ztid = $('#hidthid').val();
        $('#setZT').tree({
            url: '../comm/getNodes.aspx?clstype=' + type + '&ztid=' + ztid + '&fileter=true'
        });
        MoveZTDialog('SetZT', '设置分类', id);
    }
}

//点确定移动
function MoveToZT(id) {

    var type = $('#hidtype').val();
    var OldNodeId = $('#hidNodeId').val();
    var CpicIds;
    if (id == null) {
        CpicIds = $('#hidCpicids').val();
    }
    else {
        CpicIds = id;
    }
    var NewNodeIds = ",";

    var nodes = $('#setZT').tree('getChecked');
    if (nodes.length <= 0) {
        showMessage("选择分类");
        return;
    }
    for (var i = 0; i < nodes.length; i++) {
        if (NewNodeIds != '') NewNodeIds += ',';
        NewNodeIds += nodes[i].attributes.Nid;
    }

    showProcess();
    $.ajax({
        type: "POST",
        url: "../comm/editNodes.aspx/MoveToTH",
        data: "{'pids':'" + CpicIds + "','nodids':'" + NewNodeIds + "','type':'" + type + "','OldNodeId':'" + OldNodeId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var res = msg.d;
            if (res == "succ") {

                if (NewNodeIds.indexOf(OldNodeId) < 1) {
                    var ids = CpicIds.split(',');
                    for (var i = 0; i < ids.length; i++) {

                        $('#hidCpicids').val($('#hidCpicids').val().toString().replace(',' + ids[i] + ',', ','));
                        if ($('#td' + ids[i]).length > 0) {
                            $('#td' + ids[i]).remove();
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
                }
                closeProcess();
                art.dialog.get('DGMoveZT').close();
                showMessage("移动成功！");
            } else {
                closeProcess();
                showMessage("移动失败！");
            }


        }
    });

}



function DelToZT(Pid) {
    var type = $('#hidtype').val();
    var NodeId = $('#hidNodeId').val();
    if (Pid == "") {
        Pid = $('#hidCpicids').val();
        if (Pid == ",") {
            showMessage('提示', '请选择专利');
            return;
        }
    }
    art.dialog({
        title: '确认删除',
        content: '<img src="../js/artDialog/skins/icons/warning.png"/>您确定删除所选数据吗？',
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
                        url: "../comm/editNodes.aspx/DelToZT",
                        data: "{'pid':'" + Pid + "','nodid':'" + NodeId + "','type':'" + type + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var res = msg.d;
                            if (res == "succ") {

                                var ids = Pid.split(',');
                                for (var i = 0; i < ids.length; i++) {
                                    $('#hidCpicids').val($('#hidCpicids').val().toString().replace(',' + ids[i] + ',', ','));
                                    if ($('#td' + ids[i]).length > 0) {
                                        $('#td' + ids[i]).remove();
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
                                showMessage("删除成功");
                            } else {
                                showMessage("删除失败");
                            }
                        }
                    });
                },
                focus: true
            }
            ]

    });
}

function readystars() {
    $('.auto-submit-star').rating({
        callback: function (value, link) {

            var pid = $(this).attr('name').toString().replace('star', '');
            var NodeId = $('#hidNodeId').val();
            var type = $('#hidtype').val();
            debugger;
            $.ajax({
                type: "POST",
                url: "../comm/editNodes.aspx/setStars",
                data: "{'pid':'" + pid + "','nodeid':'" + NodeId + "','value':'" + value + "','type':'" + type + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var res = msg.d;
                    if (res == "succ") {
                    } else {
                        showMessage("设置核心专利失败！");
                    }
                }
            });

        }
    });
};




function ShowTable(type, nodeid, sourcetype, pageindex, pagesize, itemcount, showtype, sort) {
    $("#showlist").show();
    $("#showsp").hide();
    $('#divnodata').hide();
    $('#divnoupdata').hide();
    $('#divshowhelp').hide();
    $("#showspdes").hide();

    var isupdate = dd.getValue();
    if (sourcetype == "FI") {
        isSSearch = "FI";
        nodeid = "998";
    }
    else {
        isSSearch = "DB";
    }

    // 显示进度条    
    showProcess();
    $.ajax({
        type: "POST",
        url: "../comm/GetList.aspx/GetPageList1",
        data: "{'Type': '" + type + "', 'NodeId':'" + nodeid + "', 'SourceType': '" + sourcetype + "', 'ItemCount':'" + itemcount + "', 'pageindex':'" + pageindex + "', 'rows':'" + pagesize + "','Sort':'" + sort + "','isupdata':'" + isupdate + "'}",
        timeout: 30000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {

            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            closeProcess();
            showMessage("错误", msg);
            $('#list').datagrid('loaded');
            return;
        },
        success: function (msg) {

            if (sourcetype != "FI") $('#hidNodeId').val(nodeid);
            $('#hidPageIndex').val(pageindex);
            $('#hidpagesize').val(pagesize);
            $('#hidShowType').val(showtype);
            $('#hidSourceType').val(sourcetype);
            $('#hidtype').val(type);
            $('#hidItemCount').val(itemcount);
            var data = $.parseJSON(msg.d);
            FormatTable(type, data, pageindex, pagesize, showtype);

        }
    });
}
function FormatTable(type, data, pageindex, pagesize, showtype) {
    table = new zlTable("divlist", type, data);
    table.ShowZTTable(showtype);
    if (data.total == "0") {
        $("#showlist").hide();
        $("#showsp").hide();

        if (dd.getValue() == "up") {
            $('#divnoupdata').show();
            $('#divnodata').hide();
        }
        else {
            $('#divnoupdata').hide();
            $('#divnodata').show();
        }

        $('#divshowhelp').hide();
        $("#showspdes").hide();
    }
    else {
        if (dd.getValue() == "up") {
            $('#divSSC').hide();
        }
        else {
            $('#divSSC').show();
        }
        SetPage1(data.total, pagesize, pageindex);
        SetChecked();
        readystars();
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


function FormatStarts(pid, value) {
    var starts = "";
    starts += String.Format("<input class=\"auto-submit-star required\" type=\"radio\" name=\"star{0}\" value=\"1\" {1}/>", pid, ("1" == value ? "checked=\"checked\"" : ""));
    for (var i = 2; i <= 5; i++) {
        starts += String.Format("<input class=\"auto-submit-star\" type=\"radio\" name=\"star{0}\" value=\"{1}\" {2} />", pid, i, (i == value ? "checked=\"checked\"" : ""));
    }
    return starts;
}
function GOTOST() {
    var db = $("#hidtype").val();
    var type = requestUrl('type');
    if (type == "") type = "QY";
    var id = $('#hidNodeId').val();
    if (isSSearch == "FI") {
        window.open("../comm/autost.aspx?type=" + db + "&db=" + db + "&id=998");
    }
    else {
        window.open("../comm/autost.aspx?type=" + type + "&db=" + db + "&id=" + id);
    }
}