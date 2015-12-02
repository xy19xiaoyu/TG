var showtype =0;
var pindex = 1;
var psize = 10;
function iniPage(PageIndex, PageSize) {
    var myDialog = showProcess();
    ////debugger;
    $.ajax({
        type: "POST",
        url: "frmthlist.aspx/GetPageList",
        data: "{'PageIndex': '" + PageIndex + "', 'PageSize':'" + PageSize + "'}",
        timeout: 30000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            myDialog.close();
            showMessage("错误", msg);
            return;
        },
        success: function (msg) {
            var data = $.parseJSON(msg.d);
            FormatTable(data);
            SetPage(data.total, PageSize, PageIndex);
            myDialog.close();
        }
    });
}

$(document).ready(function () {
    iniPage(1, 10);
    regPatentListSelectPage();   
});

function FormatTable(data) {   
    var dblist = "";
    for (var i = 0; i < data.rows.length; i++) {
        var rowData = data.rows[i];
        var imgsrc = rowData.ztHeardImg;
        if (imgsrc == "") {
            imgsrc = "../imgs/NoImg_300.jpg";
        }
        dblist += '<li id="zt' + rowData.zid + '" class="ztitem">';
        dblist += '<div class="zt_title">';
        dblist += '<div class="zt_edit">';
        if ($("#delzt").val() == "True") {
            dblist += '<a href="javascript:void(0);" onclick="AppendZtDb(\'修改专题库\',\'' + rowData.zid + '\')">修改</a> ';
        }
        if ($("#edzt").val() == "True") {
            dblist += '<a href="javascript:void(0);" onclick="DelZtDb(\'' + rowData.ztDbName + '\',\'' + rowData.zid + '\')">删除</a>';
        }
        dblist += '</div><a target="_blank" class="a_zt_title" href="ztdb.aspx?type=zt&id=' + rowData.zid + '">' + rowData.ztDbName + '</a></div>';
        dblist += '<div class="zt_img"><img src="../ZtHeadImg/' + imgsrc + '" /></div>';
        dblist += '<div class="zt_des"><div>说明：</div><p>' + rowData.DbDes + '</p></div>';
        dblist += "</li>";
    }
    $('#ztitems').html(dblist);
}

function regPatentListSelectPage() {
    $('#pagetop').pagination({
        onSelectPage: function (pageNumber, pageSize) {
            ////debugger;
            iniPage(pageNumber, pageSize);
        }
    });
}
function SetPage(inttotal, intpageSize, intpageNumber) {
    $('#pagetop').pagination({
        displayMsg: '当前显示从 [{from}] 到 [{to}] 共[{total}]条记录',
        total: parseInt(inttotal),
        pageSize: parseInt(intpageSize),
        pageNumber: parseInt(intpageNumber)
    });
}
function changelist() {
    if (showtype == 0) {
        $('#ztdblist').attr("class", "mid_ztlist");
        $('#achange').text("列表式");
        showtype = 1;
    }
    else {
        $('#ztdblist').attr("class", "mid_ztlist1");
        $('#achange').text("两栏式");
        showtype = 0;
    }
}


var objSelectZt = null;
function AppendZtDb(title, obj) {
    if (obj == null) {
        $("#crop_preview").html('<img style="width: 130px; height: 90px;" src="../ZtHeadImg/NoImg_300.jpg">');
        $("#crop_preview").attr("title", "");
        $("#upload_avatar").find("ul").html("");
    }
    else {
        debugger;
        var ti = $('#zt' + obj).find("a.a_zt_title").html();
        var des = $('#zt' + obj).find("p").html();
        var src = $('#zt' + obj).find("img").attr("src");
        $("#newclass").val(ti);
        $("#txtNodeDes").val(des);
        $("#crop_preview").html('<img style="width: 130px; height: 90px;" src="' + src + '">');
        $("#crop_preview").attr("title", src.replace("../ZtHeadImg/", ""));
        $("#upload_avatar").find("ul").html("");
    }
    art.dialog({
        id: "DGdivAddZT",
        title: title,
        content: document.getElementById("divAddZT"),
        lock: true,
        border: false,
        button: [
            {
                value: '取消',
                callback: function () {
                    return true;
                },
                focus: true
            },
            {
                value: '确定',
                callback: function () {
                    Mag_ztDb(title, obj);
                    return false;
                },
                focus: true
            }
            ]
    });
}

function Check_ZtNm() {
    if ($("#newclass").val() != "") {
        $("#showmsg").hide();
    } else {
        //fadeIn() 与 fadeOut()
        $("#showmsg").show();
    }
}

function Mag_ztDb(title, id) {
    //异步加载 添加节点 返回节点的ID，Name
    if ($("#newclass").val() != "") {
        var txbDbName = $("#newclass").val();
        var desc = $("#txtNodeDes").val();
        var Dbtype = 0;
        if (title == "新建专题库") {
            //addZtDb(string ZtDbname, int nType, string ztDbdes)
            $.ajax({
                type: "POST",
                url: "frmthlist.aspx/addZtDb", ///comm/HdThList.ashx/ zt/frmThList.aspx
                data: "{'ZtDbname':'" + txbDbName + "','nType':'" + Dbtype + "','ztDbdes':'" + desc + "','strImg':'" + $("#crop_preview").attr("title") + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //
                    if (msg.d != "failed") {
                        showMessage("新建专题库成功");
                    } else {
                        showMessage("新建专题库失败");
                    }
                    closeProcess("DGdivAddZT");
                    iniPage($('#pagetop').pagination("options").pageNumber, $('#pagetop').pagination("options").pageSize);
                }
            });
        } else {
            $.ajax({
                type: "POST",
                url: "frmThList.aspx/UpDateZtDb",
                data: "{'zid':'" + id + "','ZtDbname':'" + txbDbName + "','ztDbdes':'" + desc + "','strImg':'" + $("#crop_preview").attr("title") + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //+ "','ztDbdes':'" + desc + "
                    var res = msg.d;
                    if (res != "failed") {
                        showMessage("更新成功");
                    } else {
                        showMessage("更新失败");
                    }
                    closeProcess("DGdivAddZT");
                    iniPage($('#pagetop').pagination("options").pageNumber, $('#pagetop').pagination("options").pageSize);
                }
            });
        }
    }
    else {
        $("#showmsg").show();
    }
    return false;
}
function DelZtDb(ztname,id) {
    art.dialog({
        id: "DGdelzt",
        title: "删除专题库",
        content: "您确定删除专题库<b> [" + ztname + "]</b>吗?",
        lock: true,
        border: false,
        button: [
            {
                value: '取消',
                callback: function () {
                    return true;
                },
                focus: true
            },
            {
                value: '确定',
                callback: function () {
                    $.ajax({
                        type: "POST",
                        url: "frmthlist.aspx/DelZtDb", ///comm/HdThList.ashx/ zt/frmThList.aspx
                        data: "{'zid':'" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            //
                            if (msg.d != "failed") {
                                showMessage("删除成功");
                                $("#zt" + id).remove();
                            } else {
                                showMessage("删除失败");
                            }
                        }
                    });
                },
                focus: true
            }
            ]
    });
  

    return false;
}