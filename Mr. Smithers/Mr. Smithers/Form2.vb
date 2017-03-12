Public Class Form2
    Private Sub Form2_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each path In files
            If InStr(path, ".iso") Then Label3.Text = path.ToString : Label4.Text = Krypto.hash_generator("md5", path)
        Next
    End Sub

    Private Sub Form2_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Label4.Text = "" Then Exit Sub
        If Trim(Label4.Text) = Trim(TextBox1.Text) Then Label5.Text = "Both hashes match!!!"
        If Trim(Label4.Text) <> Trim(TextBox1.Text) Then Label5.Text = "They don't match, sir...."
    End Sub
End Class