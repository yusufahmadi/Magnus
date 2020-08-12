
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

End Class
