<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Index.Master" AutoEventWireup="true"
    CodeBehind="frmThList.aspx.cs" Inherits="Patentquery.zt.frmThList" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../jquery-easyui-1.8.0/themes/icon.css" />
    <script type="text/javascript" src="../jquery-easyui-1.8.0/jquery.easyui.min.js"></script>
    <script src="ztdblist.js" type="text/javascript"></script>    
    <link type="text/css" rel="stylesheet" href="../js/file-uploader/fileuploader.css" />
    <script type="text/javascript" language="javascript" src="../js/file-uploader/fileuploader.min.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="ztdblist" class="mid_ztlist">
        <div class="addone">
            <span id="spaCreateZtDb" runat="server"><a href="javascript:void(0);" onclick="AppendZtDb('新建专题库',null);">添加专题库</a></span> <a id="achange"
                href="javascript:void(0);" onclick="changelist();">列表式</a>
        </div>
        <ul id="ztitems">
            <li class="ztitem">
              
            </li>
        </ul>
        <div id="pagetop" class="easyui-pagination" style="width: 920px; float: left; padding-left: 10px;"
            data-options="total:0,showRefresh: false,displayMsg: '当前显示从 [{from}] 到 [{to}] 共[{total}]条记录'">
        </div>
        <div class="clear">
        </div>
    </div>
    <!-- 添加节点开始 -->
    <div id="divAddZT" style="width:530px; height:280px; display:none;">
        <table>
            <tr>
                <th style="text-align: right;">
                    封面：
                </th>
                <td>
                    <div id="crop_preview" style="width: 130px; height: 90px; overflow: hidden;">
                    </div>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                </th>
                <td>
                    <div id="upload_avatar">
                    </div>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    名称：
                </th>
                <td>
                    <input type="text" id="newclass" name="newclass" onblur="Check_ZtNm()" style="width: 450px" />
                </td>
            </tr>
            <tr>
                <th>
                </th>
                <td>
                    <span id="showmsg" style="display: none; color: Red;">*请输入名称</span>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    描述：
                </th>
                <td>
                    <textarea id="txtNodeDes" cols="200" name="txtNodeDes" rows="40" style="width: 450px;
                        height: 100px"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript" language="javascript">
        var g_oJCrop = null;
        $(function () {
            new qq.FileUploader({
                element: document.getElementById('upload_avatar'),
                action: "../zt/ZtFileUpLoad.ashx",
                multiple: false,
                disableDefaultDropzone: true,
                allowedExtensions: ['jpg', 'jpeg', 'png', 'gif'],
                uploadButtonText: '选择专题库封面...',
                onComplete: function (id, fileName, json) {
                    if (json.success) {
                        if (g_oJCrop != null) g_oJCrop.destroy();
                        //$("#crop_tmp_avatar").val(json.tmp_avatar);
                        //$("#crop_container").show();
                        $("#crop_preview").html('<img style="width: 130px; height: 90px;" src="../ZtHeadImg/' + json.tmp_avatar + '">');
                        $("#crop_preview").attr("title", json.tmp_avatar);
                    }
                    else {
                        alert(json.description);
                    }
                }
            });
        });

        function HeardImgOnerror(obj) {
            $(obj).attr("onerror", ""); //this.src='../ZtHeadImg/neiPro.gif'
            $(obj).attr("src", "../ZtHeadImg/neiPro.gif");
        }
      
    </script>
     <asp:HiddenField id="delzt"  runat="server" ClientIDMode="Static" value= "0" />
     <asp:HiddenField id="edzt"  runat="server" ClientIDMode="Static" value= "0" />
</asp:Content>
