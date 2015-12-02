<%@ Page Title="预警历史" Language="C#" MasterPageFile="~/Master/index.master" AutoEventWireup="true"
    CodeBehind="frmYJHis.aspx.cs" Inherits="Patentquery.YJ.frmYJHis" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="zlyjhis.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery.easyui.min.js"></script>
    <script src="../jquery-easyui-1.8.0/datagrid-detailview.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="tableyj" class="easyui-datagrid" style="width: 1024px; height: 550px;"
        data-options="singleSelect:false,collapsible:true,pagination:true,pageList:[15,30,35,50]">
        <thead>
            <tr>
                <th data-options="field:'ALIAS',width:580">
                    预警名称
                </th>
                <th data-options="field:'C_DATE',width:135,align:'center',formatter:function(value,rowData,rowIndex){return ChangeDateToString(value, rowData, rowIndex,0)}">
                    更新日期
                </th>
                <th data-options="field:'CURRENTNUM',width:70,align:'center',formatter:function(value,rowData,rowIndex){return formatlist(value, rowData, rowIndex,0)}">
                    当前专利数
                </th>
                <th data-options="field:'CHANGENUM',width:70,align:'center',formatter:function(value,rowData,rowIndex){return formatlist(value, rowData, rowIndex,1)}">
                    变更数量
                </th>
                <th data-options="field:'BEIZHU',width:120,align:'left'">
                    备注
                </th>
            </tr>
        </thead>
    </table>
</asp:Content>
