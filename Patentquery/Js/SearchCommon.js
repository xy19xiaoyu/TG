﻿var SearchDbType = "cn";

function getMergeSearchEndFlag(res, chkname, dbtype) {

    var checkboxname = 'checkbox';
    if (arguments.length >= 2) {
        checkboxname = chkname;
    }

    if (dbtype) {
        SearchDbType = dbtype;
    }

    var EndFlag = "";
    try {

        var selectLen = $("input[name='" + checkboxname + "']:checked").length;
        if (selectLen > 0 && selectLen < $("input[name='" + checkboxname + "']").length) {
            $("input[name='" + checkboxname + "']:checked").each(function () {
                EndFlag += $(this).val().trim() + ",";
            });
            EndFlag = EndFlag.substring(0, EndFlag.length - 1).replace("ELSE", "OT");

            if (EndFlag != "") {
                res += SearchDbType.toUpperCase() == "CN" ? "@LX=" : "@CO=";
                res += EndFlag;
            }
        }

        EndFlag = "";
        $("input[name='LegDbType']:checked").each(function () {
            EndFlag += $(this).val().trim() + ",";
        });
        if (EndFlag != "") {
            res += SearchDbType.toUpperCase() == "CN" ? EndFlag.substring(0, EndFlag.length - 1) : "";
        }

    } catch (e) {
    }

    return res;
}

var CN_ENDFlag = "DI,UM,DP,AI,";
var EN_ENDFlag = "CN,US,EP,JP,DE,GB,RU,FR,KR,CH,WO,OT,";

function validateQueryEndFlag(strEndFlag) {
    if (strEndFlag == "") {
        return true;
    }
    strEndFlag = strEndFlag.toUpperCase();
    //strEndFlag = SearchDbType.toUpperCase() == "CN" ? strEndFlag.replace("@LX=", "") : strEndFlag.replace("@CO=", "")

    var strErroFlag = "专利类型[";
    var ObjENDFlag = CN_ENDFlag;
    if (SearchDbType.toUpperCase() == "CN") {
        strEndFlag = strEndFlag.replace("@LX=", "").replace("@SX", "").replace("@YX", "");
    } else {
        strErroFlag = "国别/组织代码[";
        ObjENDFlag = EN_ENDFlag;
        strEndFlag = strEndFlag.replace("@CO=", "");
    }

    var arryEndFlag = strEndFlag.split(",")
    for (i = 0; i < arryEndFlag.length; i++) {
        if (ObjENDFlag.indexOf(arryEndFlag[i] + ",") < 0)   //分割后的字符
        {
            showError(strErroFlag + arryEndFlag[i] + "]非法!");
            return false;
        }
    }
    return true;
}

//(.*?)(@..=.*|@YX.*|@WX.*)
var regSearchValidate = SearchDbType.toUpperCase() == "CN" ? /(.*?)(@LX=.*|@YX.*|@SX.*)/i : /(.*?)(@CO=.*|@YX.*|@SX.*)/i;

function InitCommon(SDbType) {
    SearchDbType = SDbType;
    regSearchValidate = SearchDbType.toUpperCase() == "CN" ? /(.*?)(@LX=.*|@YX.*|@SX.*)/i : /(.*?)(@CO=.*|@YX.*|@SX.*)/i;
}

//获取检索式尾部的信息， 用于类型和国别分索引查询
function getEndFlag(res) {
    var rsReg;
    var strEndFlag = "";
    if (regSearchValidate.test(res)) {
        rsReg = regSearchValidate.exec(res);
        strEndFlag = rsReg[2];
    }
    return strEndFlag;
}

//获取检索式部分， 除去国别和类型
function getSearchQuery(res) {
    var rsReg;
    var strQuery = res;
    if (regSearchValidate.test(res)) {
        rsReg = regSearchValidate.exec(res);
        strQuery = rsReg[1];
    }
    return strQuery;
}