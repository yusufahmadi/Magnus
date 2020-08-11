Partial Public Class FormDaftarTransaksi
    Inherits DevExpress.XtraEditors.XtraForm

    Public idFrmTr As IDFormTr
    Public Enum IDFormTr
        F_StokMasuk = 0
        F_StokKeluar = 1
        F_KasMasuk = 2
        F_KasKeluar = 3
    End Enum
    Public NamaForm As String = "Undefined"
    Private Sub FormDaftar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = NamaForm
        BarEditTglDari.EditValue = DateTime.Now.ToString("yyyy-MM-01")
        BarEditTglSampai.EditValue = DateTime.Now.ToString("yyyy-MM-dd")
    End Sub

    Private Sub BarButtonRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonRefresh.ItemClick

    End Sub
End Class