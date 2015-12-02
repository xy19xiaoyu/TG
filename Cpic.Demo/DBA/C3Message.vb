
Public Class C3Message
    '将Web窗体在新的窗口中打开
    'WebPage是当前的窗口
    'StrUrl是在新窗口中显示的页面
    '确省窗口大小是1000*1000PX
    Public Sub WindowsNew(ByVal WebPage As System.Web.UI.Page, ByVal strURL As String, Optional ByVal WindowName As String = "")

        Dim ScriptStr As String
        '产生一个新窗口的Script函数
        ScriptStr = String.Empty
        ScriptStr += "<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">"
        ScriptStr += "<Script LANGUAGE='JavaScript'>" & vbCrLf
        ScriptStr += "function StartUp() {" & vbCrLf
        ScriptStr += "var me ;"
        ScriptStr += "me = window ;"
        ScriptStr += "msg=window.open('"
        ScriptStr += strURL
        ScriptStr += "','" + WindowName + "','toolbar =no, menubar=no, scrollbars=yes, resizable=yes, left=0,top=0,location=no, status=no,width=1280,height=1000' );" ''
        ScriptStr += "};"
        ScriptStr += "StartUp();"
        ScriptStr += "</SCRIPT>"
        '将此脚本发送出去
        WebPage.Response.Write(ScriptStr)
    End Sub
    '显示一个告警信息
    'MSG 是需要显示的信息
    'WebPage是当前的工作窗体
    Public Sub AlertMessage(ByVal WebPage As System.Web.UI.Page, ByVal Msg As String)
        Dim ScriptStr As String
        '产生一个警告信息的Script函数
        ScriptStr = String.Empty
        ScriptStr += "<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">"
        ScriptStr += "<script type='text/javascript'>" + vbCrLf
        ScriptStr += "function StartUp() {" & vbCrLf
        ScriptStr += "alert('"
        ScriptStr += Msg.Replace("'", "\'").Replace(vbCrLf, "\n")
        ScriptStr += "');"
        ScriptStr += "};"
        ScriptStr += "</script>"
        ScriptStr += "<body onload='StartUp();'>"
        ScriptStr += "</body>"
        '将此脚本发送出去
        WebPage.Response.Write(ScriptStr)
    End Sub
End Class
