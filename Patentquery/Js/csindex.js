var Subid = "";
var SubRowIndex;
var SubGid;
var CSIndexsRowIndex;
var delDelSubRoleMessage = "";
$(document).ready(function () {
    IniCSIndex();
    $("#btnSearch").click(function () {
        IniCSIndex();
    });

    $(".btn_Add_CSIndex").each(function () {
        $(this).click(function () {
            $("#txtitemname").val("");
            $("#txtitemname").attr("disabled", false);
            $("#txtitemname").parent().removeClass("has-error");
            $("#txtvalue1").val("");
            $("#txtvalue1").attr("disabled", false);
            $("#txtvalue1").parent().removeClass("has-error");
            $("#txtvalue2").val("");
            $("#txtvalue3").val("");
            $("#txtvalue4").val("");
            $("#txtvalue5").val("");
            art.dialog({
                id: 'DGAddCSIndex',
                title: "添加标引项",
                content: document.getElementById("dlgCSIndex"),
                lock: true,
                border: false,
                cancelValue: '取消',
                cancel: function () { },
                okValue: '确定',
                ok: function () {
                    AddCSIndex();
                    return false;
                }
            });
        });
    });
    $(".btn_edit_CSIndex").each(function () {
        $(this).click(function () {
            EditIndexItem();
        });
    });


    $(".btn_Del_CSIndex").each(function () {
        $(this).click(function () {
            DLGDleCSIndex();
        });
    });
});
function AddCSIndex() {
    //debugger;
    var itemname = $("#txtitemname").val();
    var value1 = $("#txtvalue1").val();
    var value2 = $("#txtvalue2").val();
    var value3 = $("#txtvalue3").val();
    var value4 = $("#txtvalue4").val();
    var value5 = $("#txtvalue5").val();
    if (itemname.trim() == "") {
        $("#txtitemname").focus();
        $("#txtitemname").parent().addClass("has-error");
        return;
    }
    if (value1.trim() == "") {
        $("#txtvalue1").focus();
        $("#txtvalue1").parent().addClass("has-error");
        return;
    }
    showProcess();
    $.ajax({
        type: "POST",
        url: "UserCSIndex.aspx/AddIndex",
        data: "{'itemname': '" + itemname + "','value1': '" + value1 + "','value2':'" + value2 + "','value3': '" + value3 + "','value4': '" + value4 + "','value5': '" + value5 + "'}",
        timeout: 30000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            showMessage("错误", msg);
            closeProcess();
            return;
        },
        success: function (msg) {
            ////debugger;
            var result = $.parseJSON(msg.d);
            if (result.static == "false") {
                showMessage("错误", result.msg);
                closeProcess();
                return;
            }
            else {
                $('#CSIndexs').datagrid('appendRow', {
                    'itemname': itemname,
                    'id': result.msg
                });
                closeProcess();
                closeProcess("DGAddCSIndex");
            }

        }
    });

}
function EditCSIndex() {
    debugger;
    var itemname = $("#txtitemname").val();
    if ($.trim(itemname) == "") {
        $("#txtitemname").focus();
        $("#txtitemname").parent().addClass("has-error");
        return;
    }
    var value1 = $("#txtvalue1").val();
    if (value1.trim() == "") {
        $("#txtvalue1").focus();
        $("#txtvalue1").parent().addClass("has-error");
        return;
    }
    var row = $('#CSIndexs').datagrid('getSelected');
    var rowIndex = $('#CSIndexs').datagrid('getRowIndex', row);
    var id = row.id;

    var itemvalues = "";
    var showitemvalues = "";
    var items = row.ids.split(';');
    for (var i = 0; i < items.length; i++) {
        var tmp = $("#txtvalue" + (i + 1)).val().trim();
        itemvalues += tmp + ";";
        if (tmp == "") {
            continue;
        }
        showitemvalues += tmp + ";";
    }
    itemvalues = itemvalues.trimRight(';');
    showitemvalues = showitemvalues.trimRight(';');
    showProcess();
    $.ajax({
        type: "POST",
        url: "UserCSIndex.aspx/EditIndex",
        data: "{'itemname': '" + itemname + "','valuenames': '" + itemvalues + "','ids': '" + row.ids + "','id': '" + id + "'}",
        timeout: 30000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            showMessage("错误", msg);
            closeProcess();
            return;
        },
        success: function (msg) {
            var result = $.parseJSON(msg.d);
            if (result.static == "false") {
                showMessage("错误", result.msg);
                closeProcess();
                return;
            }
            else {
                $('#CSIndexs').datagrid('updateRow', {
                    index: rowIndex,
                    row: {
                        'itemname': itemname,
                        'itemvalues': showitemvalues,
                        'id': id
                    }
                });
                closeProcess();
                closeProcess("DGEditCSIndex");
                showMessage("修改成功！");
            }

        }
    });
}
function DLGDleCSIndex(index) {
    var row;
    if (arguments.length == 0) {
        row = $('#CSIndexs').datagrid('getSelected');
    }
    else {
        row = $('#CSIndexs').datagrid('getRows')[index];
    }
    if (row) {
        art.dialog({
            id: "DGDelCSIndex",
            title: "删除用户",
            content: '<img src="../js/artDialog/skins/icons/warning.png" alt=""/>&nbsp;&nbsp;&nbsp;&nbsp;' + "您确定删除用户 <span style='color:red'>" + row.itemname + "</span> 吗？",
            lock: true,
            border: false,
            cancelValue: '取消',
            cancel: function () { },
            okValue: '确定',
            ok: function () {

                var rowIndex = $('#CSIndexs').datagrid('getRowIndex', row);
                if (row) {
                    showProcess();
                    var id = row.id;
                    $.ajax({
                        type: "POST",
                        url: "UserCSIndex.aspx/DelIndex",
                        data: "{'id': '" + id + "'}",
                        timeout: 30000, // set time out 30 seconds
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var msg = XMLHttpRequest.responseText;
                            if (textStatus == "timeout") {
                                msg = "超时，请稍后再试！";
                            }
                            showMessage("错误", msg);
                            closeProcess();
                            return;
                        },
                        success: function (msg) {
                            var result = $.parseJSON(msg.d);
                            if (result.static == "false") {
                                closeProcess();
                                showMessage("错误", result.msg);
                                return;
                            }
                            else {
                                $('#CSIndexs').datagrid("deleteRow", rowIndex);
                                closeProcess();
                                closeProcess("DGDelCSIndex");
                                showMessage("删除成功！");
                            }

                        }
                    });
                }
                return false;
            }
        });
    }
    else {
        showMessage("未选择数据", "请选择一条数据！");
        return;
    }
}


function IniCSIndex() {
    var itemname = $("#itemname").val();
    var RealName = $("#RealName").val();
    showProcess();
    $.ajax({
        type: "POST",
        url: "UserCSIndex.aspx/GetIndexs",
        data: "{'itemname': '" + itemname + "'}",
        timeout: 30000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            showMessage("错误", msg);
            closeProcess();
            return;
        },
        success: function (msg) {
            //debugger;
            var data = $.parseJSON(msg.d);
            $("#CSIndexs").datagrid('loadData', data);
            closeProcess();
        }
    });
}

function EditIndexItem(index) {
    var row;
    if (arguments.length == 1) {
        row = $('#CSIndexs').datagrid('getRows')[index];
    }
    else {
        row = $('#CSIndexs').datagrid('getSelected');
    }

    if (row) {
        //debugger;

        $("#txtitemname").val(row.itemname);
        $("#txtitemname").attr("disabled", true);
        $("#txtitemname").parent().removeClass("has-error");
        $("#txtvalue1").val("");
        $("#txtvalue2").val("");
        $("#txtvalue3").val("");
        $("#txtvalue4").val("");
        $("#txtvalue5").val("");
        var items = row.itemvalues.split(';');
        for (var i = 0; i < items.length; i++) {
            $("#txtvalue" + (i + 1)).val(items[i]);
        }
    }
    else {
        showMessage("未选择数据", "请选择一条数据！");
        return;
    }
    art.dialog({
        id: 'DGEditCSIndex',
        title: "编辑用户",
        content: document.getElementById("dlgCSIndex"),
        lock: true,
        border: false,
        cancelValue: '取消',
        cancel: function () { },
        okValue: '确定',
        ok: function () {
            EditCSIndex();
            return false;
        }
    });
}
function formatOper(val, row, index) {
    return '<a href="#" onclick="EditIndexItem(' + index + ')">修改</a> &nbsp;<a href="#" onclick="DLGDleCSIndex(' + index + ')">删除</a>';
}