var cnexportdghtml = '<div id="showExportCFG" style="width:480px; display: none;">' +
        '<div id="ExportCFG" style="min-height:100px;">' +
            '<ul>' +
               '<li><label><input type="checkbox" id="chkAppNo" value="申请号" disabled="disabled" checked="checked" />申请号</label></li>' +
                '<li><label><input type="checkbox" id="chkPubNo" value="公开（公告）号" />公开（公告）号</label></li>' +
                '<li><label><input type="checkbox" id="chkIPC" value="分类号" />分类号</label></li>' +
                '<li><label><input type="checkbox" id="chkMIPC" value="主分类号" />主分类号</label></li>' +
                '<li><label><input type="checkbox" id="chkTitle" value="名称" disabled="disabled" checked="checked" />名称</label></li>' +
                '<li><label><input type="checkbox" id="chkPubDate" value="公开（公告）日" />公开（公告）日</label></li>' +
                '<li><label><input type="checkbox" id="chkAppDate" value="申请日" />申请日</label></li>' +
                '<li><label><input type="checkbox" id="chkAT" value="代理人" />代理人</label></li>' +
                '<li><label><input type="checkbox" id="chkAG" value="专利代理机构" />专利代理机构</label></li>' +
                '<li><label><input type="checkbox" id="chkabs" value="摘要" />摘要</label></li>' +
                '<li><label><input type="checkbox" id="chkCL" value="主权项" />主权项</label></li>' +
                '<li><label><input type="checkbox" id="chkCY" value="国省代码" />国省代码</label></li>' +
                '<li><label><input type="checkbox" id="chkPA" value="申请（专利权）人" />申请（专利权）人</label></li>' +
                '<li><label><input type="checkbox" id="chkIN" value="发明（设计）人" />发明（设计）人</label></li>' +
                '<li><label><input type="checkbox" id="chkPR" value="优先权" />优先权</label></li>' +
                '<li><label><input type="checkbox" id="chkdz" value="地址" />地址</label></li>' +
                '<li><label><input type="checkbox" id="chkflzt" value="法律状态" />法律状态</label></li>' +
                '<li><label><input type="checkbox" id="chkNote" value="标注内容" />标注内容</label></li>' +
                '<li><label><input type="checkbox" id="chkNoteDate" value="标注时间" />标注时间</label></li>' +
            '</ul>' +            
        '</div>' +
    '</div>';
function showExportCFG() {
    //
    var obj = arguments[0];
    var type = requestUrl('db');
    if (type == "") {
        type = $("#hidtype").val();
    }
    var exportdghtml = cnexportdghtml;
  
    if ($('#showExportCFG').length > 0) {
        $('#showExportCFG').remove();
    }
    $(document.body).append(exportdghtml);
    var obj = arguments[0];
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
            id: 'dgExport',
            title: '导出选项',
            content: document.getElementById('showExportCFG'),
            lock: true,
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
    //alert(ColNames);
    var Nodeid = $('#hidNodeId').val();
    //发送服务器
    if (CpicIds == ',') {
        alert('提示请选择专利');
        return;
    }
    var type = "";
    type = requestUrl('db');
    if (type == "") {
        type = $("#hidtype").val();
    }
    if (type == "") {
        type = "cn";
    }

    var myDialog = showProcess();

    $.ajax({
        type: "POST",
        url: "../comm/Export.aspx/ExportData",
        data: "{'CpicIds': '" + CpicIds + "', 'ColNames':'" + ColNames + "','NodeId':'" + Nodeid + "','type':'" + type + "','FileType':'csv'}",
        timeout: 120000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            alert(msg);
            myDialog.close();
            return;
        },
        success: function (msg) {
            //
            //
            //$('#ifile').attr("src", "/Comm/ExportExcel.aspx?filename=" + msg.d);
            document.getElementById("ifile").src = "/Comm/ExportExcel.aspx?filename=" + msg.d;
            myDialog.close();           
            closeProcess('dgExport');
        }
    });
    //返回
}