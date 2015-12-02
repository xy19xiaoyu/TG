//编辑检索时
function UpNode_BindData() {
    var nodes = zTree.getSelectedNodes();
    if (nodes.length == 0) {
        alert("请先选择一个节点");
        return;
    }

    var node = nodes[0];

    demoIframe.attr("src", "frmUpdateNodSp.aspx?NId=" + node.id + "&NTW=" + node.name);

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
    ///--ztree
    $.ajaxSetup({ cache: false }); //这个是为了树的准确性做的一个缓存区清理的工作
    $.fn.zTree.init($("#treecn"), setting);
    zTree = $.fn.zTree.getZTreeObj("treecn");
    rMenu = $("#mm");
});

///////////////////////////////------------------------ztree function-------------------------
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
    rMenu.show();
    //$("#rMenu ul").show();
    if (type == "root") {
        $("#m_Edit").hide();
        $("#m_del").hide();
        $("#m_delData").hide();
        $($("#m_Add").children()[0]).text("添加专题库");
    } else {
        $("#m_Edit").show();
        $("#m_del").show();
        $("#m_delData").show();
        $($("#m_Add").children()[0]).text("添加");
    }
    rMenu.css({ "top": y + "px", "left": x + "px", "visibility": "visible" });

    //$("body").bind("mousedown", onBodyMouseDown);
}
function hideRMenu() {
    if (rMenu) rMenu.css({ "visibility": "hidden" });
    $("body").unbind("mousedown", onBodyMouseDown);
}
function onBodyMouseDown(event) {
    if (!(event.target.id == "rMenu" || $(event.target).parents("#rMenu").length > 0)) {
        rMenu.css({ "visibility": "hidden" });
    }
}

//添加节点
function addNode(treeName, type) {
    var node = null;
    var pid = 0;

    if ($($("#m_Add").children()[0]).text() == "添加") {
        var nodes = zTree.getSelectedNodes();
        if (nodes.length == 0) {
            alert("请先选择一个节点");
            return;
        }
        var node = nodes[0];
        pid = node.id;
    }
    //异步加载 添加节点 返回节点的ID，Name
    if ($("#newclass").val() != "") {
        var classname = $("#newclass").val();

        $.ajax({
            type: "POST",
            url: "../comm/editNodes.aspx/addNode",
            data: "{'parent':'" + pid + "','name':'" + classname + "','clsType':'" + type + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(msg) {
                var res = msg.d;
                var obj = eval('(' + res + ')');
                if (obj.mess != "failed") {
                    zTree.addNodes(node, { id: obj.mess, pId: pid, name: classname });
                    //ShowSPTable(obj.mess, classname);
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
    var nodes = zTree.getSelectedNodes();
    if (nodes.length == 0) {
        alert("请先选择一个节点");
        return;
    }
    var node = nodes[0];

    $.ajax({
        type: "POST",
        url: "../comm/editNodes.aspx/deleteNode",
        data: "{'id':'" + node.id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg) {
            if (msg.d != "failed") {
                zTree.removeNode(node);
            } else {
                alert("删除失败！");
            }
        }
    });

}
//Tree 点添加按钮
function AppendNode() {
    easyDialog.open({
        container: 'AddNode'
    });
}
//编辑检索时
function EditNode() {
    var node = $('#treecn').tree('getSelected');
    $('#cntabllist').hide();
    $('#AddNodeSP').show();
    ShowSPTable(node.id, node.text);
}