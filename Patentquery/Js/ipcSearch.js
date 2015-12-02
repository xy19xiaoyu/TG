function ipckeydown() {
    
    if (event.keyCode == 13) {
        ipcSearch(); 
    }
}

function formatMianClassList(data, type) {
    var html = "";
    for (var i = 0; i < data.rows.length; i++) {
        var rowData = data.rows[i];
        html += "<li><a href='javascript:void(0);' onclick='showipc(\"" + rowData.nodename + "\")' title='" + rowData.des + "'>" + rowData.nodename + "&nbsp;" + rowData.des + "</a></li>";
    }
    $('#list' + type).html(html);
    $('#list' + type).show();
}

function showipc(strclass) {
    //$('#rdipc').attr("checked", 'checked');
    var type = Gettype();
    var dg = showProcess();
    $.ajax({
        type: "POST",
        url: "../Comm/ipcSearch.aspx/GetNodesList",
        data: "{'type':'" + type + "','svale':'" + strclass + "'}",
        timeout: 60000,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //
            // 撤销进度条
            dg.close();
            //$("#masklayer").css("display", "none");
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！"
            }
            showError(msg);
            return;
        },
        success: function (msg) {
            formatSubClassList($.parseJSON(msg.d), type);
            dg.close();
        }
    });
}
function Gettype() {
    var acc = $('#ipctypelist').accordion('getSelected');
    return acc.attr('id');
}

function ipcSearch() {
    
    var input = $('#IPCInput').val();
    var Stype = $('input[name="stype"]:checked').val();
    var type = Gettype();
    if (input == "") return;
    var dg = showProcess();
    $.ajax({
        type: "POST",
        url: "../Comm/ipcSearch.aspx/Search",
        data: "{'type':'" + type + "','stype':'" + Stype + "','svale':'" + input + "'}",
        timeout: 30000,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //
            // 撤销进度条
            dg.close();
            //$("#masklayer").css("display", "none");
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！"
            }
            showError(msg);
            return;
        },
        success: function (msg) {

            formatSubClassList($.parseJSON(msg.d), type);
            dg.close();
        }
    });
}
function formatSubClassList(data, type) {
    var html = "";
    for (var i = 0; i < data.rows.length; i++) {
        var rowData = data.rows[i];
        if (rowData.showtype == "0") {
            if (rowData.IsParent == "True") {
                html += '<li class="ipc_m"><div class="ipc_class"><a href="javascript:void(0);" onclick="showipc(\'' + rowData.NodeName + '\')" title="' + rowData.des + '">' + rowData.NodeName + '</a></div><div class="ipc_desc"><a href="javascript:void(0);" onclick="showipc(\'' + rowData.NodeName + '\')" title="' + rowData.des + '">' + rowData.des + '</a></div>';
            }
            else {
                html += '<li class="ipc_m"><div class="ipc_class">' + rowData.NodeName + '</div><div class="ipc_desc">' + rowData.des + '</div>';
            }

        }
        else {
            if (rowData.IsParent == "True") {
                //html += '<li class="ipc_c"><div class="ipc_class"><a href="javascript:void(0);" onclick="showipc(\'' + rowData.NodeName + '\')" title="' + rowData.des + '">' + rowData.NodeName + '</a></div><div class="ipc_desc">' + rowData.des + '</div><div style="clear: both"></div></li>';
                html += '<li class="ipc_c"><div class="ipc_class"><a href="javascript:void(0);" onclick="showipc(\'' + rowData.NodeName + '\')" title="' + rowData.des + '">' + rowData.NodeName + '</a></div><div class="ipc_desc"><a href="javascript:void(0);" onclick="showipc(\'' + rowData.NodeName + '\')" title="' + rowData.des + '">' + rowData.des + '</a></div>';
            }
            else {
                html += '<li class="ipc_c"><div class="ipc_class">' + rowData.NodeName + '</div><div class="ipc_desc">' + rowData.des + '</div>';
            }

        }
        if (parseInt(rowData.live) >= 1) {
            if (rowData.ipcs != null && rowData.ipcs != "") {
                if (type == "ADM") {
                    html += '<div class="ipc_sbtn"><span class="lansebutton" onclick="Search(\'CN\',\'' + rowData.ipcs + '\')">中国专利</span></div>'
                }
                else {
                    html += '<div class="ipc_sbtn"><span class="lansebutton" onclick="Search(\'CN\',\'' + rowData.ipcs + '\')">中国专利</span>&nbsp;<span class="lansebutton" onclick="Search(\'EN\',\'' + rowData.ipcs + '\')">世界专利</span></div>'
                }
            }
        }
        html += '<div style="clear: both"></div></li>';
    }
    if (data.total <= 0) {
        $('#nodata').show();
        $('#help').hide();
        $('#ipc_right_content').hide();
    }
    else {
        $('#ipc_result').html(html);
        $('#nodata').hide();
        $('#help').hide();
        $('#ipc_right_content').show();
    }

    if (parseInt($('#right').css('height').replace('px', '')) > 567) {
        $('#left').css('height', $('#right').css('height'));
    }
    else {
        $('#left').css('height', "567px");
    }
    $("#pinlieft").pin();
    $('#ipctypelist').accordion('resize');

    //高亮
    var input = $('#IPCInput').val();
    if (input != "") {
        $('#ipc_right_content').removeHighlight().highlight(input);
    }

}

function Search(type, ipcs) {
    ipcs = ipcs.replace("-", "").replace("-", "");
    var ary = ipcs.split(";");
    var ipc2 = "";
    var QueryContent = "";
    for (var i = 0; i < ary.length; i++) {
        var tmpipc = ary[i];
        if (tmpipc.trim() == "") continue;
        ipc2 = formatipc(type, tmpipc).trim();
        QueryContent = QueryContent + "(" + ipc2 + "/IC)+";
        
    }
    var query = "F XX " + QueryContent.substring(0, QueryContent.length - 1);
    DoPatSearch(ipc2, query, type.toUpperCase(), "3", "", "3");
}
function formatipc(type, ipc) {
    if (type.toUpperCase() == "CN") return ipc;
    var split = ipc.split('/');
    if (split.length == 2)//如果IPC中包含/
    {
        var start4 = split[0].substr(0, 4); //开始4位不变
        var mid3 = split[0].substring(4).Trim().replace(/0/g, '');
        var last4 = split[1].Trim();
        return start4 + mid3 + "/" + last4;
    }
    else {
        return ipc;
    }
}