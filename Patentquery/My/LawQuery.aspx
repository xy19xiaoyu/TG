<%@ Page Language="C#" MasterPageFile="~/Master/index.master" AutoEventWireup="true"
    Inherits="My_LawQuery" Title="" CodeBehind="LawQuery.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Css/B_cprs2010.css" rel="stylesheet" type="text/css" />
    <link href="../NewCSS/StyleSheet1.css" rel="stylesheet" type="text/css" />
    <link href="../Jquery-ui-1.10.3/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Jquery-ui-1.10.3/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.accordion.js" type="text/javascript"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/plugins/jQuery.niceTitle.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("input, img").niceTitle({ showLink: false }); //要排除一些例外的元素，例如可以用a:not([class='nono'])来排除calss为"nono"的a元素
        });
        $(function () {
            $("#DivPatDetailTabs").tabs();          
        });
        $(function () {
            $(".infoNew p").each(function () {
                var txt = $(this).parent().siblings().attr("src");
                $(this).html(txt);
            })

            $(".menuNew li").click(function () {
                var index = $(this).index();
                $(this).addClass("current").siblings().removeClass("current");
                $(".contentNew li").eq(index).show().siblings().hide();
            })

        })


        $(function () {
            $('input:button').click(function () {
                $('.input').val("");
            });
        })
        
    </script>
    <div class="div_Content_xiwl">
        <div id="DivPatDetailTabs" style="height: 550px">
            <ul id="ulPatTabs">
                <li><a href="#tabMianXml">中国专利法律状态检索</a><asp:Literal ID="LitTabsLi" runat="server" /></li>
                <%--<li><a href="#TabDegImgs">专利权利转移检索</a></li>
                <li><a href="#DivtabPdf">专利质押保全检索</a></li>
                <li><a href="#divTabDes">专利实施许可检索</a></li>--%>
            </ul>
            <div id="tabMianXml">
                <table>
                    <tr>
                        <td align="right">
                            专利申请号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAppNoFL" runat="server" CssClass="input" title="<p><strong>申请号</strong><br/>   例:2013101975930 </p>"></asp:TextBox>
                        </td>
                        <td align="right">
                            法律状态公告日：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtGongGaoR" runat="server" CssClass="input" title="&lt;p>&lt;strong>法律状态公告日&lt;/strong>&lt;br/>   例:20130724&lt;/p>"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            法律状态：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFaLvZT" runat="server" CssClass="input" title="&lt;p>&lt;strong>法律状态&lt;/strong>&lt;br/>   例:公开&lt;/p>"></asp:TextBox>
                        </td>
                        <td align="right">
                            专利权人：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtZhuanLiQR" runat="server" CssClass="input" title="&lt;p>&lt;strong>专利权人&lt;/strong>&lt;br/>   例:张三&lt;/p>"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnChaXunFL" runat="server" OnClick="btnChaXunFL_Click" Text="查询"
                                CssClass="buttoncss" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="Button1" type="button"
                                value="重置" onclick="" class="buttoncss" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="TabZlqlzy" runat="server" clientidmode="Static">
                <table>
                    <tr>
                        <td align="right">
                            申请(专利)号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAppNo" runat="server" CssClass="input" title="<p><strong>申请号</strong><br/>   例:2013101975930 </p>"></asp:TextBox>
                        </td>
                        <td align="right">
                            分类号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtIPC" runat="server" CssClass="input" title="&lt;p>&lt;strong>IPC 分类&lt;/strong>&lt;br/>   例:A01B&lt;/p>"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            名称：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMingChengZY" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                        <td align="right">
                            摘要：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtZhaiYaoZY" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            主权项：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtZhuQuanXiangZY" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            生效日：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtShengXiaoR" runat="server" CssClass="input" title="<p><strong>生效日</strong><br/>   例:20100818 </p>"></asp:TextBox>
                        </td>
                        <td align="right">
                            变更前权利人：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBianGengQQLR" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            变更后权利人：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBianGengHQLR" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                        <td align="right">
                            变更前地址：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBianGengQDZ" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            变更后地址：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBianGengHDZ" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnZhuanYi" runat="server" OnClick="btnZhuanYi_Click" Text="查询" CssClass="buttoncss" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input
                                id="Button2" type="button" value="重置" onclick="" class="buttoncss" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="TabZlzybq" runat="server" clientidmode="Static">
                <table>
                    <tr>
                        <td align="right">
                            申请(专利)号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAppNoZhiYa" runat="server" CssClass="input" title="<p><strong>申请号</strong><br/>   例:2013101975930 </p>"></asp:TextBox>
                        </td>
                        <td align="right">
                            分类号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtIPCZhiYa" runat="server" CssClass="input" title="&lt;p>&lt;strong>IPC 分类&lt;/strong>&lt;br/>   例:A01B&lt;/p>"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            名称：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMingChengBQ" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                        <td align="right">
                            摘要：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtZhaiYaoBQ" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            主权项：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtZhuQuanXiangBQ" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            法律状态：
                        </td>
                        <td align="left">
                            <asp:CheckBoxList ID="chkHeTongZT" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Selected="True">生效</asp:ListItem>
                                <asp:ListItem Selected="True">变更</asp:ListItem>
                                <asp:ListItem Selected="True">注销</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td align="right">
                            生效日：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtShengXiaoRBaoQuan" runat="server" CssClass="input" title="<p><strong>生效日</strong><br/>   例:20100224 </p>"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            变更日：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBianGengR" runat="server" CssClass="input" title="<p><strong>变更日</strong><br/>   例:20100408 </p>"></asp:TextBox>
                        </td>
                        <td align="right">
                            解除日：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtJieChuR" runat="server" CssClass="input" title="<p><strong>解除日</strong><br/>   例:20130218 </p>"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            合同登记号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDengJiH" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                        <td align="right">
                            出质人：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtChuZhiR" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            质权人：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtZhiQuanR" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnBaoQuan" runat="server" Text="查询" OnClick="btnBaoQuan_Click" CssClass="buttoncss" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input
                                id="Button3" type="button" value="重置" onclick="" class="buttoncss" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="TabZlssxk" runat="server" clientidmode="Static">
                <table>
                    <tr>
                        <td align="right">
                            申请(专利)号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAppNoXuKe" runat="server" CssClass="input" title="<p><strong>申请号</strong><br/>   例:2013101975930 </p>"></asp:TextBox>
                        </td>
                        <td align="right">
                            分类号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtIPCXuKe" runat="server" CssClass="input" title="&lt;p>&lt;strong>IPC 分类&lt;/strong>&lt;br/>   例:A01B&lt;/p>"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            名称：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMingChengXK" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                        <td align="right">
                            摘要：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtZhaiYaoXK" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            主权项：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtZhuQuanXiangXK" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            许可种类：
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlXuKeZL" runat="server">
                                <asp:ListItem Selected="True" Value="  ">请选择</asp:ListItem>
                                <asp:ListItem>独占许可</asp:ListItem>
                                <asp:ListItem>排他许可</asp:ListItem>
                                <asp:ListItem>普通许可</asp:ListItem>
                                <asp:ListItem>分许可</asp:ListItem>
                                <asp:ListItem>交叉许可</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            合同备案阶段：
                        </td>
                        <td align="left">
                            <asp:CheckBoxList ID="chkHeTongBAJD" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Selected="True">生效</asp:ListItem>
                                <asp:ListItem Selected="True">变更</asp:ListItem>
                                <asp:ListItem Selected="True">注销</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            备案日：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBeiAnR" runat="server" CssClass="input" title="<p><strong>备案日</strong><br/>   例:20080828 </p>"></asp:TextBox>
                        </td>
                        <td align="right">
                            变更日：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBianGengRiXuKe" runat="server" CssClass="input" title="<p><strong>变更日</strong><br/>   例:20100325 </p>"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            解除日：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtJieChuRiXuKe" runat="server" CssClass="input" title="<p><strong>解除日</strong><br/>   例:20130402 </p>"></asp:TextBox>
                        </td>
                        <td align="right">
                            合同备案号：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtHeTongBAH" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            让与人：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRangYuR" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                        <td align="right">
                            受让人：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtShouRangR" runat="server" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnXuKe" runat="server" Text="查询" OnClick="btnXuKe_Click" CssClass="buttoncss" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input
                                id="Button4" type="button" value="重置" onclick="" class="buttoncss" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>   
</asp:Content>
