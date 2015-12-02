<%@ Page Language="C#" AutoEventWireup="true" Inherits="th.TH.spdes" Codebehind="spdes.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery-1.8.0.min.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            //
            //修改节点
            var nodeid = getUrlParam('NodeId');
            //修改节点
            $.ajax({
                type: "POST",
                url: "../comm/editNodes.aspx/getNodeDes",
                data: "{'nodid':'" + nodeid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {
                    //
                    var res = msg.d;
                    if (res != "failed") {
                        $("#spdes").html(res);
                    } else {
                        alert("获取分类描述失败！");
                    }
                }
            });
        });

        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        }        
        
         
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="spdes">
        </div>
    </div>
    </form>
</body>
</html>
