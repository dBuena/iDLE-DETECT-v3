Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Text

Public Class Ini

    Private Declare Unicode Function GetPrivateProfileString Lib "kernel32" _
Alias "GetPrivateProfileStringW" (ByVal lpApplicationName As String,
    ByVal lpKeyName As String, ByVal lpDefault As String,
    ByVal lpReturnedString As String, ByVal nSize As Int32,
    ByVal lpFileName As String) As Int32

    Private Declare Unicode Function WritePrivateProfileString Lib "kernel32" _
    Alias "WritePrivateProfileStringW" (ByVal lpApplicationName As String,
    ByVal lpKeyName As String, ByVal lpString As String,
    ByVal lpFileName As String) As Int32

    Public Shared Sub INIDelete(ByVal INIPath As String, ByVal SectionName As String)
        Dim lpKeyName As String = Nothing
        Dim lpString As String = Nothing
        WritePrivateProfileString(SectionName, lpKeyName, lpString, INIPath)
    End Sub

    Public Shared Sub INIDelete(ByVal INIPath As String, ByVal SectionName As String, ByVal KeyName As String)
        Dim lpString As String = Nothing
        WritePrivateProfileString(SectionName, KeyName, lpString, INIPath)
    End Sub

    Public Shared Function INIRead(ByVal INIPath As String) As String
        Return INIRead(INIPath, Nothing, Nothing, "")
    End Function

    Public Shared Function INIRead(ByVal INIPath As String, ByVal SectionName As String) As String
        Return INIRead(INIPath, SectionName, Nothing, "")
    End Function

    Public Shared Function INIRead(ByVal INIPath As String, ByVal SectionName As String, ByVal KeyName As String) As String
        Return INIRead(INIPath, SectionName, KeyName, "")
    End Function

    Public Shared Function INIRead(ByVal INIPath As String, ByVal SectionName As String, ByVal KeyName As String, ByVal DefaultValue As String) As String
        Dim lpReturnedString As String = Strings.Space(2048)
        Dim length As Integer = GetPrivateProfileString(SectionName, KeyName, DefaultValue, lpReturnedString, lpReturnedString.Length, INIPath)
        If length > 0 Then
            Return lpReturnedString.Substring(0, length)
        End If
        Return ""
    End Function

    Public Shared Sub INIWrite(ByVal INIPath As String, ByVal SectionName As String, ByVal KeyName As String, ByVal TheValue As String)
        WritePrivateProfileString(SectionName, KeyName, TheValue, INIPath)
    End Sub

End Class

' how to write value
'Cls_Ini.INIWrite("C:\YourIniFile.ini", "Section0", "Key0","Value0")
'content
'[Section0]
'Key0=Value0

'how to read value
'Dim Val as string = Cls_Ini.INIRead("C:\YourIniFile.ini", "Section0","Key0")
