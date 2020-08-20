Imports DevExpress.LookAndFeel
Imports DevExpress.XtraBars.Docking2010.Views.Tabbed
Imports System.Linq

Public Class FormMain
    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        UserLookAndFeel.Default.SkinName = My.Settings.ApplicationSkinName
        'Set User Info
        BarStaticItemUsername.Caption = Username
        BarStaticIP.Caption = "Online On : " & Utils.GetLocalIP()
        ShowMenuByUserRole(IDRoleUser)
        Query.GetApplicationSetting()
        BarStaticItem1.Caption = "Server : " & Ini.BacaIni("Application", "Server", "-")
        BarStaticItem2.Caption = "Database : " & Ini.BacaIni("Application", "Database", "-")
    End Sub

    Sub ShowMenuByUserRole(ByVal _IDRole As Integer)
        Dim ds As New DataSet
        Dim sql As String = ""
        sql = "Select * From MRoleUser Where MRoleUser.ID=" & _IDRole
        ds = Query.ExecuteDataSet(sql)
        If Not ds Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                IDTypeLayout = Utils.ObjToInt(ds.Tables(0).Rows(0).Item("IDTypeLayout"))
                If Utils.ObjToBool(ds.Tables(0).Rows(0).Item("MenuSettingUser")) Then
                    RibbonPageGroupUser.Visible = True
                Else
                    RibbonPageGroupUser.Visible = False
                End If
            End If
            ds.Dispose()
        End If
        sql = "Select MMenu.*,MRoleUserD.* From MRoleUserD Left Join MMenu On MRoleUserD.IDMenu=MMenu.ID Where MRoleUserD.IDRoleUser=" & _IDRole
        ds = Query.ExecuteDataSet(sql)
        If Not ds Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                Dim e As MenuRoleUser = Nothing
                listMenu.Clear()
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    e = New MenuRoleUser
                    With e
                        .ID = Utils.ObjToInt(ds.Tables(0).Rows(i).Item("ID"))
                        .NamaForm = ds.Tables(0).Rows(i).Item("NamaForm").ToString.Trim
                        .Caption = ds.Tables(0).Rows(i).Item("Caption").ToString.Trim
                        .IsActive = Utils.ObjToBool(ds.Tables(0).Rows(i).Item("IsActive"))
                        .IsEnable = Utils.ObjToBool(ds.Tables(0).Rows(i).Item("IsBaru"))
                        .IsBaru = Utils.ObjToBool(ds.Tables(0).Rows(i).Item("IsBaru"))
                        .IsUbah = Utils.ObjToBool(ds.Tables(0).Rows(i).Item("IsUbah"))
                        .IsHapus = Utils.ObjToBool(ds.Tables(0).Rows(i).Item("IsHapus"))
                        .IsExport = Utils.ObjToBool(ds.Tables(0).Rows(i).Item("IsExport"))
                        .IsCetak = Utils.ObjToBool(ds.Tables(0).Rows(i).Item("IsCetak"))
                        .IDTypeLayoutD = Utils.ObjToInt(ds.Tables(0).Rows(i).Item("IDTypeLayout"))
                    End With
                    ShowHideMenu(e.NamaForm, e.IsActive, e.IsEnable)
                    listMenu.Add(e)
                Next
            End If
            ds.Dispose()
        End If
    End Sub

    Sub ShowHideMenu(ByVal NamaForm As String, ByVal IsActive As Boolean, ByVal IsEnable As Boolean)
        Select Case NamaForm
            Case "MasterBarang"
                BarButtonMasterBarang.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonMasterBarang.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonMasterBarang.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "MasterKategori"
                BarButtonMasterKategori.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonMasterKategori.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonMasterKategori.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "MasterKategoriBiaya"
                BarButtonMasterKategoriBiaya.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonMasterKategoriBiaya.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonMasterKategoriBiaya.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "MasterTypeTaffeta"
                BarButtonMasterTypeTaffeta.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonMasterTypeTaffeta.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonMasterTypeTaffeta.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "MasterKaryawan"
                BarButtonKaryawan.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonKaryawan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonKaryawan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "MasterRoleUser"
                BarButtonRoleUser.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonRoleUser.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonRoleUser.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "MasterUser"
                BarButtonManagementUser.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonManagementUser.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonManagementUser.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "CalcLabel"
                BarButtonCalcLabel.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonCalcLabel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonCalcLabel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "CalcRibbon"
                BarButtonCalcRibbon.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonCalcRibbon.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonCalcRibbon.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "CalcTaffeta"
                BarButtonCalcTaffeta.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonCalcTaffeta.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonCalcTaffeta.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "CalcPaket"
                BarButtonCalcPaket.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonCalcPaket.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonCalcPaket.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "StokMasuk"
                BarButtonStokMasuk.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonStokMasuk.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonStokMasuk.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "StokKeluar"
                BarButtonStokKeluar.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonStokKeluar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonStokKeluar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "KasMasuk"
                BarButtonKasMasuk.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonKasMasuk.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonKasMasuk.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "KasKeluar"
                BarButtonKasKeluar.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonKasKeluar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonKasKeluar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "LaporanKartuStok"
                BarButtonLapKartuStok.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonLapKartuStok.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonLapKartuStok.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "LaporanSaldoStok"
                BarButtonLapSaldoStok.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonLapSaldoStok.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonLapSaldoStok.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "LaporanTopItem"
                BarButtonLapTopItem.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonLapTopItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonLapTopItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "LaporanKasHarian"
                BarButtonLapKasHarian.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonLapKasHarian.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonLapKasHarian.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "LaporanKasPerKategori"
                BarButtonLapKasPerKategori.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonLapKasPerKategori.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonLapKasPerKategori.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case "LaporanPerbandinganKasBulanan"
                BarButtonLapPerbandinganBiayaBulanan.Tag = NamaForm
                If IsActive AndAlso IsEnable Then
                    BarButtonLapPerbandinganBiayaBulanan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    BarButtonLapPerbandinganBiayaBulanan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Case Else
                DevExpress.XtraEditors.XtraMessageBox.Show(Me, "Form : " & NamaForm & " Undefined.", NamaAplikasi)
        End Select
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonSettingDatabase.ItemClick
        Dim f As New FormSettingDatabase
        If f.ShowDialog() = DialogResult.OK Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Me, "Setting behasil di perbarui", NamaAplikasi)
            Me.FormMain_Load(sender, e)
        End If
    End Sub
    Private TabColor() As Color = {Color.FromArgb(35, 83, 194), Color.FromArgb(64, 168, 19), Color.FromArgb(245, 121, 10), Color.FromArgb(141, 62, 168), Color.FromArgb(70, 155, 183), Color.FromArgb(196, 19, 19)}
    Private formCount As Integer = 0

    Sub callDaftar(ByVal _idFrm As FormDaftar.IDForm, ByVal _namaForm As Object)
        Dim frmEntri As FormDaftar = Nothing
        Dim F As Object
        For Each F In MdiChildren
            If TypeOf F Is FormDaftar Then
                If DirectCast(F, Magnus.FormDaftar).NamaForm = _namaForm.ToString Then
                    frmEntri = F
                    Exit For
                End If
            End If
        Next
        If frmEntri Is Nothing Then
            frmEntri = New FormDaftar
            frmEntri.idFrm = _idFrm 'FormDaftar.IDForm.F_User
            frmEntri.NamaForm = _namaForm.ToString  '"Daftar User"
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
        callDaftar(FormDaftar.IDForm.F_User, BarButtonManagementUser.Tag)
    End Sub

    Private Sub BarButtonMasterBarang_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonMasterBarang.ItemClick
        callDaftar(FormDaftar.IDForm.F_MBarang, BarButtonMasterBarang.Tag)
    End Sub

    Private Sub BarButtonMasterKategori_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonMasterKategori.ItemClick
        callDaftar(FormDaftar.IDForm.F_MKategori, BarButtonMasterKategori.Tag)
    End Sub

    Private Sub BarButtonMasterKategoriBiaya_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonMasterKategoriBiaya.ItemClick
        callDaftar(FormDaftar.IDForm.F_MKategoriBiaya, BarButtonMasterKategoriBiaya.Tag)
    End Sub

    Private Sub BarButtonKaryawan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonKaryawan.ItemClick
        callDaftar(FormDaftar.IDForm.F_MKaryawan, BarButtonKaryawan.Tag)
    End Sub
    Private Sub BarButtonRoleUser_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonRoleUser.ItemClick
        callDaftar(FormDaftar.IDForm.F_RoleUser, BarButtonRoleUser.Tag)
    End Sub
    Private Sub BarButtonMasterTypeTaffeta_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonMasterTypeTaffeta.ItemClick
        callDaftar(FormDaftar.IDForm.F_TypeTaffeta, BarButtonMasterTypeTaffeta.Tag)
    End Sub

    Sub callDaftarTransaksi(ByVal _idFrmTr As FormDaftarTransaksi.IDFormTr, ByVal _namaForm As Object)
        Dim frmEntri As FormDaftarTransaksi = Nothing
        Dim F As Object
        For Each F In MdiChildren
            If TypeOf F Is FormDaftarTransaksi Then
                If DirectCast(F, Magnus.FormDaftarTransaksi).NamaForm = _namaForm.ToString Then
                    frmEntri = F
                    Exit For
                End If
            End If
        Next
        If frmEntri Is Nothing Then
            frmEntri = New FormDaftarTransaksi
            frmEntri.idFrmTr = _idFrmTr 'FormDaftar.IDForm.F_User
            frmEntri.NamaForm = _namaForm.ToString  '"Daftar User"
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
        callDaftarTransaksi(FormDaftarTransaksi.IDFormTr.F_StokMasuk, BarButtonStokMasuk.Tag)
    End Sub

    Private Sub BarButtonStokKeluar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonStokKeluar.ItemClick
        callDaftarTransaksi(FormDaftarTransaksi.IDFormTr.F_StokKeluar, BarButtonStokKeluar.Tag)
    End Sub

    Private Sub BarButtonKasMasuk_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonKasMasuk.ItemClick
        callDaftarTransaksi(FormDaftarTransaksi.IDFormTr.F_KasMasuk, BarButtonKasMasuk.Tag)
    End Sub

    Private Sub BarButtonKasKeluar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonKasKeluar.ItemClick
        callDaftarTransaksi(FormDaftarTransaksi.IDFormTr.F_KasKeluar, BarButtonKasKeluar.Tag)
    End Sub

    Private Sub FormMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        My.Settings.ApplicationSkinName = UserLookAndFeel.Default.SkinName
        My.Settings.Save()
        Application.Exit()
    End Sub

    Private Sub BarStaticItemUsername_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarStaticItemUsername.ItemClick
        Using f As New FormGantiPassword
            f.Location = MousePosition
            f.ShowDialog()
            FormMain_Load(sender, e)
        End Using
    End Sub

    Private Sub BarButtonCalcLabel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonCalcLabel.ItemClick
        callDaftarTransaksi(FormDaftarTransaksi.IDFormTr.F_CalcLabel, BarButtonCalcLabel.Tag)
    End Sub

    Private Sub BarButtonCalcRibbon_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonCalcRibbon.ItemClick
        callDaftarTransaksi(FormDaftarTransaksi.IDFormTr.F_CalcRibbon, BarButtonCalcRibbon.Tag)
    End Sub

    Private Sub BarButtonCalcTaffeta_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonCalcTaffeta.ItemClick
        callDaftarTransaksi(FormDaftarTransaksi.IDFormTr.F_CalcTaffeta, BarButtonCalcTaffeta.Tag)
    End Sub

    Private Sub BarButtonCalcPaket_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonCalcPaket.ItemClick
        callDaftarTransaksi(FormDaftarTransaksi.IDFormTr.F_CalcPaket, BarButtonCalcPaket.Tag)
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Using f As New FormSetting
            f.TopMost = True
            f.ShowDialog(Me)
        End Using
    End Sub

    Private Sub BarCheckItem1_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarCheckItem1.CheckedChanged
        IsEditReport = BarCheckItem1.Checked
    End Sub
End Class