'������OdbcDbAccess 
'���ܣ�ʹ��Odbc�������ݿ�Ĳ�����
'���ߣ�������
'��дʱ�䣺2007-6-20

Imports System.Data
Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports System.Configuration

''' <summary>
''' Odbc���ݿ������
''' </summary>
''' <remarks></remarks>
Public Class OdbcDbAccess

#Region "�ֶ�"
    Private Shared ReadOnly _Connstr = System.Configuration.ConfigurationManager.ConnectionStrings("OdbcConnString").ToString.Trim
    Private Shared parmCache As Hashtable = Hashtable.Synchronized(New Hashtable)
#End Region

#Region "����"
    ''' <summary>
    ''' �����ַ���
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property ConnStr() As String
        Get
            Return _Connstr
        End Get
    End Property
#End Region

#Region "����"

    ''' <summary>
    ''' �õ�Ĭ�ϵ����ݿ����Ӷ���
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' e.g.:dim conn as OdbcConnection = OdbcDbAccess.GetOdbConnection()
    ''' �õ�һ��ODBC���ݿ����Ӷ��� �����ַ���ΪWeb.Config�ļ��е�ConnectionStrings("OdbcConnString")�ڵ�
    ''' </remarks>
    Public Shared Function GetOdbConnection() As OdbcConnection
        Return New OdbcConnection(ConnStr)
    End Function
    ''' <summary>
    ''' �õ������ַ��������ݿ����Ӷ���ûɶ��
    ''' </summary>
    ''' <remarks>
    ''' e.g.: dim conn as new OdbcConnection = odbcdbaccess.GetOdbConnection("�����ַ���...")
    '''       dim conn as new OdbcConnection("�����ַ���...")
    ''' ����һ����
    ''' </remarks>      
    ''' <param name="ConnString">һ���Ϸ��������ַ���</param>
    ''' <returns></returns>
    Public Shared Function GetOdbConnection(ByVal ConnString As String) As OdbcConnection
        Return New OdbcConnection(ConnString)
    End Function

    ''' <summary>
    ''' ִ��û�з��ؽ���Ĳ�ѯ 
    ''' </summary>
    ''' <param name="ConnString">�Ϸ����ݿ������ַ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ��������</param>
    ''' <returns>������Ӱ�������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecNoQuery(ByVal ConnString As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OdbcParameter) As Integer
        Dim cmd As New OdbcCommand
        Using con As New OdbcConnection(ConnString)
            PreparativeCommand(cmd, con, Nothing, cmdType, cmdText, params)
            Dim val As Integer = cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            Return val
        End Using
    End Function
    ''' <summary>
    ''' ִ��û�з��ؽ���Ĳ�ѯ 
    ''' </summary>
    ''' <param name="Conn">�Ѵ��ڵ����ݿ����Ӷ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ��������</param>
    ''' <returns>������Ӱ�������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecNoQuery(ByVal Conn As OdbcConnection, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OdbcParameter) As Integer
        Dim cmd As New OdbcCommand
        Dim val As Integer
        PreparativeCommand(cmd, Conn, Nothing, cmdType, cmdText, params)
        val = cmd.ExecuteNonQuery()
        cmd.Parameters.Clear()
        Return val
    End Function
    ''' <summary>
    ''' ִ��û�з��ؽ���Ĳ�ѯ  
    ''' </summary>
    ''' <param name="trans">�Ѿ����ڵ����ݿ�����</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ��������</param>
    ''' <returns>������Ӱ�������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecNoQuery(ByVal trans As OdbcTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OdbcParameter) As Integer
        Dim cmd As New OdbcCommand
        PreparativeCommand(cmd, trans.Connection, trans, cmdType, cmdText, params)
        Dim val As Integer = cmd.ExecuteNonQuery()
        cmd.Parameters.Clear()
        Return val
    End Function
    ''' <summary>
    ''' ִ��û�з��ؽ���Ĳ�ѯ  ʹ��Ĭ�ϵ����ݿ�����
    ''' </summary>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ��������</param>
    ''' <returns>������Ӱ�������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecNoQuery(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OdbcParameter) As Integer
        Dim cmd As New OdbcCommand
        Using conn As New OdbcConnection(ConnStr)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim val As Integer = cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            Return val
        End Using
    End Function

    ''' <summary>
    ''' ��ѯ ����������
    ''' </summary>
    ''' <param name="ConnString">һ���Ϸ��������ַ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteReader(ByVal ConnString As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OdbcParameter) As OdbcDataReader
        Dim cmd As New OdbcCommand
        Dim odbcReader As OdbcDataReader
        Using conn As New OdbcConnection(ConnString)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Try
                odbcReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            cmd.Parameters.Clear()
            Return odbcReader
        End Using
    End Function
    ''' <summary>
    ''' ��ѯ ʹ��Ĭ�ϵ����ݿ����� ����������
    ''' </summary>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteReader(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OdbcParameter) As OdbcDataReader
        Dim cmd As New OdbcCommand
        Dim odbcReader As OdbcDataReader
        Using conn As New OdbcConnection(ConnStr)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Try
                odbcReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            cmd.Parameters.Clear()
            Return odbcReader
        End Using
    End Function

    ''' <summary>
    ''' ��ѯ���� ʹ��Ĭ�ϵ����ݿ����� ���ص�һ�еĵ�һ��
    ''' </summary>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteScalar(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OdbcParameter) As Object
        Dim cmd As New OdbcCommand
        Using conn As New OdbcConnection(ConnStr)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim val As Object = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            Return val
        End Using

    End Function
    ''' <summary>
    ''' ��ѯ���� ���ص�һ�еĵ�һ��
    ''' </summary>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteScalar(ByVal ConnString As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OdbcParameter) As Object
        Dim cmd As New OdbcCommand
        Using conn As New OdbcConnection(ConnString)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim val As Object = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            Return val
        End Using

    End Function

    ''' <summary>
    ''' ��ѯ���� ���ز�ѯ�����
    ''' </summary>
    ''' <param name="ConnString">һ���Ϸ��������ַ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataSet(ByVal ConnString As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OdbcParameter) As DataSet
        Dim cmd As New OdbcCommand
        Dim ds As New DataSet
        Using conn As New OdbcConnection(ConnString)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim adapter As New OdbcDataAdapter(cmd)
            adapter.Fill(ds)
            cmd.Parameters.Clear()
            Return ds
        End Using
    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ��Ĭ�ϵ����ݿ����� ���ز�ѯ�����
    ''' </summary>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataSet(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OdbcParameter) As DataSet
        Dim cmd As New OdbcCommand
        Dim ds As New DataSet
        Using conn As New OdbcConnection(ConnStr)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim adapter As New OdbcDataAdapter(cmd)
            adapter.Fill(ds)
            cmd.Parameters.Clear()
            Return ds
        End Using
    End Function

    ''' <summary>
    ''' ��ѯ���� ���ز�ѯ�����
    ''' </summary>
    ''' <param name="ConnString">һ���Ϸ��������ַ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataTable(ByVal ConnString As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OdbcParameter) As DataTable
        Dim cmd As New OdbcCommand
        Dim table As New DataTable
        Using conn As New OdbcConnection(ConnString)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim adapter As New OdbcDataAdapter(cmd)
            adapter.Fill(table)
            cmd.Parameters.Clear()
            Return table
        End Using
    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ��Ĭ�ϵ����ݿ����� ���ز�ѯ�����
    ''' </summary>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataTable(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OdbcParameter) As DataTable
        Dim cmd As New OdbcCommand
        Dim table As New DataTable
        Using conn As New OdbcConnection(ConnStr)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim adapter As New OdbcDataAdapter(cmd)
            adapter.Fill(table)
            cmd.Parameters.Clear()
            Return table
        End Using
    End Function


    ''' <summary>
    ''' �õ�ĳ���һ����
    ''' </summary>
    ''' <param name="ConnString">�����ַ���</param>
    ''' <param name="TableName">���ݿ����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RowBlankGet(ByVal ConnString As String, ByVal TableName As String) As DataRow
        Dim cmd As New OdbcCommand
        Dim ds As New DataSet
        Using conn As New OdbcConnection(ConnString)
            PreparativeCommand(cmd, conn, Nothing, CommandType.Text, "SELECT * FROM " & TableName & " WHERE 1=2")
            Dim adapter As New OdbcDataAdapter(cmd)
            adapter.Fill(ds)
            Return ds.Tables(0).NewRow()
        End Using
    End Function
    ''' <summary>
    ''' �õ�ĳ���һ���� ʹ��Ĭ�ϵ����ݿ�����
    ''' </summary>
    ''' <param name="TableName">���ݿ����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RowBlankGet(ByVal TableName As String) As DataRow
        Dim cmd As New OdbcCommand
        Dim ds As New DataSet
        Using conn As New OdbcConnection(ConnStr)
            PreparativeCommand(cmd, conn, Nothing, CommandType.Text, "SELECT * FROM " & TableName & " WHERE 1=2")
            Dim adapter As New OdbcDataAdapter(cmd)
            adapter.Fill(ds)
            Return ds.Tables(0).NewRow()
        End Using
    End Function


    ''' <summary>
    ''' ���±��ĳһ��
    ''' </summary>
    ''' <param name="ConnString">�����ַ���</param>
    ''' <param name="RowID">��ID</param>
    ''' <param name="TableName">���ݱ���</param>
    ''' <param name="CurRow">�µ�������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RowUpdate(ByVal ConnString As String, ByVal RowID As String, ByVal TableName As String, ByVal CurRow As DataRow) As Boolean
        Dim cmd As New OdbcCommand
        Dim ds As New DataSet
        Dim tempRow As DataRow
        Using conn As New OdbcConnection(ConnString)

            '׼��
            PreparativeCommand(cmd, conn, Nothing, CommandType.Text, "SELECT * FROM " & TableName & " WHERE ID=" & RowID)
            Dim adapter As New OdbcDataAdapter(cmd)          '������
            Dim builder As New OdbcCommandBuilder(adapter)

            '�������
            adapter.Fill(ds)

            '���û�з��ؽ�� ���Ǹ�ID������
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            '��ʼ�༭��
            tempRow = ds.Tables(0).Rows(0)
            tempRow.BeginEdit()
            '���������ʼ�ĸ�ֵ
            Try
                RowItemCopy(tempRow, CurRow)
            Catch ex As Exception
                Return False
            End Try
            'ID
            tempRow.Item("id") = RowID

            '�����༭
            tempRow.EndEdit()
            '����
            adapter.Update(ds)
            Return True
        End Using
    End Function
    ''' <summary>
    ''' ���±��ĳһ��  ʹ��Ĭ�ϵ����ݿ�����
    ''' </summary>
    ''' <param name="RowID">��ID</param>
    ''' <param name="TableName">���ݱ���</param>
    ''' <param name="CurRow">�µ�������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RowUpdate(ByVal RowID As String, ByVal TableName As String, ByVal CurRow As DataRow) As Boolean
        Dim cmd As New OdbcCommand
        Dim ds As New DataSet
        Dim tempRow As DataRow
        Using conn As New OdbcConnection(ConnStr)

            '׼��
            PreparativeCommand(cmd, conn, Nothing, CommandType.Text, "SELECT * FROM " & TableName & " WHERE ID=" & RowID)
            Dim adapter As New OdbcDataAdapter(cmd)          '������
            Dim builder As New OdbcCommandBuilder(adapter)

            '�������
            adapter.Fill(ds)

            '���û�з��ؽ�� ���Ǹ�ID������
            If ds.Tables(0).Rows.Count = 0 Then
                Return False
            End If

            '��ʼ�༭��
            tempRow = ds.Tables(0).Rows(0)
            tempRow.BeginEdit()
            '���������ʼ�ĸ�ֵ
            Try
                RowItemCopy(tempRow, CurRow)
            Catch ex As Exception
                Return False
            End Try
            'ID
            tempRow.Item("id") = RowID

            '�����༭
            tempRow.EndEdit()
            '����
            adapter.Update(ds)
            Return True
        End Using
    End Function


    ''' <summary>
    ''' �����ݱ������һ���µļ�¼ ������ID
    ''' </summary>
    ''' <param name="ConnString">�����ַ���</param>
    ''' <param name="TableName">����</param>
    ''' <param name="RowObj">������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RowAddNew(ByVal ConnString As String, ByVal TableName As String, ByVal RowObj As DataRow) As Integer
        Dim cmd As New OdbcCommand
        Dim ds As New DataSet
        Using conn As New OdbcConnection(ConnString)
            PreparativeCommand(cmd, conn, Nothing, CommandType.Text, "SELECT * FROM " & TableName & " WHERE 1=2")
            Dim adapter As New OdbcDataAdapter(cmd)          '������
            Dim builder As New OdbcCommandBuilder(adapter)

            '�������
            adapter.Fill(ds)
            '�������
            ds.Tables(0).Rows.Add(RowObj)
            '����
            adapter.Update(ds)
            '�������
            adapter.Fill(ds)
            ' ���ظ��º��ID
            Return ds.Tables(0).Rows(0).Item("ID")
        End Using
    End Function
    ''' <summary>
    ''' �����ݱ������һ���µļ�¼ ������ID��ʹ��Ĭ�ϵ����ݿ����ӣ�
    ''' </summary>
    ''' <param name="TableName">����</param>
    ''' <param name="RowObj">������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RowAddNew(ByVal TableName As String, ByVal RowObj As DataRow) As Integer
        Dim cmd As New OdbcCommand
        Dim ds As New DataSet
        Using conn As New OdbcConnection(ConnStr)
            PreparativeCommand(cmd, conn, Nothing, CommandType.Text, "SELECT * FROM " & TableName & " WHERE 1=2")
            Dim adapter As New OdbcDataAdapter(cmd)          '������
            Dim builder As New OdbcCommandBuilder(adapter)

            '�������
            adapter.Fill(ds)
            '�������
            ds.Tables(0).Rows.Add(RowObj)
            '����
            adapter.Update(ds)
            '�������
            adapter.Fill(ds)
            ' ���ظ��º��ID
            Return ds.Tables(0).Rows(0).Item("ID")
        End Using
    End Function

    ''' <summary>
    ''' Ϊִ��T-SQL�����׼��
    ''' </summary>
    ''' <param name="cmd">OledbCommand ����</param>
    ''' <param name="conn">���ݿ����Ӷ���</param>
    ''' <param name="trans">���ݿ��������</param>
    ''' <param name="cmdType">��ѯ����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="parms">��ѯ��Ҫ�Ĳ�������</param>
    ''' <remarks></remarks>
    Private Shared Sub PreparativeCommand(ByVal cmd As OdbcCommand, ByVal conn As OdbcConnection, ByVal trans As OdbcTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray parms() As OdbcParameter)

        '�������û�д� ������
        If conn.State <> ConnectionState.Open Then
            conn.Open()
        End If

        '�������ݿ�����
        cmd.Connection = conn
        '��ѯ���
        cmd.CommandText = cmdText
        '��ѯ����  T-SQL \ �洢����
        cmd.CommandType = cmdType

        '��ʹ�������ύ���ǲ�ʹ��
        If Not IsNothing(trans) Then
            cmd.Transaction = trans
        End If

        '�����ѯ������Ϊ��(��������ֵ)
        If Not IsNothing(parms) Then
            For Each par As OdbcParameter In parms
                cmd.Parameters.Add(par)
            Next
        End If
    End Sub

    ''' <summary>
    ''' ����������֮��ĸ�ֵ
    ''' </summary>
    ''' <param name="DestRow"></param>
    ''' <param name="SrcRow"></param>
    ''' <remarks></remarks>
    Private Sub RowItemCopy(ByVal DestRow As DataRow, ByVal SrcRow As DataRow)
        Dim i As Integer
        '��ÿ�����ݽ��и���
        Try
            For i = 0 To DestRow.ItemArray.Length - 1
                DestRow.Item(i) = SrcRow.Item(i)
            Next
        Catch ex As Exception
            Throw ex
            Exit Sub
        End Try

    End Sub

    '''<summary>
    '''�����ѯ����
    ''' </summary>
    ''' <param name="cacheKey">����</param>
    ''' <param name="parms">��������</param>
    ''' <remarks>
    ''' ������Ҫ����Ĳ�������
    ''' </remarks>
    Public Shared Sub CacheParameters(ByVal cacheKey As String, ByVal ParamArray parms() As OdbcParameter)
        parmCache(cacheKey) = parms
    End Sub
    ''' <summary>
    ''' �õ�����Ĳ���
    ''' </summary>
    ''' <param name="cacheKey">����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCachedParameters(ByVal cacheKey As String) As OdbcParameter()
        Dim params() As OdbcParameter = CType(parmCache(cacheKey), OdbcParameter())
        If params Is Nothing Then
            Return Nothing
        End If

        Dim clonedParms(params.Length - 1) As OdbcParameter
        For i As Integer = 0 To params.Length - 1
            clonedParms(i) = CType(CType(params(i), ICloneable).Clone, OdbcParameter)
        Next
        Return clonedParms
    End Function
#End Region

End Class
