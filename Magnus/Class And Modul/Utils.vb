
Imports System.Net

Public Class Utils
    Public Shared Function ObjToInt(ByVal Obj As Object) As Integer
        Try
            ObjToInt = Convert.ToInt32(Obj)
        Catch ex As Exception
            ObjToInt = 0
        End Try
        Return ObjToInt
    End Function

    Public Shared Function ObjToLong(ByVal Obj As Object) As Long
        Try
            ObjToLong = Convert.ToInt64(Obj)
        Catch ex As Exception
            ObjToLong = 0
        End Try
        Return ObjToLong
    End Function
    Public Shared Function ObjToDbl(ByVal Obj As Object) As Double
        Try
            ObjToDbl = Convert.ToDouble(Obj)
        Catch ex As Exception
            ObjToDbl = 0
        End Try
        Return ObjToDbl
    End Function
    Public Shared Function ObjToBool(ByVal Obj As Object) As Boolean
        Try
            ObjToBool = Convert.ToBoolean(Obj)
        Catch ex As Exception
            ObjToBool = 0
        End Try
        Return ObjToBool
    End Function

    Public Shared Function ObjToBit(ByVal Obj As Object) As Short
        Try
            If Convert.ToBoolean(Obj) Then
                ObjToBit = 1
            Else
                ObjToBit = 0
            End If
        Catch ex As Exception
            ObjToBit = 0
        End Try
        Return ObjToBit
    End Function

    Public Shared Function ObjToDateTime(ByVal X As Object) As DateTime
        If TypeOf X Is Date OrElse IsDate(X) Then
            Return Convert.ToDateTime(X)
        Else
            Return Convert.ToDateTime("1/1/1900 00:00")
        End If
    End Function
    Public Shared Function ObjToDate(ByVal X As Object) As Date
        If TypeOf X Is Date OrElse IsDate(X) Then
            Return CDate(X)
        Else
            Return CDate("1/1/1900")
        End If
    End Function
    Public Shared Function ObjToStrDateSql(ByVal X As Object) As String
        If TypeOf X Is Date Then
            Return "'" & Format(CDate(X), "yyyy-MM-dd") & "'"
        Else
            Return "NULL"
        End If
    End Function

    Public Shared Function ObjToDateMDB(ByVal X As Object) As String
        If TypeOf X Is Date Then
            Return "#" & Format(CDate(X), "MM/dd/yyyy") & "#"
        Else
            Return "NULL"
        End If
    End Function

    Public Shared Function GetLocalIP() As String
        Dim result As String = ""
        Dim IPList As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName)
        For Each IPaddress In IPList.AddressList
            'Only return IPv4 routable IPs
            If (IPaddress.AddressFamily = Sockets.AddressFamily.InterNetwork) Then 'AndAlso (Not IsPrivateIP(IPaddress.ToString)) 
                If result.Trim = String.Empty Then
                    result = IPaddress.ToString
                Else
                    result = result & " / " & IPaddress.ToString
                End If
            End If
        Next
        Return result
    End Function

    Public Shared Function IsPrivateIP(ByVal CheckIP As String) As Boolean
        Dim Quad1, Quad2 As Integer

        Quad1 = CInt(CheckIP.Substring(0, CheckIP.IndexOf(".")))
        Quad2 = CInt(CheckIP.Substring(CheckIP.IndexOf(".") + 1).Substring(0, CheckIP.IndexOf(".")))
        Select Case Quad1
            Case 10
                Return True
            Case 172
                If Quad2 >= 16 And Quad2 <= 31 Then Return True
            Case 192
                If Quad2 = 168 Then Return True
        End Select
        Return False
    End Function

    Public Shared Function FixApostropi(ByVal obj As Object) As String
        Dim x As String = ""
        Try
            x = obj.ToString.Replace("'", "''")
        Catch ex As Exception
            x = ""
        End Try
        Return x
    End Function
    Public Shared Function FixKoma(ByVal obj As Object) As String
        Dim x As String = ""
        Try
            x = obj.ToString.Replace(",", ".")
        Catch ex As Exception
            x = ""
        End Try
        Return x
    End Function

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


End Class
