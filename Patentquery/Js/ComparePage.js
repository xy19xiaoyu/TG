
//标引词汇
function GetClaims() {
    try {

        if ($("[id$='hidClaims']").val() != "") {
            var data = $("[id$='hidClaims']").val();
            $("#claim1").html(data.d[0]);
            $("#claim2").html(data.d[1]);
            return;
        }
        //alert(requestUrl('Ids'));
        $.ajax({
            type: "POST",
            url: "CompareClaims.aspx/getClaims", //?Ids=" + requestUrl('Ids') + "&type=" + requestUrl('type'),
            data: "{'Ids':'" + requestUrl('Ids') + "','_type':'" + requestUrl('type') + "'}",
            timeout: 30000,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                var msg = XMLHttpRequest.responseHTML;
                if (textStatus == "timeout") {
                    msg = "检索超时，请稍后再试！";
                }
                $("[id$='divclaim']").html("未加载权利要求内容!")

                $("[id$='hidClaims']").val("未加载权利要求内容!")
            },
            success: function (msg) {
                $("#tableClaims").show();
                $("#imgloading").hide();                
                $("#claim1").html(msg.d[0]);
                $("#claim2").html(msg.d[1]);
                $("[id$='hidClaims']").val(msg.d)
                
            }
        });
    } catch (e) {
    }
}

function GetDes() {
    try {

        if ($("[id$='hidDes']").val() != "") {
            var data = $("[id$='hidDes']").val();
            $("#divDes1").html(data.d[0]);
            $("#divDes2").html(data.d[1]);
            return;
        }
        //alert(requestUrl('Ids'));
        $.ajax({
            type: "POST",
            url: "CompareClaims.aspx/getDes", //?Ids=" + requestUrl('Ids') + "&type=" + requestUrl('type'),
            data: "{'Ids':'" + requestUrl('Ids') + "','_type':'" + requestUrl('type') + "'}",
            timeout: 30000,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                var msg = XMLHttpRequest.responseHTML;
                if (textStatus == "timeout") {
                    msg = "检索超时，请稍后再试！";
                }
                $("[id$='divDes']").html("未加载权利要求内容!")

                $("[id$='hidDes']").val("未加载权利要求内容!")
            },
            success: function (msg) {
                $("#tabDes").show();
                $("#divImglogingDes").hide();
                $("#divDes1").html(msg.d[0]);
                $("#divDes2").html(msg.d[1]);
                $("[id$='hidDes']").val(msg.d)

            }
        });
    } catch (e) {
    }
}

function IniteClaims() {
    try {
        var _type = requestUrl('type');
        if (_type != "CN") {
            $("#hclaim").hide();
            $("#divclaim").hide();
            $("#gonggao").hide();
            $("#hDes").hide();
            $("#divDes").hide();            
        }
        $("#accordion").accordion({
            heightStyle: "content",
            activate: function (event, ui) {
                switch ($(ui.newHeader).text().trim()) {
                    case '权利要求书':
                        GetClaims();
                        return;
                    case "说明书":
                        GetDes();
                        break;
                }
            }
        });
        
        DialogUiLoading.close();   //throw a error, 
    } catch (e) {
    }
}