Imports System.Data.SqlClient
Imports DevExpress.Utils
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Columns
Imports Magnus.Utils

Public Class FormDemoGridLookup
    Private qty As Double = 0.0
    Private amount As Double = 0.0
    Private vat As Double = 0.0
    Private price As Double = 0.0

    Public Sub New()
        InitializeComponent()
    End Sub


    Dim repckedit As New RepositoryItemCheckEdit
    Dim repdateedit As New RepositoryItemDateEdit
    Dim reptextedit As New RepositoryItemTextEdit
    Dim reppicedit As New RepositoryItemPictureEdit
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        LoadItems()
        Dim ds As New DataSet
        Dim tblGrid As String = "[Order Details]"
        Try
            'TD.Harga,TD.Jumlah,
            Sql = "Select TD.ID ,TD.NoUrut, TD.IDBarang ,MB.Nama NamaBarang,MS.Kode Unit,TD.Qty,TD.Harga,TD.Jumlah,TD.Catatan 
                    From TStokMasuk T Inner Join TStokMasukD TD on T.ID=TD.IDStokMasuk 
                    Inner Join MBarang MB on MB.ID=TD.IDBarang 
                    Left Join MSatuan MS On MS.ID=MB.IDSatuanTerkecil
                    Where T.ID=1"
            ds = Query.ExecuteDataSet(Sql, tblGrid)
            Dim dvManager As DataViewManager = New DataViewManager(ds)
            Dim dv As DataView = dvManager.CreateDataView(ds.Tables(tblGrid))

            If Not ds Is Nothing Then
                GC1.DataSource = dv
                ds.Dispose()
                GC1.Refresh()

                gridView1.OptionsView.ColumnAutoWidth = False
                gridView1.OptionsView.BestFitMaxRowCount = -1
                gridView1.BestFitColumns()

                With gridView1
                    For i As Integer = 0 To .Columns.Count - 1
                        Select Case gridView1.Columns(i).ColumnType.Name.ToLower
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
    Dim Sql As String = ""

    Dim list As List(Of Barang) = Nothing
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
            GridColumnIDbarang.ColumnEdit = RepositoryItemSearchLookUpEdit1
        End Using
    End Sub

    Private Sub gridView1_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gridView1.CellValueChanged
        If e.Column.FieldName = "IDBarang" Then
            Dim value = gridView1.GetRowCellValue(e.RowHandle, e.Column)
            Dim dt = list.AsEnumerable.FirstOrDefault(Function(x) x.ID = Utils.ObjToInt(value))

            If dt IsNot Nothing Then
                If Utils.ObjToInt(gridView1.GetFocusedRowCellValue(GridColumnID)) < 1 Then
                    gridView1.SetRowCellValue(e.RowHandle, "ID", -1)
                End If
                gridView1.SetRowCellValue(e.RowHandle, "NamaBarang", dt.Nama)
                gridView1.SetRowCellValue(e.RowHandle, "Unit", dt.SatuanTerkecil)
                gridView1.SetRowCellValue(e.RowHandle, "Harga", 1)
                gridView1.SetRowCellValue(e.RowHandle, "NoUrut", gridView1.RowCount)

                If Utils.NullToStr(gridView1.GetFocusedRowCellValue(colQty)) = "" Then
                    qty = 0.0
                Else
                    qty = Utils.ObjToDbl(gridView1.GetFocusedRowCellValue(colQty))
                    price = Utils.ObjToDbl(gridView1.GetFocusedRowCellValue(colPrice))
                    amount = qty * price
                    gridView1.SetFocusedRowCellValue(colAmount, amount)
                    'vat = amount / 10
                    'gridView1.SetFocusedRowCellValue(colVat, vat)
                End If
            End If
        End If

        If e.Column.Name = colQty.Name Then
            qty = Utils.ObjToDbl(gridView1.GetFocusedRowCellValue(colQty))
            price = Utils.ObjToDbl(gridView1.GetFocusedRowCellValue(colPrice))
            amount = qty * price
            gridView1.SetFocusedRowCellValue(colAmount, amount)
            'vat = amount / 10
            'gridView1.SetFocusedRowCellValue(colVat, vat)
        End If
    End Sub
    Dim _ID As Long = 1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim pesan As New Pesan With {.Hasil = False, .Value = Nothing, .Message = ""}
        Dim sql As String = ""
        Using con As New SqlConnection(conStr)
            Using com As New SqlCommand
                Try
                    com.Connection = con
                    con.Open()
                    com.Transaction = con.BeginTransaction

                    If gridView1.RowCount > 0 Then
                        For i As Integer = 0 To gridView1.RowCount - 1
                            If ObjToInt(gridView1.GetRowCellValue(i, "IDBarang")) > 0 AndAlso ObjToInt(gridView1.GetRowCellValue(i, "Qty")) > 0 Then
                                If ObjToInt(gridView1.GetRowCellValue(i, "ID")) <= 0 Then
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
                                    com.Parameters.AddWithValue("@ID", ObjToInt(gridView1.GetRowCellValue(i, "ID")))
                                End If
                                com.Parameters.AddWithValue("@IDStokMasuk", Me._ID)
                                com.Parameters.AddWithValue("@NoUrut", ObjToInt(gridView1.GetRowCellValue(i, "NoUrut")))
                                com.Parameters.AddWithValue("@IDBarang", ObjToInt(gridView1.GetRowCellValue(i, "IDBarang")))
                                com.Parameters.AddWithValue("@Qty", ObjToInt(gridView1.GetRowCellValue(i, "Qty")))
                                com.Parameters.AddWithValue("@IDSatuan", 1) 'ObjToInt(gridView1.GetRowCellValue(i, "IDSatuan"))
                                com.Parameters.AddWithValue("@Isi", 1) 'ObjToInt(gridView1.GetRowCellValue(i, "Isi"))
                                com.Parameters.AddWithValue("@Harga", ObjToInt(gridView1.GetRowCellValue(i, "Harga")))
                                com.Parameters.AddWithValue("@Jumlah", ObjToInt(gridView1.GetRowCellValue(i, "Jumlah")))
                                com.Parameters.AddWithValue("@Catatan", NullToStr(gridView1.GetRowCellValue(i, "Catatan")))

                                com.ExecuteNonQuery()
                                com.Parameters.Clear()
                            End If
                        Next
                    End If
                    com.Transaction.Commit()
                    With pesan
                        .Hasil = True
                        .Message = "Simpan Berhasil."
                        .Value = ""
                    End With
                    Me.Form1_Load(sender, e)
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
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Using f As New FormStokMasukErr
        '    f._IsNew = True
        '    If f.ShowDialog() = DialogResult.OK Then
        '    End If
        'End Using
    End Sub
End Class