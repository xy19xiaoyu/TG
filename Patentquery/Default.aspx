<%@ Page Language="C#" AutoEventWireup="true" Inherits="_Default" CodeBehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>登录 - 浙江省知识产权（专利）公共服务平台</title>
    <meta name="Keywords" content="浙江省知识产权（专利）公共服务平台" />
    <meta name="Description" content="浙江省知识产权（专利）公共服务平台" />
    <link href="~/Css/Login.css" rel="stylesheet" type="text/css" />
    <link href="~/favicon.ico" rel="shortcut icon" />
    <script type="text/javascript" src="/Js/Common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="box">
        <div id="title">
            厦漳泉科技基础资源服务平台-专利平台
        </div>
        <div id="login">
            <div id="info">
                <dl>
                    <dt>帐号 </dt>
                    <dd>
                        <asp:TextBox ID="TextBoxAccount" CssClass="textBox" runat="server" />
                    </dd>
                    <dt>密码 </dt>
                    <dd style="float: left; margin-left: 0px;">
                        <asp:TextBox TextMode="Password" ID="Password" CssClass="textBox" runat="server" />
                    </dd>
                    <dt></dt>
                    <dd>
                    </dd>
                </dl>
            </div>
            <div id="command">
                <asp:ImageButton ID="ImageButtonLogin" ImageUrl="/Images/btnLogin.png" CommandName="Login"
                    runat="server" OnClick="ImageButtonLogin_Click" /><br />
            </div>
        </div>
    </div>
    <div id="shadow">
    </div>
    <div id="footer">
        &copy;2014 版权所有：浙江省知识产权研究与服务中心<br />
        <span>技术支持：北京新发智信科技有限责任公司&nbsp;&nbsp;&nbsp;客服热线1:010-6107xxx1 &nbsp; 客服热线2: 010-6107xxx2
        </span>
    </div>
    </form>
</body>
</html>
