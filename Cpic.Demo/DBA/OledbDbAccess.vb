'������OledbDbAccess 
'���ܣ�ʹ��OledDb�������ݿ�Ĳ�����
'���ߣ�������
'��дʱ�䣺2007-6-20

Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration

''' <summary>
''' Odbc���ݿ������
''' </summary>
''' <remarks></remarks>
Public Class DbAccess

#Region "�ֶ�"
    Private Shared ReadOnly _Connstr = System.Configuration.ConfigurationManager.ConnectionStrings("OledbConnString").ToString.Trim
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
    ''' e.g.:dim conn as OleDbConnection = OledbDbAccess.GetOleDbConnection()
    ''' �õ�һ��ODBC���ݿ����Ӷ��� �����ַ���ΪWeb.Config�ļ��е�ConnectionStrings("OdbcConnString")�ڵ�
    ''' </remarks>
    Public Shared Function GetOleDbConnection() As OleDbConnection
        Return New OleDbConnection(ConnStr)
    End Function
    ''' <summary>
    ''' �õ������ַ��������ݿ����Ӷ���ûɶ��
    ''' </summary>
    ''' <remarks>
    ''' e.g.: dim conn as new OleDbConnection = OledbDbAccess.GetOleDbConnection("�����ַ���...")
    '''       dim conn as new OleDbConnection("�����ַ���...")
    ''' ����һ����
    ''' </remarks>      
    ''' <param name="ConnString">һ���Ϸ��������ַ���</param>
    ''' <returns></returns>
    Public Shared Function GetOleDbConnection(ByVal ConnString As String) As OleDbConnection
        Return New OleDbConnection(ConnString)
    End Function

    ''' <summary>
    ''' ʹ�� Ĭ�ϵ����ݿ����� ִ��û�з��ؽ����Ĳ�ѯ 
    ''' </summary>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ��������</param>
    ''' <returns>������Ӱ�������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecNoQuery(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As Integer
        Dim cmd As New OleDbCommand
        Using conn As New OleDbConnection(ConnStr)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim val As Integer = cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            Return val
        End Using
    End Function
    ''' <summary>
    ''' ʹ�� ָ�������ݿ������ַ��� ִ��û�з��ؽ����Ĳ�ѯ 
    ''' </summary>
    ''' <param name="ConnString">���ݿ������ַ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ��������</param>
    ''' <returns>������Ӱ�������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecNoQuery(ByVal ConnString As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As Integer
        Dim cmd As New OleDbCommand
        Using con As New OleDbConnection(ConnString)
            PreparativeCommand(cmd, con, Nothing, cmdType, cmdText, params)
            Dim val As Integer = cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            Return val
        End Using
    End Function
    ''' <summary>
    ''' ʹ�� ָ�������ݿ����� ִ��û�з��ؽ����Ĳ�ѯ 
    ''' </summary>
    ''' <param name="Conn">�Ѵ��ڵ����ݿ����Ӷ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ��������</param>
    ''' <returns>������Ӱ�������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecNoQuery(ByVal Conn As OleDbConnection, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As Integer
        Dim cmd As New OleDbCommand
        Dim val As Integer
        PreparativeCommand(cmd, Conn, Nothing, cmdType, cmdText, params)
        val = cmd.ExecuteNonQuery()
        cmd.Parameters.Clear()
        Return val
    End Function
    ''' <summary>
    ''' ʹ�� ָ�������ݿ����� ִ��û�з��ؽ���Ĳ�ѯ  
    ''' </summary>
    ''' <param name="trans">�Ѿ����ڵ����ݿ�����</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ��������</param>
    ''' <returns>������Ӱ�������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecNoQuery(ByVal trans As OleDbTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As Integer
        Dim cmd As New OleDbCommand
        PreparativeCommand(cmd, trans.Connection, trans, cmdType, cmdText, params)
        Dim val As Integer = cmd.ExecuteNonQuery()
        cmd.Parameters.Clear()
        Return val
    End Function

    ''' <summary>
    ''' ��ѯ ʹ��Ĭ�ϵ����ݿ����� ����������
    ''' </summary>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteReader(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As OleDbDataReader
        Dim cmd As New OleDbCommand
        Dim odbcReader As OleDbDataReader
        Using conn As New OleDbConnection(ConnStr)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            odbcReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            cmd.Parameters.Clear()
            Return odbcReader
        End Using
    End Function
    ''' <summary>
    ''' ʹ��ָ�������ݿ������ַ�����ѯ ����������
    ''' </summary>
    ''' <param name="ConnString">һ���Ϸ��������ַ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteReader(ByVal ConnString As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As OleDbDataReader
        Dim cmd As New OleDbCommand
        Dim odbcReader As OleDbDataReader
        Dim conn As New OleDbConnection(ConnString)
        PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
        odbcReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        cmd.Parameters.Clear()
        Return odbcReader
    End Function
    ''' <summary>
    ''' ��ѯ ʹ��ָ�������ݿ����� ����������
    ''' </summary>
    ''' <param name="conn">һ���Ѿ��򿪵����ݿ�����</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteReader(ByVal conn As OleDbConnection, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As OleDbDataReader
        Dim cmd As New OleDbCommand
        Dim odbcReader As OleDbDataReader
        PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
        odbcReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        cmd.Parameters.Clear()
        Return odbcReader
    End Function
    ''' <summary>
    ''' ��ѯ ʹ��ָ�������ݿ������ѯ ����������
    ''' </summary>
    ''' <param name="trans">һ����ʼ������</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteReader(ByVal trans As OleDbTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As OleDbDataReader
        Dim cmd As New OleDbCommand
        Dim odbcReader As OleDbDataReader
        PreparativeCommand(cmd, Nothing, trans, cmdType, cmdText, params)
        odbcReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        cmd.Parameters.Clear()
        Return odbcReader
    End Function

    ''' <summary>
    ''' ��ѯ���� ʹ��Ĭ�ϵ����ݿ����� ���ص�һ�еĵ�һ��
    ''' </summary>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteScalar(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As Object
        Dim cmd As New OleDbCommand
        Using conn As New OleDbConnection(ConnStr)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim val As Object = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            Return val
        End Using

    End Function
    ''' <summary>
    ''' ��ѯ����  ʹ��ָ�������ݿ������ַ��� ���ص�һ�еĵ�һ��
    ''' </summary>
    ''' <param name="ConnString">�Ϸ������ݿ������ַ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteScalar(ByVal ConnString As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As Object
        Dim cmd As New OleDbCommand
        Using conn As New OleDbConnection(ConnString)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim val As Object = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            Return val
        End Using

    End Function
    ''' <summary>
    ''' ��ѯ����  ʹ��ָ�������ݿ����� ���ص�һ�еĵ�һ��
    ''' </summary>
    ''' <param name="conn">һ���Ѿ��򿪵����ݿ�����</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteScalar(ByVal conn As OleDbConnection, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As Object
        Dim cmd As New OleDbCommand
        PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
        Dim val As Object = cmd.ExecuteScalar()
        cmd.Parameters.Clear()
        Return val

    End Function
    ''' <summary>
    ''' ��ѯ����  ʹ��ָ�������ݿ����� ���ص�һ�еĵ�һ��
    ''' </summary>
    ''' <param name="trans">һ���Ѿ�����������</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteScalar(ByVal trans As OleDbTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As Object
        Dim cmd As New OleDbCommand
        PreparativeCommand(cmd, trans.Connection, trans, cmdType, cmdText, params)
        Dim val As Object = cmd.ExecuteScalar()
        cmd.Parameters.Clear()
        Return val

    End Function

    ''' <summary>
    ''' ��ѯ���� ʹ��Ĭ�ϵ����ݿ����� ���ز�ѯ�����
    ''' </summary>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataSet(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As DataSet
        Dim cmd As New OleDbCommand
        Dim ds As New DataSet
        Using conn As New OleDbConnection(ConnStr)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim adapter As New OleDbDataAdapter(cmd)
            adapter.Fill(ds)
            cmd.Parameters.Clear()
            Return ds
        End Using
    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ��ָ���������ַ��� ���ز�ѯ�����
    ''' </summary>
    ''' <param name="ConnString">һ���Ϸ��������ַ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataSet(ByVal ConnString As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As DataSet
        Dim cmd As New OleDbCommand
        Dim ds As New DataSet
        Using conn As New OleDbConnection(ConnString)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim adapter As New OleDbDataAdapter(cmd)
            adapter.Fill(ds)
            cmd.Parameters.Clear()
            Return ds
        End Using
    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ��ָ�������ݿ����� ���ز�ѯ�����
    ''' </summary>
    ''' <param name="conn">һ����ʼ������</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataSet(ByVal conn As OleDbConnection, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As DataSet
        Dim cmd As New OleDbCommand
        Dim ds As New DataSet
        PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
        Dim adapter As New OleDbDataAdapter(cmd)
        adapter.Fill(ds)
        cmd.Parameters.Clear()
        Return ds
    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ��ָ�������ݿ����� ���ز�ѯ�����
    ''' </summary>
    ''' <param name="trans">һ����ʼ������</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataSet(ByVal trans As OleDbTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As DataSet
        Dim cmd As New OleDbCommand
        Dim ds As New DataSet
        PreparativeCommand(cmd, trans.Connection, trans, cmdType, cmdText, params)
        Dim adapter As New OleDbDataAdapter(cmd)
        adapter.Fill(ds)
        cmd.Parameters.Clear()
        Return ds
    End Function

    ''' <summary>
    ''' ��ѯ���� ʹ��Ĭ�ϵ����ݿ����� ���ز�ѯ�����
    ''' </summary>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataTable(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As DataTable
        Dim cmd As New OleDbCommand
        Dim table As New DataTable
        Using conn As New OleDbConnection(ConnStr)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim adapter As New OleDbDataAdapter(cmd)
            adapter.Fill(table)
            cmd.Parameters.Clear()
            Return table
        End Using
    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ��ָ���������ַ��� ���ز�ѯ�����
    ''' </summary>
    ''' <param name="ConnString">һ���Ϸ��������ַ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataTable(ByVal ConnString As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As DataTable
        Dim cmd As New OleDbCommand
        Dim table As New DataTable
        Using conn As New OleDbConnection(ConnString)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim adapter As New OleDbDataAdapter(cmd)
            adapter.Fill(table)
            cmd.Parameters.Clear()
            Return table
        End Using
    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ��ָ�������ݿ����� ���ز�ѯ�����
    ''' </summary>
    ''' <param name="conn">һ���Ѿ��򿪵����ݿ�����</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataTable(ByVal conn As OleDbConnection, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As DataTable
        Dim cmd As New OleDbCommand
        Dim table As New DataTable
        PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
        Dim adapter As New OleDbDataAdapter(cmd)
        adapter.Fill(table)
        cmd.Parameters.Clear()
        Return table
    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ��ָ�������ݿ����� ���ز�ѯ�����
    ''' </summary>
    ''' <param name="trans">һ���Ѿ�����������</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataTable(ByVal trans As OleDbTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As OleDbParameter) As DataTable
        Dim cmd As New OleDbCommand
        Dim table As New DataTable
        PreparativeCommand(cmd, trans.Connection, trans, cmdType, cmdText, params)
        Dim adapter As New OleDbDataAdapter(cmd)
        adapter.Fill(table)
        cmd.Parameters.Clear()
        Return table
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
    Private Shared Sub PreparativeCommand(ByVal cmd As OleDbCommand, ByVal conn As OleDbConnection, ByVal trans As OleDbTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray parms() As OleDbParameter)

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
            For Each par As OleDbParameter In parms
                cmd.Parameters.Add(par)
            Next
        End If
    End Sub
    ''' <summary>
    ''' �����ѯ����
    ''' </summary>
    ''' <param name="cacheKey">����</param>
    ''' <param name="parms">��������</param>
    ''' <remarks>
    ''' ������Ҫ����Ĳ�������
    ''' </remarks>
    Public Shared Sub CacheParameters(ByVal cacheKey As String, ByVal ParamArray parms() As OleDbParameter)
        parmCache(cacheKey) = parms
    End Sub
    ''' <summary>
    ''' �õ�����Ĳ���
    ''' </summary>
    ''' <param name="cacheKey">����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCachedParameters(ByVal cacheKey As String) As OleDbParameter()
        '�ж��Ƿ񱣴�����Ҫ�Ĳ���
        Dim params() As OleDbParameter = CType(parmCache(cacheKey), OleDbParameter())

        '���û�оͷ���nothing
        If params Is Nothing Then
            Return Nothing
        End If
        '�� �Ѿ�������Ĳ���
        Dim clonedParms(params.Length - 1) As OleDbParameter '��������������Ĳ�������һ����С�Ĳ�������

        '��ֵ
        For i As Integer = 0 To params.Length - 1
            clonedParms(i) = CType(CType(params(i), ICloneable).Clone, OleDbParameter)
        Next
        Return clonedParms
    End Function
#End Region

End Class
