<%@ Page Language="C#" MasterPageFile="~/Master/index.master" AutoEventWireup="true"
    Inherits="My_PatternList" Title="" CodeBehind="frmQueryMag.aspx.cs" %>

<asp:Content ID="he1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <link href="../css/themes/default/default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/UserCollect.js"></script>
    <link href="../Css/B_SeachPage.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../JS/jDatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true"
        runat="server">
    </asp:ScriptManager>
    <div id="left" style="width: 225px;">
        <div id="pinlieft">
            <div id="left_title " class="left_ti" style="text-align: center;">
                &nbsp;<span>用户中心</span>&nbsp;
            </div>
            <div class="left_content2" style="padding: 0px; width: 220px; min-height: 505px">
                <div id="leftmue" class="easyui-accordion " style="width: 220px; height: 505px;">
                    <div title=">>>系统后台管理" data-options="selected:false" url="../sysadmin/"></div>
                    <div title="个人资料" data-options="selected:false" url="EditUser.aspx"></div>
                    <div title="检索式管理" data-options="selected:false" url="frmQueryMag.aspx"></div>
                    <div title="标引管理" data-options="selected:false" url="UserCSIndex.aspx"></div>
                    <div title="我的收藏夹" data-options="selected:false" url="frmCollectList.aspx" style="overflow: auto; padding: 10px; min-height: 250px;">
                        <div id="divzt" style="min-height: 250px">
                            <ul id="CO" class="easyui-tree" data-options="lines:true" />
                        </div>
                    </div>
                    <div title="热门收藏" data-options="selected:true" url="" style="overflow: inherit; min-height: 250px;">
                        <div id="divtop" style="min-height: 250px; width: 202px; padding: 10px;">
                            <table id="tbhot" class="easyui-datagrid" style="width: 210px; overflow: visible"
                                data-options="singleSelect:true,collapsible:true">
                                <thead>
                                    <tr>
                                        <th data-options="field:'Title',width:210"></th>
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="Boxdiv_xiwl">
                    <div style="padding-left: 20px">
                        <asp:RadioButtonList ID="RadioButtonListTypes" RepeatColumns="10" OnSelectedIndexChanged="RadioButtonListTypes_SelectedIndexChanged"
                            AutoPostBack="true" runat="server">
                            <asp:ListItem Text="中国专利" Value="0" Selected="True" />
                            <asp:ListItem Text="世界专利" Value="1" />
                        </asp:RadioButtonList>
                    </div>
                    <div class="Boxdiv_xiwl toolbarAccount">
                        <div>
                            <table width="100%">
                                <tr style="padding-top: 2px; vertical-align: middle">
                                    <td align="right" style="padding-top: 2px; vertical-align: middle">来 源：
                                    </td>
                                    <td align="left" style="padding-top: 2px; vertical-align: middle; width: 145px;">
                                        <asp:DropDownList ID="ddlModel" runat="server" CssClass="" Width="140px">
                                            <asp:ListItem Value="-1">所有</asp:ListItem>
                                            <asp:ListItem Value="0">智能检索</asp:ListItem>
                                            <asp:ListItem Value="1">表格检索</asp:ListItem>
                                            <asp:ListItem Value="2">专家检索</asp:ListItem>
                                            <asp:ListItem Value="3">分类导航检索</asp:ListItem>
                                            <asp:ListItem Value="4">二次检索</asp:ListItem>
                                            <asp:ListItem Value="5">过滤检索</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="padding-top: 2px; vertical-align: middle">检索式：
                                    </td>
                                    <td align="left" style="padding-top: 2px; vertical-align: middle; width: 145px">
                                        <asp:TextBox ID="TextBoxKeyword" runat="server" Width="165" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td align="right" style="padding-top: 2px; vertical-align: middle">检索时间：
                                    </td>
                                    <td align="center" colspan="3" style="padding-top: 2px; vertical-align: middle; text-align: justify;">
                                        <asp:TextBox ID="txtSTime" Width="190" runat="server" CssClass="Wdate" onClick="var d5222=$dp.$('ctl00_ContentPlaceHolder1_txtEndTime');WdatePicker({onpicked:function(){d5222.focus();},maxDate:'#F{$dp.$D(\'ctl00_ContentPlaceHolder1_txtEndTime\')}'})"></asp:TextBox>
                                        &nbsp; 至&nbsp;
                                        <asp:TextBox ID="txtEndTime" Width="190" runat="server" CssClass="Wdate" onFocus="WdatePicker({minDate:'#F{$dp.$D(\'ctl00_ContentPlaceHolder1_txtSTime\')}'})"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:LinkButton ID="LinkButtonSearch" runat="server" CssClass="btn" OnClick="LinkButtonSearch_Click"
                                            Text="搜索" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="Boxdiv_xiwl">
                        <asp:GridView ID="GridView1" AllowPaging="True" PagerStyle-HorizontalAlign="Center"
                            PagerStyle-CssClass="pager" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnRowDataBound="GridView1_RowDataBound" DataKeyNames="PatternId" Width="100%"
                            GridLines="None" CssClass="gridView" AlternatingRowStyle-BackColor="#F7F7F7"
                            runat="server">
                            <AlternatingRowStyle BackColor="#F7F7F7"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <input type="checkbox" id="input2" onclick="selectAll(this)" />
                                        全选
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBoxSelect" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="检索式" ItemStyle-Width="300">
                                    <ItemTemplate>
                                        <div style="width: 300px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;"
                                            title='(<%# Eval("Number") %>) <%# Eval("Expression") %>'>
                                            (<%# Eval("Number") %>)
                                            <%# Eval("Expression") %>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="310px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="命中数" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("Hits") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="来源" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# ShowSource(Convert.ToByte(Eval("Source"))) %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="时间" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Convert.ToDateTime(Eval("CreateDate")).ToString("yyyy-MM-dd HH:mm:ss") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="执行">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# GetDispLayUrl(Eval("Types").ToString(), Eval("Number").ToString(), Eval("Hits").ToString(), Eval("Expression").ToString()) %>'
                                            Text="查看" Target="_blank"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" CssClass="pager"></PagerStyle>
                            <RowStyle Height="28px" />
                        </asp:GridView>
                        <asp:LinkButton ID="LinkButtonDeleteSelected" Text="删除" OnClick="LinkButtonDeleteSelected_Click"
                            OnClientClick="return confirm('您确认要删除么？')" CssClass="btn" runat="server" />
                        <asp:LinkButton ID="LinkButtonExport" Text="导出" OnClick="LinkButtonExport_Click"
                            CssClass="btn" runat="server" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function selectAll(obj) {
            var SetValue = obj.checked;  //  $(obj).attr("checked"); //trun|false
            //alert(SetValue);
            //$('input[type=checkbox]').attr('checked', $(checkbox).attr('checked'));
            $("input[type='checkbox']").attr("checked", SetValue);
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $(".accordion-header").each(function () {

                var url = $(this).parent().find("div.accordion-body").attr("url");
                if (url == null || url == "" || url == "undefined") {
                    return;
                }
                //如果有Url  panel不展开，取消Musedown 时间注册 
                $(this).parent().unbind();
                $(this).parent().bind("mousedown", function (e) {
                    console.log(e.button);
                    if (e.button == 0) {

                        var url = $(this).find("div.accordion-body").attr("url");
                        if (url == null || url == "" || url == "undefined") {
                            return;
                        }
                        $(this).parent().unbind();
                        location.href = url;
                    }
                });
            });
        });
    </script>
</asp:Content>
