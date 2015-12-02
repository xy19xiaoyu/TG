//--------------------------------------专家检索-------------------------------------------//

function cnExpertSearchValidate()
{
    var strQuery = document.getElementById("TextBoxPattern").value.Trim();
    if (strQuery == "") {
        showError("检索式为空！");
        return false;
    }

    // 验证检索式
    strQuery = validateLogicSearchQuery(strQuery);
    if (strQuery == "") {
        // alert("检索式不合法！");    
        return false;
    }
    else if (strQuery.indexOf("Error") != -1) {
        showError(strQuery);
        return false;
    }
    else if (strQuery.length > 255) {
        showError("检索式不能超过255个字符！");
        return false;
    }

    document.getElementById("TextBoxPattern").value = strQuery;
    // 显示进度条
    showProgress();
    return true;
}

//return
//1、 ajax页面超时。在ajax请求字段中设置"timeout: 30000,"，在返回的error中处理超时错误；
//2、 SocketSearch返回错误：1、 请求已经超时!；2、检索时发生错误!
//3、 Main主机返回的超时信息
function cnExpertSearch() {

    var strQuery = document.getElementById("TextBoxPattern").value.Trim();
    
    if ( strQuery == "" )
    {
        showError("检索式为空！");
        return;
    }

    // 验证检索式
    strQuery = validateLogicSearchQuery(strQuery);
    
    if ( strQuery == "" )
    {
        // alert("检索式不合法！");    
        return;
    }
    else if(strQuery.indexOf("Error") != -1)
    {
        showError(strQuery);
        return;
    }
    else if (strQuery.length > 255)
    {
        showError("检索式不能超过255个字符！");
        return;
    }

    document.getElementById("TextBoxPattern").value = strQuery;
    strQuery = encodeURIComponent(strQuery);
   
    // 显示进度条
    showProgress();
    $.ajax({
        type: "POST",
        url: "cnExpertSearch.aspx/expertSearch",
        data: "{'strSearchQuery':'" + strQuery + "'}",
        timeout: 30000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function(XMLHttpRequest, textStatus, errorThrown) {
            closeProgress();
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "检索超时，请稍后再试！";
            }

            showError(msg);
        },
        success: function(msg) {
            // 撤销进度条
            closeProgress();

            strMsg = msg.d;
            if (strMsg.indexOf("<hit") != -1) {
                // 更新检索历史
                updateSearchHis();
                // 将焦点设为检索框
                searchboxFocus();
            }
            else {
                showError(strMsg);
            }
        }
    });
}


//--------------------------------------专家检索检索历史-------------------------------------------//
// strHis 是从服务器返回的检索历史，格式为 YYYY-MM-DD: (003) F AD 200301 <hits:12345>
function addHistoryItem(strHis)
{
    // 插入检索式
    var objTable = document.getElementById("cnSearchHistoryGrid");
    
    var r = objTable.insertRow();
    r.insertCell().innerHTML = strHis;  
    // 获得嵌套检索历史的div
    var tableDiv = document.getElementById("cnSearchHisTable");
    // 设置滚动条自动下滑
    tableDiv.scrollTop = tableDiv.scrollHeight - tableDiv.clientHeight;
      
}

function formatTableScrollBar()
{
    var tableDiv = document.getElementById("cnSearchHisTable");
    if (tableDiv.scrollHeight > tableDiv.clientHeight)
        tableDiv.scrollTop = tableDiv.scrollHeight - tableDiv.clientHeight;
}

// 删除指定检索式
function deleteHis(num)
{
    // alert("确定删除检索式: "+ num + "?");
    $.ajax({
        type: "POST",
        url: "cnExpertSearch.aspx/cnDeleteHis",
        data: "{'num':'" + num + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg) { 
            var res = msg.d;
            
            if( res == true)    // 清空页面，设置检索编号
            {
                var objTable = document.getElementById("cnSearchHistoryGrid");
                for (i=objTable.rows.length-1; i>0; i--)
                {
                    var strQuery = objTable.rows[i].cells[0].innerText;
                    var pat = "(" + num + ")";
                    if (strQuery.indexOf(pat) != -1)
                        objTable.deleteRow(i);
                }
            } 
        }
    }); 
}
    
// 删除选中的检索式
function DeleteSelected()
{
    // 从表格中获取选中的检索编号
    var checkboxArr = $("table :checkbox");
    
    var numArr = new Array();
    var maxNum = "-1";    // 是否需要更新最大检索编号，"-1"表示不更新
    for (var i=checkboxArr.length -1; i>=0; i--)
    {
	    if (checkboxArr[i].checked == true)
	    {
		    numArr.push(checkboxArr[i].lang);
		    if (i!=0)
		    {
		        var tempNum = checkboxArr[i-1].lang
		        var nTempMaxNum =  parseInt(tempNum,10) + 1;
		        maxNum = nTempMaxNum + "";
		        
		    }
		    else 
		    {
		        maxNum = "001";
		    }
		}
    }
    if (checkboxArr[checkboxArr.length -1].checked == false) // 最大的检索编号已经删除，则需要更新
        maxNum = "-1";
    if ( DeleteBatchSearchHis(numArr, maxNum) == false )
    {
        showError("检索式删除错误，请刷新后重新删除!");
        return false;
    }
    else {  
        for (var i= checkboxArr.length -1; i>=0; i--)
        {
	        if (checkboxArr[i].checked == true) // 删除对应id的table
	        {
    	       var d = document.getElementById(checkboxArr[i].lang).parentNode;
    	       var index =d.parentNode.rowIndex; 
    	       var p = d.parentNode.parentNode.parentNode;
    	       p.deleteRow(index);
    	    }
        }
        return true;
    }
    
}

// 批量删除检索式
function DeleteBatchSearchHis(numArr, maxNum)
{
    
    var _numArr = numArr.toString();
    var nMaxNum = parseInt(maxNum,10);
    
    $.ajax({
        type: "POST",
        url: "cnExpertSearch.aspx/cnDeleteBatchHis",
        data: "{'numArr':'" + _numArr + "','maxNum':'" + nMaxNum + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error:function(XMLHttpRequest, textStatus, errorThrown) {
            return false;
        },
        success: function(msg) { 
            var res = msg.d;
            
            return res;
            
        }
    }); 

}

function viewHis(hyperlink)
{
    var reg = /[no|No]=\s*(\d+)/;
    var nmReg =  /[nm|Nm]=\s*(\d+)/;
    var name = "";
    if (reg.test(hyperlink))
    {
        name = RegExp.$1;
        
    }
    var nm = "";
    if (nmReg.test(hyperlink))
    {
        nm = RegExp.$1;
        
    }
    if (parseInt(nm) == 0)
        alert("检索结果为零");
    else
    {
        name = "CN"+ name;
        addToTab(hyperlink,name,true);
    }
}
    


// 清空检索式
function clearSearchHistory()
{
    if (!confirm("您确定清除检索式？")) return;
    $.ajax({
        type: "POST",
        url: "cnExpertSearch.aspx/cnClearSearchHistory",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg) { 
            var res = msg.d;
            
            if( res == true)    // 清空页面，设置检索编号
            {
                
                var objTable = document.getElementById("cnSearchHistoryGrid");
                for (i=objTable.rows.length-1; i>0; i--)
                {
                    objTable.deleteRow(i);
                }
                showError("检索式已清空！");
            } 
        }
    }); 
}

//--------------------------------------检索式验证-------------------------------------------//

// 针对检索入口进行验证，错误则返回错误提示，以“Error”开头，否则即为修改后的正确检索式
// 验证的内容针对检索入口及检索入口的内容是否合法验证，并予以适当修改
// 不进行页面提示设置
// 以下都是合法的检索式：1， F  TI 计算机； 2， F XX (计算机/TI)*(2008/PD)； 3，计算机 TI；4，(计算机/TI)*(2008/PD)； 5， F XX (计算机/TI)；
// var res = "F XX (200302/AD + 200201/AD) * (计算机/TI)";
// var res = "F XX ( (200302/AD + 200201/AD) * (计算机/TI)) + (200501/AD)";
var m_Entrances = "AN,PN,IC,PR,IN,PA,AG,TI,CL,AD,PD,CT,CO,DZ,TX,AB,DS,CS,GN,GD,AE,IE,PE,TE,"; //所有入口,以，连接多个
function validateLogicSearchQuery( strSearchQuery )
{
    var strQuery = strSearchQuery.Trim().ReduceSpace();
    if (strQuery == "")
        return "";
    var strQueryHead = "";
    var strQueryBody = "";
    
    // 切割检索式
    var splitReg = /^\s*([J|F|j|f]\s*[XX]*)(.*)$/;
    if ( splitReg.test(strQuery) ) // 带检索头
    {
        strQueryHead = RegExp.$1.toUpperCase();
        strQueryBody = RegExp.$2;
        strQueryHead = strQueryHead.Trim();
        strQueryBody = strQueryBody.Trim();
    }
    else // 不带检索头
    {
        strQueryBody = strQuery;
    } 
      
    // 验证检索式
    if ( strQueryHead == "J") // 交叉检索，客户端不予验证
    {
        
        return strQueryHead + " " + strQueryBody;
    }
    else    
    {
        var simpleSearchReg = /^([A-Za-z]{2})\s+([^\/].*)$/; 
       
        if ( simpleSearchReg.test(strQueryBody) ) // 简单检索
        {
            strQueryHead = "F"; 
            var key = RegExp.$1.toUpperCase();  
            var value = RegExp.$2; 
            var res = getValueAccordingToKey(key, value);
            if ( res.substring(0,5) == "Error" )    
            {
                return res;
            }
            else
                strQueryBody = key + " " + res;     
        } 
        else if ( strQueryBody.indexOf("/") == -1) // 不是简单检索又不包含符号/，则认为它是交叉检索，比如2+3
        {
            var regJ = /[\-|+|*]/;
            if (regJ.test(strQueryBody))    // 交叉检索
                return "J " + strQueryBody;
            else 
                return "Error:检索式不合法";
        }
        else  // 复杂检索，带/连接符号
        {
        // 从检索式中循环找符合正则式/\/\s*[A-Za-z]{2}/的结构，该结构即为检索入口
        // 向前寻找检索入口对应的内容，遇到以下字符则停止
        // 1    [+|-|*-];   2   [(|)]   3  字符串头
        // 找到后，消除检索内容之前的空白和括号等，然后进行验证和修改
        
            strQueryHead = " F XX"
            strQueryBodyTemp = strQueryBody;
            
            var getValueKeyReg = /\/\s*([A-Za-z]{2})/g; 
            var strResult = ""; 
            var begin = 0;
            var end = 0;
            
            
            while( strResult = getValueKeyReg.exec(strQueryBody) )
            {   
                var key = RegExp.$1.toUpperCase();
                var oldKey = RegExp.$1;
                end = strResult.index;
                var value = strQueryBody.substring(begin, end);
                begin = strResult.lastIndex;
                
                // 去掉value前后的空格以及+-*(符号
                value = value.TrimLink();
                
                
                var res = getValueAccordingToKey(key, value); // 验证并修改检索式 
                if ( res.substring(0,5) == "Error" ) 
                {
                    return res;
                }
                else 
                {
                    
                    var zhuanyiValue = value.replace("+", "\\+");
                    zhuanyiValue = zhuanyiValue.replace("-", "\\-");
                    zhuanyiValue = zhuanyiValue.replace("*", "\\*");
                    zhuanyiValue = zhuanyiValue.replace("(", "\\(");
                    zhuanyiValue = zhuanyiValue.replace(")", "\\)");
                    zhuanyiValue = zhuanyiValue.replace("（", "\\（");
                    zhuanyiValue = zhuanyiValue.replace("）", "\\）");
                    var strReg = zhuanyiValue + "\\s*\\\/\\s*" + oldKey; 
                    var reg = new RegExp(strReg,"gi");
                    strQueryBodyTemp = strQueryBodyTemp.replace(reg, res+"/"+key);
                }

            }
            strQueryBody = strQueryBodyTemp.Trim();
            
        } 
    }

    return strQueryHead.Trim() + " " + strQueryBody.Replace("'", " ");

}

// 测试是否为合法的检索入口，测试对应的检索入口内容是否符合规范
// 格式错误，返回"Error:"开头的字符串字符串，如果正确，则返回修改后的strVal,8510 + 8610/AD将变为198510/AD + 198610/AD
// value 是可以嵌套括号的，因此验证的时候需要去掉括号和空白
function getValueAccordingToKey(key, value) {
    var entrances = m_Entrances;
    if ( entrances.indexOf( key+"," ) == -1 )
    {
        return "Error:非法检索入口!";
     }  
     
    var txtId = key;
    var strVal = value.split(/[\-|+|*]/);    // 逻辑链接符
    var linkArr = value.match(/[\-|+|*]/g);    // 逻辑链接符

        
    // strRes = "Error:禁止包含非法字符:/,not,and or!";

    for(var i = 0; i<strVal.length; i++) {
        var maxInputLength = 200; 
        if (strVal[i].length > maxInputLength)
        {
            var resTemp = "Error:检索入口字符串长度不能超过";
            resTemp = resTemp + maxInputLength + "个字符！";
            return resTemp;
        }
        var regExp1 = /\s+and\s+/g;
        var regExp2 = /\s+not\s+/g;
        var regExp3 = /\s+or\s+/g;
        if ( regExp1.test(strVal[i]) || regExp2.test(strVal[i]) || regExp3.test(strVal[i])) {
            return "Error:禁止包含非法字符:not,and or!";
        }
	}
	
	//strRes = "Error:申请号必须为数字，且长度为4-12位!";
    if ( txtId == "AG" )
    {
        for(var i = 0; i<strVal.length; i++) {
            var valueTemp = strVal[i].Trim();
            valueTemp = valueTemp.TrimLink();
            var reg = /^\d{5}$/;
            if (!reg.test(valueTemp)) {
                return "Error:请输入正确的代理机构代码!";
            }
            else if ("00000" == value) {
                return 'Error:代理机构代码不能为"00000"';
            }
        }
    }
    //
    if (txtId == "AE" || txtId == "IE" || txtId == "PE" || txtId == "TE") {
        for (var i = 0; i < strVal.length; i++) {
//            var valueTemp = strVal[i].Trim();
//            valueTemp = valueTemp.TrimLink();
//            var reg = /^([\u4E00-\u9FA5]|[\uFE30-\uFFA0])*$/gi;
//            if (reg.test(valueTemp)) {
//                return "Error:英文入口中不能包含汉字!";
            //            }
            
        }

    }
    
    //strRes = "Error:申请号必须为数字，且长度为4-12位!";
    if ( txtId == "AN" )
    {
        for(var i = 0; i<strVal.length; i++) {
            var valueTemp = strVal[i].Trim();
            valueTemp = valueTemp.TrimLink();
            var res = getApplyNo(valueTemp);
            
            if ( res.substring(0,5) == "Error" ) 
            {
                return res;
            }
            else 
            {
               value =  value.replace(strVal[i].Trim(), res);
            
            }
        }
    }
    
    // 优先权必须为2位以上字母或数字
    if ( txtId == "PR" )
    {
        for(var i = 0; i<strVal.length; i++) {
            var valueTemp = strVal[i].Trim();
            valueTemp = valueTemp.TrimLink();
            if ( valueTemp.length < 2 ) 
            {
                return "Error:优先权必须为2位及以上字母或数字"; 
            }

        }
    }
    
    if ( txtId == "AD" || txtId == "PD" || txtId == "GD" )
    {
        for(var i = 0; i<strVal.length; i++) {
            var valueTemp = strVal[i].Trim();
            valueTemp = valueTemp.TrimLink();
            
            var old = valueTemp;
            var res = isDate(old);
            
            if ( res.substring(0,5) == "Error" )
            {
                return res;
            }
            else 
                value = value.replace(old,res);    // 设置value为修正后的日期值
        }
    }
    //strRes = "Error:公开号必须为2-9位的数字，检索式支持尾部通配符*";
    if (txtId == "PN" || txtId == "GN") 
    {	
        for(var i = 0; i<strVal.length; i++) {
            var valueTemp = strVal[i].Trim();
            valueTemp = valueTemp.TrimLink();
            
            var reg = /^(\d{2,8})(\d|\*)*$/;
            if (!reg.test(valueTemp))	
            {
		        return "Error:公开号必须为2-9位的数字，检索式支持前方一致";
		    }
	    }
    }   
    //strRes = "Error:范畴分类格式:2位数字+1位字母"; 
    if (txtId == "CT") 
    {
        for(var i = 0; i<strVal.length; i++) {
            var valueTemp = strVal[i].Trim();
            valueTemp = valueTemp.TrimLink();
            
            var reg = /^(\d{2})[A-Za-z]$/;
	        if (!reg.test(valueTemp))
	        {
		        return "Error:范畴分类格式:2位数字+1位字母"; 
		    }
		}
    }
    //strRes = "Error:ipc 输入不合法"
    if(txtId == "IC")
    {
        for (var i = 0; i < strVal.length; i++) {
            var valueTemp = strVal[i].Trim();
            valueTemp = valueTemp.TrimLink();
            var reg = /[^a-zA-Z0-9\/]/g;
            var _ipcQuery = valueTemp.replace(reg, '');
            if (_ipcQuery.length != valueTemp.length)
                return "Error:IPC不合法";
            if(valueTemp.length>12)
                return "Error:IPC分类检索项长度应为3—12位";
                
            var strValTemp = valueTemp;
            if(strValTemp.indexOf('/') == -1){ // 如果检索项不包含/，则不进一步做验证；
               continue;
            }  
              
            var arr = new Array();
            arr = strValTemp.split(/[\/]/);
            if (arr.length > 2)
                return "Error:IC入口中只能包含一个/";
            var head = arr[0].Trim();
            var end = (arr.length==2) ? arr[1].Trim():"";
//            headArr = head.split(/[\s]/);
//            if (headArr.length ==1 && headArr[0].length != 7)
//                return "Error:IC入口/前须为7位IC代码";
//            else if (headArr.length == 2 )
//            {
//                if (headArr[0].length != 4)
//                    return "Error:IC /前第一段必须为4位代码";
//                else if (headArr[1].length > 3)
//                    return "Error:IC /前第二段必须少于3位代码";
//                    
//            }
            if (end.length > 4)
                return "Error:IC /后必须少于4位代码";
            
            // 去掉/，替换头和尾
            // value = value.replace(strValTemp,head+end);    
                            
        }
    } 
    return value;
}

// 验证和修改申请号
function getApplyNo(apNo)
{
    var _apNo = apNo;
    var year1 =apNo.substring(0,2);
    if ( parseInt(year1,10)>50 ) 
    {
        _apNo = "19" + _apNo;
        if (_apNo.length > 5)  
            _apNo = _apNo.substring(0,5)+"00"+_apNo.substring(5);
    }
    var year2 = apNo.substring(0,1);
    if ( year2 == "0" )
    {
        _apNo = "20" + _apNo;
        if (_apNo.length > 5)  
            _apNo = _apNo.substring(0,5)+"00"+_apNo.substring(5);
    }
    var reg = /^\d{4,13}$/;
    if ( !reg.test(_apNo) ) 
    {
        return "Error:申请号必须为数字，且长度为4-12位!";
    }
    return _apNo;
}
// 判断是否为正确的日期格式，如YYYYMMDD或YYYY或YYYYMM或YY或YYMM或YYMMDD 
// 例如：19861010或者  201002-20100215 20100506+20100308 20100102>20100402 小日期在前
// 如果错误则返回以"Error"开头的字符串,否则返回修改过后的日期
function isDate(strDate)
{
    var arr = new Array();
    arr = strDate.split(">");
    if (arr.length > 2)
    {
        return "Error:范围检索日期数太多";
    }
    else if (arr.length == 1 )
    {
        var date = isSingleDate(arr[0]);
        if (date == "false" )
            return "Error:日期格式如YYYYMMDD或YYYY或YYYYMM或YY或YYMM或YYMMDD";
        else 
            return date;
    }
    else if ( arr.length == 2 )
    {
        var first = isSingleDate(arr[0]);
        var second = isSingleDate(arr[1]);
        if( first == "false" || second == "false" ) 
        {
            return "Error:范围检索时日期输入格式应为 yyyymmdd>yyyymmdd,在先的日期在前";
        }
        else
        {   
            
            if ( first.length != 8 || second.length != 8 ||  parseInt(first, 10) > parseInt(second, 10) )
                return "Error:范围检索时日期输入格式应为 yyyymmdd>yyyymmdd,在先的日期在前";
            else 
                return first + ">" + second;
        }
    }
    
    return strDate;
}

// 判断是否为正确的日期格式，如YYYYMMDD或YYYY或YYYYMM或YY或YYMM或YYMMDD 
// 如果以0开头，则补充"20"；如果以
// 返回"false"，"198601"类似的字段
function isSingleDate(strDate)
{
    var str = strDate;
    
    var year1 =str.substring(0,2);
    if ( parseInt(year1,10)>50 ) 
        str = "19" + str;
    var year2 = str.substring(0,1);
    if ( year2 == "0" )
        str = "20" + str;
    //alert(str);	    
    var reg = /^(\d{4})(\d{2})?(\d{2})?$/;
    if (reg.test(str) && RegExp.$2<=12 && RegExp.$3<=31 )	
    {
        return str;
    }        
    else 
    {
        return "false";
    }	        
}
