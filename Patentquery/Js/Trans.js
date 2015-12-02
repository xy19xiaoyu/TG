function transABS(obj, abs, Txtlang) {
    var myDialog = art.dialog({
        title: "翻译",
        follow: obj,
        width: '550px',
        content: '<img src="../Images/loading1.gif" alt="" style="margin-left:-20px;"/>&nbsp;&nbsp;&nbsp;&nbsp;翻译中,请稍候...',
        initialize: function () {
            if ($(obj).attr("title") != null) {
                this.content($(obj).attr("title"));
                return;
            }
            var text = $(obj).prev().html();
            var type = requestUrl("db");
            if (type == "") {
                type = $("#hidtype").val();
            }
            if (Txtlang != null) {
                type = Txtlang;
            }
            if (type.toUpperCase() == "CN") {
                type = 1;
            }
            else {
                type = 2;
            }
            // 1=汉英,2=英汉
            $.ajax({
                type: "POST",
                url: "../Trans/Translate.aspx/Translate",
                data: "{'inputContent':'" + text + "','type':'" + type + "'}",
                contentType: "application/json; charset=utf-8",
                timeout: 30000, // set time out 30 seconds
                dataType: "json",
                success: function (msg) {
                    var returnValue = msg.d;
                    if (returnValue == "failed") {
                        myDialog.content("翻译超时,没有翻译结果,,请稍后刷新页面再试！");
                    } else {
                        myDialog.content(returnValue);
                        $(obj).attr("title", returnValue);
                        //myDialog.size("400px","");
                    }

                }
            });
        },
        button: [
            {
                value: '谷歌翻译',
                callback: function () {                   
                    var text = $(obj).prev().html();
                    var type =  requestUrl("db");
                    if (type == "") {
                        type = $("#hidtype").val();
                    }
                    if (Txtlang != null) {
                        type = Txtlang;
                    }
                    if (type.toUpperCase() == "CN") {
                         var url = "http://translate.google.cn/?hl=zh-CN&#zh-CN/en/" + encodeURIComponent(text);
                    }
                    else {
                        var url = "http://translate.google.cn/?hl=zh-CN&#en/zh-CN/" + encodeURIComponent(text);
                    }
                   
                    window.open(url);
                    return false;
                }
            },
            {
                value: '百度翻译',
                callback: function () {
                var text = $(obj).prev().html();
                var type = requestUrl("db");
                    if (type == "") {
                        type = $("#hidtype").val();
                    }
                    if (Txtlang != null) {
                        type = Txtlang;
                    }
                    if (type.toUpperCase() == "CN") {
                         var url = "http://fanyi.baidu.com/#zh/en/" + encodeURIComponent(text);
                    }
                    else {
                         var url = "http://fanyi.baidu.com/#en/zh/" + encodeURIComponent(text);
                    }
                   
                    window.open(url);
                    return false;
                }
            },
            {
                value: '有道翻译',
                callback: function () {
                    var text = $(obj).prev().html();
                    var type = requestUrl("db");
                    if (type == "") {
                        type = $("#hidtype").val();
                    }
                    if (Txtlang != null) {
                        type = Txtlang;
                    }
                    if (type.toUpperCase() == "CN") {
                         var url = "http://fanyi.youdao.com/translate?i=" + encodeURIComponent(text);
                    }
                    else {
                         var url = "http://fanyi.youdao.com/translate?i=" + encodeURIComponent(text);
                    }
                   
                    window.open(url);
                    return false;
                }
            },
            {
                value: '确定',
                callback: function () {
                },
                focus: true,
                width: '60px'
            }
            ]
    });
    return myDialog;
}
