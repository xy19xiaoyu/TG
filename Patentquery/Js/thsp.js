
var endflag;

//展示表格检索
function ShowTabSearch(title, type) {


    if (type == "") {
        type = $("#hidtype").val();
    }
    else {
        $("#hidtype").val(type);
    }

    $("#DseachTitle").html(title);
    //$('#divSPText').show();
    switch (title) {
        case "二次检索":
        case "过滤检索":
            clsSP(type);
            //$('#divSPText').hide();
            break;
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
    if (type.toUpperCase() == 'CN') {
        $("#endivQueryTable").hide();
        $("#cndivQueryTable").show();
       
    }
    else {
        $("#endivQueryTable").show();
        $("#cndivQueryTable").hide();
       
    }
    if (title == "添加检索式") {
        if (type == 'cn') {
            var rows = $('#TabcnSP').datagrid('getRows');
            if (rows.length > 0) {
                showMessage("提示", "只允许添加一条检索式。");
                return;
            }
            cnClearSearch();
        }
        else {
            var rows = $('#TabenSP').datagrid('getRows');
            if (rows.length > 0) {
                showMessage("提示", "只允许添加一条检索式。");
                return;
            }
            docdbClearSearch();
        }
    } else if (title == "修改检索式") {
        SSP(type);
    }
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
    if (type == 'cn') {
        row = $('#TabcnSP').datagrid('getRows', 0);
        if (row && row.length > 0) {
            var sp = ReGenerateSearchCN(row[0].sp);
            $('#TxtSearch').val(sp);
        }
        else {
            showMessage("请先选择要修改的检索式！");
            return false;
        }
    }
    else {
        row = $('#TabenSP').datagrid('getRows', 0);
        if (row && row.length > 0) {
            var sp = ReGenerateSearchEN(row[0].sp);
            $('#TxtSearch').val(row[0].sp);
        }
        else {
            showMessage("请先选择要修改的检索式！");
            return false;
        }
    }
    return true;

}

//显示检索式列表
function ShowSPTable() {
    var nodeid = $("#hidNodeId").val();
    var nodetext = $("#hidNodeName").val();
    $('#TabcnSP').datagrid('loadData', { total: 0, rows: [] });
    $('#TabcnSP').datagrid({ url: '../comm/GetSearchPattern.aspx', queryParams: { NodeId: nodeid, type: 'cn'} });

    $('#TabenSP').datagrid('loadData', { total: 0, rows: [] });
    $('#TabenSP').datagrid({ url: '../comm/GetSearchPattern.aspx', queryParams: { NodeId: nodeid, type: 'en'} });
}
//
function getztsp() {

    var type = $("#hidtype").val();
    setViewResult(" "); // 清空验证域
    var strQuery = ""
    strQuery = $("#TxtSearch").val(); ;
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
            pattern = getMergeSearchEndFlag(pattern.Trim());
            $("#TxtSearch").val(pattern);
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
        case "二次检索":
        case "过滤检索":
            SSearch(title, type)
            break;
        case "添加检索式":
            var location = window.location.href.toString().toUpperCase();
            if (location.indexOf("ZTBATCHSP.ASPX") > 0) {
                BatchAddSP(type);
            }
            else {
                AddSP(type);
            }
            break;
        case "修改检索式":
            editSP(type);
            break;
    }

}

//添加检索式
function AddSP(type) {
    debugger;
    setViewResult(" "); // 清空验证域
    var strQuery;
    var ztid = $('#hidthid').val(); ;
    strQuery = $("#TxtSearch").val();
    if (strQuery == "") {
        getztsp();
        strQuery = $("#TxtSearch").val();
    }
    if (strQuery == "") return;

    //异步加载 添加节点 返回节点的ID，Name
    var noidid = $('#hidNodeId').val();
    // 显示进度条
    var myDialog = showProcess();
    $.ajax({
        type: "POST",
        url: "../comm/GetSearchPattern.aspx/addSearchPattern",
        data: "{'NodeId':'" + noidid + "','SearchPattern':'" + strQuery + "','Hit':'0','type':'" + type + "','ztid':'" + ztid + "'}",
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
function BatchAddSP() {
    setViewResult(" "); // 清空验证域
    var strQuery;
    strQuery = $("#TxtSearch").val();
    var type = $("#hidtype").val();
    strQuery = $("#TxtSearch").val();
    if (strQuery == "") {
        getztsp();
        strQuery = $("#TxtSearch").val();
    }
    if (strQuery == "") return;

    //异步加载 添加节点 返回节点的ID，Name
    //得到专题库ID
    var noidid = $('#hidNodeId').val();
    //得到当前的检索序号
    // 显示进度条
    var myDialog = showProcess();
    $.ajax({
        type: "POST",
        url: "../comm/GetSearchPattern.aspx/addSearchPattern",
        data: "{'NodeId':'" + noidid + "','SearchPattern':'" + strQuery + "','Hit':'0','type':'" + type + "'}",
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
    debugger;
    setViewResult(" "); // 清空验证域
    var strQuery;
    strQuery = $("#TxtSearch").val();
    strQuery = $("#TxtSearch").val();
    if (strQuery == "") {
        getztsp();
        strQuery = $("#TxtSearch").val();
    }
    if (strQuery == "") return;

    //异步加载 添加节点 返回节点的ID，Name
    var nodeid = $('#hidNodeId').val();
    if (type.toUpperCase() == "CN") {
        row = $('#TabcnSP').datagrid('getRows', 0);
    }
    else {
        row = $('#TabenSP').datagrid('getRows', 0);
    }
    var spid = row[0].id;
    // 显示进度条
    var myDialog = showProcess();
    $.ajax({
        type: "POST",
        url: "../comm/GetSearchPattern.aspx/UpdateSearchPattern",
        data: "{'id':'" + spid + "','SearchPattern':'" + strQuery + "','Hit':'0','nodeid':'" + nodeid + "','type':'" + type + "'}",
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
    if (type.toUpperCase() == "CN") {
        row = $('#TabcnSP').datagrid('getRows', 0);
    }
    else {
        row = $('#TabenSP').datagrid('getRows', 0);
    }
    var nodeid = $("#hidNodeId").val();
    // 显示进度条  
    if (row && row.length > 0) {
        art.dialog({
            title: '确认删除',
            content: '<img src="../js/artDialog/skins/icons/warning.png"/>您确认删除检索式:<span style="color:red">' + row[0].sp + "</span>吗？",
            button: [
            {
                value: '取消',
                callback: function () { }

            },
            {
                value: '确定',
                callback: function () {
                    var myDialog = showProcess();
                    spid = row[0].id;
                    $.ajax({
                        type: "POST",
                        url: "../comm/GetSearchPattern.aspx/deleteSearchPattern",
                        data: "{'id':'" + spid + "','nodeid':'" + nodeid + "','type':'" + type + "'}",
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


