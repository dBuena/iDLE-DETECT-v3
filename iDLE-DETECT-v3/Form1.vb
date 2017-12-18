Imports System.Threading, System.Xml
Imports System.Text, System.IO, System.Net

Public Class Form1
    Dim Root As String = AppDomain.CurrentDomain.BaseDirectory
    'CHANGE THE SERVER LOCATION TO YOUR HOSTING SERVER
    Dim server = "http://ww1.gamehax.in/"
    Dim curVer, localversion, updatefilename As String
    Dim dlError As Boolean

    Private _inactiveTimeRetriever As cIdleTimeStool
    Private Const CP_NOCLOSE_BUTTON As Integer = &H200

    'DISABLE CLOSE BUTTON
    Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim myCp As CreateParams = MyBase.CreateParams
            myCp.ClassStyle = myCp.ClassStyle Or CP_NOCLOSE_BUTTON
            Return myCp
        End Get
    End Property

#Region "XML DETECTION"
    Sub DetectKeyword()
        Dim serverXml As XDocument = XDocument.Load((Root + "list.xml"))

        For Each list As XElement In serverXml.Descendants("list")
            Dim kword As String = list.Element("kword").Value


            'KILL BY WINDOW NAME DETECTION
            Dim o As New Process
            For Each o In Process.GetProcesses(".")
                If o.MainWindowTitle.Contains(kword) Or o.MainWindowTitle.Contains(kword.ToLower) Then
                    o.CloseMainWindow()
                    o.Kill()

                End If
            Next
        Next
        picStatus.BackColor = Color.Lime
    End Sub

    Sub DetectProcess()
        Dim listxml As XDocument = XDocument.Load((Root + "list.xml"))

        For Each List As XElement In listxml.Descendants("plist")
            Dim proc As String = List.Element("proc").Value

            'KILL BY PROCESSNAME DETECTION
            For Each rP In Process.GetProcessesByName(proc)
                If rP.ProcessName.Contains(proc) Or rP.ProcessName.Contains(proc.ToLower) Then
                    rP.CloseMainWindow()
                    rP.Kill()
                End If
            Next
        Next

    End Sub

#End Region

    Function DeleteFile(ByVal f As String) As Boolean
        Try
            File.Delete(f)
            Return True
        Catch ex As Exception
            'Return False
            MsgBox(ex.Message)
        End Try
    End Function

    Sub GetFiles()
        Try

            'checks client version
            Dim lclVersion As String = Ini.INIRead(Root & "ver.cfg", "Version", "local")
            Dim localVersion As Decimal = Decimal.Parse(lclVersion)

            'server's list of updates
            Dim serverXml As XDocument = XDocument.Load((server + "Updates.xml"))

            'The Update Process
            For Each update As XElement In serverXml.Descendants("update")
                Dim version As String = update.Element("version").Value
                Dim file As String = update.Element("file").Value
                updatefilename = file
                Dim serverVersion As Decimal = Decimal.Parse(version)
                Dim sUrlToReadFileFrom As String = (server + file)
                Dim sFilePathToWriteFileTo As String = (Root + file)

                'cleanup / delete unfinished files
                DeleteFile(file)

                'compare the version & download missing updates
                If serverVersion > localVersion Then
                    Dim url As New Uri(sUrlToReadFileFrom)
                    Dim request As System.Net.HttpWebRequest = CType(System.Net.WebRequest.Create(url), System.Net.HttpWebRequest)
                    Dim response As System.Net.HttpWebResponse = CType(request.GetResponse(), System.Net.HttpWebResponse)
                    response.Close()

                    Dim iSize As Int64 = response.ContentLength

                    Dim iRunningByteTotal As Int64 = 0

                    Using client As New System.Net.WebClient()
                        Using streamRemote As System.IO.Stream = client.OpenRead(New Uri(sUrlToReadFileFrom))
                            Using streamLocal As Stream = New FileStream(sFilePathToWriteFileTo, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None)
                                Dim iByteSize As Integer = 0
                                Dim byteBuffer(iSize - 1) As Byte
                                iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)
                                Do While iByteSize > 0


                                    streamLocal.Write(byteBuffer, 0, iByteSize)
                                    iRunningByteTotal += iByteSize

                                    Dim dIndex As Double = CDbl(iRunningByteTotal)
                                    Dim dTotal As Double = CDbl(byteBuffer.Length)
                                    Dim dProgressPercentage As Double = (dIndex / dTotal)
                                    Dim iProgressPercentage As Integer = CInt(Math.Truncate(dProgressPercentage * 100))

                                    bgDownloader.ReportProgress(iProgressPercentage)
                                    iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)

                                Loop

                                streamLocal.Close()
                            End Using

                            streamRemote.Close()
                        End Using

                    End Using

                    '## EXTRACTING ##

                    Dim upFile As String = Application.StartupPath & file
                    Dim proc As New Process


                    proc.StartInfo.FileName = file 'upFile
                    proc.StartInfo.Arguments = "-y -gm2 -InstallPath=" & Application.StartupPath & " > Nul & Del "
                    proc.StartInfo.UseShellExecute = False
                    proc.StartInfo.RedirectStandardError = True
                    proc.StartInfo.CreateNoWindow = True
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    proc.Start()


                    proc.WaitForExit()

                    Thread.Sleep(1000) 'wait 1sec before deleting update

                    '################

                    'Update ver.cfg with our downloaded version
                    Ini.INIWrite(Root & "ver.cfg", "Version", "local", serverVersion)

                    'Delete Zip File
                    DeleteFile(file)

                End If

            Next

        Catch ex As Exception
            MsgBox(ex.Message)
            dlError = True
        End Try

    End Sub

    Sub CheckCFG()
        If Not File.Exists(Root & "ver.cfg") Then
            Ini.INIWrite(Root & "ver.cfg", "Version", "local", "1.0")
        Else
            'passes the local value to curVer (predefined variable)
            curVer = Ini.INIRead(Root & "ver.cfg", "Version", "local")
        End If
    End Sub

    Sub AddAutoStartRegistry()
        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run", _
    Application.ProductName, _
    Application.ExecutablePath)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'DOWNLOAD DIFINITION UPDATES
            bgDownloader.RunWorkerAsync()

            _inactiveTimeRetriever = New cIdleTimeStool

            With tmrIsActive
                .Interval = 60000
                .Start()
            End With

            'WRITE OUR INITIAL LOCAL VERSION
            CheckCFG()

            AddAutoStartRegistry()


        Catch ex As Exception

            'RESTART THE APP WHEN ERROR IS ENCOUNTERED
            'TO MAKE THIS APP PERSISTENT
            Application.Restart()
        End Try
    End Sub

    Private Sub bgDetectBlackList_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgDetectBlackList.DoWork

        'TRYING TO SET THE CPU LOAD TO LOWEST
        Thread.CurrentThread.Priority = ThreadPriority.Lowest

        'THREAD SLEEPING ALSO HELPS LOWERING CPU LOAD
        'HOWEVER, DOING THIS WILL SLOW THE DETECTION RATE
        'AS LONG AS IT WORKS, IT WORKS! XD
        DetectKeyword()
        Thread.Sleep(30000)
        'WAITING 30 SECONDS
        'SO 1 MINUTE IN TOTAL
        'WE CAN MAKE FASTER DETECTIONS 
        'IF WE HAVE 2 BACKGROUND WORKERS RUNNING SIMULTANEOUSLY
        DetectProcess()
        Thread.Sleep(30000)

    End Sub

    Private Sub bgDetectBlackList_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgDetectBlackList.ProgressChanged
        Thread.CurrentThread.Priority = ThreadPriority.Lowest

        DetectKeyword()

        DetectProcess()

    End Sub

    Private Sub bgDetectBlackList_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgDetectBlackList.RunWorkerCompleted
        Thread.CurrentThread.Priority = ThreadPriority.Lowest
        bgDetectBlackList.RunWorkerAsync()
    End Sub

    Private Sub tmrIsActive_Tick(sender As Object, e As EventArgs) Handles tmrIsActive.Tick
        Dim inactiveTime = _inactiveTimeRetriever.GetInactiveTime

        Static iMin As Integer
        iMin = iMin + 1

        'CHECK IDLE STATE EVERY 5 MINUTES
        If iMin = 5 Then
            If (inactiveTime Is Nothing) Then
                picStatus.BackColor = Color.Yellow

            ElseIf (inactiveTime.Value.TotalSeconds > 20) Then
                picStatus.BackColor = Color.Red

                'close process if inactive for 5 minutes
                frmClose.Show()

                tmrIsActive.Stop()
            Else

                picStatus.BackColor = Color.Lime
            End If

            iMin = 0
        End If
    End Sub

    Private Sub bgDownloader_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgDownloader.DoWork
        GetFiles()
    End Sub

    Private Sub bgDownloader_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgDownloader.RunWorkerCompleted
        Try
            If dlError = False Then
                'START THE DETECTION POCESS
                'SINCE THE XML HAS BEEN UPDATED
                bgDetectBlackList.RunWorkerAsync()
            Else
                'MAKE PERSISTENT IN CASE DOWNLOAD WAS INTERRUPTED
                'Application.Restart()

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
