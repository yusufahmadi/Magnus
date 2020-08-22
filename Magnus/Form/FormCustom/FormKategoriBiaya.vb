Imports DevExpress.XtraLayout
Imports DevExpress.XtraLayout.Helpers
Imports System.ComponentModel.DataAnnotations
Imports System.IO
Imports Magnus.Utils


Partial Public Class FormKategoriBiaya
    Public _IsNew As Boolean
    Public _ID As String = ""
    Public FormName As String = "Kategori Biaya"
    Public TableName As String = "MAkun"
    Public KlasifikasiAkun As String = "0601" 'Biaya
    Public Sub New()
        InitializeComponent()

    End Sub

    Sub LoadDataAdapter()
        Try
            txtIDAkunLv2.Properties.DataSource = Query.ExecuteDataSet("Select ID,Nama From MAkunLv2 Where IsActive=1").Tables(0)
            txtIDAkunLv2.Properties.DisplayMember = "Nama"
            txtIDAkunLv2.Properties.ValueMember = "ID"
            txtIDAkunLv2.EditValue = "0601"

            txtParent.Properties.DataSource = Query.ExecuteDataSet("Select ID,Nama From MAkun Where IsActive=1").Tables(0)
            txtParent.Properties.DisplayMember = "Nama"
            txtParent.Properties.ValueMember = "ID"
            txtParent.EditValue = "0"
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(Me, ex.Message, NamaAplikasi)
        End Try
    End Sub
    Private Sub FormBasic_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDataAdapter()
        Me.Text = FormName
        If _IsNew Then
            ClearData()
        Else
            Dim ds As New DataSet
            Dim sql As String = "Select * from " & TableName & " Where Left(IDAkunLv2,4)='" & KlasifikasiAkun & "' AND ID='" & Me._ID & "'"
            ds = Query.ExecuteDataSet(sql)
            If Not ds Is Nothing Then
                With ds.Tables(0).Rows(0)
                    Me._ID = NullToStr(.Item("ID"))
                    txtIDAkunLv2.EditValue = NullToStr(.Item("IDAkunLv2"))
                    txtParent.EditValue = NullToStr(.Item("IDParent"))
                    txtKode.Text = NullToStr(.Item("ID"))
                    txtNama.Text = NullToStr(.Item("Nama"))
                    txtKeterangan.Text = NullToStr(.Item("Keterangan"))
                    ckIsActive.Checked = ObjToBool(.Item("IsActive"))
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
        txtKeterangan.Text = ""
        ckIsActive.Checked = True
    End Sub
    Function IsValid() As Boolean
        IsValid = True
        'If txtKode.Text = "" Then
        '    txtKode.Focus()
        '    IsValid = False
        '    Exit Function
        'End If
        If txtNama.Text = "" Then
            txtNama.Focus()
            IsValid = False
            Exit Function
        End If
        'If txtKeterangan.Text = "" Then
        '    txtKeterangan.Focus()
        '    IsValid = False
        '    Exit Function
        'End If
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
        'If CInt(Query.ExecuteScalar("Select Count(*) From " & TableName & " Where ID<> '" & Me._ID & "' AND Kode ='" & txtKode.Text & "'")) > 0 Then
        '    MessageBox.Show("Kode sudah ada.", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    txtKode.Focus()
        '    IsValidOnDB = False
        '    Exit Function
        'End If
        If CInt(Query.ExecuteScalar("Select Count(*) From " & TableName & " Where ID<> '" & Me._ID & "' AND Nama ='" & txtNama.Text & "'")) > 0 Then
            MessageBox.Show("Nama sudah ada.", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtKode.Focus()
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
                    sql = "Select Isnull(Max(Substring(ID," & NullToStr(txtIDAkunLv2.EditValue).Length + 1 & ",2)),0)+1 From " & TableName & " Where LEFT(ID," & NullToStr(txtIDAkunLv2.EditValue).Length & ")= '" & NullToStr(txtIDAkunLv2.EditValue) & "'"
                    Me._ID = NullToStr(txtIDAkunLv2.EditValue) & Format(ObjToInt(Query.ExecuteScalar(sql)), "00")
                    sql = "Insert Into " & TableName & " (ID,IDAkunLv2,IDParent,Nama,Keterangan,IsActive,LevelPerkiraan) " & vbCrLf &
                          " Values ('" & Me._ID & "','" & NullToStr(txtIDAkunLv2.EditValue) & "','" & NullToStr(txtParent.EditValue) & "'," & vbCrLf &
                            " '" & txtNama.Text.Trim & "'," & vbCrLf &
                            " '" & txtKeterangan.Text.Trim & "'," & vbCrLf &
                            Utils.ObjToBit(ckIsActive.Checked) & "," & LevelPerkiraan & ")"

                Else
                    sql = "Update " & TableName & " Set " & vbCrLf &
                            " IDAkunLv2 ='" & NullToStr(txtIDAkunLv2.EditValue) & "',  " & vbCrLf &
                            " IDParent ='" & NullToStr(txtParent.EditValue) & "',  " & vbCrLf &
                            " Nama ='" & FixApostropi(txtNama.Text).Trim & "',  " & vbCrLf &
                            " Keterangan ='" & FixApostropi(txtKeterangan.Text).Trim & "',  " & vbCrLf &
                            " IsActive= " & Utils.ObjToBit(ckIsActive.Checked) & " " & vbCrLf &
                            " Where ID ='" & Me._ID & "'"
                End If
                If sql <> "" Then
                    e = Query.Execute(sql)
                End If
                If Utils.ObjToBool(e.Hasil) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me,"Data Tersimpan.")
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me,"Simpan gagal : " & e.Message)
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
        Dim f As Pesan = Query.DeleteDataMaster(TableName, "ID='" & Me._ID & "'")
        If f.Hasil = True Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Me,f.Message)
            DialogResult = DialogResult.OK
            Me.Close()
        Else
            DevExpress.XtraEditors.XtraMessageBox.Show(Me,f.Message)
        End If
    End Sub

    Private Sub BbiClose_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiClose.ItemClick
        Me.Close()
    End Sub

    Private Sub txtKode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtKode.KeyDown
        If e.KeyCode = Keys.F2 AndAlso txtKode.Properties.ReadOnly Then
            Using x As New FormOtorisasi
                If x.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                    txtKode.Properties.ReadOnly = False
                End If
            End Using
        End If
    End Sub
End Class
