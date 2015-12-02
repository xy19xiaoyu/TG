$(document).ready(function () {
    

    $('#pagetop').pagination({
        displayMsg: '当前显示从 [{from}] 到 [{to}] 共[{total}]条记录',
        onSelectPage: function (pageNumber, pageSize) {
            
            ShowHotTop(pageNumber, pageSize);
        },
        pageSize: "10"
    });
    ShowHotTop("1", "10");

});
function ShowHotTop(pageNumber, pageSize) {

    $('#tbhot').datagrid('loading')
    var appno = requestUrl("appno");
    var cpic =  requestUrl("id");
    $.ajax({
        type: "POST",
        url: "../comm/getSimilar.aspx/getFamily",
        data: "{'appno': '" + appno + "','pageNumber': '" + pageNumber + "','pageSize': '" + pageSize + "','CPIC': '" + cpic + "'}",
        timeout: 30000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            showMessage("错误", msg);
            return;
        },
        success: function (msg) {
            debugger;
            var data = $.parseJSON(msg.d);
            var listview = $.extend({}, $.fn.datagrid.defaults.view, {
                renderRow: function (target, fields, frozen, rowIndex, rowData) {
                    if (rowData == null) return;
                    var cc = [];

                    if (!frozen) {
                        cc.push('<td width="100px">');
                        cc.push(fan(rowData.ANX, rowData.apno));
                        cc.push('</td>');
                        cc.push('<td width="600px">');
                        cc.push(fti(rowData.ANX, rowData.title));
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
                    }
                    else {
                        $('#pagetop').show();
                        $('#divSimilar').show();
                        $('#divNoData').hide();
                    }
                    //alert('xy');
                }
            })
            $('#process').hide();
            if (data.total == 0) {
                $('#divNoData').show();
                $('#divSimilar').show();
            }
            else {
                $('#tbhot').datagrid('loadData', { "total": data.total, "rows": data.rows });
            }
            $('#pagetop').pagination({
                displayMsg: '当前显示从 [{from}] 到 [{to}] 共[{total}]条记录',
                total: data.total,
                pageSize: pageSize,
                pageNumber: pageNumber
            });
        }
    });
}
function fan(strANX, appno) {
    var url = "../my/EnPatentDetails.aspx?Id=";
    url += strANX;
    strReturn = "<a href=\"" + url + "\" target=\"_blank\">" + appno + "</a>";
    return strReturn;
}
function fti(strANX, ti) {
    
    if (ti == null) ti = "无";
    if (ti.trim() == "") ti = "无";
    if (ti.length > 75) ti = ti.substr(0, 75) + "...";
    var url = "../my/EnPatentDetails.aspx?Id=";
    url += strANX;
    strReturn = "<a href=\"" + url + "\" target=\"_blank\">" + ti + "</a>";
    return strReturn;
}