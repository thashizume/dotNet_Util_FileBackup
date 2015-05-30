Imports System.ComponentModel

Public Class DataFile

    Private _prefix As String = "【参照用】"
    Private _file As System.IO.FileInfo = Nothing
    Private _description As String = String.Empty
    Private _backupPath As System.IO.DirectoryInfo = Nothing
    Private _deployPath As System.IO.DirectoryInfo = Nothing
    Private _guid As String

    Public Sub New()
        _guid = System.Guid.NewGuid().ToString.ToUpper
    End Sub

    <Category("1.ファイル"), Description("Description"), DisplayName("ファイル説明")> _
    Public Property Description As String
        Get
            Return _description
        End Get
        Set(value As String)
            _description = value
        End Set
    End Property


    <ReadOnlyAttribute(True), Category("その他"), Description("内部識別子"), DisplayName("GUID")> _
    Public Property GUID As String
        Get
            Return _guid
        End Get
        Set(value As String)
            _guid = value
        End Set
    End Property

    <Description("バックアップもしくは、配布するするファイル名"), Category("1.ファイル"), DisplayName("ファイル名")> _
    Public Property FullName As String
        Get
            If Me._file Is Nothing Then Return String.Empty
            Return Me._file.FullName

        End Get
        Set(value As String)

            If (New System.IO.FileInfo(value)).Exists Then
                Me._file = New System.IO.FileInfo(value)
            Else
                Me._file = Nothing
                Throw New Exception("File Not Found " & value)
            End If

        End Set
    End Property

    <Description("バックアップする先のフォルダ名"), Category("2.バックアップ"), DisplayName("バックアップ先フォルダ名")> _
    Public Property BackupPathName As String
        Get
            If Me._backupPath Is Nothing Then Return String.Empty
            Return Me._backupPath.FullName

        End Get
        Set(value As String)
            If value.Length <= 0 Then
                Me._backupPath = Nothing

            Else
                If (New System.IO.DirectoryInfo(value)).Exists Then
                    Me._backupPath = New System.IO.DirectoryInfo(value)
                Else
                    _backupPath = Nothing
                    Throw New Exception("Directory Not Found " & value)
                End If
            End If

        End Set
    End Property

    <Description("配布する先のフォルダの名"), Category("3.配布"), DisplayName("配布先フォルダ名")> _
    Public Property DeployPathName As String
        Get
            If Me._deployPath Is Nothing Then Return String.Empty
            Return Me._deployPath.FullName
        End Get
        Set(value As String)
            If value.Length <= 0 Then
                _deployPath = Nothing

            Else
                If (New System.IO.DirectoryInfo(value)).Exists Then
                    Me._deployPath = New System.IO.DirectoryInfo(value)
                Else
                    _deployPath = Nothing
                    Throw New Exception("Directory Not Found " & value)
                End If

            End If
        End Set
    End Property

    <ReadOnlyAttribute(True), Description("配布先のファイル名に付加する文字列"), Category("3.配布")> _
    Public Property Prefix As String
        Get
            Return Me._prefix
        End Get
        Set(value As String)
            Me._prefix = value
        End Set
    End Property

    <Description("配布するファイル名"), Category("3.配布"), DisplayName("配布ファイル名")> _
    Public ReadOnly Property DeployFileName As String
        Get
            If _file Is Nothing Then Return String.Empty
            If _deployPath Is Nothing Then Return String.Empty

            Dim _fileName As String = Me._prefix & _file.Name.Replace(_file.Extension, String.Empty) & _file.Extension
            _fileName = Me._deployPath.FullName & "\" & _fileName
            Return _fileName

        End Get
    End Property

    <Description("バックアップファイル名。ファイル名には実行時の年月日が付加される"), Category("2.バックアップ"), DisplayName("バックアップファイル名")> _
    Public ReadOnly Property BackupFileName As String
        Get
            If _file Is Nothing Then Return String.Empty
            If _backupPath Is Nothing Then Return String.Empty

            Dim dateString As String = DateTime.Now.ToString("yyyyMMdd")
            Dim _fileName As String = _file.Name.Replace(_file.Extension, String.Empty) & "_" & dateString & _file.Extension
            _fileName = _backupPath.FullName & "\" & _fileName
            Return _fileName

        End Get
    End Property

    Public Function Backup() As Boolean

        If Me.BackupFileName.Length <= 0 Then Return False
        If Me.FullName.Length <= 0 Then Return False
        If Not Me.ExistFile(Me.FullName) Then Throw New Exception("file not found:" & Me.FullName)

        Dim _f As System.IO.FileInfo = New System.IO.FileInfo(Me.FullName)
        If Me.ExistFile(Me.BackupFileName) Then Return False
        _f.CopyTo(Me.BackupFileName, False)
        Return True

    End Function

    Public Function Deploy() As Boolean

        If Me.DeployFileName.Length <= 0 Then Return False
        If Me.FullName.Length <= 0 Then Return False
        If Not Me.ExistFile(Me.FullName) Then Throw New Exception("file not found:" & Me.FullName)

        Dim _f As System.IO.FileInfo = New System.IO.FileInfo(Me.FullName)
        _f.CopyTo(Me.DeployFileName, True)
        Return True

    End Function

    Private Function ExistFile(fileName As String) As Boolean
        Dim result As Boolean = False
        Dim _f As System.IO.FileInfo

        If fileName.Length <= 0 Then Throw New Exception("undefine file:" & fileName)
        _f = New System.IO.FileInfo(fileName)

        Return _f.Exists

    End Function

End Class
