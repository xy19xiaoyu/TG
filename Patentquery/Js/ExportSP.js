var Excnsp = '<div id="Excnsp" style="width: 540px; display: none;">' +
        '<div id="Excnsp1" style="min-height:100px;">' +
            '<ul>' +
                '<li><input type="checkbox" id="chkTitle" value="名称" disabled="disabled" checked="checked" />名称</li>' +
                '<li><input type="checkbox" id="chkAppNo" value="申请号" disabled="disabled" checked="checked" />申请号</li>' +
                '<li><input type="checkbox" id="chkAppDate" value="申请日" />申请日</li>' +
                '<li><input type="checkbox" id="chkPubNo" value="公开号" />公开号</li>' +
                '<li><input type="checkbox" id="chkPubDate" value="公开日" />公开日</li>' +                
                '<li><input type="checkbox" id="chkPubNo" value="公告号" />公告号</li>' +
                '<li><input type="checkbox" id="chkPubDate" value="公告日" />公告日</li>' +
                '<li><input type="checkbox" id="chkPA" value="申请人" />申请人</li>' +
                '<li><input type="checkbox" id="chkIN" value="发明人" />发明人</li>' +
                '<li><input type="checkbox" id="chkIPC" value="分类号" />分类号</li>' +
                '<li><input type="checkbox" id="chkAG" value="代理机构" />代理机构</li>' +
                '<li><input type="checkbox" id="chkAT" value="代理人" />代理人</li>' +
                '<li><input type="checkbox" id="chkPR" value="优先权" />优先权</li>' +
                '<li><input type="checkbox" id="chkdz" value="申请人地址" />申请人地址</li>' +
                '<li><input type="checkbox" id="chkflzt" value="法律状态" />法律状态</li>' +
                '<li><input type="checkbox" id="chkCL" value="主权利要求" />主权利要求</li>' +                
                '<li><input type="checkbox" id="chkabs" value="摘要" />摘要</li>' +
            '</ul>' +
            '<div style="clear:both"/><ul><li style="width:400px"><div id="slider-range"></div></li></ul>' +
        '</div>' +
    '</div>';
var Exensp = '<div id="Exensp" style="width: 540px; display: none;">' +
        '<div id="Exensp1" style="min-height: 100px;">' +
            '<ul>' +
                '<li><input type="checkbox" id="chkTitle" value="名称" disabled="disabled" checked="checked" />名称</li>' +
                '<li><input type="checkbox" id="chkAppNo" value="申请号" disabled="disabled" checked="checked" />申请号</li>' +
                '<li><input type="checkbox" id="chkAppDate" value="申请日" />申请日</li>' +
                '<li><input type="checkbox" id="chkPubNo" value="公开号" />公开号</li>' +
                '<li><input type="checkbox" id="chkPubDate" value="公开日" />公开日</li>' +
                '<li><input type="checkbox" id="chkPA" value="申请人" />申请人</li>' +
                '<li><input type="checkbox" id="chkIN" value="发明人" />发明人</li>' +
                '<li><input type="checkbox" id="chkIPC" value="分类号" />分类号</li>' +
                '<li><input type="checkbox" id="chkEC" value="欧洲分类" />欧洲分类</li>' +
                '<li><input type="checkbox" id="chkPR" value="优先权" />优先权</li>' +
                '<li><input type="checkbox" id="chkCT" value="引用文献" />引用文献</li>' +
                '<li><input type="checkbox" id="chkabs" value="摘要" />摘要</li>' +
            '</ul>' +
            '<div style="clear:both"/><ul><li style="width:400px"><div id="slider-range"></div></li></ul>' +
        '</div>' +
    '</div>';
function showExportCFG() {
    //
    var obj = arguments[0];
    var type = getUrlParam('db');
    if (type == null) {
        type = $("#hidtype").val();
    }
    var exportdghtml;
    
    if (type.toUpperCase() == "CN") {
        exportdghtml = Excnsp;
    }
    else {
        exportdghtml = Exensp;
    }
    if ($('#showExportCFG').length == 0) {
        $(document.body).append(exportdghtml);
    }
    if (obj != null) {
        art.dialog({
            title: '导出选项',
            content: document.getElementById('showExportCFG'),
            lock: true,
            padding:'2px',
            id: 'dgExport',
            button: [
                {
                    value: '全选',
                    callback: function () {
                        selectcfg('true');
                        return false;
                    },
                    focus: false
                },
                {
                    value: '取消全选',
                    callback: function () {
                        selectcfg('false');
                        return false;
                    },
                    focus: false
                },
                {
                    value: '导出',
                    callback: function () {
                        Export(obj);
                    },
                    focus: true
                }
            ]
        });
    }
    else {
        //发送服务器
        if ($('#hidCpicids').val() == ',') {
            alert('提示请选择专利');
            return;
        }
        art.dialog({
            title: '导出选项',
            content: document.getElementById('showExportCFG'),
            lock: true,
            id: 'dgExport',
            button: [
                {
                    value: '全选',
                    callback: function () {
                        selectcfg('true');
                        return false;
                    },
                    focus: false
                },
                {
                    value: '取消全选',
                    callback: function () {
                        selectcfg('false');
                        return false;
                    },
                    focus: false
                },
                {
                    value: '导出',
                    callback: function () {
                        Export();
                    },
                    focus: true
                }
            ]
        });
    }
}
function selectcfg() {
    //
    var obj = arguments[0];

    if (obj == "true") {
        $("div#ExportCFG input[type='checkbox']").attr("checked", true);
    } else {
        $("div#ExportCFG input[type='checkbox']").attr("checked", false);
    }
    $('#chkAppNo').attr("checked", true);
    $('#chkTitle').attr("checked", true);
}

function Export() {
    //
    var obj = arguments[0];
    var CpicIds = "";
    if (obj == null) {
        CpicIds = $('#hidCpicids').val();
    }
    else {
        CpicIds = obj;
    }

    var ColNames = "";
    $("div#ExportCFG input[type='checkbox']").each(function () {
        if ($(this).attr("checked") == "checked") {
            ColNames += $(this).attr("value") + "|";
        }
    });

    var type = "";
    type = getUrlParam('db');
    if (type == null) {
        type = $("#hidtype").val();
    }
    if (type == "") {
        type = "cn";
    }


    //发送服务器
    if (CpicIds == ',') {
        showMessage('提示请选择专利');
        return;
    }
    var myDialog = showProcess();

    $.ajax({
        type: "POST",
        url: "../comm/Export.aspx/ExportData",
        data: "{'CpicIds': '" + CpicIds + "', 'ColNames':'" + ColNames + "','type':'" + type + "'}",
        timeout: 30000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            showMessage(msg);
            myDialog.close();
            return;
        },
        success: function (msg) {
            //
            $('#ifile').attr("src", msg.d);
            myDialog.close();
            art.dialog.get('dgExport').close();
        }
    });
    //返回
}

