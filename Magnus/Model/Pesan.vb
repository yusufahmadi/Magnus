
Public Class Pesan
    Private Hasil_ As Boolean
    Private Message_ As String
    Private Value_ As Object

    Public Property Hasil() As Boolean
        Get
            Return Hasil_
        End Get
        Set(ByVal value As Boolean)
            Hasil_ = value
        End Set
    End Property

    Public Property Message() As String
        Get
            Return Message_
        End Get
        Set(ByVal value As String)
            Message_ = value
        End Set
    End Property

    Public Property Value() As Object
        Get
            Return Value_
        End Get
        Set(ByVal value As Object)
            Value_ = value
        End Set
    End Property
End Class