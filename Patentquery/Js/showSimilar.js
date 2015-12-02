$(document).ready(function () {
    $('#pagetop').pagination({
        displayMsg: '当前显示从 [{from}] 到 [{to}] 共[{total}]条记录',
        onSelectPage: function (pageNumber, pageSize) {
            
            ShowHotTop(pageNumber, pageSize);
        },
        pageSize:10
    });
    ShowHotTop(1, 10);
});
var data;
function ShowHotTop(pageNumber, pageSize) {
    $("#proces").show()
    $('#tbhot').datagrid('loading')
    var appno = requestUrl("appno");
    $.ajax({
        type: "POST",
        url: "../comm/getSimilar.aspx/getSimilars",
        data: "{'appno': '" + appno + "','pageNumber': '" + pageNumber + "','pageSize': '" + pageSize + "'}",
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
            data = $.parseJSON(msg.d);
            formatTable(pageNumber, pageSize)       
        }
    });
}
function formatTable(pageNumber, pageSize) {
    var listview = $.extend({}, $.fn.datagrid.defaults.view, {
        renderRow: function (target, fields, frozen, rowIndex, rowData) {
            if (rowData == null) return;
            var cc = [];
            ////
            if (!frozen) {
                cc.push('<td width="100px">');
                cc.push(fan(rowData.ANF, rowData.apno));
                cc.push('</td>');
                cc.push('<td width="390px">');
                cc.push(fti(rowData.ANF, rowData.title, rowData.apno));
                cc.push('</td>');
                cc.push('<td width="57px">');
                cc.push(rowData.similar);
                cc.push('</td>');
            }

            return cc.join('');
        }
    });
    $('#tbhot').datagrid({
        view: listview
    });
    $('#tbhot').datagrid({
        'onLoadSuccess': function (data) {
            if (data.total == 0) {
                $('#divNoData').show();
                $('#divSimilar').hide();
                $('#pagetop').hide();
                $("#process").hide();
            }
            else {
                $('#divSimilar').show();
                $('#pagetop').show();
                $('#divNoData').hide();
                $("#process").hide();
            }
            //alert('xy');
        }
    })
    
    if (data.total == 0) {
        $('#divNoData').show();
        $('#divSimilar').hide();
        $('#pagetop').hide();
        $("#process").hide();
    }
    else {
        $('#divSimilar').hide();
        $('#pagetop').show();
        $("#process").hide();
        $('#tbhot').datagrid('loadData', { "total": data.total, "rows": data.rows });
    }
    $('#pagetop').pagination({
        displayMsg: '当前显示从 [{from}] 到 [{to}] 共[{total}]条记录',
        total: data.total,
        pageSize: pageSize,
        pageNumber: pageNumber
    });
}
function fan(strANX, appno) {
    var url = "";
    var type = 1;
    if (appno == 12) type = appno.substr(4, 1);
    if (appno == 8) type = appno.substr(2, 1);
    if (type == "3") {
        url = "../my/frmPatDetails.aspx?Id=";
    }
    else {
        url = "../my/frmPatDetails.aspx?Id=";
    }
    url += strANX;
    strReturn = "<a href=\"" + url + "\" target=\"_blank\">" + appno + "</a>";
    return strReturn;
}
function fti(strANX, ti, appno) {
    ti = ti.trim();
    if (ti.trim() == "") ti = "无";
    if (ti.length > 75) ti = ti.substr(0, 75) + "...";
    var url = "";
    var type = 1;
    if (appno == 12) type = appno.substr(4, 1);
    if (appno == 8) type = appno.substr(2, 1);
    if (type == "3") {
        url = "../my/frmPatDetails.aspx?Id=";
    }
    else {
        url = "../my/frmPatDetails.aspx?Id=";
    }
    url += strANX;
    strReturn = "<a href=\"" + url + "\" target=\"_blank\">" + ti + "</a>";
    return strReturn;
}