
Public Class Ini
    Public Shared appini As String = System.Windows.Forms.Application.StartupPath & "\System\Setting.ini"
#Region "API Calls"

    Private Declare Unicode Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringW" (ByVal lpApplicationName As String,
ByVal lpKeyName As String, ByVal lpString As String,
ByVal lpFileName As String) As Int32

    Private Declare Unicode Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringW" (ByVal lpApplicationName As String,
ByVal lpKeyName As String, ByVal lpDefault As String,
ByVal lpReturnedString As String, ByVal nSize As Int32,
ByVal lpFileName As String) As Int32

#End Region
#Region "INIRead Overloads"

    Private Overloads Function INIRead(ByVal INIPath As String,
ByVal SectionName As String, ByVal KeyName As String) As String

        Return INIRead(INIPath, SectionName, KeyName, "")

    End Function

    Private Overloads Function INIRead(ByVal INIPath As String,
ByVal SectionName As String) As String

        Return INIRead(INIPath, SectionName, Nothing, "")

    End Function

    Private Overloads Function INIRead(ByVal INIPath As String) As String
        Return INIRead(INIPath, Nothing, Nothing, "")
    End Function

#End Region
    Private Overloads Shared Function INIRead(ByVal INIPath As String,
    ByVal SectionName As String, ByVal KeyName As String,
    ByVal DefaultValue As String) As String
        Dim n As Int32
        Dim sData As String = Space$(1024)

        n = GetPrivateProfileString(SectionName, KeyName, DefaultValue,
    sData, sData.Length, INIPath)

        If n > 0 Then

            INIRead = sData.Substring(0, n)

        Else

            INIRead = ""

        End If

    End Function
    Private Shared Sub INIWrite(ByVal INIPath As String, ByVal SectionName As String,
ByVal KeyName As String, ByVal TheValue As String)

        Call WritePrivateProfileString(SectionName, KeyName, TheValue, INIPath)

    End Sub
    Private Overloads Sub INIDelete(ByVal INIPath As String, ByVal SectionName As String,
ByVal KeyName As String)

        Call WritePrivateProfileString(SectionName, KeyName, Nothing, INIPath)

    End Sub
    Private Overloads Sub INIDelete(ByVal INIPath As String, ByVal SectionName As String)
        Call WritePrivateProfileString(SectionName, Nothing, Nothing, INIPath)
    End Sub
    Public Shared Function BacaIni(ByVal Section As String, ByVal Kunci As String, ByVal IsiDefault As String) As String
        Dim Sisi As String = INIRead(appini, Section, Kunci, IsiDefault)
        Return Sisi

    End Function
    Public Shared Sub TulisIni(ByVal Section As String, ByVal Kunci As String, ByVal Datanya As String)
        INIWrite(appini, Section, Kunci, Datanya)

    End Sub
    Public Shared Function BacaIniPath(ByVal Path As String, ByVal Section As String, ByVal Kunci As String, ByVal IsiDefault As String) As String
        Dim Sisi As String = INIRead(Path, Section, Kunci, IsiDefault)
        Return Sisi
    End Function
    Public Shared Sub TulisIniPath(ByVal Path As String, ByVal Section As String, ByVal Kunci As String, ByVal Datanya As String)
        INIWrite(Path, Section, Kunci, Datanya)

    End Sub
End Class
