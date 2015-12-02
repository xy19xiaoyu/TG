document.writeln("<script type='text/javascript' src='../Js/AjaxDoPatSearch.js'></script>");
document.writeln("<script type='text/javascript' src='../Js/SearchCommon.js'></script>");


//////-------------
function DoTableSearchNew(_sdbType, _sFlag) {

    _sdbType = _sdbType.toUpperCase(); //转大写

    var strQuery = "";
    var res = "";
    var strEndFlag = "";
    if (_sFlag == "0") {
        res = getGenerateSearch();
        //strEndFlag = getEndFlag(res);
    } else {
        strQuery = document.getElementById("TxtSearch").value.Trim();

        if (_sdbType == "CN") {
            var reg = /(\d{9})(a|b|A|B)(\/PN|\/GN)/g; //去掉公开公告最后一位字母
            if (reg.test(strQuery)) {
                strQuery = strQuery.replace(reg, "$1$3");
            }
        }
        strEndFlag = getEndFlag(strQuery);
        if (validateQueryEndFlag(strEndFlag)) {
            strQuery = validateLogicSearchQuery(getSearchQuery(strQuery));
            res = strQuery + strEndFlag;
        } else {
            return;
        }
    }

    if (_sdbType != "CN") {
        var nQueryMaxLen = nEnTbQueryMaxLen;
    }

    // 验证检索式
    if (res == "") {
        showError("检索式为空！");
        //setSearchFormula("");
        return;
    }
    else if (res.indexOf("Error") != -1) {
        showError(res);
        //setSearchFormula("");
        return;
    }
    else if (res.length > nQueryMaxLen) {
        showError("检索式不能超过" + nQueryMaxLen + "个字符！");
        //setSearchFormula("");
        return;
    }
    //var res = "F XX (200302/AD + 200201/AD) * (计算机/TI)";
    //var res = "F XX ( (200302/AD + 200201/AD) * (计算机/TI)) + (200501/AD)";
    //document.getElementById("TxtSearch").value = res;

    DoPatSearch("", res, _sdbType, "1", "");
}

function validateSearchQuery(res) {
    getEndFlag(res);

    res = validateLogicSearchQuery(strQuery);
}


// 生成检索式
function getGenerateSearch(_sdbType) {
    setViewResult(" "); // 清空验证域
    var strQuery = getTableSearchQuery().Trim();
    if (_sdbType == "CN") {
        var reg = /(\d{9})(a|b|A|B)(\/PN|\/GN)/g; //去掉公开公告最后一位字母
        if (reg.test(strQuery)) {
            strQuery = strQuery.replace(reg, "$1$3");
        }
    }
    var res = validateLogicSearchQuery(strQuery);
    res = getMergeSearchEndFlag(res.Trim());
    return res;
}
// 清空所有text框架


///----------配置表格项
var Select_Entrances = "";
var objDbType = "CN";
function OpenSetTable() {

    //读取现有配置项
    //InitTableEnterances(objDbType);

    //            easyDialog.open({
    //                container: 'DivAddNode'
    //            });
    //            return true;

    art.dialog({
        id: "artSetTable",
        title: "配置表格项",
        content: document.getElementById("divSetTable1"),   //DivAddNode divSetTable divSetTable1       
        lock: true,
        fixed: true,
        border: true
    });

    return true;
}

//全选取消
function selectAllSetTable(objPtType) {
    var SetValue = $("#checkBoxSelectAllSetTable").attr("checked"); //trun|false
    if (objPtType == "cn") {
        $("input[name='checkBoxSetCn']").attr("checked", SetValue);
    } else {
        $("input[name='checkBoxSetEn']").attr("checked", SetValue);
    }
}

//取消应用
function CloseSetTab() {
    try {
        //恢复配置项
        InitTableEnterances(objDbType);

        easyDialog.close();
    } catch (e) {
    }

    try {
        art.dialog.get('artSetTable').close();
    } catch (e) {
    }
}
//应用
function ApplaySetTable(objPtType) {
    //checkBoxSetCn
    var checkBoxName = "checkBoxSet";
    var TbFlag = "Table";

    if (objPtType.toUpperCase() == "CN") {
        checkBoxName += "Cn";
        TbFlag += "Cn";
    } else {
        checkBoxName += "En";
        TbFlag += "En";
    }
    //
    var TmpEntrItem = "";
    var TmpSelectEntr = "";

    //<input type="checkbox" name="checkBoxSetCn" value="TableCnIC" />
    $("input[name='" + checkBoxName + "']").each(function () {
        TmpEntrItem = $(this).val().replace(TbFlag, "").toUpperCase();

        if ($(this).attr("checked") == true) {
            //id = "dtTbAB"
            TmpSelectEntr += TmpEntrItem + ",";
        }
    });

    if (TmpSelectEntr == "") {
        TmpSelectEntr = ",";
    }

    if (TmpSelectEntr == Select_Entrances) {
        CloseSetTab();
        return;
    }

    //保存数据库
    $("#spanLoading").show();
    $("#spanErr").hide();
    //updateEntrancesCfg(string strEntrances, string _strCfgType)
    $.ajax({
        type: "POST",
        url: "/my/frmCnTbSearch.aspx/updateEntrancesCfg",
        data: "{'strEntrances':'" + encodeURIComponent(TmpSelectEntr) + "', '_strCfgType':'" + objPtType + "'}",
        timeout: 30000,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            // 撤销进度条
            //$.unfunkyUI();
            //alert('应用失败!');
            //easyDialog.close();
            $("#spanLoading").hide();
            $("#spanErr").show();
        },
        success: function (msg) {
            // 撤销进度条
            //alert(msg);
            //$.unfunkyUI();
            Select_Entrances = TmpSelectEntr;
            InitTableEnterances(objPtType);
            $("#spanLoading").hide();
            $("#spanErr").hide();
            CloseSetTab();
        }
    });
}

function InitPageData(objPageType) {
    InitCommon(objPageType); //searchCommon.js
    objDbType = objPageType;
    Select_Entrances = $("#hfSelEntrances").val();
    //    if (Select_Entrances == "") {
    //        //$("#checkBoxSelectAllSetTable").click();
    //        $("#checkBoxSelectAllSetTable").attr("checked", true);
    //        selectAllSetTable(objDbType);
    //    } else {
    //        InitTableEnterances(objDbType);
    //    }

    InitTableEnterances(objDbType);

    try {
        if (objDbType == "cn") {
            $("input[name='checkBoxSetCn']").click(function () {
                if ($("input[name='checkBoxSetCn']:checked").length == $("input[name='checkBoxSetCn']").length) {
                    $("#checkBoxSelectAllSetTable").attr("checked", true);
                } else {
                    $("#checkBoxSelectAllSetTable").attr("checked", false);
                }
            });
        } else {
            $("input[name='checkBoxSetEn']").click(function () {
                if ($("input[name='checkBoxSetEn']:checked").length == $("input[name='checkBoxSetEn']").length) {
                    $("#checkBoxSelectAllSetTable").attr("checked", true);
                } else {
                    $("#checkBoxSelectAllSetTable").attr("checked", false);
                }
            });
        }

        $("#chkAll").attr("checked", true); //trun|false
        $("input[name='checkbox']").attr("checked", true);

        $("input[name='checkbox']").click(function () {
            if ($("input[name='checkbox']:checked").length == $("input[name='checkbox']").length) {
                $("#chkAll").attr("checked", true);
            } else {
                $("#chkAll").attr("checked", false);
            }
        });
    } catch (e) {
    }
}

function InitTableEnterances(objPtType) {
    //alert(Select_Entrances);
    var checkBoxName = "checkBoxSet";
    var TbFlag = "Table";

    if (objPtType.toUpperCase() == "CN") {
        checkBoxName += "Cn";
        TbFlag += "Cn";
    } else {
        checkBoxName += "En";
        TbFlag += "En";
    }
    //
    var TmpEntrItem = "";

    //<input type="checkbox" name="checkBoxSetCn" value="TableCnIC" />
    $("#checkBoxSelectAllSetTable").attr("checked", true);
    //第一次使用用户,数据库默认为空
    if (Select_Entrances == "") {
        selectAllSetTable(objDbType);
        return;
    }

    $("input[name='" + checkBoxName + "']").each(function () {
        TmpEntrItem = $(this).val().replace(TbFlag, "").toUpperCase();

        if (Select_Entrances.indexOf(TmpEntrItem + ",") > -1) {
            $(this).attr("checked", true)
            //id = "dtTbAB"           
            $("#dtTb" + TmpEntrItem).show();
            $("#dtTb" + TmpEntrItem).next("dd").show();

        } else {
            $(this).attr("checked", false)
            //id = "dtTbAB"           
            $("#dtTb" + TmpEntrItem).hide();
            $("#dtTb" + TmpEntrItem).next("dd").hide();
            $("#dtTb" + TmpEntrItem).next("dd").children("input").val("");
            $("#checkBoxSelectAllSetTable").attr("checked", false);
        }
    });
}

///////////////////
$(document).ready(function () {
    $("[id^='Txt']").attr("title", "");

    $("[id^='Txt']").each(function () {
        //alert($(this).attr("title"));
        try {
            var Flag = $("[name='RadioGroup1']:checked").val().trim();
            Flag = Flag == "中国专利" ? "CN_" : "EN_";

            $(this).attr("title", TableEnTrancesTips[Flag + $(this).attr("lang").toUpperCase()]);
            $(this).next().attr("title", TableEnTrancesTips[Flag + $(this).attr("lang").toUpperCase()]);            
        } catch (e) { }
    });

    $("input,img,textarea").niceTitle({ showLink: false }); //要排除一些例外的元素，例如可以用a:not([class='nono'])来排除calss为"nono"的a元素
});

function addcommand(obj) {
    var strCommand = obj.name;

    var destinationObj = document.getElementById("TxtSearch");
    // 如果当前活动控件是文本输入框(id值以"Txt开头")，则在当前控件中插入；否则在检索式输入框插入；
    //            var activeObj = cursPos;
    //            var activeId = activeObj.id.toLowerCase();
    //            if (activeId.indexof("txt") != -1)
    //                destinationObj = activeObj;

    destinationObj.focus();
    if (typeof document.selection != "undefined") {
        document.selection.createRange().text = strCommand;
    }
    else {
        var start = destinationObj.selectionStart;
        var oldValue = destinationObj.value;
        destinationObj.value = oldValue.substring(0, start) + strCommand + oldValue.substring(start, oldValue.length);
        destinationObj.selectionStart = start;
        destinationObj.selectionEnd = start + strCommand.length;
    }
}

/////回车事件
// 用户在文本框输入时，将enter
document.onkeydown = function enterToTab(event) {
    event = event || window.event;
    if (event.keyCode == 13) {
        //alert("enter");
        var obj = document.activeElement;
        //alert(obj.id);
        if (obj.id == 'TxtSearch') {
            event.keyCode = 9;
            document.getElementById('imgBtnSearch2').onclick();
        } else {
            document.getElementById('BtnSearch').onclick();
        }
    }
}


function Select() {
    var SetValue = $("#chkAll").attr("checked"); //trun|false
    $("input[name='checkbox']").attr("checked", SetValue);

}