Imports DevExpress.XtraBars.Docking2010.Views.Tabbed
Imports System.Linq
Public Class FormMain

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarStaticItemUsername.Caption = Username
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonSettingDatabase.ItemClick
        Dim f As New FormSetting
        If f.ShowDialog() = DialogResult.OK Then
            MsgBox("Setting behasil di perbarui")
        End If
    End Sub
    Private TabColor() As Color = {Color.FromArgb(35, 83, 194), Color.FromArgb(64, 168, 19), Color.FromArgb(245, 121, 10), Color.FromArgb(141, 62, 168), Color.FromArgb(70, 155, 183), Color.FromArgb(196, 19, 19)}
    Private formCount As Integer = 0

    Sub callDaftar(ByVal _idFrm As FormDaftar.IDForm, ByVal _namaForm As String)
        Dim frmEntri As FormDaftar = Nothing
        Dim F As Object
        For Each F In MdiChildren
            If TypeOf F Is FormDaftar Then
                If DirectCast(F, Magnus.FormDaftar).NamaForm = _namaForm Then
                    frmEntri = F
                    Exit For
                End If
            End If
        Next
        If frmEntri Is Nothing Then
            frmEntri = New FormDaftar
            frmEntri.idFrm = _idFrm 'FormDaftar.IDForm.F_User
            frmEntri.NamaForm = _namaForm  '"Daftar User"
            frmEntri.WindowState = FormWindowState.Maximized
            frmEntri.MdiParent = Me
        End If
        frmEntri.Show()
        frmEntri.Focus()

        'Dim doc As Document = TabbedView1.Documents.OfType(Of Document)().Where(Function(d) d.Control = frmEntri).FirstOrDefault()
        'doc.Appearance.HeaderActive.BackColor = TabColor((formCount - 1) Mod 6)
        'doc.Appearance.Header.BackColor = TabColor((formCount - 1) Mod 6)
        'doc.Appearance.HeaderHotTracked.BackColor = TabColor((formCount - 1) Mod 6)
    End Sub

    Private Sub BarButtonItem6_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonManagementUser.ItemClick
        callDaftar(FormDaftar.IDForm.F_User, "Daftar User")
    End Sub

    Private Sub BarButtonMasterBarang_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonMasterBarang.ItemClick
        callDaftar(FormDaftar.IDForm.F_MBarang, "Daftar Master Barang")
    End Sub

    Private Sub BarButtonMasterKategori_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonMasterKategori.ItemClick
        callDaftar(FormDaftar.IDForm.F_MKategori, "Daftar Master Kategori")
    End Sub

    Private Sub BarButtonMasterKategoriBiaya_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonMasterKategoriBiaya.ItemClick
        callDaftar(FormDaftar.IDForm.F_MKategoriBiaya, "Daftar Master Kategori Biaya")
    End Sub

    Private Sub BarButtonKaryawan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonKaryawan.ItemClick
        callDaftar(FormDaftar.IDForm.F_MKaryawan, "Daftar Master Karyawan")
    End Sub
    Private Sub BarButtonRoleUser_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonRoleUser.ItemClick
        callDaftar(FormDaftar.IDForm.F_RoleUser, "Daftar Role User")
    End Sub
    Private Sub BarButtonMasterTypeTaffeta_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonMasterTypeTaffeta.ItemClick
        callDaftar(FormDaftar.IDForm.F_TypeTaffeta, "Daftar Type Taffeta")
    End Sub

    Sub callDaftarTransaksi(ByVal _idFrmTr As FormDaftarTransaksi.IDFormTr, ByVal _namaForm As String)
        Dim frmEntri As FormDaftarTransaksi = Nothing
        Dim F As Object
        For Each F In MdiChildren
            If TypeOf F Is FormDaftarTransaksi Then
                If DirectCast(F, Magnus.FormDaftarTransaksi).NamaForm = _namaForm Then
                    frmEntri = F
                    Exit For
                End If
            End If
        Next
        If frmEntri Is Nothing Then
            frmEntri = New FormDaftarTransaksi
            frmEntri.idFrmTr = _idFrmTr 'FormDaftar.IDForm.F_User
            frmEntri.NamaForm = _namaForm  '"Daftar User"
            frmEntri.WindowState = FormWindowState.Maximized
            frmEntri.MdiParent = Me
        End If
        frmEntri.Show()
        frmEntri.Focus()

        'Dim doc As Document = TabbedView1.Documents.OfType(Of Document)().Where(Function(d) d.Control = frmEntri).FirstOrDefault()
        'doc.Appearance.HeaderActive.BackColor = TabColor((formCount - 1) Mod 6)
        'doc.Appearance.Header.BackColor = TabColor((formCount - 1) Mod 6)
        'doc.Appearance.HeaderHotTracked.BackColor = TabColor((formCount - 1) Mod 6)
    End Sub

    Private Sub BarButtonStokMasuk_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonStokMasuk.ItemClick
        callDaftarTransaksi(FormDaftarTransaksi.IDFormTr.F_StokMasuk, "Stok Masuk")
    End Sub

    Private Sub BarButtonStokKeluar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonStokKeluar.ItemClick
        callDaftarTransaksi(FormDaftarTransaksi.IDFormTr.F_StokKeluar, "Stok Keluar")
    End Sub

    Private Sub BarButtonKasMasuk_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonKasMasuk.ItemClick
        callDaftarTransaksi(FormDaftarTransaksi.IDFormTr.F_KasMasuk, "Kas Masuk")
    End Sub

    Private Sub BarButtonKasKeluar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonKasKeluar.ItemClick
        callDaftarTransaksi(FormDaftarTransaksi.IDFormTr.F_KasKeluar, "Kas Keluar")
    End Sub

    Private Sub FormMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub

    Private Sub BarStaticItemUsername_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarStaticItemUsername.ItemClick
        Dim f As New FormGantiPassword
        f.Location = MousePosition
        f.ShowDialog()
    End Sub

    Private Sub BarButtonCalcLabel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonCalcLabel.ItemClick
        callDaftarTransaksi(FormDaftarTransaksi.IDFormTr.F_KasKeluar, "Calc Label")
    End Sub

    Private Sub BarButtonCalcRibbon_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonCalcRibbon.ItemClick
        callDaftarTransaksi(FormDaftarTransaksi.IDFormTr.F_KasKeluar, "Calc Ribbon")
    End Sub

    Private Sub BarButtonCalcTaffeta_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonCalcTaffeta.ItemClick
        callDaftarTransaksi(FormDaftarTransaksi.IDFormTr.F_KasKeluar, "Calc Taffeta")
    End Sub

    Private Sub BarButtonCalcPaket_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonCalcPaket.ItemClick
        callDaftarTransaksi(FormDaftarTransaksi.IDFormTr.F_KasKeluar, "Calc Paket")
    End Sub

End Class