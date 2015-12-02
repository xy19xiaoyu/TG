<%@ Page Title="主页" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="test._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="addyjdiv" style=" height:auto;">
    <table>
        <tr>
            <td>
                <span id="dgti1">预警名称：</span>
            </td>
            <td>
                <input type="text" id="itxtname" />
            </td>
        </tr>
        <tr>
            <td>
                <div id="divfanwei" >
                    <span>预警范围：</span>
                    <select id="ddlfanwei" runat="server">
                    </select>                    
                </div>
            </td>
            <td>
                 <span id="dgti2"></span>
                <img id="TiShi" src="../images/img/home-08.jpg" alt="提示:高级预警" style="cursor: pointer;"
                    title="<strong>提示</strong><br/>     &nbsp;&nbsp;发明名称（TI）、摘要（AB）<br/>     &nbsp;&nbsp;主权利要求（CL）、关键词（TX）<br/>     &nbsp;&nbsp;申请人（PA）、分类号（IC）<br/>     &nbsp;&nbsp;申请号（AN）、申请日（AD）<br/>     &nbsp;&nbsp;公开号（PN）、公开日（PD）<br/>     &nbsp;&nbsp;公告号（GN）、公告日（GD）<br/>     &nbsp;&nbsp;优先权号（PR）、发明人（IN）<br/>     &nbsp;&nbsp;范畴分类（CT）、申请人地址（DZ）<br/>     &nbsp;&nbsp;国省代码（CO）、代理机构（AG）<br/>     &nbsp;&nbsp;代理人（AT）、主分类号（MC）<br/>     &nbsp;&nbsp;权利要求（CS）、说明书（DS）" />
           
            </td>
        </tr>
        <tr>
            <td>
                <!--onkeyup="findColors();" -->
                <div id="sheng" style="display: none">
                    <select id="ddlSheng" runat="server">
                    </select></div>
                <div id="GuoJia" style="display: none">
                    <select id="ddlGuoJia" runat="server">
                    </select></div>
                <div id="ShiJie" style="display: none">
                    <select id="ddlShiJie" runat="server">
                    </select></div>
                 <div id="divkeyword">
                    <input type="text" id="txtKeyWord" onkeyup="findColors();" />
                </div>                          
                <div id="divzhuanti">
                    <input id="cc" method='get' value="请选择专题库" style="width:250px" />
                </div>
                <div id="popup">
                    <ul id="colors_ul">
                    </ul>
                </div>
            </td>
            <td>
                <input type="button" value="添加" onclick="additem();" />
            </td>
        </tr>
        <tr>
            <td>
                 <dt><span id="dgti3"></span></dt>            
            <dd>                                
                <textarea id="txtkeys" rows="3"></textarea>
            </dd>
                <div id="dgti" style="display:none">                
                <dt><span id="dgti4"></span>
                    <div id="shichang" style="display: none">
                    <select id="ddlshichang" runat="server">                        
                    </select></div>
                    <div id="ShiJie1" style="display: none">
                    <select id="ddlShiJie1" runat="server">
                    </select></div>
                </dt> 
                <input type="text" id="txtKeyWord1" onkeyup="findTopColors();" />
                <input type="button" value="添加" onclick="addtopitem();" />
                <dt><span id="dgti5"></span></dt>           
                <dd>
                    <textarea id="txtkeys1" rows="3"></textarea>
                </dd>
            </div>
            </td>
        </tr>
    </table>

           
            <dt><span>备注:</span></dt>
            <dd>
                <textarea id="txtare" rows="3"></textarea></dd>
            <dt><span>预警周期:</span>
                <select id="ddlyjdate" onchange="yjdate()">
                    <option value="1">每月</option>
                    <option value="12">每年</option>
                    <%--<option value="4">四周</option>--%>
                </select>
                </dt>
                <dt>
                <span>预警状态:</span>
                <select id="ddlyjStatus" >
                    <option value="1">启动</option>
                    <option value="2">停止</option>
                    <%--<option value="4">四周</option>--%>
                </select></dt>
            <dt><span>下次预警时间：</span><span id="yjdate"></span></dt>
        </dl>
    </div>
</asp:Content>
