<%@ Page Title="" Language="C#" MasterPageFile="~/Master/My.master" AutoEventWireup="true"
    CodeBehind="frmYJJingZhengDS.aspx.cs" Inherits="Patentquery.YJ.frmYJJingZhengDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="../Jquery-ui-1.10.3/themes/base/jquery-ui.css">
    <script src="../Jquery-ui-1.10.3/jquery-1.9.1.js"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery-ui.js"></script>
    <script src="../Jquery-ui-1.10.3/ui/jquery.ui.widget.js"></script>
    <script>
        $(function () {
            var name = $("#name"),
            email = $("#email"),
            password = $("#password"),
            allFields = $([]).add(name).add(email).add(password),
            tips = $(".validateTips");

            function updateTips(t) {
                tips.text(t).addClass("ui-state-highlight");
                setTimeout(function () {
                    tips.removeClass("ui-state-highlight", 1500);
                }, 500);
            }

            function checkLength(o, n, min, max) {
                if (o.val().length > max || o.val().length < min) {
                    o.addClass("ui-state-error");
                    updateTips("Length of " + n + " must be between " + min + " and " + max + ".");

                    return false;

                } else {

                    return true;

                }
            }



            function checkRegexp(o, regexp, n) {

                if (!(regexp.test(o.val()))) {

                    o.addClass("ui-state-error");

                    updateTips(n);

                    return false;

                } else {

                    return true;

                }

            }

            function ok() {
                var options = {
                    type: "POST",
                    url: "frmYJJingZhengDS.aspx/GetAvailableTickets",
                    data: "{ID:" + $("#name").val() + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        $("#users tbody").append(response.d);

                    }
                };
                //Call the PageMethods
                $.ajax(options);

            }

            $("#dialog-form").dialog({

                autoOpen: false,

                height: 300,

                width: 350,

                modal: true,

                buttons: {

                    "Create an account": function () {
                        ok();
                        $(this).dialog("close");
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                },

                close: function () {

                    allFields.val("").removeClass("ui-state-error");

                }

            });



            $("#create-user").button().click(function () {
                $("#dialog-form").dialog("open");
            });
        });

    </script>
    <div id="dialog-form" title="Create new user">
        <form>
        <table id="fieldset">
        </table>
        </form>
    </div>
    <table cellpadding="0" cellspacing="0" border="1" width="100%">
        <tr>
            <td valign="top" align="left" colspan="2">
                <asp:LinkButton ID="btnCN" runat="server">中国专利预警</asp:LinkButton>
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="btnEN" runat="server">世界专利预警</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
            </td>
            <td valign="top" align="left">
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem>竞争对手</asp:ListItem>
                    <asp:ListItem>预警设置日期</asp:ListItem>
                    <asp:ListItem>预警名称</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtKeyWord" runat="server"></asp:TextBox>
                <asp:Button ID="btnChaXun" runat="server" OnClick="btnChaXun_Click" Text="查询" />
                <asp:Button ID="btnAdd" runat="server" Text="新增" />
                <button id="create-user">
                    新增</button>
                <asp:Button ID="btnUpdate" runat="server" Text="修改" />
                <asp:Button ID="btnDel" runat="server" Text="删除" />
                <asp:Button ID="btnReDian" runat="server" Text="热点预警" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="left">
            </td>
            <td valign="top" align="left">
                <div id="users-contain" class="ui-widget">
                    <h1>
                        Existing Users:</h1>
                    <table id="users" class="ui-widget ui-widget-content">
                        <asp:GridView ID="grvInfo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None" Width="100%">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="预警名称">
                                    <ItemTemplate>
                                        <a href="#">
                                            <%# Eval("ALIAS")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="C_DATE" HeaderText="预警设置日期" />
                                <asp:BoundField DataField="CURRENTNUM" HeaderText="当前专利数" />
                                <asp:BoundField DataField="CHANGENUM" HeaderText="变更数量" />
                                <asp:BoundField DataField="BEIZHU" HeaderText="备注" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href="#">手动更新</a> <a href="#">预警历史</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </table>
                </div>
                <div class="demo-description">
                    <p>
                        Use a modal dialog to require that the user enter data during a multi-step process.
                        Embed form markup in the content area, set the <code>modal</code> option to true,
                        and specify primary and secondary user actions with the <code>buttons</code> option.</p>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
