Imports DevExpress.XtraLayout
Imports DevExpress.XtraLayout.Helpers
Imports System.ComponentModel.DataAnnotations
Imports System.IO
Imports Magnus.Utils


Partial Public Class FormCalcPaket
    Public _IsNew As Boolean
    Public _ID As Integer = 0
    Public FormName As String = "Undefined"
    Public TableName As String = "TTaffeta"
    Public Sub New()
        InitializeComponent()
    End Sub

    Sub LoadDataAdapter()
        'Dim str As String = "", IDKategori As Integer = 0
        'str = "Select Df_IDKategoriBahanTaffeta From MSettingCalc"
        'IDKategori = ObjToInt(Query.ExecuteScalar(str))
        'str = "Select ID,Kode,Nama From MBarang Where IDKategori=" & IDKategori
        'txtIDBahan.Properties.DataSource = Query.ExecuteDataSet(str).Tables(0)
        'txtIDBahan.Properties.DisplayMember = "Nama"
        'txtIDBahan.Properties.ValueMember = "ID"
        txtTanggal.EditValue = Now
        If IDRoleUser = 1 Then
        Else
            LayoutControlItemHargaInc.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            LayoutControlItemModal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
    Private Sub FormBasic_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDataAdapter()
        Me.Text = FormName
        If _IsNew Then
            ClearData()
        Else
            Dim ds As New DataSet
            ds = Query.ExecuteDataSet("Select * from " & TableName & " WHere [no]='" & Me._ID & "'")
            If Not ds Is Nothing Then
                With ds.Tables(0).Rows(0)
                    txtDocument.Text = .Item("dokumen").ToString
                    txtTanggal.EditValue = ObjToDate(.Item("tgl"))

                    editTextcustomer_minta_bikin_jadinya_line.EditValue = ObjToDbl(.Item("customer_minta_bikin_jadinya_line"))
                    editTextqty_order_customer_pcs.EditValue = ObjToDbl(.Item("qty_order_customer_pcs"))
                    editTextisi_roll.EditValue = ObjToDbl(.Item("isi_roll"))

                    editTextLebar.EditValue = ObjToDbl(.Item("lebar"))
                    editTexttinggi.EditValue = ObjToDbl(.Item("tinggi"))
                    editTextpisau_yang_digunakan.EditValue = ObjToDbl(.Item("pisau_yang_digunakan"))

                    editTextdibulatkan.EditValue = ObjToDbl(.Item("dibulatkan"))
                    editTextlebar_ribbon.EditValue = ObjToDbl(.Item("lebar_ribbon"))
                    editTextpanjang_ribbon.EditValue = ObjToDbl(.Item("panjang_ribbon"))
                    Hitung()
                End With
                ds.Dispose()
            End If
        End If
    End Sub
    Sub Hitung()
        Dim xtotal_jadi_roll As Double = 0.0, xlebar_bahan As Double = 0.0, x1roll_jadiny_pcs As Double = 0.0,
            xjadi_belanja_bahan_baku_label_dalam_Roll As Double = 0.0, KomisiSalesNominal As Double = 0.0, x1roll_ribbon_bisa_cetak_pcs As Double = 0.0, xmaka_kebutuhan_ribbonnya As Double = 0.0
        Try

            xtotal_jadi_roll = ObjToDbl(editTextqty_order_customer_pcs.EditValue) / ObjToDbl(editTextisi_roll.EditValue)
            editTextxtotal_jadi_roll.EditValue = ObjToDbl(xtotal_jadi_roll)

            xlebar_bahan = (ObjToDbl(editTextLebar.EditValue) * ObjToDbl(editTextpisau_yang_digunakan.EditValue)) +
                    (3 * (ObjToDbl(editTextpisau_yang_digunakan.EditValue) - 1) + 8)
            editTextxlebar_bahan.EditValue = ObjToDbl(xlebar_bahan)

            x1roll_jadiny_pcs = (975000 / (ObjToDbl(editTexttinggi.EditValue) + 3) * (ObjToDbl(editTextpisau_yang_digunakan.EditValue)))
            editTextx1roll_jadiny_pcs.EditValue = ObjToDbl(x1roll_jadiny_pcs)

            xjadi_belanja_bahan_baku_label_dalam_Roll = ObjToDbl(editTextqty_order_customer_pcs.EditValue) / ObjToDbl(editTextx1roll_jadiny_pcs.EditValue)
            editTextxjadi_belanja_bahan_baku_label_dalam_Roll.EditValue = ObjToDbl(xjadi_belanja_bahan_baku_label_dalam_Roll)

            x1roll_ribbon_bisa_cetak_pcs = (ObjToDbl(editTextpanjang_ribbon.EditValue) * 1000) / (ObjToDbl(editTexttinggi.EditValue) + 3) * ObjToDbl(editTextcustomer_minta_bikin_jadinya_line.EditValue)
            editTextx1roll_ribbon_bisa_cetak_pcs.EditValue = ObjToDbl(x1roll_ribbon_bisa_cetak_pcs)

            xmaka_kebutuhan_ribbonnya = ObjToDbl(editTextqty_order_customer_pcs.EditValue) / ObjToDbl(editTextx1roll_ribbon_bisa_cetak_pcs.EditValue)
            editTextxmaka_kebutuhan_ribbonnya.EditValue = ObjToDbl(xmaka_kebutuhan_ribbonnya)
        Catch e As Exception
        End Try
    End Sub
    Private Sub bbiSave_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiSave.ItemClick
        If SaveData() Then
            _IsNew = False
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
        txtDocument.Text = ""
        txtDocument.Focus()
        txtTanggal.EditValue = Now

        editTextcustomer_minta_bikin_jadinya_line.EditValue = 0.0
        editTextqty_order_customer_pcs.EditValue = 0.0
        editTextisi_roll.EditValue = 0.0

        editTextLebar.EditValue = 0.0
        editTexttinggi.EditValue = 0.0
        editTextpisau_yang_digunakan.EditValue = 0.0

        editTextdibulatkan.EditValue = 0.0
        editTextlebar_ribbon.EditValue = 0.0
        editTextpanjang_ribbon.EditValue = 0.0
    End Sub

    Private Sub txtIDBahan_EditValueChanged(sender As Object, e As EventArgs)
        Try
            'If ObjToInt(txtIDBahan.EditValue) > 0 Then
            '    Dim Sql As String = "Select HargaBeli From MBarang Where ID=" & txtIDBahan.EditValue.ToString
            '    editTextcustomer_minta_bikin_jadinya_line.EditValue = ObjToDbl(Query.ExecuteScalar(Sql))
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Function IsValid() As Boolean
        Dim res As Boolean = True
        If txtTanggal.Text = "" Then
            txtTanggal.Focus()
            res = False
            Exit Function
        End If
        'If txtIDBahan.Text = "" Then
        '    txtIDBahan.Focus()
        '    res = False
        '    Exit Function
        'End If
        If ObjToInt(editTextLebar.EditValue) = 0 Then
            editTextLebar.Focus()
            res = False
            Exit Function
        End If
        If ObjToInt(editTexttinggi.EditValue) = 0 Then
            editTexttinggi.Focus()
            res = False
            Exit Function
        End If

        Return res
    End Function
    Function IsValidOnDB() As Boolean
        Dim res As Boolean = True
        If txtDocument.Text.Trim <> "" AndAlso CInt(Query.ExecuteScalar("Select Count(*) From " & TableName & " Where [no]<> " & Me._ID & " AND [dokumen] ='" & txtDocument.Text.Trim & "'")) > 0 Then
            DevExpress.XtraEditors.XtraMessageBox.Show("No Dokumen sudah terpakai.", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDocument.Focus()
            res = False
            Exit Function
        End If
        Return res
    End Function
    Function SaveData() As Boolean
        Dim e As New Pesan With {.Hasil = False, .Message = "", .Value = Nothing}
        If IsValid() AndAlso IsValidOnDB() Then
            Dim sql As String = ""
            Try
                If _IsNew Then
                    sql = "Select Isnull(Max(no),0)+1 From " & TableName
                    Me._ID = ObjToInt(Query.ExecuteScalar(sql))
                    sql = "Insert Into " & TableName & " (
                            [no]
                           ,[dokumen]
                           ,[tgl]
                           ,[customer_minta_bikin_jadinya_line]
                            ,[qty_order_customer_pcs]
                            ,[isi_roll]
                            ,[lebar]
                            ,[tinggi]
                            ,[pisau_yang_digunakan]
                            ,[dibulatkan]
                            ,[lebar_ribbon]
                            ,[panjang_ribbon]) " & vbCrLf &
                            " Values (" & Me._ID & "," & vbCrLf &
                            "'" & FixApostropi(txtDocument.Text.Trim) & "'," & vbCrLf &
                            "" & ObjToStrDateSql(txtTanggal.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextcustomer_minta_bikin_jadinya_line.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextqty_order_customer_pcs.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextisi_roll.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextLebar.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTexttinggi.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextpisau_yang_digunakan.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextdibulatkan.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextlebar_ribbon.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextpanjang_ribbon.EditValue) & ")"
                Else
                    sql = "Update " & TableName & " Set " & vbCrLf &
                          " [dokumen] = '" & FixApostropi(txtDocument.Text.Trim) & "'," & vbCrLf &
                            "[tgl] = " & ObjToStrDateSql(txtTanggal.EditValue) & "," & vbCrLf &
                            "[customer_minta_bikin_jadinya_line] = " & ObjToDbl(editTextcustomer_minta_bikin_jadinya_line.EditValue) & "," & vbCrLf &
                            "[qty_order_customer_pcs] = " & ObjToDbl(editTextqty_order_customer_pcs.EditValue) & "," & vbCrLf &
                            "[isi_roll] = " & ObjToDbl(editTextisi_roll.EditValue) & "," & vbCrLf &
                            "[lebar]= " & ObjToDbl(editTextLebar.EditValue) & "," & vbCrLf &
                            "[tinggi] = " & ObjToDbl(editTexttinggi.EditValue) & "," & vbCrLf &
                            "[pisau_yang_digunakan] = " & ObjToDbl(editTextpisau_yang_digunakan.EditValue) & "," & vbCrLf &
                            "[dibulatkan] = " & ObjToDbl(editTextdibulatkan.EditValue) & "," & vbCrLf &
                            "[lebar_ribbon] = " & ObjToDbl(editTextlebar_ribbon.EditValue) & "," & vbCrLf &
                            "[panjang_ribbon] = " & ObjToDbl(editTextpanjang_ribbon.EditValue) & " " & vbCrLf &
                            " Where [no] ='" & Me._ID & "'"
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
        Dim f As Pesan = Query.DeleteDataMaster("TTaffeta", "[no]=" & Me._ID)
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

    Private Sub editTextLebar_EditValueChanged(sender As Object, e As EventArgs) Handles editTextLebar.EditValueChanged, editTexttinggi.EditValueChanged
        Hitung()
    End Sub

    Private Sub editTextQtyOrderPcs_EditValueChanged(sender As Object, e As EventArgs) Handles editTextxlebar_bahan.EditValueChanged, editTextx1roll_jadiny_pcs.EditValueChanged
        Hitung()
    End Sub

    Private Sub editTextBiayaToyobo_EditValueChanged(sender As Object, e As EventArgs) Handles editTextdibulatkan.EditValueChanged, editTextpisau_yang_digunakan.EditValueChanged
        Hitung()
    End Sub

    Private Sub editTextJualSesuaiOrder_EditValueChanged(sender As Object, e As EventArgs) 
        Hitung()
    End Sub

    Private Sub editTextcustomer_minta_bikin_jadinya_line_EditValueChanged(sender As Object, e As EventArgs) Handles editTextcustomer_minta_bikin_jadinya_line.EditValueChanged
        Hitung()
    End Sub

    Private Sub editTextqty_order_customer_pcs_EditValueChanged(sender As Object, e As EventArgs) Handles editTextqty_order_customer_pcs.EditValueChanged
        Hitung()
    End Sub

    Private Sub editTextisi_roll_EditValueChanged(sender As Object, e As EventArgs) Handles editTextisi_roll.EditValueChanged
        Hitung()
    End Sub

    Private Sub editTextlebar_ribbon_EditValueChanged(sender As Object, e As EventArgs) Handles editTextlebar_ribbon.EditValueChanged
        Hitung()
    End Sub

    Private Sub editTextpanjang_ribbon_EditValueChanged(sender As Object, e As EventArgs) Handles editTextpanjang_ribbon.EditValueChanged
        Hitung()
    End Sub

    'Private Sub textView30Persen_DoubleClick(sender As Object, e As EventArgs) Handles textView10Persen.DoubleClick
    '    editTextJualSesuaiOrder.EditValue = textView10Persen.EditValue
    '    TabbedControlGroupProfit.SelectedTabPageIndex = 0
    '    editTextJualSesuaiOrder.Focus()
    'End Sub

    'Private Sub textView50Persen_DoubleClick(sender As Object, e As EventArgs) Handles textView15Persen.DoubleClick
    '    editTextJualSesuaiOrder.EditValue = textView15Persen.EditValue
    '    TabbedControlGroupProfit.SelectedTabPageIndex = 0
    '    editTextJualSesuaiOrder.Focus()
    'End Sub

    'Private Sub textView75Persen_DoubleClick(sender As Object, e As EventArgs) Handles textView25Persen.DoubleClick
    '    editTextJualSesuaiOrder.EditValue = textView25Persen.EditValue
    'End Sub

    'Private Sub textView100Persen_DoubleClick(sender As Object, e As EventArgs) Handles textView35Persen.DoubleClick
    '    editTextJualSesuaiOrder.EditValue = textView35Persen.EditValue
    '    TabbedControlGroupProfit.SelectedTabPageIndex = 0
    '    editTextJualSesuaiOrder.Focus()
    'End Sub

    'Private Sub textView125Persen_DoubleClick(sender As Object, e As EventArgs) Handles textView45Persen.DoubleClick
    '    editTextJualSesuaiOrder.EditValue = textView45Persen.EditValue
    '    TabbedControlGroupProfit.SelectedTabPageIndex = 0
    '    editTextJualSesuaiOrder.Focus()
    'End Sub

    'Private Sub textView150Persen_DoubleClick(sender As Object, e As EventArgs) Handles textView55Persen.DoubleClick
    '    editTextJualSesuaiOrder.EditValue = textView55Persen.EditValue
    '    TabbedControlGroupProfit.SelectedTabPageIndex = 0
    '    editTextJualSesuaiOrder.Focus()
    'End Sub

    'Private Sub textView175Persen_DoubleClick(sender As Object, e As EventArgs) Handles textView65Persen.DoubleClick
    '    editTextJualSesuaiOrder.EditValue = textView65Persen.EditValue
    '    TabbedControlGroupProfit.SelectedTabPageIndex = 0
    '    editTextJualSesuaiOrder.Focus()
    'End Sub

    'Private Sub textView200Persen_DoubleClick(sender As Object, e As EventArgs) Handles textView75Persen.DoubleClick
    '    editTextJualSesuaiOrder.EditValue = textView75Persen.EditValue
    '    TabbedControlGroupProfit.SelectedTabPageIndex = 0
    '    editTextJualSesuaiOrder.Focus()
    'End Sub

    'Private Sub textView225Persen_DoubleClick(sender As Object, e As EventArgs) Handles textView85Persen.DoubleClick
    '    editTextJualSesuaiOrder.EditValue = textView85Persen.EditValue
    '    TabbedControlGroupProfit.SelectedTabPageIndex = 0
    '    editTextJualSesuaiOrder.Focus()
    'End Sub
End Class
