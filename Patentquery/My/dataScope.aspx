﻿<%@ Page Title="数据范围说明" Language="C#" MasterPageFile="~/Master/index.Master" AutoEventWireup="true"
     CodeBehind="dataScope.aspx.cs" Inherits="Patentquery.My.dataScope" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table style="width: 100%" border="1" cellspacing="2" cellpadding="2" frame="box"
            align="center">
            <tr style="vertical-align: top">
                <td class="Tj_Data" width="100%">
                    <div style="width: 100%" id="Div1">
                        <table style="border-right-width: 0px; background-color: #cccccc; width: 100%; border-top-width: 0px;
                            border-bottom-width: 0px; border-left-width: 0px" id="Table1" border="0" rules="all"
                            cellspacing="1" cellpadding="4">
                            <tr style="background-color: #e3eaeb; height: 30px">
                                <th scope="col">
                                    平台数据更新说明
                                </th>
                            </tr>
                            <tr style="background-color: white; height: 30px" align="left">
                                <td>
                                    1、中国专利平均两周更新一次；每次更新在周五下午4点开始；
                                </td>
                            </tr>
                            <tr style="background-color: white; height: 30px" align="left">
                                <td>
                                    2、世界专利平均每月更新一次；与中国专利一起更新，每次更新在周五下午4点开始；
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="vertical-align: top">
                <td class="Tj_Data" width="100%">
                    <div style="width: 100%" id="Div2">
                        <table style="border-right-width: 0px; background-color: #cccccc; width: 100%; border-top-width: 0px;
                            border-bottom-width: 0px; border-left-width: 0px" id="grdGjTjTable" border="0"
                            rules="all" cellspacing="1" cellpadding="4">
                            <tr style="background-color: #e3eaeb; height: 30px">
                                <th scope="col" colspan="4">
                                    平台数据统计表
                                </th>
                            </tr>
                            <tr style="background-color: #e1ecf3; height: 30px">
                                <th scope="col">
                                    数据名称
                                </th>
                                <th scope="col">
                                    数据范围
                                </th>
                                <th scope="col">
                                    文摘总量
                                </th>
                                <th scope="col">
                                    全文图形总量
                                </th>
                            </tr>
                            <tr style="background-color: white; height: 30px" align="middle">
                                <td>
                                    中国发明专利
                                </td>
                                <td>
                                    —
                                </td>
                                <td>
                                    1813036
                                </td>
                                <td>
                                    1813036
                                </td>
                            </tr>
                            <tr style="background-color: white; height: 30px" align="middle">
                                <td>
                                    中国实用新型专利
                                </td>
                                <td>
                                    —
                                </td>
                                <td>
                                    2538659
                                </td>
                                <td>
                                    2538659
                                </td>
                            </tr>
                            <tr style="background-color: white; height: 30px" align="middle">
                                <td>
                                    中国外观专利
                                </td>
                                <td>
                                    —
                                </td>
                                <td>
                                    1762556
                                </td>
                                <td>
                                    —
                                </td>
                            </tr>
                            <tr style="background-color: #e1ecf3; height: 30px" align="middle">
                                <td>
                                    中国专利合计
                                </td>
                                <td>
                                    截止到2011-12-14已公开的数据
                                </td>
                                <td>
                                    6114251
                                </td>
                                <td>
                                    4351695
                                </td>
                            </tr>
                            <tr style="background-color: white; height: 30px" align="middle">
                                <td>
                                    美国专利
                                </td>
                                <td>
                                    —
                                </td>
                                <td>
                                    10992693
                                </td>
                                <td>
                                    10649216
                                </td>
                            </tr>
                            <tr style="background-color: white; height: 30px" align="middle">
                                <td>
                                    英国专利
                                </td>
                                <td>
                                    —
                                </td>
                                <td>
                                    2667969
                                </td>
                                <td>
                                    2170736
                                </td>
                            </tr>
                            <tr style="background-color: white; height: 30px" align="middle">
                                <td>
                                    日本专利
                                </td>
                                <td>
                                    —
                                </td>
                                <td>
                                    18946520
                                </td>
                                <td>
                                    16565968
                                </td>
                            </tr>
                            <tr style="background-color: white; height: 30px" align="middle">
                                <td>
                                    德国专利
                                </td>
                                <td>
                                    —
                                </td>
                                <td>
                                    6433097
                                </td>
                                <td>
                                    5699141
                                </td>
                            </tr>
                            <tr style="background-color: white; height: 30px" align="middle">
                                <td>
                                    瑞士专利
                                </td>
                                <td>
                                    —
                                </td>
                                <td>
                                    709771
                                </td>
                                <td>
                                    681034
                                </td>
                            </tr>
                            <tr style="background-color: white; height: 30px" align="middle">
                                <td>
                                    法国专利
                                </td>
                                <td>
                                    —
                                </td>
                                <td>
                                    2539429
                                </td>
                                <td>
                                    2683259
                                </td>
                            </tr>
                            <tr style="background-color: white; height: 30px" align="middle">
                                <td>
                                    其它
                                </td>
                                <td>
                                    —
                                </td>
                                <td>
                                    —
                                </td>
                                <td>
                                    —
                                </td>
                            </tr>
                            <tr style="background-color: #e1ecf3; height: 30px" align="middle">
                                <td>
                                    世界专利合计
                                </td>
                                <td>
                                    截止到2011-12-2已公开的数据
                                </td>
                                <td>
                                    75987464
                                </td>
                                <td>
                                    64057843
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
