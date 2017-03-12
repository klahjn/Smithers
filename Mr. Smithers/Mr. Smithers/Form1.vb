Imports System.IO
Imports System.Security
Imports System.Security.Cryptography
Public Class Form1
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim grab As String = FolderBrowserDialog1.ShowDialog()
        If FolderBrowserDialog1.SelectedPath <> "" Then
            For Each folder As String In System.IO.Directory.GetDirectories(FolderBrowserDialog1.SelectedPath)
                If IO.Directory.Exists(folder + "\PS3_GAME\") = True Then CheckedListBox1.Items.Add(folder)
            Next
            My.Settings.Source = FolderBrowserDialog1.SelectedPath.ToString
            My.Settings.Save()
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        For i = 0 To CheckedListBox1.Items.Count - 1
            CheckedListBox1.SelectedIndex = i
            If CheckedListBox1.GetItemChecked(i) = True Then
                Dim startInfo As New ProcessStartInfo
                If CheckBox4.Checked = False Then startInfo.FileName = Application.StartupPath + "\tools\mk.exe"
                If CheckBox4.Checked = True Then startInfo.FileName = Application.StartupPath + "\tools\gen20.exe"
                If CheckBox4.Checked = False Then startInfo.Arguments = " " + Chr(34) + CheckedListBox1.Text.ToString + Chr(34) + " " + Chr(34) + My.Settings.Output.ToString + "\" + GetFolderName(CheckedListBox1.Text.ToString) + ".iso" + Chr(34)
                If CheckBox4.Checked = True Then startInfo.Arguments = " " + Chr(34) + CheckedListBox1.Text.ToString + Chr(34) + " " + Chr(34) + My.Settings.Output.ToString + "\" + GetFolderName(CheckedListBox1.Text.ToString) + ".iso" + Chr(34)
                Dim process As Process = Process.Start(startInfo)
                process.WaitForExit()
            End If
        Next
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim grab As String = FolderBrowserDialog1.ShowDialog
        If IO.Directory.Exists(FolderBrowserDialog1.SelectedPath.ToString) = True Then My.Settings.Output = FolderBrowserDialog1.SelectedPath.ToString : My.Settings.Save()
        For Each file As String In System.IO.Directory.GetFiles(FolderBrowserDialog1.SelectedPath)
            If InStr(file, ".iso") Then CheckedListBox2.Items.Add(file)
        Next
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        If My.Settings.Deleter = "YES" Then CheckBox3.Checked = True
        If My.Settings.Drive <> "" Then ComboBox1.Text = My.Settings.Drive
        If My.Settings.Copies <> "" Then NumericUpDown1.Value = My.Settings.Copies
        If IO.File.Exists("C:\Program Files (x86)\ImgBurn\ImgBurn.exe") = True Then My.Settings.ImgburnLoc = "C:\Program Files (x86)\ImgBurn\ImgBurn.exe" : My.Settings.Save()
        If IO.File.Exists("C:\Program Files\ImgBurn\ImgBurn.exe") = True Then My.Settings.ImgburnLoc = "C:\Program Files\ImgBurn\ImgBurn.exe" : My.Settings.Save()
        For Each drive In DriveInfo.GetDrives()
            If drive.DriveType = DriveType.CDRom Then ComboBox1.Items.Add(drive.ToString())
        Next
        If My.Settings.Output = "" Then Exit Sub
        For Each file As String In System.IO.Directory.GetFiles(My.Settings.Output)
            If InStr(file, ".iso") Then CheckedListBox2.Items.Add(file)
        Next
        If My.Settings.Source = "" Then Exit Sub
        For Each folder As String In System.IO.Directory.GetDirectories(My.Settings.Source.ToString)
            If IO.Directory.Exists(folder + "\PS3_GAME\") = True Then CheckedListBox1.Items.Add(folder)
        Next
        Form2.Show()
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
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            For i = 0 To CheckedListBox2.Items.Count - 1
                CheckedListBox2.SetItemChecked(i, True)
            Next
            CheckBox1.Text = "None"
            Exit Sub
        End If
        If CheckBox1.CheckState = CheckState.Unchecked Then
            For i = 0 To CheckedListBox2.Items.Count - 1
                CheckedListBox2.SetItemChecked(i, False)
            Next
            CheckBox1.Text = "All"
        End If
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        For i = 0 To CheckedListBox2.Items.Count - 1
            CheckedListBox2.SelectedIndex = i
            If CheckedListBox2.GetItemChecked(i) = True Then
                Dim startInfo As New ProcessStartInfo
                startInfo.FileName = My.Settings.ImgburnLoc
                startInfo.Arguments = " /MODE WRITE /SRC " + Chr(34) + CheckedListBox2.Text.ToString + Chr(34) + " /DEST " + My.Settings.Drive.ToString + " /START /COPIES " + My.Settings.Copies.ToString + " /DELETEIMAGE " + My.Settings.Deleter.ToString
                Dim process As Process = Process.Start(startInfo)
                process.WaitForExit()
            End If
        Next
    End Sub
    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        My.Settings.Copies = NumericUpDown1.Value.ToString : My.Settings.Save()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        My.Settings.Drive = ComboBox1.SelectedItem.ToString : My.Settings.Save()
    End Sub
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.CheckState = 0 Then My.Settings.Deleter = "NO" : My.Settings.Save() : Exit Sub
        If CheckBox3.CheckState = 1 Then My.Settings.Deleter = "YES" : My.Settings.Save() : Exit Sub
    End Sub
    Private Sub Form1_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub
    Private Sub Form1_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each path In files
            If InStr(path, ".iso") Then CheckedListBox2.Items.Add(path.ToString) : GoTo gEntly
            If Not InStr(path, ".iso") Then CheckedListBox1.Items.Add(path.ToString) : GoTo gEntly
gEntly:
        Next
    End Sub
    Function GetFolderName(ByVal sDir As String) As String
        Dim gimp() = Split(sDir, "\")
        Return gimp(gimp.Length - 1)
    End Function

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim form As New Form2
        form.Show()
    End Sub
End Class
