
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
End Class
