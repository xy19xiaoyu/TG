document.writeln("<script type='text/javascript' src='../Js/AjaxDoPatSearch.js'></script>");
document.writeln("<script type='text/javascript' src='../Js/SearchCommon.js'></script>");

//////-------------
function DoExpertSearchNew(_sdbType, _sFlag) {

    _sdbType = _sdbType.toUpperCase(); //转大写

    var strQuery = document.getElementById("SearchTextBox").value.Trim();

    if (strQuery == "") {
        showError("检索式为空！");
        return;
    }

    // 验证检索式   
    //strQuery = validateLogicSearchQuery(strQuery);
    var strEndFlag = getEndFlag(strQuery);
    if (validateQueryEndFlag(strEndFlag)) {
        strQuery = validateLogicSearchQuery(getSearchQuery(strQuery));
        strQuery = strQuery + strEndFlag;
    } else {
        return;
    }
    

    if (_sdbType != "CN") {
        var nQueryMaxLen = nEnQueryMaxLen;
    }

    if (strQuery == "") {
        // alert("检索式不合法！");    
        return;
    }
    else if (strQuery.indexOf("Error") != -1) {
        showError(strQuery);
        return;
    }
    else if (strQuery.length > nQueryMaxLen) {
        showError("检索式不能超过" + nQueryMaxLen + "个字符！");
        return;
    }

    document.getElementById("SearchTextBox").value = strQuery;

    //var res = "F XX (200302/AD + 200201/AD) * (计算机/TI)";
    //var res = "F XX ( (200302/AD + 200201/AD) * (计算机/TI)) + (200501/AD)";
    DoPatSearch("", strQuery, _sdbType, "2", "");
}


// 删除选中的检索式
function DeleteSelected() {
    // 从表格中获取选中的检索编号
    var checkboxArr = $("table :checkbox");

    var numArr = new Array();
    var maxNum = "-1";    // 是否需要更新最大检索编号，"-1"表示不更新
    for (var i = checkboxArr.length - 1; i >= 0; i--) {
        if (checkboxArr[i].checked == true) {
            numArr.push(checkboxArr[i].lang);
        }
    }
    if (numArr.length == 0) { alert("请选择检索式！"); return true; }
    if (!confirm("您确定删除选中的检索式么？")) return true;

    if (checkboxArr[checkboxArr.length - 1].checked == false) // 最大的检索编号已经删除，则需要更新
        maxNum = "-1";
    if (DeleteBatchSearchHis(numArr, maxNum) == false) {
        showError("检索式删除错误，请刷新后重新删除!");
        return false;
    }
    else {
        for (var i = checkboxArr.length - 1; i >= 0; i--) {
            if (checkboxArr[i].checked == true) // 删除对应id的table
            {
                var d = document.getElementById("tab" + checkboxArr[i].lang).parentNode;
                var index = d.parentNode.rowIndex;
                var p = d.parentNode.parentNode.parentNode;
                p.deleteRow(index);
            }
        }
        document.getElementById("select").checked = false;
        return true;
    }

}

// 批量删除检索式
function DeleteBatchSearchHis(numArr, maxNum) {

    var _numArr = numArr.toString();
    var nMaxNum = parseInt(maxNum, 10);

    $.ajax({
        type: "POST",
        url: "frmcnExpertSearch.aspx/DeleteBatchHis",
        data: "{'numArr':'" + _numArr + "','maxNum':'" + nMaxNum + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            return false;
        },
        success: function (msg) {
            var res = msg.d;
            return res;

        }
    });
}


//////////////////////////页面JS----cn|en 合并
$(document).ready(function () {
    $("li[id^='Lab']").each(function () {
        //alert($(this).attr("title"));
        try {
            var Flag = $("[name='RadioGroup1']:checked").val().trim();
            Flag = Flag == "中国专利" ? "CN_" : "EN_";

            $(this).attr("title", ExperEnTrancesTips[Flag + $(this).attr("lang").toUpperCase()]);
        } catch (e) { }
    });

    $("input, img, li,textarea").niceTitle({ showLink: false }); //要排除一些例外的元素，例如可以用a:not([class='nono'])来排除calss为"nono"的a元素
});
function addSearchEntrance(entrance) {
    var strCommand = entrance.toUpperCase();
    //JoinSQuery("/" + strCommand);
    JoinSQuery(strCommand);
}

function JoinSQuery(entrance) {
    var strCommand = entrance;
    var destinationObj = document.getElementById("SearchTextBox");
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

function ClearSearch() {
    document.getElementById("SearchTextBox").value = "";
}
function updateSearchHis() {
    document.getElementById("BtnSearchHisUpdate").click();
}

function SelectAll() {
    var SetValue = $("#select").attr("checked"); //trun|false

    //$('input[type=checkbox]').attr('checked', $(checkbox).attr('checked'));
    $("input[type='checkbox']").attr("checked", SetValue);

}

function checkHasSelect() {
    try {
        //获取到所有name为'chk_list'并选中的checkbox(集合)
        var arrChk = $("input[type='checkbox']:checked");
        var nSele = arrChk.length;
        if ($("#select").attr("checked")) {
            nSele = nSele - 1;
        }

        if (nSele < 1) {
            alert("请选择要导出的检索式!");
            return false;
        }
    } catch (e) {
        return false;
    }
}

function searchboxFocus() {
    document.getElementById("SearchTextBox").focus();
    document.getElementById("SearchTextBox").value = document.getElementById("SearchTextBox").value;
    //alert(document.getElementById("SearchTextBox").value);
}
// 用户在文本框输入时，将enter
document.onkeydown = function enterToTab(event) {
    event = event || window.event;
    if (event.keyCode == 13) {
        //alert("enter");
        var obj = document.activeElement;
        //alert(obj.id);
        if (obj.id == 'SearchTextBox') {
            event.keyCode = 9;
            //document.getElementById('BtnSearch').onclick();
            document.getElementById('BtnSearch').click();
        }
    }
}

function tablelength() {
    var obj = document.getElementById('Lab1');
    var obj2 = document.getElementById('searchEntranceCollection');
    var obj3 = document.getElementById('searchInteraction');
    alert("td:" + obj.style.width);
    alert("left table:" + obj2.style.width);
    alert("rigth table:" + obj3.style.width);
}
function changeb() {
    document.getElementById("BtnSearch").src = "Images/new_style/searchon.jpg";
}
function changeb2() {
    document.getElementById("BtnSearch").src = "Images/new_style/search.jpg";
}
function changeb3() {
    document.getElementById("BtnSearch").src = "Images/new_style/searchdown.jpg";
}

function addcommand(obj) {
    var strCommand = obj.name;
    JoinSQuery(strCommand);
}

    