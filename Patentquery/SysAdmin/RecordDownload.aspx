<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecordDownload.aspx.cs" Title="厦漳泉科技基础资源服务平台-专利平台后台[下载统计]" 
    MasterPageFile="~/SysAdmin/MasterPage.master" Inherits="Patentquery.SysAdmin.RecordDownload" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server">
    <script src="../jquery-easyui-1.8.0/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/syscfg.js"></script>
    <script type="text/javascript" src="../Js/Common.js"></script>
    <script type="text/javascript" src="../Js/sysdialog.js"></script>
    <script src="../Js/artDialog/artDialog.min.js"></script>
    <script src="../Js/artDialog/artDialog.plugins.min.js"></script>   
    <script src="../jquery-easyui-1.8.0/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="../Css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Css/button.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <link href="../Js/artDialog/skins/blue.css" rel="stylesheet" />    
    <link href="../jquery-easyui-1.8.0/plugins/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script src="../jquery-easyui-1.8.0/plugins/jquery.autocomplete.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../JS/jDatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="body1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="UsreNames" runat="server" ClientIDMode="Static" />
    <div style="padding: 5px;font:12px/30px 宋体;">
        用户名：<input id="txtUserName" style="width: 110px;" />
        数据类型：<input id="dbtype"  class="easyui-combotree" style="width: 110px;" />       
        
        <span id="spcolname">IPC</span>：<select id="stcol" style="width:70px" class="easyui-combobox"  data-options="valueField: 'id',textField: 'text'">
            <option value="部">部</option>
            <option value="大类">大类</option>
            <option value="小类">小类</option>
            <option value="大组">大组</option>
            <option value="IPC">IPC</option>
        </select>
        <br />
        日期范围：
        <input id="sdate"  name ="sdate" class="Wdate" onClick="var d5222=$dp.$('enddate');WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',onpicked:function(){d5222.focus();},maxDate:'#F{$dp.$D(\'enddate\')}'})" />-
        <input id ="enddate" name="Wdate"  class="Wdate"  onFocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',minDate:'#F{$dp.$D(\'sdate\')}'})"></input>
        <a href="javascript:void(0);" onclick="gotst()" class="button button-primary">统计</a>
        <div style="padding:10px">
            <table id="tbhot" class="easyui-datagrid" style="width: 600px; height: 550px" data-options="singleSelect:true,collapsible:true">
                <thead>
                    <tr>
                        <th data-options="field:'IPC',width:180">
                            IPC
                        </th>
                        <th data-options="field:'下载量',width:100">
                            下载量
                        </th>                        
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dbtype').combotree('loadData', [
                    {
                        id: 1,
                        text: '中国',
                        children: [{ id: 11, text: '发明' }, { id: 12, text: '新型' }, { id: 13, text: '外观' }, { id: 14, text: '发明&新型'}]
                    },
                    {
                        id: 2,
                        text: '世界'
                    }]);
            $('#dbtype').combotree('tree').tree({ 'onClick': function (node) { chenagcol(node) } });
            var t = new Date();
            var t1 = t.getFullYear() + "-" + (t.getMonth() + 1) + "-" + t.getDate() + " 23:59:59";
//            $('#enddate').datetimebox({
//                value: t1,
//                required: true,
//                showSeconds: true
//            });
//            $('#sdate').datetimebox({
//                value: t.getFullYear() + "-" + (t.getMonth() ) + "-" + t.getDate(),
//                required: true,
//                showSeconds: true
//            });

            var data = $("#UsreNames").val().split(";");
            var option = {
                max: 12,    //列表里的条目数
                minChars: 0,    //自动完成激活之前填入的最小字符
                width: 200,     //提示的宽度，溢出隐藏
                scrollHeight: 300,   //提示的高度，溢出显示滚动条
                matchContains: true,    //包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示
                autoFill: false    //自动填充

            };           
            $('#txtUserName').autocomplete(data, option).result(function (event, data, formatted) {
                $('#txtUserName').val(data.toString().substring());

            });
        });
        function chenagcol(obj) {
            
            val = obj.text;
            $('#dbtype').next().children().first().val(val);
            $('.tree').parent().parent().toggle();
            if (val == "外观") {
                $('#spcolname').html("外观分类");
                $('#stcol').combobox("loadData", [{ text: '大类', id: '大类' }, { text: '小类', id: '小类' }, { text: '外观分类', id: '外观分类'}]);
                $('#stcol').combobox("setValue", "大类");
            }
            else {
                $('#spcolname').html("IPC");
                $('#stcol').combobox("loadData", [{ text: '部', id: '部' }, { text: '大类', id: '大类' }, { text: '小类', id: '小类' }, { text: '大组', id: '大组' }, { text: 'IPC', id: 'IPC'}]);
                $('#stcol').combobox("setValue", "部");
            }
            //$('#dbtype').combotree('setValue', val);

        }
        function gotst() {
            
            //得到查询条件
            var dbtype = $('#dbtype').next().children().first().val();
            if (dbtype == "") {
                showMessage("请选择数据类型");
                return;
            }
            var stcol = $('#stcol').combobox("getValue");
            if (stcol == "") {
                showMessage("请选择要统计的IPC");
                return;
            }
            //得到日期范围
            var sdate = $('#sdate').val(); // $('#sdate').datetimebox("getValue");
            var edate = $('#enddate').val(); //$('#enddate').datetimebox("getValue");
            if (sdate == "")  {
                showMessage("请输入起始日期");
                return;
            }
            if (edate == "")  {
                showMessage("请输入起始日期");
                return;
            }
            //判断
            if (!isStartEndDate(sdate, edate)) {
                showMessage("结束日期必须大于开始日期！");
                return;
            }
            var UserName = $("#txtUserName").val();
            showProcess();
            //dbtype, string stcol, string sdate, string edate
            $.ajax({
                type: "POST",
                url: "RecordDownload.aspx/Record",
                data: "{'dbtype':'" + dbtype + "','stcol':'" + stcol + "','sdate':'" + sdate + "','edate':'" + edate + "','UserName':'" + UserName + "'}",
                timeout: 30000, // set time out 30 seconds
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    var msg = XMLHttpRequest.responseText;
                    if (textStatus == "timeout") {
                        msg = "检索超时，请稍后再试！";
                    }
                    closeProcess();
                    showMessage("错误", msg);
                    return;
                },
                success: function (msg) {
                    
                    var data = $.parseJSON(msg.d);
                    if (data.total > 0) {
                        $('#tbhot').datagrid({ columns: [[{ field: stcol, title: stcol, width: 180 }, { field: '下载量', title: '下载量', width: 100}]] });
                        $('#tbhot').datagrid('loadData', { "total": data.total, "rows": data.rows });
                    }
                    else {
                        $('#tbhot').datagrid({ columns: [[{ field: stcol, title: stcol, width: 180 }, { field: '下载量', title: '下载量', width: 100}]] });
                        $('#tbhot').datagrid('loadData', { "total":0, "rows": [] });
                        showMessage("没有数据！");                        
                    }
                    closeProcess();
                }
            });
            
            //得到统计

        }
        function isStartEndDate(startDate, endDate) {
            //alert(startDate "===" endDate);   
            if (startDate.length > 0 && endDate.length > 0) {
                var startDateTemp = startDate.split(" ");
                var endDateTemp = endDate.split(" ");
                var arrStartDate = startDateTemp[0].split("-");
                var arrEndDate = endDateTemp[0].split("-");
                var arrStartTime = startDateTemp[1].split(":");
                var arrEndTime = endDateTemp[1].split(":");
                var allStartDate = new Date(arrStartDate[0], arrStartDate[1], arrStartDate[2], arrStartTime[0], arrStartTime[1], arrStartTime[2]);
                var allEndDate = new Date(arrEndDate[0], arrEndDate[1], arrEndDate[2], arrEndTime[0], arrEndTime[1], arrEndTime[2]);
                if (allStartDate.getTime() > allEndDate.getTime()) {
                    return false;
                }
            }
            return true;
        }   
        </script>
</asp:Content>
