<%@ Page Title="分类导航" Language="C#" MasterPageFile="~/Master/indexnoform.master" AutoEventWireup="true"
    CodeBehind="frmIPCSearch.aspx.cs" Inherits="Patentquery.My.frmIPCSearch" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery.easyui.min.js"></script>
    <script src="../jquery-easyui-1.8.0/datagrid-detailview.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/ipcSearch.js"></script>
    <script src="../Js/AjaxDoPatSearch.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- IPC 检索-->
    <div id="left">
        <div id="pinlieft">
            <div id="left_title" class="left_ti">
                &nbsp;<span>分类导航</span>&nbsp;
            </div>
            <div id="ipcleft" class="left_content" style="height: 518px; overflow: hidden;">
                <div id="ipctypelist" class="easyui-accordion" data-options="fit:true" style="width: 220px;">
                    <div id="IPC" title="IPC分类" style="padding: 10px; overflow: hidden;">
                        <ul id="listIPC">
                            <li><a href="javascript:void(0);" onclick="showipc('A')" title="人类生活必需">A&nbsp;人类生活必需</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('B')" title="作业；运输">B&nbsp;作业；运输</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('C')" title="化学；冶金">C&nbsp;化学；冶金</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('D')" title="纺织；造纸">D&nbsp;纺织；造纸</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('E')" title="固定建筑物">E&nbsp;固定建筑物</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('F')" title="机械工程；照明；加热；武器；爆破">F&nbsp;机械工程；照明；加热；武器；爆破</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('G')" title="物理">G&nbsp;物理</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('H')" title="电学">H&nbsp;电学</a></li>
                        </ul>
                    </div>
                    <div id="ADM" title="外观设计分类" style="padding: 10px; min-height: 250px;">
                        <ul id="listADM" style="display: block;">
                            <li><a href="javascript:void(0);" onclick="showipc('01')" title="食品">01&nbsp;食品</a></li><li>
                                <a href="javascript:void(0);" onclick="showipc('02')" title="服装和服饰用品">02&nbsp;服装和服饰用品</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('03')" title="其它类未列入的旅行用品，箱子，阳伞和个人用品">
                                03&nbsp;其它类未列入的旅行用品，箱子，阳伞和个人用品</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('04')" title="刷子类">04&nbsp;刷子类</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('05')" title="纺织品，人造或天然材料片材类">05&nbsp;纺织品，人造或天然材料片材类</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('06')" title="家具">06&nbsp;家具</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('07')" title="其他类未列入的家用物品">07&nbsp;其他类未列入的家用物品</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('08')" title="工具和金属器具">08&nbsp;工具和金属器具</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('09')" title="用于商品运输或装卸的包装和容器">09&nbsp;用于商品运输或装卸的包装和容器</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('10')" title="钟，表和其它计量仪器，检查和信号仪器">
                                10&nbsp;钟，表和其它计量仪器，检查和信号仪器</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('11')" title="装饰品">11&nbsp;装饰品</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('12')" title="运输或提升工具">12&nbsp;运输或提升工具</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('13')" title="发电，配电和输电的设备">13&nbsp;发电，配电和输电的设备</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('14')" title="录音，通讯或信息再现设备">14&nbsp;录音，通讯或信息再现设备</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('15')" title="其它类未列入的机械">15&nbsp;其它类未列入的机械</a></li><li>
                                <a href="javascript:void(0);" onclick="showipc('16')" title="照相，电影摄影和光学仪器">16&nbsp;照相，电影摄影和光学仪器</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('17')" title="乐器">17&nbsp;乐器</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('18')" title="印刷和办公机械">18&nbsp;印刷和办公机械</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('19')" title="文具用品，办公设备，艺术家用品及教学材料">
                                19&nbsp;文具用品，办公设备，艺术家用品及教学材料</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('20')" title="销售和广告设备，标志">20&nbsp;销售和广告设备，标志</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('21')" title="游戏，玩具，帐篷和体育用品">21&nbsp;游戏，玩具，帐篷和体育用品</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('22')" title="武器，烟火，涉猎用品，捕杀有害动物的器具">
                                22&nbsp;武器，烟火，涉猎用品，捕杀有害动物的器具</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('23')" title="液体分配设备，卫生，供暖，通风和空调设备，固体燃料">
                                23&nbsp;液体分配设备，卫生，供暖，通风和空调设备，固体燃料</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('24')" title="医疗和实验室设备">24&nbsp;医疗和实验室设备</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('25')" title="建筑构件和施工元件">25&nbsp;建筑构件和施工元件</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('26')" title="照明设备">26&nbsp;照明设备</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('27')" title="烟草和吸烟用具">27&nbsp;烟草和吸烟用具</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('28')" title="药品，化妆品，梳妆用品和器具">28&nbsp;药品，化妆品，梳妆用品和器具</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('29')" title="防火灾，防事故救援装置和设备">29&nbsp;防火灾，防事故救援装置和设备</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('30')" title="动物的管理与驯养设备">30&nbsp;动物的管理与驯养设备</a></li><li>
                                <a href="javascript:void(0);" onclick="showipc('31')" title="其他类未列入的食品或饮料制作机械和设备">31&nbsp;其他类未列入的食品或饮料制作机械和设备</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc('99')" title="其它杂项">99&nbsp;其它杂项</a></li>
                        </ul>
                    </div>
                    <div id="ARE" title="国民经济分类" style="padding: 10px; min-height: 250px;">
                        <ul id="listARE">
                            <li><a href="javascript:void(0);" onclick="showipc(&quot;A&quot;)" title="农、林、牧、渔业">
                                A&nbsp;农、林、牧、渔业</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc(&quot;B&quot;)" title="采矿业">B&nbsp;采矿业</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc(&quot;C&quot;)" title="制造业">C&nbsp;制造业</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc(&quot;D&quot;)" title="电力、燃气及水的生产和供应业">
                                D&nbsp;电力、燃气及水的生产和供应业</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc(&quot;E&quot;)" title="建筑业">E&nbsp;建筑业</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc(&quot;F&quot;)" title="交通运输、仓储和邮政业">
                                F&nbsp;交通运输、仓储和邮政业</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc(&quot;G&quot;)" title="信息传输、计算机服务和软件业">
                                G&nbsp;信息传输、计算机服务和软件业</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc(&quot;H&quot;)" title="批发和零售业">H&nbsp;批发和零售业</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc(&quot;L&quot;)" title="租赁和商务服务业">
                                L&nbsp;租赁和商务服务业</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc(&quot;M&quot;)" title="科学研究、技术服务和地质勘查业">
                                M&nbsp;科学研究、技术服务和地质勘查业</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc(&quot;N&quot;)" title="水利、环境和公共设施管理业">
                                N&nbsp;水利、环境和公共设施管理业</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc(&quot;O&quot;)" title="居民服务和其他服务业">
                                O&nbsp;居民服务和其他服务业</a></li>
                            <li><a href="javascript:void(0);" onclick="showipc(&quot;R&quot;)" title="文化、体育和娱乐业">
                                R&nbsp;文化、体育和娱乐业</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="right" class="right" style="min-height: 555px">
        <div id="right_top" class="right_top">
            <input type="radio" id="rdkey" name="stype" value="key" checked="checked" /><label
                for="rdkey">按关键字查找</label>
            <input type="radio" id="rdipc" name="stype" value="ipc" /><label for="rdipc">按分类号查找</label>
            <input type="text" id="IPCInput" onkeydown="ipckeydown()" name="IPCInput" style="width: 300px; height: 22px;"/>
            <span class="button" onclick="ipcSearch()">查询</span>
        </div>
        <div id="ipc_right_content" style="display: none;">
            <dl class="ipc_title">
                <dt class="sp_title">分类</dt>
                <dd class="sp_desc">
                    类描述</dd>
            </dl>
            <ul id="ipc_result">
            </ul>
        </div>
        <div id="help" style="display: block; padding: 50px 0px 0px 50px;">
            <ul>
                <li>1.请选择从左侧选择分类</li><li>2.输入要搜索的关键字，或者分类号进行搜索。</li></ul>
        </div>
        <div id="nodata" style="display: none; padding: 50px 0px 0px 50px;">
            <ul>
                <li>没有找到符合条件的分类</li><li>1.请选择从左侧选择分类</li><li>2.输入要搜索的关键字，或者分类号进行搜索。</li></ul>
        </div>
    </div>
</asp:Content>
