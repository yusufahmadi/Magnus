Imports DevExpress.XtraLayout
Imports DevExpress.XtraLayout.Helpers
Imports System.ComponentModel.DataAnnotations
Imports System.IO
Imports Magnus.Utils


Partial Public Class FormKaryawan
    Public _IsNew As Boolean
    Public _ID As Integer
    Public FormName As String = "Karyawan"
    Public TableName As String = "MKaryawan"
    Public Sub New()
        InitializeComponent()
    End Sub

    Sub LoadDataAdapter()
        'cbRoleUser.Properties.DataSource = Query.ExecuteDataSet("Select ID,Kode,Nama From MRoleUser Where IsActive=1").Tables(0)
        'cbRoleUser.Properties.DisplayMember = "Nama"
        'cbRoleUser.Properties.ValueMember = "ID"
    End Sub
    Private Sub FormBasic_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDataAdapter()
        Me.Text = FormName
        If _IsNew Then
            ClearData()
        Else
            Dim ds As New DataSet
            ds = Query.ExecuteDataSet("Select * from " & TableName & " WHere ID='" & Me._ID & "'")
            If Not ds Is Nothing Then
                With ds.Tables(0).Rows(0)
                    txtKode.Text = .Item("Kode").ToString
                    txtNama.Text = .Item("Nama").ToString
                    txtAlias.Text = .Item("Alias").ToString
                    txtKeterangan.Text = .Item("Keterangan").ToString
                    txtAlamat.Text = .Item("Alamat").ToString
                    txtAlamat2.Text = .Item("Alamat2").ToString
                    txtHP.Text = .Item("HP").ToString
                    ckIsActive.Checked = CBool(.Item("IsActive"))
                End With
                ds.Dispose()
            End If
        End If
    End Sub

    Private Sub bbiSave_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiSave.ItemClick
        If SaveData() Then
            _IsNew = False
            txtKode.Properties.ReadOnly = True
        End If
    End Sub

    Private Sub bbiSaveAndClose_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiSaveAndClose.ItemClick
        If SaveData() Then
            DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub bbiSaveAndNew_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiSaveAndNew.ItemClick
        If SaveData() Then
            Me._IsNew = True
            Me._ID = 0
            ClearData()
            Me.FormBasic_Load(sender, e)
        End If
    End Sub

    Sub ClearData()
        txtKode.Text = ""
        txtKode.Focus()
        txtNama.Text = ""
        txtAlias.Text = ""
        txtKeterangan.Text = ""
        txtAlamat.Text = ""
        txtAlamat2.Text = ""
        txtHP.Text = ""
        ckIsActive.Checked = True
    End Sub
    Function IsValid() As Boolean
        IsValid = True
        If txtKode.Text = "" Then
            txtKode.Focus()
            DxErrorProvider1.SetError(txtKode, "Kode harus di isi.")
            IsValid = False
            Exit Function
        End If
        If txtNama.Text = "" Then
            DxErrorProvider1.SetError(txtNama, "Nama harus di isi.")
            txtNama.Focus()
            IsValid = False
            Exit Function
        End If
        If Not CBool(ckIsActive.Checked) Then
            If DialogResult.No = MessageBox.Show("Set " & FormName & " Non Aktif", NamaAplikasi, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) Then
                ckIsActive.Focus()
                IsValid = False
                Exit Function
            End If
        End If
        Return IsValid
    End Function
    Function IsValidOnDB() As Boolean
        IsValidOnDB = True
        If CInt(Query.ExecuteScalar("Select Count(*) From " & TableName & " Where ID<> " & Me._ID & " AND Kode ='" & txtKode.Text & "'")) > 0 Then
            MessageBox.Show("Kode sudah ada.", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtKode.Focus()
            DxErrorProvider1.SetError(txtKode, "Kode sudah ada/terpakai.")
            IsValidOnDB = False
            Exit Function
        End If
        If CInt(Query.ExecuteScalar("Select Count(*) From " & TableName & " Where ID<> " & Me._ID & " AND Nama ='" & txtNama.Text & "'")) > 0 Then
            MessageBox.Show("Nama sudah ada.", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtNama.Focus()
            DxErrorProvider1.SetError(txtNama, "Nama sudah ada/terpakai.")
            IsValidOnDB = False
            Exit Function
        End If
        Return IsValidOnDB
    End Function
    Function SaveData() As Boolean
        Dim e As New Pesan With {.Hasil = False, .Message = "", .Value = Nothing}
        If IsValid() AndAlso IsValidOnDB() Then
            Dim sql As String = ""
            Try
                If _IsNew Then
                    sql = "Select Isnull(Max(ID),0)+1 From " & TableName
                    Me._ID = ObjToInt(Query.ExecuteScalar(sql))
                    sql = "Insert Into " & TableName & " (ID,Kode,Nama,Alias,Keterangan,Alamat,Alamat2,HP,IsActive) " & vbCrLf &
                          " Values (" & Me._ID & ",'" & FixApostropi(txtKode.Text.Trim) & "'," & vbCrLf &
                            " '" & FixApostropi(txtNama.Text.Trim) & "'," & vbCrLf &
                            " '" & FixApostropi(txtAlias.Text.Trim) & "'," & vbCrLf &
                            " '" & FixApostropi(txtKeterangan.Text.Trim) & "'," & vbCrLf &
                            " '" & FixApostropi(txtAlamat.Text.Trim) & "'," & vbCrLf &
                            " '" & FixApostropi(txtAlamat2.Text.Trim) & "'," & vbCrLf &
                            " '" & FixApostropi(txtHP.Text.Trim) & "'," & vbCrLf &
                            Utils.ObjToBit(ckIsActive.Checked) & ")"

                Else
                    sql = "Update " & TableName & " Set " & vbCrLf &
                            " Kode ='" & FixApostropi(txtKode.Text.Trim) & "',  " & vbCrLf &
                            " Nama ='" & FixApostropi(txtNama.Text.Trim) & "',  " & vbCrLf &
                            " Alias ='" & FixApostropi(txtAlias.Text.Trim) & "',  " & vbCrLf &
                            " Keterangan ='" & FixApostropi(txtKeterangan.Text.Trim) & "',  " & vbCrLf &
                            " Alamat ='" & FixApostropi(txtAlamat.Text.Trim) & "',  " & vbCrLf &
                            " Alamat2 ='" & FixApostropi(txtAlamat2.Text.Trim) & "',  " & vbCrLf &
                            " HP ='" & FixApostropi(txtHP.Text.Trim) & "',  " & vbCrLf &
                            " IsActive= " & Utils.ObjToBit(ckIsActive.Checked) & " " & vbCrLf &
                            " Where ID =" & Me._ID.ToString
                End If
                If sql <> "" Then
                    e = Query.Execute(sql)
                End If
                If Utils.ObjToBool(e.Hasil) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, "Data Tersimpan.", NamaAplikasi)
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, "Simpan gagal : " & e.Message, NamaAplikasi)
                End If
            Catch ex As Exception
                SaveData = False
            End Try
        End If
        Return e.Hasil
    End Function

    Private Sub bbiReset_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiReset.ItemClick
        Me.FormBasic_Load(sender, e)
    End Sub

    Private Sub bbiDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiDelete.ItemClick
        Dim f As Pesan = Query.DeleteDataMaster("MUser", "Username='" & txtKode.Text.Trim & "'")
        If f.Hasil = True Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
            DialogResult = DialogResult.OK
            Me.Close()
        Else
            DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
        End If
    End Sub

    Private Sub bbiClose_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiClose.ItemClick
        Me.Close()
    End Sub
End Class
