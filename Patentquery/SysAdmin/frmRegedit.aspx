<%@ Page Title="用户注册" Language="C#" AutoEventWireup="true" CodeBehind="frmRegedit.aspx.cs"
    Inherits="Patentquery.SysAdmin.frmRegedit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>浙江省知识产权（专利）公共服务平台</title>
    <meta name="Keywords" content="浙江省知识产权（专利）公共服务平台" />
    <meta name="Description" content="浙江省知识产权（专利）公共服务平台" />
    <link href="../Css/index.css" rel="stylesheet" type="text/css" />
    <link href="../Css/B_SeachPage.css" rel="stylesheet" type="text/css" />
    <link href="../Css/smartNew.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="divSmarBody">
        <div id="divBaner" class="baneer">
            <div class="help" style="display: none">
                <ul>
                    <li><span class="bangzhu"><a href="../My/Help.aspx" target="_blank">帮助</a></span></li><li>
                        <span class="shouye"><a href="../My/Default.aspx" target="">首页</a></span></li></ul>
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
        <div class="Center">
            <div style="height: 10px">
            </div>
            <div class="ConntentRegInfo" style="">
                <asp:Panel ID="PanelView" runat="server">
                    <dl class="user">
                        <dt>用户类型：</dt>
                        <dd>
                            <asp:RadioButtonList ID="rbtYongHuLeiXing" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="rbtYongHuLeiXing_SelectedIndexChanged">
                                <asp:ListItem Selected="True">个人</asp:ListItem>
                                <asp:ListItem>企业</asp:ListItem>
                            </asp:RadioButtonList>
                        </dd>
                        <dt runat="server" id="ZZJGDMZ1">组织机构代码证：</dt>
                        <dd runat="server" id="ZZJGDMZ2">
                            <asp:FileUpload ID="fl" runat="server" CssClass="textBox" Height="28" /></dd>
                        <dt runat="server" id="QYMC1">企业名称：</dt>
                        <dd runat="server" id="QYMC2">
                            <asp:TextBox ID="txtQiYeMingCheng" runat="server" CssClass="textBox"></asp:TextBox>
                            <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </dd>
                        <dt>帐号：</dt>
                        <dd>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="textBox"></asp:TextBox>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </dd>
                        <dt>密码：</dt>
                        <dd>
                            <asp:TextBox ID="txtPWD" runat="server" CssClass="textBox" TextMode="Password"></asp:TextBox>
                            <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPWD"
                                Display="None" ErrorMessage="密码只能输入数字或字母" Text="<img src='/Images/iconRequire.gif' />"
                                ValidationExpression="^[\w\~\!\@\#\$\%\^\&\*\(\)_\-\+\=\[\]\\\|\}\{\:\;\<\>]{6,20}$"></asp:RegularExpressionValidator>
                            6~20位英文、数字或常用符号</dd>
                        <dt>确认密码：</dt>
                        <dd>
                            <asp:TextBox ID="txtQueRen" runat="server" CssClass="textBox" TextMode="Password"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            再次输入上面的密码
                        </dd>
                        <dt>姓名：</dt>
                        <dd>
                            <asp:TextBox ID="txtRealName" runat="server" CssClass="textBox"></asp:TextBox>
                            <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </dd>
                        <dt>联系电话：</dt>
                        <dd>
                            <asp:TextBox ID="txtDianHua" runat="server" CssClass="textBox"></asp:TextBox>
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="(如：0592-2102350) "></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDianHua"
                                Display="None" ErrorMessage="您输入的电话格式错误，请检查后输入！" ValidationExpression="(\d{3,4}-)?\d{6,8}"></asp:RegularExpressionValidator></dd>
                        <dt>手机：</dt>
                        <dd>
                            <asp:TextBox ID="txtShouJi" runat="server" CssClass="textBox"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtShouJi"
                                Display="None" ErrorMessage="您输入的手机格式错误，请检查后输入！" ValidationExpression="\d{11}"></asp:RegularExpressionValidator></dd>
                        <dt>通信地址：</dt>
                        <dd>
                            <asp:TextBox ID="txtDiZhi" runat="server" CssClass="textBox"></asp:TextBox>
                        </dd>
                        <dt>邮箱地址：</dt>
                        <dd>
                            <asp:TextBox ID="txtYouXiang" runat="server" CssClass="textBox"></asp:TextBox>
                            <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*(邮箱务必真实有效)"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtYouXiang"
                                Display="None" ErrorMessage="您输入的邮箱格式错误，请重新输入！" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </dd>
                        <dd style="text-align: center;">
                            <asp:ImageButton ID="ImageButtonLogin" ImageUrl="~/imgs/zhuce_bj2.png" runat="server"
                                OnClick="LinkButtonSave_Click" />
                            &nbsp; <a href="../frmLogin.aspx">
                                <img src="../imgs/Return.jpg" height="37px" /></a>
                        </dd>
                    </dl>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
                </asp:Panel>
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
