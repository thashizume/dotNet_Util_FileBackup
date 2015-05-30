Public Class ucFiles

    Private Sub lnAdd_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnAdd.LinkClicked
        Dim file As New addFile
        If file.ShowDialog() = DialogResult.OK Then

        End If
        initDataGridView()

    End Sub

    Private Sub ucFiles_Load(sender As Object, e As EventArgs) Handles Me.Load
        initDataGridView()

    End Sub

    Private Sub initDataGridView()
        DataGridView1.Columns.Clear()

        Dim c1 As System.Windows.Forms.DataGridViewLinkColumn = New System.Windows.Forms.DataGridViewLinkColumn
        Dim c2 As System.Windows.Forms.DataGridViewLinkColumn = New System.Windows.Forms.DataGridViewLinkColumn
        Dim c3 As System.Windows.Forms.DataGridViewLinkColumn = New System.Windows.Forms.DataGridViewLinkColumn
        Dim c4 As System.Windows.Forms.DataGridViewLinkColumn = New System.Windows.Forms.DataGridViewLinkColumn

        c1.Name = "LNK01"
        c1.UseColumnTextForLinkValue = True
        c1.HeaderText = String.Empty
        c1.Text = "Modify"
        c1.LinkBehavior = LinkBehavior.HoverUnderline
        c1.TrackVisitedState = True
        c1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridView1.Columns.Add(c1)

        c4.Name = "LNK04"
        c4.UseColumnTextForLinkValue = True
        c4.HeaderText = String.Empty
        c4.Text = "Delete"
        c4.LinkBehavior = LinkBehavior.HoverUnderline
        c4.TrackVisitedState = True
        c4.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridView1.Columns.Add(c4)

        c2.Name = "LNK02"
        c2.UseColumnTextForLinkValue = True
        c2.HeaderText = String.Empty
        c2.Text = "Backup"
        c2.LinkBehavior = LinkBehavior.HoverUnderline
        c2.TrackVisitedState = True
        c2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridView1.Columns.Add(c2)

        c3.Name = "LNK03"
        c3.UseColumnTextForLinkValue = True
        c3.HeaderText = String.Empty
        c3.Text = "Deploy"
        c3.LinkBehavior = LinkBehavior.HoverUnderline
        c3.TrackVisitedState = True
        c3.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        DataGridView1.Columns.Add(c3)

        DataGridView1.DataSource = Me._dataView((New DataFiles).Files)
        DataGridView1.Columns("GUID").Visible = False
        DataGridView1.Columns("FULL_NAME").Visible = False
        DataGridView1.Columns("SHORT_NAME").HeaderText = "File Name"
        DataGridView1.Columns("SHORT_NAME").Width = "200"

        DataGridView1.Columns("DESCRIPTION").HeaderText = "Description"
        DataGridView1.Columns("DESCRIPTION").Width = "200"

        DataGridView1.Columns("BACKUP_DIRECTORY").HeaderText = "Backup Directory Name"
        DataGridView1.Columns("BACKUP_DIRECTORY").Width = 300


        DataGridView1.Columns("DEPLOY_DIRECTORY").HeaderText = "Deploy Directory Name"
        DataGridView1.Columns("DEPLOY_DIRECTORY").Width = 300
        DataGridView1.Columns("DEPLOY_DIRECTORY").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect

    End Sub

    Private Function _dataView(dt As System.Data.DataTable) As System.Data.DataTable
        Dim result As System.Data.DataTable = New System.Data.DataTable

        result.Columns.Add("GUID", GetType(System.String))
        result.Columns.Add("FULL_NAME", GetType(System.String))
        result.Columns.Add("SHORT_NAME", GetType(System.String))
        result.Columns.Add("DESCRIPTION", GetType(System.String))
        result.Columns.Add("BACKUP_DIRECTORY", GetType(System.String))
        result.Columns.Add("DEPLOY_DIRECTORY", GetType(System.String))


        For Each row As System.Data.DataRow In dt.Rows
            Try
                Dim _row As System.Data.DataRow = result.NewRow
                _row("GUID") = row("GUID")
                _row("FULL_NAME") = row("FileName")
                _row("SHORT_NAME") = New System.IO.FileInfo(row("FileName")).Name
                _row("DESCRIPTION") = row("Description")
                _row("BACKUP_DIRECTORY") = row("BackupDirectory")
                _row("DEPLOY_DIRECTORY") = row("DeployDirectory")

                result.Rows.Add(_row)

            Catch ex As Exception

            End Try
        Next
        Return result
    End Function


    Private Sub lnExit_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnExit.LinkClicked
        Application.Exit()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim dgv As DataGridView = CType(sender, DataGridView)
        Dim GUID As String = dgv.Rows(e.RowIndex).Cells("GUID").Value
        If dgv.Columns(e.ColumnIndex).Name = "LNK01" Then
            Dim _df As DataFile = (New DataFiles).getDatafile(GUID)
            Dim frm As addFile = New addFile(_df)
            frm.ShowDialog()

        ElseIf dgv.Columns(e.ColumnIndex).Name = "LNK02" Then
            If MsgBox("Backup, are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim _df As DataFile = (New DataFiles).getDatafile(GUID)
                _df.Backup()

            End If

        ElseIf dgv.Columns(e.ColumnIndex).Name = "LNK03" Then
            If MsgBox("Deploy, are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim _df As DataFile = (New DataFiles).getDatafile(GUID)
                _df.Deploy()

            End If

        ElseIf dgv.Columns(e.ColumnIndex).Name = "LNK04" Then
            If MsgBox("Delete, are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim _dfs As DataFiles = New DataFiles
                _dfs.Remove(GUID)
            End If

        End If
        initDataGridView()

    End Sub

End Class
