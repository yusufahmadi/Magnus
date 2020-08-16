Imports DevExpress.XtraLayout
Imports DevExpress.XtraLayout.Helpers
Imports System.ComponentModel.DataAnnotations
Imports System.Data.SqlClient
Imports System.IO
Imports Magnus.Utils

Partial Public Class FormBarang
    Public _IsNew As Boolean
    Public _ID As Integer
    Public Sub New()
        InitializeComponent()
    End Sub
    Sub LoadDataAdapter()
        txtIDKategori.Properties.DataSource = Query.ExecuteDataSet("Select ID,Kode,Nama,IsActive From MKategori ").Tables(0)
        txtIDKategori.Properties.DisplayMember = "Nama"
        txtIDKategori.Properties.ValueMember = "ID"

        txtIDSatuanTerbesar.Properties.DataSource = Query.ExecuteDataSet("Select ID,Kode,Nama,IsActive From MSatuan ").Tables(0)
        txtIDSatuanTerbesar.Properties.DisplayMember = "Nama"
        txtIDSatuanTerbesar.Properties.ValueMember = "ID"

        txtIDSatuanTerkecil.Properties.DataSource = Query.ExecuteDataSet("Select ID,Kode,Nama,IsActive From MSatuan ").Tables(0)
        txtIDSatuanTerkecil.Properties.DisplayMember = "Nama"
        txtIDSatuanTerkecil.Properties.ValueMember = "ID"

        repo_cbIDSatuan.DataSource = Query.ExecuteDataSet("Select ID,Kode,Nama,IsActive From MSatuan ").Tables(0)
        repo_cbIDSatuan.DisplayMember = "Nama"
        repo_cbIDSatuan.ValueMember = "ID"
    End Sub
    Sub LoadLayoutByRole()
        If IDRoleUser = 1 Then
            LayoutControlItemHargaBeli.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            LayoutControlItemProsenUp.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            txtHargaJual.Properties.ReadOnly = False
        Else
            'WAJIB DISABLE
            LayoutControlItemHargaBeli.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            LayoutControlItemProsenUp.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            txtHargaJual.Properties.ReadOnly = True
        End If
        'Not Ready
        LayoutControlItemP.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LayoutControlItemL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LayoutControlItemT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LayoutControlItemIDSatuanTerbesar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LayoutControlItemIsi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LayoutControlItemIDSatuanTerkecil.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        'Simple View
        Me.Width = 450
        LayoutControlItemGVD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub
    Private Sub FormUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDataAdapter()
        LoadLayoutByRole()
        If _IsNew Then
            txtKode.Properties.ReadOnly = False
            ClearData()
        Else
            txtKode.Properties.ReadOnly = True
            Dim ds As New DataSet
            ds = Query.ExecuteDataSet("Select * from MBarang WHere ID='" & Me._ID & "'")
            If Not ds Is Nothing Then
                With ds.Tables(0).Rows(0)
                    txtIDKategori.EditValue = CInt(.Item("IDKategori"))
                    txtKode.EditValue = .Item("Kode").ToString
                    txtNama.EditValue = .Item("Nama").ToString
                    txtCatatan.EditValue = .Item("Catatan").ToString
                    txtP.EditValue = .Item("P").ToString
                    txtL.EditValue = .Item("L").ToString
                    txtT.EditValue = .Item("T").ToString
                    txtIDSatuanTerbesar.EditValue = CInt(.Item("IDSatuanTerbesar"))
                    txtIsi.EditValue = CInt(.Item("Isi"))
                    txtIDSatuanTerkecil.EditValue = CInt(.Item("IDSatuanTerkecil"))
                    txtHargaBeli.EditValue = ObjToDbl(.Item("HargaBeli"))
                    txtProsenUp.EditValue = ObjToDbl(.Item("ProsenUp"))
                    txtHargaJual.EditValue = ObjToDbl(.Item("HargaJual"))

                    txtUserBuat.EditValue = .Item("UserBuat").ToString
                    txtTanggalBuat.EditValue = ObjToDate(.Item("TanggalBuat"))
                    txtUserUbah.EditValue = .Item("UserUpdate").ToString
                    txtTanggalUbah.EditValue = ObjToDate(.Item("TanggalUpdate"))

                    ckIsActive.Checked = CBool(.Item("IsActive"))
                    ckIsNonStok.Checked = CBool(.Item("IsNonStok"))
                End With
                ds.Dispose()

                ds = Query.ExecuteDataSet("Select A.* from MBarangD A WHere IDBarang='" & Me._ID & "'")
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
        txtIDKategori.EditValue = 0
        txtKode.Text = ""
        txtKode.Focus()
        txtNama.Text = ""
        txtCatatan.Text = ""
        txtP.Text = ""
        txtL.Text = ""
        txtT.Text = ""
        txtIDSatuanTerbesar.EditValue = 1
        txtIsi.EditValue = 1
        txtIDSatuanTerkecil.EditValue = 1
        txtHargaBeli.EditValue = 0.0
        txtProsenUp.EditValue = 0.0
        txtHargaJual.EditValue = 0.0
        txtUserBuat.Text = ""
        txtUserUbah.Text = ""
        txtTanggalBuat.Text = ""
        txtTanggalUbah.Text = ""
        ckIsActive.Checked = True
        ckIsNonStok.Checked = False

        GridControl1.DataSource = Nothing
    End Sub
    Function IsValid() As Boolean
        Dim res As Boolean = True
        If txtKode.Text = "" Then
            txtKode.Focus()
            res = False
            Exit Function
        End If
        If txtNama.Text = "" Then
            txtNama.Focus()
            res = False
            Exit Function
        End If
        If CInt(txtIDKategori.EditValue) <= 0 Then
            txtIDKategori.Focus()
            res = False
            Exit Function
        End If
        If Not CBool(ckIsActive.Checked) Then
            If DialogResult.No = MessageBox.Show("Set Barang Non Aktif", NamaAplikasi, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) Then
                ckIsActive.Focus()
                res = False
                Exit Function
            End If
        End If

        'Defaultkan
        txtIsi.EditValue = IIf(ObjToInt(txtIsi.EditValue) < 1, 1, ObjToInt(txtIsi.EditValue))
        If ObjToInt(txtIsi.EditValue) = 1 Then
            If ObjToInt(txtIDSatuanTerbesar.EditValue) < 1 Then
                txtIDSatuanTerbesar.EditValue = 1
                Exit Function
            End If
            If ObjToInt(txtIDSatuanTerkecil.EditValue) < 1 Then
                txtIDSatuanTerkecil.EditValue = 1
                Exit Function
            End If
        End If
        Return res
    End Function
    Function IsValidOnDB() As Boolean
        Dim res As Boolean = True
        If CInt(Query.ExecuteScalar("Select Count(*) From MBarang Where ID<>" & Me._ID & " And Kode ='" & txtKode.Text & "'")) > 0 Then
            MessageBox.Show("Kode sudah ada/terpakai.", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtKode.Focus()
            res = False
        End If
        If CInt(Query.ExecuteScalar("Select Count(*) From MBarang Where ID<>" & Me._ID & " And Nama ='" & txtNama.Text & "'")) > 0 Then
            MessageBox.Show("Nama sudah ada/terpakai.", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtKode.Focus()
            res = False
        End If
        Return res
    End Function

    Private Function SaveData() As Boolean
        Dim pesan As New Pesan With {.Hasil = False, .Value = Nothing, .Message = ""}
        Dim sql As String = ""
        Dim IDBarangD As Integer = 0
        If IsValid() AndAlso IsValidOnDB() Then
            Using con As New SqlConnection(conStr)
                Using com As New SqlCommand
                    Try
                        com.Connection = con
                        con.Open()
                        com.Transaction = con.BeginTransaction
                        If _IsNew Then
                            com.CommandText = "Select Isnull(Max(ID),0)+1 From [MBarang]"
                            Me._ID = ObjToInt(com.ExecuteScalar)
                            sql = "Insert Into MBarang ([ID],[IDKategori]
                                    ,[Kode],[Nama],[Catatan]
                                    ,[P],[L],[T]
                                    ,[IDSatuanTerbesar],[Isi],[IDSatuanTerkecil]
                                    ,[HargaBeli],[ProsenUp],[HargaJual]
                                    ,[UserBuat],[TanggalBuat],[TanggalUpdate]
                                    ,[UserUpdate],[IsActive],[IsNonStok]) " & vbCrLf &
                                    " Values (" & Me._ID & "," & vbCrLf &
                                    ObjToInt(txtIDKategori.EditValue) & "," & vbCrLf &
                                    " '" & txtKode.Text.Trim & "'," & vbCrLf &
                                    " '" & txtNama.Text.Trim & "'," & vbCrLf &
                                    " '" & txtCatatan.Text.Trim & "'," & vbCrLf &
                                    " '" & txtP.Text.Trim & "'," & vbCrLf &
                                    " '" & txtL.Text.Trim & "'," & vbCrLf &
                                    " '" & txtT.Text.Trim & "'," & vbCrLf &
                                    ObjToInt(txtIDSatuanTerbesar.EditValue) & "," & vbCrLf &
                                    ObjToInt(txtIsi.EditValue) & "," & vbCrLf &
                                    ObjToInt(txtIDSatuanTerkecil.EditValue) & "," & vbCrLf &
                                    ObjToDbl(txtHargaBeli.EditValue) & "," & vbCrLf &
                                    ObjToDbl(txtProsenUp.EditValue) & "," & vbCrLf &
                                    ObjToDbl(txtHargaJual.EditValue) & "," & vbCrLf &
                                    "'" & Username & "'," & vbCrLf &
                                    "GetDate() ," & vbCrLf &
                                    "Null ," & vbCrLf &
                                    "Null ," & vbCrLf &
                                    ObjToBit(ckIsActive.Checked) & "," & vbCrLf &
                                    ObjToBit(ckIsNonStok.Checked) & ")"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            com.CommandText = "Delete MBarangD Where IDBarang=" & Me._ID
                            com.ExecuteNonQuery()

                            'Auto + Satuan Terkecil
                            com.CommandText = "Select Isnull(Max(ID),0)+1 From [MBarangD]"
                            IDBarangD = ObjToInt(com.ExecuteScalar)
                            sql = " INSERT INTO [MBarangD]
                                            ([ID],[IDBarang]
                                            ,[IDSatuan],[Barcode]
                                            ,[Isi],[ProsenUp]
                                            ,[HargaJual],[UserBuat]
                                            ,[TanggalBuat],[UserUpdate]
                                            ,[TanggalUpdate]) " & vbCrLf &
                                  " Select " & IDBarangD & " ID," & Me._ID & " As IDBarang, " & vbCrLf &
                                            ObjToInt(txtIDSatuanTerkecil.EditValue) & " As IDSatuan," & vbCrLf &
                                            "'" & txtKode.Text & "' As Barcode," & vbCrLf &
                                            "1 As Isi," & vbCrLf &
                                            ObjToInt(txtProsenUp.EditValue) & " As ProsenUp," & vbCrLf &
                                            ObjToInt(txtHargaJual.EditValue) & " As HargaJual," & vbCrLf &
                                            "'" & Username & "' As UserBuat," & vbCrLf &
                                            "GetDate() As TanggalBuat," & vbCrLf &
                                            "Null UserUpdate," & vbCrLf &
                                            "Null As TanggalUpdate "
                            com.CommandText = sql
                            com.ExecuteNonQuery()
                        Else
                            sql = "UPDATE MBarang SET " & vbCrLf &
                                    " [IDKategori] =" & ObjToInt(txtIDKategori.EditValue) & "," & vbCrLf &
                                    " [Kode] = '" & txtKode.Text.Trim & "'," & vbCrLf &
                                    " [Nama] = '" & txtNama.Text.Trim & "'," & vbCrLf &
                                    " [Catatan] = '" & txtCatatan.Text.Trim & "'," & vbCrLf &
                                    " [P] = '" & txtP.Text.Trim & "'," & vbCrLf &
                                    " [L] = '" & txtL.Text.Trim & "'," & vbCrLf &
                                    " [T] = '" & txtT.Text.Trim & "'," & vbCrLf &
                                    " [IDSatuanTerbesar] =" & ObjToInt(txtIDSatuanTerbesar.EditValue) & "," & vbCrLf &
                                    " [Isi] = " & ObjToInt(txtIsi.EditValue) & "," & vbCrLf &
                                    " [IDSatuanTerkecil] = " & ObjToInt(txtIDSatuanTerkecil.EditValue) & "," & vbCrLf &
                                    " [HargaBeli] = " & ObjToDbl(txtHargaBeli.EditValue) & "," & vbCrLf &
                                    " [ProsenUp] = " & ObjToDbl(txtProsenUp.EditValue) & "," & vbCrLf &
                                    " [HargaJual] = " & ObjToDbl(txtHargaJual.EditValue) & "," & vbCrLf &
                                    " [TanggalUpdate] = GetDate() ," & vbCrLf &
                                    " [UserUpdate] = '" & Username & "'," & vbCrLf &
                                    " [IsActive] = " & ObjToBit(ckIsActive.Checked) & "," & vbCrLf &
                                    " [IsNonStok] = " & ObjToBit(ckIsNonStok.Checked) & "" & vbCrLf &
                                "Where [ID]=" & Me._ID
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            'Detail Dari Header
                            sql = "UPDATE MBarangD SET " & vbCrLf &
                                " [IDSatuan] = " & ObjToInt(txtIDSatuanTerkecil.EditValue) & "," & vbCrLf &
                                " [Barcode] = '" & txtKode.Text.Trim & "'," & vbCrLf &
                                " [ProsenUp] = " & ObjToDbl(txtProsenUp.EditValue) & "," & vbCrLf &
                                " [HargaJual] = " & ObjToDbl(txtHargaJual.EditValue) & "," & vbCrLf &
                                " [TanggalUpdate] = GetDate() ," & vbCrLf &
                                " [UserUpdate] = '" & Username & "'" & vbCrLf &
                                " Where [IDBarang]=" & Me._ID & " AND [Barcode] = '" & txtKode.Text.Trim & "' And Isi=1"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            'Detail Lainya
                            If GridView1.RowCount > 0 Then
                                For i As Integer = 0 To GridView1.RowCount - 1
                                    If txtKode.Text <> GridView1.GetRowCellValue(i, "Barcode").ToString Then
                                        If ObjToInt(GridView1.GetRowCellValue(i, "ID")) < 1 Then
                                            com.CommandText = "Select Isnull(Max(ID),0)+1 From [MBarangD]"
                                            IDBarangD = ObjToInt(com.ExecuteScalar)
                                            sql = " INSERT INTO [MBarangD]
                                                        ([ID],[IDBarang]
                                                        ,[IDSatuan],[Barcode]
                                                        ,[Isi],[ProsenUp]
                                                        ,[HargaJual],[UserBuat]
                                                        ,[TanggalBuat],[UserUpdate]
                                                        ,[TanggalUpdate]) " & vbCrLf &
                                                " Values (" & IDBarangD & "," & vbCrLf &
                                                        " @IDBarang," & vbCrLf &
                                                        " @IDSatuan," & vbCrLf &
                                                        " @Barcode," & vbCrLf &
                                                        " @Isi," & vbCrLf &
                                                        " @ProsenUp," & vbCrLf &
                                                        " @HargaJual," & vbCrLf &
                                                        " @Username," & vbCrLf &
                                                        " GetDate()," & vbCrLf &
                                                        " Null," & vbCrLf &
                                                        " Null)"
                                        Else
                                            sql = "UPDATE MBarangD SET " & vbCrLf &
                                                " [IDBarang] = @IDBarang
                                                  ,[IDSatuan] = @IDSatuan
                                                  ,[Barcode] = @Barcode
                                                  ,[Isi] = @Isi
                                                  ,[ProsenUp] = @ProsenUp
                                                  ,[HargaJual] = @HargaJual
                                                  ,[UserUpdate] = @Username
                                                  ,[TanggalUpdate] = GetDate() " & vbCrLf &
                                                " WHERE [ID]=@ID"
                                        End If
                                        com.CommandText = sql
                                        com.Parameters.AddWithValue("@ID", ObjToInt(GridView1.GetRowCellValue(i, "ID")))
                                        com.Parameters.AddWithValue("@IDBarang", Me._ID)
                                        com.Parameters.AddWithValue("@IDSatuan", ObjToInt(GridView1.GetRowCellValue(i, "IDSatuan")))
                                        com.Parameters.AddWithValue("@Barcode", FixApostropi(GridView1.GetRowCellValue(i, "Barcode")))
                                        com.Parameters.AddWithValue("@Isi", ObjToInt(GridView1.GetRowCellValue(i, "Isi")))
                                        com.Parameters.AddWithValue("@ProsenUp", ObjToDbl(GridView1.GetRowCellValue(i, "ProsenUp")))
                                        com.Parameters.AddWithValue("@HargaJual", ObjToDbl(GridView1.GetRowCellValue(i, "HargaJual")))
                                        com.Parameters.AddWithValue("@Username", Username)

                                        com.ExecuteNonQuery()
                                        com.Parameters.Clear()
                                    End If
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
        Dim f As Pesan = Query.DeleteDataMaster("MBarang", "ID=" & Me._ID & "")
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

    Private Sub txtHargaBeli_EditValueChanged(sender As Object, e As EventArgs) Handles txtHargaBeli.EditValueChanged

    End Sub

    Private Sub txtHargaBeli_LostFocus(sender As Object, e As EventArgs) Handles txtHargaBeli.LostFocus
        If ObjToDbl(txtHargaBeli.EditValue) = 0 Then
            txtProsenUp.EditValue = 0
        Else
            txtHargaJual.EditValue = ObjToDbl(txtHargaBeli.EditValue) * (1 + (ObjToDbl(txtProsenUp.EditValue) / 100))
        End If
    End Sub

    Private Sub txtProsenUp_EditValueChanged(sender As Object, e As EventArgs) Handles txtProsenUp.EditValueChanged

    End Sub

    Private Sub txtProsenUp_LostFocus(sender As Object, e As EventArgs) Handles txtProsenUp.LostFocus
        If ObjToDbl(txtHargaBeli.EditValue) = 0 OrElse ObjToDbl(txtProsenUp.EditValue) = 0 Then
            Exit Sub
        Else
            txtHargaJual.EditValue = ObjToDbl(txtHargaBeli.EditValue) * (1 + (ObjToDbl(txtProsenUp.EditValue) / 100))
        End If
    End Sub

    Private Sub txtHargaJual_EditValueChanged(sender As Object, e As EventArgs) Handles txtHargaJual.EditValueChanged

    End Sub

    Private Sub txtHargaJual_LostFocus(sender As Object, e As EventArgs) Handles txtHargaJual.LostFocus
        If ObjToDbl(txtHargaBeli.EditValue) = 0 OrElse ObjToDbl(txtHargaJual.EditValue) = 0 Then
            Exit Sub
        Else
            txtProsenUp.EditValue = (100 * (ObjToDbl(txtHargaJual.EditValue) / ObjToDbl(txtHargaBeli.EditValue))) - 100
        End If
    End Sub
End Class
