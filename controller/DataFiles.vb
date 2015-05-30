Public Class DataFiles
    Private _dt As System.Data.DataTable
    Private Const FILE_NAME As String = "config.tab"
    Private Const __dataTableName__ As String = "DataFile"

    Public ReadOnly Property Files As System.Data.DataTable
        Get
            Return _dt
        End Get
    End Property

    Public Sub New()

        If New System.IO.FileInfo(FILE_NAME).Exists Then
            Dim d2t As jp.polestar.io.Datatable2TSV = New jp.polestar.io.Datatable2TSV
            _dt = d2t.tsv2dt(FILE_NAME, __dataTableName__, False)

        Else
            initDatatable()
        End If

    End Sub

    ''' <summary>
    ''' Function : Add
    ''' </summary>
    ''' <param name="dataFile"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Add(dataFile As DataFile) As Boolean

        Dim row As System.Data.DataRow
        row = _dt.NewRow
        row(0) = dataFile.GUID
        row(1) = dataFile.FullName
        row(2) = dataFile.BackupPathName
        row(3) = dataFile.DeployPathName
        row(4) = dataFile.Description
        _dt.Rows.Add(row)
        If Me.Save() = False Then Throw New Exception("Exception Save Method")
        Return True

    End Function

    Public Function Remove(guid As String) As Boolean

        Dim rows() As System.Data.DataRow = _dt.Select("GUID='" & guid & "'")
        For Each row As System.Data.DataRow In rows
            _dt.Rows.Remove(row)

        Next
        If Me.Save() = False Then Throw New Exception("Exception Save Method")
        
        Return True
    End Function

    ''' <summary>
    ''' Function Validate
    ''' </summary>
    ''' <param name="dataFile"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Validate(dataFile As DataFile) As Boolean

        If Not New System.IO.FileInfo(dataFile.FullName).Exists Then Return False
        If dataFile.BackupPathName.Length <= 0 And dataFile.DeployPathName.Length <= 0 Then Return False

        If dataFile.BackupPathName.Length > 0 Then
            If Not New System.IO.DirectoryInfo(dataFile.BackupPathName).Exists Then Return False
        End If

        If dataFile.DeployPathName.Length > 0 Then
            If Not New System.IO.DirectoryInfo(dataFile.DeployPathName).Exists Then Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' Function: getDatafile
    ''' </summary>
    ''' <param name="guid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDatafile(guid As String) As DataFile

        Dim result As DataFile = New DataFile
        Dim rows() As System.Data.DataRow

        rows = _dt.Select("GUID='" & guid & "'")
        result.GUID = rows(0)(0)
        result.FullName = rows(0)(1)
        result.BackupPathName = rows(0)(2)
        result.DeployPathName = rows(0)(3)

        Return result

    End Function

    ''' <summary>
    ''' Function: Modify
    ''' </summary>
    ''' <param name="dataFile"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Modify(dataFile As DataFile) As Boolean

        ' If Validate(dataFile) = False Then Return False

        Dim row() As System.Data.DataRow = _dt.Select("GUID='" & dataFile.GUID & "'")
        If row(0)(0) <> dataFile.GUID Then Return False

        row(0)(1) = dataFile.FullName
        row(0)(2) = dataFile.BackupPathName
        row(0)(3) = dataFile.DeployPathName
        row(0)(4) = dataFile.Description

        If Me.Save() = False Then Throw New Exception("Exception Save Method")

        Return True

    End Function

    ''' <summary>
    ''' Function: Save
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save() As Boolean
        Try
            Dim d2t As jp.polestar.io.Datatable2TSV = New jp.polestar.io.Datatable2TSV
            d2t.dt2tsv(_dt, FILE_NAME, True)
        Catch ex As Exception
            Return False
        End Try
       
        Return True
    End Function

    Private Sub initDatatable()
        _dt = New System.Data.DataTable(Me.ToString)
        _dt.Columns.Add("GUID", GetType(System.String))
        _dt.Columns.Add("FileName", GetType(System.String))
        _dt.Columns.Add("BackupDirectory", GetType(System.String))
        _dt.Columns.Add("DeployDirectory", GetType(System.String))
        _dt.Columns.Add("Description", GetType(System.String))
    End Sub

End Class
