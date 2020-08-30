﻿Imports DevExpress.XtraLayout
Imports DevExpress.XtraLayout.Helpers
Imports System.ComponentModel.DataAnnotations
Imports System.IO
Imports Magnus.Utils


Partial Public Class FormCalcRibbon
    Public _IsNew As Boolean
    Public _ID As Integer = 0
    Public FormName As String = "Undefined"
    Public TableName As String = "TRibbon"
    Public Sub New()
        InitializeComponent()
    End Sub

    Sub LoadDataAdapter()
        Dim str As String = "", IDKategori As Integer = 0
        str = "Select Df_IDKategoriBahanRibbon From MSettingCalc"
        IDKategori = ObjToInt(Query.ExecuteScalar(str))
        str = "Select ID,Kode,Nama From MBarang Where IDKategori=" & IDKategori
        txtIDBahan.Properties.DataSource = Query.ExecuteDataSet(str).Tables(0)
        txtIDBahan.Properties.DisplayMember = "Nama"
        txtIDBahan.Properties.ValueMember = "ID"
        txtTanggal.EditValue = Now
        If IDRoleUser = 1 Then
        Else
            LayoutControlGroupMarginInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            LayoutControlItemHargaInc.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            LayoutControlItemModal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            LayoutControlItemNetProfit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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

                    txtIDBahan.EditValue = ObjToInt(.Item("id_bahan"))
                    editTextHargaModal.EditValue = ObjToDbl(.Item("harga_bahan"))
                    editTextLebar.EditValue = ObjToDbl(.Item("lebar"))
                    editTextPanjang.EditValue = ObjToDbl(.Item("panjang"))
                    editTextModal.EditValue = ObjToDbl(.Item("modal"))
                    editTextQty.EditValue = ObjToDbl(.Item("qty"))
                    editTextJualRoll.EditValue = ObjToDbl(.Item("jual_roll"))
                    editTextJumlahProfitKotor.EditValue = ObjToDbl(.Item("jumlah_profit_kotor"))
                    editTextTransport.EditValue = ObjToDbl(.Item("transport"))
                    editTextKomisiSalesProsen.EditValue = ObjToDbl(.Item("komisisalesprosen"))
                    editTextNetProfit.EditValue = ObjToDbl(.Item("netprofit"))
                    Hitung()
                End With
                ds.Dispose()
            End If
        End If
    End Sub
    Sub Hitung()
        Dim modalperroll As Double = 0.0, profitkotor As Double = 0.0, netprofit As Double = 0.0, KomisiSalesNominal As Double = 0.0
        Try
            modalperroll = (ObjToDbl(editTextLebar.EditValue) / 1000) * ObjToDbl(editTextPanjang.EditValue) * ObjToDbl(editTextHargaModal.EditValue)
            editTextModal.EditValue = ObjToDbl(modalperroll)
            textView10Persen.EditValue = ObjToDbl(modalperroll * 1.1)
            textView15Persen.EditValue = ObjToDbl(modalperroll * 1.15)
            textView25Persen.EditValue = ObjToDbl(modalperroll * 1.25)
            textView35Persen.EditValue = ObjToDbl(modalperroll * 1.35)
            textView45Persen.EditValue = ObjToDbl(modalperroll * 1.45)
            textView55Persen.EditValue = ObjToDbl(modalperroll * 1.55)
            textView65Persen.EditValue = ObjToDbl(modalperroll * 1.65)
            textView75Persen.EditValue = ObjToDbl(modalperroll * 1.75)
            profitkotor = (ObjToDbl(editTextJualRoll.EditValue) - modalperroll) * ObjToDbl(editTextQty.EditValue)
            editTextJumlahProfitKotor.EditValue = ObjToDbl(profitkotor)
            KomisiSalesNominal = ObjToDbl(editTextJumlahProfitKotor.EditValue) * ObjToDbl(editTextKomisiSalesProsen.EditValue) / 100
            editTextKomisiSalesNominal.EditValue = ObjToDbl(KomisiSalesNominal)
            netprofit = profitkotor - ObjToDbl(editTextTransport.EditValue) - ObjToDbl(editTextKomisiSalesNominal.EditValue)
            editTextNetProfit.EditValue = ObjToDbl(netprofit)
        Catch e As Exception
        End Try
    End Sub
    Private Sub bbiSave_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiSave.ItemClick
        If SaveData() Then
            _IsNew = False
            d = DialogResult.OK
        End If
    End Sub

    Private Sub bbiSaveAndClose_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiSaveAndClose.ItemClick
        If SaveData() Then
            d = DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub bbiSaveAndNew_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiSaveAndNew.ItemClick
        If SaveData() Then
            Me._IsNew = True
            Me._ID = 0
            ClearData()
            d = DialogResult.OK
            Me.FormBasic_Load(sender, e)
        End If
    End Sub

    Sub ClearData()
        txtDocument.Text = ""
        txtDocument.Focus()
        txtTanggal.EditValue = Now

        editTextModal.Text = ""
        txtIDBahan.EditValue = 0
        editTextHargaModal.EditValue = 0.0
        editTextLebar.EditValue = 0.0
        editTextPanjang.EditValue = 0.0

        editTextJualRoll.EditValue = 0.0
        editTextQty.EditValue = 0.0

        editTextKomisiSalesProsen.EditValue = 0.0
        editTextKomisiSalesNominal.EditValue = 0.0

        editTextTransport.EditValue = 0.0
    End Sub

    Private Sub txtIDBahan_EditValueChanged(sender As Object, e As EventArgs) Handles txtIDBahan.EditValueChanged
        Try
            If ObjToInt(txtIDBahan.EditValue) > 0 Then
                Dim Sql As String = "Select HargaBeli From MBarang Where ID=" & txtIDBahan.EditValue.ToString
                editTextHargaModal.EditValue = ObjToDbl(Query.ExecuteScalar(Sql))
            End If
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
        If txtIDBahan.Text = "" Then
            txtIDBahan.Focus()
            res = False
            Exit Function
        End If
        If ObjToInt(editTextLebar.EditValue) = 0 Then
            editTextLebar.Focus()
            res = False
            Exit Function
        End If
        If ObjToInt(editTextPanjang.EditValue) = 0 Then
            editTextPanjang.Focus()
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
                           ,[id_bahan]
                           ,[harga_bahan]
                           ,[lebar]
                           ,[panjang]
                           ,[modal]
                           ,[qty]
                           ,[jual_roll]
                           ,[jumlah_profit_kotor]
                           ,[transport]
                           ,[komisisalesprosen]
                           ,[netprofit]) " & vbCrLf &
                            " Values (" & Me._ID & "," & vbCrLf &
                            "'" & FixApostropi(txtDocument.Text.Trim) & "'," & vbCrLf &
                            "" & ObjToStrDateSql(txtTanggal.EditValue) & "," & vbCrLf &
                            ObjToInt(txtIDBahan.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextHargaModal.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextLebar.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextPanjang.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextModal.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextQty.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextJualRoll.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextJumlahProfitKotor.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextTransport.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextKomisiSalesProsen.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextNetProfit.EditValue) & ")"
                Else
                    sql = "Update " & TableName & " Set " & vbCrLf &
                          " [dokumen] = '" & FixApostropi(txtDocument.Text.Trim) & "'," & vbCrLf &
                            "[tgl] = " & ObjToStrDateSql(txtTanggal.EditValue) & "," & vbCrLf &
                            "[id_bahan] = " & ObjToInt(txtIDBahan.EditValue) & "," & vbCrLf &
                            "[harga_bahan] = " & ObjToDbl(editTextHargaModal.EditValue) & "," & vbCrLf &
                            "[lebar] = " & ObjToDbl(editTextLebar.EditValue) & "," & vbCrLf &
                            "[panjang] = " & ObjToDbl(editTextPanjang.EditValue) & "," & vbCrLf &
                            "[modal] = " & ObjToDbl(editTextModal.EditValue) & "," & vbCrLf &
                            "[qty] = " & ObjToDbl(editTextQty.EditValue) & "," & vbCrLf &
                            "[jual_roll] = " & ObjToDbl(editTextJualRoll.EditValue) & "," & vbCrLf &
                            "[jumlah_profit_kotor] = " & ObjToDbl(editTextJumlahProfitKotor.EditValue) & "," & vbCrLf &
                            "[komisisalesprosen] = " & ObjToDbl(editTextKomisiSalesProsen.EditValue) & "," & vbCrLf &
                            "[transport] = " & ObjToDbl(editTextTransport.EditValue) & ", " & vbCrLf &
                            "[netprofit] = " & ObjToDbl(editTextNetProfit.EditValue) & " " & vbCrLf &
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
            d = DialogResult.OK
            Me.Close()
        Else
            DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
        End If
    End Sub

    Private Sub bbiClose_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiClose.ItemClick
        Me.Close()
    End Sub

    Private Sub editTextLebar_EditValueChanged(sender As Object, e As EventArgs) Handles editTextLebar.EditValueChanged, editTextPanjang.EditValueChanged
        Hitung()
    End Sub

    Private Sub editTextQtyOrderPcs_EditValueChanged(sender As Object, e As EventArgs) Handles editTextQty.EditValueChanged, editTextJualRoll.EditValueChanged
        Hitung()
    End Sub

    Private Sub editTextBiayaToyobo_EditValueChanged(sender As Object, e As EventArgs) Handles editTextTransport.EditValueChanged, editTextKomisiSalesProsen.EditValueChanged, editTextKomisiSalesNominal.EditValueChanged
        Hitung()
    End Sub

    Private Sub editTextJualSesuaiOrder_EditValueChanged(sender As Object, e As EventArgs) 
        Hitung()
    End Sub
    Dim d As DialogResult
    Private Sub FormCalcRibbon_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        DialogResult = IIf(d = DialogResult.None, DialogResult.Cancel, d)
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
