Imports DevExpress.XtraLayout
Imports DevExpress.XtraLayout.Helpers
Imports System.ComponentModel.DataAnnotations
Imports System.Data.SqlClient
Imports System.IO
Imports Magnus.Utils

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

        repo_cbTypeLayout.DataSource = Query.ExecuteDataSet("Select ID,Kode,Nama From MTypeLayout Where IsActive=1").Tables(0)
        repo_cbTypeLayout.DisplayMember = "Nama"
        repo_cbTypeLayout.ValueMember = "ID"
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

                ds = Query.ExecuteDataSet("Select B.NamaForm,A.* from MRoleUserD A Inner Join MMenu B On A.IDMenu=B.ID WHere IDRoleUser='" & Me._ID & "'")
                If Not ds Is Nothing Then
                    GridControl1.DataSource = ds.Tables(0)
                    ds.Dispose()
                    GridControl1.Refresh()

                    GridView1.OptionsView.ColumnAutoWidth = False
                    GridView1.OptionsView.BestFitMaxRowCount = -1
                    GridView1.BestFitColumns()
                End If

            End If
        End If
    End Sub
    Private Sub bbiSave_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiSave.ItemClick
        If Utils.ObjToBool(SaveData()) Then
            _IsNew = False
            txtKode.Properties.ReadOnly = True
            Me.FormUser_Load(sender, e)
        End If
    End Sub

    Private Sub bbiSaveAndClose_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiSaveAndClose.ItemClick
        If Utils.ObjToBool(SaveData()) Then
            DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub bbiSaveAndNew_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiSaveAndNew.ItemClick
        If Utils.ObjToBool(SaveData()) Then
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
        GridControl1.DataSource = Nothing
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
            If DialogResult.No = MessageBox.Show("Set Role Non Aktif", AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) Then
                ckIsActive.Focus()
                res = False
            End If
        End If
        Return res
    End Function
    Function IsValidOnDB() As Boolean
        Dim res As Boolean = True
        If CInt(Query.ExecuteScalar("Select Count(*) From MRoleUser Where ID<>" & Me._ID & " And Kode ='" & txtKode.Text & "'")) > 0 Then
            MessageBox.Show("Kode sudah ada/terpakai.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtKode.Focus()
            res = False
        End If
        If CInt(Query.ExecuteScalar("Select Count(*) From MRoleUser Where ID<>" & Me._ID & " And Nama ='" & txtNama.Text & "'")) > 0 Then
            MessageBox.Show("Nama sudah ada/terpakai.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtKode.Focus()
            res = False
        End If
        Return res
    End Function

    Private Function SaveData() As Boolean
        Dim pesan As New Pesan With {.Hasil = False, .Value = Nothing, .Message = ""}
        Dim sql As String = ""
        If IsValid() AndAlso IsValidOnDB() Then
            Using con As New SqlConnection(conStr)
                Using com As New SqlCommand
                    Try
                        com.Connection = con
                        con.Open()
                        com.Transaction = con.BeginTransaction
                        If _IsNew Then
                            com.CommandText = "Select Isnull(Max(ID),0)+1 From [MRoleUser]"
                            Me._ID = ObjToInt(com.ExecuteScalar)
                            sql = "Insert Into MRoleUser ([ID]
                                ,[Kode]
                                ,[Nama]
                                ,[Keterangan]
                                ,[IsActive]
                                ,[IDTypeLayout]
                                ,[MenuSettingUser]) " & vbCrLf &
                                    " Values (" & Me._ID & ",'" & txtKode.Text.Trim & "'," & vbCrLf &
                                    " '" & txtNama.Text.Trim & "'," & vbCrLf &
                                    " '" & txtKeterangan.Text.Trim & "'," & vbCrLf &
                                    ObjToBit(ckIsActive.Checked) & "," & vbCrLf &
                                    ObjToInt(cbTypeLayout.EditValue) & "," & vbCrLf &
                                    ObjToBit(ckMenuSettingUser.Checked) & ")"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            com.CommandText = "Delete MRoleUserD Where IDRoleUser=" & Me._ID
                            com.ExecuteNonQuery()

                            sql = " INSERT INTO [MRoleUserD]
                                            ([IDRoleUser],[IDMenu],[IsEnable]
                                            ,[IsBaru],[IsUbah],[IsHapus],[IsCetak],IsExport
                                            ,[IDTypeLayout]) " & vbCrLf &
                                  " Select " & Me._ID & " As IDRoleUser,
                                       ID As IDMenu,IsActive IsEnable,IsActive IsBaru,
                                       IsActive IsUbah,IsActive IsHapus,IsActive IsCetak,IsActive IsExport," &
                                           ObjToInt(cbTypeLayout.EditValue) & " IDTypeLayout  " & vbCrLf &
                                  "From MMenu"
                            com.CommandText = sql
                            com.ExecuteNonQuery()
                        Else
                            sql = "Update MRoleUser Set" & vbCrLf &
                                  "[Kode] ='" & txtKode.Text.Trim & "',
                              [Nama] ='" & txtNama.Text.Trim & "',
                              [Keterangan] ='" & txtKeterangan.Text.Trim & "',
                              [IsActive] = " & ObjToBit(ckIsActive.Checked) & ",
                              [IDTypeLayout] = " & ObjToInt(cbTypeLayout.EditValue) & ",
                              [MenuSettingUser] = " & ObjToBit(ckMenuSettingUser.Checked) & " " & vbCrLf &
                                  "Where [ID]=" & Me._ID
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            com.CommandText = "UPDATE [MRoleUserD]
                                                   SET [IsEnable] = @IsEnable
                                                      ,[IsBaru] = @IsBaru
                                                      ,[IsUbah] = @IsUbah
                                                      ,[IsHapus] = @IsHapus
                                                      ,[IsCetak] = @IsCetak
                                                      ,[IsExport] = @IsExport
                                                      ,[IDTypeLayout] = @IDTypeLayout
                                                       WHERE  [IDRoleUser] = " & Me._ID & " And
                                                      [IDMenu] = @IDMenu"
                            If GridView1.RowCount > 0 Then
                                For i As Integer = 0 To GridView1.RowCount - 1
                                    com.Parameters.AddWithValue("@IDMenu", ObjToInt(GridView1.GetRowCellValue(i, "IDMenu")))
                                    com.Parameters.AddWithValue("@IsEnable", ObjToBit(GridView1.GetRowCellValue(i, "IsEnable")))
                                    com.Parameters.AddWithValue("@IsBaru", ObjToBit(GridView1.GetRowCellValue(i, "IsBaru")))
                                    com.Parameters.AddWithValue("@IsUbah", ObjToBit(GridView1.GetRowCellValue(i, "IsUbah")))
                                    com.Parameters.AddWithValue("@IsHapus", ObjToBit(GridView1.GetRowCellValue(i, "IsHapus")))
                                    com.Parameters.AddWithValue("@IsCetak", ObjToBit(GridView1.GetRowCellValue(i, "IsCetak")))
                                    com.Parameters.AddWithValue("@IsExport", ObjToBit(GridView1.GetRowCellValue(i, "IsExport")))
                                    If ObjToInt(GridView1.GetRowCellValue(i, "TypeLayout")) = 0 Then
                                        com.Parameters.AddWithValue("@IDTypeLayout", ObjToInt(cbTypeLayout.EditValue))
                                    Else
                                        com.Parameters.AddWithValue("@IDTypeLayout", ObjToInt(GridView1.GetRowCellValue(i, "TypeLayout")))
                                    End If
                                    com.ExecuteNonQuery()
                                    com.Parameters.Clear()
                                Next
                            End If
                        End If
                        com.Transaction.Commit()
                        With pesan
                            .Hasil = True
                            .Message = "Simpan Berhasil."
                            .Value = ""
                        End With
                        MsgBox(pesan.Message)
                    Catch ex As Exception
                        com.Transaction.Rollback()
                        With pesan
                            .Hasil = False
                            .Message = "Simpan Gagal." & ex.Message
                            .Value = ""
                        End With
                        MsgBox(pesan.Message)
                    Finally
                        com.Transaction = Nothing
                    End Try
                End Using
            End Using
        End If
        Return pesan.Hasil
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
