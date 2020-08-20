Public Class Barang
    Private _id As Integer
    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property
    Private _kode As String
    Public Property Kode() As String
        Get
            Return _kode
        End Get
        Set(ByVal value As String)
            _kode = value
        End Set
    End Property
    Private _nama As String
    Public Property Nama() As String
        Get
            Return _nama
        End Get
        Set(ByVal value As String)
            _nama = value
        End Set
    End Property

    Private _satuanTerkecil As String
    Public Property SatuanTerkecil() As String
        Get
            Return _satuanTerkecil
        End Get
        Set(ByVal value As String)
            _satuanTerkecil = value
        End Set
    End Property

End Class
