Class MainWindow
    Public Sub MapDrive(ByVal DriveLetter As String, ByVal UNCPath As String, ByVal strUsername As String, ByVal strPassword As String)
        Dim p As New Process()
        p.StartInfo.FileName = "net.exe"
        p.StartInfo.Arguments = " use " & DriveLetter & ": " & UNCPath & " " & strPassword & " /USER:" & strUsername
        p.StartInfo.CreateNoWindow = True
        p.Start()
        p.WaitForExit()
    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

    End Sub

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        MapDrive("Z:", "ftp://192.168.31.82/dev_hdd0/", "anonymous", "anonymous")
    End Sub
End Class
