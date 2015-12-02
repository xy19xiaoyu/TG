<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Family.aspx.cs" Inherits="Patentquery.Comm.Family" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery-1.8.0.min.js"></script>
    <script src="../jquery-easyui-1.8.0/jquery.query-2.1.7.js" type="text/javascript"></script>
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/Common.js"></script>
    <script type="text/javascript" src="../Js/showFamily.js"></script>
    <style>
        a
        {
            color: #555555;
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="process" style="width: 600px; height: 278px; text-align:center;padding-top:100px;">
        <img src="../Images/loading1.gif" alt="" style="margin-left:-20px;"/>&nbsp;&nbsp;&nbsp;&nbsp;正在处理中,请稍候...
    </div>
    <div id="divSimilar" style="min-height:278px; display;none">
        <table id="tbhot" class="easyui-datagrid" style="width: 600px; height:278px; overflow: visible"
            data-options="singleSelect:true,collapsible:true">
            <thead>
                <tr>
                    <th data-options="field:'apno',width:96">
                        公开号
                    </th>
                    <th data-options="field:'title',width:500">
                        发明名称
                    </th>                   
                </tr>
            </thead>
        </table>        
    </div>
    <div id="pagetop" class="easyui-pagination"  style="width: 598px;border:1px solid #95B8E7; border-top-width:0px;" data-options="total:0,showRefresh: false,displayMsg: '当前显示从 [{from}] 到 [{to}] 共[{total}]条记录'"></div>
    <div id="divNoData" style="width: 600px; height: 278px; text-align:center; display: none;padding-top:100px;">
        暂无数据
    </div>
    </form>
</body>
</html>
