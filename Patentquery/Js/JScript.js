var oInputField;    //考虑到很多函数中都要使用 
var oPopDiv;        //因此采用全局变量的形式 
var oColorsUl;
var xmlHttp;
function createXMLHttpRequest() {
    if (window.ActiveXObject)
        xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
    else if (window.XMLHttpRequest)
        xmlHttp = new XMLHttpRequest();
}
function initVars() {
    //初始化变量 
    oInputField = document.getElementById("txtKeyWord");
    oPopDiv = document.getElementById("popup");
    oColorsUl = document.getElementById("colors_ul");
}
function clearColors() {
    //清除提示内容 
    for (var i = oColorsUl.childNodes.length - 1; i >= 0; i--)
        oColorsUl.removeChild(oColorsUl.childNodes[i]);
    oPopDiv.className = "hide";

}
function LassFoc() {
    
    initVars();
    clearColors();
}
function setColors(the_colors) {
    //显示提示框，传入的参数即为匹配出来的结果组成的数组 
    clearColors();    //每输入一个字母就先清除原先的提示，再继续 
    oPopDiv.className = "show";
    var oLi;
    for (var i = 0; i < the_colors.length; i++) {
        //将匹配的提示结果逐一显示给用户 
        oLi = document.createElement("li");
        oColorsUl.appendChild(oLi);
        oLi.appendChild(document.createTextNode(the_colors[i]));

        oLi.onmouseover = function () {
            this.className = "mouseOver";    //鼠标经过时高亮 
        }
        oLi.onmouseout = function () {
            this.className = "mouseOut";    //离开时恢复原样 
        }
        oLi.onclick = function () {
            //用户点击某个匹配项时，设置输入框为该项的值 
            oInputField.value = this.firstChild.nodeValue;
            clearColors();    //同时清除提示框 
        }
    }
}
function findColors() {
    initVars();        //初始化变量 
    if (oInputField.value.length > 0) {
        createXMLHttpRequest();        //将用户输入发送给服务器 
        var sUrl = "AJAX.aspx?dbtype="+ dbtype +"&sheng=" + getSelectedValue('ctl00_ContentPlaceHolder1_ddlSheng') + "&flag=" + ary[selectedmenu] + "&sColor=" + encodeURI(oInputField.value);
        xmlHttp.open("GET", sUrl, true);
        xmlHttp.onreadystatechange = function () {
            if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
                var aResult = new Array();
                if (xmlHttp.responseText.length) {
                    aResult = xmlHttp.responseText.split(",");
                    setColors(aResult);    //显示服务器结果 
                }
                else
                    clearColors();
            }
        }
        xmlHttp.send(null);
    }
    else
        clearColors();    //无输入时清除提示框（例如用户按del键） 
}
function findTopColors() {
    oInputField = document.getElementById("txtKeyWord1");
    if (oInputField.value.length > 0) {
        createXMLHttpRequest();        //将用户输入发送给服务器 
        var sUrl = "AJAX.aspx?dbtype=" + dbtype + "&sheng=" + getSelectedValue('ctl00_ContentPlaceHolder1_ddlSheng') + "&topflag=" + selecttop + "&sColor=" + encodeURI(oInputField.value);
        xmlHttp.open("GET", sUrl, true);
        xmlHttp.onreadystatechange = function () {
            if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {
                var aResult = new Array();
                if (xmlHttp.responseText.length) {
                    aResult = xmlHttp.responseText.split(",");
                    setColors(aResult);    //显示服务器结果 
                }
                else
                    clearColors();
            }
        }
        xmlHttp.send(null);
    }
    else
        clearColors();    //无输入时清除提示框（例如用户按del键） 
}
function getSelectedValue(name) {
    var obj = document.getElementById(name);
    return obj.value;      //如此简单，直接用其对象的value属性便可获取到
}