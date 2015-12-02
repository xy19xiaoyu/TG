<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="yjhot.aspx.cs" Inherits="Patentquery.Comm.yjhot" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery-1.8.0.min.js"></script>
    <script src="../jquery-easyui-1.8.0/jquery.query-2.1.7.js" type="text/javascript"></script>
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/Common.js"></script>   
     <script>
         var type=getUrlParam('C_TYPE');
         var dbtype = getUrlParam('db');
         $('#ddv-' + index).datagrid({
             url: '../comm/yjitems.aspx?cid=' + row.C_ID,
             fitColumns: true,
             singleSelect: true,
             rownumbers: true,
             loadMsg: '正在加载....',
             height: 'auto',
             columns: [[
                            { field: 'S_NAME', title: '预警项', width: 200 },
                            { field: 'CURRENTNUM', title: '当前专利数', width: 100, align: 'center', formatter: function (value, rowData, rowIndex) { return formatlist(value, rowData, rowIndex, 0) } },
                            { field: 'CHANGENUM', title: '变更数量', width: 100, align: 'center', formatter: function (value, rowData, rowIndex) { return formatlist(value, rowData, rowIndex, 1) } }
                        ]],
             onResize: function () {
                 $('#tableyj').datagrid('fixDetailRowHeight', index);
             },
             onLoadSuccess: function () {

                 setTimeout(function () {
                     $('#tableyj').datagrid('fixDetailRowHeight', index);
                 }, 0);
             }
         });     
     </script> 
</head>
<body>
    <div>
     <table id="tableyj" class="easyui-datagrid" style="width: 300; height: 200px;"
        data-options="singleSelect:false,collapsible:true,pagination:true,pageList:[15,30,35,50]">
        <thead>
            <tr>
                <th data-options="field:'ALIAS',width:580">
                    预警项
                </th>
                <th data-options="field:'C_DATE',width:135,align:'center'">
                     预警次数
                </th>                
            </tr>
        </thead>
    </table>
    </div>
</body>
</html>
