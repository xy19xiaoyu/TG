Imports Microsoft.VisualBasic
Imports System.Data
Imports Microsoft.Office.Interop.Owc11

#Region "ExcelDisplay"

'本方法类是一个公共类,主要用于数据集或数据表中的数据转换成Excel文件,
'直接将数据以Excel形式发送到客户端等
'主要包含如下函数(过程):
'         Public Sub TableToExcel(ByVal Dst As DataTable, ByVal XlsFileName As String)
'               -------- 将一个数据表保存成一个标准的Excel数据文件:
'结构编码设计:            quxg                 2006/06/08
'程序实现:                Tanhm                2006/06/16
'修改成VS2005:            quxg                 2006/06/19
'初步测试：               LiuY                 2006/08/14
'添加到公共库             quxg                 2006/09/21

Public Class ExcelDisplay

    '本过程是将一个数据表保存成一个标准的Excel数据文件 
    '输入:     Dst 包含数据的数据报表
    '           XlsFileName是转化成的Excel数据文件 
    '如果数据表中没有数据,什么也不做,如果已经存在了XlsFileName,数据文件将会被覆盖
    '错误返回: 有可能什么也没做,也有可能运行中错误,返回一个异常

    Private Sub TableToExcel(ByVal Dst As DataTable, ByVal XlsFileName As String)

        Dim OwcExcle As New Microsoft.Office.Interop.Owc11.Spreadsheet
        Dim RowTmp As DataRow
        Dim ColTmp As New DataColumn
        Dim RowCount As Integer
        Dim ColumnCount As Integer

        '排除数据表为空的情况
        If IsNothing(Dst) Then
            Exit Sub
        End If


        '将数据表中的所有列名称添加到Excel控件的第一列
        ColumnCount = 0
        For Each ColTmp In Dst.Columns
            ColumnCount += 1
            If Not IsDBNull(ColTmp.ColumnName) Then
                OwcExcle.ActiveSheet.Cells(2, ColumnCount) = ColTmp.ColumnName
            End If
        Next

        OwcExcle.ActiveSheet.Cells(1, 1) = Dst.TableName
        'OwcExcle.ActiveSheet.Range(1, ColumnCount).Merge()

        '将数据表中的所有数据行全部添加到新的Excel数据行中去
        ColumnCount = 0
        RowCount = 0
        '首先对数据中的每一列循环
        For Each ColTmp In Dst.Columns
            ColumnCount += 1
            '然后对所有数据行
            RowCount = 2 'Execel 中从第2行开始，有一行是表头
            For Each RowTmp In Dst.Rows
                RowCount += 1
                If Not IsDBNull(RowTmp(ColTmp)) Then '数据不为空，那就保存到本页中去
                    OwcExcle.ActiveSheet.Cells(RowCount, ColumnCount) = CStr(RowTmp(ColTmp))
                End If
            Next
        Next

        '设定相关的格式，显示会更美观些  
        ColumnCount = 0
        'For Each ColTmp In Dst.Columns
        '    ColumnCount += 1
        '    OwcExcle.ActiveSheet.Columns(ColumnCount).EntireColumn.AutoFitColumns()
        'Next
        OwcExcle.ActiveSheet.Rows(1).Font.Bold = True '第一行变黑些
        'OwcExcle.ActiveSheet.Rows(1).font.fontname = "黑体"

        '保存成指定的文件,如果出错则返回一个异常
        Try
            OwcExcle.Export(XlsFileName, Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, SheetExportFormat.ssExportHTML)
        Catch ex As Exception '保存的时候有错误
            Throw ex     '抛出异常
        End Try
    End Sub

    Public strXlsFile As String = ""

    '本过程是将一个数据表保存成一个标准的Excel数据文件后再将其返回到WEB客户端 
    '输入:     Dst 包含数据的数据报表
    '          pg  调用的WEB程序页面,主要是利用它来启动事件

    '如果数据表中没有数据,什么也不做,如果已经存在了XlsFileName,数据文件将会被覆盖
    '错误返回: 有可能什么也没做,也有可能运行中错误,返回一个异常
    '本过程将会用到Script函数打开一个新窗口
    Public Sub PushXls2Web(ByVal pg As Web.UI.Page, ByVal Dst As DataTable)
        Dim XlsTmpFileName, CurWorkPath As String
        Dim FullPathTmpFileName As String

        '获得当前的工作目录
        CurWorkPath = pg.Request.MapPath("")

        '清理此目录下的过期临时文件,此处定义为24小时以前的
        ClearTempXlsFile(CurWorkPath, 24)
        '获得一个新临时文件名称
        XlsTmpFileName = GetXlsTempFileName()

        '判断此文件是否已经存在了？
        Do
            '生成文件的真实路径
            FullPathTmpFileName = CurWorkPath + "\" + XlsTmpFileName
            '判断是否已经存在了？
            If System.IO.File.Exists(FullPathTmpFileName) Then
                '存在，继续产生临时文件
                XlsTmpFileName = GetXlsTempFileName()
            Else '不存在，保留此文件名称
                Exit Do
            End If
        Loop
        '然后将数据表转化成Excel临时文件
        Try
            TableToExcel(Dst, FullPathTmpFileName)
        Catch ex As Exception
            Throw ex
            Exit Sub '如果转换出错，就什么也不做了
        End Try

        ' Thread.Sleep(500) 'May be The TableToexec Not Well Done 

        '再将此文件发送到新的窗口中去
        Dim Msgcls As New C3Message
        'Msgcls.WindowsNew(pg, XlsTmpFileName)
        strXlsFile = XlsTmpFileName
    End Sub

    '产生一个临时文件名称，用以保证在指定的目录下文件名称不会冲突
    Private Function GetXlsTempFileName() As String
        Dim TempName As String
        Dim RandSum As Integer
        Dim CurTime As DateTime
        '根据当前时间产生一个文件名称
        CurTime = Now
        TempName = "tmp" + CurTime.Day.ToString + CurTime.Hour.ToString + CurTime.Minute.ToString + CurTime.Second.ToString
        '再增加3位随机数
        RandSum = Rnd() * 1000
        '生成一个长文件名称，以XLS后缀结尾
        TempName = TempName + RandSum.ToString + ".xls"
        Return TempName
    End Function
    '删除已经过期的文件，通常为24小时以前的
    'TempDir 是指定的目录
    'Hours 是超期的小时数目

    Private Sub ClearTempXlsFile(ByVal TempDir As String, ByVal Hours As Double)
        Dim Files(), FileName As String
        Dim FileDate As Date

        Try
            '读取本目录下所有的文件和目录
            Files = System.IO.Directory.GetFiles(TempDir)
            For Each FileName In Files
                '读取上次更新的时间
                FileDate = System.IO.File.GetLastWriteTime(FileName)
                '是否.Xls结尾，否则跳过

                If FileName.IndexOf(".xls") < 0 Then
                    Continue For
                End If

                'modified by Liuyan  20060628
                '判断是否是tmp文件,否则跳过
                If FileName.IndexOf("tmp") > 0 Then

                    '与当前时间比较是否超期了？
                    If FileDate.AddHours(Hours) < Now() Then
                        System.IO.File.Delete(FileName) '删除此临时文件

                    End If

                Else
                    Continue For
                End If

            Next
        Catch ex As Exception
            '操作中一旦出问题简单退出
            Exit Sub
        End Try
    End Sub
End Class
#End Region

