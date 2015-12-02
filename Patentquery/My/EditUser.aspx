<%@ Page Language="C#" MasterPageFile="~/Master/index.master" AutoEventWireup="true"
    Inherits="My_EditUser" Title="" CodeBehind="EditUser.aspx.cs" %>

<asp:Content ID="he1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <link href="../css/themes/default/default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/UserCollect.js"></script>
    <link href="../Css/B_SeachPage.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="left" style="width: 225px;">
        <div id="pinlieft">
            <div id="left_title " class="left_ti" style="text-align: center;">
                &nbsp;<span>用户中心</span>&nbsp;
            </div>
            <div class="left_content2" style="padding: 0px; width: 220px">
                <div class="panel" style="width: 220px; background-color: #FBEC88">
                    <div class="panel-header accordion-header accordion-header-selected" style="height: 16px;
                        width: 210px; border-width: 0 0 1px;">
                        <div class="panel-title">
                            <a href="../sysadmin/"><span>>>>系统后台管理</span></a>
                        </div>
                    </div>
                </div>
                <div class="panel" style="width: 220px; background-color: #FBEC88">
                    <div class="panel-header accordion-header accordion-header-selected" style="height: 16px;
                        width: 210px; border-width: 0 0 1px;">
                        <div class="panel-title ">
                            <a href="#"><span style="color: #800000;">个人资料</span></a></div>
                        <</div>
                </div>
                <div class="panel" style="width: 220px; background-color: #FBEC88">
                    <div class="panel-header accordion-header accordion-header-selected" style="height: 16px;
                        width: 210px; border-width: 0 0 1px;">
                        <div class="panel-title">
                            <a href="frmQueryMag.aspx"><span>检索式管理</span></a>
                        </div>
                    </div>
                </div>
                <div class="panel" style="width: 220px; ">
                    <div class="panel-header accordion-header" style="height: 16px;
                        width: 210px; border-width: 0 0 1px;">
                        <div class="panel-title ">
                            <a href="UserCSIndex.aspx">标引管理</a></div>
                        <</div>
                </div>
                <div class="panel" style="width: 220px;">
                    <div class="panel-header accordion-header" style="height: 16px; width: 210px; border-width: 0 0 0 0px;">
                        <div class="panel-title">
                            <a href="frmCollectList.aspx"><span>我的收藏夹</span></a>
                        </div>
                    </div>
                </div>
                <div class="easyui-accordion " style="width: 220px; height: 410px;">
                    <div title="热门收藏" style="overflow: inherit; min-height: 250px;">
                        <div id="divtop" style="min-height: 250px; width: 202px; padding: 10px;">
                            <table id="tbhot" class="easyui-datagrid" style="width: 210px; overflow: visible"
                                data-options="singleSelect:true,collapsible:true">
                                <thead>
                                    <tr>
                                        <th data-options="field:'Title',width:210">
                                        </th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="right" style="width: 768px; min-height: 550px;">
        <asp:Panel ID="PanelView" runat="server">
            <dl class="userInfo">
                <dt>密码：</dt>
                <dd>
                    <asp:TextBox ID="TextBoxPassword" TextMode="Password" CssClass="textBox" runat="server" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorTextBoxPassword" ControlToValidate="TextBoxPassword"
                        ValidationExpression="^[\w\~\!\@\#\$\%\^\&\*\(\)_\-\+\=\[\]\\\|\}\{\:\;\<\>]{6,20}$"
                        ErrorMessage="密码格式有误" ToolTip="密码格式有误" Text="<img src='/Images/iconRequire.gif' />"
                        Display="Dynamic" runat="server" />位英文、数字或常用符号，不修改请留空
                </dd>
                <dt>确认密码：</dt>
                <dd>
                    <asp:TextBox ID="TextBoxConfirmPassword" TextMode="Password" CssClass="textBox" runat="server" />
                    <asp:CompareValidator ID="CompareValidatorConfirmPassword" ControlToCompare="TextBoxPassword"
                        ControlToValidate="TextBoxConfirmPassword" ErrorMessage="“密码”和“确认密码”必须匹配。" ToolTip="“密码”和“确认密码”必须匹配。"
                        Text="<img src='/Images/iconRequire.gif'>" Display="Dynamic" runat="server" />
                    再次输入上面的密码
                </dd>
                <dt>姓名：</dt>
                <dd>
                    <asp:TextBox ID="TextBoxTrueName" CssClass="textBox" runat="server" />
                </dd>
                <dt>手机：</dt>
                <dd>
                    <asp:TextBox ID="TextBoxMobile" CssClass="textBox" runat="server" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxMobile"
                        Display="None" ErrorMessage="您输入的手机号格式错误，请检查后输入！" ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
                </dd>
                <dt>电话：</dt>
                <dd>
                    <asp:TextBox ID="TextBoxTel" CssClass="textBox" runat="server" />
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="(如：0592-2102350) "></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBoxTel"
                        Display="None" ErrorMessage="您输入的电话格式错误，请检查后输入！" ValidationExpression="(\d{3,4}-)?\d{6,8}"></asp:RegularExpressionValidator>
                </dd>
                <dt>地址：</dt>
                <dd>
                    <asp:TextBox ID="TextBoxAdds" CssClass="textBox" runat="server" />
                </dd>
                <dt>邮箱：</dt>
                <dd>
                    <asp:TextBox ID="txtEmail" CssClass="textBox" runat="server" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmail"
                        Display="None" ErrorMessage="邮箱格式错误，请验证后输入" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </dd>
                <dt>&nbsp;&nbsp;</dt>
                <dd>
                    <div style="text-align: center; width: 380px;">
                        <asp:LinkButton ID="LinkButtonSave" Text="修改" OnClick="LinkButtonSave_Click" CssClass="btn"
                            runat="server" />
                        &nbsp;
                    </div>
                </dd>
            </dl>
        </asp:Panel>
        <asp:Panel ID="PanelResult" Visible="false" runat="server">
            <h2>
                修改成功!</h2>
        </asp:Panel>
    </div>
</asp:Content>
