Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views
Imports DevExpress.XtraGrid.Columns
Imports Magnus.Utils
Partial Public Class FormDaftarTransaksi
    Inherits DevExpress.XtraEditors.XtraForm

    Public idFrmTr As IDFormTr
    Public Enum IDFormTr
        F_StokMasuk = 0
        F_StokKeluar = 1
        F_KasMasuk = 2
        F_KasKeluar = 3
        F_CalcLabel = 4
        F_CalcRibbon = 5
        F_CalcTaffeta = 6
        F_CalcPaket = 7
    End Enum
    Public NamaForm As String = "Undefined"
    Private Sub FormDaftar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowMenuRoleUser()
        If ObjToInt(Query.ExecuteScalar("Select TipeTanggal From MSettingLainya")) = 0 Then
            BarEditTglDari.EditValue = ObjToDate(Query.ExecuteScalar("Select GetDate()")).ToString("yyyy-MM-01") ' DateTime.Now.ToString("yyyy-MM-01")
        Else
            BarEditTglDari.EditValue = ObjToDate(Query.ExecuteScalar("Select GetDate()")).ToString("yyyy-MM-dd") 'DateTime.Now.ToString("yyyy-MM-dd")
        End If
        BarEditTglSampai.EditValue = ObjToDate(Query.ExecuteScalar("Select GetDate()")).ToString("yyyy-MM-dd") ' DateTime.Now.ToString("yyyy-MM-dd")
        If ObjToBool(Query.ExecuteScalar("Select IsLockTglInHeader From MSettingLainya")) Then
            BarEditTglDari.Enabled = False
            BarEditTglSampai.Enabled = False
        End If
        Refresher()
    End Sub
    Sub ShowMenuRoleUser()
        Try
            Dim newList As List(Of MenuRoleUser) = listMenu.Where(Function(m) m.NamaForm = Me.NamaForm).[Select](Function(m) New MenuRoleUser With {
            .Caption = m.Caption,
            .IsBaru = m.IsBaru,
            .IsUbah = m.IsUbah,
            .IsHapus = m.IsHapus,
            .IsCetak = m.IsCetak,
            .IsExport = m.IsExport}).ToList()
            BarButtonBaru.Enabled = newList(0).IsBaru
            BarButtonUbah.Enabled = newList(0).IsUbah
            BarButtonHapus.Enabled = newList(0).IsHapus
            BarButtonExport.Enabled = newList(0).IsExport
            BarButtonCetak.Enabled = newList(0).IsCetak
            Me.Text = IIf(newList(0).Caption = "", NamaForm, newList(0).Caption).ToString
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(Me, NamaForm & " " & ex.Message, NamaAplikasi)
        End Try
    End Sub
    Private Sub BarButtonRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonRefresh.ItemClick
        Refresher()
    End Sub

    Dim repckedit As New RepositoryItemCheckEdit
    Dim repdateedit As New RepositoryItemDateEdit
    Dim reptextedit As New RepositoryItemTextEdit
    Dim reppicedit As New RepositoryItemPictureEdit
    Sub Refresher()
        Dim ds As New DataSet
        Dim sql As String = ""
        Dim TglDari As String = "", TglSampai As String = ""
        Select Case idFrmTr
            Case IDFormTr.F_StokMasuk
                sql = "Select * From StokMasuk (NOLOCK)  T " & vbCrLf
            Case IDFormTr.F_StokKeluar
                sql = "Select * From StokKeluar (NOLOCK)  T " & vbCrLf
            Case IDFormTr.F_KasMasuk
                sql = "Select T.ID,T.IsPosted,T.Tgl,MAkun.Nama [Kas/Bank],T.Kode,T.KodeReff,IsNull(TD.Total,0) As Total," & vbCrLf &
                       "T.Keterangan, T.IsBG, T.NoGiro, T.JTBG, T.UserBuat, T.TanggalBuat, T.UserUbah, T.TanggalUbah  " & vbCrLf
                sql &= " From KasBankMasuk (NOLOCK) T " & vbCrLf &
                       "Left Join (Select IDKasBankMasuk,Sum(IsNull(Nominal,0)*IsNull(Kurs,1)) Total From KasBankMasukD (NOLOCK) Group By IDKasBankMasuk) TD on TD.IDKasBankMasuk=T.ID " & vbCrLf &
                       "Left Join MAkun (NOLOCK) On Makun.ID=T.IDKasBank" & vbCrLf
            Case IDFormTr.F_KasKeluar
                sql = "Select T.ID,T.IsPosted,T.Tgl,MAkun.Nama [Kas/Bank],T.Kode,T.KodeReff,IsNull(TD.Total,0) As Total," & vbCrLf &
                       "T.Keterangan, T.IsBG, T.NoGiro, T.JTBG, T.UserBuat, T.TanggalBuat, T.UserUbah, T.TanggalUbah  " & vbCrLf
                sql &= " From KasBankKeluar (NOLOCK) T " & vbCrLf &
                       "Left Join (Select IDKasBankKeluar,Sum(IsNull(Nominal,0)*IsNull(Kurs,1)) Total From KasBankKeluarD (NOLOCK) Group By IDKasBankKeluar) TD on TD.IDKasBankKeluar=T.ID " & vbCrLf &
                       "Left Join MAkun (NOLOCK) On Makun.ID=T.IDKasBank" & vbCrLf
            Case IDFormTr.F_CalcLabel
                If IDRoleUser = 1 Then
                    sql = "Select T.No,T.Tgl,T.Dokumen,B.Nama Bahan,T.harga_bahan HargaBahan ,T.Lebar,T.Tinggi,T.Gap,T.Pisau,T.Pembulatan,T.Qty_Order,T.Jual_Sesuai_Order,Cast((Isnull(T.biaya_pisau,0) + Isnull(T.biaya_tinta,0) + Isnull(T.biaya_toyobo,0) + Isnull(T.biaya_operator,0) + Isnull(T.biaya_kirim,0)) as numeric(18, 2))  TotalBiaya " & vbCrLf
                Else
                    sql = "Select T.No,T.Tgl,T.Dokumen,B.Nama Bahan,T.Lebar,T.Tinggi,T.Gap,T.Pisau,T.Pembulatan,T.Qty_Order,T.Jual_Sesuai_Order,Cast((Isnull(T.biaya_pisau,0) + Isnull(T.biaya_tinta,0) + Isnull(T.biaya_toyobo,0) + Isnull(T.biaya_operator,0) + Isnull(T.biaya_kirim,0)) as numeric(18, 2))  TotalBiaya  " & vbCrLf
                End If
                sql = sql & " From TLabel (NOLOCK) T Left Join MBarang (NOLOCK) B On B.ID=T.ID_Bahan " & vbCrLf
            Case IDFormTr.F_CalcRibbon
                If IDRoleUser = 1 Then
                    sql = "Select T.[No],T.Dokumen,T.Tgl,B.Nama Bahan,T.Harga_Bahan,T.Lebar,T.Panjang,T.Modal,T.Qty,T.Jual_Roll,T.Jumlah_Profit_Kotor As Jumlah,T.Transport,T.Komisisalesprosen [Komisi(%)],Cast( (T.Jumlah_Profit_Kotor*T.Komisisalesprosen)/100 as numeric(18, 2)) KomisiSales,T.NetProfit" & vbCrLf
                Else
                    sql = "Select T.[No],T.Dokumen,T.Tgl,B.Nama Bahan,T.Lebar,T.Panjang,T.Qty,T.Jual_Roll,T.Jumlah_Profit_Kotor As Jumlah,T.Transport,T.Komisisalesprosen [Komisi(%)]" & vbCrLf
                End If
                sql = sql & " From TRibbon (NOLOCK) T Left Join MBarang (NOLOCK) B On B.ID=T.ID_Bahan " & vbCrLf
            Case IDFormTr.F_CalcTaffeta
                If IDRoleUser = 1 Then
                    sql = "Select T.[No],T.Dokumen,T.Tgl,B.Nama Bahan,T.Harga_Bahan,T.Kurs,T.Lebar,T.Panjang,T.Modal,T.Qty,T.Jual_Roll,T.Jumlah_Profit_Kotor As Jumlah,T.Transport,T.Komisisalesprosen [Komisi(%)],Cast( (T.Jumlah_Profit_Kotor*T.Komisisalesprosen)/100 as numeric(18, 2)) KomisiSales,T.NetProfit" & vbCrLf
                Else
                    sql = "Select T.[No],T.Dokumen,T.Tgl,B.Nama Bahan,T.Lebar,T.Panjang,T.Qty,T.Jual_Roll,T.Jumlah_Profit_Kotor As Jumlah,T.Transport,T.Komisisalesprosen [Komisi(%)]" & vbCrLf
                End If
                sql &= " From TTaffeta (NOLOCK) T Left Join MBarang (NOLOCK) B On B.ID=T.ID_Bahan " & vbCrLf
            Case IDFormTr.F_CalcPaket
                sql = "Select * From TPaket (NOLOCK) T " & vbCrLf
        End Select
        sql = sql & " Where 1=1 "
        If BarEditTglDari.EditValue.ToString <> "" Then
            sql = sql & " And T.Tgl between '" & ObjToDate(BarEditTglDari.EditValue) & "' And '" & ObjToDate(BarEditTglSampai.EditValue) & "'"
        End If
        ds = Query.ExecuteDataSet(sql)
        If Not ds Is Nothing Then
            GridControl1.DataSource = ds.Tables(0)
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
    End Sub
    Private Sub BarButtonBaru_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonBaru.ItemClick
        Baru()
    End Sub

    Sub Baru()
        Select Case idFrmTr
            Case IDFormTr.F_StokMasuk
                Using f As New FormStokMasuk
                    f._IsNew = True
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                    End If
                End Using
            Case IDFormTr.F_StokKeluar
                Using f As New FormStokKeluar
                    f._IsNew = True
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                    End If
                End Using
            Case IDFormTr.F_KasMasuk
                Using f As New FormKasBankMasuk
                    f._IsNew = True
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                    End If
                End Using
            Case IDFormTr.F_KasKeluar

            Case IDFormTr.F_CalcLabel
                Using f As New FormCalcRibbon
                    f._IsNew = True
                    f.FormName = "Label Calculator"
                    f.TableName = "TLabel"
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                    End If
                End Using
            Case IDFormTr.F_CalcRibbon
                Using f As New FormCalcRibbon
                    f._IsNew = True
                    f.FormName = "Ribbon Calculator"
                    f.TableName = "TRibbon"
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                    End If
                End Using
            Case IDFormTr.F_CalcTaffeta
                Using f As New FormCalcTaffeta
                    f._IsNew = True
                    f.FormName = "Taffeta Calculator"
                    f.TableName = "Ttaffeta"
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                    End If
                End Using
            Case IDFormTr.F_CalcPaket
                Using f As New FormCalcPaket
                    f._IsNew = True
                    f.FormName = "Paket Calculator"
                    f.TableName = "TPaket"
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                    End If
                End Using
        End Select
    End Sub

    Private Sub BarButtonUbah_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonUbah.ItemClick
        Ubah()
    End Sub

    Sub Ubah()
        Dim view As ColumnView = GridControl1.FocusedView
        Select Case idFrmTr
            Case IDFormTr.F_StokMasuk
                Using f As New FormStokMasuk
                    f._IsNew = False
                    f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                        GridView1.ClearSelection()
                        GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), f._ID.ToString)
                        GridView1.SelectRow(GridView1.FocusedRowHandle)
                    End If
                End Using
            Case IDFormTr.F_StokKeluar
                Using f As New FormStokKeluar
                    f._IsNew = False
                    f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                        GridView1.ClearSelection()
                        GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), f._ID.ToString)
                        GridView1.SelectRow(GridView1.FocusedRowHandle)
                    End If
                End Using
            Case IDFormTr.F_KasMasuk
                Using f As New FormKasBankMasuk
                    f._IsNew = False
                    f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                        GridView1.ClearSelection()
                        GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), f._ID.ToString)
                        GridView1.SelectRow(GridView1.FocusedRowHandle)
                    End If
                End Using
            Case IDFormTr.F_KasKeluar

            Case IDFormTr.F_CalcLabel
                Using f As New FormCalcRibbon
                    f._IsNew = False
                    f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("no"))
                    f.FormName = "Label Calculator"
                    f.TableName = "TLabel"
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                        GridView1.ClearSelection()
                        GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("no"), f._ID.ToString)
                        GridView1.SelectRow(GridView1.FocusedRowHandle)
                    End If
                End Using
            Case IDFormTr.F_CalcRibbon
                Using f As New FormCalcRibbon
                    f._IsNew = False
                    f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("no"))
                    f.FormName = "Ribbon Calculator"
                    f.TableName = "TRibbon"
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                        GridView1.ClearSelection()
                        GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("no"), f._ID.ToString)
                        GridView1.SelectRow(GridView1.FocusedRowHandle)
                    End If
                End Using
            Case IDFormTr.F_CalcTaffeta
                Using f As New FormCalcTaffeta
                    f._IsNew = False
                    f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("no"))
                    f.FormName = "Taffeta Calculator"
                    f.TableName = "TTaffeta"
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                        GridView1.ClearSelection()
                        GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("no"), f._ID.ToString)
                        GridView1.SelectRow(GridView1.FocusedRowHandle)
                    End If
                End Using
            Case IDFormTr.F_CalcPaket
                Using f As New FormCalcPaket
                    f._IsNew = False
                    f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("no"))
                    f.FormName = "Paket Calculator"
                    f.TableName = "TPaket"
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                        GridView1.ClearSelection()
                        GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("no"), f._ID.ToString)
                        GridView1.SelectRow(GridView1.FocusedRowHandle)
                    End If
                End Using
        End Select
    End Sub
    Private Sub BarButtonHapus_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonHapus.ItemClick
        Hapus()
    End Sub

    Sub Hapus()
        Dim ID As Integer = 0
        Dim view As ColumnView = GridControl1.FocusedView
        Select Case idFrmTr
            Case IDFormTr.F_StokMasuk
                ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                Dim f As Pesan = Query.DeleteDataMaster("StokMasuk", "ID=" & ID & "", False)
                If f.Hasil = True Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("no"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                End If
            Case IDFormTr.F_StokKeluar

                ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                Dim f As Pesan = Query.DeleteDataMaster("StokKeluar", "ID=" & ID & "", False)
                If f.Hasil = True Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("no"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                End If
            Case IDFormTr.F_KasMasuk
                ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                Dim f As Pesan = Query.DeleteDataMaster("KasBankMasuk", "ID=" & ID & "", False)
                If f.Hasil = True Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("no"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                End If
            Case IDFormTr.F_KasKeluar
                ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                Dim f As Pesan = Query.DeleteDataMaster("KasBankKeluar", "ID=" & ID & "", False)
                If f.Hasil = True Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("no"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                End If
            Case IDFormTr.F_CalcLabel
                ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("no"))
                Dim f As Pesan = Query.DeleteDataMaster("TLabel", "no=" & ID & "", False)
                If f.Hasil = True Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("no"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                End If
            Case IDFormTr.F_CalcRibbon
                ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("no"))
                Dim f As Pesan = Query.DeleteDataMaster("TRibbon", "no=" & ID & "", False)
                If f.Hasil = True Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("no"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                End If
            Case IDFormTr.F_CalcTaffeta
                ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("no"))
                Dim f As Pesan = Query.DeleteDataMaster("TTaffeta", "no=" & ID & "", False)
                If f.Hasil = True Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("no"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                End If
            Case IDFormTr.F_CalcPaket
                ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("no"))
                Dim f As Pesan = Query.DeleteDataMaster("TPaket", "no=" & ID & "", False)
                If f.Hasil = True Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("no"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                End If
        End Select
    End Sub

    Private Sub BarButtonExport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonExport.ItemClick
        Export()
    End Sub

    Sub Export()
        Using dlgsave As New SaveFileDialog
            dlgsave.Title = "Export Daftar ke Excel"
            dlgsave.Filter = "Excel Files|*.xls"
            If dlgsave.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                GridControl1.ExportToXls(dlgsave.FileName)
                BukaFile(dlgsave.FileName)
            End If
        End Using
    End Sub

    Private Sub BarButtonCetak_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonCetak.ItemClick
        Cetak()
    End Sub

    Sub Cetak()
        Dim action As ActionReport = Nothing
        If (IsEditReport) Then
            action = ActionReport.Edit
        Else
            action = ActionReport.Preview
        End If
        Dim PathFile As String = Application.StartupPath & "\Report\"
        Dim str, operasi As String()
        Dim RgText As String = ""
        Dim CalcRb As String = ""
        Dim CalcField As String = ""

        Dim namafile As String = "Print" & NamaForm & ".repx"
        PathFile &= namafile
        If IO.File.Exists(PathFile) OrElse action = ActionReport.Edit Then
            Refresher()
            str = (CalcRb).ToString().Split("|")
            For i = 0 To UBound(str)
                operasi = str(i).Split(";")
                'If operasi(0) = XtraTab1.SelectedTabPage.Name Then
                '    RgText = operasi(1)
                '    Exit For
                'Else
                '    RgText = "Tampilkan Semua"
                'End If
            Next

            If (CalcField) <> "" Then
                RgText = "|" & "Radio=String='Status: " & RgText & "'"
            Else
                RgText = "" & "Radio=String='Status: " & RgText & "'"
            End If
            clsDXReport.ViewXtraReport(Me.MdiParent, action, PathFile, Me.Text, namafile, GCtoDSRowFiltered(GridControl1), , CalcField & RgText)
            'clsDXReport.ViewXtraReport(Me.MdiParent, action, PathFile, Me.Text, namafile, GCtoDSRowFiltered(GridControl1), , CalcField & RgText)
        Else
            clsDXReport.NewPreview(NamaForm, GridControl1, Me.Text, "Tanggal : " & BarEditTglDari.EditValue.ToString & " s/d " & BarEditTglSampai.EditValue.ToString, GridView1.ActiveFilterString.Replace("[", "").Replace("]", ""))
        End If
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If BarButtonUbah.Enabled Then
            Ubah()
        End If
    End Sub
End Class