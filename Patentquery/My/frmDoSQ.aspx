<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmDoSQ.aspx.cs" Inherits="Patentquery.My.frmDoSQ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.8.0/jquery.query-2.1.7.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Js/Common.js"></script>
    <script type="text/javascript" src="../js/syscfg.js"></script>    
    <script type="text/javascript" src="/Js/sysdialog.js"></script>
    <script src="../Js/artDialog/artDialog.min.js"></script>
    <script src="../Js/artDialog/artDialog.plugins.min.js"></script>
    <link  href="../Js/artDialog/skins/blue.css" rel="stylesheet" />
    <script src="../Js/AjaxDoPatSearch.js" type="text/javascript"></script>
    <script type="text/ecmascript" language="javascript">
        $(document).ready(function () {
            //
            if ($("#hidSearchTxt").val() != "" && $.query.get('db') != "") {
                //$.query.get('Query')
                DoPatSearch("", $("#hidSearchTxt").val(), $.query.get('db'), "-1", "");
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="hidSearchTxt" runat="server" />
    </div>
    </form>
</body>
</html>
