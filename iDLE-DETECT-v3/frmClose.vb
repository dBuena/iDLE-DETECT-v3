Imports System.Threading, System.Xml
Imports System.Media.SystemSounds

Public Class frmClose
    Private TargetDT As DateTime
    Private CountDownFrom As TimeSpan = TimeSpan.FromMinutes(1)
    Private FlashLbl As Boolean = False
    Dim Root As String = AppDomain.CurrentDomain.BaseDirectory
#Region "System Sound"
    Sub PlaySystemSound()
        My.Computer.Audio.PlaySystemSound(
            System.Media.SystemSounds.Asterisk)
    End Sub
#End Region
    Private Sub FlashLabel()

        Dim myColor As Color = Color.Lime
        While FlashLbl

            If Label1.ForeColor.Equals(myColor) Then
                Label1.ForeColor = Color.Red
            Else
                Label1.ForeColor = Color.Lime
            End If

            Thread.Sleep(100)

        End While
    End Sub

    'STANDARD KILLING OF PROCESSES
    Private Sub DoSomething()
        Try
            For Each RunningProcess In Process.GetProcessesByName("maxthon")
                RunningProcess.Kill()
            Next
        Catch ex As Exception
        End Try

        Try
            For Each RunningProcess In Process.GetProcessesByName("crossfire")
                RunningProcess.Kill()
            Next
        Catch ex As Exception
        End Try

        Try
            For Each RunningProcess In Process.GetProcessesByName("iexplore")
                RunningProcess.Kill()
            Next
        Catch ex As Exception
        End Try

        Try
            For Each RunningProcess In Process.GetProcessesByName("steam")
                RunningProcess.Kill()
            Next
        Catch ex As Exception
        End Try

        Try
            For Each RunningProcess In Process.GetProcessesByName("chrome")
                RunningProcess.Kill()
            Next
        Catch ex As Exception
        End Try

        Try
            For Each RunningProcess In Process.GetProcessesByName("firefox")
                RunningProcess.Kill()
            Next
        Catch ex As Exception
        End Try

        Try
            For Each RunningProcess In Process.GetProcessesByName("mxnitro")
                RunningProcess.Kill()
            Next
        Catch ex As Exception
        End Try

        Try
            For Each RunningProcess In Process.GetProcessesByName("paladins")
                RunningProcess.Kill()
            Next
        Catch ex As Exception
        End Try

        Thread.Sleep(3000)
        tmrCloseApps.Stop()
        Form1.tmrIsActive.Start()
        Me.Close()
    End Sub

    'KILL PROCESS FRO XML LIST
    Sub KillProcess()

        Try
            Dim listxml As XDocument = XDocument.Load((Root + "list.xml"))

            For Each List As XElement In listxml.Descendants("lproc")
                Dim proc As String = List.Element("procx").Value

                'KILL BY PROCESSNAME DETECTION
                For Each rP In Process.GetProcessesByName(proc)
                    If rP.ProcessName.Contains(proc) Or rP.ProcessName.Contains(proc.ToLower) Then
                        rP.CloseMainWindow()
                        rP.Kill()
                    End If
                Next
            Next

            Thread.Sleep(3000)
            tmrCloseApps.Stop()
            Form1.tmrIsActive.Start()
            Me.Close()

        Catch ex As Exception
            MsgBox("KillProces()" & ex.Message)
        End Try



    End Sub

    Sub AddAutoStartRegistry()
        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run", _
    Application.ProductName, _
    Application.ExecutablePath)
    End Sub

    Private Sub frmClose_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            'STANDARD FORM LOAD
            'Me.Width = My.Computer.Screen.WorkingArea.Width
            '## CENTER BUTTON
            Dim x1, y1 As Integer
            x1 = btnCancel.Parent.Height \ 2 - btnCancel.Height \ 2
            y1 = btnCancel.Parent.Width \ 2 - btnCancel.Width \ 2
            btnCancel.Location = New Point(y1, 106)

            'DEFINE TIMERS
            TargetDT = DateTime.Now.Add(CountDownFrom)
            tmrCountDown.Start()
            tmrCloseApps.Interval = 60000
            tmrCloseApps.Start()

            'OUR ALERT MESSAGE
            FlashLbl = True
            Dim flashThread As New Thread(New ThreadStart(AddressOf FlashLabel))
            flashThread.Start()

            'PLAY SYSTEM SOUND WHEN AFK
            PlaySystemSound()

            'ADD REGISTRY STARTUP
            AddAutoStartRegistry()

        Catch ex As Exception

            'PERSISTENT: ON ERROR, RESTART
            Application.Restart()
        End Try

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            FlashLbl = False

            'tmrCountdown.Stop()
            Thread.Sleep(500)
            Form1.tmrIsActive.Start() 'restart the idle detection method
            Form1.picStatus.BackColor = Color.Lime
            Thread.Sleep(500)

            Me.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub tmrCountDown_Tick(sender As Object, e As EventArgs) Handles tmrCountDown.Tick
        Dim ts As TimeSpan = TargetDT.Subtract(DateTime.Now)
        If ts.TotalMilliseconds > 0 Then

            progMain.Value = ts.Seconds

            'PROGRESSBAR COLOR
            Select Case progMain.Value
                Case 40
                    progMain.ProgressColor = Color.GreenYellow
                Case 20
                    progMain.ProgressColor = Color.Yellow
                Case 10
                    progMain.ProgressColor = Color.Red
            End Select

            'CENTER FORM LOCATION
            Me.StartPosition = FormStartPosition.Manual
            Dim X As Integer = (Screen.PrimaryScreen.Bounds.Width - Me.Width) / 2
            Dim Y As Integer = (Screen.PrimaryScreen.Bounds.Height - Me.Height) / 2
            Me.StartPosition = FormStartPosition.Manual
            Me.Location = New System.Drawing.Point(X, 0)
            Me.TopMost = True

        Else
            tmrCountDown.Stop()
        End If
    End Sub

    Private Sub tmrCloseApps_Tick(sender As Object, e As EventArgs) Handles tmrCloseApps.Tick
        Static iMin As Integer
        iMin = iMin + 1
        If iMin = 1 Then '1 Minute

            'START KILLING PROCESSES
            bgMain.RunWorkerAsync()

            'KILLING PROCESSES THE OLD SCHOOL WAY
            DoSomething()


            Form1.picStatus.BackColor = Color.Lime
            iMin = 0

        End If
    End Sub

    Private Sub bgMain_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgMain.DoWork
        Try
            KillProcess()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bgMain_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgMain.ProgressChanged
        Try
            KillProcess()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bgMain_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgMain.RunWorkerCompleted
        Try
            Thread.Sleep(3000)
            tmrCloseApps.Stop()
            Form1.tmrIsActive.Start()
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub
End Class