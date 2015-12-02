<%@ Page Title="" Language="C#" MasterPageFile="~/Master/index.Master" AutoEventWireup="true"
    CodeBehind="frmSearchWord.aspx.cs" Inherits="Patentquery.My.frmSearchWord" %>

<asp:Content ID="he1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <link href="../css/themes/default/default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/UserCollect.js"></script>
    <link href="../Css/B_SeachPage.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="left" style="width: 225px;">
        <div id="pinlieft">
            <div id="left_title " class="left_ti" style="text-align: center;">
                &nbsp;<span>辅助工具</span>&nbsp;
            </div>
            <div class="left_content2" style="padding: 0px; width: 220px">
                <div class="panel" style="width: 220px; background-color: #FBEC88">
                    <div class="panel-header accordion-header accordion-header-selected" style="height: 16px;
                        width: 210px; border-width: 0 0 1px;">
                        <div class="panel-title ">
                            <a href="frmSearchIPCIndex.aspx"><span style="color: #800000;">分类号关联查询</span></a>
                        </div>
                        <</div>
                </div>
                <div class="panel" style="width: 220px; background-color: #FBEC88">
                    <div class="panel-header accordion-header accordion-header-selected" style="height: 16px;
                        width: 210px; border-width: 0 0 1px;">
                        <div class="panel-title">
                            <a href="#"><span style="color: #800000;">相关词查询</span></a>
                        </div>
                    </div>
                </div>
                <div class="panel" style="width: 220px; background-color: #FBEC88">
                    <div class="panel-header accordion-header accordion-header-selected" style="height: 16px;
                        width: 210px; border-width: 0 0 1px;">
                        <div class="panel-title">
                            <a href="frmCountryCode.aspx"><span>国别代码查询</span></a>
                        </div>
                    </div>
                </div>
                <div class="panel" style="width: 220px;">
                    <div class="panel-header accordion-header" style="height: 16px; width: 210px; border-width: 0 0 0 0px;">
                        <div class="panel-title">
                            <a href="frmEnterpriseName.aspx"><span>企业名称查询</span></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="right" style="width: 768px; min-height: 550px;">
        <div id="right_top" class="right_top">
            基本词：<asp:TextBox ID="TextBox1" runat="server" Height="22px" Width="300px"></asp:TextBox>
            <asp:Button ID="btnSearch" CssClass="button" Height="32px" Width="79px" runat="server"
                Text="查询" OnClick="btnSearch_Click" />&nbsp;
            <input id="btnReset" type="reset" class="button" style="width: 79px; height: 32px;"
                value="重置" />
        </div>
        <div id="ipc_right_content" style="display: ; margin-left :5px">
            <asp:ListBox ID="lstSameWord" runat="server" Width="533px"></asp:ListBox>
        </div>
    </div>
</asp:Content>
