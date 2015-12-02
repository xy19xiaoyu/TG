document.write("<script type='text/javascript' src='../jquery-easyui-1.8.0/plugins/jquery.funkyUI.js'></script>");
// 显示检索进度框
function showProgress() {
    //setBtnState("disabled");
    //    var bar = document.getElementById("bar");
    //	bar.style.display="block";
    //	bar.style.top = (screen.availHeight-bar.offsetHeight)/2-200;
    //	bar.style.left = document.body.clientWidth/2-210;
    //	document.body.style.cursor="wait";
    $.funkyUI({
        id: Math.round(Math.random() * 10000),
        type: "CN",
        showDialog: false
    });
    return;
}

// 撤销检索进度框
function closeProgress() {
    $.unfunkyUI();
    //    setBtnState("");
    //	var bar = document.getElementById("bar");
    //	bar.style.display = "none";
    //	bar.style.top = document.body.clientHeight;
    //	bar.style.left = document.body.clientWidth;
    //	document.body.style.cursor="default";
    return;
}


//
function setBtnState(_bEnable) {
    //document.getElementById("BtnSearch").disabled=_bEnable;       
}