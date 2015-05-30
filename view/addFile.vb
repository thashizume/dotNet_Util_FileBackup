Public Class addFile

    Private _file As DataFile
    Private _isNew As Boolean = False

    Private Sub frmFile_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
        PropertyGrid1.SelectedObject = Me._file

    End Sub

    ''' <summary>
    ''' Constractor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        _file = New DataFile
        _isNew = True
    End Sub
    ''' <summary>
    ''' Constractor
    ''' </summary>
    ''' <param name="file"></param>
    ''' <remarks></remarks>
    Public Sub New(file As DataFile)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        _file = file
    End Sub

    ''' <summary>
    ''' Event: Button Click [OK]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click

        Dim _dfs As DataFiles = New DataFiles

        If _isNew = True Then
            _dfs.Add(_file)

        Else
            _dfs.Modify(_file)

        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    Public ReadOnly Property dataFile As DataFile
        Get
            Return _file
        End Get
    End Property
End Class