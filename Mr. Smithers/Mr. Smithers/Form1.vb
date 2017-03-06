Public Class Form1
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim grab As String = FolderBrowserDialog1.ShowDialog()
        If FolderBrowserDialog1.SelectedPath <> "" Then
            For Each folder As String In System.IO.Directory.GetDirectories(FolderBrowserDialog1.SelectedPath)
                If IO.Directory.Exists(folder + "\PS3_GAME\") = True Then CheckedListBox1.Items.Add(folder)
            Next
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For i = 0 To CheckedListBox1.Items.Count - 1
            CheckedListBox1.SelectedIndex = i
            If CheckedListBox1.GetItemChecked(i) = True Then
                Dim startInfo As New ProcessStartInfo
                startInfo.FileName = Application.StartupPath + "\tools\mk.exe"
                startInfo.Arguments = " " + Chr(34) + CheckedListBox1.Text.ToString + Chr(34) + " " + Chr(34) + My.Settings.Output.ToString + "\" + Chr(34)
                Dim process As Process = Process.Start(startInfo)
                process.WaitForExit()
            End If
        Next
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim grab As String = FolderBrowserDialog1.ShowDialog
        If IO.Directory.Exists(FolderBrowserDialog1.SelectedPath.ToString) = True Then My.Settings.Output = FolderBrowserDialog1.SelectedPath.ToString : My.Settings.Save()
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then CheckBox1.Checked = True : My.Settings.Burn = "Yes" : Exit Sub
        If CheckBox1.CheckState = CheckState.Unchecked Then CheckBox1.Checked = False : My.Settings.Burn = "No" : Exit Sub
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IO.File.Exists("C:\Program Files (x86)\ImgBurn\ImgBurn.exe") = True Then My.Settings.ImgburnLoc = "C:\Program Files (x86)\ImgBurn\ImgBurn.exe" : My.Settings.Save()
        If IO.File.Exists("C:\Program Files\ImgBurn\ImgBurn.exe") = True Then My.Settings.ImgburnLoc = "C:\Program Files\ImgBurn\ImgBurn.exe" : My.Settings.Save()
        If My.Settings.Burn = "Yes" Then CheckBox1.Checked = True
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.CheckState = CheckState.Checked Then
            For i = 0 To CheckedListBox1.Items.Count - 1
                CheckedListBox1.SetItemChecked(i, True)
            Next
            CheckBox2.Text = "None"
            Exit Sub
        End If
        If CheckBox2.CheckState = CheckState.Unchecked Then
            For i = 0 To CheckedListBox1.Items.Count - 1
                CheckedListBox1.SetItemChecked(i, False)
            Next
            CheckBox2.Text = "All"
        End If
    End Sub
End Class
