var ztname = "";
var dd;
var strSort = "STAR";
var strSortText;
var ddsort;
var ddsort1;
function Loadzttype() {
    $('#showlist').hide();
    $('#divnodata').hide();
    $('#showspdes').hide();
    $('#divshowhelp').show();
    var ztid = requestUrl('id');
    var type = requestUrl('type');
    if (type == "") type = "QY";
    if (type.toUpperCase() == "ZT") {
        $('#hidthid').val(ztid);
        $('#pagetitle').html("专题数据库");
        //todo: 得到专题库名称 
        //修改节点
        $.ajax({
            type: "POST",
            url: "/comm/editNodes.aspx/getztName",
            data: "{'id':'" + ztid + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                
                var res = msg.d;
                if (res != "failed") {
                    ztname = res;
                    $('#dd .dropdown').prepend("<li val='zt'><a href='#'>全库</a></li>");
                    dd = new DropDown($('#dd'), ztname, Showtree);
                    $('.dropdown-zt').removeClass('active');   
                    Showtree();
                } else {
                    showMessage("获取专题库名称失败!");
                }
            }
        });
    }
    else {
        
        ztname = "企业在线数据库";
        $('#pagetitle').html(ztname);
        $('#dd .dropdown').prepend("<li val='qy'><a href='#'>全库</a></li>");
        dd = new DropDown($('#dd'), ztname);
        dd.itemClick = Showtree();
        $('.dropdown-zt').removeClass('active');       
        Showtree();

    }
    ddsort = new DropDown($('#sort'), null, Orderby);
    ddsort.setText("核心专利↓");
    ddsort1 = new DropDown($('#sort1'), null, Orderby);
    ddsort1.setText("核心专利↓");
    regSelectPage();
    iniRight();

}


//根据不同的类型展示不同的专题树  
//专题库，企业在线数据库，还有竞争对手，以及专题库更新
function Showtree() {

    var ztid = $('#hidthid').val();
    var type = dd.getValue();
    var strurl = "../comm/getNodes.aspx?clstype=" + type + "&ztid=" + ztid;

    $('#divzt').hide();
    $('#divca').hide();
    $('#divup').hide();
    $('#divqy').hide();
    $('#div' + type).show();
    //showMessage(type);
    switch (type) {
        case "zt":
            $('#zt').tree({
                url: strurl,
                onExpand: function () {
                    addhover('zt');
                }, onLoadSuccess: function (node, data) {
                    addhover('zt');
                }, onContextMenu: function (e, node) {

                    e.preventDefault();
                    $(this).tree('select', node.target);
                    if (node.text != "未标引") {
                        $('#mm').menu('show', { left: e.pageX, top: e.pageY });
                    }
                }
            });
            $('#divzt').bind('contextmenu', function (e) {
                e.preventDefault();
                $('#addzt').menu('show', { left: e.pageX, top: e.pageY });
            });
            break;
        case "ca":
            $('#ca').tree({
                url: strurl,
                onExpand: function () {

                    addhover('ca');
                },
                onLoadSuccess: function (node, data) {

                    addhover('ca');
                }, onContextMenu: function (e, node) {
                    e.preventDefault();
                    $(this).tree('select', node.target);
                    if (node.text != "未标引") {
                        $('#mmca').menu('show', { left: e.pageX, top: e.pageY });
                    }
                }
            });
            $('#divca').bind('contextmenu', function (e) {
                e.preventDefault();
                $('#AddCA').menu('show', { left: e.pageX, top: e.pageY });
            });
            break;
        case "qy":

            $('#qy').tree({
                url: strurl,
                onExpand: function () {
                    addhover('qy');
                }, onLoadSuccess: function (node, data) {
                    addhover('qy');
                }, onContextMenu: function (e, node) {

                    e.preventDefault();
                    $(this).tree('select', node.target);
                    if (node.text != "未标引") {
                        $('#mmqy').menu('show', { left: e.pageX, top: e.pageY });
                    }
                }
            });
            $('#divqy').bind('contextmenu', function (e) {
                e.preventDefault();
                $('#addqy').menu('show', { left: e.pageX, top: e.pageY });
            });
            break;
        case "up":

            type = requestUrl('type');
            if (type == "") type = "qy";
            strurl = "../comm/getNodes.aspx?clstype=" + type + "&ztid=" + ztid;
            $('#up').tree({
                url: strurl,
                onExpand: function () {
                    addhover('up');
                }, onLoadSuccess: function (node, data) {
                    addhover('up');
                }
            });
            break;
    }
    $("#spdes").html("<p>如果您还没有<b>专利专题库</b>或<b>企业在线数据库</b>或<b>竞争对手</b>，请在左侧方块中<b>点击右键</b>进行添加</p>");
}



//添加节点
function addNode() {

    var treeName = $("#tname").val();
    //异步加载 添加节点 返回节点的ID，Name
    if ($("#newclass").val() != "") {
        var title = $('#dialogtitle').html();
        var classname = $("#newclass").val();
        var desc = $("#txtNodeDes").val();
        var t = $('#' + treeName);
        var node = t.tree('getSelected');
        var li = 0;
        debugger;
        var nodeid = "";
        var thid = $("#hidthid").val();
        if (node != null) {            
            if (node.attributes) {
                li = parseInt(node.attributes.live);
                li += 1;
                nodeid = node.attributes.Nid;
                //showMessage(li);
            }
        }
        var type = treeName;
        var myDialog = showProcess();
        switch (title) {
            case "添加分类":
                $.ajax({
                    type: "POST",
                    url: "/comm/editNodes.aspx/addNode",
                    data: "{'thid':'" + thid + "','parent':'" + nodeid + "','name':'" + HTMLEncode(classname) + "','clsType':'" + type + "','des':'" + HTMLEncode(desc) + "','live':'" + li + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var res = msg.d;
                        var obj = eval('(' + res + ')');
                        if (obj.mess == "exists") {
                            showMessage("已存在同样名称的节点!");
                            $('#newclass').focus();
                            return;
                        }
                        else if (obj.id != "failed") {
                            var t = $('#' + treeName);
                            var node = t.tree('getSelected');
                            t.tree('append', {
                                    parent: (node ? node.target : null),
                                    data: [{ id: obj.id, text: classname, attributes: { live: li, des: desc, Nid: obj.Nid}}]
                            });                           
                            t.tree('select', t.tree('find', obj.id).target);
                            art.dialog.get('DGDivAddNode').hidden();
                        } else {
                            showMessage("添加失败");
                        }
                        myDialog.close();

                    }
                });
                break;
            case "添加主分类":
                nodeid = '';
                li = '0';
                $.ajax({
                    type: "POST",
                    url: "/comm/editNodes.aspx/addNode",
                    data: "{'thid':'" + thid + "','parent':'" + nodeid + "','name':'" + HTMLEncode(classname) + "','clsType':'" + type + "','des':'" + HTMLEncode(desc) + "','live':'" + li + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        myDialog.close();
                        var res = msg.d;
                        var obj = eval('(' + res + ')');
                        if (obj.id == "exists") {
                            showMessage("已存在同样名称的节点!");
                            $('#newclass').focus();
                            return;
                        }
                        else if (obj.id != "failed") {
                            debugger;
                            var t = $('#' + treeName);
                            var node = t.tree('getSelected');
                            t.tree('append', {
                                parent: null,
                                data: [{ id: obj.id, text: classname, attributes: { live: li, des: desc, Nid: obj.Nid}}]
                            });
                            t.tree('select', t.tree('find', obj.id).target);
                            art.dialog.get('DGDivAddNode').hidden();
                        } else {
                            showMessage("添加失败");
                        }

                    }
                });
                break;
            default:
                //修改节点
                $.ajax({
                    type: "POST",
                    url: "/comm/editNodes.aspx/Rename",
                    data: "{'id':'" + nodeid + "','name':'" + HTMLEncode(classname) + "','des':'" + HTMLEncode(desc) + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        myDialog.close();
                        var res = msg.d;
                        if (res == "exists") {
                            showMessage("已存在同样名称的节点!");
                            $('#newclass').focus();
                            return;
                        }
                        else if (res != "failed") {
                            var t = $('#' + treeName);
                            var node = t.tree('getSelected');
                            if (node) {
                                node.text = classname;
                                node.attributes.des = desc;
                                t.tree("update", node);
                            }
                            art.dialog.get('DGDivAddNode').hidden();
                        } else {
                            showMessage("修改失败");
                        }
                    }
                });
                break;
        }
        return false;
    }
    else {
        $("#showmsg").show();
    }
}
//删除节点
function RemoveNode(treename) {
    var node = $('#' + treename).tree('getSelected');

    art.dialog({
        title: '确认删除',
        content: '<img src="../js/artDialog/skins/icons/warning.png"/>您确定删除所选节点:<span style="color:red">' + node.text + '</span>吗？',
        button: [
          {
              value: '取消',
              callback: function () { }

          },
            {
                value: '确定',
                callback: function () {

                    $.ajax({
                        type: "POST",
                        url: "../comm/editNodes.aspx/deleteNode",
                        data: "{'id':'" +  node.attributes.Nid + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d != "failed") {
                                $('#' + treename).tree('remove', node.target);

                            } else {
                                showMessage("删除失败！");
                            }
                        }
                    });
                },
                focus: true
            }
            ]
    });

}
//Tree 点添加按钮
function append(treename, title) {

    switch (title) {
        case "添加主分类":
        case "添加竞争对手":
            break;
        default:
            var t = $('#' + treename);
            var node = t.tree('getSelected');
            if (node) {
                if (node.attributes) {
                    var li = parseInt(node.attributes.live);
                    if (treename == "ca") {
                        if (li == SYS_TREE_CA_MAXLIVE) {
                            showMessage(SYS_TREE_MAXLIVE_DIALOG_TI, SYS_TREE_CA_MAXLIVE_DIALOG_MSG);
                            return;
                        }
                    }
                    else {
                        if (li == SYS_TREE_MAXLIVE) {
                            showMessage(SYS_TREE_MAXLIVE_DIALOG_TI, SYS_TREE_MAXLIVE_DIALOG_MSG);
                            return;
                        }
                    }
                }
            }
            break;
    }

    $('#tname').val(treename);
    $("#newclass").val('');
    $("#txtNodeDes").val('');
    $('#dialogtitle').html(title);
    $("#showmsg").hide();
    var addnodedlg = art.dialog.get('DGDivAddNode');

    if (addnodedlg == null) {
        addnodedlg = art.dialog({
            title: title,
            content: document.getElementById("DivAddNode"),
            id: 'DGDivAddNode',
            lock: true,
            border: false,
            padding: 0,
            okValue: '确定',
            ok: function () {
                addNode();
                return false;
            }
        });
    }
    addnodedlg.visible();
}
//编辑节点时
function edit(treename, title) {

    $('#tname').val(treename);
    $('#dialogtitle').html(title);
    $("#txtNodeDes").val('');
    var node = $('#' + treename).tree('getSelected');
    $("#newclass").val(node.text);
    $("#txtNodeDes").val(node.attributes.des);
    $("#showmsg").hide();
    var addnodedlg = art.dialog.get('DGDivAddNode');

    if (addnodedlg == null) {
        addnodedlg = art.dialog({
            title: title,
            content: document.getElementById("DivAddNode"),
            id: 'DGDivAddNode',
            lock: true,
            border: false,
            padding: 0,
            okValue: '确定',
            ok: function () {
                addNode();
                return false;
            }
        });
    }
    addnodedlg.visible();

}
//显示节点检索式
function showtbsp(treename) {
    var node = $('#' + treename).tree('getSelected');
    $("#showlist").hide();
    $("#showsp").show();
    $('#divnodata').hide();
    $('#divshowhelp').hide();
    $("#showspdes").hide();
    $("#hidNodeId").val(node.attributes.Nid);
    $("#hidNodeName").val(node.text)
    ShowSPTable();
}


//鼠标悬浮是添加中文+英文，离开移除
function addhover(treename) {
    var $divary = $("#" + treename).find("div.tree-node");
    //showMessage($divary.length);
    $divary.each(function (index) {

        var html = $divary.eq(index).html();
        var nodeid = $divary.eq(index).attr("node-id");
        var node = $("#" + treename).tree('find', nodeid);
        if (node) {
            if (node.attributes) {
                var li = parseInt(node.attributes.live);
                //if (li != 0) {


                //添加的元素
                addstr = '<span id="showlist' + treename + nodeid + '" style="display:none"><img id="showcn' + treename + nodeid + '" src="../Images/cn1.png" border="0" />&nbsp;<img id="showdocdb' + treename + nodeid + '" src="../Images/en1.png" border="0" /></span><span class="tree-title"';
                if ($("#showlist" + nodeid).length > 0) {

                }
                else {
                    html = html.replace('<span class="tree-title"', addstr);
                    html = html.replace('<SPAN class=tree-title', addstr);
                    //添加
                    $divary.eq(index).html(html);
                    //注册onclick事件
                    $("#showcn" + treename + nodeid).unbind();
                    $("#showcn" + treename + nodeid).click(function () {                        
                        show('cn', node.attributes.Nid);
                    });
                    //注册onclick事件
                    $("#showdocdb" + treename + nodeid).unbind();
                    $("#showdocdb" + treename + nodeid).click(function () {                       
                        show('en', node.attributes.Nid);
                    });

                }

                $divary.eq(index).unbind();
                //鼠标移上
                $divary.eq(index).mouseenter(function () {
                    var nodeid = $divary.eq(index).attr("node-id");
                    $("#showlist" + treename + nodeid).show();
                });
                //鼠标移走
                $divary.eq(index).mouseleave(function () {
                    var nodeid = $divary.eq(index).attr("node-id");
                    $("#showlist" + treename + nodeid).hide();
                });
            }

            //}
        }

    });
    //为原来的Nodename添加click事件
    var $titleary = $("#" + treename).find("span.tree-title");
    $titleary.each(function (index) {
        $titleary.eq(index).click(function () {
            var nodeid = $titleary.eq(index).parent().attr("node-id");
            var node = $("#" + treename).tree('find', nodeid);
            if (node) {

                if (node.attributes) {
                    $("#showlist").hide();
                    $("#showsp").hide();
                    $('#divnodata').hide();
                    $('#divshowhelp').hide();
                    $("#showspdes").show();
                    var des = node.attributes.des;
                    if (des == "") {
                        des = "<b>暂无简介...</b>";
                    }
                    else {
                        des = "<b>简介</b>：</br>" + des
                    }
                    $("#spdes").html(des);
                }
            }
        });
    });
}

function show(type, nodeid) {
    if ($('#hidtype').val() != type && $('#hidNodeId').val() != nodeid) {
        $('#hidCpicids').val(',');
    }
    $('#hidNodeId').val(nodeid);
    $('#hidCpicids').val(',');
    $('#hidtype').val(type);
    $("#showlist").show();
    $("#showsp").hide();
    $("#showspdes").hide();
    ShowTable(type, nodeid, "db", 1, 10, 10, "1", strSort);
}
