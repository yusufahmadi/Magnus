Public Class MenuRoleUser
    Private _id As Integer
    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property
    Private _namaForm As String
    Public Property NamaForm() As String
        Get
            Return _namaForm
        End Get
        Set(ByVal value As String)
            _namaForm = value
        End Set
    End Property
    Private _caption As String
    Public Property Caption() As String
        Get
            Return _caption
        End Get
        Set(ByVal value As String)
            _caption = value
        End Set
    End Property
    Private _isActive As Boolean
    Public Property IsActive() As Boolean
        Get
            Return _isActive
        End Get
        Set(ByVal value As Boolean)
            _isActive = value
        End Set
    End Property

    Private _isEnable As Boolean
    Public Property IsEnable() As Boolean
        Get
            Return _isEnable
        End Get
        Set(ByVal value As Boolean)
            _isEnable = value
        End Set
    End Property
    Private _isBaru As Boolean
    Public Property IsBaru() As Boolean
        Get
            Return _isBaru
        End Get
        Set(ByVal value As Boolean)
            _isBaru = value
        End Set
    End Property
    Private _isUbah As Boolean
    Public Property IsUbah() As Boolean
        Get
            Return _isUbah
        End Get
        Set(ByVal value As Boolean)
            _isUbah = value
        End Set
    End Property
    Private _isHapus As Boolean
    Public Property IsHapus() As Boolean
        Get
            Return _isHapus
        End Get
        Set(ByVal value As Boolean)
            _isHapus = value
        End Set
    End Property

    Private _isCetak As Boolean
    Public Property IsCetak() As Boolean
        Get
            Return _isCetak
        End Get
        Set(ByVal value As Boolean)
            _isCetak = value
        End Set
    End Property

    Private _isExport As Boolean
    Public Property IsExport() As Boolean
        Get
            Return _isExport
        End Get
        Set(ByVal value As Boolean)
            _isExport = value
        End Set
    End Property
    Private _typeLayoutD As Integer
    Public Property IDTypeLayoutD() As Integer
        Get
            Return _typeLayoutD
        End Get
        Set(ByVal value As Integer)
            _typeLayoutD = value
        End Set
    End Property
End Class
