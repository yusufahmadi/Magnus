Imports DevExpress.XtraLayout
Imports DevExpress.XtraLayout.Helpers
Imports System.ComponentModel.DataAnnotations
Imports System.IO

Partial Public Class FormRoleUser
    Public _IsNew As Boolean
    Public _ID As Integer
    Public Sub New()
        InitializeComponent()
    End Sub
    Sub LoadDataAdapter()
        cbTypeLayout.Properties.DataSource = Query.ExecuteDataSet("Select ID,Kode,Nama From MTypeLayout Where IsActive=1").Tables(0)
        cbTypeLayout.Properties.DisplayMember = "Nama"
        cbTypeLayout.Properties.ValueMember = "ID"
    End Sub
    Private Sub FormUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDataAdapter()
        If _IsNew Then
            txtKode.Properties.ReadOnly = False
            ClearData()
        Else
            txtKode.Properties.ReadOnly = True
            Dim ds As New DataSet
            ds = Query.ExecuteDataSet("Select * from MRoleUser WHere ID='" & Me._ID & "'")
            If Not ds Is Nothing Then
                With ds.Tables(0).Rows(0)
                    txtKode.Text = .Item("Kode").ToString
                    txtNama.Text = .Item("Nama").ToString
                    txtKeterangan.Text = .Item("Keterangan").ToString
                    cbTypeLayout.EditValue = CInt(.Item("IDTypeLayout"))
                    ckIsActive.Checked = CBool(.Item("IsActive"))
                End With
                ds.Dispose()

                ds = Query.ExecuteDataSet("Select * from MRoleUserD WHere IDRoleUser='" & Me._ID & "'")
                If Not ds Is Nothing Then
                    GridControl1.DataSource = ds.Tables(0)
                    ds.Dispose()
                End If

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
            Me.FormUser_Load(sender, e)
        End If
    End Sub

    Sub ClearData()
        Me._ID = 0
        txtKode.Text = ""
        txtKode.Focus()
        txtNama.Text = ""
        txtKeterangan.Text = ""
        cbTypeLayout.EditValue = 0
        ckIsActive.Checked = False
        ckMenuSettingUser.Checked = False
    End Sub
    Function IsValid() As Boolean
        Dim res As Boolean = True
        If txtKode.Text = "" Then
            txtKode.Focus()
            res = False
        End If
        If txtNama.Text = "" Then
            txtNama.Focus()
            res = False
        End If
        If txtKeterangan.Text = "" Then
            txtKeterangan.Focus()
            res = False
        End If
        If CInt(cbTypeLayout.EditValue) <= 0 Then
            cbTypeLayout.Focus()
            res = False
        End If
        If Not CBool(ckIsActive.Checked) Then
            If DialogResult.No = MessageBox.Show("Set User Non Aktif", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) Then
                ckIsActive.Focus()
                res = False
            End If
        End If
        Return res
    End Function
    Function IsValidOnDB() As Boolean
        Dim res As Boolean = True
        If _IsNew AndAlso CInt(Query.ExecuteScalar("Select Count(*) From MRoleUser Where Kode ='" & txtKode.Text & "'")) > 0 Then
            MessageBox.Show("Kode sudah ada/terpakai.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtKode.Focus()
            res = False
        End If
        If _IsNew AndAlso CInt(Query.ExecuteScalar("Select Count(*) From MRoleUser Where Nama ='" & txtNama.Text & "'")) > 0 Then
            MessageBox.Show("Nama sudah ada/terpakai.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtKode.Focus()
            res = False
        End If
        Return res
    End Function
    Function SaveData() As Boolean
        Dim e As New Pesan With {.Hasil = False, .Message = "", .Value = Nothing}
        If IsValid() AndAlso IsValidOnDB() Then
            Dim sql As String = ""
            Try

                If _IsNew Then
                    'sql = "Insert Into MRoleUser (Username,Alias,Password,IsActive,IDRoleUser) " & vbCrLf &
                    '      " Values ('" & txtKode.Text.Trim & "'," & vbCrLf &
                    '        " '" & txtNama.Text.Trim & "'," & vbCrLf &
                    '        " '" & AES_Encrypt(txtKode.Text.Trim, "Kia") & "'," & vbCrLf &
                    '        Utils.ObjToInt(cbTypeLayout.EditValue) & "," & vbCrLf &
                    '        Utils.ObjToBit(ckIsActive.Checked) & ")"

                Else
                    'sql = "Update MRoleUser Set " & vbCrLf &
                    '        " Alias ='" & txtNama.Text.Trim & "',  " & vbCrLf &
                    '        " Password ='" & AES_Encrypt(txtKode.Text.Trim, "Kia") & "', " & vbCrLf &
                    '        " IsActive= " & Utils.ObjToInt(cbTypeLayout.EditValue) & ", " & vbCrLf &
                    '        " IDRoleUser =" & Utils.ObjToBit(ckIsActive.Checked) & vbCrLf &
                    '        " Where Username ='" & txtKode.Text.Trim & "'"
                End If
                If sql <> "" Then
                    e = Query.Execute(sql)
                End If
                If Utils.ObjToBool(e.Hasil) Then
                    MsgBox("Data Tersimpan.")
                Else
                    MsgBox("Simpan gagal : " & e.Message)
                End If
            Catch ex As Exception
                SaveData = False
            End Try
        End If
        Return e.Hasil
    End Function

    Private Sub bbiReset_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiReset.ItemClick
        Me.FormUser_Load(sender, e)
    End Sub

    Private Sub bbiDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiDelete.ItemClick
        If _IsNew Then
            Exit Sub
        End If
        Dim f As Pesan = Query.DeleteDataMaster("MRoleUser", "Kode='" & txtKode.Text.Trim & "'")
        If f.Hasil = True Then
            MsgBox(f.Message)
            DialogResult = DialogResult.OK
            Me.Close()
        Else
            MsgBox(f.Message)
        End If
    End Sub

    Private Sub bbiClose_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiClose.ItemClick
        Me.Close()
    End Sub
End Class
