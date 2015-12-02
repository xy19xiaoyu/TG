var zid = "";
var pids = "";
var ids = "";
$(document).ready(function () {
    pids = getUrlParam('xy');
    zid = getUrlParam('zid');
    if (zid == null) return;
    $('#pg').propertygrid({
        url: '../comm/getCSIndexValues.aspx?zid=' + zid + '&pid=' + pids,
        showGroup: true,
        showHeader: false,
        scrollbarSize: 0
    });
    $("#DLGAddIndex").show();
});
function AddIndex(type) {
    
    var ids = '';
    var tmps = "";
    var rows = $('#pg').propertygrid('getRows');
    for (var i = 0; i < rows.length; i++) {
        if (rows[i].value == "否" || rows[i].value == "") continue;
        ids += rows[i].itemid + ',' + rows[i].valueid + ';';
        tmps += rows[i].name + ',' + rows[i].value + ';';
    }
    var myDialog = showProcess();
    $.ajax({
        type: "POST",
        url: "../comm/UserCollects.aspx/AddToIndex",
        data: "{'zid':'" + zid + "','pids':'" + pids + "','ids':'" + ids + "','type':'" + type + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            myDialog.close();
            var res = msg.d;
            if (res == "succ") {
                showMessage("标引成功！");
                art.dialog.get('addtoindex').close();
            } else {
                showMessage("标引失败！");
            }

        }
    });
}
