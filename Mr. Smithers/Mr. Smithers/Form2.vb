Imports System.IO
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
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Label4.Text = "" Then Exit Sub
        If Trim(Label4.Text) = Trim(TextBox1.Text) Then Label5.Text = "Both hashes match!!!"
        If Trim(Label4.Text) <> Trim(TextBox1.Text) Then Label5.Text = "They don't match, sir...."
    End Sub
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.Items.AddRange(IO.Directory.GetFiles(Application.StartupPath + "\tools\", "*.exe"))
        For i = 0 To ListBox1.Items.Count - 1
            ListBox1.SelectedIndex = i
            ListBox1.Items(ListBox1.SelectedIndex) = Replace(ListBox1.SelectedItem.ToString, Application.StartupPath.ToString + "\tools\", "")
        Next
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim crash As String = Replace(TextBox2.Text, "/file", Form1.CheckedListBox1.Text)
        crash = Replace(crash, "/dest", My.Settings.Output + GetFolderName(Form1.CheckedListBox1.Text.ToString) + ".iso")
        Select Case MsgBox(Application.StartupPath + "\tools\" + ListBox1.SelectedItem.ToString + " " + crash + vbCrLf + "Would you like to run this?", MsgBoxStyle.Critical + MsgBoxStyle.YesNo, "Advanced development run tool")
            Case MsgBoxResult.Yes
                Dim startInfo As New ProcessStartInfo
                startInfo.FileName = Application.StartupPath + "\tools\" + ListBox1.Text
                startInfo.Arguments = crash
                Dim process As Process = Process.Start(startInfo)
                process.WaitForExit()
            Case MsgBoxResult.No
                Exit Sub
        End Select
    End Sub
    Function GetFolderName(ByVal sDir As String) As String
        Dim gimp() = Split(sDir, "\")
        Return gimp(gimp.Length - 1)
    End Function
End Class