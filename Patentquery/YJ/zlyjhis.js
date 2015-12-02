var dbtype = "CN";
var CID;
var PageIndex = 1;
var PageSize = 15

$(document).ready(function () {
    show($('#jzds'));
});

function show(obj) {
    dbtype = getUrlParam('db');
    CID = getUrlParam('CID');
    showMianTable();
}

function showMianTable() {  
    showProcess();
    $.ajax({
        type: "POST",
        url: "../comm/yujing.aspx/GetPageListhis",
        data: "{'CID':'" + CID + "','PageIndex':'" + PageIndex + "','PageSize':'" + PageSize + "'}",
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
            return;
        },
        success: function (msg) {

            var data = $.parseJSON(msg.d);
            $('#tableyj').datagrid({
                view: detailview,
                detailFormatter: function (index, row) {
                    return '<div style="padding:2px"><table id="ddv-' + index + '"></table></div>';
                },
                onExpandRow: function (index, row) {

                    $('#ddv-' + index).datagrid({
                        url: '../comm/yjitemsHis.aspx?wid=' + row.W_ID,
                        fitColumns: true,
                        singleSelect: true,
                        rownumbers: true,
                        loadMsg: '正在加载....',
                        height: 'auto',
                        columns: [[
                            { field: 'S_NAME', title: '预警项', width: 200 },                            
                            { field: 'CURRENTNUM', title: '当前专利数', width: 100, align: 'center', formatter: function (value, rowData, rowIndex) { return formatlist(value, rowData, rowIndex, 0) } },
                            { field: 'CHANGENUM', title: '变更数量', width: 100, align: 'center', formatter: function (value, rowData, rowIndex) { return formatlist(value, rowData, rowIndex, 1) } }
                        ]],
                        onResize: function () {
                            $('#tableyj').datagrid('fixDetailRowHeight', index);
                        },
                        onLoadSuccess: function () {

                            setTimeout(function () {
                                $('#tableyj').datagrid('fixDetailRowHeight', index);
                            }, 0);
                        }
                    });
                    $('#tableyj').datagrid('fixDetailRowHeight', index);
                }
            });
            var pager = $('#tableyj').datagrid().datagrid('getPager');
            pager.pagination({
                onSelectPage: function (pageNumber, pageSize) {
                    
                    PageIndex = pageNumber;
                    PageSize = pageSize;
                    showMianTable();
                },
                total: parseInt(data.total),
                displayMsg: '当前显示从 [{from}] 到 [{to}] 共[{total}]条记录'
            });
            
            $('#tableyj').datagrid('loadData', data);
            pager.pagination({ 'pageSize': PageSize, 'pageNumber': PageIndex });
            closeProcess();
        }
    });
}

function formathit(value, rowData, rowIndex) {
    return "<a href='javascript:void(0);' onclick='updateyjdate(" + rowData.C_ID + ");'>手动更新</a>&nbsp;<a href='javascript:void(0);' onclick='showEdityjDG(" + rowIndex + ");'>修改</a>&nbsp;<a target='_blank' href='frmYJHis.aspx?CID=" + rowData.C_ID + "'>更新历史</a>";
}
function formatlist(value, rowData, rowIndex, isupdate) {
    return "<a href='javascript:void(0);' onclick='showlist(" + rowData.W_ID + "," + isupdate + "," + value + ")'>" + value + "</a>";
}
function showlist(W_ID, isupdate, Nm) {
    window.open("../my/frmPatentList.aspx?db=" + dbtype + "&No=" + W_ID + "&from=yj" + isupdate + "&Nm=" + Nm);
}

function ChangeDateToString(CurDate) {
//    Year = CurDate.substring(0, 4);
//    Month = CurDate.substring(5, 7);
//    Day = CurDate.substring(8, 10);

//    return Year + "-" + Month + "-" + Day;


    var DateIn = new Date(CurDate);
    var Year = 0;
    var Month = 0;
    var Day = 0;
    var CurrentDate = "";
    //    初始化时间
    Year = DateIn.getFullYear();
    Month = DateIn.getMonth() + 1;
    Day = DateIn.getDate();

    CurrentDate = Year + "-";
    if (Month >= 10) {
        CurrentDate = CurrentDate + Month + "-";
    }
    else {
        CurrentDate = CurrentDate + "0" + Month + "-";
    }
    if (Day >= 10) {
        CurrentDate = CurrentDate + Day;
    }
    else {
        CurrentDate = CurrentDate + "0" + Day;
    }
    return CurrentDate;
}

 