<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs"
    Inherits="Patentquery.frmLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>浙江省知识产权（专利）公共服务平台</title>
    <meta name="Keywords" content="浙江省知识产权（专利）公共服务平台" />
    <meta name="Description" content="浙江省知识产权（专利）公共服务平台" />
    <link href="Css/index.css" rel="stylesheet" type="text/css" />
    <link href="Css/smartNew.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="divSmarBody">
        <div id="divBaner" class="baneer">
            <div class="help" style="display: none">
                <ul>
                    <li><span class="bangzhu"><a href="../My/Help.aspx" target="_blank">帮助</a></span></li>
                    <li><span class="shouye"><a href="../My/opinion.aspx" target="" style="background: url(../Images/iconInvalid.png) no-repeat 20px 8px">
                        建议征集</a></span></li>
                    <li><span class="shouye"><a href="../My/dataScope.aspx" target="" style="background: url(../Images/iconUp.png) no-repeat 20px 8px">
                        数据说明</a></span></li>
                </ul>
            </div>
            <div id="nav" style="display: none">
                <div id="navtop">
                    <ul>
                        <li><a href="../My/SmartQuery.aspx" target="">智能检索</a></li>
                        <li>|</li>
                        <li><a href="../My/frmCnTbSearch.aspx" target="">表格检索</a></li>
                        <li>|</li>
                        <li><a href="../My/frmcnExpertSearch.aspx" target="">专家检索</a></li>
                        <li>|</li>
                        <li><a href="../My/LawQuery.aspx" target="">法律状态检索</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div id="divCenter" class="Center">
            <div id="divConntent" class="ConntentLogin" style="padding-top: 10px;">
                <div class="dl">
                    <div class="dltitle">
                    </div>
                    <div class="dlcn">
                        <ul class="dllf">
                            <li><strong>&nbsp;&nbsp; 会员登录</strong></li>
                            <li><span>用户名：</span><div class="dlsr">
                                <b>
                                    <asp:TextBox ID="TextBoxAccount" runat="server" /></b></div>
                            </li>
                            <li><span>密 码：</span><div class="dlsr">
                                <b>
                                    <asp:TextBox TextMode="Password" ID="Password" runat="server" /></b></div>
                            </li>
                            <li>
                                <div class="xdl">
                                </div>
                            </li>
                            <li>
                                <div class="dlbut">
                                    <asp:ImageButton ID="ImageButtonLogin" ImageUrl="~/imgs/denglu_bj.png" runat="server"
                                        OnClick="ImageButtonLogin_Click" />
                                    <asp:ImageButton ID="imgBtnYk" ImageUrl="~/imgs/youke_bj.png" runat="server" OnClick="imgBtnYk_Click" />
                                </div>
                            </li>
                        </ul>
                        <ul class="dlrt">
                            <li><strong>还没账户？</strong></li>
                            <li><a href="#" onclick="top.location.href='http://www.zjip.org/login.do?method=register'">
                                <img src="imgs/zhuce_bj.png" alt="立即注册"></a></li>
                            <li>
                                <p>
                                    成为注册用户，您可以使用到更多功能！如您已拥有帐户，可以在左侧登录。</p>
                            </li>
                        </ul>
                    </div>
                </div>
                <br />
            </div>
             <div class="Smartfoot" style="padding: 5px;">
                &copy;2014 版权所有：浙江省知识产权研究与服务中心<br />
                <span>技术支持：北京新发智信科技有限责任公司&nbsp;&nbsp;&nbsp;客服热线1:010-6107xxx1 &nbsp; 客服热线2: 010-6107xxx2
                </span>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
