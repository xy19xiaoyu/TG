<%@ Page Language="C#" MasterPageFile="~/SysAdmin/MasterPage.master" AutoEventWireup="true" Inherits="frmUserInfoDetails" Title="用户管理" Codebehind="frmUserInfoDetails.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfUserLeiXing" runat="server" />
    <table cellpadding="0" cellspacing="0" border="1">
        <tr>
            <td align="right" valign="top">
                帐号：
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr id="pwd" runat="server">
            <td align="right" valign="top">
                密码：
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txtPWD" runat="server" TextMode="Password"></asp:TextBox>
                <asp:HiddenField ID="hfPWD" runat="server" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPWD"
                    Display="None" ErrorMessage="密码只能输入数字或字母" ValidationExpression="\d+|\w+"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                姓名：
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txtRealName" runat="server"></asp:TextBox>
            </td>
        </tr>  <tr>
            <td align="right" valign="top">
                企业名称：
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txtQiYeMingCheng" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr id="trUserLeiXing" runat="server">
            <td align="right" valign="top">
                用户类型：
            </td>
            <td align="left" valign="top">
                <asp:DropDownList ID="ddlYongHuLX" runat="server">
                    <asp:ListItem>系统管理</asp:ListItem>
                    <asp:ListItem>企业</asp:ListItem>
                    <asp:ListItem>政府</asp:ListItem>
                    <asp:ListItem>个人</asp:ListItem>                   
                </asp:DropDownList>
            </td>
        </tr>

          <tr>
            <td align="right" valign="top">
                联系电话：
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txtDianHua" runat="server"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" ForeColor="#FF3300" 
                    Text="(如：010-62102350)"></asp:Label>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                    Display="None" ErrorMessage="您输入的电话号码格式错误，请验证后输入！" 
                    ValidationExpression="(\d{3,4}-)?\d{6,8}" 
                    ControlToValidate="txtDianHua"></asp:RegularExpressionValidator>
            </td>
        </tr>
          <tr>
            <td align="right" valign="top">
                手机：
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txtShouJi" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                    ControlToValidate="txtShouJi" Display="None" ErrorMessage="您输入的手机格式错误，请验证后输入！" 
                    ValidationExpression="\d{11}"></asp:RegularExpressionValidator>
            </td>
        </tr>
          <tr>
            <td align="right" valign="top">
                通信地址：
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txtDiZhi" runat="server"></asp:TextBox>
            </td>
        </tr>
          <tr>
            <td align="right" valign="top">
                邮箱地址：
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txtYouXiang" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ControlToValidate="txtYouXiang" Display="None" 
                    ErrorMessage="您输入的邮箱地址格式错误，请重新输入！" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr id=trJueSe runat=server>
            <td align="right" valign="top">
                角色：
            </td>
            <td align="left" valign="top">
                <asp:CheckBoxList ID="chkRole" runat="server" RepeatColumns="2">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
            </td>
            <td align="left" valign="top">
                <asp:TextBox ID="txbMailBody" runat="server" Height="113px" 
                    TextMode="MultiLine" Width="99%"></asp:TextBox>
                <br />
                <asp:Button ID="btnCreatMailBody" runat="server" 
                    onclick="btnCreatMailBody_Click" Text="生成邮件内容" />
&nbsp;<asp:CheckBox ID="chkEmail" runat="server" Checked="True" Text="给用户发送通知邮件" />
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
            </td>
            <td align="left" valign="top">
                <asp:Button ID="btnQueDing" runat="server" Text="保存" OnClick="btnQueDing_Click" />
                &nbsp;<asp:Button ID="btnSendMail" runat="server" onclick="btnSendMail_Click" 
                    Text="仅发送邮件" />
&nbsp;<asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="返回" CausesValidation="False" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
                <asp:HiddenField ID="hfSH" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
