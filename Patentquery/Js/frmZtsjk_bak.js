//编辑检索时
function UpNode_BindData() {
    var node = $('#treecn').tree('getSelected');
    //    $('#cntabllist').hide();
    //    $('#AddNodeSP').show();
    //ShowSPTable(node.id, node.text);

    demoIframe.attr("src", "frmUpdateNodSp.aspx?NId=" + node.id + "&NTW=" + node.text);

}

///重置ifm高度
function loadReady() {
    var bodyH = demoIframe.contents().find("body").get(0).scrollHeight,
		htmlH = demoIframe.contents().find("html").get(0).scrollHeight,
		maxH = Math.max(bodyH, htmlH), minH = Math.min(bodyH, htmlH),
		h = demoIframe.height() >= maxH ? minH : maxH;
    if (h < 530) h = 530;
    demoIframe.height(h);
}



///------------
$(document).ready(function() {
    demoIframe = $("#testIframe");
    demoIframe.bind("load", loadReady);

    //    $.ajaxSetup({ cache: false }); //这个是为了树的准确性做的一个缓存区清理的工作
    //    $('#treecn').tree({//从这里开始初始化JSTree          
    //        type: "json", //支持如xml等多种类型，这里是获取JSON格式数据源
    //        url: "/comm/getNodes.aspx?clstype=cn",       //每次获得数据从这个链接  
    //        async: true,    //动态加载数据
    //        async_data: function(node) {   //请求数据时带的参数列表，可通过getParameter获得。
    //            return parent_Id ? $(node).attr("id") : -1;
    //        },
    //        loading: "loading…",  //在用户等待数据渲染的时候给出的提示内容，默认为loading  
    //        //在这个option中设置context来控制JSTree的右键操作，如果在context的visible函数内始终返回false则表示在任何节点的右键都无效。  
    //        visible: function(NODE, TREE_OBJ) {
    //            return false;
    //        },
    //        onContextMenu: function(e, node) {
    //            OnRightClick(e, node.id, node);
    //        },
    //        onClick: function(node) {
    //            //demoIframe.attr("src", "http://www.baidu.com");
    //            //ShowTable('cn', node.id, 'db', '0');
    //            addHoverDom(node.id, node);
    //        },
    //        onHoverOverNode: function(e, node) {
    //            alert('dfdf');
    //        }
    //    });

    ///--ztree
    $.fn.zTree.init($("#treecn"), setting);
    zTree = $.fn.zTree.getZTreeObj("treecn");
    rMenu = $("#mm");
});

///////////////////////////////------------------------ztree-------------------------
function OnRightClick(event, treeId, treeNode) {
    event.preventDefault();
    //$(this).tree('select', treeNode.target);
    //    $('#mm').menu('show', {
    //        left: event.pageX,
    //        top: event.pageY
    //    });

    if (!treeNode && event.target.tagName.toLowerCase() != "button" && $(event.target).parents("a").length == 0) {
        zTree.cancelSelectedNode();
        showRMenu("root", event.clientX, event.clientY);
    } else if (treeNode && !treeNode.noR) {
        zTree.selectNode(treeNode);
        showRMenu("node", event.clientX, event.clientY);
    }
}

function showRMenu(type, x, y) {
    $("#mm").show();
    //$("#rMenu ul").show();
    //    if (type == "root") {
    //        $("#m_del").hide();
    //        $("#m_check").hide();
    //        $("#m_unCheck").hide();
    //    } else {
    //        $("#m_del").show();
    //        $("#m_check").show();
    //        $("#m_unCheck").show();
    //    }
    rMenu.css({ "top": y + "px", "left": x + "px", "visibility": "visible" });

    $("body").bind("mousedown", onBodyMouseDown);
}
function hideRMenu() {
    if (rMenu) rMenu.css({ "visibility": "hidden" });
    $("body").unbind("mousedown", onBodyMouseDown);
}
function onBodyMouseDown(event) {
    if (!(event.target.id == "rMenu" || $(event.target).parents("#rMenu").length > 0)) {
        // rMenu.css({ "visibility": "hidden" });
    }
}

//添加节点
function addNode(treeName, type) {
    //异步加载 添加节点 返回节点的ID，Name
    if ($("#newclass").val() != "") {
        var classname = $("#newclass").val();
        var t = $('#' + treeName);
        var node = t.tree('getSelected');
        $.ajax({
            type: "POST",
            url: "../comm/editNodes.aspx/addNode",
            data: "{'parent':'" + node.id + "','name':'" + classname + "','clsType':'" + type + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(msg) {

                var res = msg.d;
                var obj = eval('(' + res + ')');
                if (obj.mess != "failed") {
                    var t = $('#' + treeName);
                    var node = t.tree('getSelected');
                    t.tree('append', {
                        parent: (node ? node.target : null),
                        data: [{ id: obj.mess, text: classname}]
                    });
                    t.tree('select', t.tree('find', obj.mess).target);
                    ShowSPTable(obj.mess, classname);
                } else {
                    alert("添加失败");
                }
                easyDialog.close();

            }
        });
        return false;
    }
    else {
        $("#showmsg").show();
    }
}
//删除节点
function RemoveNode(treename) {
    var node = $('#' + treename).tree('getSelected');
    $.ajax({
        type: "POST",
        url: "../comm/editNodes.aspx/deleteNode",
        data: "{'id':'" + node.id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg) {
            if (msg.d != "failed") {
                $('#' + treename).tree('remove', node.target);

            } else {
                alert("删除失败！");
            }
        }
    });

}
//Tree 点添加按钮
function append() {
    $('#cntabllist').hide();
    easyDialog.open({
        container: 'AddNode'
    });
}
//编辑检索时
function edit() {
    var node = $('#treecn').tree('getSelected');
    $('#cntabllist').hide();
    $('#AddNodeSP').show();
    ShowSPTable(node.id, node.text);
}