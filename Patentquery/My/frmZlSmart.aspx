<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmZlSmart.aspx.cs" Inherits="Patentquery.My.frmZlSmart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>专利平台</title>
    <meta name="Keywords" content="厦漳泉科技基础资源服务平台-专利平台" />
    <meta name="Description" content="厦漳泉科技基础资源服务平台-专利平台" />
    <script type="text/javascript" src="/Js/Common.js"></script>
    <script type="text/javascript" src="../js/syscfg.js"></script>
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="/Js/sysdialog.js"></script>
    <script src="../Js/artDialog/artDialog.min.js"></script>
    <script src="../Js/artDialog/artDialog.plugins.min.js"></script>
    <link href="../Js/artDialog/skins/blue.css" rel="stylesheet" />
    <link href="../Css/index.css" rel="stylesheet" type="text/css" />
    <link href="../Css/smartNew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/smartPage.js" type="text/javascript"></script>
    <%--<script src="../jquery-easyui-1.8.0/plugins/jquery.funkyUI.js" type="text/javascript"></script>--%>
    <style>
        .Center
        {
            background-image: url('../Images/sumetro/bg.png');
            background-repeat: repeat;
            height: 120px;
            width: 985px;
        }
        #SmNavbom ul li
        {
            float: left;
            text-align: center;
            width: 90px; /*margin: 0px 8px 0px 8px;*/
        }
        #SmNavbom ul li img
        {
            width: 50px;
            height: 46px; /* margin-bottom: 10px;*/
        }
        #SmNavbom ul li a
        {
            /*float: left;*/
            font: 14px/30px "宋体";
            font-weight: bold;
            color: #002863;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divCenter" class="Center">
        <div style="padding-left: 5px;">
            <div id="BtnUl">
                <ul>
                    <li><a id="btnPatentCn" class="btnPatentCn" onclick="switchPatentType(this)">中国专利检索
                    </a></li>
                    <li><a id="btnPatentEn" class="btnPatentEnOff" onclick="switchPatentType(this)">世界专利检索</a>
                    </li>
                </ul>
            </div>
            <div id="divText3" style="width: 790px; vertical-align: middle; float: left">
                <textarea id="searchContent" cols="20" rows="2" class="simpleSearchTxb" style="width: 675px;
                    height: 65px;"></textarea>&nbsp; <a id="btnQuery_11" href="javascript:;">
                        <img id="BtnSearch" alt="检索" src="../imgs/smartQuery.png" style="cursor: hand; height: 70px;"
                            title="命令行检索" onclick="simpleSearchNew()" /></a>
            </div>
            <div id="SmNavbom" style="vertical-align: middle; float: left">
                <ul>
                    <li><a href="../comm/zfgl.aspx" target="_blank">
                        <img src="../imgs/ico_zfgl.png" /><br />
                        政府管理</a></li>
                    <li><a href="../ZT/frmthlist.aspx" target="_blank">
                        <img src="../imgs/ico_ztk.png" /><br />
                        专题数据库</a></li>
                </ul>
            </div>
            <div id="divHidref" style="display: none">
                <a id="hrfRs" href="#" target="_blank">检索结果</a>
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript" language="javascript">
        //disp rs
        function myrefresh() {
            window.location.reload();
        }

        try {
            objDisPlayHef = document.getElementById("hrfRs");
            setTimeout('myrefresh()', 600000); //指定1秒(1000)刷新一次 600000=10分
        } catch (e) {
        }  
    </script>
</body>
</html>
