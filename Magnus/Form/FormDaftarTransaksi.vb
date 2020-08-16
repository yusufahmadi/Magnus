﻿Imports DevExpress.XtraEditors.Repository
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
        BarEditTglDari.EditValue = DateTime.Now.ToString("yyyy-MM-01")
        BarEditTglSampai.EditValue = DateTime.Now.ToString("yyyy-MM-dd")
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
            MsgBox(NamaForm & " " & ex.Message)
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
        Select Case idFrmTr
            Case IDFormTr.F_StokMasuk
                sql = "Select * From MStokMasuk "
            Case IDFormTr.F_StokKeluar
                sql = "Select * From MStokKeluar "
            Case IDFormTr.F_KasMasuk
                sql = "Select * From MKasMasuk "
            Case IDFormTr.F_KasKeluar
                sql = "Select * From MKasKeluar "
            Case IDFormTr.F_CalcLabel
                If IDRoleUser = 1 Then
                    sql = "Select T.No,T.Tgl,T.Dokumen,B.Nama Bahan,T.harga_bahan HargaBahan ,T.Lebar,T.Tinggi,T.Gap,T.Pisau,T.Pembulatan,T.Qty_Order,T.Jual_Sesuai_Order,(Isnull(T.biaya_pisau,0) + Isnull(T.biaya_tinta,0) + Isnull(T.biaya_toyobo,0) + Isnull(T.biaya_operator,0) + Isnull(T.biaya_kirim,0)) TotalBiaya " & vbCrLf
                Else
                    sql = "Select T.No,T.Tgl,T.Dokumen,B.Nama Bahan,T.Lebar,T.Tinggi,T.Gap,T.Pisau,T.Pembulatan,T.Qty_Order,T.Jual_Sesuai_Order,(Isnull(T.biaya_pisau,0) + Isnull(T.biaya_tinta,0) + Isnull(T.biaya_toyobo,0) + Isnull(T.biaya_operator,0) + Isnull(T.biaya_kirim,0)) TotalBiaya  " & vbCrLf
                End If
                sql = sql & "From TLabel T Left Join MBarang B On B.ID=T.ID_Bahan "
            Case IDFormTr.F_CalcRibbon
                sql = "Select * From TRibbon "
            Case IDFormTr.F_CalcTaffeta
                sql = "Select * From TTaffeta "
            Case IDFormTr.F_CalcTaffeta
        End Select
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
                    ' MsgBox(GV1.Columns(i).fieldname.ToString)
                    Select Case GridView1.Columns(i).ColumnType.Name.ToLower
                        Case "int32", "int64", "int"
                            .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .Columns(i).DisplayFormat.FormatString = "n2"
                        Case "decimal", "single", "money", "double"
                            .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .Columns(i).DisplayFormat.FormatString = "n2"
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
                        Case "boolean"
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

            Case IDFormTr.F_StokKeluar

            Case IDFormTr.F_KasMasuk

            Case IDFormTr.F_KasKeluar

            Case IDFormTr.F_CalcLabel
                Dim f As New FormCalcLabel
                f._IsNew = True
                f.FormName = "Label Calculator"
                f.TableName = "TLabel"
                If f.ShowDialog() = DialogResult.OK Then
                    BarButtonRefresh.PerformClick()
                End If
            Case IDFormTr.F_CalcRibbon
                Dim f As New FormCalcLabel
                f._IsNew = True
                f.FormName = "Ribbon Calculator"
                f.TableName = "TRibbon"
                If f.ShowDialog() = DialogResult.OK Then
                    BarButtonRefresh.PerformClick()
                End If
            Case IDFormTr.F_CalcTaffeta
                Dim f As New FormCalcLabel
                f._IsNew = True
                f.FormName = "Taffeta Calculator"
                f.TableName = "Ttaffeta"
                If f.ShowDialog() = DialogResult.OK Then
                    BarButtonRefresh.PerformClick()
                End If
            Case IDFormTr.F_CalcPaket
                Dim f As New FormCalcLabel
                f._IsNew = True
                f.FormName = "Paket Calculator"
                f.TableName = "TPaket"
                If f.ShowDialog() = DialogResult.OK Then
                    BarButtonRefresh.PerformClick()
                End If
        End Select
    End Sub

    Private Sub BarButtonUbah_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonUbah.ItemClick
        Ubah()
    End Sub

    Sub Ubah()
        Dim view As ColumnView = GridControl1.FocusedView
        Select Case idFrmTr
            Case IDFormTr.F_StokMasuk

            Case IDFormTr.F_StokKeluar

            Case IDFormTr.F_KasMasuk

            Case IDFormTr.F_KasKeluar

            Case IDFormTr.F_CalcLabel
                Dim f As New FormCalcLabel
                f._IsNew = False
                f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("no"))
                f.FormName = "Label Calculator"
                f.TableName = "TLabel"
                If f.ShowDialog() = DialogResult.OK Then
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), f._ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                End If
            Case IDFormTr.F_CalcRibbon
                Dim f As New FormCalcLabel
                f._IsNew = False
                f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("no"))
                f.FormName = "Label Calculator"
                f.TableName = "TLabel"
                If f.ShowDialog() = DialogResult.OK Then
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), f._ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                End If
            Case IDFormTr.F_CalcTaffeta
                Dim f As New FormCalcLabel
                f._IsNew = False
                f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("no"))
                f.FormName = "Label Calculator"
                f.TableName = "TLabel"
                If f.ShowDialog() = DialogResult.OK Then
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), f._ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                End If
            Case IDFormTr.F_CalcPaket
                Dim f As New FormCalcLabel
                f._IsNew = False
                f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("no"))
                f.FormName = "Label Calculator"
                f.TableName = "TLabel"
                If f.ShowDialog() = DialogResult.OK Then
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), f._ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                End If
        End Select
    End Sub
    Private Sub BarButtonHapus_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonHapus.ItemClick
        Hapus()
    End Sub

    Sub Hapus()
        Dim view As ColumnView = GridControl1.FocusedView
        Dim ID As Integer = 0
        ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("no"))
        Select Case idFrmTr
            Case IDFormTr.F_StokMasuk

            Case IDFormTr.F_StokKeluar

            Case IDFormTr.F_KasMasuk

            Case IDFormTr.F_KasKeluar

            Case IDFormTr.F_CalcLabel
                Dim f As Pesan = Query.DeleteDataMaster("TLabel", "no=" & ID & "", False)
                If f.Hasil = True Then
                    MsgBox(f.Message)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    MsgBox(f.Message)
                End If
            Case IDFormTr.F_CalcRibbon
                Dim f As Pesan = Query.DeleteDataMaster("TRibbon", "no=" & ID & "", False)
                If f.Hasil = True Then
                    MsgBox(f.Message)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    MsgBox(f.Message)
                End If
            Case IDFormTr.F_CalcTaffeta
                Dim f As Pesan = Query.DeleteDataMaster("TTaffeta", "no=" & ID & "", False)
                If f.Hasil = True Then
                    MsgBox(f.Message)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    MsgBox(f.Message)
                End If
            Case IDFormTr.F_CalcPaket
                Dim f As Pesan = Query.DeleteDataMaster("TPaket", "no=" & ID & "", False)
                If f.Hasil = True Then
                    MsgBox(f.Message)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    MsgBox(f.Message)
                End If
        End Select
    End Sub

    Private Sub BarButtonExport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonExport.ItemClick
        Export()
    End Sub

    Sub Export()
        Dim dlgsave As New SaveFileDialog
        dlgsave.Title = "Export Daftar ke Excel"
        dlgsave.Filter = "Excel Files|*.xls"
        If dlgsave.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            GridControl1.ExportToXls(dlgsave.FileName)
            BukaFile(dlgsave.FileName)
        End If
        dlgsave.Dispose()
    End Sub

    Private Sub BarButtonCetak_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonCetak.ItemClick
        Cetak()
    End Sub

    Sub Cetak()
        Dim action As ActionReport = IIf(IsEditReport, ActionReport.Edit, ActionReport.Preview)
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
            RgText = IIf(CalcField <> "", "|", "") & "Radio=String='Status: " & RgText & "'"
            clsDXReport.ViewXtraReport(Me.MdiParent, action, PathFile, Me.Text, namafile, GCtoDSRowFiltered(GridControl1), , CalcField & RgText)
        Else
            clsDXReport.NewPreview(NamaForm, GridControl1, Me.Text)
        End If
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        Ubah()
    End Sub
End Class