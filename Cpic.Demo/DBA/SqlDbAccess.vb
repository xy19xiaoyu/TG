'������SqlDbAccess 
'���ܣ�SQL SERVER ר�õ��������ݿ�Ĳ�����
'���ߣ�������
'��дʱ�䣺2007-6-30


Imports System.Data
Imports System.Data.sql
Imports System.Data.SqlClient
Imports System.Configuration
''' <summary>
''' SQL SERVER���ݿ������
''' </summary>
''' <remarks>
''' dim strSQL = "UPDATE [TABLENAME] SET [COLNAME]=@VALUE WHERE ID=@ID"
''' dim params(1) = new SqlParameter()
''' params(0) = SqlParameter("@VALUE",sqldbtype.varchar,10)
''' params(1) = sqlparameter("@id",sqldbtype.int)
''' params(0).value  = "234"
''' parmas(1).value = 123
''' try
'''     if sqldbaccess.ExecNoQuery(commandtype.text,strsql,params) != 0 then
'''     'TODO: something
'''     end if
''' catch ex as Exception
''' end try
''' 
''' </remarks>
Public Class SqlDbAccess

#Region "�ֶ�"
    Private Shared _Connstr = System.Configuration.ConfigurationManager.ConnectionStrings("SqlServerStr").ToString.Trim
    Private Shared parmCache As Hashtable = Hashtable.Synchronized(New Hashtable)
#End Region

#Region "����"
    ''' <summary>
    ''' �����ַ���
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Property ConnStr() As String
        Get
            Return _Connstr
        End Get
        Set(ByVal value As String)
            _Connstr = value
        End Set
    End Property
#End Region

#Region "����"

    ''' <summary>
    ''' �õ�Ĭ�ϵ����ݿ����Ӷ���
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' e.g.:dim conn as SqlConnection = SqlDbAccess.GetSqlConnection()
    ''' �õ�һ��ODBC���ݿ����Ӷ��� �����ַ���ΪWeb.Config�ļ��е�ConnectionStrings("SqlServerConnString")�ڵ�
    ''' </remarks>
    Public Shared Function GetSqlConnection() As SqlConnection
        Return New SqlConnection(_Connstr)
    End Function
    ''' <summary>
    ''' �õ������ַ��������ݿ����Ӷ���ûɶ��
    ''' </summary>
    ''' <remarks>
    ''' e.g.: dim conn as new SqlConnection = SqlDbAccess.GetSqlConnection("�����ַ���...")
    '''       dim conn as new SqlConnection("�����ַ���...")
    ''' ����һ����
    ''' </remarks>      
    ''' <param name="ConnString">һ���Ϸ��������ַ���</param>
    ''' <returns></returns>
    Public Shared Function GetSqlConnection(ByVal ConnString As String) As SqlConnection
        Return New SqlConnection(ConnString)
    End Function

    ''' <summary>
    ''' ʹ��Ĭ�ϵ����ݿ����� ִ��û�з��ؽ�����Ĳ�ѯ  
    ''' </summary>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ��������</param>
    ''' <returns>������Ӱ�������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecNoQuery(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As Integer
        Dim cmd As New SqlCommand
        Using conn As New SqlConnection(ConnStr)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim val As Integer = cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            Return val
        End Using
    End Function
    ''' <summary>
    ''' ʹ��ָ�������ݿ������ַ��� ִ��û�з��ؽ�����Ĳ�ѯ 
    ''' </summary>
    ''' <param name="ConnString">���ݿ������ַ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ��������</param>
    ''' <returns>������Ӱ�������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecNoQuery(ByVal ConnString As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As Integer
        Dim cmd As New SqlCommand
        Using con As New SqlConnection(ConnString)
            PreparativeCommand(cmd, con, Nothing, cmdType, cmdText, params)
            Dim val As Integer = cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            Return val
        End Using
    End Function
    ''' <summary>
    ''' ʹ��ָ�������ݿ����� ִ��û�з��ؽ�����Ĳ�ѯ 
    ''' </summary>
    ''' <param name="Conn">�Ѵ��ڵ����ݿ����Ӷ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ��������</param>
    ''' <returns>������Ӱ�������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecNoQuery(ByVal Conn As SqlConnection, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As Integer
        Dim cmd As New SqlCommand
        Dim val As Integer
        PreparativeCommand(cmd, Conn, Nothing, cmdType, cmdText, params)
        val = cmd.ExecuteNonQuery()
        cmd.Parameters.Clear()
        Return val
    End Function
    ''' <summary>
    ''' ʹ��ָ�������ݿ��������� ִ��û�з��ؽ�����Ĳ�ѯ  
    ''' </summary>
    ''' <param name="trans">�Ѿ����ڵ����ݿ�����</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ��������</param>
    ''' <returns>������Ӱ�������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecNoQuery(ByVal trans As SqlTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As Integer
        Dim cmd As New SqlCommand
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
    Public Shared Function ExecuteReader(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As SqlDataReader
        Dim cmd As New SqlCommand
        Dim odbcReader As SqlDataReader
        Dim conn As New SqlConnection(ConnStr)
        PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
        odbcReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        cmd.Parameters.Clear()
        Return odbcReader
    End Function
    ''' <summary>
    ''' ��ѯ ʹ��ָ�������ݿ������ַ��� ����������
    ''' </summary>
    ''' <param name="ConnString">һ���Ϸ��������ַ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteReader(ByVal ConnString As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As SqlDataReader
        Dim cmd As New SqlCommand
        Dim odbcReader As SqlDataReader
        Dim conn As New SqlConnection(ConnString)
        PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
        odbcReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        cmd.Parameters.Clear()
        Return odbcReader
    End Function
    ''' <summary>
    ''' ��ѯ ʹ��ָ�������ݿ����� ����������
    ''' </summary>
    ''' <param name="conn">�Ѵ��ڵ����ݿ����Ӷ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteReader(ByVal conn As SqlConnection, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As SqlDataReader
        Dim cmd As New SqlCommand
        Dim odbcReader As SqlDataReader
        PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
        odbcReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        cmd.Parameters.Clear()
        Return odbcReader
    End Function
    ''' <summary>
    ''' ��ѯ ʹ��ָ�������ݿ��������� ����������
    ''' </summary>
    ''' <param name="trans">�Ѿ����ڵ����ݿ�����</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>������</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteReader(ByVal trans As SqlTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As SqlDataReader
        Dim cmd As New SqlCommand
        Dim odbcReader As SqlDataReader
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
    Public Shared Function ExecuteScalar(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As Object
        Dim cmd As New SqlCommand
        Using conn As New SqlConnection(ConnStr)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim val As Object = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            Return val
        End Using

    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ��ָ���������ַ��� ���ص�һ�еĵ�һ��
    ''' </summary>
    ''' <param name="ConnString">ʹ��ָ���������ַ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteScalar(ByVal ConnString As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As Object
        Dim cmd As New SqlCommand
        Using conn As New SqlConnection(ConnString)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim val As Object = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            Return val
        End Using

    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ��ָ�������ݿ����� ���ص�һ�еĵ�һ��
    ''' </summary>
    ''' <param name="conn">һ���Ѿ����������ݿ�����</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteScalar(ByVal conn As SqlConnection, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As Object
        Dim cmd As New SqlCommand
        PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
        Dim val As Object = cmd.ExecuteScalar()
        cmd.Parameters.Clear()
        Return val

    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ��ָ�������ݿ��������� ���ص�һ�еĵ�һ��
    ''' </summary>
    ''' <param name="trans">һ���Ѿ�����������</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteScalar(ByVal trans As SqlTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As Object
        Dim cmd As New SqlCommand
        PreparativeCommand(cmd, trans.Connection, trans, cmdType, cmdText, params)
        Dim val As Object = cmd.ExecuteScalar()
        cmd.Parameters.Clear()
        Return val

    End Function


    ''' <summary>
    ''' ��ѯ���� ʹ��Ĭ�ϵ����ݿ�����   ���ز�ѯ�����
    ''' </summary>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataSet(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As DataSet
        Dim cmd As New SqlCommand
        Dim ds As New DataSet
        Using conn As New SqlConnection(ConnStr)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(ds)
            cmd.Parameters.Clear()
            Return ds
        End Using
    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ��ָ���������ַ���   ���ز�ѯ�����
    ''' </summary>
    ''' <param name="ConnString">һ���Ϸ��������ַ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataSet(ByVal ConnString As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As DataSet
        Dim cmd As New SqlCommand
        Dim ds As New DataSet
        Using conn As New SqlConnection(ConnString)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(ds)
            cmd.Parameters.Clear()
            Return ds
        End Using
    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ��ָ�������ݿ�����   ���ز�ѯ�����
    ''' </summary>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataSet(ByVal conn As SqlConnection, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As DataSet
        Dim cmd As New SqlCommand
        Dim ds As New DataSet
        PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
        Dim adapter As New SqlDataAdapter(cmd)
        adapter.Fill(ds)
        cmd.Parameters.Clear()
        Return ds
    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ�õ�ָ�����ݿ��������� ���ز�ѯ�����
    ''' </summary>
    ''' <param name="trans">ָ�����ݿ���������</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataSet(ByVal trans As SqlTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As DataSet
        Dim cmd As New SqlCommand
        Dim ds As New DataSet
        PreparativeCommand(cmd, trans.Connection, trans, cmdType, cmdText, params)
        Dim adapter As New SqlDataAdapter(cmd)
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
    Public Shared Function GetDataTable(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As DataTable
        Dim cmd As New SqlCommand
        Dim table As New DataTable
        Using conn As New SqlConnection(ConnStr)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(table)
            cmd.Parameters.Clear()
            Return table
        End Using
    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ��ָ���������ַ���   ���ز�ѯ�����
    ''' </summary>
    ''' <param name="ConnString">һ���Ϸ��������ַ���</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataTable(ByVal ConnString As String, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As DataTable
        Dim cmd As New SqlCommand
        Dim table As New DataTable
        Using conn As New SqlConnection(ConnString)
            PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
            Dim adapter As New SqlDataAdapter(cmd)
            adapter.Fill(table)
            cmd.Parameters.Clear()
            Return table
        End Using
    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ��ָ�������ݿ�����   ���ز�ѯ�����
    ''' </summary>
    ''' <param name="conn">һ���򿪵����ݿ�����</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataTable(ByVal conn As SqlConnection, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As DataTable
        Dim cmd As New SqlCommand
        Dim table As New DataTable
        PreparativeCommand(cmd, conn, Nothing, cmdType, cmdText, params)
        Dim adapter As New SqlDataAdapter(cmd)
        adapter.Fill(table)
        cmd.Parameters.Clear()
        Return table
    End Function
    ''' <summary>
    ''' ��ѯ���� ʹ�õ�ָ�����ݿ��������� ���ز�ѯ�����
    ''' </summary>
    ''' <param name="trans">һ���Ѿ�����������</param>
    ''' <param name="cmdType">��ѯ����T-SQL���\�洢����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="params">��ѯ����</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataTable(ByVal trans As SqlTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray params() As SqlParameter) As DataTable
        Dim cmd As New SqlCommand
        Dim table As New DataTable
        PreparativeCommand(cmd, trans.Connection, trans, cmdType, cmdText, params)
        Dim adapter As New SqlDataAdapter(cmd)
        adapter.Fill(table)
        cmd.Parameters.Clear()
        Return table
    End Function


    ''' <summary>
    ''' Ϊִ��T-SQL�����׼��
    ''' </summary>
    ''' <param name="cmd">SqlCommand ����</param>
    ''' <param name="conn">���ݿ����Ӷ���</param>
    ''' <param name="trans">���ݿ��������</param>
    ''' <param name="cmdType">��ѯ����</param>
    ''' <param name="cmdText">��ѯ���</param>
    ''' <param name="parms">��ѯ��Ҫ�Ĳ�������</param>
    ''' <remarks></remarks>
    Private Shared Sub PreparativeCommand(ByVal cmd As SqlCommand, ByVal conn As SqlConnection, ByVal trans As SqlTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal ParamArray parms() As SqlParameter)

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
            For Each par As SqlParameter In parms
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
    Public Shared Sub CacheParameters(ByVal cacheKey As String, ByVal ParamArray parms() As SqlParameter)
        parmCache(cacheKey) = parms
    End Sub
    ''' <summary>
    ''' �õ�����Ĳ���
    ''' </summary>
    ''' <param name="cacheKey">����</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCachedParameters(ByVal cacheKey As String) As SqlParameter()
        '�ж��Ƿ񱣴�����Ҫ�Ĳ���
        Dim params() As SqlParameter = CType(parmCache(cacheKey), SqlParameter())

        '���û�оͷ���nothing
        If params Is Nothing Then
            Return Nothing
        End If
        '�� �Ѿ�������Ĳ���
        Dim clonedParms(params.Length - 1) As SqlParameter '��������������Ĳ�������һ����С�Ĳ�������

        '��ֵ
        For i As Integer = 0 To params.Length - 1
            clonedParms(i) = CType(CType(params(i), ICloneable).Clone, SqlParameter)
        Next
        Return clonedParms
    End Function
#End Region

End Class
