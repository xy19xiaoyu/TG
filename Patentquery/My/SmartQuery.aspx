<%@ Page Language="C#" MasterPageFile="~/Master/Smart.master" AutoEventWireup="true"
    Inherits="My_SmartQuery" Title="" CodeBehind="SmartQuery.aspx.cs" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="/Js/smartPage.js" type="text/javascript"></script>
    <script src="../Js/SearchCommon.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/plugins/jquery.funkyUI.js" type="text/javascript"></script>
    <script type="text/javascript">
        //        $(document).ready(function () {
        //            $('#btnPatentEn,#btnPatentCn,#btnQuery')
        //                        .wrapInner('<span class="hover"></span>')
        //                        .css('textIndent', '0')
        //        				.each(function () {
        //        				    $('span.hover').css('opacity', 100).hover(function () {
        //        				        //$(this).stop().fadeTo(650, 1);
        //        				    }, function () {
        //        				        // $(this).stop().fadeTo(650, 0);
        //        				    });
        //        				});
        //        });
        function Select() {
            var SetValue = $("#chkAll").attr("checked"); //trun|false
            if (SetValue == "checked") {
                $("input[name='chk_list']").attr("checked", SetValue);
            }
            else {
                $("input[name='chk_list']").removeAttr("checked");
            }

        }
        $(document).ready(function () {
            //alert(requestUrl('db'));   alert(getUrlParam('db'));
            if (getUrlParam('db') == "EN") {
                switchPatentType(document.getElementById("btnPatentEn"));
            }
        });
    </script>
    <div class="SearchLeftPad">
        <div id="divCnSmart" >
            <div class="DivSelect" style="font-weight: bold">
            <ul>
                <li>
                    <label for="rdAll">
                        <input id="rdAll" type="radio" checked="checked" name="LegDbType" title="全库" value="" />全库</label></li>
                <li>
                    <label for="rdYX">
                        <input id="rdYX" type="radio" name="LegDbType" title="有效库" value="@YX" />有效库
                    </label>
                </li>
                <li>
                    <label for="rdSX">
                        <input id="rdSX" type="radio" name="LegDbType" title="失效库" value="@SX" />失效库</label></li></ul>
            </div>
            <br />
            <div class="DivSelect">
            <ul>
                <li>
                    <label>
                        <input name="checkbox" checked="checked" type="checkbox" value="DI" />发明公开【DI】</label></li>
                <li>
                    <label>
                        <input name="checkbox" checked="checked" type="checkbox" value="UM" />实用新型【UM】</label></li>
                <li>
                    <label>
                        <input name="checkbox" checked="checked" type="checkbox" value="DP" />外观设计【DP】</label></li>
                <li>
                    <label>
                        <input name="checkbox" type="checkbox" value="AI" />发明授权【AI】</label></li>
            </ul>
            </div>
        </div>
        <div id="divWdSmart" style="overflow: auto; display:none;">
            <div class="DivSelect"><label>
                <input type="checkbox" id="chkAll" onclick="Select();" />全选</label>
            </div>
            <div class="DivSelect">
                <ul>
                    <li>
                        <label><input name="chk_list" type="checkbox" value="CN" />中国[CN]</label></li>
                    <li>
                        <label><input name="chk_list" type="checkbox" value="US" />美国[US]</label></li>
                    <li>
                        <label><input name="chk_list" type="checkbox" value="DE" />德国[DE]</label></li>
                    <li>
                        <label><input name="chk_list" type="checkbox" value="JP" />日本[JP]</label></li>
                    <li>
                        <label><input name="chk_list" type="checkbox" value="GB" />英国[GB]</label></li>
                    <li>
                        <label><input name="chk_list" type="checkbox" value="FR" />法国[FR]</label></li>
                    <li>
                        <label><input name="chk_list" type="checkbox" value="KR" />韩国[KR]</label></li>
                    <li>
                        <label><input name="chk_list" type="checkbox" value="RU" />俄罗斯[RU]</label></li>
                    <li>
                        <label><input name="chk_list" type="checkbox" value="CH" />瑞士[CH]</label></li>
                    <li>
                        <label><input name="chk_list" type="checkbox" value="EP" />EPO[EP]&nbsp;</label></li>
                    <li>
                        <label><input name="chk_list" type="checkbox" value="WO" />WIPO[WO]</label></li>
                    <li style="display: ">
                        <label><input name="chk_list" type="checkbox" value="ELSE" />其他[OT]<img style="display: none"
                            src="../images/img/home-20.jpg" /></label></li>
                </ul>
            </div>
                      
        </div>
        <br />
        <br />
        <div id="BtnUl">
            <ul>
                <li><a id="btnPatentCn" class="btnPatentCn" onclick="switchPatentType(this)">中国专利检索
                </a></li>
                <li><a id="btnPatentEn" class="btnPatentEnOff" onclick="switchPatentType(this)">世界专利检索</a>
                </li>
            </ul>
        </div>
        <div id="divText" style="vertical-align: middle;">
            <textarea id="searchContent" cols="20" rows="2" class="simpleSearchTxb"></textarea>&nbsp;
            <a id="btnQuery_11" href="javascript:;">
                <img id="BtnSearch" alt="检索" src="../imgs/smartQuery.png" style="cursor: hand; height: 50px;"
                    title="命令行检索" onclick="simpleSearchNew()" /></a>
        </div>
    </div>
</asp:Content>
