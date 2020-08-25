Imports DevExpress.LookAndFeel
Imports DevExpress.Utils
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Docking2010.Views.Tabbed
Imports System.Data.SqlClient
Imports System.Linq

Public Class FormMain2
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

    Dim WithEvents itemParent As BarSubItem
    Dim WithEvents itemParentBar2 As BarSubItem
    Dim WithEvents barTemp As New Bar
    Dim WithEvents itemmenu As BarButtonItem
    Dim WithEvents barsubitemmenu As BarSubItem
    Private Sub AktivekanMenubyUser()
        Dim oConn As SqlConnection
        Dim ocmd As SqlCommand
        Dim odr As SqlDataReader
        Dim oConn1 As SqlConnection
        Dim ocmd1 As SqlCommand
        Dim odr1 As SqlDataReader
        Dim oConn2 As SqlConnection
        Dim ocmd2 As SqlCommand
        Dim odr2 As SqlDataReader
        Dim strsql As String
        Dim KeyMode As String()
        Dim Hasil As Boolean = True
        Dim AksesMenu() As String
        Dim MenuEnable As Boolean
        Using dlg As New WaitDialogForm("Load User Menu ", "Please Wait")
            Try
                dlg.Show()
                Application.DoEvents()
                strsql = "Select mmenu.noid,mmenu.kode,mmenu.caption,mshortcut.idshortcut," &
                " mmenu.icon,mmenu.isawalgroup,mmenu.objectrun,mmenu.shortcut,mmenu.keyshortcut from mmenu left join mshortcut on mmenu.shortcut=mshortcut.nama WHERE isactive=1 and idparent=-1 AND MMenu.NoID IN (SELECT MUserD.IDMenu FROM MUserD WHERE MUserD.[Visible]=1 AND MUserD.IDUser=" & Username & ") order by nourut"
                oConn = New SqlConnection(conStr)
                ocmd = New SqlCommand(strsql, oConn)
                oConn.Open()
                oConn1 = New SqlConnection(conStr)
                oConn1.Open()
                oConn2 = New SqlConnection(conStr)
                oConn2.Open()
                odr = ocmd.ExecuteReader
                Do While odr.Read
                    itemParent = New BarSubItem
                    itemParentBar2 = New BarSubItem
                    'Create a new bar item representing a hyperlink editor
                    itemParent.PaintStyle = BarItemPaintStyle.CaptionInMenu
                    itemParent.Name = NullToStr(odr.GetValue(1))
                    itemParentBar2.Name = NullToStr(odr.GetValue(1)) & "Bar2"
                    itemParent.Caption = NullToStr(odr.GetValue(2))
                    itemParentBar2.Caption = NullToStr(odr.GetValue(2))
                    'Ambil Sesuai Setting
                    MenuEnable = ObjToBool(Query.ExecuteScalar("SELECT MUserD.[Enable] FROM MUserD WHERE IDMenu=" & ObjToLong(odr.GetValue(0)) & " AND MUserD.IDUser=" & Username, ObjToInt(Ini.BacaIni("appconfig", "LoadDataSaatLogin", "0")) = 1))
                    itemParent.Enabled = MenuEnable
                    itemParentBar2.Enabled = MenuEnable
                    'itemParent.ShortCut = ObjToInt(odr.GetValue(3))
                    'itemParentBar2.ShortCut = ObjToInt(odr.GetValue(3))
                    If ObjToInt(odr.GetValue(8)) <> 0 Then
                        Dim a As New System.Windows.Forms.Shortcut
                        a = ObjToInt(odr.GetValue(8))
                        itemParentBar2.ItemShortcut = New DevExpress.XtraBars.BarShortcut(a)
                        itemParentBar2.ShortcutKeyDisplayString = NullToStr(odr.GetValue(7))
                        itemParent.ItemShortcut = New DevExpress.XtraBars.BarShortcut(a)
                        itemParent.ShortcutKeyDisplayString = NullToStr(odr.GetValue(7))
                    End If
                    Try
                        itemParent.Glyph = ImageCollectionLarge.Images(ObjToInt(odr.GetValue(4)))
                        itemParent.GlyphDisabled = ImageCollectionLarge.Images(ObjToInt(odr.GetValue(4)))
                    Catch
                    End Try
                    AddHandler itemParent.ItemClick, AddressOf itemParent_ItemClick
                    AddHandler itemParentBar2.ItemClick, AddressOf itemParent_ItemClick
                    'here
                    'Bar1.AddItem(itemParent)

                    barTemp.AddItem(itemParent)
                    strsql = "Select mmenu.noid,mmenu.kode,mmenu.caption,mshortcut.idshortcut," &
                             "mmenu.icon,mmenu.isawalgroup,mmenu.objectrun,mmenu.shortcut,mmenu.keyshortcut,mmenu.IsBarSubItem,mmenu.KeyMode from mmenu left join mshortcut on mmenu.shortcut=mshortcut.nama where isactive=1 And ISNULL(IDBarSubItem,0)=0 AND MMenu.NoID IN (SELECT MUserD.IDMenu FROM MUserD WHERE MUserD.[Visible]=1 AND MUserD.IDUser=" & Username & ") and idparent=" & ObjToLong(odr.GetValue(0)) & " order by nourut"
                    ocmd1 = New SqlCommand(strsql, oConn1)
                    odr1 = ocmd1.ExecuteReader
                    Do While odr1.Read

                        KeyMode = AES_Decrypt(NullToStr(odr1.GetValue(10))).Split("|") 'Decrypt(NullToStr(odr1.GetValue(10)), VPOINT_KEYS.Keys).Split("|")
                        Hasil = False
                        For d As Integer = 0 To KeyMode.Length - 1
                            'AksesMenu = mdlSherialShield.AktifkanMenu
                            Hasil = False
                            For xx As Integer = 0 To AksesMenu.Length - 1
                                'If Util.VPOINT_KEYS.Trial Then
                                '    Hasil = True
                                '    Exit For
                                'ElseIf KeyMode(d).ToUpper <> "" AndAlso KeyMode(d).ToUpper = AksesMenu(xx).ToString.ToUpper AndAlso KeyMode(d).ToUpper <> "NATHING" Then
                                '    Hasil = True
                                '    Exit For
                                'End If
                            Next
                            If Hasil Then
                                Hasil = True
                                Exit For
                            Else
                            End If
                        Next
                        If Hasil Then
                            If Not ObjToBool(odr1.GetValue(9)) Then
                                itemmenu = New BarButtonItem
                                itemmenu.Name = NullToStr(odr1.GetValue(1))
                                itemmenu.Caption = NullToStr(odr1.GetValue(2))
                                'Ambil Sesuai Setting
                                MenuEnable = ObjToBool(Query.ExecuteScalar("SELECT MUserD.[Enable] FROM MUserD WHERE IDMenu=" & ObjToLong(odr1.GetValue(0)) & " AND MUserD.IDUser=" & Username, ObjToInt(Ini.BacaIni("appconfig", "LoadDataSaatLogin", "0")) = 1))
                                itemmenu.Enabled = MenuEnable
                                'itemmenu.ShortCut = ObjToInt(odr1.GetValue(3))
                                If ObjToInt(odr1.GetValue(8)) <> 0 Then
                                    Dim a As New System.Windows.Forms.Shortcut
                                    a = ObjToInt(odr1.GetValue(8))
                                    itemmenu.ItemShortcut = New DevExpress.XtraBars.BarShortcut(a)
                                    itemmenu.ShortcutKeyDisplayString = NullToStr(odr1.GetValue(7))
                                End If
                                itemmenu.ImageIndex = ObjToInt(odr1.GetValue(4))
                                itemmenu.Tag = NullToStr(odr1.GetValue(6)) & ":" & ObjToLong(odr1.GetValue(0))
                                AddHandler itemmenu.ItemClick, AddressOf itemmenu_ItemClick
                                itemParent.ItemLinks.Add(itemmenu, ObjToBool(odr1.GetValue(5)))
                                itemParentBar2.ItemLinks.Add(itemmenu, ObjToBool(odr1.GetValue(5)))
                                'mnBarSubMaster.ItemLinks.Add(itemmenu, ObjToBool(odr1.GetValue(5)))
                            Else
                                barsubitemmenu = New BarSubItem
                                barsubitemmenu.Name = NullToStr(odr1.GetValue(1))
                                barsubitemmenu.Caption = NullToStr(odr1.GetValue(2))
                                MenuEnable = ObjToBool(Query.ExecuteScalar("SELECT MUserD.[Enable] FROM MUserD WHERE IDMenu=" & ObjToLong(odr1.GetValue(0)) & " AND MUserD.IDUser=" & Username, ObjToInt(Ini.BacaIni("appconfig", "LoadDataSaatLogin", "0")) = 1))
                                barsubitemmenu.Enabled = MenuEnable
                                'itemmenu.ShortCut = ObjToInt(odr1.GetValue(3))
                                If ObjToInt(odr1.GetValue(8)) <> 0 Then
                                    Dim a As New System.Windows.Forms.Shortcut
                                    a = ObjToInt(odr1.GetValue(8))
                                    barsubitemmenu.ItemShortcut = New DevExpress.XtraBars.BarShortcut(a)
                                    barsubitemmenu.ShortcutKeyDisplayString = NullToStr(odr1.GetValue(7))
                                End If
                                barsubitemmenu.ImageIndex = ObjToInt(odr1.GetValue(4))
                                barsubitemmenu.Tag = NullToStr(odr1.GetValue(6))
                                'AddHandler barsubitemmenu.ItemClick, AddressOf itemmenu_ItemClick
                                itemParent.ItemLinks.Add(barsubitemmenu, ObjToBool(odr1.GetValue(5)))
                                itemParentBar2.ItemLinks.Add(barsubitemmenu, ObjToBool(odr1.GetValue(5)))
                                'mnBarSubMaster.ItemLinks.Add(itemmenu, ObjToBool(odr1.GetValue(5)))

                                strsql = "Select mmenu.noid,mmenu.kode,mmenu.caption,mshortcut.idshortcut," &
                                     "mmenu.icon,mmenu.isawalgroup,mmenu.objectrun,mmenu.shortcut,mmenu.keyshortcut,mmenu.keymode from mmenu left join mshortcut on mmenu.shortcut=mshortcut.nama where isactive=1 AND MMenu.NoID IN (SELECT MUserD.IDMenu FROM MUserD WHERE MUserD.[Visible]=1 AND MUserD.IDUser=" & Username & ") and idparent=" & ObjToLong(odr.GetValue(0)) & " AND IDBarSubItem=" & ObjToLong(odr1.GetValue(0)) & " order by nourut"
                                ocmd2 = New SqlCommand(strsql, oConn2)
                                odr2 = ocmd2.ExecuteReader
                                Do While odr2.Read
                                    'KeyMode = DecryptText(NullToStr(odr2.GetValue(9)), VPOINT_KEYS.Keys).Split("|")
                                    Hasil = False
                                    For d As Integer = 0 To KeyMode.Length - 1
                                        'AksesMenu = mdlSherialShield.AktifkanMenu
                                        'For xx As Integer = 0 To AksesMenu.Length - 1
                                        '    If Util.VPOINT_KEYS.Trial Then
                                        '        Hasil = True
                                        '        Exit For
                                        '    ElseIf KeyMode(d).ToUpper <> "" AndAlso KeyMode(d).ToUpper = AksesMenu(xx).ToString.ToUpper AndAlso KeyMode(d).ToUpper <> "NATHING" Then
                                        '        Hasil = True
                                        '        Exit For
                                        '    End If
                                        'Next
                                        If Hasil Then
                                            Hasil = True
                                            Exit For
                                        End If
                                    Next
                                    If Hasil Then
                                        itemmenu = New BarButtonItem
                                        itemmenu.Name = NullToStr(odr2.GetValue(1))
                                        itemmenu.Caption = NullToStr(odr2.GetValue(2))
                                        MenuEnable = ObjToBool(Query.ExecuteScalar("SELECT MUserD.[Enable] FROM MUserD WHERE IDMenu=" & ObjToLong(odr2.GetValue(0)) & " AND MUserD.IDUser=" & Username, ObjToInt(Ini.BacaIni("appconfig", "LoadDataSaatLogin", "0")) = 1))
                                        itemmenu.Enabled = MenuEnable
                                        'itemmenu.ShortCut = ObjToInt(odr1.GetValue(3))
                                        If ObjToInt(odr2.GetValue(8)) <> 0 Then
                                            Dim a As New System.Windows.Forms.Shortcut
                                            a = ObjToInt(odr2.GetValue(8))
                                            itemmenu.ItemShortcut = New DevExpress.XtraBars.BarShortcut(a)
                                            itemmenu.ShortcutKeyDisplayString = NullToStr(odr2.GetValue(7))
                                        End If
                                        itemmenu.ImageIndex = ObjToInt(odr2.GetValue(4))
                                        itemmenu.Tag = NullToStr(odr2.GetValue(6)) & ":" & ObjToLong(odr2.GetValue(0))
                                        'itemmenu.isa = ObjToBool(odr2.GetValue(5))
                                        AddHandler itemmenu.ItemClick, AddressOf itemmenu_ItemClick
                                        barsubitemmenu.AddItem(itemmenu)
                                        'itemParentBar2.ItemLinks.Add(itemmenu, ObjToBool(odr2.GetValue(5)))
                                        'mnBarSubMaster.ItemLinks.Add(itemmenu, ObjToBool(odr1.GetValue(5)))
                                    End If
                                Loop
                                ocmd2.Dispose()
                                odr2.Close()
                            End If
                        End If
                    Loop
                    ocmd1.Dispose()
                    odr1.Close()
                Loop
                ocmd.Dispose()
                oConn.Close()
                oConn.Dispose()
                oConn1.Close()
                oConn1.Dispose()
                oConn2.Close()
                oConn2.Dispose()

                dlg.SetCaption("Getting date & time from Main Server")
                Application.DoEvents()
                'Kalau Ini Tetap Biarkan Ke Server Pusat
                TanggalSystem = Query.ExecuteScalar("DECLARE @TZ SMALLINT; " & vbCrLf &
                                                     "SELECT @TZ=DATEPART(TZ, SYSDATETIMEOFFSET()); " & vbCrLf &
                                                     "SELECT DATEADD(HOUR, -1*@TZ/60, GETDATE()) AS Tanggal")
                TimeZoneInformation.TimeZoneFunctionality.SetTime(System.TimeZone.CurrentTimeZone.ToLocalTime(TanggalSystem))
                'mnStatusUser.Caption = "Login : " & Username
                'Dim Ssql As String = "SELECT MGudang.Nama + Case When IsNull(MWilayah.NoID,0)>0 then ' | Cabang : ' + IsNull(MCabang.Nama,'(Not Set)') + ' | Wilayah : '+ IsNull(MWilayah.Nama,'(Not Set)') Else ' | Wilayah : '+ IsNull(MWilayah2.Nama,'(Not Set)') End FROM Muser LEFT JOIN MGudang ON MGudang.NoID=MUSer.IDGudangDefault Left Join MCabang On MCabang.NoID=MGudang.IDCabang Left Join MWilayah On MWilayah.NoiD=MCabang.IDWilayah Left Join MWilayah MWilayah2 On MWilayah2.NoiD=MGudang.IDWilayah WHERE MUser.NoID=" & Username
                'mnStatusGudang.Caption = "Gudang : " & NullToStr(Query.ExecuteScalar(Ssql))
                ''mnStatusGudang.Caption = "Gudang : " & NullToStr(Query.ExecuteScalar("SELECT MGudang.Nama FROM Muser LEFT JOIN MGudang ON MGudang.NoID=MUSer.IDGudangDefault WHERE MUser.NoID=" & Username))
                'mnStatusRole.Caption = "Role : (None)"

                'For Each mnParent As BarSubItemLink In Bar1.ItemLinks
                '    If mnParent.Item.ItemLinks.Count <= 0 Then
                '        mnParent.Visible = False
                '    End If
                'Next
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        'If System.IO.File.Exists(LayoutsHelper.FolderLayouts &  Me.Name & "_BarMenu.xml") Then
        '    BarManager1.RestoreLayoutFromXml(LayoutsHelper.FolderLayouts &  Me.Name & "_BarMenu.xml")
        'End If
    End Sub

    Private Sub itemParent_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
        'MsgBox(e.Item.Name)
    End Sub
    Private Sub itemmenu_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
        'If Not IsLogin Then Exit Sub
        'If e.Item.Tag.ToString <> "" Then
        '    Dim perintah() As String
        '    Try
        '        perintah = Split(e.Item.Tag.ToString, ":")
        '        '
        '        '
        '        If perintah(0).Trim.ToLower = "DaftarKartuStokperGudang".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarKartuStok = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarKartuStok Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarKartuStok
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LapSaldoHarian".ToLower Then
        '            Dim x As New FrmLapSaldoHarian
        '            Try
        '                If x.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        '                End If
        '            Catch ex As Exception
        '                XtraMessageBox.Show("Kesalahan : " & ex.Message)
        '            Finally
        '                x.Dispose()
        '            End Try
        '        ElseIf perintah(0).Trim.ToLower = "LaporanPerbandinganPromo".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanPerbandinganPromo = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPerbandinganPromo Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPerbandinganPromo
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanPenjualanHarian".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanPenjualanHarian = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPenjualanHarian Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPenjualanHarian
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanPenjualanBarangKonsinyasi".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanPenjualanBarangKonsinyasi = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPenjualanBarangKonsinyasi Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPenjualanBarangKonsinyasi
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarTargetOmsetPembelian".ToLower Or perintah(0).Trim.ToLower = "DaftarTargetOmsetPenjualan".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarTargetSupplier = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarTargetSupplier Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarTargetSupplier
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanDetilMemo".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanMemo = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanMemo Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanMemo
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanRekapPO".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanRekapPO = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanRekapPO Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanRekapPO
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanPenjualanPivot".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanPenjualanPivot = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPenjualanPivot Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPenjualanPivot
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanPenjualanFashion".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanRekapPenjualanFashion = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanRekapPenjualanFashion Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanRekapPenjualanFashion
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarKartuStokVarian".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarKartuStokVarian = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarKartuStokVarian Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarKartuStokVarian
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanAktivaTetap".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanAktivaTetap = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanAktivaTetap Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanAktivaTetap
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarStockOpname".ToLower Then
        '            mnHasilStokOpnameDgAlat_ItemClick(Nothing, Nothing)
        '        ElseIf perintah(0).Trim.ToLower = "LaporanRekapAllTraksaksi".Trim.ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1 laporanrekapalltransaksi 		laporanrekapalltransaksi
        '            Dim frmEntri As frmLaporanRekapAllTransaksi = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanRekapAllTransaksi Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanRekapAllTransaksi
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "laporanperincianpersediaan".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanPerincianPersediaan = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPerincianPersediaan Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPerincianPersediaan
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "laporanperincianhutangpiutang".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanPerincianHutangPiutang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPerincianHutangPiutang Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPerincianHutangPiutang
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarTargetSalesman".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarTargetSalesman = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarTargetSalesman Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarTargetSalesman
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanPembayaranPiutang".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanPembayaranPiutang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPembayaranPiutang Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPembayaranPiutang
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanInsentifPenjualan".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanInsentifPenjualan = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanInsentifPenjualan Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanInsentifPenjualan
        '                frmEntri._Laporan = frmLaporanInsentifPenjualan._pLap._1_lapInsentifPeljualan
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanDetailPenjualanDanRetur".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanDetailPenjualanDanReturnya = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanDetailPenjualanDanReturnya Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanDetailPenjualanDanReturnya
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "laporansejarahhutangpiutang".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanSejarahHutangPiutang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanSejarahHutangPiutang Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanSejarahHutangPiutang
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            'ElseIf perintah(0).Trim.ToLower = "RepairHPP".ToLower Then
        '            '    Using x As New frmPerbaikanPostingAll
        '            '        Try
        '            '            x.ShowDialog(Me)
        '            '        Catch ex As Exception
        '            '            XtraMessageBox.Show("Info Keslahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '            '        End Try
        '            '    End Using
        '        ElseIf perintah(0).Trim.ToLower = "PostingAllTransaction".ToLower Then
        '            Using x As New frmPostingAllTransaction
        '                Try
        '                    x.ShowDialog(Me)
        '                Catch ex As Exception
        '                    XtraMessageBox.Show("Info Keslahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                End Try
        '            End Using
        '        ElseIf perintah(0).Trim.ToLower = "TutupBuku".ToLower Then
        '            Using x As New frmTutupBukuBulanan
        '                Try
        '                    x.ShowDialog(Me)
        '                Catch ex As Exception
        '                    XtraMessageBox.Show("Info Kesalahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK)
        '                End Try
        '            End Using
        '        ElseIf perintah(0).Trim.ToLower = "DaftarBarangPaket".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarBarangPaket = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarBarangPaket Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarBarangPaket
        '                frmEntri.IDMenu = NullToLong(perintah(1).Trim)
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarPembelianAsset".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frDaftarAktivaTetapPembelian = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frDaftarAktivaTetapPembelian Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frDaftarAktivaTetapPembelian
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarPenjualanAsset".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frDaftarAktivaTetapPenjualan = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frDaftarAktivaTetapPenjualan Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frDaftarAktivaTetapPenjualan
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarTandaTerimaNota".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            'Dim frmEntri As frmTandaTerimaNota = Nothing
        '            'Dim F As Object
        '            'For Each F In MdiChildren
        '            '    If TypeOf F Is frmTandaTerimaNota Then
        '            '        frmEntri = F
        '            '        Exit For
        '            '    End If
        '            'Next
        '            'If frmEntri Is Nothing Then
        '            '    frmEntri = New frmTandaTerimaNota
        '            '    frmEntri.WindowState = FormWindowState.Maximized
        '            '    frmEntri.MdiParent = Me
        '            'End If
        '            'frmEntri.Show()
        '            'frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanDetilPembelian".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanPembelianDetil = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPembelianDetil Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPembelianDetil
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "laporanstockbarangkeluar".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanStockBarangKeluar = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanStockBarangKeluar Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanStockBarangKeluar
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanStockBarangMasuk".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanStockBarangMasuk = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanStockBarangMasuk Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanStockBarangMasuk
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "EntriBarang".ToLower Then
        '            Select Case Util.EntriBarangFrameworkForm
        '                Case FormEntriMasterBarang.Entri3Satuan
        '                    Dim x As New frmEntriBarangNonFramework(frmEntriBarangNonFramework.pTipe.Baru, -1)
        '                    x.MdiParent = Me
        '                    x.WindowState = FormWindowState.Normal

        '                    x.Show()
        '                    x.Focus()
        '                Case FormEntriMasterBarang.FrameworkFormLama
        '                    Dim x As New frmEntriBarangLama
        '                    'x.FormName = "EntriBarang"
        '                    x.isNew = True

        '                    x.MdiParent = Me
        '                    x.WindowState = FormWindowState.Normal

        '                    x.Show()
        '                    x.Focus()
        '                Case FormEntriMasterBarang.FrameworkFormDC
        '                    Dim x As New frmEntriBarangLamaDC
        '                    'x.FormName = "EntriBarang"
        '                    x.isNew = True

        '                    x.MdiParent = Me
        '                    x.WindowState = FormWindowState.Normal

        '                    x.Show()
        '                    x.Focus()
        '                Case Else 'FormEntriMasterBarang.FrameworkFormBaru
        '                    Dim x As New clsBarang
        '                    'x.FormName = "EntriBarang"
        '                    x.isNew = True

        '                    x.MdiParent = Me
        '                    x.WindowState = FormWindowState.Normal

        '                    x.Show()
        '                    x.Focus()
        '            End Select
        '        ElseIf perintah(0).Trim.ToLower = "laporankasharian".ToLower Then
        '            Dim frmEntri As frLaporanKasHarian = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frLaporanKasHarian Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frLaporanKasHarian

        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()

        '            'Using frmEntri As New frLaporanKasHarian
        '            '    Try
        '            '        frmEntri.StartPosition = FormStartPosition.CenterParent
        '            '        frmEntri.WindowState = FormWindowState.Normal
        '            '        frmEntri.ShowDialog(Me)
        '            '    Catch ex As Exception
        '            '        XtraMessageBox.Show("Info Kesalahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '            '    End Try
        '            'End Using
        '        ElseIf perintah(0).Trim.ToLower = "LaporanRevisiPOS".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmPOSInKassa = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmPOSInKassa Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmPOSInKassa
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '                frmEntri.IDMenu = NullToLong(perintah(1))
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "MasterMinimumMaximum".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmMinMaxGudang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmMinMaxGudang Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmMinMaxGudang
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "laporanrekapomsetbulanan".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanRekapPenjualanGlobalBulanan = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanRekapPenjualanGlobalBulanan Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanRekapPenjualanGlobalBulanan
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            'frLaporanRekapPemakaianVoucher
        '        ElseIf perintah(0).Trim.ToLower = "LaporanSaldoPiutang".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanSaldoAgingHutang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanSaldoAgingHutang Then
        '                    If F.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.ALL Then
        '                        frmEntri = F
        '                        Exit For
        '                    End If
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanSaldoAgingHutang
        '                frmEntri.Text = "Laporan Saldo Aging Hutang / Piutang"
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanSaldoHutangSupplier".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanSaldoAgingHutang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanSaldoAgingHutang Then
        '                    If F.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.HutangSupplier Then
        '                        frmEntri = F
        '                        Exit For
        '                    End If
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanSaldoAgingHutang
        '                frmEntri.Text = "Laporan Saldo Aging Hutang Supplier"
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '                frmEntri.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.HutangSupplier
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanSaldoPiutangCustomer".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanSaldoAgingHutang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanSaldoAgingHutang Then
        '                    If F.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.PiutangCustomer Then
        '                        frmEntri = F
        '                        Exit For
        '                    End If
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanSaldoAgingHutang
        '                frmEntri.Text = "Laporan Saldo Aging Piutang Customer"
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '                frmEntri.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.PiutangCustomer
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanSaldoPiutangRekanan".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanSaldoAgingHutang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanSaldoAgingHutang Then
        '                    If F.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.PiutangRekanan Then
        '                        frmEntri = F
        '                        Exit For
        '                    End If
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanSaldoAgingHutang
        '                frmEntri.Text = "Laporan Saldo Aging Piutang Rekanan"
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '                frmEntri.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.PiutangRekanan
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            'frLaporanRekapPemakaianVoucher

        '        ElseIf perintah(0).Trim.ToLower = "DaftarRekapKasir".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarEODPenjualanBKPNon = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarEODPenjualanBKPNon Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarEODPenjualanBKPNon
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "laporanrekappembelianbulanan".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanRekapPenmbelianGlobalBulanan = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanRekapPenmbelianGlobalBulanan Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanRekapPenmbelianGlobalBulanan
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            'frLaporanRekapPemakaianVoucher
        '        ElseIf perintah(0).Trim.ToLower = "SetoranKasirTanpaOmset".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarEODPenjualanSimple = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarEODPenjualanSimple Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarEODPenjualanSimple
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "SetoranKasir".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarEODPenjualan = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarEODPenjualan Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarEODPenjualan
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "SetoranPerKasir".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            'Dim frmEntri As frmDaftarEODPeruser = Nothing
        '            'Dim F As Object
        '            'For Each F In MdiChildren
        '            '    If TypeOf F Is frmDaftarEODPeruser Then
        '            '        frmEntri = F
        '            '        Exit For
        '            '    End If
        '            'Next
        '            'If frmEntri Is Nothing Then
        '            '    frmEntri = New frmDaftarEODPeruser
        '            '    frmEntri.WindowState = FormWindowState.Maximized
        '            '    frmEntri.MdiParent = Me
        '            'End If
        '            'frmEntri.Show()
        '            'frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "SaldoAwalNeraca".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmSaldoAwalAkun = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmSaldoAwalAkun Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmSaldoAwalAkun
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            'frLaporanRekapPemakaianVoucher
        '        ElseIf perintah(0).Trim.ToLower = "laporandetillpb".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanDetilLPB = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanDetilLPB Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanDetilLPB
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            'frLaporanRekapPemakaianVoucher
        '        ElseIf perintah(0).Trim.ToLower = "LaporanRekapPemakaianVoucher".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frLaporanRekapPemakaianVoucher = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frLaporanRekapPemakaianVoucher Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frLaporanRekapPemakaianVoucher
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            'frLaporanRekapPemakaianVoucher
        '        ElseIf perintah(0).Trim.ToLower = "LaporanRekapPenjualanPerProduk".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanRekapPenjualanPerProduk = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanRekapPenjualanPerProduk Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanRekapPenjualanPerProduk
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanRekapPenjualanPerSupplier".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanRekapPenjualanPerSupplier = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanRekapPenjualanPerSupplier Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanRekapPenjualanPerSupplier
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanRekapResetKasir".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanRekapResetKasir = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanRekapResetKasir Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanRekapResetKasir
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanQtyPenjualanPerSupplier".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanPenjualanPerSupplier = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPenjualanPerSupplier Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPenjualanPerSupplier
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanDetilPemakaianVoucher".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frLaporanDetilPemakaianVoucher = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frLaporanDetilPemakaianVoucher Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frLaporanDetilPemakaianVoucher
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "laporankashariantransaksional".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frLaporanKasHarianTransaksional = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frLaporanKasHarianTransaksional Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frLaporanKasHarianTransaksional
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "laporanaruskas".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frLaporanCashFlow = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frLaporanCashFlow Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frLaporanCashFlow

        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "mnlaporanaginghutang".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frLaporanSaldoAgingHutangPerNota = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frLaporanSaldoAgingHutangPerNota Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frLaporanSaldoAgingHutangPerNota
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "laporankhususbengkel".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As FormPoinMekanik = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is FormPoinMekanik Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New FormPoinMekanik

        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "mnlaporanagingpiutang".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            'Dim frmEntri As frLaporanAgingPiutang = Nothing
        '            Dim frmEntri As frmLaporanSaldoAgingPiutangPerJenisPenjualan = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                'If TypeOf F Is frLaporanAgingPiutang Then
        '                If TypeOf F Is frmLaporanSaldoAgingPiutangPerJenisPenjualan Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                'frmEntri = New frLaporanAgingPiutang
        '                frmEntri = New frmLaporanSaldoAgingPiutangPerJenisPenjualan
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            'laporan Hasil Stock Opname
        '            'ElseIf perintah(0).Trim.ToLower = "ServiceAutoPostingPenjualanPOS".ToLower Then
        '            '    'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            '    Dim frmEntri As frmServiceAutoPosting = Nothing
        '            '    Dim F As Object
        '            '    For Each F In MdiChildren
        '            '        If TypeOf F Is frmServiceAutoPosting Then
        '            '            frmEntri = F
        '            '            Exit For
        '            '        End If
        '            '    Next
        '            '    If frmEntri Is Nothing Then
        '            '        frmEntri = New frmServiceAutoPosting
        '            '        frmEntri.WindowState = FormWindowState.Maximized
        '            '        frmEntri.MdiParent = Me
        '            '    End If
        '            '    frmEntri.Show()
        '            '    frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanDetilPenjualan".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanPenjualanPromoDiskon = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPenjualanPromoDiskon Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPenjualanPromoDiskon
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "Daftarkasbank".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLUKas = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLUKas Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLUKas
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.PanelControl1.Visible = True
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarAsset".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frDaftarAktivaTetap = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frDaftarAktivaTetap Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frDaftarAktivaTetap
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            'frmEntri.pStatus = Publik.ptipe.Lihat

        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarJurnalUmum".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarJurnalUmum = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarJurnalUmum Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarJurnalUmum
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '                frmEntri.IDMenu = NullToLong(perintah(1).Trim.ToString)
        '            End If
        '            'frmEntri.pStatus = Publik.ptipe.Lihat

        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanBukuBesar".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frDaftarBukuBesar = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frDaftarBukuBesar Then
        '                    If TryCast(F, frDaftarBukuBesar).PtipeBukuBesar = frDaftarBukuBesar.PTipe.BukuBesar Then
        '                        frmEntri = F
        '                        Exit For
        '                    End If
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frDaftarBukuBesar
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            'frmEntri.pStatus = Publik.ptipe.Lihat

        '            frmEntri.PtipeBukuBesar = frDaftarBukuBesar.PTipe.BukuBesar
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanJurnalKosong".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1 frDaftarJurnalKosong
        '            Dim frmEntri As frDaftarJurnalKosong = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frDaftarJurnalKosong Then
        '                    If TryCast(F, frDaftarJurnalKosong).PtipeBukuBesar = frDaftarJurnalKosong.PTipe.JurnalKosong Then
        '                        frmEntri = F
        '                        Exit For
        '                    End If
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frDaftarJurnalKosong
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            'frmEntri.pStatus = Publik.ptipe.Lihat

        '            frmEntri.PtipeBukuBesar = frDaftarJurnalKosong.PTipe.JurnalKosong
        '            frmEntri.Show()
        '            frmEntri.Focus()

        '            ''KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1 frDaftarJurnalKosong
        '            'Dim frmEntri As frDaftarBukuBesar = Nothing
        '            'Dim F As Object
        '            'For Each F In MdiChildren
        '            '    If TypeOf F Is frDaftarBukuBesar Then
        '            '        If TryCast(F, frDaftarBukuBesar).PtipeBukuBesar = frDaftarBukuBesar.PTipe.JurnalKosong Then
        '            '            frmEntri = F
        '            '            Exit For
        '            '        End If
        '            '    End If
        '            'Next
        '            'If frmEntri Is Nothing Then
        '            '    frmEntri = New frDaftarBukuBesar
        '            '    frmEntri.WindowState = FormWindowState.Maximized
        '            '    frmEntri.MdiParent = Me
        '            'End If
        '            ''frmEntri.pStatus = Publik.ptipe.Lihat

        '            'frmEntri.PtipeBukuBesar = frDaftarBukuBesar.PTipe.JurnalKosong
        '            'frmEntri.Show()
        '            'frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanJurnalTidakBalance".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frDaftarJurnalTidakBalance = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frDaftarJurnalTidakBalance Then
        '                    If TryCast(F, frDaftarJurnalTidakBalance).PtipeBukuBesar = frDaftarJurnalTidakBalance.PTipe.JurnalTidakBalance Then
        '                        frmEntri = F
        '                        Exit For
        '                    End If
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frDaftarJurnalTidakBalance
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            'frmEntri.pStatus = Publik.ptipe.Lihat

        '            frmEntri.PtipeBukuBesar = frDaftarJurnalTidakBalance.PTipe.JurnalTidakBalance
        '            frmEntri.Show()
        '            frmEntri.Focus()

        '            ''KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            'Dim frmEntri As frDaftarBukuBesar = Nothing
        '            'Dim F As Object
        '            'For Each F In MdiChildren
        '            '    If TypeOf F Is frDaftarBukuBesar Then
        '            '        If TryCast(F, frDaftarBukuBesar).PtipeBukuBesar = frDaftarBukuBesar.PTipe.JurnalTidakBalance Then
        '            '            frmEntri = F
        '            '            Exit For
        '            '        End If
        '            '    End If
        '            'Next
        '            'If frmEntri Is Nothing Then
        '            '    frmEntri = New frDaftarBukuBesar
        '            '    frmEntri.WindowState = FormWindowState.Maximized
        '            '    frmEntri.MdiParent = Me
        '            'End If
        '            ''frmEntri.pStatus = Publik.ptipe.Lihat

        '            'frmEntri.PtipeBukuBesar = frDaftarBukuBesar.PTipe.JurnalTidakBalance
        '            'frmEntri.Show()
        '            'frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanNeracaPercobaan".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frLaporanMutasiBukuBesarNew = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frLaporanMutasiBukuBesarNew Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frLaporanMutasiBukuBesarNew
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            'frmEntri.pStatus = Publik.ptipe.Lihat

        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanLabaRugi".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frLaporanLabaRugi = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frLaporanLabaRugi Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frLaporanLabaRugi
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            'frmEntri.pStatus = Publik.ptipe.Lihat

        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanNeraca".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frLaporanNeraca = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frLaporanNeraca Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frLaporanNeraca
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            'frmEntri.pStatus = Publik.ptipe.Lihat

        '            frmEntri.Show()
        '            frmEntri.Focus()

        '        ElseIf perintah(0).Trim.ToLower = "DaftarPenyusutanAsset".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frDaftarPenyusutanAktiva = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frDaftarPenyusutanAktiva Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frDaftarPenyusutanAktiva
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarAkun".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLUAkun = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLUAkun Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLUAkun(mdlAccPublik.ptipe.Lihat)
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If

        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarSubKlasAkun".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLUSubKlasAkun = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLUSubKlasAkun Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLUSubKlasAkun
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.pStatus = mdlAccPublik.ptipe.Lihat

        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarKlasAkun".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLUKlasAkun = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLUKlasAkun Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLUKlasAkun
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.pStatus = mdlAccPublik.ptipe.Lihat

        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarGiroMasuk".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            'XtraMessageBox.Show("will be add soon")
        '            Dim frmEntri As frmDaftarGiroMasuk = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarGiroMasuk Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarGiroMasuk
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '                frmEntri.IDMenu = NullToLong(perintah(1).Trim.ToString)
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarGiroKeluar".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            'XtraMessageBox.Show("will be add soon")
        '            Dim frmEntri As frmDaftarGiroKeluar = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarGiroKeluar Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarGiroKeluar
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '                frmEntri.IDMenu = NullToLong(perintah(1).Trim.ToString)
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanHutangPiutangPerNota".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frLaporanHutangPiutang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frLaporanHutangPiutang Then
        '                    If DirectCast(DirectCast(F, System.Windows.Forms.Control).AccessibilityObject, System.Windows.Forms.Control.ControlAccessibleObject).Name = "Laporan Hutang Dan Piutang Per Nota" Then
        '                        frmEntri = F
        '                        Exit For
        '                    End If
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frLaporanHutangPiutang
        '                frmEntri.PtipeHutangPiutang = 2
        '                frmEntri.Text = "Laporan Hutang Dan Piutang Per Nota"
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanHutangPerNota".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frLaporanHutangPiutang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frLaporanHutangPiutang Then
        '                    If DirectCast(DirectCast(F, System.Windows.Forms.Control).AccessibilityObject, System.Windows.Forms.Control.ControlAccessibleObject).Name = "Laporan Hutang Per Nota" Then
        '                        frmEntri = F
        '                        Exit For
        '                    End If
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frLaporanHutangPiutang
        '                frmEntri.PtipeHutangPiutang = 0
        '                frmEntri.Text = "Laporan Hutang Per Nota"
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanPiutangPerNota".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frLaporanHutangPiutang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frLaporanHutangPiutang Then
        '                    If DirectCast(DirectCast(F, System.Windows.Forms.Control).AccessibilityObject, System.Windows.Forms.Control.ControlAccessibleObject).Name = "Laporan Piutang Per Nota" Then
        '                        frmEntri = F
        '                        Exit For
        '                    End If
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frLaporanHutangPiutang
        '                frmEntri.Text = "Laporan Piutang Per Nota"
        '                frmEntri.PtipeHutangPiutang = 1
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanRekapPenjualanPelunasanDanBiaya".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanRekapPenjualanPelunasanDanBiaya = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanRekapPenjualanPelunasanDanBiaya Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanRekapPenjualanPelunasanDanBiaya
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarKasKeluar".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarKasOut = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarKasOut Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarKasOut
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '                frmEntri.IDMenu = NullToLong(perintah(1).Trim.ToString)
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarKasMasuk".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarKasIN = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarKasIN Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarKasIN
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '                frmEntri.IDMenu = NullToLong(perintah(1).Trim.ToString)
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()

        '        ElseIf perintah(0).Trim.ToLower = "DaftarKasINOUTDetail".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarKasDetail = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarKasDetail Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarKasDetail
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '                frmEntri.IDMenu = NullToLong(perintah(1).Trim.ToString)
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()

        '        ElseIf perintah(0).Trim.ToLower = "LaporanStockOpnameD".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanStockOpnameD = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanStockOpnameD Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanStockOpnameD
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()

        '        ElseIf perintah(0).Trim.ToLower = "LaporanStokOpnameOnline".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanStockOpnameOnline = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanStockOpnameOnline Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanStockOpnameOnline
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()

        '        ElseIf perintah(0).Trim.ToLower = "LaporanRekapPemakaianVoucherCustomer".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanRekapPemakaianVoucherCustomer = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanRekapPemakaianVoucherCustomer Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanRekapPemakaianVoucherCustomer
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()

        '        ElseIf perintah(0).Trim.ToLower = "LaporanCrew".ToLower Then
        '            Dim frmEntri As New frmLihatPenjualanGuide
        '            frmEntri.ShowDialog(Me)
        '            frmEntri.Dispose()

        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            'Dim frmEntri As frmLihatPenjualanGuide = Nothing
        '            'Dim F As Object
        '            'For Each F In MdiChildren
        '            '    If TypeOf F Is frmLihatPenjualanGuide Then
        '            '        frmEntri = F
        '            '        Exit For
        '            '    End If
        '            'Next
        '            'If frmEntri Is Nothing Then
        '            '    frmEntri = New frmLihatPenjualanGuide
        '            '    frmEntri.WindowState = FormWindowState.Maximized
        '            '    frmEntri.MdiParent = Me
        '            'End If
        '            'frmEntri.Show()
        '            'frmEntri.Focus()

        '            'DaftarPPNMasukkan
        '        ElseIf perintah(0).Trim.ToLower = "DaftarPPNMasukkan".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarPPNMasukkan = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarPPNMasukkan Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarPPNMasukkan
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarPPNKeluaran".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarPPNKeluaran = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarPPNKeluaran Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarPPNKeluaran
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarKartuStokVarianPerGudang".ToLower Then
        '            ''KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            'Dim frmEntri As frmDaftarKartuStokVarian = Nothing
        '            'Dim F As Object
        '            'For Each F In MdiChildren
        '            '    If TypeOf F Is frmDaftarKartuStokVarian Then
        '            '        frmEntri = F
        '            '        Exit For
        '            '    End If
        '            'Next
        '            'If frmEntri Is Nothing Then
        '            '    frmEntri = New frmDaftarKartuStokVarian
        '            '    frmEntri.WindowState = FormWindowState.Maximized
        '            '    frmEntri.MdiParent = Me
        '            'End If
        '            'frmEntri.Show()
        '            'frmEntri.Focus()
        '            ''RekapPenjualanPerDepartemen

        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarKartuStokVarian = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarKartuStokVarian Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarKartuStokVarian
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            'RekapPenjualanPerDepartemen
        '        ElseIf perintah(0).Trim.ToLower = "RekapPenjualanPerDepartemen".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanPenjualanPerDepartemen = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPenjualanPerDepartemen Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPenjualanPerDepartemen
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()

        '            'LaporanRekapSaldoHutang
        '        ElseIf perintah(0).Trim.ToLower = "LaporanKartuPiutang".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanKartuHutangPerSupplier = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanKartuHutangPerSupplier Then
        '                    If F.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.ALL Then
        '                        frmEntri = F
        '                        Exit For
        '                    End If
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanKartuHutangPerSupplier
        '                frmEntri.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.ALL
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanKartuHutang".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanKartuHutangPerSupplier = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanKartuHutangPerSupplier Then
        '                    If F.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.ALL Then
        '                        frmEntri = F
        '                        Exit For
        '                    End If
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanKartuHutangPerSupplier
        '                frmEntri.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.ALL
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanKartuHutangSupplier".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanKartuHutangPerSupplier = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanKartuHutangPerSupplier Then
        '                    If F.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.HutangSupplier Then
        '                        frmEntri = F
        '                        Exit For
        '                    End If
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanKartuHutangPerSupplier
        '                frmEntri.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.HutangSupplier
        '                frmEntri.Text = "Kartu Hutang Supplier"
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanKartuPiutangCustomer".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanKartuHutangPerSupplier = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanKartuHutangPerSupplier Then
        '                    If F.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.PiutangCustomer Then
        '                        frmEntri = F
        '                        Exit For
        '                    End If
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanKartuHutangPerSupplier
        '                frmEntri.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.PiutangCustomer
        '                frmEntri.Text = "Kartu Piutang Customer"
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanKartuPiutangRekanan".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanKartuHutangPerSupplier = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanKartuHutangPerSupplier Then
        '                    If F.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.PiutangRekanan Then
        '                        frmEntri = F
        '                        Exit For
        '                    End If
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanKartuHutangPerSupplier
        '                frmEntri.JenisForm = frmLaporanSaldoAgingHutang.pHutangPiutang.PiutangRekanan
        '                frmEntri.Text = "Kartu Piutang Rekanan"
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanKartuHutangGroup".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanKartuHutangPerGroupSupplier = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanKartuHutangPerGroupSupplier Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanKartuHutangPerGroupSupplier
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanOmzetAccBonus2".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanOmzetBonus2 = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanOmzetBonus2 Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanOmzetBonus2
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanOmzetAccBonus3".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanOmzetBonus3 = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanOmzetBonus3 Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanOmzetBonus3
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanOmzetAccBonus".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanOmzetBonus = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanOmzetBonus Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanOmzetBonus
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanRekapSaldoHutang".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanRegisterHutangPiutang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanRegisterHutangPiutang Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanRegisterHutangPiutang
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()

        '            ''KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            'Dim frmEntri As frLaporanSaldoAgingHutangPerNota = Nothing
        '            'Dim F As Object
        '            'For Each F In MdiChildren
        '            '    If TypeOf F Is frLaporanSaldoAgingHutangPerNota Then
        '            '        frmEntri = F
        '            '        Exit For
        '            '    End If
        '            'Next
        '            'If frmEntri Is Nothing Then
        '            '    frmEntri = New frLaporanSaldoAgingHutangPerNota
        '            '    frmEntri.WindowState = FormWindowState.Maximized
        '            '    frmEntri.MdiParent = Me
        '            'End If
        '            'frmEntri.Show()
        '            'frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanRekapSaldoPiutang".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanSaldoAgingHutang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanSaldoAgingHutang Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanSaldoAgingHutang
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanPerbandinganQtyPembelianDanPenjualan".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanPembelianvsPenjualan = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPembelianvsPenjualan Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPembelianvsPenjualan
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            'CekHargaJual
        '        ElseIf perintah(0).Trim.ToLower = "LaporanSaldoStockPerRak".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanSaldoStokPerRak = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanSaldoStokPerRak Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanSaldoStokPerRak
        '                frmEntri.IDMenu = NullToLong(perintah(1).Trim.ToLower)
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            'CekHargaJual
        '        ElseIf perintah(0).Trim.ToLower = "LaporanSaldoStockPerVarian".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanSaldoPersediaanVarian = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanSaldoPersediaanVarian Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanSaldoPersediaanVarian
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanSaldoStokPerVarian".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanSaldoStokVarian = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanSaldoStokVarian Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanSaldoStokVarian
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            'CekHargaJual
        '        ElseIf perintah(0).Trim.ToLower = "LaporanMutasiStock".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanRegisterPersediaan = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanRegisterPersediaan Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanRegisterPersediaan
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()

        '            ''KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            'Dim frmEntri As frmLaporanMutasiStock = Nothing
        '            'Dim F As Object
        '            'For Each F In MdiChildren
        '            '    If TypeOf F Is frmLaporanMutasiStock Then
        '            '        frmEntri = F
        '            '        Exit For
        '            '    End If
        '            'Next
        '            'If frmEntri Is Nothing Then
        '            '    frmEntri = New frmLaporanMutasiStock
        '            '    frmEntri.IDMenu = NullToLong(perintah(1).Trim.ToLower)
        '            '    frmEntri.WindowState = FormWindowState.Maximized
        '            '    frmEntri.MdiParent = Me
        '            'End If
        '            'frmEntri.Show()
        '            'frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanSaldoStock".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanSaldoStok = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanSaldoStok Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanSaldoStok
        '                frmEntri.IDMenu = NullToLong(perintah(1).Trim.ToLower)
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanSaldoStockMinus".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanSaldoStokMinus = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanSaldoStokMinus Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanSaldoStokMinus
        '                frmEntri.IDMenu = NullToLong(perintah(1).Trim.ToLower)
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()

        '        ElseIf perintah(0).Trim.ToLower = "LaporanBarangExpiredRusak".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanBarangExpiredDanRusak = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanBarangExpiredDanRusak Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanBarangExpiredDanRusak
        '                frmEntri.IDMenu = NullToLong(perintah(1).Trim.ToLower)
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()

        '        ElseIf perintah(0).Trim.ToLower = "LaporanSaldoStockNew".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanSaldoPersediaan = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanSaldoPersediaan Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanSaldoPersediaan
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "FormOrderPO".ToLower Then
        '            Dim x As New FrmOrderPO
        '            Try
        '                If x.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        '                End If
        '            Catch ex As Exception
        '                XtraMessageBox.Show("Kesalahan : " & ex.Message)
        '            Finally
        '                x.Dispose()
        '            End Try
        '        ElseIf perintah(0).Trim.ToLower = "CekHargaJual".ToLower Then
        '            Dim x As New FrmCekHargaJual
        '            Try
        '                If x.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        '                End If
        '            Catch ex As Exception
        '                XtraMessageBox.Show("Kesalahan : " & ex.Message)
        '            Finally
        '                x.Dispose()
        '            End Try
        '        ElseIf perintah(0).Trim.ToLower = "CekHargaBeli".ToLower Then
        '            Dim x As New FrmCekHargaBeli
        '            Try
        '                If x.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        '                End If
        '            Catch ex As Exception
        '                XtraMessageBox.Show("Kesalahan : " & ex.Message)
        '            Finally
        '                x.Dispose()
        '            End Try
        '        ElseIf perintah(0).Trim.ToLower = "CekPoinMember".ToLower Then
        '            Dim x As New frmCekPoin
        '            Try
        '                If x.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        '                End If
        '            Catch ex As Exception
        '                XtraMessageBox.Show("Kesalahan : " & ex.Message)
        '            Finally
        '                x.Dispose()
        '            End Try
        '        ElseIf perintah(0).Trim.ToLower = "LaporanTopTenSales".ToLower Then
        '            Dim frmEntri As frmLaporanTopTenSales = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanTopTenSales Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanTopTenSales
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanRekapPenjualanPerCustomer".ToLower Then
        '            Dim frmEntri As frmLaporanRekapPenjualanPerCustomer = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanRekapPenjualanPerCustomer Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanRekapPenjualanPerCustomer
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanRekapPenjualanPerSales".ToLower Then
        '            Dim frmEntri As frmLaporanRekapPenjualanPerSalesman = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanRekapPenjualanPerSalesman Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanRekapPenjualanPerSalesman
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanBarangDiskonPromo".ToLower Then
        '            Dim frmEntri As frmLaporanBarangDiskonPromo = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanBarangDiskonPromo Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanBarangDiskonPromo
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanDetilPenjualanPerCustomer".ToLower Then
        '            Dim frmEntri As frmLaporanDetilPenjualanPerCustomer = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanDetilPenjualanPerCustomer Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanDetilPenjualanPerCustomer
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanPenjualanPerJam".ToLower Then
        '            Dim frmEntri As frmLaporanPenjualanPerJam = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPenjualanPerJam Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPenjualanPerJam
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "RekapPenjualanPerDepartemenBulanan".ToLower Then
        '            Dim frmEntri As frmLaporanRekapPenjualanPerDepartemen = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanRekapPenjualanPerDepartemen Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanRekapPenjualanPerDepartemen
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "RekapPenjualanPerDepartemenTahunan".ToLower Then
        '            Dim frmEntri As frmLaporanRekapPenjualanPerDepartemenTahunan = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanRekapPenjualanPerDepartemenTahunan Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanRekapPenjualanPerDepartemenTahunan
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanReturPembelian".ToLower Then
        '            Dim frmEntri As frmLaporanReturPembelianPerSupplier = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanReturPembelianPerSupplier Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanReturPembelianPerSupplier
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanPembelianPerDepartemenBulanan".ToLower Then
        '            Dim frmEntri As frmLaporanPembelianPerDepartemenBulanan = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPembelianPerDepartemenBulanan Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPembelianPerDepartemenBulanan
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanPembelianPerSupplierBulanan".ToLower Then
        '            Dim frmEntri As frmLaporanPembelianPerSupplierBulanan = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPembelianPerSupplierBulanan Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPembelianPerSupplierBulanan
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanLabaKotorPerBarang".ToLower Then
        '            Dim frmEntri As frmLaporanLabaKotorPerBarang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanLabaKotorPerBarang Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanLabaKotorPerBarang
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanLabaKotorPerKategori".ToLower Then
        '            Dim frmEntri As frmLaporanLabaKotorPerKategori = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanLabaKotorPerKategori Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanLabaKotorPerKategori
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "laporanpembelianpersupplierperdepartemen".ToLower Then
        '            Dim frmEntri As frmLaporanPembelianPerSupplierPerDepartemen = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPembelianPerSupplierPerDepartemen Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPembelianPerSupplierPerDepartemen
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "laporanomsetpersupplierperdepartemen".ToLower Then
        '            Dim frmEntri As frmLaporanOmzetPerSupplierPerDepartemen2 = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanOmzetPerSupplierPerDepartemen2 Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanOmzetPerSupplierPerDepartemen2
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanLabaKotorPerHari".ToLower Then
        '            Dim frmEntri As frmLaporanLabaKotorPerhari = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanLabaKotorPerhari Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanLabaKotorPerhari
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DownloadPos".ToLower Then
        '            Dim x As New FrmDownloadPenjualanKasir
        '            Try
        '                If x.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        '                End If
        '            Catch ex As Exception
        '                XtraMessageBox.Show("Kesalahan : " & ex.Message)
        '            Finally
        '                x.Dispose()
        '            End Try
        '        ElseIf perintah(0).Trim.ToLower = "DownloadRekapPenjualanKasir".ToLower Then
        '            Dim x As New FrmDownloadRekapPenjualanKasir
        '            Try
        '                If x.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        '                End If
        '            Catch ex As Exception
        '                XtraMessageBox.Show("Kesalahan : " & ex.Message)
        '            Finally
        '                x.Dispose()
        '            End Try
        '        ElseIf perintah(0).Trim.ToLower = "RubahMasterJenis".ToLower Then
        '            Dim x As New frmRubahJenisBarang
        '            Try
        '                If x.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

        '                End If
        '            Catch ex As Exception
        '                XtraMessageBox.Show("Kesalahan : " & ex.Message)
        '            Finally
        '                x.Dispose()
        '            End Try
        '        ElseIf perintah(0).Trim.ToLower = "DaftarPembayaranHutang".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frBayarHutang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frBayarHutang Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frBayarHutang
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            'ElseIf perintah(0).Trim.ToLower = "DaftarHutangSupplier".ToLower Then
        '            '    'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            '    Dim frmEntri As frmDaftarBayarHutang = Nothing
        '            '    Dim F As Object
        '            '    For Each F In MdiChildren
        '            '        If TypeOf F Is frmDaftarBayarHutang Then
        '            '            frmEntri = F
        '            '            If frmEntri.IsHutang Then
        '            '                Exit For
        '            '            Else
        '            '                frmEntri = Nothing
        '            '            End If
        '            '            Exit For
        '            '        End If
        '            '    Next
        '            '    If frmEntri Is Nothing Then
        '            '        frmEntri = New frmDaftarBayarHutang
        '            '        frmEntri.WindowState = FormWindowState.Maximized
        '            '        frmEntri.MdiParent = Me
        '            '        frmEntri.IsHutang = True
        '            '    End If
        '            '    frmEntri.Show()
        '            '    frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarPembayaranPiutang".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frBayarPiutang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frBayarPiutang Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frBayarPiutang
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            'ElseIf perintah(0).Trim.ToLower = "DaftarPiutangCustomer".ToLower Then
        '            '    'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            '    Dim frmEntri As frmDaftarBayarHutang = Nothing
        '            '    Dim F As Object
        '            '    For Each F In MdiChildren
        '            '        If TypeOf F Is frmDaftarBayarHutang Then
        '            '            frmEntri = F
        '            '            If Not frmEntri.IsHutang Then
        '            '                Exit For
        '            '            Else
        '            '                frmEntri = Nothing
        '            '            End If
        '            '            Exit For
        '            '        End If
        '            '    Next
        '            '    If frmEntri Is Nothing Then
        '            '        frmEntri = New frmDaftarBayarHutang
        '            '        frmEntri.WindowState = FormWindowState.Maximized
        '            '        frmEntri.MdiParent = Me
        '            '        frmEntri.IsHutang = False
        '            '    End If
        '            '    frmEntri.Show()
        '            '    frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarLapPiutangPerCustomer".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarPiutangPerCustomer = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarPiutangPerCustomer Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarPiutangPerCustomer
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarBarang".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarBarang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarBarang Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarBarang
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.IDMenu = NullToLong(perintah(1).Trim.ToLower)
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarRencanaPerubahanHarga".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarRencanaPerubahanHarga = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarRencanaPerubahanHarga Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarRencanaPerubahanHarga
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.IDMenu = NullToLong(perintah(1).Trim.ToLower)
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "PerubahanHargaJual".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarPerubahanHarga = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarPerubahanHarga Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarPerubahanHarga
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "PerubahanHargaBeli".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarPerubahanHargaBeli = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarPerubahanHargaBeli Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarPerubahanHargaBeli
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarAlamat".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarAlamat = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarAlamat Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarAlamat
        '                frmEntri.IDMenu = NullToLong(perintah(1).Trim.ToLower)
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarJenisBarang".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarJenisBarang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarJenisBarang Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarJenisBarang
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "DaftarBarcode".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmCetakBarcodeVer2 = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmCetakBarcodeVer2 Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmCetakBarcodeVer2
        '                frmEntri.TypeCetak = frmCetakBarcodeVer2.Tipe.Bartender
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            'MDI AJA BRO REPORTNYA GBA BISA

        '            'Dim frmEntri As frmCetakBarcodeVer2 = Nothing

        '            'Try
        '            '    'CetakBarcodeBartender.RemoveRange(0, 0)
        '            '    If CetakBarcodeBartender.Count >= 1 Then
        '            '        XtraMessageBox.Show("form Cetak Barcode Sudah dibuka!", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '            '        frmEntri = CetakBarcodeBartender.Item(0)
        '            '        frmEntri.Show()
        '            '        frmEntri.WindowState = FormWindowState.Normal
        '            '    Else
        '            '        frmEntri = New frmCetakBarcodeVer2
        '            '        frmEntri.TypeCetak = frmCetakBarcodeVer2.Tipe.Bartender
        '            '        frmEntri.FormPemanggil = Me
        '            '        frmEntri.Show()
        '            '        CetakBarcodeBartender.Add(frmEntri)
        '            '    End If
        '            'Catch ex As Exception
        '            '    XtraMessageBox.Show("Info " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '            'Finally
        '            '    'frmEntri.Dispose()
        '            'End Try
        '        ElseIf perintah(0).Trim.ToLower = "DaftarBarcodeV2".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmCetakBarcodeVer2 = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmCetakBarcodeVer2 Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmCetakBarcodeVer2
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            'MDI AJA BRO REPORTNYA GBA BISA
        '            'Dim frmEntri As frmCetakBarcodeVer2 = Nothing

        '            'Try
        '            '    If CetakBarcodeDevexpress.Count >= 1 Then
        '            '        XtraMessageBox.Show("form Cetak Barcode Sudah dibuka!", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '            '        frmEntri = CetakBarcodeDevexpress.Item(0)
        '            '        frmEntri.Show()
        '            '    Else
        '            '        frmEntri = New frmCetakBarcodeVer2
        '            '        frmEntri.TypeCetak = frmCetakBarcodeVer2.Tipe.DevExpress
        '            '        frmEntri.FormPemanggil = Me
        '            '        frmEntri.Show()
        '            '        CetakBarcodeDevexpress.Add(frmEntri)
        '            '    End If
        '            'Catch ex As Exception
        '            '    XtraMessageBox.Show("Info " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '            'Finally
        '            '    'frmEntri.Dispose()
        '            'End Try
        '        ElseIf perintah(0).Trim.ToLower = "DaftarTandaTerimaFakturPajak".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarTTFakturPajak = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarTTFakturPajak Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarTTFakturPajak
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "LaporanPelunasanHutangRealVSBayangan".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanPelunasanRealVSBayangan = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanPelunasanRealVSBayangan Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanPelunasanRealVSBayangan
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "mnmasterpromocustomer".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmDaftarPromoPenjualan = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmDaftarPromoPenjualan Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmDaftarPromoPenjualan
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '        ElseIf perintah(0).Trim.ToLower = "mngeneratepo".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmGeneratePO = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmGeneratePO Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmGeneratePO
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()
        '            frmEntri.SimpleButton1.Visible = False
        '            frmEntri.SimpleButton3.Visible = False

        '        ElseIf perintah(0).Trim.ToLower = "DaftarRak".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frDaftarRak = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frDaftarRak Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frDaftarRak
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()

        '        ElseIf perintah(0).Trim.ToLower = "DaftarSewaRak".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frDaftarSewaDisplay = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frDaftarSewaDisplay Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frDaftarSewaDisplay
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()

        '        ElseIf perintah(0).Trim.ToLower = "laporanhistorihutangpiutang".ToLower Then
        '            'KODE DIBAWAH UNTUK MEMBUKA FORM HANYA 1
        '            Dim frmEntri As frmLaporanHistoriHutangPiutang = Nothing
        '            Dim F As Object
        '            For Each F In MdiChildren
        '                If TypeOf F Is frmLaporanHistoriHutangPiutang Then
        '                    frmEntri = F
        '                    Exit For
        '                End If
        '            Next
        '            If frmEntri Is Nothing Then
        '                frmEntri = New frmLaporanHistoriHutangPiutang
        '                frmEntri.WindowState = FormWindowState.Maximized
        '                frmEntri.MdiParent = Me
        '            End If
        '            frmEntri.Show()
        '            frmEntri.Focus()

        '        ElseIf UBound(perintah) >= 0 Then
        '            ExecuteFormbyCommand(perintah(0).Trim, NullToLong(perintah(1).Trim))
        '        End If
        '    Catch ex As Exception
        '        XtraMessageBox.Show("Gagal Load Menu. Perintah menu " & e.Item.Caption & " (" & e.Item.Name & ") " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    End Try
        'Else
        '    XtraMessageBox.Show("Perintah menu " & e.Item.Caption & " (" & e.Item.Name & ") belum disetting!", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'End If
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