var dbtype = "CN";
var preselecedmenu = "jzds";
var selectedmenu = "jzds";
var ary = { jzds: '1', jsdx: '2', fmrdx: '3', qyfb: '4', gwlh: '5', gaoji: '6' };
var ary1 = { touru: '1', chengguo: '2', shichangzhongxin: '3',jishuzhongxin: '4', shenqingren: '5', yanfaren: '6', zhiliang: '7', shouming: '8',laihua: '9',zidingyi: '10' };
var arykeyword = { jzds: '竞争对手', jsdx: '申请人', fmrdx: '发明人', qyfb: '发明人', gwlh: '来华公司', gaoji: '检索式' };
var aryti1 = { jzds: '行业', jsdx: '申请人', fmrdx: '区域', qyfb: '发明人', gwlh: '来华专利预警设置', gaoji: '自定义预警设置' };
var arytitop = { 0: '专利投入预警设置', 1: '专利成果预警设置', 2: '市场重心预警设置', 3: '技术重心预警设置', 4: '申请人预警设置',
    5: '研发人才预警设置', 6: '专利质量预警设置', 7: '专利寿命预警设置', 8: '', 9: ''};
var aryti2 = { jzds: '请选择行业名称：', jsdx: '请输入申请人：', fmrdx: '请选择区域：', qyfb: '请输入发明人：', gwlh: '请输入来华公司：', gaoji: '请输入检索式：' };
var aryti3 = { jzds: '已选择的行业名称：', jsdx: '已输入申请人：', fmrdx: '已添加区域：', qyfb: '已添加发明人：', gwlh: '已添加来华公司：', gaoji: '已添加检索式：' };
var aryti4 = { 0: '', 1: '', 2: '请选择区域：', 3: '请输入技术领域：', 4: '请输入申请人：', 5: '请输入发明人：', 6: '请选择专利类型：' };
var aryti5 = { 0: '', 1: '', 2: '已选择区域：', 3: '已添加技术领域：', 4: '已输入申请人：', 5: '已输入发明人：', 6: '已选择专利类型: ' };
var CID;
var PageIndex = 1;
var PageSize = 15;
var C_TYPE;
var top_type;
var selecttop="touru";
$(document).ready(function () {

    showRight($('#jzds'));
    $('#toptabs').tabs({
        fxFade: true,
        fxSpeed: 'fast',
        activate: function (event, ui) {
            selecttop = ui.newTab.parent().children().index(ui.newTab);
            showMianTable();
        }
    });
});

function changetitle(type) {
    if (type == 'EN') {
        $('#left_title').attr('class', 'left_ti_right');
        dbtype = "EN";
        $("#gwlh").parent().hide();
        showRight($('#jzds'));
    }
    else {
        $('#left_title').attr('class', 'left_ti_left');
        dbtype = "CN";
        $("#gwlh").parent().show();
        showRight($('#jzds'));
    }
}

function changeleft() {
    if ($('#left-min').css('display') == "none") {
        $('#left-min').toggle(500);
        $('#leftctr').attr('src', "../imgs/2left.png");
        $('#leftctr').attr('title', "隐藏");
        $('#right').attr('class', "rightmin");
        $('#tableyj').datagrid({ width: 800 });
    }
    else {

        $('#left-min').toggle(500, changeleft1);
    }
}
function changeleft1() {
    $('#leftctr').attr('src', "../imgs/2right.png");
    $('#leftctr').attr('title', "显示");
    $('#right').attr('class', "rightmax");

    $('#tableyj').datagrid({ width: 1000 });
}
function setselected(obj) {
    var css = $(obj).attr("id") + "selected";
    $(obj).attr("class", css);
}

function unsetselected(obj) {
    var css = $(obj).attr("id");
    if ($(obj).attr("id") == selectedmenu) return;
    $(obj).attr("class", css);
}

function showRight(obj) {
    
    $("#txtSearch").val('');
    selectedmenu = "";
    unsetselected($('#' + preselecedmenu));
    selectedmenu = $(obj).attr("id");
    preselecedmenu = selectedmenu;
    
    setselected($('#' + selectedmenu));
    
    $('#ddlctype option').remove();
    
    
    
    ShowTop(selectedmenu)
    $('#ddlctype').append("<option value='2'>预警名称</option>");
    $('#ddlctype').append("<option value='1'>设置时间</option>");
    showMianTable();
}
function ShowTop(area)
{
    switch(area)
    {
        case "jzds":            
            if (dbtype == "CN") {
                $("#touru").show();
                $("#chengguo").show();
                $("#shichangzhongxin").show();
                $("#jishuzhongxin").hide();
                $("#shenqingren").show();
                $("#yanfaren").show();
                $("#zhiliang").show();
                $("#shouming").show();
                $("#laihua").hide();
                $("#zidingyi").hide();
                selecttop = 0;
                $("#toptabs").tabs({ active: selecttop });
            }
            else {                                
                $("#touru").show();
                $("#chengguo").hide();
                $("#shichangzhongxin").show();
                $("#jishuzhongxin").hide();
                $("#shenqingren").show();
                $("#yanfaren").show();
                $("#zhiliang").hide();
                $("#shouming").hide();
                $("#laihua").hide();
                $("#zidingyi").hide();
                selecttop = 0;
                $("#toptabs").tabs({ active: selecttop });
            }
            break;
        case "jsdx":
            if (dbtype == "CN") {
                
                $("#touru").show();
                $("#chengguo").show();
                $("#shichangzhongxin").hide();
                $("#jishuzhongxin").show();
                $("#shenqingren").hide();
                $("#yanfaren").show();
                $("#zhiliang").show();
                $("#shouming").show();
                $("#laihua").hide();
                $("#zidingyi").hide();
                selecttop = 0;
                $("#toptabs").tabs({ active: selecttop });
            }
            else {
                $("#touru").show();
                $("#chengguo").hide();
                $("#shichangzhongxin").show();
                $("#jishuzhongxin").show();
                $("#shenqingren").hide();
                $("#yanfaren").show();
                $("#zhiliang").hide();
                $("#shouming").hide();
                $("#laihua").hide();
                $("#zidingyi").hide();
                selecttop = 0;
                $("#toptabs").tabs({ active: selecttop });
            }
            $('#ddlctype').append("<option value='0' selected='true'>" + arykeyword[selectedmenu] + "</option>");
            break;
        case "fmrdx":
            if (dbtype == "CN") {
                $("#chengguo").show();
                $("#jishuzhongxin").show();
            } else {
                $("#chengguo").hide();
                $("#jishuzhongxin").hide();
            }
            $("#touru").show();
            
            $("#shichangzhongxin").hide();
            
            $("#shenqingren").hide();
            $("#yanfaren").hide();
            $("#zhiliang").hide();
            $("#shouming").hide();
            $("#laihua").hide();
            $("#zidingyi").hide();
            selecttop = 0;
            $("#toptabs").tabs({ active: selecttop });
            break;
        case "qyfb":
            $("#touru").show();
            if (dbtype == "CN") {
                $("#chengguo").show();
            } else {
                $("#chengguo").hide();
            }
            $("#shichangzhongxin").hide();
            $("#jishuzhongxin").show();
            $("#shenqingren").hide();
            $("#yanfaren").hide();
            $("#zhiliang").hide();
            $("#shouming").hide();
            $("#laihua").hide();
            $("#zidingyi").hide();
            selecttop = 0;
            $("#toptabs").tabs({ active: 0 });
            $('#ddlctype').append("<option value='0' selected='true'>" + arykeyword[selectedmenu] + "</option>");
            break;
        case "gwlh":
            $("#touru").hide();
            $("#chengguo").hide();
            $("#shichangzhongxin").hide();
            $("#jishuzhongxin").hide();
            $("#shenqingren").hide();
            $("#yanfaren").hide();
            $("#zhiliang").hide();
            $("#shouming").hide();
            $("#laihua").show();
            $("#zidingyi").hide();
            selecttop = 8;
            $("#toptabs").tabs({ active: selecttop });
            $('#ddlctype').append("<option value='0' selected='true'>" + arykeyword[selectedmenu] + "</option>");
            break;
        case "gaoji":
            $("#touru").hide();
            $("#chengguo").hide();
            $("#shichangzhongxin").hide();
            $("#jishuzhongxin").hide();
            $("#shenqingren").hide();
            $("#yanfaren").hide();
            $("#zhiliang").hide();
            $("#shouming").hide();
            $("#laihua").hide();
            $("#zidingyi").show();
            selecttop = 9;
            $("#toptabs").tabs({ active: selecttop });            
            break;
    }
}
function showMianTable() {
    var KeyWord = $('#ddlctype').find("option:selected").val();
    var KeyValue = $("#txtSearch").val();
    C_TYPE = ary[selectedmenu];
    
    var type;
    var country = dbtype;

    if (KeyWord == 1) {
        if (Datecheck(KeyValue) == false) {
            showMessage("提示", "日期格式错误，请验证后输入");
            return;
        }
    }
    showProcess();

    $.ajax({
        type: "POST",
        url: "../comm/yujing.aspx/GetPageList",
        data: "{'KeyWord':'" + KeyWord + "','KeyValue':'" + KeyValue + "','C_TYPE':'" + C_TYPE + selecttop + "','type':'" + type + "','country':'" + country + "','PageIndex':'" + PageIndex + "','PageSize':'" + PageSize + "'}",
        timeout: 330000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            closeProcess();
            showMessage("错误", msg);
            return;
        },
        success: function (msg) {
            var data = $.parseJSON(msg.d);
            var ageColumns;
            var tiColumns;
            if (selecttop == "7") {
                ageColumns = [[{ field: 'S_NAME', title: '预警项', width: 200 }, { field: 'CURRENTNUM', title: '平均寿命', width: 100, align: 'center' }, { field: 'CHANGENUM', title: '变更数量', width: 100, align: 'center', formatter: function (value, rowData, rowIndex) { return formatlist(value, rowData, rowIndex, 1) } }]];
                tiColumns = [[{ field: 'ck', checkbox: 'true' },
                            { field: 'ALIAS', title: '预警名称', width: 80 },
                            { field: 'S_NAME', title: '预警项', width: 100 },
                            { field: 'C_DATE', title: '设置日期', width: 80, align: 'center', formatter: function (value, rowData, rowIndex) { return ChangeDateToString(value, rowData, rowIndex, 0) } },
                            { field: 'CURRENTNUM', title: '平均寿命', width: 100, align: 'center' },
                            { field: 'CHANGENUM', title: '变更数', width: 100, align: 'center', formatter: function (value, rowData, rowIndex) { return formatlist(value, rowData, rowIndex, 1) } },
                            { field: 'BEIZHU', width: 110, align: 'left', title: '备注' },
                            { field: 'C_Id', title: '操作', width: 135, align: 'center', formatter: function (value, rowData, rowIndex) { return formathit(value, rowData, rowIndex) } },
                            { field: 'C_Id_status', title: '状态', width: 100, align: 'center', formatter: function (value, rowData, rowIndex) { return formatstatus(value, rowData, rowIndex) } }
                            ]];
                
            }
            else {

                ageColumns = [[{ field: 'S_NAME', title: '预警项', width: 200 }, { field: 'CURRENTNUM', title: '当前专利数', width: 100, align: 'center', formatter: function (value, rowData, rowIndex) { return formatlist(value, rowData, rowIndex, 0) } }, { field: 'CHANGENUM', title: '变更数量', width: 100, align: 'center', formatter: function (value, rowData, rowIndex) { return formatlist(value, rowData, rowIndex, 1) } }]];
                tiColumns = [[{ field: 'ck', checkbox: 'true' },
                            { field: 'ALIAS', title: '预警名称', width: 80 },
                            { field: 'S_NAME', title: '预警项', width: 100 },
                            { field: 'C_DATE', title: '设置日期', width: 80, align: 'center', formatter: function (value, rowData, rowIndex) { return ChangeDateToString(value, rowData, rowIndex, 0) } },
                            { field: 'CURRENTNUM', title: '当前数', width: 100, align: 'center', formatter: function (value, rowData, rowIndex) { return formatlist(value, rowData, rowIndex, 0) } },
                            { field: 'CHANGENUM', title: '变更数', width: 100, align: 'center', formatter: function (value, rowData, rowIndex) { return formatlist(value, rowData, rowIndex, 1) } },
                            { field: 'BEIZHU', width: 110, align: 'left', title: '备注' },
                            { field: 'C_Id', title: '操作', width: 135, align: 'center', formatter: function (value, rowData, rowIndex) { return formathit(value, rowData, rowIndex) } },
                            { field: 'C_Id_status', title: '状态', width: 100, align: 'center', formatter: function (value, rowData, rowIndex) { return formatstatus(value, rowData, rowIndex) } }
                            ]];
            }

            $('#tableyj').datagrid({
                view: detailview,
                columns: tiColumns,
                detailFormatter: function (index, row) {
                    return '<div style="padding:2px"><table id="ddv-' + index + '"></table></div>';
                },
                onExpandRow: function (index, row) {

                    $('#ddv-' + index).datagrid({
                        url: '../comm/yjitems.aspx?cid=' + row.C_ID,
                        fitColumns: true,
                        singleSelect: true,
                        rownumbers: true,
                        loadMsg: '正在加载....',
                        height: 'auto',
                        columns: ageColumns,
                        onResize: function () {
                            $('#tableyj').datagrid('fixDetailRowHeight', index);
                        },
                        onLoadSuccess: function () {

                            setTimeout(function () {
                                $('#tableyj').datagrid('fixDetailRowHeight', index);
                            }, 0);
                        }
                    });
                    $('#tableyj').datagrid('fixDetailRowHeight', index);
                }
            });
            var pager = $('#tableyj').datagrid().datagrid('getPager');
            pager.pagination({
                onSelectPage: function (pageNumber, pageSize) {

                    PageIndex = pageNumber;
                    PageSize = pageSize;
                    showMianTable();
                },
                total: parseInt(data.total),
                displayMsg: '当前显示从 [{from}] 到 [{to}] 共[{total}]条记录'
            });

            $('#tableyj').datagrid('loadData', data);
            pager.pagination({ 'pageSize': PageSize, 'pageNumber': PageIndex });

            closeProcess();
        }
    });
}

function showAddyjDG() {
    LassFoc();
    clear();
    $('#dgti2').html(aryti2[selectedmenu]);
    $('#dgti3').html(aryti3[selectedmenu]);
    $('#dgti4').html(aryti4[selecttop]);
    $('#dgti5').html(aryti5[selecttop]);
    yjdate();

    art.dialog({
        title: aryti1[selectedmenu] + arytitop[selecttop],
        content: document.getElementById('addyjdiv'),
        id: 'AddyjDG',
        lock: true,
        padding: 0,
        okValue: '添加',
        ok: function () {
            Addyj();
            return false;
        },
        cancelValue: '取消',
        cancel: function () { }
    });
    
    if (ary[selectedmenu] == 2) {
        $('#divfanwei').css('display', 'block');
    } else {
        $('#divfanwei').css('display', 'none');
    }
    if (ary[selectedmenu] == 3 && dbtype == "CN") {
        $('#sheng').css('display', 'block');
    } else {
        $('#sheng').css('display', 'none');
    }
    if (ary[selectedmenu] == 5) {
        $('#GuoJia').css('display', 'block');
    } else {
        $('#GuoJia').css('display', 'none');
    }
    if (ary[selectedmenu] == 3 && dbtype == "EN") {
        $('#ShiJie').css('display', 'block');
    } else {
        $('#ShiJie').css('display', 'none');
    }

    if (ary[selectedmenu] == 6) {
        $('#TiShi').css('display', '');
    } else {
        $('#TiShi').css('display', 'none');
    }
//    0: '专利投入预警设置', 1: '专利成果预警设置', 2: '市场重心预警设置', 3: '技术重心预警设置', 4: '申请人预警设置',
//    5: '研发人才预警设置', 6: '专利质量预警设置', 7: '专利寿命预警设置', 8: '', 9: ''}
    switch (selecttop) {
        case 0:
            $('#dgti').css('display', 'none');
            $('#shichang').css('display', 'none');
            break;
        case 1:
            $('#dgti').css('display', 'none');
            $('#shichang').css('display', 'none');
            break;
        case 2:
            $('#dgti').css('display', 'block'); 
            if (dbtype == "EN") {
                $('#ShiJie1').css('display', 'block');
                $('#shichang').css('display', 'none');
            } else {
                $('#shichang').css('display', 'block');
                $('#ShiJie1').css('display', 'none');
            }
            break;
        case 3:
            $('#dgti').css('display', 'block');
            $('#shichang').css('display', 'none');
            $('#ShiJie1').css('display', 'none');
            break;
        case 4:
            $('#dgti').css('display', 'block');
            $('#shichang').css('display', 'none');
            $('#ShiJie1').css('display', 'none');
            break;
        case 5:
            $('#dgti').css('display', 'block');
            $('#shichang').css('display', 'none');
            $('#ShiJie1').css('display', 'none');
            break;
        case 6:
            $('#dgti').css('display', 'block');
            $('#shichang').css('display', 'none');
            $('#ShiJie1').css('display', 'none');
            //$('#shichang').css('display', 'block');
            break;
        case 7:
            $('#dgti').css('display', 'none');
            $('#shichang').css('display', 'none');
            $('#ShiJie1').css('display', 'none');
            break;
        case 8:
            $('#dgti').css('display', 'none');
            $('#shichang').css('display', 'none');
            $('#ShiJie1').css('display', 'none');
            break;
        case 9:
            $('#dgti').css('display', 'none');
            $('#shichang').css('display', 'none');
            $('#ShiJie1').css('display', 'none');
            break;
    }
    if (ary[selectedmenu] == 1) {
        $("#divkeyword").css('display', 'none');
        $("#divzhuanti").css('display', 'block');
    } else {
        $("#divkeyword").css('display', 'block');
        $("#divzhuanti").css('display', 'none');
    }
}

function additem() {
    var sheng = $('#ctl00_ContentPlaceHolder1_ddlSheng').find("option:selected").text();
    var ShiJie = $('#ctl00_ContentPlaceHolder1_ddlShiJie').find("option:selected").text();
    var input = $('#txtKeyWord').val().Trim();
    if ($("#txtKeyWord").is(":hidden")) {
        $("#ctl00_ContentPlaceHolder1_zhuantiID").val($('#cc').combo('getValue'));
        input = $('#cc').combotree('getText');
        if ((input == "请选择专题库")||(input == '')) {
            showMessage("提示", "请选择专题库，如您还未建立专题数据库，请先建立再预警");
            return;
        }
    } else {
        input = input.replace(/;/g, "");
        if (input == "") {
            showMessage("提示", "请输入要添加的预警信息");
            return;
        }        
        if (ary[selectedmenu] == 3 && dbtype == "CN") {
            input = sheng + "(" + input + ")";
        }

        if (ary[selectedmenu] == 3 && dbtype == "EN") {
            input = ShiJie + "(" + input + ")";
        }
        if (ary[selectedmenu] == 2) {
            var fanwei = $('#ctl00_ContentPlaceHolder1_ddlfanwei').find("option:selected").text();
            if (fanwei != "平台数据") {
                input = fanwei + "(" + input + ")";
                $("#ctl00_ContentPlaceHolder1_zhuantiID").val($('#ctl00_ContentPlaceHolder1_ddlfanwei').find("option:selected").val());
            }            
        }
        if (ary[selectedmenu] == 6) {
            var flag = validateLogicSearchQuery(input);
            if (flag.substring(0, 5) == "Error") {
                showMessage("提示", flag.substring(6));
                return;
            }
        }
    }
    //input += ";";
    var keys = $('#txtkeys').val().Trim(';');
    if (keys != "") keys += ";";

    $('#txtkeys').val(input);
    $('#txtKeyWord').val('')
}
function addtopitem() {
    var sheng = $('#ctl00_ContentPlaceHolder1_ddlshichang').find("option:selected").text();
    var ShiJie = $('#ctl00_ContentPlaceHolder1_ddlShiJie1').find("option:selected").text();
    var input = $('#txtKeyWord1').val().Trim();    
    input = input.replace(/;/g, "");
    if (input == "") {
        showMessage("提示", "请输入要添加的预警信息");
        return;
    }

    if (selecttop == 2 && dbtype == "CN") {
        input = sheng + "(" + input + ")";
    }

    if (selecttop == 2 && dbtype == "EN") {
        input = ShiJie + "(" + input + ")";
    }

//    if (ary[selectedmenu] == 6) {
//        var flag = validateLogicSearchQuery(input);
//        if (flag.substring(0, 5) == "Error") {
//            showMessage("提示", flag.substring(6));
//            return;
//        }
//    }

    input += ";";
    var keys = $('#txtkeys1').val().Trim(';');
    if (keys != "") keys += ";";

    $('#txtkeys1').val(keys+input);
    $('#txtKeyWord1').val('')
}
function yjdate() {

    var week = $('#ddlyjdate').find("option:selected").val();
    var _date = new Date()
    var d = _date.setMonth(_date.getMonth() + parseInt(week));
    var s = new Date(d);
    $('#yjdate').html(s.getFullYear() + "年" + (s.getMonth() + 1) + "月" + s.getDate() + "日");

}
function clear() {
    $("#itxtname").val('');
    $("#txtkeys").val('');
    $("#txtKeyWord").val('');
    $("#txtare").val('');
    $("#txtSearch").val('');
    $("#txtkeys1").val('');
    $("#ctl00_ContentPlaceHolder1_zhuantiID").val('');
}
function Addyj() {
    var KeyWord = $('#ddlctype').find("option:selected").val();
    var yjname = $("#itxtname").val();
    var KeyValue = $("#txtkeys").val();
    var TopKeyValue = $("#txtkeys1").val();
    var des = $("#txtare").val();
    var sheng = $('#ctl00_ContentPlaceHolder1_ddlSheng').find("option:selected").val();
    var GuoJia = $('#ctl00_ContentPlaceHolder1_ddlGuoJia').find("option:selected").val();
    var ShiJie = $('#ctl00_ContentPlaceHolder1_ddlShiJie').find("option:selected").val();
    var status=$('#ddlyjStatus').find("option:selected").val();
    var hangye = $("#ctl00_ContentPlaceHolder1_zhuantiID").val();
    var C_TYPE = ary[selectedmenu];
    var type;
    var country = dbtype;
    if (yjname.Trim() == "") { $("#itxtname").focus(); showMessage("提示", "请输入预警名称"); return; }
    if (yjname.Trim().length > 200) { $("#itxtname").focus(); showMessage("提示", "您输入的预警名称超出长度限制"); return; }
    if (KeyValue.Trim() == "") { $("#txtKeyWord").focus(); showMessage("提示", "请输入预警信息"); return; }
    var week = $('#ddlyjdate').find("option:selected").val();
    if ($("#dgti").is(":visible")) {
        if (TopKeyValue.Trim() == "") { $("#txtKeyWord1").focus(); showMessage("提示", "请输入预警信息"); return; }
    } 
    showProcess();

    $.ajax({
        type: "POST",
        url: "../comm/yujing.aspx/addyj",
        data: "{'KeyWord':'" + KeyWord + "','KeyValue':'" + encodeURIComponent(KeyValue) + "','C_TYPE':'" + C_TYPE + selecttop + "','type':'" + type + "','country':'" + country + "','des':'" + encodeURIComponent(des) + "','yjname':'" + encodeURIComponent(yjname) + "','week':'" + week + "','sheng':'" + sheng + "','GuoJia':'" + GuoJia + "','ShiJie':'" + ShiJie + "','Status':'" + status + "','Hangye':'" + hangye + "','TopKeyValue':'"+TopKeyValue+"'}",
        timeout: 330000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            closeProcess();
            showMessage("错误", msg);
            return;
        },
        success: function (msg) {
            if (msg.d == "failed") {
                showMessage("错误", "添加失败，请检查输入信息，是否符合格式要求");
            }
            else {
                closeProcess('AddyjDG');
                showMianTable();
                return true;
            }
            closeProcess();
        }
    });
}


function showEdityjDG(rowindex) {

    //判断是否至选择一行
    var selectrows = $('#tableyj').datagrid('getRows');
    //得到各种值
    $("#itxtname").val(selectrows[rowindex].ALIAS);

    $("#txtkeys").val(selectrows[rowindex].S_NAME);
    $("#txtare").val(selectrows[rowindex].BEIZHU);
    var week = $('#ddlyjdate').find("option:selected").val();
    $("#ddlyjdate ").val(selectrows[rowindex].PERIOD);
    CID = selectrows[rowindex].C_ID;


    $('#dgti2').html(aryti2[selectedmenu]);
    $('#dgti3').html(aryti3[selectedmenu]);

    $("#txtKeyWord").val('');
    if (ary[selectedmenu] == 2) {
        $('#divfanwei').css('display', 'block');
    } else {
        $('#divfanwei').css('display', 'none');
    }
    if (ary[selectedmenu] == 3 && dbtype == "CN") {
        $('#sheng').css('display', 'block');
    } else {
        $('#sheng').css('display', 'none');
    }
    if (ary[selectedmenu] == 5) {
        $('#GuoJia').css('display', 'block');
    } else {
        $('#GuoJia').css('display', 'none');
    }
    if (ary[selectedmenu] == 3 && dbtype == "EN") {
        $('#ShiJie').css('display', 'block');
    } else {
        $('#ShiJie').css('display', 'none');
    }

    if (ary[selectedmenu] == 6) {
        $('#TiShi').css('display', '');
    } else {
        $('#TiShi').css('display', 'none');
    }
    //    0: '专利投入预警设置', 1: '专利成果预警设置', 2: '市场重心预警设置', 3: '技术重心预警设置', 4: '申请人预警设置',
    //    5: '研发人才预警设置', 6: '专利质量预警设置', 7: '专利寿命预警设置', 8: '', 9: ''}
    switch (selecttop) {
        case 0:
            $('#dgti').css('display', 'none');
            $('#shichang').css('display', 'none');
            break;
        case 1:
            $('#dgti').css('display', 'none');
            $('#shichang').css('display', 'none');
            break;
        case 2:
            $('#dgti').css('display', 'block');
            $('#shichang').css('display', 'block');
            break;
        case 3:
            $('#dgti').css('display', 'block');
            $('#shichang').css('display', 'none');
            break;
        case 4:
            $('#dgti').css('display', 'block');
            $('#shichang').css('display', 'none');
            break;
        case 5:
            $('#dgti').css('display', 'block');
            $('#shichang').css('display', 'none');
            break;
        case 6:
            $('#dgti').css('display', 'block');
            $('#shichang').css('display', 'none');
            //$('#shichang').css('display', 'block');
            break;
        case 7:
            $('#dgti').css('display', 'none');
            $('#shichang').css('display', 'none');
            break;
        case 8:
            $('#dgti').css('display', 'none');
            $('#shichang').css('display', 'none');
            break;
        case 9:
            $('#dgti').css('display', 'none');
            $('#shichang').css('display', 'none');
            break;
    }
    if (ary[selectedmenu] == 1) {
        $("#divkeyword").css('display', 'none');
        $("#divzhuanti").css('display', 'block');
    } else {
        $("#divkeyword").css('display', 'block');
        $("#divzhuanti").css('display', 'none');
    }
    LassFoc();

    $.ajax({
        type: "POST",
        url: "../comm/yujing.aspx/GetYjByID",
        data: "{'CID':'" + CID + "'}",
        timeout: 330000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            closeProcess();
            showMessage("错误", msg);
            return;
        },
        success: function (msg) {
            if (msg.d == "failed") {
                showMessage("错误", "添加失败，请检查输入信息，是否符合格式要求");
            }
            else {
                var o = msg.d;
                data = $.parseJSON(o);
                $("#ctl00_ContentPlaceHolder1_zhuantiID").val(data[0].nid);
                $("#txtkeys1").val(data[0].sname);

            }
            closeProcess();
        }
    });
    //显示对话框
    yjdate();
    art.dialog({
        title: aryti1[selectedmenu],
        content: document.getElementById('addyjdiv'),
        id: 'AddyjDG',
        lock: true,
        padding: 0,
        cancelValue: '取消',
        cancel: function () { },
        okValue: '修改',
        ok: function () {
            Edit();
            return false;
        }

    });
}
function Edit() {
    var KeyWord = $('#ddlctype').find("option:selected").val();
    var yjname = $("#itxtname").val();
    var KeyValue = $("#txtkeys").val();
    var TopKeyValue = $("#txtkeys1").val();
    var des = $("#txtare").val();
    var C_TYPE = ary[selectedmenu];
    var sheng = $('#ctl00_ContentPlaceHolder1_ddlSheng').find("option:selected").val();
    var GuoJia = $('#ctl00_ContentPlaceHolder1_ddlGuoJia').find("option:selected").val();
    var ShiJie = $('#ctl00_ContentPlaceHolder1_ddlShiJie').find("option:selected").val();
    var status = $('#ddlyjStatus').find("option:selected").val();
    var hangye = $("#ctl00_ContentPlaceHolder1_zhuantiID").val();
    var type;
    var country = dbtype;
    if (yjname.Trim() == "") { $("#itxtname").focus(); showMessage("提示", "请输入预警名称"); return; }
    if (yjname.Trim().length > 200) { $("#itxtname").focus(); showMessage("提示", "您输入的预警名称超出长度限制"); return; }
    if (KeyValue.Trim() == "") { $("#txtKeyWord").focus(); showMessage("提示", "请输入预警信息"); return; }
    var week = $('#ddlyjdate').find("option:selected").val();

    showProcess();

    $.ajax({
        type: "POST",
        url: "../comm/yujing.aspx/edityj",
        data: "{'KeyWord':'" + KeyWord + "','KeyValue':'" + encodeURIComponent(KeyValue) + "','C_TYPE':'" + C_TYPE + selecttop + "','type':'" + type + "','country':'" + country + "','des':'" + encodeURIComponent(des) + "','yjname':'" + encodeURIComponent(yjname) + "','week':'" + week + "','CID':'" + CID + "','sheng':'" + sheng + "','GuoJia':'" + GuoJia + "','ShiJie':'" + ShiJie + "','Status':'" + status + "','Hangye':'" + hangye + "','TopKeyValue':'" + TopKeyValue + "'}",
        timeout: 330000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            closeProcess();
            showMessage("错误", msg);
            return;
        },
        success: function (msg) {
            if (msg.d == "failed") {
                showMessage("错误", "修改失败，请检查输入信息，是否符合格式要求");
            }
            else {
                closeProcess('AddyjDG');
                showMianTable();
                return true;
            }
            closeProcess();
        }
    });
}
function delyj() {

    var cids = "";
    var selectrows = $('#tableyj').datagrid('getChecked');
    if (selectrows.length == 0) {
        showMessage("提示", "请您先选择一条记录！");
        return;
    }
    for (var i = 0; i < selectrows.length; i++) {
        cids = cids + selectrows[i].C_ID + ",";
    }
    showProcess();
    art.dialog({
        title: '确认删除',
        content: '<img src="../js/artDialog/skins/icons/warning.png"/>您确定删除所选的预警吗？',
        button: [
            {
                value: '确定',
                callback: function () {
                    $.ajax({
                        type: "POST",
                        url: "../comm/yujing.aspx/delYJ",
                        data: "{'cids':'" + cids + "'}",
                        timeout: 330000, // set time out 30 seconds
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            var msg = XMLHttpRequest.responseText;
                            if (textStatus == "timeout") {
                                msg = "超时，请稍后再试！";
                            }
                            closeProcess();
                            showMessage("错误", msg);
                            return;
                        },
                        success: function (msg) {

                            if (msg.d == "failed") {
                                showMessage("错误", "删除失败");
                            }
                            else {
                                closeProcess('AddyjDG');
                                showMianTable();
                                return true;
                            }
                            closeProcess();
                        }
                    });
                },
                focus: true
            },
            {
                value: '取消',
                callback: function () { }

            }
            ]
    });
}


function formathit(value, rowData, rowIndex) {
    
    return "<a href='javascript:void(0);' onclick='updateyjdate(" + rowData.C_ID + ");'>手动更新</a>&nbsp;<a href='javascript:void(0);' onclick='showEdityjDG(" + rowIndex + ");'>修改</a>&nbsp;<a target='_blank' href='frmYJHis.aspx?CID=" + rowData.C_ID + "&db=" + dbtype + "'>更新历史</a>";
}
function formatstatus(value, rowData, rowIndex) {
    if (rowData.STATUS == "1") {
        return "<a style=\"text-decoration:none\">启动</a>&nbsp;&nbsp;<a href='javascript:void(0);' onclick='updateyjqidong(" + rowData.C_ID + ",0 );'>停止</a>";
    } else {
        return "<a href='javascript:void(0);' onclick='updateyjqidong(" + rowData.C_ID + ",1);'>启动</a>&nbsp;&nbsp;<a style=\"text-decoration:none\">停止</a>";
    }
}

function formatlist(value, rowData, rowIndex, isupdate) {
    return "<a href='javascript:void(0);' onclick='showlist(" + rowData.W_ID + "," + isupdate + "," + value + ")'>" + value + "</a>";
}
function showlist(W_ID, isupdate, Nm) {
    window.open("../my/frmPatentList.aspx?db=" + dbtype + "&No=" + W_ID + "&from=yj" + isupdate + "&Nm=" + Nm);
}
function updateyjqidong(objcid, status) {
    showProcess();
    $.ajax({
        type: "POST",
        url: "../comm/yujing.aspx/updateStatus",
        data: "{'C_ID':'" + objcid + "','STATUS':'"+status +"'}",
        timeout: 330000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            closeProcess();
            showMessage("错误", msg);
            return;
        },
        success: function (msg) {
            if (msg.d == "failed") {
                showMessage("错误", "更新失败");
            }
            else {
                showMianTable();
                return true;
            }
            closeProcess();
        }
    });
}
function updateyjdate(objcid) {
    showProcess();
    $.ajax({
        type: "POST",
        url: "../comm/yujing.aspx/handupdata",
        data: "{'C_ID':'" + objcid + "'}",
        timeout: 330000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            closeProcess();
            showMessage("错误", msg);
            return;
        },
        success: function (msg) {
            if (msg.d == "failed") {
                showMessage("错误", "手动更新失败");
            }
            else {
                showMianTable();
                return true;
            }
            closeProcess();
        }
    });
}

function showhot() {
    showProcess();
    $.ajax({
        type: "POST",
        url: "../comm/yujing.aspx/getHot",
        data: "{'C_TYPE':'" + ary[selectedmenu] + "','country':'" + dbtype + "'}",
        timeout: 330000, // set time out 30 seconds
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var msg = XMLHttpRequest.responseText;
            if (textStatus == "timeout") {
                msg = "超时，请稍后再试！";
            }
            closeProcess();
            showMessage("错误", msg);
            return;
        },
        success: function (msg) {
            var data = $.parseJSON(msg.d);
            $('#hot').datagrid({
                fitColumns: true,
                singleSelect: true,
                rownumbers: true,
                loadMsg: '正在加载....',
                height: 'auto',
                columns: [[
                            { field: 'S_NAME', title: arykeyword[selectedmenu], width: 180 },
                            { field: 'ct', title: '关注度', width: 120, align: 'center' }
                        ]]
            });

            $('#hot').datagrid('loadData', data);
            closeProcess();
            var d = art.dialog({
                title: "热点预警",
                content: document.getElementById('yjhot'),
                id: 'dlgshowyjhot',
                padding: '0px',
                ok: "确定",
                okValue: '确定',
                lock: true,
                border: false,
                follow: document.getElementById('dgPostion')
            });
        }
    });

}
function Datecheck(date) {
    var a = /^(\d{4})-(\d{2})-(\d{2})$/;
    if (!a.test(date)) {
        return false;
    }
    else
        return true;
}


function ChangeDateToString(CurDate) {

//    Year = CurDate.substring(0, 4);
//    Month = CurDate.substring(5, 7);
//    Day = CurDate.substring(8, 10);

    //return Year + "-" + Month + "-" + Day;

    var DateIn = new Date(CurDate);
    var Year = 0;
    var Month = 0;
    var Day = 0;
    var CurrentDate = "";
    //    初始化时间
    Year = DateIn.getFullYear();
    Month = DateIn.getMonth() + 1;
    Day = DateIn.getDate();

    CurrentDate = Year + "-";
    if (Month >= 10) {
        CurrentDate = CurrentDate + Month + "-";
    }
    else {
        CurrentDate = CurrentDate + "0" + Month + "-";
    }
    if (Day >= 10) {
        CurrentDate = CurrentDate + Day;
    }
    else {
        CurrentDate = CurrentDate + "0" + Day;
    }
    return CurrentDate;
}

 