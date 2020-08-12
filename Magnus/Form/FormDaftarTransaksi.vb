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
        ShowMenuRoleUser()
        BarEditTglDari.EditValue = DateTime.Now.ToString("yyyy-MM-01")
        BarEditTglSampai.EditValue = DateTime.Now.ToString("yyyy-MM-dd")
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

    End Sub
End Class