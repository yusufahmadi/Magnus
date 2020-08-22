Public Class Akun
    Private _id As String
    Public Property ID() As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
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
    Private _level As Integer
    Public Property Level() As Integer
        Get
            Return _level
        End Get
        Set(ByVal value As Integer)
            _level = value
        End Set
    End Property
    Private _idjenisbukupembantu As Integer
    Public Property IDJenisBukuPembantu() As Integer
        Get
            Return _idjenisbukupembantu
        End Get
        Set(ByVal value As Integer)
            _idjenisbukupembantu = value
        End Set
    End Property
    Private _iskas As Boolean
    Public Property IsKas() As Boolean
        Get
            Return _iskas
        End Get
        Set(ByVal value As Boolean)
            _iskas = value
        End Set
    End Property
    Private _isactive As Boolean
    Public Property IsActive() As Boolean
        Get
            Return _isactive
        End Get
        Set(ByVal value As Boolean)
            _isactive = value
        End Set
    End Property
    Private _idparent As String
    Public Property IDParent() As String
        Get
            Return _idparent
        End Get
        Set(ByVal value As String)
            _idparent = value
        End Set
    End Property
    Private _idakunlv2 As String
    Public Property IDAkunLv2() As String
        Get
            Return _idakunlv2
        End Get
        Set(ByVal value As String)
            _idakunlv2 = value
        End Set
    End Property
End Class

Public Class AkunLv2
    Private _id As String
    Public Property ID() As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
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
    Private _isactive As Boolean
    Public Property IsActive() As Boolean
        Get
            Return _isactive
        End Get
        Set(ByVal value As Boolean)
            _isactive = value
        End Set
    End Property

    Private _detail As List(Of Akun)
    Public Property NewProperty() As List(Of Akun)
        Get
            Return _detail
        End Get
        Set(ByVal value As List(Of Akun))
            _detail = value
        End Set
    End Property
End Class

Public Class AkunLv1
    Private _id As String
    Public Property ID() As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
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
    Private _isactive As Boolean
    Public Property IsActive() As Boolean
        Get
            Return _isactive
        End Get
        Set(ByVal value As Boolean)
            _isactive = value
        End Set
    End Property

    Private _isdebet As Boolean
    Public Property IsDebet() As Boolean
        Get
            Return _isdebet
        End Get
        Set(ByVal value As Boolean)
            _isdebet = value
        End Set
    End Property

    Private _isneraca As Boolean
    Public Property IsNeraca() As Boolean
        Get
            Return _isneraca
        End Get
        Set(ByVal value As Boolean)
            _isneraca = value
        End Set
    End Property

    Private _detail As List(Of AkunLv2)
    Public Property NewProperty() As List(Of AkunLv2)
        Get
            Return _detail
        End Get
        Set(ByVal value As List(Of AkunLv2))
            _detail = value
        End Set
    End Property
End Class

Public Class Rekanan
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
    Private _alias As String
    Public Property Alias_() As String
        Get
            Return _alias
        End Get
        Set(ByVal value As String)
            _alias = value
        End Set
    End Property
    Private _keterangan As String
    Public Property Keterangan() As String
        Get
            Return _keterangan
        End Get
        Set(ByVal value As String)
            _keterangan = value
        End Set
    End Property
    Private _alamat As String
    Public Property Alamat() As String
        Get
            Return _alamat
        End Get
        Set(ByVal value As String)
            _alamat = value
        End Set
    End Property
    Private _alamat2 As String
    Public Property Alamat2() As String
        Get
            Return _alamat2
        End Get
        Set(ByVal value As String)
            _alamat2 = value
        End Set
    End Property
    Private _hp As String
    Public Property HP() As String
        Get
            Return _hp
        End Get
        Set(ByVal value As String)
            _hp = value
        End Set
    End Property
    Private _idjenisrekanan As Integer
    Public Property IDJenisRekanan() As Integer
        Get
            Return _idjenisrekanan
        End Get
        Set(ByVal value As Integer)
            _idjenisrekanan = value
        End Set
    End Property
    Private _isactive As Boolean
    Public Property IsActive() As Boolean
        Get
            Return _isactive
        End Get
        Set(ByVal value As Boolean)
            _isactive = value
        End Set
    End Property
End Class

Public Class HutangPiutang
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
    Private _kodereff As String
    Public Property KodeReff() As String
        Get
            Return _kodereff
        End Get
        Set(ByVal value As String)
            _kodereff = value
        End Set
    End Property

    Private _keterangan As String
    Public Property Keterangan() As String
        Get
            Return _keterangan
        End Get
        Set(ByVal value As String)
            _keterangan = value
        End Set
    End Property
    Private _nominal As Double
    Public Property Nominal() As Double
        Get
            Return _nominal
        End Get
        Set(ByVal value As Double)
            _nominal = value
        End Set
    End Property
    Private _kurs As Double
    Public Property Kurs() As Double
        Get
            Return _kurs
        End Get
        Set(ByVal value As Double)
            _kurs = value
        End Set
    End Property
    Private _idakun As String
    Public Property IDAkun() As String
        Get
            Return _idakun
        End Get
        Set(ByVal value As String)
            _idakun = value
        End Set
    End Property
    Private _idrekanan As Integer
    Public Property IDRekanan() As Integer
        Get
            Return _idrekanan
        End Get
        Set(ByVal value As Integer)
            _idrekanan = value
        End Set
    End Property
    Private _idreff As Integer
    Public Property IDReff() As Integer
        Get
            Return _idreff
        End Get
        Set(ByVal value As Integer)
            _idreff = value
        End Set
    End Property
    Private _Tgl As Date
    Public Property Tgl() As Date
        Get
            Return _Tgl
        End Get
        Set(ByVal value As Date)
            _Tgl = value
        End Set
    End Property
    Private _tgljt As Date
    Public Property TglJT() As Date
        Get
            Return _tgljt
        End Get
        Set(ByVal value As Date)
            _tgljt = value
        End Set
    End Property
    Private _idjenistransaksi As Integer
    Public Property IDJenisTransaksi() As Integer
        Get
            Return _idjenistransaksi
        End Get
        Set(ByVal value As Integer)
            _idjenistransaksi = value
        End Set
    End Property
    Private _idtransaksi As Integer
    Public Property IDTransaksi() As Integer
        Get
            Return _idtransaksi
        End Get
        Set(ByVal value As Integer)
            _idtransaksi = value
        End Set
    End Property
End Class