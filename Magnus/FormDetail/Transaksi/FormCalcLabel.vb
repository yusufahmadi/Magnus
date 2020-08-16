Imports DevExpress.XtraLayout
Imports DevExpress.XtraLayout.Helpers
Imports System.ComponentModel.DataAnnotations
Imports System.IO
Imports Magnus.Utils


Partial Public Class FormCalcLabel
    Public _IsNew As Boolean
    Public _ID As Integer = 0
    Public FormName As String = "Undefined"
    Public TableName As String = "TLabel"
    Public Sub New()
        InitializeComponent()
    End Sub

    Sub LoadDataAdapter()
        txtIDBahan.Properties.DataSource = Query.ExecuteDataSet("Select ID,Kode,Nama From MBarang Where IDKategori=1").Tables(0)
        txtIDBahan.Properties.DisplayMember = "Nama"
        txtIDBahan.Properties.ValueMember = "ID"
        txtTanggal.EditValue = Now
        If IDRoleUser = 1 Then
        Else
            LayoutControlItemHargaInc.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            TabbedControlGroupProfit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
                    editTextTinggi.EditValue = ObjToDbl(.Item("tinggi"))
                    editTextGap.EditValue = ObjToDbl(.Item("gap"))
                    editTextPisau.EditValue = ObjToDbl(.Item("pisau"))
                    editTextPembulatanRoll.EditValue = ObjToDbl(.Item("pembulatan"))
                    editTextQtyOrderPcs.EditValue = ObjToDbl(.Item("qty_order"))
                    editTextJualSesuaiOrder.EditValue = ObjToDbl(.Item("jual_sesuai_order"))
                    editTextBiayaPisau.EditValue = ObjToDbl(.Item("biaya_pisau"))
                    editTextBiayaTinta.EditValue = ObjToDbl(.Item("biaya_tinta"))
                    editTextBiayaToyobo.EditValue = ObjToDbl(.Item("biaya_toyobo"))
                    editTextBiayaOperator.EditValue = ObjToDbl(.Item("biaya_operator"))
                    editTextBiayaKirim.EditValue = ObjToDbl(.Item("biaya_kirim"))
                    Hitung()
                End With
                ds.Dispose()
            End If
        End If
    End Sub
    Sub Hitung()
        Dim Pembulatan As Double = 0, RollBahan As Double = 0.0, Kebutuhannya As Double = 0.0, SaranPcs As Double = 0.0, LebarBahanBelanjanya As Double = 0.0, ModalBahanUtuh As Double = 0.0, ModalperPcs As Double = 0.0, TotalBiaya As Double = 0.0, NetProfit1 As Double = 0.0, NetProfit2 As Double = 0.0
        Try
            LebarBahanBelanjanya = (ObjToDbl(editTextLebar.EditValue) * ObjToDbl(editTextPisau.EditValue)) + 8 + ((ObjToDbl(editTextPisau.EditValue) - 1) * ObjToDbl(editTextGap.EditValue))
            editTextLebarBahanBelanja.EditValue = ObjToDbl(LebarBahanBelanjanya)

            If ObjToDbl(editTextTinggi.EditValue) + ObjToDbl(editTextGap.EditValue) = 0.0 Then
                RollBahan = 0.0
            Else
                RollBahan = 975000.0 / (ObjToDbl(editTextTinggi.EditValue) + ObjToDbl(editTextGap.EditValue)) * ObjToDbl(editTextPisau.EditValue)
            End If

            editText1RollPcs.EditValue = ObjToDbl(RollBahan)
            If (RollBahan = 0) Then
                Kebutuhannya = 0.0
            Else
                Kebutuhannya = ObjToDbl(editTextQtyOrderPcs.EditValue) / RollBahan
            End If
            editTextKebutuhanRoll.EditValue = ObjToDbl(Kebutuhannya)

            Pembulatan = ObjToDbl(editTextPembulatanRoll.EditValue)
            SaranPcs = RollBahan * Pembulatan
            editTextSaranOrderPcs.EditValue = ObjToDbl(SaranPcs)
            ModalBahanUtuh = ObjToDbl(editTextHargaModal.EditValue) * LebarBahanBelanjanya * Pembulatan
            editTextModalBahanUtuh.EditValue = ObjToDbl(ModalBahanUtuh)

            ModalperPcs = ObjToDbl(editTextQtyOrderPcs.EditValue)
            ModalperPcs = IIf(ModalperPcs = 0.0, 0.0, ModalBahanUtuh / ModalperPcs)

            editTextModalPerPcs.EditValue = ObjToDbl(ModalperPcs)
            textView30Persen.EditValue = ObjToDbl(ModalperPcs * (1 + 0.3))
            textView50Persen.EditValue = ObjToDbl(ModalperPcs * (1 + 0.5))
            textView75Persen.EditValue = ObjToDbl(ModalperPcs * (1 + 0.75))
            textView100Persen.EditValue = ObjToDbl(ModalperPcs * (1 + 1.0))
            textView125Persen.EditValue = ObjToDbl(ModalperPcs * (1 + 1.25))
            textView150Persen.EditValue = ObjToDbl(ModalperPcs * (1 + 1.5))
            textView175Persen.EditValue = ObjToDbl(ModalperPcs * (1 + 1.75))
            textView200Persen.EditValue = ObjToDbl(ModalperPcs * (1 + 2.0))
            textView225Persen.EditValue = ObjToDbl(ModalperPcs * (1 + 2.25))

            TotalBiaya = ObjToDbl(editTextBiayaPisau.EditValue) + ObjToDbl(editTextBiayaTinta.EditValue) + ObjToDbl(editTextBiayaToyobo.EditValue) + ObjToDbl(editTextBiayaOperator.EditValue) + ObjToDbl(editTextBiayaKirim.EditValue)
            editTextBiayaTotal.EditValue = ObjToDbl(TotalBiaya)
            NetProfit1 = ((ObjToDbl(editTextJualSesuaiOrder.EditValue) - ModalperPcs) * ObjToDbl(editTextQtyOrderPcs.EditValue)) - TotalBiaya
            editTextJualSesuaiSaran.EditValue = ObjToDbl(editTextJualSesuaiOrder.EditValue)
            NetProfit2 = ((ObjToDbl(editTextJualSesuaiSaran.EditValue) - ModalperPcs) * ObjToDbl(editTextSaranOrderPcs.EditValue)) - TotalBiaya
            editTextProfit1.EditValue = ObjToDbl(NetProfit1)
            editTextProfit2.EditValue = ObjToDbl(NetProfit2)
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
        editText1RollPcs.Text = ""
        editTextLebarBahanBelanja.Text = ""
        txtIDBahan.EditValue = 0
        editTextHargaModal.EditValue = 0.0
        editTextLebar.EditValue = 0.0
        editTextTinggi.EditValue = 0.0
        editTextGap.EditValue = 0.0
        editTextPisau.EditValue = 0.0
        editTextPembulatanRoll.EditValue = 0.0
        editTextQtyOrderPcs.EditValue = 0.0
        editTextJualSesuaiOrder.EditValue = 0.0
        editTextBiayaPisau.EditValue = 0.0
        editTextBiayaTinta.EditValue = 0.0
        editTextBiayaToyobo.EditValue = 0.0
        editTextBiayaOperator.EditValue = 0.0
        editTextBiayaKirim.EditValue = 0.0
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
        If editTextLebar.EditValue = 0 Then
            editTextLebar.Focus()
            res = False
            Exit Function
        End If
        If editTextTinggi.EditValue = 0 Then
            editTextTinggi.Focus()
            res = False
            Exit Function
        End If
        If editTextGap.EditValue = 0 Then
            editTextGap.Focus()
            res = False
            Exit Function
        End If
        If editTextPisau.EditValue = 0 Then
            editTextPisau.Focus()
            res = False
            Exit Function
        End If
        Return res
    End Function
    Function IsValidOnDB() As Boolean
        Dim res As Boolean = True
        If txtDocument.Text.Trim <> "" AndAlso CInt(Query.ExecuteScalar("Select Count(*) From " & TableName & " Where [no]<> " & Me._ID & " AND [dokumen] ='" & txtDocument.Text.Trim & "'")) > 0 Then
            MessageBox.Show("No Dokumen sudah terpakai.", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
            editTextPisau.Focus()
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
                            ,[tinggi]
                            ,[gap]
                            ,[pisau]
                            ,[pembulatan]
                            ,[qty_order]
                            ,[jual_sesuai_order]
                            ,[biaya_pisau]
                            ,[biaya_tinta]
                            ,[biaya_toyobo]
                            ,[biaya_operator]
                            ,[biaya_kirim]) " & vbCrLf &
                            " Values (" & Me._ID & "," & vbCrLf &
                            "'" & FixApostropi(txtDocument.Text.Trim) & "'," & vbCrLf &
                            "" & ObjToStrDateSql(txtTanggal.EditValue) & "," & vbCrLf &
                            ObjToInt(txtIDBahan.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextHargaModal.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextLebar.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextTinggi.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextGap.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextPisau.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextPembulatanRoll.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextQtyOrderPcs.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextJualSesuaiOrder.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextBiayaPisau.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextBiayaTinta.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextBiayaToyobo.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextBiayaOperator.EditValue) & "," & vbCrLf &
                            ObjToDbl(editTextBiayaKirim.EditValue) & ")"
                Else
                    sql = "Update " & TableName & " Set " & vbCrLf &
                          " [dokumen] = '" & txtDocument.Text.Trim & "'," & vbCrLf &
                            "[tgl] = " & ObjToStrDateSql(txtTanggal.EditValue) & "," & vbCrLf &
                            "[id_bahan] = " & ObjToInt(txtIDBahan.EditValue) & "," & vbCrLf &
                            "[harga_bahan] = " & ObjToDbl(editTextHargaModal.EditValue) & "," & vbCrLf &
                            "[lebar] = " & ObjToDbl(editTextLebar.EditValue) & "," & vbCrLf &
                            "[tinggi] = " & ObjToDbl(editTextTinggi.EditValue) & "," & vbCrLf &
                            "[gap] = " & ObjToDbl(editTextGap.EditValue) & "," & vbCrLf &
                            "[pisau] = " & ObjToDbl(editTextPisau.EditValue) & "," & vbCrLf &
                            "[pembulatan] = " & ObjToDbl(editTextPembulatanRoll.EditValue) & "," & vbCrLf &
                            "[qty_order] = " & ObjToDbl(editTextQtyOrderPcs.EditValue) & "," & vbCrLf &
                            "[jual_sesuai_order] = " & ObjToDbl(editTextJualSesuaiOrder.EditValue) & "," & vbCrLf &
                            "[biaya_pisau] = " & ObjToDbl(editTextBiayaPisau.EditValue) & "," & vbCrLf &
                            "[biaya_tinta] = " & ObjToDbl(editTextBiayaTinta.EditValue) & "," & vbCrLf &
                            "[biaya_toyobo] = " & ObjToDbl(editTextBiayaToyobo.EditValue) & "," & vbCrLf &
                            "[biaya_operator] = " & ObjToDbl(editTextBiayaOperator.EditValue) & "," & vbCrLf &
                            "[biaya_kirim] = " & ObjToDbl(editTextBiayaKirim.EditValue) & " " & vbCrLf &
                            " Where [no] ='" & Me._ID & "'"
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
        Me.FormBasic_Load(sender, e)
    End Sub

    Private Sub bbiDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiDelete.ItemClick
        Dim f As Pesan = Query.DeleteDataMaster("TTaffeta", "[no]=" & Me._ID)
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

    Private Sub editTextLebar_EditValueChanged(sender As Object, e As EventArgs) Handles editTextPisau.EditValueChanged, editTextLebar.EditValueChanged, editTextGap.EditValueChanged, editTextTinggi.EditValueChanged
        Hitung()
    End Sub

    Private Sub editTextQtyOrderPcs_EditValueChanged(sender As Object, e As EventArgs) Handles editTextQtyOrderPcs.EditValueChanged, editTextPembulatanRoll.EditValueChanged
        Hitung()
    End Sub

    Private Sub editTextBiayaToyobo_EditValueChanged(sender As Object, e As EventArgs) Handles editTextBiayaKirim.EditValueChanged, editTextBiayaOperator.EditValueChanged, editTextBiayaToyobo.EditValueChanged, editTextBiayaPisau.EditValueChanged, editTextBiayaTinta.EditValueChanged
        Hitung()
    End Sub

    Private Sub editTextJualSesuaiOrder_EditValueChanged(sender As Object, e As EventArgs) Handles editTextJualSesuaiOrder.EditValueChanged
        Hitung()
    End Sub
End Class
