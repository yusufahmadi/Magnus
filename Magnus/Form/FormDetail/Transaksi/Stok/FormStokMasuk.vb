Imports System.Data.SqlClient
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports Magnus.Utils

Public Class FormStokMasuk
    Public _IsNew As Boolean
    Public _ID As Integer
    Public KodeDepan As String = "SM"
    Public TableMaster As String = "StokMasuk"
    Private qty As Double = 0.0
    Private amount As Double = 0.0
    Private vat As Double = 0.0
    Private price As Double = 0.0
    Private list As List(Of Barang) = Nothing
    Public Sub New()
        InitializeComponent()
    End Sub


    Dim repckedit As New RepositoryItemCheckEdit
    Dim repdateedit As New RepositoryItemDateEdit
    Dim reptextedit As New RepositoryItemTextEdit
    Dim reppicedit As New RepositoryItemPictureEdit
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        LoadItems()
        If _IsNew Then
            ClearData()
            txtTgl.EditValue = ObjToDate(Query.ExecuteScalar("Select GetDate()")).ToString("yyyy-MM-dd")
            txtKode.Text = GlobalFunc.GetKodeTransaksi(txtTgl.DateTime, KodeDepan, TableMaster).Value.ToString
            txtKode.Properties.ReadOnly = False
        Else
            Dim ds As New DataSet
            Sql = "Select * From " & TableMaster & " T WHere T.ID=" & Me._ID & ""
            ds = Query.ExecuteDataSet(Sql)
            If Not ds Is Nothing Then
                With ds.Tables(0).Rows(0)
                    txtTgl.EditValue = ObjToDate(.Item("Tgl")).ToString("yyyy-MM-dd")
                    txtKode.Properties.ReadOnly = True
                    txtKode.Text = .Item("Kode").ToString
                    txtKeterangan.Text = .Item("Keterangan").ToString
                End With
                ds.Dispose()
            End If
        End If
        Refresher()
    End Sub
    Dim Sql As String = ""

    Sub Refresher()
        Dim ds As New DataSet
        Dim tblGrid As String = "[Order Details]"
        Try
            Sql = "Select TD.ID ,TD.NoUrut, TD.IDBarang ,MB.Nama NamaBarang,MS.Kode Unit,TD.Qty,TD.Harga,TD.Jumlah,TD.Catatan 
                    From " & TableMaster & " T Inner Join " & TableMaster & "D TD on T.ID=TD.[ID" & TableMaster & "] 
                    Inner Join MBarang MB on MB.ID=TD.IDBarang 
                    Left Join MSatuan MS On MS.ID=MB.IDSatuanTerkecil
                    Where T.ID= " & Me._ID
            ds = Query.ExecuteDataSet(Sql, tblGrid)
            Dim dvManager As DataViewManager = New DataViewManager(ds)
            Dim dv As DataView = dvManager.CreateDataView(ds.Tables(tblGrid))

            If Not ds Is Nothing Then
                GC1.DataSource = dv
                ds.Dispose()
                GC1.Refresh()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadItems()
        Dim ds As DataSet = New DataSet()
        Dim tblLookUp As String = "Products"
        Using d As New WaitDialogForm("Loading Order Details...")
            'Refresher()
            d.SetCaption("Loading Products...")
            Sql = "SELECT MB.ID ID,MB.Kode,MB.Nama,MS.Kode Satuan  " &
                               " From MBarang MB Left Join MSatuan MS on MS.ID=MB.IDSatuanTerkecil " &
                               " Where MB.IsActive=1 ORDER BY MB.Nama ASC"
            ds = Query.ExecuteDataSet(Sql, tblLookUp)

            Dim myData = ds.Tables(tblLookUp).AsEnumerable().[Select](Function(r) New Barang With {
                    .ID = r.Field(Of Integer)("ID"),
                    .Kode = r.Field(Of String)("Kode"),
                    .Nama = r.Field(Of String)("Nama"),
                    .SatuanTerkecil = r.Field(Of String)("Satuan")
                })
            list = myData.ToList()
            If Not ds Is Nothing Then
                RepositoryItemSearchLookUpEdit1.DataSource = list 'ds.Tables(tblLookUp)
                RepositoryItemSearchLookUpEdit1.ValueMember = "ID"
                RepositoryItemSearchLookUpEdit1.DisplayMember = "Kode"
                ds.Dispose()
            End If
            RepositoryItemSearchLookUpEdit1.NullText = "Pilih Barang"
            GColIDBarang.ColumnEdit = RepositoryItemSearchLookUpEdit1
        End Using
    End Sub

    Private Sub gridView1_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GV1.CellValueChanged
        If e.Column.FieldName = "IDBarang" Then
            Dim value = GV1.GetRowCellValue(e.RowHandle, e.Column)
            Dim dt = list.AsEnumerable.FirstOrDefault(Function(x) x.ID = Utils.ObjToInt(value))

            If dt IsNot Nothing Then
                If Utils.ObjToInt(GV1.GetFocusedRowCellValue(GColID)) < 1 Then
                    GV1.SetRowCellValue(e.RowHandle, "ID", -1)
                End If
                GV1.SetRowCellValue(e.RowHandle, "NamaBarang", dt.Nama)
                GV1.SetRowCellValue(e.RowHandle, "Unit", dt.SatuanTerkecil)
                GV1.SetRowCellValue(e.RowHandle, "Harga", 1)
                GV1.SetRowCellValue(e.RowHandle, "NoUrut", GV1.RowCount)

                If Utils.NullToStr(GV1.GetFocusedRowCellValue(GColQty)) = "" Then
                    qty = 0.0
                Else
                    qty = Utils.ObjToDbl(GV1.GetFocusedRowCellValue(GColQty))
                    price = Utils.ObjToDbl(GV1.GetFocusedRowCellValue(GColHarga))
                    amount = qty * price
                    GV1.SetFocusedRowCellValue(GColJumlah, amount)
                    'vat = amount / 10
                    'gridView1.SetFocusedRowCellValue(colVat, vat)
                End If
            End If
        End If

        If e.Column.Name = GColQty.Name Then
            qty = Utils.ObjToDbl(GV1.GetFocusedRowCellValue(GColQty))
            price = Utils.ObjToDbl(GV1.GetFocusedRowCellValue(GColHarga))
            amount = qty * price
            GV1.SetFocusedRowCellValue(GColJumlah, amount)
            'vat = amount / 10
            'gridView1.SetFocusedRowCellValue(colVat, vat)
        End If
    End Sub

    Private Sub bbiSave_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiSave.ItemClick
        If DialogResult.No = XtraMessageBox.Show(Me, "Yakin ingin menyimpan ?", NamaAplikasi, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) Then
            Exit Sub
        End If
        If Utils.ObjToBool(SaveData()) Then
            _IsNew = False
            txtKode.Properties.ReadOnly = True
            Me.Form1_Load(sender, e)
        End If
    End Sub

    Private Sub bbiSaveAndClose_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiSaveAndClose.ItemClick
        If DialogResult.No = XtraMessageBox.Show(Me, "Yakin ingin menyimpan dan menutup entrian ini ?", NamaAplikasi, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) Then
            Exit Sub
        End If
        If Utils.ObjToBool(SaveData()) Then
            DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub bbiSaveAndNew_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiSaveAndNew.ItemClick
        If DialogResult.No = XtraMessageBox.Show(Me, "Yakin ingin menyimpan dan membuat entrian Baru ?", NamaAplikasi, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) Then
            Exit Sub
        End If
        If Utils.ObjToBool(SaveData()) Then
            Me._IsNew = True
            Me._ID = 0
            ClearData()
            Me.Form1_Load(sender, e)
        End If
    End Sub

    Sub ClearData()
        Me._ID = 0
        txtKode.Text = ""
        txtKode.Focus()
        txtKeterangan.Text = ""
        GC1.DataSource = Nothing
    End Sub
    Function IsValid() As Boolean
        IsValid = True

        'Ketika Input di Grid masih focus di rownya maka akan di cancel solusi di focuskan ke yang lain
        txtKode.Focus()

        If txtKode.Text = "" Then
            txtKode.Focus()
            DxErrorProvider1.SetError(txtKode, "Kode tidak boleh kosong")
            IsValid = False
            Exit Function
        End If
        'If txtNama.Text = "" Then
        '    txtNama.Focus()
        '    IsValid = False
        '    Exit Function
        'End If
        If txtKeterangan.Text = "" Then
            txtKeterangan.Focus()
            DxErrorProvider1.SetError(txtKeterangan, "isi Keterangan minimal 1 karakter")
            IsValid = False
            Exit Function
        End If
        'If CInt(cbTypeLayout.EditValue) <= 0 Then
        '    cbTypeLayout.Focus()
        '    IsValid = False
        '    Exit Function
        'End If
        'If Not CBool(ckIsActive.Checked) Then
        '    If DialogResult.No = MessageBox.Show("Set Role Non Aktif", NamaAplikasi, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) Then
        '        ckIsActive.Focus()
        '        IsValid = False
        '        Exit Function
        '    End If
        'End If
        Return IsValid
    End Function

    Function IsValidOnDB() As Boolean
        IsValidOnDB = True
        If CInt(Query.ExecuteScalar("Select Count(*) From " & TableMaster & " Where ID<>" & Me._ID & " And Kode ='" & txtKode.Text & "'")) > 0 Then
            If DialogResult.Yes = XtraMessageBox.Show("Kode sudah ada/terpakai. Perbarui Kode Otomatis ?", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information) Then
                txtKode.Text = GlobalFunc.GetKodeTransaksi(txtTgl.DateTime, KodeDepan, TableMaster).Value.ToString
            Else
                txtKode.Focus()
                DxErrorProvider1.SetError(txtKeterangan, "Kode sudah ada/terpakai.")
                IsValidOnDB = False
                Exit Function
            End If
        End If
        'If CInt(Query.ExecuteScalar("Select Count(*) From MRoleUser Where ID<>" & Me._ID & " And Nama ='" & txtNama.Text & "'")) > 0 Then
        '    MessageBox.Show("Nama sudah ada/terpakai.", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    txtKode.Focus()
        '    IsValidOnDB = False
        '    Exit Function
        'End If
        Return IsValidOnDB
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
                            com.CommandText = "Select Isnull(Max(ID),0)+1 From [" & TableMaster & "]"
                            Me._ID = ObjToInt(com.ExecuteScalar)
                            sql = "Insert Into " & TableMaster & " ([ID],[Tgl]
                                  ,[Kode]
                                  ,[Keterangan]
                                  ,[TanggalBuat]
                                  ,[UserBuat]) " & vbCrLf &
                                  " Values (" & Me._ID & "," & Utils.ObjToStrDateSql(txtTgl.DateTime) & ",'" & FixApostropi(txtKode.Text.Trim) & "'," & vbCrLf &
                                  " '" & FixApostropi(txtKeterangan.Text.Trim) & "',GetDate()," & vbCrLf &
                                  " '" & Username & "')"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                        Else
                            sql = "Update " & TableMaster & " Set " & vbCrLf &
                                "[Tgl] =" & Utils.ObjToStrDateSql(txtTgl.DateTime) & "," & vbCrLf &
                                "[Kode] ='" & FixApostropi(txtKode.Text.Trim) & "'," & vbCrLf &
                                "[Keterangan] ='" & FixApostropi(txtKeterangan.Text.Trim) & "'," & vbCrLf &
                                "[TanggalUbah]=Getdate(),[UserUbah]= '" & Username & "'" & vbCrLf &
                                " Where [ID]=" & Me._ID

                            com.CommandText = sql
                            com.ExecuteNonQuery()

                        End If

                        If Me._ID > 0 Then
                            If GV1.RowCount > 0 Then
                                For i As Integer = 0 To GV1.RowCount - 1
                                    If ObjToInt(GV1.GetRowCellValue(i, "IDBarang")) > 0 AndAlso ObjToInt(GV1.GetRowCellValue(i, "Qty")) > 0 Then
                                        If ObjToInt(GV1.GetRowCellValue(i, "ID")) <= 0 Then
                                            Dim IDDetail As Long = 0
                                            com.CommandText = "Select Isnull(Max(ID),0)+1 From " & TableMaster & "D"
                                            IDDetail = ObjToLong(com.ExecuteScalar())
                                            com.CommandText = "INSERT INTO [" & TableMaster & "D]
                                                            ([ID]
                                                            ,[IDStokMasuk]
                                                            ,[NoUrut]
                                                            ,[IDBarang]
                                                            ,[Qty]
                                                            ,[IDSatuan]
                                                            ,[Isi]
                                                            ,[Harga]
                                                            ,[Jumlah]
                                                            ,[Catatan]) 
                                                            VALUES (@ID,@IDStokMasuk,@NoUrut,@IDBarang,@Qty,@IDSatuan,@Isi,@Harga,@Jumlah,@Catatan)"
                                            com.Parameters.AddWithValue("@ID", IDDetail)
                                        Else
                                            com.CommandText = "UPDATE [" & TableMaster & "D] SET
                                                            [IDStokMasuk]=@IDStokMasuk
                                                            ,[NoUrut]=@NoUrut
                                                            ,[IDBarang]=@IDBarang
                                                            ,[Qty]=@Qty
                                                            ,[IDSatuan]= @IDSatuan
                                                            ,[Isi] =@Isi
                                                            ,[Harga] =Harga
                                                            ,[Jumlah] =@Jumlah
                                                            ,[Catatan] =@Catatan
                                                            Where [ID]=@ID"
                                            com.Parameters.AddWithValue("@ID", ObjToInt(GV1.GetRowCellValue(i, "ID")))
                                        End If
                                        com.Parameters.AddWithValue("@IDStokMasuk", Me._ID)
                                        com.Parameters.AddWithValue("@NoUrut", ObjToInt(GV1.GetRowCellValue(i, "NoUrut")))
                                        com.Parameters.AddWithValue("@IDBarang", ObjToInt(GV1.GetRowCellValue(i, "IDBarang")))
                                        com.Parameters.AddWithValue("@Qty", ObjToDbl(GV1.GetRowCellValue(i, "Qty")))
                                        com.Parameters.AddWithValue("@IDSatuan", 1) 'ObjToInt(gridView1.GetRowCellValue(i, "IDSatuan"))
                                        com.Parameters.AddWithValue("@Isi", 1) 'ObjToInt(gridView1.GetRowCellValue(i, "Isi"))
                                        com.Parameters.AddWithValue("@Harga", ObjToInt(GV1.GetRowCellValue(i, "Harga")))
                                        com.Parameters.AddWithValue("@Jumlah", ObjToInt(GV1.GetRowCellValue(i, "Jumlah")))
                                        com.Parameters.AddWithValue("@Catatan", NullToStr(GV1.GetRowCellValue(i, "Catatan")))

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
                        DevExpress.XtraEditors.XtraMessageBox.Show(Me, pesan.Message, NamaAplikasi)
                    Catch ex As Exception
                        com.Transaction.Rollback()
                        With pesan
                            .Hasil = False
                            .Message = "Simpan Gagal." & ex.Message
                            .Value = ""
                        End With
                        DevExpress.XtraEditors.XtraMessageBox.Show(Me, pesan.Message, NamaAplikasi)
                    Finally
                        com.Transaction = Nothing
                    End Try
                End Using
            End Using
        End If
        Return pesan.Hasil
    End Function



    Private Sub bbiReset_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiReset.ItemClick
        Me.Form1_Load(sender, e)
    End Sub

    Private Sub bbiDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiDelete.ItemClick
        If _IsNew Then
            Exit Sub
        End If
        Dim f As Pesan = Query.DeleteDataMaster(TableMaster, "ID='" & Me._ID & "'", False)
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

    Private Sub txtTgl_EditValueChanged(sender As Object, e As EventArgs) Handles txtTgl.EditValueChanged
        If txtKode.Properties.ReadOnly = False Then
            txtKode.Text = GlobalFunc.GetKodeTransaksi(txtTgl.DateTime, KodeDepan, TableMaster).Value.ToString
        End If
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonHapusItem.ItemClick
        Using f As New FormBarang
            f._IsNew = True
            If f.ShowDialog() = DialogResult.OK Then
                LoadItems()
            End If
        End Using
    End Sub

    Private Sub gridView1_DataSourceChanged(sender As Object, e As EventArgs) Handles GV1.DataSourceChanged
        RestoreGVLayouts(GV1)
    End Sub

    Private Sub FormStokMasuk_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Try
            If GC1.DataSource IsNot Nothing Then
                GV1.SaveLayoutToXml(LayoutsHelper.FolderLayouts & Me.Name & GV1.Name & ".xml")
            End If
            If RepositoryItemSearchLookUpEdit1.DataSource IsNot Nothing Then
                RepositoryItemSearchLookUpEdit1View.SaveLayoutToXml(LayoutsHelper.FolderLayouts & Me.Name & RepositoryItemSearchLookUpEdit1View.Name & ".xml")
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Kesalahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
    End Sub

    Private Sub RepositoryItemSearchLookUpEdit1View_DataSourceChanged(sender As Object, e As EventArgs) Handles RepositoryItemSearchLookUpEdit1View.DataSourceChanged
        RestoreGVLayouts(RepositoryItemSearchLookUpEdit1View)
    End Sub


    Private Sub RestoreGVLayouts(GV As GridView)
        With GV
            GV.OptionsView.ColumnAutoWidth = False
            GV.OptionsView.BestFitMaxRowCount = -1
            GV.BestFitColumns()
            .OptionsDetail.SmartDetailExpandButtonMode = DetailExpandButtonMode.AlwaysEnabled
            If System.IO.File.Exists(LayoutsHelper.FolderLayouts & Me.Text & GV.Name & ".xml") Then
                .RestoreLayoutFromXml(LayoutsHelper.FolderLayouts & Me.Text & GV.Name & ".xml")
            End If
            For i As Integer = 0 To .Columns.Count - 1
                Select Case .Columns(i).ColumnType.Name.ToLower
                    Case "date", "datetime"
                        If .Columns(i).FieldName.Trim.ToLower = "jam" Then
                            .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                            .Columns(i).DisplayFormat.FormatString = "HH:mm"
                        ElseIf .Columns(i).FieldName.Trim.ToLower = "tanggalstart" Or .Columns(i).FieldName.Trim.ToLower = "tanggalend" Then
                            .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                            .Columns(i).DisplayFormat.FormatString = "dd-MM-yyyy HH:mm"
                        Else
                            .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                            .Columns(i).DisplayFormat.FormatString = "dd-MM-yyyy"
                        End If
                    Case "int32", "int64", "int"
                        .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        .Columns(i).DisplayFormat.FormatString = "n0"
                    Case "decimal", "single", "double", "numeric"
                        .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        .Columns(i).DisplayFormat.FormatString = "n2"
                    Case "money"
                        .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        .Columns(i).DisplayFormat.FormatString = "c2"
                    Case "string"
                        .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
                        .Columns(i).DisplayFormat.FormatString = ""
                    Case "byte[]"
                        reppicedit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
                        .Columns(i).OptionsColumn.AllowGroup = False
                        .Columns(i).OptionsColumn.AllowSort = False
                        .Columns(i).OptionsFilter.AllowFilter = False
                        .Columns(i).ColumnEdit = reppicedit
                    Case "boolean", "bit"
                        repckedit.DisplayValueUnchecked = "False"
                        repckedit.DisplayValueChecked = "True"
                        .Columns(i).ColumnEdit = repckedit
                End Select
                If .Columns(i).FieldName.Length >= 4 AndAlso .Columns(i).FieldName.Substring(0, 4).ToLower = "Kode".ToLower Then
                    .Columns(i).Fixed = FixedStyle.Left
                ElseIf .Columns(i).FieldName.ToLower = "Nama".ToLower Then
                    .Columns(i).Fixed = FixedStyle.Left
                ElseIf .Columns(i).FieldName.ToLower = "kurs" Then
                    .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    .Columns(i).DisplayFormat.FormatString = "n4"
                ElseIf .Columns(i).FieldName.ToLower = "id" Or .Columns(i).FieldName.ToLower = "nourut" Or .Columns(i).FieldName.ToLower = "no" Or .Columns(i).FieldName.ToLower = "isposted" Then
                    .Columns(i).Visible = False
                End If
            Next
        End With
    End Sub
    Private Sub BarButtonHapusItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonHapusItem.ItemClick, BarButtonItem2.ItemClick
        Try
            If GV1.SelectedRowsCount > 0 Then
                Dim IDD As Long = ObjToLong(GV1.GetFocusedDataRow("ID"))
                If IDD > 0 Then
                    Sql = "Delete " & TableMaster & "D Where ID=" & IDD
                    Query.Execute(Sql)
                End If
                GV1.DeleteSelectedRows()
                GV1.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GV1_MouseDown(sender As Object, e As MouseEventArgs) Handles GV1.MouseDown
        Dim View As GridView = CType(sender, GridView)
        If View Is Nothing Then Return
        ' obtaining hit info
        Dim hitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = View.CalcHitInfo(New System.Drawing.Point(e.X, e.Y))
        If (e.Button = Windows.Forms.MouseButtons.Right) And (hitInfo.InRow) And
              (Not View.IsGroupRow(hitInfo.RowHandle)) Then
            PopupMenu1.ShowPopup(Control.MousePosition)
        End If
    End Sub
End Class