Imports System.Threading, System.Xml
Imports System.Text, System.IO, System.Net

Public Class Form1
    Dim localfile As String = Application.StartupPath & "\"

#Region "XML DETECTIONS"

    Sub DetectKeyword()
        Dim serverXml As XDocument = XDocument.Load((localfile + "list.xml"))

        For Each list As XElement In serverXml.Descendants("list")
            Dim kword As String = list.Element("kword").Value


            'KILL BY WINDOW NAME DETECTION
            Dim o As New Process
            For Each o In Process.GetProcesses(".")
                If o.MainWindowTitle.Contains(kword) Or o.MainWindowTitle.Contains(kword.ToLower) Then
                    o.CloseMainWindow()
                    o.CloseMainWindow()
                    o.Kill()
                    ' MsgBox("Detected: " & kword, vbCritical)
                End If
            Next
        Next
    End Sub

    Sub DetectProcess()
        Dim listxml As XDocument = XDocument.Load((localfile + "list.xml"))

        For Each List As XElement In listxml.Descendants("plist")
            Dim proc As String = List.Element("proc").Value

            'KILL BY PROCESSNAME DETECTION
            For Each rP In Process.GetProcessesByName(proc)
                If rP.ProcessName.Contains(proc) Or rP.ProcessName.Contains(proc.ToLower) Then
                    rP.CloseMainWindow()
                    rP.Kill()
                    'MsgBox("Detected: " & rP.ProcessName & ".exe", vbCritical)

                    rP.CloseMainWindow()
                    rP.Kill()

                End If
            Next
        Next
    End Sub

#End Region

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

        Catch ex As Exception

            'RESTART THE APP WHEN ERROR IS ENCOUNTERED
            'TO MAKE THIS APP PERSISTENT
            Application.Restart()
        End Try
    End Sub
End Class
