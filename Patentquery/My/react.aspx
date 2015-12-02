<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="react.aspx.cs" Inherits="Patentquery.My.react" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Js/ceact/react.js" type="text/javascript"></script>
    <script src="../Js/ceact/react-dom.js" type="text/javascript"></script>
    <script src="../easyui/jquery.min.js" type="text/javascript"></script>
    <script src="../Js/ceact/browser.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="example">
        </div>
        <div id="json">
        </div>
        <script type="text/babel">
              var arr = [
                <h1>Hello world!</h1>,
                <h2>React is awesome</h2>,
              ];
              ReactDOM.render(
                <div>{arr}</div>,
                document.getElementById('example')
              );

      ReactDOM.render(
        <h3>hello json</h3>, 
        $("#json")[0]  
      );     
      //alert($("#json").first());
    </script>
    </div>
    </form>
</body>
</html>
