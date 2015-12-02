<%@ Page Title="" Language="C#" MasterPageFile="~/Master/index.Master" AutoEventWireup="true"
    CodeBehind="frmEnterpriseName.aspx.cs" Inherits="Patentquery.My.frmEnterpriseName" %>

<asp:Content ID="he1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <link href="../css/themes/default/default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../js/UserCollect.js"></script>
    <link href="../Css/B_SeachPage.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="left" style="width: 225px;">
        <div id="pinlieft">
            <div id="left_title " class="left_ti" style="text-align: center;">
                &nbsp;<span>辅助工具</span>&nbsp;
            </div>
            <div class="left_content2" style="padding: 0px; width: 220px">
                <div class="panel" style="width: 220px; background-color: #FBEC88">
                    <div class="panel-header accordion-header accordion-header-selected" style="height: 16px;
                        width: 210px; border-width: 0 0 1px;">
                        <div class="panel-title ">
                            <a href="frmSearchIPCIndex.aspx"><span>分类号关联查询</span></a>
                        </div>
                        <</div>
                </div>
                <div class="panel" style="width: 220px; background-color: #FBEC88">
                    <div class="panel-header accordion-header accordion-header-selected" style="height: 16px;
                        width: 210px; border-width: 0 0 1px;">
                        <div class="panel-title">
                            <a href="frmSearchWord.aspx"><span>相关词查询</span></a>
                        </div>
                    </div>
                </div>
                <div class="panel" style="width: 220px; background-color: #FBEC88">
                    <div class="panel-header accordion-header accordion-header-selected" style="height: 16px;
                        width: 210px; border-width: 0 0 1px;">
                        <div class="panel-title">
                            <a href="frmCountryCode.aspx"><span>国别代码查询</span></a>
                        </div>
                    </div>
                </div>
                <div class="panel" style="width: 220px;">
                    <div class="panel-header accordion-header" style="height: 16px; width: 210px; border-width: 0 0 0 0px;">
                        <div class="panel-title">
                            <a href="#"><span style="color: #800000;">企业名称查询</span></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="right" style="width: 768px; min-height: 550px;">
        <div id="right_top" class="right_top">
            请输入申请（专利权）人名称：<asp:TextBox ID="TextBox1" runat="server" Height="22px" Width="300px"></asp:TextBox>
            <asp:Button ID="btnSearch" CssClass="button" Height="32px" Width="79px" runat="server"
                Text="查询" OnClick="btnSearch_Click" />&nbsp;
            <input id="btnReset" type="reset" class="button" style="width: 79px; height: 32px;"
                value="重置" />
        </div>
        <div id="ipc_right_content" style="display: ;">
            <asp:GridView ID="grvRsData" runat="server" Width="100%" AutoGenerateColumns="False"  ForeColor="#1B2761" 
                AllowPaging="True" PageSize="20" OnPageIndexChanging="grvRsData_PageIndexChanging">
                <EmptyDataRowStyle CssClass="empty"></EmptyDataRowStyle>
                <EmptyDataTemplate>
                    <img src="../Images/iconImportant.png" alt="" />
                    暂无数据
                </EmptyDataTemplate>
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField HeaderText="申请（专利权）人名称" DataField="PA">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <PagerTemplate>
                    <div style="text-align: center; color: Blue">
                        共<asp:Label ID="Label1" ForeColor="Blue" runat="server" Text='<%# recordCount %>'></asp:Label>
                        条数据
                        <asp:LinkButton ID="cmdFirstPage" runat="server" CommandName="Page" CommandArgument="First"
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">首页</asp:LinkButton>
                        <asp:LinkButton ID="cmdPreview" runat="server" CommandArgument="Prev" CommandName="Page"
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">前页
                        </asp:LinkButton>
                        第<asp:Label ID="lblcurPage" ForeColor="Blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1  %>'></asp:Label>
                        页/共
                        <asp:Label ID="lblPageCount" ForeColor="blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>
                        页
                        <asp:LinkButton ID="cmdNext" runat="server" CommandName="Page" CommandArgument="Next"
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">后页</asp:LinkButton>
                        <asp:LinkButton ID="cmdLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">尾页</asp:LinkButton>
                        &nbsp;<asp:TextBox ID="txtGoPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>'
                            Width="32px" CssClass="inputmini"></asp:TextBox>页<asp:LinkButton ID="Button3" runat="server"
                                OnClick="Button3_Click" CausesValidation="false" Style="text-decoration: none"
                                Text="转到"></asp:LinkButton>
                    </div>
                </PagerTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
