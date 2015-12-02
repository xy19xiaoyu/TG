$(document).ready(function(){
    ShowSPTable();
});

//显示检索式列表
function ShowSPTable() {
    var ztid = $("#ztnamelist").find("option:selected").val();
    if (ztid != null) {
        $('#TabcnSP').datagrid({ url: '../comm/BSP.aspx', queryParams: { 'ztid': ztid, 'type': 'cn'} });
        $('#TabenSP').datagrid({ url: '../comm/BSP.aspx', queryParams: { 'ztid': ztid, 'type': 'en'} });
        $('#TabcnSP').datagrid({
            onRowContextMenu: function (e, rowIndex, rowData) {
                e.preventDefault();
                
                $('#hidspid').val(rowData.id);
                $('#tableindex').val(rowIndex);
                $('#BoundToZT').menu('show', { left: e.pageX, top: e.pageY });
            }
        });
        $('#TabenSP').datagrid({
            onRowContextMenu: function (e, rowIndex, rowData) {
                e.preventDefault();
                $('#hidspid').val(rowData.id);
                $('#tableindex').val(rowIndex);
                $('#EnBoundToZT').menu('show', { left: e.pageX, top: e.pageY });
            }
        });
    }
    //
}

//展示表格检索
function ShowTabSearch(title, type) {
    clsSP(type);
    if (type == "") {
        type = $("#hidtype").val();
    }
    else {
        $("#hidtype").val(type);
    }

    $("#DseachTitle").html(title);
    $('#divSPText').show();
    switch (title) {
        case "添加检索式":
            clsSP(type);
            break;
        case "修改检索式":
            if (SSP(type) == false) {
                return;
            }
            break;
    }
    ShowTBSdialog(type, title);

}
function ShowTBSdialog(type, title) {
  
    switch (title) {
        case "添加检索式":
        case "修改检索式":

            art.dialog({
                id: "DGtabSearch",
                title: title,
                content: document.getElementById("tabSearch"),
                lock: true,
                border: false,
                button: [
            {
                value: '清空检索式',
                callback: function () {
                    clsSP(type);
                    return false;
                },
                focus: true,
                width: '100px'
            }
            ,
            {
                value: '生成检索式',
                callback: function () {
                    getztsp();
                    return false;
                },
                focus: true,
                width: '100px'
            },
              {
                  value: '确定',
                  callback: function () {
                      ztQuery();
                      return false;
                  },
                  focus: true,
                  width: '60px'
              }
            ]
            });
            break;
    }

}

function clsSP(type) {
    if (type.toUpperCase() == 'CN') {
        $("#endivQueryTable").hide();
        $("#cndivQueryTable").show();
        cnClearSearch();
    }
    else {
        $("#endivQueryTable").show();
        $("#cndivQueryTable").hide();
        docdbClearSearch();
    }
}

function SSP(type) {
    
    var row;
    var rowindex = parseInt($('#tableindex').val());
    if (type == 'cn') {
        row = $('#TabcnSP').datagrid('getRows');
        if (row && row.length>0) {
            var sp = ReGenerateSearchCN(row[rowindex].sp);
            $('#TxtSearch').val(sp);
        }
        else {
            showMessage("请先选择要修改的检索式！");
            return false;
        }
    }
    else {
        row = $('#TabenSP').datagrid('getRows');
        if (row && row.length > 0) {
            var sp = ReGenerateSearchEN(row[rowindex].sp);
            $('#TxtSearch').val(sp);
        }
        else {
            showMessage("请先选择要修改的检索式！");
            return false;
        }
    }
    return true;

}

function getztsp() {
    debugger;
    var type = $("#hidtype").val();
    setViewResult(" "); // 清空验证域
    var strQuery = "";
    if (type.toUpperCase() == "CN") {
        if (strQuery == "") {
            strQuery = getTableSearchQueryCN().Trim();
        }
        var pattern = validateLogicSearchQueryCN(strQuery);
        if (pattern == "") {
            showMessage("请输入检索条件");
            return;
        }
        pattern = pattern.Trim();
        if (pattern.substring(0, 5) == "Error") {
            showMessage(pattern);
            return;
        }
        else {
            strQuery = getMergeSearchEndFlag(strQuery.Trim());
            $("#TxtSearch").val(strQuery);
        }

    }
    else {
        if (strQuery == "") {
            strQuery = getTableSearchQueryEN().Trim();
        }
        var pattern = validateLogicSearchQueryEN(strQuery);
        pattern = pattern.Trim();
        if (pattern == "") {
            showMessage("请输入检索条件");
            return;
        }
        if (pattern.substring(0, 5) == "Error") {
            showMessage(pattern);
            return;
        }
        else {
            SearchDbType = "EN";
            pattern = getMergeSearchEndFlag(pattern.Trim(), 'enckbox');
            $("#TxtSearch").val(pattern);
            strQuery = pattern;
        }

    }

}

//中文检索
function ztQuery() {
    //1.判断检索类型
    var title = $("#DseachTitle").html();
    var type = $("#hidtype").val();
    switch (title) {
        case "添加检索式":
            AddSP(type);
            break;
        case "修改检索式":
            editSP(type);
            break;
    }

}

//添加检索式
function AddSP(type) {

    setViewResult(" "); // 清空验证域
    var maxspNum = 0;
    var rows;
    var strQuery = $("#TxtSearch").val();    
    if (strQuery == "") {
        getztsp();
        strQuery = $("#TxtSearch").val();
    }
    if (strQuery == "") return;

    if (type.toUpperCase() == "CN") {
        rows = $('#TabcnSP').datagrid('getRows');
    }
    else {
        rows = $('#TabenSP').datagrid('getRows');
    }

    if (rows.length > 0) {
        maxspNum = rows[rows.length - 1].SPNum;
    }
    spNum = parseInt(maxspNum) + 1;
    //异步加载 添加节点 返回节点的ID，Name
    var ztid = $("#ztnamelist").find("option:selected").val();
    // 显示进度条
    var myDialog = showProcess();  
    $.ajax({
        type: "POST",
        url: "../comm/BSP.aspx/addSearchPattern",
        data: "{'ztid':'" + ztid + "','spNum':'" + spNum + "','SearchPattern':'" + strQuery + "','Hit':'NULL','type':'" + type + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            myDialog.close();
            var res = msg.d;
            if (res == "succ") {
                ShowSPTable();
                art.dialog.get('DGtabSearch').close();
            } else {
                showMessage(res);
            }
        }
    });
    return false;

}

function editSP(type) {

    setViewResult(" "); // 清空验证域
    var strQuery;
    strQuery = $("#TxtSearch").val();
    if (strQuery == "") {
        getztsp();
        strQuery = $("#TxtSearch").val();
    }
    if (strQuery == "") return;

    if (type.toUpperCase() == "CN") {
        rows = $('#TabcnSP').datagrid('getRows');
    }
    else {        
        rows = $('#TabenSP').datagrid('getRows');
    }

    var spid = $('#hidspid').val();
    // 显示进度条
    var myDialog = showProcess();
    $.ajax({
        type: "POST",
        url: "../comm/BSP.aspx/UpdateSearchPattern",
        data: "{'id':'" + spid + "','SearchPattern':'" + strQuery + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var res = msg.d;
            myDialog.close();
            if (res == "succ") {
                art.dialog.get('DGtabSearch').close();
                ShowSPTable();


            } else {
                showMessage(res);
            }
        }
    });
    return false;
}

function deleteSP(type) {
    
    var row;
    var rowindex = parseInt($('#tableindex').val());
    if (type.toUpperCase() == "CN") {
        row = $('#TabcnSP').datagrid('getRows');
    }
    else {
        row = $('#TabenSP').datagrid('getRows');
    }
    var spid = row[rowindex].id;
    // 显示进度条  
    if (row && row.length >0) {
        art.dialog({
            title: '确认删除',
            content: '<img src="../js/artDialog/skins/icons/warning.png"/>您确认删除检索式:<span style="color:red">' + row[rowindex].sp + "</span>吗？",
            button: [
            {
                value: '取消',
                callback: function () { }

            },
            {
                value: '确定',
                callback: function () {
                    var myDialog = showProcess();
                    spid = spid;
                    $.ajax({
                        type: "POST",
                        url: "../comm/BSP.aspx/deleteSearchPattern",
                        data: "{'id':'" + spid + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d != "failed") {
                                myDialog.close();
                                ShowSPTable();
                            } else {
                                showMessage("删除失败！");
                            }
                        }
                    });

                },
                focus: true
            }
            ]
        });
    }
    else {
        showMessage("请先选择要删除的检索式！");
        return;
    }
}

function ShowAddToZT() {
    
    var title = "检索式绑定至分类导航";
    var ztid = $("#ztnamelist").find("option:selected").val();
    $('#AddZT').tree({
        url: '../comm/getNodes.aspx?ztid=' + ztid + '&fileter=true',
        onBeforeCheck: function (node, checked) {            
            if (checked == true) {
                var nodes = $('#AddZT').tree('getChecked');
                if (nodes.length > 0) {
                    for (var i = 0; i < nodes.length; i++) {
                        $('#AddZT').tree('uncheck', nodes[i].target);
                    }
                }
            }

        }

    });
  
    AddZTDialog('AddToZT', title, '');
}

//添加到专题库
function AddToZT(CpicIds) {
    
    var NewNodeIds = "";               //节点IDS 
    var nodes = $('#AddZT').tree('getChecked');

    if (nodes.length <= 0) {
        showMessage("选择分类");
        return;
    }
    for (var i = 0; i < nodes.length; i++) {
        if (NewNodeIds != '') NewNodeIds += ',';
        NewNodeIds += nodes[i].attributes.Nid;
    }

    var type = requestUrl('db');
    if (type == "") {
        type = $("#hidtype").val();
    }
    var spid = $('#hidspid').val();
    // 显示进度条
    var myDialog = showProcess();
    $.ajax({
        type: "POST",
        url: "../comm/BSP.aspx/SPBoundNode",
        data: "{'spid':'" + spid + "','nodeid':'" + NewNodeIds + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            myDialog.close();
            var res = msg.d;
            if (res == "succ") {
                art.dialog.get('DGAddZT').close();
                ShowSPTable();
                showMessage("添加成功！");
            } else {
                showMessage("添加失败");
            }

        }
    });
}
function RemoveBindToZT(type) {
    var row;
    debugger;
    var rowindex = parseInt($('#tableindex').val());   
    if (type.toUpperCase() == "CN") {
        row = $('#TabcnSP').datagrid('getRows');
    }
    else {
        row = $('#TabenSP').datagrid('getRows');
    }
    var spid = row[rowindex].id;
    // 显示进度条  
    if (row && row.length > 0) {
        art.dialog({
            title: '确认删除',
            content: '<img src="../js/artDialog/skins/icons/warning.png"/>您确认解除绑定:<span style="color:red">' + row[rowindex].NodeName + "</span>吗？",
            button: [
            {
                value: '取消',
                callback: function () { }

            },
            {
                value: '确定',
                callback: function () {
                    var myDialog = showProcess();
                    spid = spid;
                    $.ajax({
                        type: "POST",
                        url: "../comm/BSP.aspx/RemoveBindToZT",
                        data: "{'spid':'" + spid + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d != "failed") {
                                myDialog.close();
                                ShowSPTable();
                            } else {
                                showMessage("解除失败！");
                            }
                        }
                    });

                },
                focus: true
            }
            ]
        });
    }
    else {
        showMessage("请先选择要删除的检索式！");
        return;
    }
}