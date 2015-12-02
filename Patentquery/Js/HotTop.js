$(document).ready(function () {
    $('#divtop .datagrid-header').remove();
    $("div#divtop [class='datagrid-wrap panel-body panel-body-noheader']").css("border-width", "0");
    ShowHotTop();
});
function ShowHotTop() {
    debugger;    
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
                        cc.push(FormatTitle1(rowData.type, rowData.title, rowData.Number, rowData.appno, rowData.count, rowData.Pid));
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

function FormatTitle1(type, strTitle, strANX, zhuanLiLeiXing, count, cpic) {
    var strReturn = "";
    strTitle
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