Public Class main


    Private Sub main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ucFile As ucFiles = New ucFiles
        Me.Controls.Add(ucFile)
        Me.Controls(ucFile.Name).Dock = DockStyle.Fill
    End Sub
End Class
