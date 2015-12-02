<%@ Page Title="" Language="C#" MasterPageFile="~/Master/index.Master" AutoEventWireup="true"
    CodeBehind="Opinion.aspx.cs" Inherits="Patentquery.My.Opinion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Css/B_cprs2010.css" rel="stylesheet" type="text/css" />--%>
    <style type="text/css">
        .xbgNUM020
        {
        }
        #mid
        {
            min-height: 350px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function CheckInput() {
            if (document.getElementById("txbUName").value.trim() == "") {
                alert("请输入您的姓名!");
                return false;
            }
            if (document.getElementById("txbTitle").value.trim() == "") {
                alert("请输入意见标题!");
                return false;
            }

            if (document.getElementById("txbContent").value.trim() == "") {
                alert("请输入您意见内容!");
                return false;
            }

            if (document.getElementById("txbContent").value.trim().length<5) {
                alert("意见内容请勿少于5个字!");
                return false;
            }
        }

        function ClearTxt() {
            document.getElementById("txbUName").value = "";
            document.getElementById("txbTitle").value = "";
            document.getElementById("txbContent").value = "";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<div class="div_Content_xiwl home2_con">--%>
    <br />
    <div id="divQueryTable" class="home2_table div_xiwl" style="overflow: auto;">
        <table width="98%" border="0" cellspacing="0" cellpadding="0" style="padding-left: 20px">
            <tbody>
                <tr>
                    <td background="" height="30" style="background-color: #F8F8F8">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tbody>
                                <tr>
                                    <td align="center" width="32">
                                        <img src="../Images/iconInvalid.png">
                                    </td>
                                    <td>
                                        <strong>发表意见</strong>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <table class="SWXC06_5" cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tbody>
                                <tr style="background-color: #F8F8F8">
                                    <td align="right" height="55" width="213">
                                        您的姓名：
                                    </td>
                                    <td width="773">
                                        <asp:TextBox ID="txbUName" runat="server" CssClass="xbgNUM020" MaxLength="10" 
                                            Width="234px" ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8F8F8">
                                    <td align="right" height="55" width="213">
                                        意见标题：
                                    </td>
                                    <td width="773">
                                        <asp:TextBox ID="txbTitle" runat="server" CssClass="xbgNUM020" MaxLength="100" 
                                            Width="241px" ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="background-color: #F9FBFC;">
                                    <td align="right" height="100" valign="top">
                                        <br>
                                        意见内容：
                                    </td>
                                    <td>
                                        <b class="fluid-input"><b class="fluid-input-inner">
                                            <asp:TextBox ID="txbContent" runat="server" TextMode="MultiLine" 
                                            Height="82px" MaxLength="500"
                                                Rows="6" Width="500px" ClientIDMode="Static"></asp:TextBox>
                                        </b></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8F8F8">
                                    <td align="center">
                                        &nbsp;
                                    </td>
                                    <td height="50">
                                        <asp:Button ID="Button1" runat="server" Text="提交意见" Height="30px" OnClientClick="return CheckInput()"
                                            Width="79px" OnClick="Button1_Click" />
                                        &nbsp;<asp:Button ID="Button2" runat="server" Text="重置" Height="30px" Width="79px"
                                            OnClientClick="ClearTxt();return false;" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <%--</div>--%>
</asp:Content>
