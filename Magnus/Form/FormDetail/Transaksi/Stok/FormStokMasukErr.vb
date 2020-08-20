Imports DevExpress.XtraLayout
Imports DevExpress.XtraLayout.Helpers
Imports System.ComponentModel.DataAnnotations
Imports System.Data.SqlClient
Imports System.IO
Imports Magnus.Utils
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils

Partial Public Class FormStokMasukErr
    Public _IsNew As Boolean
    Public _ID As Integer
    Public Sub New()
        InitializeComponent()
    End Sub

    Private sql As String = ""
    Private list As List(Of Barang) = Nothing
    Sub LoadDataAdapter()
        'Repo_ItemLookUpBarang.DataSource = Query.ExecuteDataSet("Select ID,Kode,Nama From MBarang Where IsActive=1").Tables(0)
        'Repo_ItemLookUpBarang.DisplayMember = "Kode"
        'Repo_ItemLookUpBarang.ValueMember = "ID"
        Dim ds As DataSet = New DataSet()
        Dim tblLookUp As String = "Products"
        Using d As New WaitDialogForm("Loading Data Barang . . .")
            'Refresher()
            d.SetCaption("Loading Products...")
            sql = "SELECT MB.ID ID,MB.Kode,MB.Nama,MS.Kode Satuan  " &
                               " From MBarang MB Left Join MSatuan MS on MS.ID=MB.IDSatuanTerkecil " &
                               " Where MB.IsActive=1 ORDER BY MB.Nama ASC"
            ds = Query.ExecuteDataSet(sql, tblLookUp)

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

            End If
            RepositoryItemSearchLookUpEdit1.NullText = "Pilih Barang"
            GridColumnIDBarang.ColumnEdit = RepositoryItemSearchLookUpEdit1


            If Not ds Is Nothing Then
                RepositoryItemSearchLookUpEdit2.DataSource = list 'ds.Tables(tblLookUp)
                RepositoryItemSearchLookUpEdit2.ValueMember = "ID"
                RepositoryItemSearchLookUpEdit2.DisplayMember = "Kode"
                ds.Dispose()
            End If
            RepositoryItemSearchLookUpEdit2.NullText = "Pilih Barang"
            GridColumnIDBarang.ColumnEdit = RepositoryItemSearchLookUpEdit2
        End Using
    End Sub

    Dim repckedit As New RepositoryItemCheckEdit
    Dim repdateedit As New RepositoryItemDateEdit
    Dim reptextedit As New RepositoryItemTextEdit
    Dim reppicedit As New RepositoryItemPictureEdit
    Private Sub FormUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDataAdapter()
        If _IsNew Then
            txtKode.Properties.ReadOnly = False
            ClearData()
        Else
            Dim ds As New DataSet
            sql = "Select * From TStokMasuk T WHere T.ID=" & Me._ID & ""
            ds = Query.ExecuteDataSet(sql)
            If Not ds Is Nothing Then
                With ds.Tables(0).Rows(0)
                    txtKode.Properties.ReadOnly = True
                    txtKode.Text = .Item("Kode").ToString
                    txtKeterangan.Text = .Item("Keterangan").ToString
                End With
                ds.Dispose()
                Refresher()
            End If
        End If
    End Sub
    Sub Refresher()
        Dim ds As New DataSet
        Try
            sql = "Select TD.ID ,TD.NoUrut, TD.IDBarang ,MB.Nama NamaBarang,MS.Kode Unit,TD.Qty,TD.Harga,TD.Jumlah,TD.Catatan 
                    From TStokMasuk T Inner Join TStokMasukD TD on T.ID=TD.IDStokMasuk 
                    Inner Join MBarang MB on MB.ID=TD.IDBarang 
                    Left Join MSatuan MS On MS.ID=MB.IDSatuanTerkecil
                    Where T.ID=" & Me._ID
            ds = Query.ExecuteDataSet(sql)
            Dim dvManager As DataViewManager = New DataViewManager(ds)
            Dim dv As DataView = dvManager.CreateDataView(ds.Tables(0))

            If Not ds Is Nothing Then
                GridControl1.DataSource = dv
                ds.Dispose()
                GridControl1.Refresh()

                GridView1.OptionsView.ColumnAutoWidth = False
                GridView1.OptionsView.BestFitMaxRowCount = -1
                GridView1.BestFitColumns()

                With GridView1
                    For i As Integer = 0 To .Columns.Count - 1
                        Select Case GridView1.Columns(i).ColumnType.Name.ToLower
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
                            Case "date", "datetime"
                                .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                                .Columns(i).DisplayFormat.FormatString = "dd-MM-yyyy"
                            Case "byte[]"
                                reppicedit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
                                .Columns(i).OptionsColumn.AllowGroup = False
                                .Columns(i).OptionsColumn.AllowSort = False
                                .Columns(i).OptionsFilter.AllowFilter = False
                                .Columns(i).ColumnEdit = reppicedit
                            Case "boolean", "bit"
                                .Columns(i).ColumnEdit = repckedit
                        End Select
                        If .Columns(i).FieldName.Length >= 4 AndAlso .Columns(i).FieldName.Substring(0, 4).ToLower = "Kode".ToLower Then
                            .Columns(i).Fixed = FixedStyle.Left
                        ElseIf .Columns(i).FieldName.ToLower = "Nama".ToLower Then
                            .Columns(i).Fixed = FixedStyle.Left
                        ElseIf .Columns(i).FieldName.ToLower = "Kurs" Then
                            .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .Columns(i).DisplayFormat.FormatString = "n4"
                        End If
                    Next
                End With
            End If

            Dim tblGrid As String = "[Order Details]"
            'TD.Harga,TD.Jumlah,
            sql = "Select TD.ID ,TD.NoUrut, TD.IDBarang ,MB.Nama NamaBarang,MS.Kode Unit,TD.Qty,TD.Harga,TD.Jumlah,TD.Catatan 
                    From TStokMasuk T Inner Join TStokMasukD TD on T.ID=TD.IDStokMasuk 
                    Inner Join MBarang MB on MB.ID=TD.IDBarang 
                    Left Join MSatuan MS On MS.ID=MB.IDSatuanTerkecil
                    Where T.ID=1"
            ds = Query.ExecuteDataSet(sql, tblGrid)
            Dim dvManager2 As DataViewManager = New DataViewManager(ds)
            Dim dv2 As DataView = dvManager2.CreateDataView(ds.Tables(tblGrid))

            If Not ds Is Nothing Then
                GC1.DataSource = dv2
                ds.Dispose()
                GC1.Refresh()

                GridView1.OptionsView.ColumnAutoWidth = False
                GridView1.OptionsView.BestFitMaxRowCount = -1
                GridView1.BestFitColumns()

                With GridView1
                    For i As Integer = 0 To .Columns.Count - 1
                        Select Case GridView1.Columns(i).ColumnType.Name.ToLower
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
                            Case "date", "datetime"
                                .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                                .Columns(i).DisplayFormat.FormatString = "dd-MM-yyyy"
                            Case "byte[]"
                                reppicedit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
                                .Columns(i).OptionsColumn.AllowGroup = False
                                .Columns(i).OptionsColumn.AllowSort = False
                                .Columns(i).OptionsFilter.AllowFilter = False
                                .Columns(i).ColumnEdit = reppicedit
                            Case "boolean", "bit"
                                .Columns(i).ColumnEdit = repckedit
                        End Select
                        If .Columns(i).FieldName.Length >= 4 AndAlso .Columns(i).FieldName.Substring(0, 4).ToLower = "Kode".ToLower Then
                            .Columns(i).Fixed = FixedStyle.Left
                        ElseIf .Columns(i).FieldName.ToLower = "Nama".ToLower Then
                            .Columns(i).Fixed = FixedStyle.Left
                        ElseIf .Columns(i).FieldName.ToLower = "Kurs" Then
                            .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .Columns(i).DisplayFormat.FormatString = "n4"
                        End If
                    Next
                End With
            End If
        Catch ex As Exception

        End Try
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
        txtKeterangan.Text = ""
        GridControl1.DataSource = Nothing
    End Sub
    Function IsValid() As Boolean
        IsValid = True
        If txtKode.Text = "" Then
            txtKode.Focus()
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
        If CInt(Query.ExecuteScalar("Select Count(*) From MRoleUser Where ID<>" & Me._ID & " And Kode ='" & txtKode.Text & "'")) > 0 Then
            MessageBox.Show("Kode sudah ada/terpakai.", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtKode.Focus()
            IsValidOnDB = False
            Exit Function
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
                            com.CommandText = "Select Isnull(Max(ID),0)+1 From [TStokMasuk]"
                            Me._ID = ObjToInt(com.ExecuteScalar)
                            sql = "Insert Into TStokMasuk ([ID]
                                  ,[Kode]
                                  ,[Keterangan]
                                  ,[TanggalBuat]
                                  ,[UserBuat]) " & vbCrLf &
                                    " Values (" & Me._ID & ",'" & FixApostropi(txtKode.Text.Trim) & "'," & vbCrLf &
                                    " '" & FixApostropi(txtKeterangan.Text.Trim) & "',GetDate()," & vbCrLf &
                                    Username & ")"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                        Else
                            sql = "Update TStokMasuk Set " & vbCrLf &
                                "[Kode] ='" & FixApostropi(txtKode.Text.Trim) & "'," & vbCrLf &
                                "[Keterangan] ='" & FixApostropi(txtKeterangan.Text.Trim) & "'," & vbCrLf &
                                "[Keterangan] ='" & FixApostropi(txtKeterangan.Text.Trim) & "'," & vbCrLf &
                                "[TanggalUbah]=Getdate(),[UserBuat]= '" & Username & "'" & vbCrLf &
                                " Where [ID]=" & Me._ID

                            com.CommandText = sql
                            com.ExecuteNonQuery()

                        End If

                        If Me._ID > 0 Then
                            If GridView1.RowCount > 0 Then
                                For i As Integer = 0 To GridView1.RowCount - 1
                                    If ObjToInt(GridView1.GetRowCellValue(i, "IDBarang")) > 0 AndAlso ObjToInt(GridView1.GetRowCellValue(i, "Qty")) > 0 Then
                                        If ObjToInt(GridView1.GetRowCellValue(i, "ID")) <= 0 Then
                                            Dim IDDetail As Long = 0
                                            com.CommandText = "Select Isnull(Max(ID),0)+1 From TStokMasukD"
                                            IDDetail = ObjToLong(com.ExecuteScalar())
                                            com.CommandText = "INSERT INTO [TStokMasukD]
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
                                            com.CommandText = "UPDATE [TStokMasukD] SET
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
                                            com.Parameters.AddWithValue("@ID", ObjToInt(GridView1.GetRowCellValue(i, "ID")))
                                        End If
                                        com.Parameters.AddWithValue("@IDStokMasuk", Me._ID)
                                        com.Parameters.AddWithValue("@NoUrut", ObjToInt(GridView1.GetRowCellValue(i, "NoUrut")))
                                        com.Parameters.AddWithValue("@IDBarang", ObjToInt(GridView1.GetRowCellValue(i, "IDBarang")))
                                        com.Parameters.AddWithValue("@Qty", ObjToInt(GridView1.GetRowCellValue(i, "Qty")))
                                        com.Parameters.AddWithValue("@IDSatuan", 1) 'ObjToInt(gridView1.GetRowCellValue(i, "IDSatuan"))
                                        com.Parameters.AddWithValue("@Isi", 1) 'ObjToInt(gridView1.GetRowCellValue(i, "Isi"))
                                        com.Parameters.AddWithValue("@Harga", ObjToInt(GridView1.GetRowCellValue(i, "Harga")))
                                        com.Parameters.AddWithValue("@Jumlah", ObjToInt(GridView1.GetRowCellValue(i, "Jumlah")))
                                        com.Parameters.AddWithValue("@Catatan", NullToStr(GridView1.GetRowCellValue(i, "Catatan")))

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
        Me.FormUser_Load(sender, e)
    End Sub

    Private Sub bbiDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiDelete.ItemClick
        If _IsNew Then
            Exit Sub
        End If
        Dim f As Pesan = Query.DeleteDataMaster("MRoleUser", "Kode='" & txtKode.Text.Trim & "'")
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


    'Private Sub Repo_ItemBarang_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs)
    '    If e.Button.Index = 0 Then
    '        Using x As New frmLookup
    '            Dim NamaBarang As String = ""
    '            Try
    '                x.Strsql = "SELECT ID,Kode,Nama " &
    '                           " From MBarang MB " &
    '                           " Where IsActive=1 ORDER BY MB.Nama ASC"
    '                x.FormName = "Daftar Barang Aktif"
    '                x.Text = "Daftar Barang Aktif"
    '                If x.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
    '                    'GridView1.SetFocusedValue 'Buat Combobox
    '                    GridView1.SetFocusedValue(x.NoID)
    '                    GridView1.SetFocusedRowCellValue(GridColumnNamaBarang, x.Nama)
    '                End If
    '            Catch ex As Exception

    '            End Try
    '        End Using
    '    End If
    'End Sub

    'Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
    '    If XtraMessageBox.Show(String.Format("Delete row? (Nama Barang = {0} Qty = {1})", GridView1.GetFocusedRowCellValue("NamaBarang"), GridView1.GetFocusedRowCellValue("Qty")), "Delete rows dialog", MessageBoxButtons.YesNo) = DialogResult.Yes Then
    '        GridView1.DeleteSelectedRows()
    '    End If
    'End Sub

    'Private Sub Repo_ItemLookUpBarang_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles Repo_ItemLookUpBarang.ButtonClick
    '    Select Case e.Button.Index
    '        Case 0

    '        Case 1
    '            Using x As New frmLookup
    '                Dim NamaBarang As String = ""
    '                Try
    '                    x.Strsql = "SELECT ID,Kode,Nama " &
    '                           " From MBarang MB " &
    '                           " Where IsActive=1 ORDER BY MB.Nama ASC"
    '                    x.FormName = "Daftar Barang Aktif"
    '                    x.Text = "Daftar Barang Aktif"
    '                    If x.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
    '                        'GridView1.SetFocusedValue 'Buat Combobox
    '                        GridView1.SetFocusedValue(x.NoID)
    '                        GridView1.SetFocusedRowCellValue(GridColumnNamaBarang, x.Nama)
    '                    End If
    '                Catch ex As Exception

    '                End Try
    '            End Using
    '    End Select
    'End Sub
    Private qty As Double = 0.0
    Private price As Double = 0.0
    Private amount As Double = 0.0
    Private Sub gridView1_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column.FieldName = "IDBarang" Then
            Dim value = GridView1.GetRowCellValue(e.RowHandle, GridColumnIDBarang)
            Dim dt = list.AsEnumerable.FirstOrDefault(Function(x) x.ID = Utils.ObjToInt(value))

            If dt IsNot Nothing Then
                If Utils.ObjToInt(GridView1.GetFocusedRowCellValue(GridColumn_ID)) < 1 Then
                    GridView1.SetRowCellValue(e.RowHandle, "ID", -1)
                End If
                GridView1.SetRowCellValue(e.RowHandle, "NamaBarang", dt.Nama)
                GridView1.SetRowCellValue(e.RowHandle, "Unit", dt.SatuanTerkecil)
                GridView1.SetRowCellValue(e.RowHandle, "Harga", 1)
                GridView1.SetRowCellValue(e.RowHandle, "NoUrut", GridView1.RowCount)

                If Utils.NullToStr(GridView1.GetFocusedRowCellValue(GridColumnQty)) = "" Then
                    qty = 0.0
                Else
                    qty = Utils.ObjToDbl(GridView1.GetFocusedRowCellValue(GridColumnQty))
                    price = Utils.ObjToDbl(GridView1.GetFocusedRowCellValue(GridColumnHarga))
                    amount = qty * price
                    GridView1.SetFocusedRowCellValue(GridColumnJumlah, amount)
                    'vat = amount / 10
                    'gridView1.SetFocusedRowCellValue(colVat, vat)
                End If
            End If
        End If

        If e.Column.Name = GridColumnQty.Name Then
            qty = Utils.ObjToDbl(GridView1.GetFocusedRowCellValue(GridColumnQty))
            price = Utils.ObjToDbl(GridView1.GetFocusedRowCellValue(GridColumnHarga))
            amount = qty * price
            GridView1.SetFocusedRowCellValue(GridColumnJumlah, amount)
            'vat = amount / 10
            'gridView1.SetFocusedRowCellValue(colVat, vat)
        End If
    End Sub

    Private Sub gridView2_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView2.CellValueChanged
        If e.Column.FieldName = "IDBarang" Then
            Dim value = GridView1.GetRowCellValue(e.RowHandle, e.Column)
            Dim dt = list.AsEnumerable.FirstOrDefault(Function(x) x.ID = Utils.ObjToInt(value))

            If dt IsNot Nothing Then
                If Utils.ObjToInt(GridView1.GetFocusedRowCellValue(GridColumnID)) < 1 Then
                    GridView1.SetRowCellValue(e.RowHandle, "ID", -1)
                End If
                GridView1.SetRowCellValue(e.RowHandle, "NamaBarang", dt.Nama)
                GridView1.SetRowCellValue(e.RowHandle, "Unit", dt.SatuanTerkecil)
                GridView1.SetRowCellValue(e.RowHandle, "Harga", 1)
                GridView1.SetRowCellValue(e.RowHandle, "NoUrut", GridView1.RowCount)

                If Utils.NullToStr(GridView1.GetFocusedRowCellValue(colQty)) = "" Then
                    qty = 0.0
                Else
                    qty = Utils.ObjToDbl(GridView1.GetFocusedRowCellValue(colQty))
                    price = Utils.ObjToDbl(GridView1.GetFocusedRowCellValue(colPrice))
                    amount = qty * price
                    GridView1.SetFocusedRowCellValue(colAmount, amount)
                    'vat = amount / 10
                    'gridView1.SetFocusedRowCellValue(colVat, vat)
                End If
            End If
        End If

        If e.Column.Name = colQty.Name Then
            qty = Utils.ObjToDbl(GridView1.GetFocusedRowCellValue(colQty))
            price = Utils.ObjToDbl(GridView1.GetFocusedRowCellValue(colPrice))
            amount = qty * price
            GridView1.SetFocusedRowCellValue(colAmount, amount)
            'vat = amount / 10
            'gridView1.SetFocusedRowCellValue(colVat, vat)
        End If
    End Sub
End Class
