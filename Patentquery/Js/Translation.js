
var nTransTxtMaxLeng = 3000;
var nTransTollsTxtMaxLeng = 30;

function OpenTrsUrl(type, UrlC2E, UrlE2C, text) {
    var url = "";
    if (type == "1") {
        url = UrlC2E + encodeURIComponent(text);
    }
    else {
        url = UrlE2C + encodeURIComponent(text);
    }
    window.open(url);
}

//翻译
function translationCustomServices(type, yqsrc) {

    var item = $("input[type=radio][checked]").val();

    $("#loading11").show();

    $("#loading22").show();

    var bTranslated = true;

    if (type == "1") {//汉英
        var obj = $("#ctl00_ContentPlaceHolder1_txtInputContent"); //汉原文
        var oldObj = $("#ctl00_ContentPlaceHolder1_txtTranslateContent"); //英翻译结果
        var loadbar = $("#loading11");
        $("#loading11").show();
    } else {//英汉
        var obj = $("#ctl00_ContentPlaceHolder1_txtInputContent1"); //英原文
        var oldObj = $("#ctl00_ContentPlaceHolder1_txtTranslateContent1"); //汉翻译结果
        var loadbar = $("#loading22");

    }
    //    alert(loadbar);    loadbar.toggle();
    if (!!oldObj) {
        oldObj.val("");
    }

    if (!!obj) {
        var text = obj.val();

        if (text == "") { //没有输入内容,点翻译按钮了
            alert("请输入翻译内容");
            $("#loading11").hide();

            $("#loading22").hide();
            return;

        } else {//有翻译的内容
            if (text.length >= nTransTxtMaxLeng) {
                alert("您输入的文字长度为" + text.length + "字,输入的内容太长,只能翻译" + nTransTxtMaxLeng + "字以内的内容.");

                $("#loading11").hide();

                $("#loading22").hide();
                return;
            }
        }

        if (yqsrc) {
            var c2eUrl = "";
            var e2cUrl = "";
            switch (yqsrc) {
                case "bd":
                    c2eUrl = "http://fanyi.baidu.com/#zh/en/";
                    e2cUrl = "http://fanyi.baidu.com/#en/zh/";
                    break;
                case "yd":
                    c2eUrl = "http://fanyi.youdao.com/translate?i=";
                    e2cUrl = "http://fanyi.youdao.com/translate?i=";
                    break;
                case "gl":
                    c2eUrl = "http://translate.google.cn/?hl=zh-CN&#zh-CN/en/";
                    e2cUrl = "http://translate.google.cn/?hl=zh-CN&#en/zh-CN/";
                    break;
            }
            OpenTrsUrl(type, c2eUrl, e2cUrl, text);
            $("#loading11").hide();
            $("#loading22").hide();
            return;
        }


        if (item == "cprs") {//机器翻译引擎
            // 1=汉英,2=英汉
            $.ajax({
                type: "POST",
                url: "Translate.aspx/Translate",
                data: "{'inputContent':'" + text + "','type':'" + type + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var returnValue = msg.d;
                    if (returnValue == "failed") {
                        oldObj.val("翻译超时,没有翻译结果");

                    } else {
                        oldObj.val(returnValue);
                    }


                    $("#loading11").hide();

                    $("#loading22").hide();

                }
            });

        } else {//送google
            // 1=汉英,2=英汉
            $.ajax({
                type: "POST",
                url: "Translate.aspx/Translate2Google",
                data: "{'inputContent':'" + text + "','type':'" + type + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var returnValue = msg.d;
                    if (returnValue == "failed") {
                        oldObj.val("翻译超时,没有翻译结果");

                    } else {
                        oldObj.val(returnValue);
                    }


                    $("#loading11").hide();

                    $("#loading22").hide();

                }
            });
        }
    }
    if (!!oldObj) {
        bTranslated = oldObj.val() === "";
    }
}

//清空
function clearInputContent(type) {

    if (type == "1") {//汉英
        var inputObj = $("#ctl00_ContentPlaceHolder1_txtInputContent"); //汉原文
        var targetObj = $("#ctl00_ContentPlaceHolder1_txtTranslateContent"); //英翻译结果
    } else {//英汉
        var inputObj = $("#ctl00_ContentPlaceHolder1_txtInputContent1"); //英原文
        var targetObj = $("#ctl00_ContentPlaceHolder1_txtTranslateContent1"); //汉翻译结果
    }
    if (!!inputObj) {
        inputObj.val("");
    }

    if (!!targetObj) {
        targetObj.val("");
    }
}

$(document).ready(function () {
    $("#loading").hide()
});


//翻译小工具
function TranslateTool() {

    $("#atrans").hide();
    $("#loading").show();

    var type = $('input[name="TransType"]:checked').val();

    var bTranslated = true;

    var obj = $("#ctl00_ContentPlaceHolder1_txtWord"); //原文//ctl00_ContentPlaceHolder1_txtWord
    var oldObj = $("#ctl00_ContentPlaceHolder1_lbTransResult"); //翻译结果

    //alert(oldObj.val());

    if (!!oldObj) {
        oldObj.val("");
    }

    if (!!obj) {
        var text = obj.val();

        if (text == "") { //没有输入内容,点翻译按钮了
            alert("请输入翻译内容");
            $("#atrans").show();
            $("#loading").hide();
            return;

        } else {//有翻译的内容
            if (text.length >= nTransTollsTxtMaxLeng) {
                alert("您输入的文字长度为" + text.length + "字,输入的内容太长,只能翻译" + nTransTollsTxtMaxLeng + "字以内的内容.");
                $("#atrans").show();
                $("#loading").hide();
                return;
            }
        }

        // 1=汉英,2=英汉
        $.ajax({
            type: "POST",
            url: "Translate.aspx/Translate",
            data: "{'inputContent':'" + text + "','type':'" + type + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var returnValue = msg.d;
                if (returnValue == "failed") {
                    oldObj.val("翻译超时,没有翻译结果");
                } else {
                    // alert(returnValue);
                    oldObj.val(returnValue);
                }
                $("#atrans").show();
                $("#loading").hide();
            }
        });
    }

}

