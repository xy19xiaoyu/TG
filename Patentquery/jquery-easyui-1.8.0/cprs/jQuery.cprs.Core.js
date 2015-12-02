/// <reference path="JQuery/jquery-1.3.2-vsdoc.js" />

/*****************************************************************
* 文　件: jQuery.cprs.Core
* 说　明: 一些公共方法
* ****************************************************************
* 创建者: xiwenlei
* Email : xiwenlei@cnpat.com.cn
*****************************************************************/


jQuery.fn.extend({
    toHTML: function() {
        var obj = $(this[0]);
        if (obj[0].outerHTML) {
            return obj[0].outerHTML;
        }
        else {
            if ($('.cprs-hidearea') == null || $('.cprs-hidearea')[0] == null) {
                $('body').append("<div class='cprs-hidearea' style='display:none;'></div>");
            }
            $('.cprs-hidearea').css('display', 'none');
            $('.cprs-hidearea').html('');
            obj.clone(true).prependTo('.cprs-hidearea');
            var rs = $('.cprs-hidearea').html();
            $('.cprs-hidearea').html('');
            return rs;
        }
    }
});



//////////////////////////////////////


function ConvertDataTable(jsondata) {
    return jsonStringToDataTable(jsondata);
}


function jsonStringToDataTable(jsondata) {
    try {
        var table = eval("(" + jsondata + ")");
        return table;
    }
    catch (ex) {
        return null;
    }
}



function querystring() {
    try {
        var url = unescape(window.location.href);
        var allargs = url.split("?")[1];
        var args = allargs.split("&");
        for (var i = 0; i < args.length; i++) {
            var arg = args[i].split("=");
            eval('this.' + arg[0] + '="' + arg[1] + '";');
        }
    }
    catch (ex)
    { }
}



//扩展String
String.prototype.trim = function() {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}

//去除字符串头部空格或指定字符
String.prototype.trimStart = function(c) {
    if (c == null || c == "") {
        var str = this.replace(/(^\s*)/, '');
        return str;
    }
    else {
        var rg = new RegExp("^" + c + "*");
        var str = this.replace(rg, '');
        return str;
    }
}

//去除字符串尾部空格或指定字符
String.prototype.trimEnd = function(c) {
    if (c == null || c == "") {
        var str = this;
        var rg = /\s/;
        var i = str.length;
        while (rg.test(str.charAt(--i)));
        return str.slice(0, i + 1);
    }
    else {
        var str = this;
        var rg = new RegExp(c);
        var i = str.length;
        while (rg.test(str.charAt(--i)));
        return str.slice(0, i + 1);
    }
}

//
String.prototype.Replace = function(str1, str2) {
    var rs = this.replace(new RegExp(str1, "gm"), str2);
    return rs;
}

////////////////////////////////
String.prototype.escape = function() {
    return escape(this);
}
String.prototype.unescape = function() {
    return unescape(this);
}

Date.prototype.format = function(format) {
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(),    //day
        "h+": this.getHours(),   //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": this.getMilliseconds() //millisecond
    }
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
 (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
 RegExp.$1.length == 1 ? o[k] :
 ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}

///////////////////////////

///////////////////////////////////////////

//可访问全局变量的Eval ,eval只能访问 局部变量
var Eval = function(code) {
    if (!(window.attachEvent && !window.opera)) {
        //ie
        execScript(code);
    }
    else {
        //not ie
        window.eval(code);
    }
}

//实现一个Include
function Include(path, reload) {
    var scripts = document.getElementsByTagName("script");
    if (!reload)
        for (var i = 0; i < scripts.length; i++)
        if (scripts[i].src && scripts[i].src.toLowerCase() == path.toLowerCase()) return;
    var sobj = document.createElement('script');
    sobj.type = "text/javascript";
    sobj.src = path;
    var headobj = document.getElementsByTagName('head')[0];
    headobj.appendChild(sobj);
}


/****************************************
*方法说明：表格排序
*参数：     tableSelector 要排序的表格
*           colindex 排序列
*           dataType 数据类型
*           orderType   排序类型,1正序，-1倒序,默认为-1
*           pstattr  起始行
*           pendtr   结束行
******************************************/
function sortTable(tableSelector, colIndex, dataType, orderType, pstatTr, pendTr) {

    //选择表格  
    var table = $(tableSelector);
    var allRows = table.find('tr');
    var oleFirstRow = allRows.slice(0, 1).clone(true);

    //设置排序方式
    var order = -1;
    if (orderType != null) {
        order = orderType;
        if (table.data('sortCol') != null && table.data('sortCol') == colIndex && table.data('order') == order) {
            return;
        }
    } else {
        if (table.data('sortCol') != null && table.data('sortCol') == colIndex) {
            order = parseInt(table.data('order')) * -1;
        }
    }

    //设置排序的范围
    var startTr = 0;
    var endTr = allRows.length;
    if (pendTr != null && pendTr < endTr) endTr = pendTr + 1;
    if (pstatTr != null && pstatTr > startTr) startTr = pstatTr;
    if (startTr > endTr) startTr = endTr;

    //排序
    var sortRows = allRows.slice(startTr, endTr);
    sortRows.sort(compareEle2(colIndex, dataType, order));
    //
    var sortRowsClone = sortRows.clone(true);
    var si = 0;
    for (i = startTr; i < endTr; i++) {
        $(allRows[i]).replaceWith($(sortRowsClone[si]));
        si++;
    }

    table.data('sortCol', colIndex);
    table.data('order', order);
    //

    table.find('tr').find('td').width('auto');
    var oleFirstRowTds = oleFirstRow.find('td');
    var newFirstRowTds = table.find('tr').slice(0, 1).find('td');
    newFirstRowTds.each(function() {
        var itemtd = $(this);
        itemtd.css('width', $(oleFirstRowTds[newFirstRowTds.index(itemtd)]).css('width'));
    });

}
//-------------------------------------------------------------------------------------------    

////CPRS概览结果数据表格排序
function compareEle2(colIndex, dataType, order) {
    return function(oTR1, oTR2) {
        var rs = 0;
        var spanIndex = colIndex;
        if (colIndex > 0) {
            var vDiv1 = oTR1.cells[0].childNodes[0].childNodes[1];
            var vDiv2 = oTR2.cells[0].childNodes[0].childNodes[1];
            spanIndex = colIndex * 2 - 1;
        } else {
            var vDiv1 = oTR1.cells[0].childNodes[0].childNodes[0];
            var vDiv2 = oTR2.cells[0].childNodes[0].childNodes[0];
            spanIndex = 1;
        }

        var vValue1 = convertDataType(getCellsValue(vDiv1.childNodes[spanIndex]), dataType);
        var vValue2 = convertDataType(getCellsValue(vDiv2.childNodes[spanIndex]), dataType);

        if (vValue1 < vValue2) {
            rs = -1;
        } else if (vValue1 > vValue2) {
            rs = 1;
        } else {
            rs = 0;
        }
        return rs * order;
    };
}


//表格通用排序--按单元格的值进行排序
function compareEle(colIndex, dataType, order) {
    return function(oTR1, oTR2) {
        var rs = 0;
        var vValue1 = convertDataType(getCellsValue(oTR1.cells[colIndex]), dataType);
        var vValue2 = convertDataType(getCellsValue(oTR2.cells[colIndex]), dataType);
        if (vValue1 < vValue2) {
            rs = -1;
        } else if (vValue1 > vValue2) {
            rs = 1;
        } else {
            rs = 0;
        }
        return rs * order;
    };
}

//类型转换
function convertDataType(sValue, dataType) {
    switch (dataType) {
        case "int":
            return parseInt(sValue);
        case "float":
            return parseFloat(sValue);
        case "date":
            return new Date(Date.parse(sValue));
        default:
            return sValue.toString();
    }
}


///得到单元格的内容
function getCellsValue(ob) {
    if (ob.textContent != null)
        return ob.textContent;
    var s = ob.innerText;
    return s.substring(0, s.length);
}

//←→↑↓
//[ ∧ ] logical and = wedge 
//[name: &and;] [number: &#8743;] 
//[ ∨ ] logical or = vee 
//[name: &or;] [number: &#8744;] 
//[ ← ] leftwards arrow 
//[name: &larr;] [number: &#8592;] 
//[ ↑ ] upwards arrow 
//[name: &uarr;] [number: &#8593;] 
//[ → ] rightwards arrow 
//[name: &rarr;] [number: &#8594;] 
//[ ↓ ] downwards arrow 
//[name: &darr;] [number: &#8495;] 
//▲  ▼