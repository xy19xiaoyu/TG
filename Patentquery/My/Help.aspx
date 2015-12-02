<%@ Page Language="C#" MasterPageFile="~/Master/Index.master" AutoEventWireup="true"
    Inherits="My_Help" Title="" CodeBehind="Help.aspx.cs" %>

<asp:Content ID="he1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <link href="../css/themes/default/default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/UserCollect.js"></script>
    <link href="../Css/B_SeachPage.css" rel="stylesheet" type="text/css" />
    <style>
        .new_list li
        {
            border-bottom: 1px dashed #CCC;
            line-height: 35px;
            padding-left: 15px;
        }
        .more
        {
            float: right;
            padding-right: 10px;
            font-weight: lighter;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="left" style="width: 225px;">
        <div id="pinlieft">
            <div id="left_title " class="left_ti" style="text-align: center;">
                &nbsp;<span>帮助中心</span>&nbsp;
            </div>
            <div class="left_content2" style="padding: 0px; width: 220px">
                <div class="easyui-accordion " style="width: 220px; height: 500px;">
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
    <div id="right" style="width: 768px; min-height: 540px;">
        <br />
        <div style="width: 95%; padding-left: 25px">
            <ul class="new_list color_e">
                <asp:GridView ID="grvInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    PageSize="12" ShowHeader="False" BorderWidth="0px" CellPadding="4" CellSpacing="1"
                    Width="99%" BackColor="#cccccc" GridLines="Both" OnPageIndexChanging="grvInfo_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <li><a target="_blank" href='<%# String.Format("../ZtHeadImg/{0}", DataBinder.Eval(Container.DataItem,"filename"))%>'>
                                    <span class="more">
                                        <%# String.Format("{0:yyyy年MM月dd日}", DataBinder.Eval(Container.DataItem, "uploaddate"))%>
                                    </span>
                                    <%# DataBinder.Eval(Container.DataItem, "helptitle")%>
                                </a></li>
                            </ItemTemplate>
                            <ItemStyle Width="80%" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                        <div class="page1">
                            <asp:LinkButton ID="btnFirst" CommandName="Page" Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>"
                                CommandArgument="first" runat="server">首 页</asp:LinkButton>
                            <asp:LinkButton ID="btnPrev" CommandName="Page" Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>"
                                CommandArgument="prev" runat="server">上一页</asp:LinkButton>
                            第<asp:Label ID="lblcurPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1      %>'></asp:Label>页/共<asp:Label
                                ID="lblPageCount" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>页
                            <asp:LinkButton ID="btnNext" CommandName="Page" Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>"
                                CommandArgument="next" runat="server">下一页</asp:LinkButton>
                            <asp:LinkButton ID="btnLast" CommandName="Page" Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>"
                                CommandArgument="last" runat="server">末 页</asp:LinkButton>
                        </div>
                    </PagerTemplate>
                    <PagerStyle HorizontalAlign="center" />
                    <RowStyle BackColor="White" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" />
                    <PagerStyle BackColor="White" HorizontalAlign="center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#E3EAEB" />
                    <EditRowStyle BackColor="#e1ecd3" />
                    <AlternatingRowStyle BackColor="#e1ecf3" />
                </asp:GridView>
            </ul>
        </div>
    </div>
</asp:Content>
