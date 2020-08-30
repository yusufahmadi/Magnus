Imports DevExpress.XtraEditors
Imports DevExpress.Utils
Imports System.IO.File
Imports DevExpress.Utils.Menu

Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Helpers
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data
Imports DevExpress.XtraEditors.Repository

Imports System.Data.Linq
Imports System.Linq
Imports System.ComponentModel
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.Data.Filtering.Helpers

Public Class FungsiControl

    Public Enum action_
        Edit = 0
        Preview = 1
        Print = 2
    End Enum
    Public Enum ptipe
        Lihat = 0
        LookUp = 1
        LookUpParent = 2
        Baru = 3
        Edit = 4
    End Enum
    Public Enum ptipeJenisBukuPembantu
        TanpaBukuPembantu = 0
        Persediaan = 1
        HutangPiutang = 2
        BiayaKendaraan = 3
    End Enum
    Public Enum button_ As Long
        cmdNew = 0
        cmdEdit = 1
        cmdDelete = 2
        cmdOK = 3
        cmdCancel = 4
        cmdRefresh = 5
        cmdExit = 6
        cmdExportXls = 7
        cmdMark = 8
        cmdUnMark = 9
        cmdPosting = 10
        cmdUnposting = 11
        cmdPrint = 12
        cmdSave = 13
        cmdCancelSave = 14
        cmdViewDetil = 15
        cmdReset = 16
        cmdLogin = 17
        cmdLogout = 18
        cmdDatabase = 19
        cmdLainnya = 99
    End Enum
    Public Shared FontName As String, FontSize As Long, IsBold As Boolean
    Public Shared Function LayOutKu(ByVal frm As String) As String
        'Dim FolderLayout As String = VPOINT.Serialshield.Ini.BacaIni("Application", "LayoutSource", Application.StartupPath)
        'If Not DirectoryExists(FolderLayout) Then
        '    System.IO.Directory.CreateDirectory(FolderLayout)
        'End If
        LayOutKu = LayoutsHelper.FolderLayouts & frm.ToString & ".xml"
    End Function

    Public Shared Function CekAngka(ByVal v1)
        If InStr(1, "1234567890," & Chr(8), Chr(v1)) Then
            CekAngka = v1
        Else
            CekAngka = 0
        End If
    End Function
    Private Shared Sub lkEdit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Back Then
                TryCast(sender, DXSample.CustomSearchLookUpEdit).EditValue = -1
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Info : " & ex.Message & " Keys : " & e.KeyCode.ToString, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    Public Shared Sub OnUpdateDisplayFilter(ByVal sender As Object, ByVal e As DXSample.DisplayFilterEventArgs)
        If e.FilterText.IndexOf(" "c) = -1 Then Return
        e.FilterText = """"c + e.FilterText + """"c
    End Sub

    Public Shared Sub SetForm(ByRef frm As DevExpress.XtraEditors.XtraForm)
        Dim ico As System.Drawing.Icon = Nothing
        Try
            For Each ctrl In frm.Controls
                If TypeOf ctrl Is DevExpress.XtraLayout.LayoutControl Then
                    Dim ly As DevExpress.XtraLayout.LayoutControl = TryCast(ctrl, DevExpress.XtraLayout.LayoutControl)
                    If IsEditLayout Then
                        ly.AllowCustomization = True
                    Else
                        ly.AllowCustomization = False
                    End If
                    ly.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignMode.AlignInGroups

                    For Each ctl In ly.Controls
                        If TypeOf ctl Is DevExpress.XtraEditors.SimpleButton Then
                            Dim btn As DevExpress.XtraEditors.SimpleButton = TryCast(ctl, DevExpress.XtraEditors.SimpleButton)
                            btn.Appearance.Options.UseBackColor = False
                            btn.Appearance.Options.UseBorderColor = False
                            btn.Appearance.Options.UseFont = True
                            btn.Appearance.Options.UseForeColor = False
                            btn.LookAndFeel.UseDefaultLookAndFeel = True
                            AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
                            AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
                        ElseIf TypeOf ctl Is DevExpress.XtraGrid.GridControl Then
                            Dim gc1 As DevExpress.XtraGrid.GridControl = TryCast(ctl, DevExpress.XtraGrid.GridControl)
                            Dim gv1 As DevExpress.XtraGrid.Views.Grid.GridView = gc1.DefaultView
                            gv1.OptionsBehavior.AllowIncrementalSearch = True
                            AddHandler gv1.ShowCustomizationForm, AddressOf GV1_ShowCustomizationForm
                            gv1.OptionsMenu.ShowGroupSummaryEditorItem = True
                            Dim TempMyFindPanelFilterHelper As New MyFindPanelFilterHelper(gv1)
                            AddHandler gv1.CustomDrawCell, AddressOf GV1_CustomDrawCell
                        ElseIf TypeOf ctl Is DevExpress.XtraEditors.DateEdit Then
                            Dim x As DateEdit = TryCast(ctl, DateEdit)
                            x.EnterMoveNextControl = True
                            x.Properties.DisplayFormat.FormatString = "dd-MM-yyyy"
                            x.Properties.EditFormat.FormatString = "dd-MM-yyyy"
                            x.Properties.Mask.EditMask = "dd-MM-yyyy"
                            x.Properties.EditFormat.FormatType = FormatType.DateTime
                            x.Properties.DisplayFormat.FormatType = FormatType.DateTime
                            x.Properties.Mask.MaskType = Mask.MaskType.DateTimeAdvancingCaret
                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                        ElseIf TypeOf ctl Is DXSample.CustomSearchLookUpEdit Then
                            Dim x As DXSample.CustomSearchLookUpEdit = TryCast(ctl, DXSample.CustomSearchLookUpEdit)
                            AddHandler x.UpdateDisplayFilter, AddressOf OnUpdateDisplayFilter
                            AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                            x.Properties.AllowMouseWheel = False
                            x.Properties.UseCtrlScroll = True
                            If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                                x.Properties.PopupFilterMode = PopupFilterMode.Default
                            Else
                                x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                                x.Properties.View.ActiveFilterEnabled = False
                                x.Properties.View.ActiveFilterString = ""
                            End If
                        ElseIf TypeOf ctl Is DevExpress.XtraEditors.TextEdit Then
                            Dim x As TextEdit = TryCast(ctl, TextEdit)
                            If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                If x.Properties.DisplayFormat.FormatString = "" Then
                                    x.Properties.DisplayFormat.FormatString = "n2"
                                End If
                                x.Properties.EditFormat.FormatType = FormatType.Numeric
                                If x.Properties.EditFormat.FormatString = "" Then
                                    x.Properties.EditFormat.FormatString = "n2"
                                End If
                                x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                If x.Properties.Mask.EditMask = "" Then
                                    x.Properties.Mask.EditMask = "n2"
                                End If
                                x.Properties.Mask.UseMaskAsDisplayFormat = True
                                AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                AddHandler x.GotFocus, AddressOf txt_GotFocus
                                AddHandler x.Click, AddressOf txt_GotFocus
                            End If
                            ''x.Properties.CharacterCasing = CharacterCasing.Upper
                        ElseIf TypeOf ctl Is DevExpress.XtraEditors.ButtonEdit Then
                            Dim x As ButtonEdit = TryCast(ctl, ButtonEdit)
                            If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                If x.Properties.DisplayFormat.FormatString = "" Then
                                    x.Properties.DisplayFormat.FormatString = "n2"
                                End If
                                x.Properties.EditFormat.FormatType = FormatType.Numeric
                                If x.Properties.EditFormat.FormatString = "" Then
                                    x.Properties.EditFormat.FormatString = "n2"
                                End If
                                x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                If x.Properties.Mask.EditMask = "" Then
                                    x.Properties.Mask.EditMask = "n2"
                                End If
                                x.Properties.Mask.UseMaskAsDisplayFormat = True
                                AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                AddHandler x.GotFocus, AddressOf button_GotFocus
                                AddHandler x.Click, AddressOf button_GotFocus
                            End If
                            ''x.Properties.CharacterCasing = CharacterCasing.Upper
                        ElseIf TypeOf ctl Is DevExpress.XtraEditors.CalcEdit Then
                            Dim x As CalcEdit = TryCast(ctl, CalcEdit)
                            If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                If x.Properties.DisplayFormat.FormatString = "" Then
                                    x.Properties.DisplayFormat.FormatString = "n2"
                                End If
                                x.Properties.EditFormat.FormatType = FormatType.Numeric
                                If x.Properties.EditFormat.FormatString = "" Then
                                    x.Properties.EditFormat.FormatString = "n2"
                                End If
                                x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                If x.Properties.Mask.EditMask = "" Then
                                    x.Properties.Mask.EditMask = "n2"
                                End If
                                x.Properties.Mask.UseMaskAsDisplayFormat = True
                                AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                AddHandler x.GotFocus, AddressOf clc_GotFocus
                                AddHandler x.Click, AddressOf clc_GotFocus
                            End If
                            ''x.Properties.CharacterCasing = CharacterCasing.Upper
                            'Tambahan Akbar, ada XtraTab di dalam LayoutControl
                        ElseIf TypeOf ctl Is DevExpress.XtraTab.XtraTabControl Then
                            Dim tab As DevExpress.XtraTab.XtraTabControl = TryCast(ctl, DevExpress.XtraTab.XtraTabControl)
                            For Each tb In tab.TabPages
                                Dim TabPage As DevExpress.XtraTab.XtraTabPage = TryCast(tb, DevExpress.XtraTab.XtraTabPage)
                                For Each ctlax In TabPage.Controls
                                    If TypeOf ctlax Is DevExpress.XtraLayout.LayoutControl Then
                                        Dim lyx As DevExpress.XtraLayout.LayoutControl = TryCast(ctlax, DevExpress.XtraLayout.LayoutControl)
                                        If IsEditLayout Then
                                            lyx.AllowCustomization = True
                                        Else
                                            lyx.AllowCustomization = False
                                        End If
                                        lyx.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignMode.AlignInGroups
                                        For Each ctlxx In lyx.Controls
                                            If TypeOf ctlxx Is DevExpress.XtraEditors.SimpleButton Then
                                                Dim btn As DevExpress.XtraEditors.SimpleButton = TryCast(ctlxx, DevExpress.XtraEditors.SimpleButton)
                                                btn.Appearance.Options.UseBackColor = False
                                                btn.Appearance.Options.UseBorderColor = False
                                                btn.Appearance.Options.UseFont = True
                                                btn.Appearance.Options.UseForeColor = False
                                                btn.LookAndFeel.UseDefaultLookAndFeel = True
                                                AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
                                                AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
                                            ElseIf TypeOf ctlxx Is DevExpress.XtraGrid.GridControl Then
                                                Dim gc1 As DevExpress.XtraGrid.GridControl = TryCast(ctlxx, DevExpress.XtraGrid.GridControl)
                                                Dim gv1 As DevExpress.XtraGrid.Views.Grid.GridView = gc1.DefaultView
                                                gv1.OptionsBehavior.AllowIncrementalSearch = True
                                                AddHandler gv1.ShowCustomizationForm, AddressOf GV1_ShowCustomizationForm
                                                gv1.OptionsMenu.ShowGroupSummaryEditorItem = True
                                                Dim TempMyFindPanelFilterHelper As New MyFindPanelFilterHelper(gv1)
                                                AddHandler gv1.CustomDrawCell, AddressOf GV1_CustomDrawCell
                                            ElseIf TypeOf ctlxx Is DevExpress.XtraEditors.DateEdit Then
                                                Dim x As DateEdit = TryCast(ctlxx, DateEdit)
                                                x.EnterMoveNextControl = True
                                                x.Properties.DisplayFormat.FormatString = "dd-MM-yyyy"
                                                x.Properties.EditFormat.FormatString = "dd-MM-yyyy"
                                                x.Properties.Mask.EditMask = "dd-MM-yyyy"
                                                x.Properties.EditFormat.FormatType = FormatType.DateTime
                                                x.Properties.DisplayFormat.FormatType = FormatType.DateTime
                                                x.Properties.Mask.MaskType = Mask.MaskType.DateTimeAdvancingCaret
                                                x.Properties.Mask.UseMaskAsDisplayFormat = True
                                            ElseIf TypeOf ctlxx Is DXSample.CustomSearchLookUpEdit Then
                                                Dim x As DXSample.CustomSearchLookUpEdit = TryCast(ctlxx, DXSample.CustomSearchLookUpEdit)
                                                AddHandler x.UpdateDisplayFilter, AddressOf OnUpdateDisplayFilter
                                                AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                                                x.Properties.AllowMouseWheel = False
                                                x.Properties.UseCtrlScroll = True
                                                If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                                                    x.Properties.PopupFilterMode = PopupFilterMode.Default
                                                Else
                                                    x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                                                    x.Properties.View.ActiveFilterEnabled = False
                                                    x.Properties.View.ActiveFilterString = ""
                                                End If
                                            ElseIf TypeOf ctlxx Is DevExpress.XtraEditors.TextEdit Then
                                                Dim x As TextEdit = TryCast(ctlxx, TextEdit)
                                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                                        x.Properties.DisplayFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.EditFormat.FormatString = "" Then
                                                        x.Properties.EditFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                    If x.Properties.Mask.EditMask = "" Then
                                                        x.Properties.Mask.EditMask = "n2"
                                                    End If
                                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                    AddHandler x.GotFocus, AddressOf txt_GotFocus
                                                    AddHandler x.Click, AddressOf txt_GotFocus
                                                End If
                                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                                            ElseIf TypeOf ctlxx Is DevExpress.XtraEditors.ButtonEdit Then
                                                Dim x As ButtonEdit = TryCast(ctlxx, ButtonEdit)
                                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                                        x.Properties.DisplayFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.EditFormat.FormatString = "" Then
                                                        x.Properties.EditFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                    If x.Properties.Mask.EditMask = "" Then
                                                        x.Properties.Mask.EditMask = "n2"
                                                    End If
                                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                    AddHandler x.GotFocus, AddressOf button_GotFocus
                                                    AddHandler x.Click, AddressOf button_GotFocus
                                                End If
                                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                                            ElseIf TypeOf ctlxx Is DevExpress.XtraEditors.CalcEdit Then
                                                Dim x As CalcEdit = TryCast(ctlxx, CalcEdit)
                                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                                        x.Properties.DisplayFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.EditFormat.FormatString = "" Then
                                                        x.Properties.EditFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                    If x.Properties.Mask.EditMask = "" Then
                                                        x.Properties.Mask.EditMask = "n2"
                                                    End If
                                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                    AddHandler x.GotFocus, AddressOf clc_GotFocus
                                                    AddHandler x.Click, AddressOf clc_GotFocus
                                                End If
                                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                                            End If
                                        Next
                                    ElseIf TypeOf ctlax Is DevExpress.XtraEditors.SimpleButton Then
                                        Dim btn As DevExpress.XtraEditors.SimpleButton = TryCast(ctlax, DevExpress.XtraEditors.SimpleButton)
                                        btn.Appearance.Options.UseBackColor = False
                                        btn.Appearance.Options.UseBorderColor = False
                                        btn.Appearance.Options.UseFont = True
                                        btn.Appearance.Options.UseForeColor = False
                                        btn.LookAndFeel.UseDefaultLookAndFeel = True
                                        AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
                                        AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
                                    ElseIf TypeOf ctlax Is DevExpress.XtraGrid.GridControl Then
                                        Dim gc1 As DevExpress.XtraGrid.GridControl = TryCast(ctlax, DevExpress.XtraGrid.GridControl)
                                        Dim gv1 As DevExpress.XtraGrid.Views.Grid.GridView = gc1.DefaultView
                                        gv1.OptionsBehavior.AllowIncrementalSearch = True
                                        AddHandler gv1.ShowCustomizationForm, AddressOf GV1_ShowCustomizationForm
                                        gv1.OptionsMenu.ShowGroupSummaryEditorItem = True
                                        Dim TempMyFindPanelFilterHelper As New MyFindPanelFilterHelper(gv1)
                                        AddHandler gv1.CustomDrawCell, AddressOf GV1_CustomDrawCell
                                    ElseIf TypeOf ctlax Is DevExpress.XtraEditors.XtraPanel Then
                                        Dim pnlx As DevExpress.XtraEditors.XtraPanel = TryCast(ctlax, DevExpress.XtraEditors.XtraPanel)
                                        For Each ctlbx In pnlx.Controls
                                            If TypeOf ctlbx Is DevExpress.XtraLayout.LayoutControl Then
                                                Dim lyx As DevExpress.XtraLayout.LayoutControl = TryCast(ctlbx, DevExpress.XtraLayout.LayoutControl)
                                                If IsEditLayout Then
                                                    lyx.AllowCustomization = True
                                                Else
                                                    lyx.AllowCustomization = False
                                                End If
                                                lyx.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignMode.AlignInGroups
                                                For Each ctlxx In lyx.Controls
                                                    If TypeOf ctlxx Is DevExpress.XtraEditors.SimpleButton Then
                                                        Dim btn As DevExpress.XtraEditors.SimpleButton = TryCast(ctlxx, DevExpress.XtraEditors.SimpleButton)
                                                        btn.Appearance.Options.UseBackColor = False
                                                        btn.Appearance.Options.UseBorderColor = False
                                                        btn.Appearance.Options.UseFont = True
                                                        btn.Appearance.Options.UseForeColor = False
                                                        btn.LookAndFeel.UseDefaultLookAndFeel = True
                                                        AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
                                                        AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
                                                    ElseIf TypeOf ctlxx Is DevExpress.XtraGrid.GridControl Then
                                                        Dim gc1 As DevExpress.XtraGrid.GridControl = TryCast(ctlxx, DevExpress.XtraGrid.GridControl)
                                                        Dim gv1 As DevExpress.XtraGrid.Views.Grid.GridView = gc1.DefaultView
                                                        gv1.OptionsBehavior.AllowIncrementalSearch = True
                                                        AddHandler gv1.ShowCustomizationForm, AddressOf GV1_ShowCustomizationForm
                                                        gv1.OptionsMenu.ShowGroupSummaryEditorItem = True
                                                        Dim TempMyFindPanelFilterHelper As New MyFindPanelFilterHelper(gv1)
                                                        AddHandler gv1.CustomDrawCell, AddressOf GV1_CustomDrawCell
                                                    ElseIf TypeOf ctlxx Is DevExpress.XtraEditors.DateEdit Then
                                                        Dim x As DateEdit = TryCast(ctlxx, DateEdit)
                                                        x.EnterMoveNextControl = True
                                                        x.Properties.DisplayFormat.FormatString = "dd-MM-yyyy"
                                                        x.Properties.EditFormat.FormatString = "dd-MM-yyyy"
                                                        x.Properties.Mask.EditMask = "dd-MM-yyyy"
                                                        x.Properties.EditFormat.FormatType = FormatType.DateTime
                                                        x.Properties.DisplayFormat.FormatType = FormatType.DateTime
                                                        x.Properties.Mask.MaskType = Mask.MaskType.DateTimeAdvancingCaret
                                                        x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                    ElseIf TypeOf ctlxx Is DXSample.CustomSearchLookUpEdit Then
                                                        Dim x As DXSample.CustomSearchLookUpEdit = TryCast(ctlxx, DXSample.CustomSearchLookUpEdit)
                                                        AddHandler x.UpdateDisplayFilter, AddressOf OnUpdateDisplayFilter
                                                        AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                                                        x.Properties.AllowMouseWheel = False
                                                        x.Properties.UseCtrlScroll = True
                                                        If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                                                            x.Properties.PopupFilterMode = PopupFilterMode.Default
                                                        Else
                                                            x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                                                            x.Properties.View.ActiveFilterEnabled = False
                                                            x.Properties.View.ActiveFilterString = ""
                                                        End If
                                                    ElseIf TypeOf ctlxx Is DevExpress.XtraEditors.TextEdit Then
                                                        Dim x As TextEdit = TryCast(ctlxx, TextEdit)
                                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                                x.Properties.DisplayFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.EditFormat.FormatString = "" Then
                                                                x.Properties.EditFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                            If x.Properties.Mask.EditMask = "" Then
                                                                x.Properties.Mask.EditMask = "n2"
                                                            End If
                                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                            AddHandler x.GotFocus, AddressOf txt_GotFocus
                                                            AddHandler x.Click, AddressOf txt_GotFocus
                                                        End If
                                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                                    ElseIf TypeOf ctlxx Is DevExpress.XtraEditors.ButtonEdit Then
                                                        Dim x As ButtonEdit = TryCast(ctlxx, ButtonEdit)
                                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                                x.Properties.DisplayFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.EditFormat.FormatString = "" Then
                                                                x.Properties.EditFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                            If x.Properties.Mask.EditMask = "" Then
                                                                x.Properties.Mask.EditMask = "n2"
                                                            End If
                                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                            AddHandler x.GotFocus, AddressOf button_GotFocus
                                                            AddHandler x.Click, AddressOf button_GotFocus
                                                        End If
                                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                                    ElseIf TypeOf ctlxx Is DevExpress.XtraEditors.CalcEdit Then
                                                        Dim x As CalcEdit = TryCast(ctlxx, CalcEdit)
                                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                                x.Properties.DisplayFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.EditFormat.FormatString = "" Then
                                                                x.Properties.EditFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                            If x.Properties.Mask.EditMask = "" Then
                                                                x.Properties.Mask.EditMask = "n2"
                                                            End If
                                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                            AddHandler x.GotFocus, AddressOf clc_GotFocus
                                                            AddHandler x.Click, AddressOf clc_GotFocus
                                                        End If
                                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                                    End If
                                                Next
                                            ElseIf TypeOf ctlbx Is DevExpress.XtraEditors.SimpleButton Then
                                                Dim btn As DevExpress.XtraEditors.SimpleButton = TryCast(ctlbx, DevExpress.XtraEditors.SimpleButton)
                                                btn.Appearance.Options.UseBackColor = False
                                                btn.Appearance.Options.UseBorderColor = False
                                                btn.Appearance.Options.UseFont = True
                                                btn.Appearance.Options.UseForeColor = False
                                                btn.LookAndFeel.UseDefaultLookAndFeel = True
                                                AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
                                                AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
                                            ElseIf TypeOf ctlbx Is DevExpress.XtraGrid.GridControl Then
                                                Dim gc1 As DevExpress.XtraGrid.GridControl = TryCast(ctlbx, DevExpress.XtraGrid.GridControl)
                                                Dim gv1 As DevExpress.XtraGrid.Views.Grid.GridView = gc1.DefaultView
                                                gv1.OptionsBehavior.AllowIncrementalSearch = True
                                                AddHandler gv1.ShowCustomizationForm, AddressOf GV1_ShowCustomizationForm
                                                gv1.OptionsMenu.ShowGroupSummaryEditorItem = True
                                                Dim TempMyFindPanelFilterHelper As New MyFindPanelFilterHelper(gv1)
                                                AddHandler gv1.CustomDrawCell, AddressOf GV1_CustomDrawCell
                                            ElseIf TypeOf ctlbx Is DevExpress.XtraEditors.DateEdit Then
                                                Dim x As DateEdit = TryCast(ctlbx, DateEdit)
                                                x.EnterMoveNextControl = True
                                                x.Properties.DisplayFormat.FormatString = "dd-MM-yyyy"
                                                x.Properties.EditFormat.FormatString = "dd-MM-yyyy"
                                                x.Properties.Mask.EditMask = "dd-MM-yyyy"
                                                x.Properties.EditFormat.FormatType = FormatType.DateTime
                                                x.Properties.DisplayFormat.FormatType = FormatType.DateTime
                                                x.Properties.Mask.MaskType = Mask.MaskType.DateTimeAdvancingCaret
                                                x.Properties.Mask.UseMaskAsDisplayFormat = True
                                            ElseIf TypeOf ctlbx Is DXSample.CustomSearchLookUpEdit Then
                                                Dim x As DXSample.CustomSearchLookUpEdit = TryCast(ctlbx, DXSample.CustomSearchLookUpEdit)
                                                AddHandler x.UpdateDisplayFilter, AddressOf OnUpdateDisplayFilter
                                                AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                                                x.Properties.AllowMouseWheel = False
                                                x.Properties.UseCtrlScroll = True
                                                If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                                                    x.Properties.PopupFilterMode = PopupFilterMode.Default
                                                Else
                                                    x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                                                    x.Properties.View.ActiveFilterEnabled = False
                                                    x.Properties.View.ActiveFilterString = ""
                                                End If
                                            ElseIf TypeOf ctlbx Is DevExpress.XtraEditors.TextEdit Then
                                                Dim x As TextEdit
                                                x = TryCast(ctlbx, TextEdit)
                                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                                        x.Properties.DisplayFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.EditFormat.FormatString = "" Then
                                                        x.Properties.EditFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                    If x.Properties.Mask.EditMask = "" Then
                                                        x.Properties.Mask.EditMask = "n2"
                                                    End If
                                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                    AddHandler x.GotFocus, AddressOf txt_GotFocus
                                                    AddHandler x.Click, AddressOf txt_GotFocus
                                                End If
                                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                                            ElseIf TypeOf ctlbx Is DevExpress.XtraEditors.ButtonEdit Then
                                                Dim x As ButtonEdit = TryCast(ctlbx, ButtonEdit)
                                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                                        x.Properties.DisplayFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.EditFormat.FormatString = "" Then
                                                        x.Properties.EditFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                    If x.Properties.Mask.EditMask = "" Then
                                                        x.Properties.Mask.EditMask = "n2"
                                                    End If
                                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                    AddHandler x.GotFocus, AddressOf button_GotFocus
                                                    AddHandler x.Click, AddressOf button_GotFocus
                                                End If
                                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                                            ElseIf TypeOf ctlbx Is DevExpress.XtraEditors.CalcEdit Then
                                                Dim x As CalcEdit = TryCast(ctlbx, CalcEdit)
                                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                                        x.Properties.DisplayFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.EditFormat.FormatString = "" Then
                                                        x.Properties.EditFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                    If x.Properties.Mask.EditMask = "" Then
                                                        x.Properties.Mask.EditMask = "n2"
                                                    End If
                                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                    AddHandler x.GotFocus, AddressOf clc_GotFocus
                                                    AddHandler x.Click, AddressOf clc_GotFocus
                                                End If
                                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                                            End If
                                        Next
                                    ElseIf TypeOf ctlax Is DevExpress.XtraEditors.DateEdit Then
                                        Dim x As DateEdit = TryCast(ctlax, DateEdit)
                                        x.EnterMoveNextControl = True
                                        x.Properties.DisplayFormat.FormatString = "dd-MM-yyyy"
                                        x.Properties.EditFormat.FormatString = "dd-MM-yyyy"
                                        x.Properties.Mask.EditMask = "dd-MM-yyyy"
                                        x.Properties.EditFormat.FormatType = FormatType.DateTime
                                        x.Properties.DisplayFormat.FormatType = FormatType.DateTime
                                        x.Properties.Mask.MaskType = Mask.MaskType.DateTimeAdvancingCaret
                                        x.Properties.Mask.UseMaskAsDisplayFormat = True
                                    ElseIf TypeOf ctlax Is DXSample.CustomSearchLookUpEdit Then
                                        Dim x As DXSample.CustomSearchLookUpEdit
                                        x = TryCast(ctlax, DXSample.CustomSearchLookUpEdit)
                                        AddHandler x.UpdateDisplayFilter, AddressOf OnUpdateDisplayFilter
                                        AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                                        x.Properties.AllowMouseWheel = False
                                        x.Properties.UseCtrlScroll = True
                                        If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                                            x.Properties.PopupFilterMode = PopupFilterMode.Default
                                        Else
                                            x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                                            x.Properties.View.ActiveFilterEnabled = False
                                            x.Properties.View.ActiveFilterString = ""
                                        End If
                                    ElseIf TypeOf ctlax Is DevExpress.XtraEditors.TextEdit Then
                                        Dim x As TextEdit = TryCast(ctlax, TextEdit)
                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                x.Properties.DisplayFormat.FormatString = "n2"
                                            End If
                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                            If x.Properties.EditFormat.FormatString = "" Then
                                                x.Properties.EditFormat.FormatString = "n2"
                                            End If
                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                            If x.Properties.Mask.EditMask = "" Then
                                                x.Properties.Mask.EditMask = "n2"
                                            End If
                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                            AddHandler x.GotFocus, AddressOf txt_GotFocus
                                            AddHandler x.Click, AddressOf txt_GotFocus
                                        End If
                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                    ElseIf TypeOf ctlax Is DevExpress.XtraEditors.ButtonEdit Then
                                        Dim x As ButtonEdit = TryCast(ctlax, ButtonEdit)
                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                x.Properties.DisplayFormat.FormatString = "n2"
                                            End If
                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                            If x.Properties.EditFormat.FormatString = "" Then
                                                x.Properties.EditFormat.FormatString = "n2"
                                            End If
                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                            If x.Properties.Mask.EditMask = "" Then
                                                x.Properties.Mask.EditMask = "n2"
                                            End If
                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                            AddHandler x.GotFocus, AddressOf button_GotFocus
                                            AddHandler x.Click, AddressOf button_GotFocus
                                        End If
                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                    ElseIf TypeOf ctlax Is DevExpress.XtraEditors.CalcEdit Then
                                        Dim x As CalcEdit = TryCast(ctlax, CalcEdit)
                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                x.Properties.DisplayFormat.FormatString = "n2"
                                            End If
                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                            If x.Properties.EditFormat.FormatString = "" Then
                                                x.Properties.EditFormat.FormatString = "n2"
                                            End If
                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                            If x.Properties.Mask.EditMask = "" Then
                                                x.Properties.Mask.EditMask = "n2"
                                            End If
                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                            AddHandler x.GotFocus, AddressOf clc_GotFocus
                                            AddHandler x.Click, AddressOf clc_GotFocus
                                        End If
                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                    End If
                                Next
                            Next
                        End If
                    Next
                ElseIf TypeOf ctrl Is DevExpress.XtraEditors.SimpleButton Then
                    Dim btn As DevExpress.XtraEditors.SimpleButton = TryCast(ctrl, DevExpress.XtraEditors.SimpleButton)
                    btn.Appearance.Options.UseBackColor = False
                    btn.Appearance.Options.UseBorderColor = False
                    btn.Appearance.Options.UseFont = True
                    btn.Appearance.Options.UseForeColor = False
                    btn.LookAndFeel.UseDefaultLookAndFeel = True
                    AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
                    AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
                ElseIf TypeOf ctrl Is DevExpress.XtraGrid.GridControl Then
                    Dim gc1 As DevExpress.XtraGrid.GridControl = TryCast(ctrl, DevExpress.XtraGrid.GridControl)
                    Dim gv1 As DevExpress.XtraGrid.Views.Grid.GridView = gc1.DefaultView
                    gv1.OptionsBehavior.AllowIncrementalSearch = True
                    AddHandler gv1.ShowCustomizationForm, AddressOf GV1_ShowCustomizationForm
                    gv1.OptionsMenu.ShowGroupSummaryEditorItem = True
                    Dim TempMyFindPanelFilterHelper As New MyFindPanelFilterHelper(gv1)
                    AddHandler gv1.CustomDrawCell, AddressOf GV1_CustomDrawCell
                ElseIf TypeOf ctrl Is DevExpress.XtraEditors.XtraPanel Then
                    Dim pnl As DevExpress.XtraEditors.XtraPanel = TryCast(ctrl, DevExpress.XtraEditors.XtraPanel)
                    For Each ctla In pnl.Controls
                        If TypeOf ctla Is DevExpress.XtraLayout.LayoutControl Then
                            Dim ly As DevExpress.XtraLayout.LayoutControl = TryCast(ctla, DevExpress.XtraLayout.LayoutControl)
                            If IsEditLayout Then
                                ly.AllowCustomization = True
                            Else
                                ly.AllowCustomization = False
                            End If
                            ly.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignMode.AlignInGroups
                            For Each ctl In ly.Controls
                                If TypeOf ctl Is DevExpress.XtraEditors.SimpleButton Then
                                    Dim btn As DevExpress.XtraEditors.SimpleButton = TryCast(ctl, DevExpress.XtraEditors.SimpleButton)
                                    btn.Appearance.Options.UseBackColor = False
                                    btn.Appearance.Options.UseBorderColor = False
                                    btn.Appearance.Options.UseFont = True
                                    btn.Appearance.Options.UseForeColor = False
                                    btn.LookAndFeel.UseDefaultLookAndFeel = True
                                    AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
                                    AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
                                ElseIf TypeOf ctl Is DevExpress.XtraGrid.GridControl Then
                                    Dim gc1 As DevExpress.XtraGrid.GridControl = TryCast(ctl, DevExpress.XtraGrid.GridControl)
                                    Dim gv1 As DevExpress.XtraGrid.Views.Grid.GridView = gc1.DefaultView
                                    gv1.OptionsBehavior.AllowIncrementalSearch = True
                                    AddHandler gv1.ShowCustomizationForm, AddressOf GV1_ShowCustomizationForm
                                    gv1.OptionsMenu.ShowGroupSummaryEditorItem = True
                                    Dim TempMyFindPanelFilterHelper As New MyFindPanelFilterHelper(gv1)
                                    AddHandler gv1.CustomDrawCell, AddressOf GV1_CustomDrawCell
                                ElseIf TypeOf ctl Is DevExpress.XtraEditors.DateEdit Then
                                    Dim x As DateEdit = TryCast(ctl, DateEdit)
                                    x.EnterMoveNextControl = True
                                    x.Properties.DisplayFormat.FormatString = "dd-MM-yyyy"
                                    x.Properties.EditFormat.FormatString = "dd-MM-yyyy"
                                    x.Properties.Mask.EditMask = "dd-MM-yyyy"
                                    x.Properties.EditFormat.FormatType = FormatType.DateTime
                                    x.Properties.DisplayFormat.FormatType = FormatType.DateTime
                                    x.Properties.Mask.MaskType = Mask.MaskType.DateTimeAdvancingCaret
                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                ElseIf TypeOf ctl Is DXSample.CustomSearchLookUpEdit Then
                                    Dim x As DXSample.CustomSearchLookUpEdit = TryCast(ctl, DXSample.CustomSearchLookUpEdit)
                                    AddHandler x.UpdateDisplayFilter, AddressOf OnUpdateDisplayFilter
                                    AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                                    x.Properties.AllowMouseWheel = False
                                    x.Properties.UseCtrlScroll = True
                                    If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                                        x.Properties.PopupFilterMode = PopupFilterMode.Default
                                    Else
                                        x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                                        x.Properties.View.ActiveFilterEnabled = False
                                        x.Properties.View.ActiveFilterString = ""
                                    End If
                                ElseIf TypeOf ctl Is DevExpress.XtraEditors.TextEdit Then
                                    Dim x As TextEdit = TryCast(ctl, TextEdit)
                                    If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                        x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                        If x.Properties.DisplayFormat.FormatString = "" Then
                                            x.Properties.DisplayFormat.FormatString = "n2"
                                        End If
                                        x.Properties.EditFormat.FormatType = FormatType.Numeric
                                        If x.Properties.EditFormat.FormatString = "" Then
                                            x.Properties.EditFormat.FormatString = "n2"
                                        End If
                                        x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                        If x.Properties.Mask.EditMask = "" Then
                                            x.Properties.Mask.EditMask = "n2"
                                        End If
                                        x.Properties.Mask.UseMaskAsDisplayFormat = True
                                        AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                        AddHandler x.GotFocus, AddressOf txt_GotFocus
                                        AddHandler x.Click, AddressOf txt_GotFocus
                                    End If
                                    'x.Properties.CharacterCasing = CharacterCasing.Upper
                                ElseIf TypeOf ctl Is DevExpress.XtraEditors.ButtonEdit Then
                                    Dim x As ButtonEdit = TryCast(ctl, ButtonEdit)
                                    If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                        x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                        If x.Properties.DisplayFormat.FormatString = "" Then
                                            x.Properties.DisplayFormat.FormatString = "n2"
                                        End If
                                        x.Properties.EditFormat.FormatType = FormatType.Numeric
                                        If x.Properties.EditFormat.FormatString = "" Then
                                            x.Properties.EditFormat.FormatString = "n2"
                                        End If
                                        x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                        If x.Properties.Mask.EditMask = "" Then
                                            x.Properties.Mask.EditMask = "n2"
                                        End If
                                        x.Properties.Mask.UseMaskAsDisplayFormat = True
                                        AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                        AddHandler x.GotFocus, AddressOf button_GotFocus
                                        AddHandler x.Click, AddressOf button_GotFocus
                                    End If
                                    'x.Properties.CharacterCasing = CharacterCasing.Upper
                                ElseIf TypeOf ctl Is DevExpress.XtraEditors.CalcEdit Then
                                    Dim x As CalcEdit = TryCast(ctl, CalcEdit)
                                    If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                        x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                        If x.Properties.DisplayFormat.FormatString = "" Then
                                            x.Properties.DisplayFormat.FormatString = "n2"
                                        End If
                                        x.Properties.EditFormat.FormatType = FormatType.Numeric
                                        If x.Properties.EditFormat.FormatString = "" Then
                                            x.Properties.EditFormat.FormatString = "n2"
                                        End If
                                        x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                        If x.Properties.Mask.EditMask = "" Then
                                            x.Properties.Mask.EditMask = "n2"
                                        End If
                                        x.Properties.Mask.UseMaskAsDisplayFormat = True
                                        AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                        AddHandler x.GotFocus, AddressOf clc_GotFocus
                                        AddHandler x.Click, AddressOf clc_GotFocus
                                    End If
                                    'x.Properties.CharacterCasing = CharacterCasing.Upper
                                    'Tambahan Akbar, ada XtraTab di dalam LayoutControl
                                ElseIf TypeOf ctl Is DevExpress.XtraTab.XtraTabControl Then
                                    Dim tab As DevExpress.XtraTab.XtraTabControl = TryCast(ctl, DevExpress.XtraTab.XtraTabControl)
                                    For Each tb In tab.TabPages
                                        Dim TabPage As DevExpress.XtraTab.XtraTabPage = TryCast(tb, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctlax In TabPage.Controls
                                            If TypeOf ctlax Is DevExpress.XtraLayout.LayoutControl Then
                                                Dim lyx As DevExpress.XtraLayout.LayoutControl = TryCast(ctlax, DevExpress.XtraLayout.LayoutControl)
                                                If IsEditLayout Then
                                                    lyx.AllowCustomization = True
                                                Else
                                                    lyx.AllowCustomization = False
                                                End If
                                                lyx.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignMode.AlignInGroups
                                                For Each ctlxx In lyx.Controls
                                                    If TypeOf ctlxx Is DevExpress.XtraEditors.SimpleButton Then
                                                        Dim btn As DevExpress.XtraEditors.SimpleButton = TryCast(ctlxx, DevExpress.XtraEditors.SimpleButton)
                                                        btn.Appearance.Options.UseBackColor = False
                                                        btn.Appearance.Options.UseBorderColor = False
                                                        btn.Appearance.Options.UseFont = True
                                                        btn.Appearance.Options.UseForeColor = False
                                                        btn.LookAndFeel.UseDefaultLookAndFeel = True
                                                        AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
                                                        AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
                                                    ElseIf TypeOf ctlxx Is DevExpress.XtraGrid.GridControl Then
                                                        Dim gc1 As DevExpress.XtraGrid.GridControl = TryCast(ctlxx, DevExpress.XtraGrid.GridControl)
                                                        Dim gv1 As DevExpress.XtraGrid.Views.Grid.GridView = gc1.DefaultView
                                                        gv1.OptionsBehavior.AllowIncrementalSearch = True
                                                        AddHandler gv1.ShowCustomizationForm, AddressOf GV1_ShowCustomizationForm
                                                        gv1.OptionsMenu.ShowGroupSummaryEditorItem = True
                                                        Dim TempMyFindPanelFilterHelper As New MyFindPanelFilterHelper(gv1)
                                                        AddHandler gv1.CustomDrawCell, AddressOf GV1_CustomDrawCell
                                                    ElseIf TypeOf ctlxx Is DevExpress.XtraEditors.DateEdit Then
                                                        Dim x As DateEdit = TryCast(ctlxx, DateEdit)
                                                        x.EnterMoveNextControl = True
                                                        x.Properties.DisplayFormat.FormatString = "dd-MM-yyyy"
                                                        x.Properties.EditFormat.FormatString = "dd-MM-yyyy"
                                                        x.Properties.Mask.EditMask = "dd-MM-yyyy"
                                                        x.Properties.EditFormat.FormatType = FormatType.DateTime
                                                        x.Properties.DisplayFormat.FormatType = FormatType.DateTime
                                                        x.Properties.Mask.MaskType = Mask.MaskType.DateTimeAdvancingCaret
                                                        x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                    ElseIf TypeOf ctlxx Is DXSample.CustomSearchLookUpEdit Then
                                                        Dim x As DXSample.CustomSearchLookUpEdit = TryCast(ctlxx, DXSample.CustomSearchLookUpEdit)
                                                        AddHandler x.UpdateDisplayFilter, AddressOf OnUpdateDisplayFilter
                                                        AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                                                        x.Properties.AllowMouseWheel = False
                                                        x.Properties.UseCtrlScroll = True
                                                        If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                                                            x.Properties.PopupFilterMode = PopupFilterMode.Default
                                                        Else
                                                            x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                                                            x.Properties.View.ActiveFilterEnabled = False
                                                            x.Properties.View.ActiveFilterString = ""
                                                        End If
                                                    ElseIf TypeOf ctlxx Is DevExpress.XtraEditors.TextEdit Then
                                                        Dim x As TextEdit = TryCast(ctlxx, TextEdit)
                                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                                x.Properties.DisplayFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.EditFormat.FormatString = "" Then
                                                                x.Properties.EditFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                            If x.Properties.Mask.EditMask = "" Then
                                                                x.Properties.Mask.EditMask = "n2"
                                                            End If
                                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                            AddHandler x.GotFocus, AddressOf txt_GotFocus
                                                            AddHandler x.Click, AddressOf txt_GotFocus
                                                        End If
                                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                                    ElseIf TypeOf ctlxx Is DevExpress.XtraEditors.ButtonEdit Then
                                                        Dim x As ButtonEdit = TryCast(ctlxx, ButtonEdit)
                                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                                x.Properties.DisplayFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.EditFormat.FormatString = "" Then
                                                                x.Properties.EditFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                            If x.Properties.Mask.EditMask = "" Then
                                                                x.Properties.Mask.EditMask = "n2"
                                                            End If
                                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                            AddHandler x.GotFocus, AddressOf button_GotFocus
                                                            AddHandler x.Click, AddressOf button_GotFocus
                                                        End If
                                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                                    ElseIf TypeOf ctlxx Is DevExpress.XtraEditors.CalcEdit Then
                                                        Dim x As CalcEdit = TryCast(ctlxx, CalcEdit)
                                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                                x.Properties.DisplayFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.EditFormat.FormatString = "" Then
                                                                x.Properties.EditFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                            If x.Properties.Mask.EditMask = "" Then
                                                                x.Properties.Mask.EditMask = "n2"
                                                            End If
                                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                            AddHandler x.GotFocus, AddressOf clc_GotFocus
                                                            AddHandler x.Click, AddressOf clc_GotFocus
                                                        End If
                                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                                    End If
                                                Next
                                            ElseIf TypeOf ctlax Is DevExpress.XtraEditors.SimpleButton Then
                                                Dim btn As DevExpress.XtraEditors.SimpleButton = TryCast(ctlax, DevExpress.XtraEditors.SimpleButton)
                                                btn.Appearance.Options.UseBackColor = False
                                                btn.Appearance.Options.UseBorderColor = False
                                                btn.Appearance.Options.UseFont = True
                                                btn.Appearance.Options.UseForeColor = False
                                                btn.LookAndFeel.UseDefaultLookAndFeel = True
                                                AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
                                                AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
                                            ElseIf TypeOf ctlax Is DevExpress.XtraGrid.GridControl Then
                                                Dim gc1 As DevExpress.XtraGrid.GridControl = TryCast(ctlax, DevExpress.XtraGrid.GridControl)
                                                Dim gv1 As DevExpress.XtraGrid.Views.Grid.GridView = gc1.DefaultView
                                                gv1.OptionsBehavior.AllowIncrementalSearch = True
                                                AddHandler gv1.ShowCustomizationForm, AddressOf GV1_ShowCustomizationForm
                                                gv1.OptionsMenu.ShowGroupSummaryEditorItem = True
                                                Dim TempMyFindPanelFilterHelper As New MyFindPanelFilterHelper(gv1)
                                                AddHandler gv1.CustomDrawCell, AddressOf GV1_CustomDrawCell
                                            ElseIf TypeOf ctlax Is DevExpress.XtraEditors.XtraPanel Then
                                                Dim pnlx As DevExpress.XtraEditors.XtraPanel = TryCast(ctlax, DevExpress.XtraEditors.XtraPanel)
                                                For Each ctlbx In pnlx.Controls
                                                    If TypeOf ctlbx Is DevExpress.XtraLayout.LayoutControl Then
                                                        Dim lyx As DevExpress.XtraLayout.LayoutControl = TryCast(ctlbx, DevExpress.XtraLayout.LayoutControl)
                                                        If IsEditLayout Then
                                                            lyx.AllowCustomization = True
                                                        Else
                                                            lyx.AllowCustomization = False
                                                        End If
                                                        lyx.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignMode.AlignInGroups
                                                        For Each ctlxx In lyx.Controls
                                                            If TypeOf ctlxx Is DevExpress.XtraEditors.SimpleButton Then
                                                                Dim btn As DevExpress.XtraEditors.SimpleButton = TryCast(ctlxx, DevExpress.XtraEditors.SimpleButton)
                                                                btn.Appearance.Options.UseBackColor = False
                                                                btn.Appearance.Options.UseBorderColor = False
                                                                btn.Appearance.Options.UseFont = True
                                                                btn.Appearance.Options.UseForeColor = False
                                                                btn.LookAndFeel.UseDefaultLookAndFeel = True
                                                                AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
                                                                AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
                                                            ElseIf TypeOf ctlxx Is DevExpress.XtraGrid.GridControl Then
                                                                Dim gc1 As DevExpress.XtraGrid.GridControl = TryCast(ctlxx, DevExpress.XtraGrid.GridControl)
                                                                Dim gv1 As DevExpress.XtraGrid.Views.Grid.GridView = gc1.DefaultView
                                                                gv1.OptionsBehavior.AllowIncrementalSearch = True
                                                                AddHandler gv1.ShowCustomizationForm, AddressOf GV1_ShowCustomizationForm
                                                                gv1.OptionsMenu.ShowGroupSummaryEditorItem = True
                                                                Dim TempMyFindPanelFilterHelper As New MyFindPanelFilterHelper(gv1)
                                                                AddHandler gv1.CustomDrawCell, AddressOf GV1_CustomDrawCell
                                                            ElseIf TypeOf ctlxx Is DevExpress.XtraEditors.DateEdit Then
                                                                Dim x As DateEdit = TryCast(ctlxx, DateEdit)
                                                                x.EnterMoveNextControl = True
                                                                x.Properties.DisplayFormat.FormatString = "dd-MM-yyyy"
                                                                x.Properties.EditFormat.FormatString = "dd-MM-yyyy"
                                                                x.Properties.Mask.EditMask = "dd-MM-yyyy"
                                                                x.Properties.EditFormat.FormatType = FormatType.DateTime
                                                                x.Properties.DisplayFormat.FormatType = FormatType.DateTime
                                                                x.Properties.Mask.MaskType = Mask.MaskType.DateTimeAdvancingCaret
                                                                x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                            ElseIf TypeOf ctlxx Is DXSample.CustomSearchLookUpEdit Then
                                                                Dim x As DXSample.CustomSearchLookUpEdit = TryCast(ctlxx, DXSample.CustomSearchLookUpEdit)
                                                                AddHandler x.UpdateDisplayFilter, AddressOf OnUpdateDisplayFilter
                                                                AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                                                                x.Properties.AllowMouseWheel = False
                                                                x.Properties.UseCtrlScroll = True
                                                                If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                                                                    x.Properties.PopupFilterMode = PopupFilterMode.Default
                                                                Else
                                                                    x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                                                                    x.Properties.View.ActiveFilterEnabled = False
                                                                    x.Properties.View.ActiveFilterString = ""
                                                                End If
                                                            ElseIf TypeOf ctlxx Is DevExpress.XtraEditors.TextEdit Then
                                                                Dim x As TextEdit = TryCast(ctlxx, TextEdit)
                                                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                                                        x.Properties.DisplayFormat.FormatString = "n2"
                                                                    End If
                                                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                                    If x.Properties.EditFormat.FormatString = "" Then
                                                                        x.Properties.EditFormat.FormatString = "n2"
                                                                    End If
                                                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                                    If x.Properties.Mask.EditMask = "" Then
                                                                        x.Properties.Mask.EditMask = "n2"
                                                                    End If
                                                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                                    AddHandler x.GotFocus, AddressOf txt_GotFocus
                                                                    AddHandler x.Click, AddressOf txt_GotFocus
                                                                End If
                                                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                                                            ElseIf TypeOf ctlxx Is DevExpress.XtraEditors.ButtonEdit Then
                                                                Dim x As ButtonEdit = TryCast(ctlxx, ButtonEdit)
                                                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                                                        x.Properties.DisplayFormat.FormatString = "n2"
                                                                    End If
                                                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                                    If x.Properties.EditFormat.FormatString = "" Then
                                                                        x.Properties.EditFormat.FormatString = "n2"
                                                                    End If
                                                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                                    If x.Properties.Mask.EditMask = "" Then
                                                                        x.Properties.Mask.EditMask = "n2"
                                                                    End If
                                                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                                    AddHandler x.GotFocus, AddressOf button_GotFocus
                                                                    AddHandler x.Click, AddressOf button_GotFocus
                                                                End If
                                                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                                                            ElseIf TypeOf ctlxx Is DevExpress.XtraEditors.CalcEdit Then
                                                                Dim x As CalcEdit = TryCast(ctlxx, CalcEdit)
                                                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                                                        x.Properties.DisplayFormat.FormatString = "n2"
                                                                    End If
                                                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                                    If x.Properties.EditFormat.FormatString = "" Then
                                                                        x.Properties.EditFormat.FormatString = "n2"
                                                                    End If
                                                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                                    If x.Properties.Mask.EditMask = "" Then
                                                                        x.Properties.Mask.EditMask = "n2"
                                                                    End If
                                                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                                    AddHandler x.GotFocus, AddressOf clc_GotFocus
                                                                    AddHandler x.Click, AddressOf clc_GotFocus
                                                                End If
                                                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                                                            End If
                                                        Next
                                                    ElseIf TypeOf ctlbx Is DevExpress.XtraEditors.SimpleButton Then
                                                        Dim btn As DevExpress.XtraEditors.SimpleButton = TryCast(ctlbx, DevExpress.XtraEditors.SimpleButton)
                                                        btn.Appearance.Options.UseBackColor = False
                                                        btn.Appearance.Options.UseBorderColor = False
                                                        btn.Appearance.Options.UseFont = True
                                                        btn.Appearance.Options.UseForeColor = False
                                                        btn.LookAndFeel.UseDefaultLookAndFeel = True
                                                        AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
                                                        AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
                                                    ElseIf TypeOf ctlbx Is DevExpress.XtraGrid.GridControl Then
                                                        Dim gc1 As DevExpress.XtraGrid.GridControl = TryCast(ctlbx, DevExpress.XtraGrid.GridControl)
                                                        Dim gv1 As DevExpress.XtraGrid.Views.Grid.GridView = gc1.DefaultView
                                                        gv1.OptionsBehavior.AllowIncrementalSearch = True
                                                        AddHandler gv1.ShowCustomizationForm, AddressOf GV1_ShowCustomizationForm
                                                        gv1.OptionsMenu.ShowGroupSummaryEditorItem = True
                                                        Dim TempMyFindPanelFilterHelper As New MyFindPanelFilterHelper(gv1)
                                                        AddHandler gv1.CustomDrawCell, AddressOf GV1_CustomDrawCell
                                                    ElseIf TypeOf ctlbx Is DevExpress.XtraEditors.DateEdit Then
                                                        Dim x As DateEdit = TryCast(ctlbx, DateEdit)
                                                        x.EnterMoveNextControl = True
                                                        x.Properties.DisplayFormat.FormatString = "dd-MM-yyyy"
                                                        x.Properties.EditFormat.FormatString = "dd-MM-yyyy"
                                                        x.Properties.Mask.EditMask = "dd-MM-yyyy"
                                                        x.Properties.EditFormat.FormatType = FormatType.DateTime
                                                        x.Properties.DisplayFormat.FormatType = FormatType.DateTime
                                                        x.Properties.Mask.MaskType = Mask.MaskType.DateTimeAdvancingCaret
                                                        x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                    ElseIf TypeOf ctlbx Is DXSample.CustomSearchLookUpEdit Then
                                                        Dim x As DXSample.CustomSearchLookUpEdit = TryCast(ctlbx, DXSample.CustomSearchLookUpEdit)
                                                        AddHandler x.UpdateDisplayFilter, AddressOf OnUpdateDisplayFilter
                                                        AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                                                        x.Properties.AllowMouseWheel = False
                                                        x.Properties.UseCtrlScroll = True
                                                        If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                                                            x.Properties.PopupFilterMode = PopupFilterMode.Default
                                                        Else
                                                            x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                                                            x.Properties.View.ActiveFilterEnabled = False
                                                            x.Properties.View.ActiveFilterString = ""
                                                        End If
                                                    ElseIf TypeOf ctlbx Is DevExpress.XtraEditors.TextEdit Then
                                                        Dim x As TextEdit
                                                        x = TryCast(ctlbx, TextEdit)
                                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                                x.Properties.DisplayFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.EditFormat.FormatString = "" Then
                                                                x.Properties.EditFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                            If x.Properties.Mask.EditMask = "" Then
                                                                x.Properties.Mask.EditMask = "n2"
                                                            End If
                                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                            AddHandler x.GotFocus, AddressOf txt_GotFocus
                                                            AddHandler x.Click, AddressOf txt_GotFocus
                                                        End If
                                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                                    ElseIf TypeOf ctlbx Is DevExpress.XtraEditors.ButtonEdit Then
                                                        Dim x As ButtonEdit = TryCast(ctlbx, ButtonEdit)
                                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                                x.Properties.DisplayFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.EditFormat.FormatString = "" Then
                                                                x.Properties.EditFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                            If x.Properties.Mask.EditMask = "" Then
                                                                x.Properties.Mask.EditMask = "n2"
                                                            End If
                                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                            AddHandler x.GotFocus, AddressOf button_GotFocus
                                                            AddHandler x.Click, AddressOf button_GotFocus
                                                        End If
                                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                                    ElseIf TypeOf ctlbx Is DevExpress.XtraEditors.CalcEdit Then
                                                        Dim x As CalcEdit = TryCast(ctlbx, CalcEdit)
                                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                                x.Properties.DisplayFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                            If x.Properties.EditFormat.FormatString = "" Then
                                                                x.Properties.EditFormat.FormatString = "n2"
                                                            End If
                                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                            If x.Properties.Mask.EditMask = "" Then
                                                                x.Properties.Mask.EditMask = "n2"
                                                            End If
                                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                            AddHandler x.GotFocus, AddressOf clc_GotFocus
                                                            AddHandler x.Click, AddressOf clc_GotFocus
                                                        End If
                                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                                    End If
                                                Next
                                            ElseIf TypeOf ctlax Is DevExpress.XtraEditors.DateEdit Then
                                                Dim x As DateEdit = TryCast(ctlax, DateEdit)
                                                x.EnterMoveNextControl = True
                                                x.Properties.DisplayFormat.FormatString = "dd-MM-yyyy"
                                                x.Properties.EditFormat.FormatString = "dd-MM-yyyy"
                                                x.Properties.Mask.EditMask = "dd-MM-yyyy"
                                                x.Properties.EditFormat.FormatType = FormatType.DateTime
                                                x.Properties.DisplayFormat.FormatType = FormatType.DateTime
                                                x.Properties.Mask.MaskType = Mask.MaskType.DateTimeAdvancingCaret
                                                x.Properties.Mask.UseMaskAsDisplayFormat = True
                                            ElseIf TypeOf ctlax Is DXSample.CustomSearchLookUpEdit Then
                                                Dim x As DXSample.CustomSearchLookUpEdit
                                                x = TryCast(ctlax, DXSample.CustomSearchLookUpEdit)
                                                AddHandler x.UpdateDisplayFilter, AddressOf OnUpdateDisplayFilter
                                                AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                                                x.Properties.AllowMouseWheel = False
                                                x.Properties.UseCtrlScroll = True
                                                If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                                                    x.Properties.PopupFilterMode = PopupFilterMode.Default
                                                Else
                                                    x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                                                    x.Properties.View.ActiveFilterEnabled = False
                                                    x.Properties.View.ActiveFilterString = ""
                                                End If
                                            ElseIf TypeOf ctlax Is DevExpress.XtraEditors.TextEdit Then
                                                Dim x As TextEdit = TryCast(ctlax, TextEdit)
                                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                                        x.Properties.DisplayFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.EditFormat.FormatString = "" Then
                                                        x.Properties.EditFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                    If x.Properties.Mask.EditMask = "" Then
                                                        x.Properties.Mask.EditMask = "n2"
                                                    End If
                                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                    AddHandler x.GotFocus, AddressOf txt_GotFocus
                                                    AddHandler x.Click, AddressOf txt_GotFocus
                                                End If
                                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                                            ElseIf TypeOf ctlax Is DevExpress.XtraEditors.ButtonEdit Then
                                                Dim x As ButtonEdit = TryCast(ctlax, ButtonEdit)
                                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                                        x.Properties.DisplayFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.EditFormat.FormatString = "" Then
                                                        x.Properties.EditFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                    If x.Properties.Mask.EditMask = "" Then
                                                        x.Properties.Mask.EditMask = "n2"
                                                    End If
                                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                    AddHandler x.GotFocus, AddressOf button_GotFocus
                                                    AddHandler x.Click, AddressOf button_GotFocus
                                                End If
                                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                                            ElseIf TypeOf ctlax Is DevExpress.XtraEditors.CalcEdit Then
                                                Dim x As CalcEdit = TryCast(ctlax, CalcEdit)
                                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                                        x.Properties.DisplayFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.EditFormat.FormatString = "" Then
                                                        x.Properties.EditFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                    If x.Properties.Mask.EditMask = "" Then
                                                        x.Properties.Mask.EditMask = "n2"
                                                    End If
                                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                    AddHandler x.GotFocus, AddressOf clc_GotFocus
                                                    AddHandler x.Click, AddressOf clc_GotFocus
                                                End If
                                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                                            End If
                                        Next
                                    Next
                                End If
                            Next
                        ElseIf TypeOf ctla Is DevExpress.XtraEditors.SimpleButton Then
                            Dim btn As DevExpress.XtraEditors.SimpleButton = TryCast(ctla, DevExpress.XtraEditors.SimpleButton)
                            btn.Appearance.Options.UseBackColor = False
                            btn.Appearance.Options.UseBorderColor = False
                            btn.Appearance.Options.UseFont = True
                            btn.Appearance.Options.UseForeColor = False
                            btn.LookAndFeel.UseDefaultLookAndFeel = True
                            AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
                            AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
                        ElseIf TypeOf ctla Is DevExpress.XtraGrid.GridControl Then
                            Dim gc1 As DevExpress.XtraGrid.GridControl = TryCast(ctla, DevExpress.XtraGrid.GridControl)
                            Dim gv1 As DevExpress.XtraGrid.Views.Grid.GridView = gc1.DefaultView
                            gv1.OptionsBehavior.AllowIncrementalSearch = True
                            AddHandler gv1.ShowCustomizationForm, AddressOf GV1_ShowCustomizationForm
                            gv1.OptionsMenu.ShowGroupSummaryEditorItem = True
                            Dim TempMyFindPanelFilterHelper As New MyFindPanelFilterHelper(gv1)
                            AddHandler gv1.CustomDrawCell, AddressOf GV1_CustomDrawCell
                        ElseIf TypeOf ctla Is DevExpress.XtraEditors.DateEdit Then
                            Dim x As DateEdit = TryCast(ctla, DateEdit)
                            x.EnterMoveNextControl = True
                            x.Properties.DisplayFormat.FormatString = "dd-MM-yyyy"
                            x.Properties.EditFormat.FormatString = "dd-MM-yyyy"
                            x.Properties.Mask.EditMask = "dd-MM-yyyy"
                            x.Properties.EditFormat.FormatType = FormatType.DateTime
                            x.Properties.DisplayFormat.FormatType = FormatType.DateTime
                            x.Properties.Mask.MaskType = Mask.MaskType.DateTimeAdvancingCaret
                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                        ElseIf TypeOf ctla Is DXSample.CustomSearchLookUpEdit Then
                            Dim x As DXSample.CustomSearchLookUpEdit = TryCast(ctla, DXSample.CustomSearchLookUpEdit)
                            AddHandler x.UpdateDisplayFilter, AddressOf OnUpdateDisplayFilter
                            AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                            x.Properties.AllowMouseWheel = False
                            x.Properties.UseCtrlScroll = True
                            If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                                x.Properties.PopupFilterMode = PopupFilterMode.Default
                            Else
                                x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                                x.Properties.View.ActiveFilterEnabled = False
                                x.Properties.View.ActiveFilterString = ""
                            End If
                        ElseIf TypeOf ctla Is DevExpress.XtraEditors.TextEdit Then
                            Dim x As TextEdit = TryCast(ctla, TextEdit)
                            If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                If x.Properties.DisplayFormat.FormatString = "" Then
                                    x.Properties.DisplayFormat.FormatString = "n2"
                                End If
                                x.Properties.EditFormat.FormatType = FormatType.Numeric
                                If x.Properties.EditFormat.FormatString = "" Then
                                    x.Properties.EditFormat.FormatString = "n2"
                                End If
                                x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                If x.Properties.Mask.EditMask = "" Then
                                    x.Properties.Mask.EditMask = "n2"
                                End If
                                x.Properties.Mask.UseMaskAsDisplayFormat = True
                                AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                AddHandler x.GotFocus, AddressOf txt_GotFocus
                                AddHandler x.Click, AddressOf txt_GotFocus
                            End If
                            'x.Properties.CharacterCasing = CharacterCasing.Upper
                        ElseIf TypeOf ctla Is DevExpress.XtraEditors.ButtonEdit Then
                            Dim x As ButtonEdit = TryCast(ctla, ButtonEdit)
                            If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                If x.Properties.DisplayFormat.FormatString = "" Then
                                    x.Properties.DisplayFormat.FormatString = "n2"
                                End If
                                x.Properties.EditFormat.FormatType = FormatType.Numeric
                                If x.Properties.EditFormat.FormatString = "" Then
                                    x.Properties.EditFormat.FormatString = "n2"
                                End If
                                x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                If x.Properties.Mask.EditMask = "" Then
                                    x.Properties.Mask.EditMask = "n2"
                                End If
                                x.Properties.Mask.UseMaskAsDisplayFormat = True
                                AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                AddHandler x.GotFocus, AddressOf button_GotFocus
                                AddHandler x.Click, AddressOf button_GotFocus
                            End If
                            'x.Properties.CharacterCasing = CharacterCasing.Upper
                        ElseIf TypeOf ctla Is DevExpress.XtraEditors.CalcEdit Then
                            Dim x As CalcEdit = TryCast(ctla, CalcEdit)
                            If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                If x.Properties.DisplayFormat.FormatString = "" Then
                                    x.Properties.DisplayFormat.FormatString = "n2"
                                End If
                                x.Properties.EditFormat.FormatType = FormatType.Numeric
                                If x.Properties.EditFormat.FormatString = "" Then
                                    x.Properties.EditFormat.FormatString = "n2"
                                End If
                                x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                If x.Properties.Mask.EditMask = "" Then
                                    x.Properties.Mask.EditMask = "n2"
                                End If
                                x.Properties.Mask.UseMaskAsDisplayFormat = True
                                AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                AddHandler x.GotFocus, AddressOf clc_GotFocus
                                AddHandler x.Click, AddressOf clc_GotFocus
                            End If
                            'x.Properties.CharacterCasing = CharacterCasing.Upper
                        End If
                    Next
                ElseIf TypeOf ctrl Is DevExpress.XtraTab.XtraTabControl Then
                    Dim tab As DevExpress.XtraTab.XtraTabControl = TryCast(ctrl, DevExpress.XtraTab.XtraTabControl)
                    For Each tb In tab.TabPages
                        Dim TabPage As DevExpress.XtraTab.XtraTabPage = TryCast(tb, DevExpress.XtraTab.XtraTabPage)
                        For Each ctla In TabPage.Controls
                            If TypeOf ctla Is DevExpress.XtraLayout.LayoutControl Then
                                Dim ly As DevExpress.XtraLayout.LayoutControl = TryCast(ctla, DevExpress.XtraLayout.LayoutControl)
                                If IsEditLayout Then
                                    ly.AllowCustomization = True
                                Else
                                    ly.AllowCustomization = False
                                End If
                                ly.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignMode.AlignInGroups
                                For Each ctl In ly.Controls
                                    If TypeOf ctl Is DevExpress.XtraEditors.SimpleButton Then
                                        Dim btn As DevExpress.XtraEditors.SimpleButton = TryCast(ctl, DevExpress.XtraEditors.SimpleButton)
                                        btn.Appearance.Options.UseBackColor = False
                                        btn.Appearance.Options.UseBorderColor = False
                                        btn.Appearance.Options.UseFont = True
                                        btn.Appearance.Options.UseForeColor = False
                                        btn.LookAndFeel.UseDefaultLookAndFeel = True
                                        AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
                                        AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
                                    ElseIf TypeOf ctl Is DevExpress.XtraGrid.GridControl Then
                                        Dim gc1 As DevExpress.XtraGrid.GridControl = TryCast(ctl, DevExpress.XtraGrid.GridControl)
                                        Dim gv1 As DevExpress.XtraGrid.Views.Grid.GridView = gc1.DefaultView
                                        gv1.OptionsBehavior.AllowIncrementalSearch = True
                                        AddHandler gv1.ShowCustomizationForm, AddressOf GV1_ShowCustomizationForm
                                        gv1.OptionsMenu.ShowGroupSummaryEditorItem = True
                                        Dim TempMyFindPanelFilterHelper As New MyFindPanelFilterHelper(gv1)
                                        AddHandler gv1.CustomDrawCell, AddressOf GV1_CustomDrawCell
                                    ElseIf TypeOf ctl Is DevExpress.XtraEditors.DateEdit Then
                                        Dim x As DateEdit = TryCast(ctl, DateEdit)
                                        x.EnterMoveNextControl = True
                                        x.Properties.DisplayFormat.FormatString = "dd-MM-yyyy"
                                        x.Properties.EditFormat.FormatString = "dd-MM-yyyy"
                                        x.Properties.Mask.EditMask = "dd-MM-yyyy"
                                        x.Properties.EditFormat.FormatType = FormatType.DateTime
                                        x.Properties.DisplayFormat.FormatType = FormatType.DateTime
                                        x.Properties.Mask.MaskType = Mask.MaskType.DateTimeAdvancingCaret
                                        x.Properties.Mask.UseMaskAsDisplayFormat = True
                                    ElseIf TypeOf ctl Is DXSample.CustomSearchLookUpEdit Then
                                        Dim x As DXSample.CustomSearchLookUpEdit = TryCast(ctl, DXSample.CustomSearchLookUpEdit)
                                        AddHandler x.UpdateDisplayFilter, AddressOf OnUpdateDisplayFilter
                                        AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                                        x.Properties.AllowMouseWheel = False
                                        x.Properties.UseCtrlScroll = True
                                        If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                                            x.Properties.PopupFilterMode = PopupFilterMode.Default
                                        Else
                                            x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                                            x.Properties.View.ActiveFilterEnabled = False
                                            x.Properties.View.ActiveFilterString = ""
                                        End If
                                    ElseIf TypeOf ctl Is DevExpress.XtraEditors.TextEdit Then
                                        Dim x As TextEdit = TryCast(ctl, TextEdit)
                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                x.Properties.DisplayFormat.FormatString = "n2"
                                            End If
                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                            If x.Properties.EditFormat.FormatString = "" Then
                                                x.Properties.EditFormat.FormatString = "n2"
                                            End If
                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                            If x.Properties.Mask.EditMask = "" Then
                                                x.Properties.Mask.EditMask = "n2"
                                            End If
                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                            AddHandler x.GotFocus, AddressOf txt_GotFocus
                                            AddHandler x.Click, AddressOf txt_GotFocus
                                        End If
                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                    ElseIf TypeOf ctl Is DevExpress.XtraEditors.ButtonEdit Then
                                        Dim x As ButtonEdit = TryCast(ctl, ButtonEdit)
                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                x.Properties.DisplayFormat.FormatString = "n2"
                                            End If
                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                            If x.Properties.EditFormat.FormatString = "" Then
                                                x.Properties.EditFormat.FormatString = "n2"
                                            End If
                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                            If x.Properties.Mask.EditMask = "" Then
                                                x.Properties.Mask.EditMask = "n2"
                                            End If
                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                            AddHandler x.GotFocus, AddressOf button_GotFocus
                                            AddHandler x.Click, AddressOf button_GotFocus
                                        End If
                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                    ElseIf TypeOf ctl Is DevExpress.XtraEditors.CalcEdit Then
                                        Dim x As CalcEdit = TryCast(ctl, CalcEdit)
                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                x.Properties.DisplayFormat.FormatString = "n2"
                                            End If
                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                            If x.Properties.EditFormat.FormatString = "" Then
                                                x.Properties.EditFormat.FormatString = "n2"
                                            End If
                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                            If x.Properties.Mask.EditMask = "" Then
                                                x.Properties.Mask.EditMask = "n2"
                                            End If
                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                            AddHandler x.GotFocus, AddressOf clc_GotFocus
                                            AddHandler x.Click, AddressOf clc_GotFocus
                                        End If
                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                    End If
                                Next
                            ElseIf TypeOf ctla Is DevExpress.XtraEditors.SimpleButton Then
                                Dim btn As DevExpress.XtraEditors.SimpleButton = TryCast(ctla, DevExpress.XtraEditors.SimpleButton)
                                btn.Appearance.Options.UseBackColor = False
                                btn.Appearance.Options.UseBorderColor = False
                                btn.Appearance.Options.UseFont = True
                                btn.Appearance.Options.UseForeColor = False
                                btn.LookAndFeel.UseDefaultLookAndFeel = True
                                AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
                                AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
                            ElseIf TypeOf ctla Is DevExpress.XtraGrid.GridControl Then
                                Dim gc1 As DevExpress.XtraGrid.GridControl = TryCast(ctla, DevExpress.XtraGrid.GridControl)
                                Dim gv1 As DevExpress.XtraGrid.Views.Grid.GridView = gc1.DefaultView
                                gv1.OptionsBehavior.AllowIncrementalSearch = True
                                AddHandler gv1.ShowCustomizationForm, AddressOf GV1_ShowCustomizationForm
                                gv1.OptionsMenu.ShowGroupSummaryEditorItem = True
                                Dim TempMyFindPanelFilterHelper As New MyFindPanelFilterHelper(gv1)
                                AddHandler gv1.CustomDrawCell, AddressOf GV1_CustomDrawCell
                            ElseIf TypeOf ctla Is DevExpress.XtraEditors.XtraPanel Then
                                Dim pnl As DevExpress.XtraEditors.XtraPanel = TryCast(ctla, DevExpress.XtraEditors.XtraPanel)
                                For Each ctlb In pnl.Controls
                                    If TypeOf ctlb Is DevExpress.XtraLayout.LayoutControl Then
                                        Dim ly As DevExpress.XtraLayout.LayoutControl = TryCast(ctlb, DevExpress.XtraLayout.LayoutControl)
                                        If IsEditLayout Then
                                            ly.AllowCustomization = True
                                        Else
                                            ly.AllowCustomization = False
                                        End If
                                        ly.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignMode.AlignInGroups
                                        For Each ctl In ly.Controls
                                            If TypeOf ctl Is DevExpress.XtraEditors.SimpleButton Then
                                                Dim btn As DevExpress.XtraEditors.SimpleButton = TryCast(ctl, DevExpress.XtraEditors.SimpleButton)
                                                btn.Appearance.Options.UseBackColor = False
                                                btn.Appearance.Options.UseBorderColor = False
                                                btn.Appearance.Options.UseFont = True
                                                btn.Appearance.Options.UseForeColor = False
                                                btn.LookAndFeel.UseDefaultLookAndFeel = True
                                                AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
                                                AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
                                            ElseIf TypeOf ctl Is DevExpress.XtraGrid.GridControl Then
                                                Dim gc1 As DevExpress.XtraGrid.GridControl = TryCast(ctl, DevExpress.XtraGrid.GridControl)
                                                Dim gv1 As DevExpress.XtraGrid.Views.Grid.GridView = gc1.DefaultView
                                                gv1.OptionsBehavior.AllowIncrementalSearch = True
                                                AddHandler gv1.ShowCustomizationForm, AddressOf GV1_ShowCustomizationForm
                                                gv1.OptionsMenu.ShowGroupSummaryEditorItem = True
                                                Dim TempMyFindPanelFilterHelper As New MyFindPanelFilterHelper(gv1)
                                                AddHandler gv1.CustomDrawCell, AddressOf GV1_CustomDrawCell
                                            ElseIf TypeOf ctl Is DevExpress.XtraEditors.DateEdit Then
                                                Dim x As DateEdit = TryCast(ctl, DateEdit)
                                                x.EnterMoveNextControl = True
                                                x.Properties.DisplayFormat.FormatString = "dd-MM-yyyy"
                                                x.Properties.EditFormat.FormatString = "dd-MM-yyyy"
                                                x.Properties.Mask.EditMask = "dd-MM-yyyy"
                                                x.Properties.EditFormat.FormatType = FormatType.DateTime
                                                x.Properties.DisplayFormat.FormatType = FormatType.DateTime
                                                x.Properties.Mask.MaskType = Mask.MaskType.DateTimeAdvancingCaret
                                                x.Properties.Mask.UseMaskAsDisplayFormat = True
                                            ElseIf TypeOf ctl Is DXSample.CustomSearchLookUpEdit Then
                                                Dim x As DXSample.CustomSearchLookUpEdit = TryCast(ctl, DXSample.CustomSearchLookUpEdit)
                                                AddHandler x.UpdateDisplayFilter, AddressOf OnUpdateDisplayFilter
                                                AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                                                x.Properties.AllowMouseWheel = False
                                                x.Properties.UseCtrlScroll = True
                                                If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                                                    x.Properties.PopupFilterMode = PopupFilterMode.Default
                                                Else
                                                    x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                                                    x.Properties.View.ActiveFilterEnabled = False
                                                    x.Properties.View.ActiveFilterString = ""
                                                End If
                                            ElseIf TypeOf ctl Is DevExpress.XtraEditors.TextEdit Then
                                                Dim x As TextEdit = TryCast(ctl, TextEdit)
                                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                                        x.Properties.DisplayFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.EditFormat.FormatString = "" Then
                                                        x.Properties.EditFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                    If x.Properties.Mask.EditMask = "" Then
                                                        x.Properties.Mask.EditMask = "n2"
                                                    End If
                                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                    AddHandler x.GotFocus, AddressOf txt_GotFocus
                                                    AddHandler x.Click, AddressOf txt_GotFocus
                                                End If
                                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                                            ElseIf TypeOf ctl Is DevExpress.XtraEditors.ButtonEdit Then
                                                Dim x As ButtonEdit = TryCast(ctl, ButtonEdit)
                                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                                        x.Properties.DisplayFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.EditFormat.FormatString = "" Then
                                                        x.Properties.EditFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                    If x.Properties.Mask.EditMask = "" Then
                                                        x.Properties.Mask.EditMask = "n2"
                                                    End If
                                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                    AddHandler x.GotFocus, AddressOf button_GotFocus
                                                    AddHandler x.Click, AddressOf button_GotFocus
                                                End If
                                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                                            ElseIf TypeOf ctl Is DevExpress.XtraEditors.CalcEdit Then
                                                Dim x As CalcEdit = TryCast(ctl, CalcEdit)
                                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                                        x.Properties.DisplayFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                                    If x.Properties.EditFormat.FormatString = "" Then
                                                        x.Properties.EditFormat.FormatString = "n2"
                                                    End If
                                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                                    If x.Properties.Mask.EditMask = "" Then
                                                        x.Properties.Mask.EditMask = "n2"
                                                    End If
                                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                                    AddHandler x.GotFocus, AddressOf clc_GotFocus
                                                    AddHandler x.Click, AddressOf clc_GotFocus
                                                End If
                                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                                            End If
                                        Next
                                    ElseIf TypeOf ctlb Is DevExpress.XtraEditors.SimpleButton Then
                                        Dim btn As DevExpress.XtraEditors.SimpleButton = TryCast(ctlb, DevExpress.XtraEditors.SimpleButton)
                                        btn.Appearance.Options.UseBackColor = False
                                        btn.Appearance.Options.UseBorderColor = False
                                        btn.Appearance.Options.UseFont = True
                                        btn.Appearance.Options.UseForeColor = False
                                        btn.LookAndFeel.UseDefaultLookAndFeel = True
                                        AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
                                        AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
                                    ElseIf TypeOf ctlb Is DevExpress.XtraGrid.GridControl Then
                                        Dim gc1 As DevExpress.XtraGrid.GridControl = TryCast(ctlb, DevExpress.XtraGrid.GridControl)
                                        Dim gv1 As DevExpress.XtraGrid.Views.Grid.GridView = gc1.DefaultView
                                        gv1.OptionsBehavior.AllowIncrementalSearch = True
                                        AddHandler gv1.ShowCustomizationForm, AddressOf GV1_ShowCustomizationForm
                                        gv1.OptionsMenu.ShowGroupSummaryEditorItem = True
                                        Dim TempMyFindPanelFilterHelper As New MyFindPanelFilterHelper(gv1)
                                        AddHandler gv1.CustomDrawCell, AddressOf GV1_CustomDrawCell
                                    ElseIf TypeOf ctlb Is DevExpress.XtraEditors.DateEdit Then
                                        Dim x As DateEdit = TryCast(ctlb, DateEdit)
                                        x.EnterMoveNextControl = True
                                        x.Properties.DisplayFormat.FormatString = "dd-MM-yyyy"
                                        x.Properties.EditFormat.FormatString = "dd-MM-yyyy"
                                        x.Properties.Mask.EditMask = "dd-MM-yyyy"
                                        x.Properties.EditFormat.FormatType = FormatType.DateTime
                                        x.Properties.DisplayFormat.FormatType = FormatType.DateTime
                                        x.Properties.Mask.MaskType = Mask.MaskType.DateTimeAdvancingCaret
                                        x.Properties.Mask.UseMaskAsDisplayFormat = True
                                    ElseIf TypeOf ctlb Is DXSample.CustomSearchLookUpEdit Then
                                        Dim x As DXSample.CustomSearchLookUpEdit = TryCast(ctlb, DXSample.CustomSearchLookUpEdit)
                                        AddHandler x.UpdateDisplayFilter, AddressOf OnUpdateDisplayFilter
                                        AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                                        x.Properties.AllowMouseWheel = False
                                        x.Properties.UseCtrlScroll = True
                                        If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                                            x.Properties.PopupFilterMode = PopupFilterMode.Default
                                        Else
                                            x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                                            x.Properties.View.ActiveFilterEnabled = False
                                            x.Properties.View.ActiveFilterString = ""
                                        End If
                                    ElseIf TypeOf ctlb Is DevExpress.XtraEditors.TextEdit Then
                                        Dim x As TextEdit
                                        x = TryCast(ctlb, TextEdit)
                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                x.Properties.DisplayFormat.FormatString = "n2"
                                            End If
                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                            If x.Properties.EditFormat.FormatString = "" Then
                                                x.Properties.EditFormat.FormatString = "n2"
                                            End If
                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                            If x.Properties.Mask.EditMask = "" Then
                                                x.Properties.Mask.EditMask = "n2"
                                            End If
                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                            AddHandler x.GotFocus, AddressOf txt_GotFocus
                                            AddHandler x.Click, AddressOf txt_GotFocus
                                        End If
                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                    ElseIf TypeOf ctlb Is DevExpress.XtraEditors.ButtonEdit Then
                                        Dim x As ButtonEdit = TryCast(ctlb, ButtonEdit)
                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                x.Properties.DisplayFormat.FormatString = "n2"
                                            End If
                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                            If x.Properties.EditFormat.FormatString = "" Then
                                                x.Properties.EditFormat.FormatString = "n2"
                                            End If
                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                            If x.Properties.Mask.EditMask = "" Then
                                                x.Properties.Mask.EditMask = "n2"
                                            End If
                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                            AddHandler x.GotFocus, AddressOf button_GotFocus
                                            AddHandler x.Click, AddressOf button_GotFocus
                                        End If
                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                    ElseIf TypeOf ctlb Is DevExpress.XtraEditors.CalcEdit Then
                                        Dim x As CalcEdit = TryCast(ctlb, CalcEdit)
                                        If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                            x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                            If x.Properties.DisplayFormat.FormatString = "" Then
                                                x.Properties.DisplayFormat.FormatString = "n2"
                                            End If
                                            x.Properties.EditFormat.FormatType = FormatType.Numeric
                                            If x.Properties.EditFormat.FormatString = "" Then
                                                x.Properties.EditFormat.FormatString = "n2"
                                            End If
                                            x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                            If x.Properties.Mask.EditMask = "" Then
                                                x.Properties.Mask.EditMask = "n2"
                                            End If
                                            x.Properties.Mask.UseMaskAsDisplayFormat = True
                                            AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                            AddHandler x.GotFocus, AddressOf clc_GotFocus
                                            AddHandler x.Click, AddressOf clc_GotFocus
                                        End If
                                        'x.Properties.CharacterCasing = CharacterCasing.Upper
                                    End If
                                Next
                            ElseIf TypeOf ctla Is DevExpress.XtraEditors.DateEdit Then
                                Dim x As DateEdit = TryCast(ctla, DateEdit)
                                x.EnterMoveNextControl = True
                                x.Properties.DisplayFormat.FormatString = "dd-MM-yyyy"
                                x.Properties.EditFormat.FormatString = "dd-MM-yyyy"
                                x.Properties.Mask.EditMask = "dd-MM-yyyy"
                                x.Properties.EditFormat.FormatType = FormatType.DateTime
                                x.Properties.DisplayFormat.FormatType = FormatType.DateTime
                                x.Properties.Mask.MaskType = Mask.MaskType.DateTimeAdvancingCaret
                                x.Properties.Mask.UseMaskAsDisplayFormat = True
                            ElseIf TypeOf ctla Is DXSample.CustomSearchLookUpEdit Then
                                Dim x As DXSample.CustomSearchLookUpEdit
                                x = TryCast(ctla, DXSample.CustomSearchLookUpEdit)
                                AddHandler x.UpdateDisplayFilter, AddressOf OnUpdateDisplayFilter
                                AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                                x.Properties.AllowMouseWheel = False
                                x.Properties.UseCtrlScroll = True
                                If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                                    x.Properties.PopupFilterMode = PopupFilterMode.Default
                                Else
                                    x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                                    x.Properties.View.ActiveFilterEnabled = False
                                    x.Properties.View.ActiveFilterString = ""
                                End If
                            ElseIf TypeOf ctla Is DevExpress.XtraEditors.TextEdit Then
                                Dim x As TextEdit = TryCast(ctla, TextEdit)
                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                        x.Properties.DisplayFormat.FormatString = "n2"
                                    End If
                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                    If x.Properties.EditFormat.FormatString = "" Then
                                        x.Properties.EditFormat.FormatString = "n2"
                                    End If
                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                    If x.Properties.Mask.EditMask = "" Then
                                        x.Properties.Mask.EditMask = "n2"
                                    End If
                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                    AddHandler x.GotFocus, AddressOf txt_GotFocus
                                    AddHandler x.Click, AddressOf txt_GotFocus
                                End If
                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                            ElseIf TypeOf ctla Is DevExpress.XtraEditors.ButtonEdit Then
                                Dim x As ButtonEdit = TryCast(ctla, ButtonEdit)
                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                        x.Properties.DisplayFormat.FormatString = "n2"
                                    End If
                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                    If x.Properties.EditFormat.FormatString = "" Then
                                        x.Properties.EditFormat.FormatString = "n2"
                                    End If
                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                    If x.Properties.Mask.EditMask = "" Then
                                        x.Properties.Mask.EditMask = "n2"
                                    End If
                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                    AddHandler x.GotFocus, AddressOf button_GotFocus
                                    AddHandler x.Click, AddressOf button_GotFocus
                                End If
                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                            ElseIf TypeOf ctla Is DevExpress.XtraEditors.CalcEdit Then
                                Dim x As CalcEdit = TryCast(ctla, CalcEdit)
                                If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                    x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                                    If x.Properties.DisplayFormat.FormatString = "" Then
                                        x.Properties.DisplayFormat.FormatString = "n2"
                                    End If
                                    x.Properties.EditFormat.FormatType = FormatType.Numeric
                                    If x.Properties.EditFormat.FormatString = "" Then
                                        x.Properties.EditFormat.FormatString = "n2"
                                    End If
                                    x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                                    If x.Properties.Mask.EditMask = "" Then
                                        x.Properties.Mask.EditMask = "n2"
                                    End If
                                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                                    AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                                    AddHandler x.GotFocus, AddressOf clc_GotFocus
                                    AddHandler x.Click, AddressOf clc_GotFocus
                                End If
                                'x.Properties.CharacterCasing = CharacterCasing.Upper
                            End If
                        Next
                    Next
                ElseIf TypeOf ctrl Is DXSample.CustomSearchLookUpEdit Then
                    Dim x As DXSample.CustomSearchLookUpEdit
                    x = TryCast(ctrl, DXSample.CustomSearchLookUpEdit)
                    AddHandler x.UpdateDisplayFilter, AddressOf OnUpdateDisplayFilter
                    AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                    x.Properties.AllowMouseWheel = False
                    x.Properties.UseCtrlScroll = True
                    If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                        x.Properties.PopupFilterMode = PopupFilterMode.Default
                    Else
                        x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                        x.Properties.View.ActiveFilterEnabled = False
                        x.Properties.View.ActiveFilterString = ""
                    End If
                ElseIf TypeOf ctrl Is DevExpress.XtraEditors.DateEdit Then
                    Dim x As DateEdit = TryCast(ctrl, DateEdit)
                    x.EnterMoveNextControl = True
                    x.Properties.DisplayFormat.FormatString = "dd-MM-yyyy"
                    x.Properties.EditFormat.FormatString = "dd-MM-yyyy"
                    x.Properties.Mask.EditMask = "dd-MM-yyyy"
                    x.Properties.EditFormat.FormatType = FormatType.DateTime
                    x.Properties.DisplayFormat.FormatType = FormatType.DateTime
                    x.Properties.Mask.MaskType = Mask.MaskType.DateTimeAdvancingCaret
                    x.Properties.Mask.UseMaskAsDisplayFormat = True
                ElseIf TypeOf ctrl Is DXSample.CustomSearchLookUpEdit Then
                    Dim x As DXSample.CustomSearchLookUpEdit = TryCast(ctrl, DXSample.CustomSearchLookUpEdit)
                    AddHandler x.KeyDown, AddressOf lkEdit_KeyDown
                    x.Properties.AllowMouseWheel = False
                    x.Properties.UseCtrlScroll = True
                    If TipePencarianLookup = pTipePencarianLookup.DepanBelakang Then
                        x.Properties.PopupFilterMode = PopupFilterMode.Default
                    Else
                        x.Properties.PopupFilterMode = PopupFilterMode.StartsWith
                        x.Properties.View.ActiveFilterEnabled = False
                        x.Properties.View.ActiveFilterString = ""
                    End If
                ElseIf TypeOf ctrl Is DevExpress.XtraEditors.TextEdit Then
                    Dim x As TextEdit = TryCast(ctrl, TextEdit)
                    If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                        x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                        If x.Properties.DisplayFormat.FormatString = "" Then
                            x.Properties.DisplayFormat.FormatString = "n2"
                        End If
                        x.Properties.EditFormat.FormatType = FormatType.Numeric
                        If x.Properties.EditFormat.FormatString = "" Then
                            x.Properties.EditFormat.FormatString = "n2"
                        End If
                        x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                        If x.Properties.Mask.EditMask = "" Then
                            x.Properties.Mask.EditMask = "n2"
                        End If
                        x.Properties.Mask.UseMaskAsDisplayFormat = True
                        AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                        AddHandler x.GotFocus, AddressOf txt_GotFocus
                        AddHandler x.Click, AddressOf txt_GotFocus
                    End If
                    'x.Properties.CharacterCasing = CharacterCasing.Upper
                ElseIf TypeOf ctrl Is DevExpress.XtraEditors.ButtonEdit Then
                    Dim x As ButtonEdit = TryCast(ctrl, ButtonEdit)
                    If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                        x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                        If x.Properties.DisplayFormat.FormatString = "" Then
                            x.Properties.DisplayFormat.FormatString = "n2"
                        End If
                        x.Properties.EditFormat.FormatType = FormatType.Numeric
                        If x.Properties.EditFormat.FormatString = "" Then
                            x.Properties.EditFormat.FormatString = "n2"
                        End If
                        x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                        If x.Properties.Mask.EditMask = "" Then
                            x.Properties.Mask.EditMask = "n2"
                        End If
                        x.Properties.Mask.UseMaskAsDisplayFormat = True
                        AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                        AddHandler x.GotFocus, AddressOf button_GotFocus
                        AddHandler x.Click, AddressOf button_GotFocus
                    End If
                    'x.Properties.CharacterCasing = CharacterCasing.Upper
                ElseIf TypeOf ctrl Is DevExpress.XtraEditors.CalcEdit Then
                    Dim x As CalcEdit = TryCast(ctrl, CalcEdit)
                    If x.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                        x.Properties.DisplayFormat.FormatType = FormatType.Numeric
                        If x.Properties.DisplayFormat.FormatString = "" Then
                            x.Properties.DisplayFormat.FormatString = "n2"
                        End If
                        x.Properties.EditFormat.FormatType = FormatType.Numeric
                        If x.Properties.EditFormat.FormatString = "" Then
                            x.Properties.EditFormat.FormatString = "n2"
                        End If
                        x.Properties.Mask.MaskType = Mask.MaskType.Numeric
                        If x.Properties.Mask.EditMask = "" Then
                            x.Properties.Mask.EditMask = "n2"
                        End If
                        x.Properties.Mask.UseMaskAsDisplayFormat = True
                        AddHandler x.KeyDown, AddressOf txtNumeric_KeyDown
                        AddHandler x.GotFocus, AddressOf clc_GotFocus
                        AddHandler x.Click, AddressOf clc_GotFocus
                    End If
                    'x.Properties.CharacterCasing = CharacterCasing.Upper
                End If
            Next
            If System.IO.File.Exists(Application.StartupPath & "\ICON.ico") Then
                ico = New System.Drawing.Icon(Application.StartupPath & "\ICON.ico")
                frm.Icon = ico
            End If
        Catch ex As Exception
            'Finally
            '    ico.Dispose()
        End Try
    End Sub
#Region " Fix the disabled column chooser option on the grid "

    ' Handler for grid menu 
    Private Shared Sub GV1_ShowCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs)
        ' Make sure we've got the right menu
        Dim GV1 As DevExpress.XtraGrid.Views.Grid.GridView
        GV1 = TryCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim x As New FormOtorisasi
        Try
            If Not IsEditLayout Then
                If Not x.ShowDialog(FormMain) = Windows.Forms.DialogResult.OK Then
                    GV1.DestroyCustomization()
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Kesalahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            x.Dispose()
        End Try
        'If se = GridMenuType.Column Then
        '    ' Remove the non-working "Column Chooser" option
        '    For Each item As DXMenuItem In e.Menu.Items
        '        If item.Caption = GridLocalizer.Active.GetLocalizedString(GridStringId.MenuColumnColumnCustomization) Then
        '            item.Visible = False
        '            Exit For
        '        End If
        '    Next item

        '    ' Add our replacement option
        '    Dim menuItem As New DXMenuItem("Column Chooser", New EventHandler(AddressOf ItemGrid_ColumnChooser_Click))
        '    menuItem.BeginGroup = True
        '    e.Menu.Items.Add(menuItem)
        'End If
    End Sub

    Private Shared Sub GV1_CustomDrawCell(sender As Object, e As RowCellCustomDrawEventArgs)
        'Try
        '    Dim GV1 As GridView = TryCast(sender, GridView)
        '    If GV1.OptionsBehavior.Editable Or Not String.IsNullOrEmpty(GV1.GetAutoFilterValue(GV1.FocusedColumn)) Then Exit Sub

        '    Dim cc As GridColumn
        '    Dim Format As String = ""
        '    'For Each cc In sender.VisibleColumns
        '    '    If cc.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum Then
        '    '        Format = cc.SummaryItem.DisplayFormat.Replace("SUM=", "")
        '    '        cc.SummaryItem.DisplayFormat = Format
        '    '    ElseIf cc.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count Then
        '    '        Format = "COUNT={0}"
        '    '        cc.SummaryItem.DisplayFormat = Format
        '    '    End If
        '    'Next
        '    cc = e.Column
        '    If cc.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum Then
        '        Format = cc.SummaryItem.DisplayFormat.Replace("SUM=", "")
        '        cc.SummaryItem.DisplayFormat = Format
        '    ElseIf cc.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count Then

        '        Format = "COUNT={0}"
        '        cc.SummaryItem.DisplayFormat = Format

        '    End If
        '    GV1.UpdateSummary()
        'Catch ex As Exception

        'End Try
    End Sub

    ' Handler for click on our replacement column chooser
    'Private Sub ItemGrid_ColumnChooser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    ' Open the column customization form
    '    GV1.ColumnsCustomization()
    'End Sub

#End Region
    Public Shared Sub SetButton(ByRef btn As SimpleButton, ByVal tipe As button_, Optional ByVal includehandles As Boolean = True)
        If includehandles Then
            'Add Handler
            'AddHandler btn.MouseHover, AddressOf SimpleButton_MouseHover
            'AddHandler btn.MouseLeave, AddressOf SimpleButton_MouseLeave
        End If
        'Properties
        Select Case tipe
            Case button_.cmdNew
                btn.Text = "&New"
                btn.ToolTip = "Tombol untuk entri item baru."

            Case button_.cmdEdit
                btn.Text = "&Edit"
                btn.ToolTip = "Tombol untuk edit item."

            Case button_.cmdDelete
                btn.Text = "&Delete"
                btn.ToolTip = "Tombol untuk menghapus item."

            Case button_.cmdCancel
                btn.Text = "&Cancel"
                btn.ToolTip = "Tombol untuk membatalkan proses."

            Case button_.cmdExit
                btn.Text = "E&xit"
                btn.ToolTip = "Tombol untuk keluar."

            Case button_.cmdExportXls
                btn.Text = "Exce&l"
                btn.ToolTip = "Tombol untuk mengexport ke excel."

            Case button_.cmdMark
                btn.Text = "&Mark"
                btn.ToolTip = "Tombol untuk menandai item."

            Case button_.cmdOK
                btn.Text = "&Ok"
                btn.ToolTip = "Tombol OK."

            Case button_.cmdPosting
                btn.Text = "&Posting"
                btn.ToolTip = "Tombol untuk memosting item."

            Case button_.cmdPrint
                btn.Text = "P&rint"
                btn.ToolTip = "Tombol untuk print laporan."

            Case button_.cmdRefresh
                btn.Text = "Re&fresh"
                btn.ToolTip = "Tombol untuk merefresh data."

            Case button_.cmdUnMark
                btn.Text = "U&nmark"
                btn.ToolTip = "Tombol untuk menghilangi tanda."

            Case button_.cmdUnposting
                btn.Text = "&Unposting"
                btn.ToolTip = "Tombol untuk mengunposting item."

            Case button_.cmdSave
                btn.Text = "&Save"
                btn.ToolTip = "Tombol untuk menyimpan data."

            Case button_.cmdCancelSave
                btn.Text = "&Cancel"
                btn.ToolTip = "Tombol untuk membatalkan penyimpanan."

            Case button_.cmdViewDetil
                btn.Text = "&View Detil"
                btn.ToolTip = "Tombol untuk melihat detil item."

            Case button_.cmdReset
                btn.Text = "&Reset"
                btn.ToolTip = "Tombol untuk mereset item."

            Case button_.cmdLogin
                btn.Text = "&Login"
                btn.ToolTip = "Tombol untuk masuk ke aplikasi."

            Case button_.cmdLogout
                btn.Text = "Log&out"
                btn.ToolTip = "Tombol untuk keluar dari aplikasi."

            Case button_.cmdDatabase
                btn.Text = ""
                btn.ToolTip = "Tombol untuk menyeting database yang digunakan aplikasi."

            Case Else
                btn.Text = btn.Text.Trim.Replace("&", "").Substring(0, 1).ToUpper & btn.Text.Trim.Replace("&", "").Substring(1).ToLower
                btn.ToolTip = "Tombol untuk " & LCase(btn.Text) & "."
        End Select
        FontSize = CInt(Ini.BacaIni("Font", "Size", "10"))
        FontName = CStr(Ini.BacaIni("Font", "Name", "Calibri"))
        IsBold = CBool(Ini.BacaIni("Font", "IsBold", "1"))
        If IsBold Then
            btn.Font = New Font(FontName, FontSize, FontStyle.Bold)
        Else
            btn.Font = New Font(FontName, FontSize, FontStyle.Regular)
        End If

        'Style
        Dim FileImage As String = Application.StartupPath & "\System\Image\" & CStr(tipe.ToString) & ".ico"
        Try
            If System.IO.File.Exists(FileImage) Then
                btn.Image = Image.FromFile(FileImage)
            Else
                If Not DirectoryExists(Application.StartupPath & "\System\Image") Then
                    System.IO.Directory.CreateDirectory(Application.StartupPath & "\System\Image")
                End If
                btn.Image = Image.FromFile(Application.StartupPath & "\System\Image\command.ico")
            End If

            btn.Cursor = Cursors.Hand
            'btn.BackColor = Color.Snow
            'btn.Appearance.BackColor2 = Color.PowderBlue
            'btn.Appearance.BorderColor = SystemColors.ButtonFace
            'btn.Appearance.ForeColor = Color.FromName(Ini.BacaIni("Button", "ForeColor", "Black"))
            btn.Appearance.Options.UseBackColor = False
            btn.Appearance.Options.UseBorderColor = False
            btn.Appearance.Options.UseFont = True
            btn.Appearance.Options.UseForeColor = False
            btn.LookAndFeel.UseDefaultLookAndFeel = True
            btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message & vbCrLf & FileImage)
        End Try
    End Sub
    Public Shared Sub SimpleButton_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim Tbl As SimpleButton
        'Tbl = trycast(sender, SimpleButton)
        'Dim xfont As Font = Tbl.Font
        'Tbl.Font = New Font(xfont, Tbl.Font.Size + 2)
    End Sub
    Public Shared Sub SimpleButton_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim Tbl As SimpleButton = trycast(sender, SimpleButton)
        'Dim xfont As Font = Tbl.Font
        'If Tbl.Font.Size >= 9.75 Then
        '    Tbl.Font = New Font(xfont, Tbl.Font.Size - 2)
        'End If
    End Sub
    Public Shared Sub txtNumeric_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            e.Handled = True
        End If
    End Sub
    Private Shared Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txt As DevExpress.XtraEditors.TextEdit = TryCast(sender, DevExpress.XtraEditors.TextEdit)
        txt.SelectAll()
    End Sub
    Private Shared Sub clc_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txt As DevExpress.XtraEditors.CalcEdit = TryCast(sender, DevExpress.XtraEditors.CalcEdit)
        txt.SelectAll()
    End Sub
    Private Shared Sub button_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txt As DevExpress.XtraEditors.ButtonEdit = TryCast(sender, DevExpress.XtraEditors.ButtonEdit)
        txt.SelectAll()
    End Sub
    Public Shared Sub SendKeys(ByVal Keys As String)
        Try
            Shell(Keys, AppWinStyle.NormalFocus, True, 30)
        Catch ex As Exception
            XtraMessageBox.Show("Pesan Kesalahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class

Public NotInheritable Class FilterCriteriaHelper

    Private Sub New()
    End Sub
    Public Shared Function ReplaceFilterCriteria(ByVal source As CriteriaOperator, ByVal prevOperand As CriteriaOperator, ByVal newOperand As CriteriaOperator) As CriteriaOperator
        Dim groupOperand As GroupOperator = TryCast(source, GroupOperator)
        If ReferenceEquals(groupOperand, Nothing) Then
            Return newOperand
        End If
        Dim clone As GroupOperator = groupOperand.Clone()
        clone.Operands.Remove(prevOperand)
        If clone.Equals(source) Then
            Return newOperand
        End If
        clone.Operands.Add(newOperand)
        Return clone
    End Function

    Public Shared Function ReplaceFindPanelCriteria(ByVal source As CriteriaOperator, ByVal view As GridView, ByVal newOperand As CriteriaOperator) As CriteriaOperator
        Return ReplaceFilterCriteria(source, ConvertFindPanelTextToCriteriaOperator(view.FindFilterText, view, True), newOperand)
    End Function

    Public Shared Function ConvertFindPanelTextToCriteriaOperator(ByVal findPanelText As String, ByVal view As GridView, ByVal applyPrefixes As Boolean) As CriteriaOperator
        If (Not String.IsNullOrEmpty(findPanelText)) Then
            Dim FieldsName As String = ""
            Dim parseResult As FindSearchParserResults =
                New FindSearchParser().Parse(findPanelText, GetFindToColumnsCollection(view, FieldsName))

            If applyPrefixes Then
                parseResult.AppendColumnFieldPrefixes()
            End If
            If NullToStr(FieldsName) <> "" AndAlso FieldsName.Length > 1 Then FieldsName = FieldsName.Substring(0, FieldsName.Length - 1)
            Dim column As String() = FieldsName.Split(",")

            Return DxFtsContainsHelper.Create(column, parseResult)
        End If
        Return Nothing
    End Function


    Private Shared Function GetFindToColumnsCollection(ByVal view As GridView, ByRef FieldsName As String) As List(Of IDataColumnInfo)
        Dim res As New List(Of IDataColumnInfo)()
        FieldsName = ""
        For Each column As GridColumn In view.VisibleColumns
            If IsAllowFindColumn(column) And column.ColumnType = GetType(String) Then
                Dim dataColumn As DataColumnInfo = view.DataController.Columns(column.FieldName)
                If dataColumn IsNot Nothing Then
                    res.Add(dataColumn)
                    FieldsName &= column.FieldName & ","
                End If
            End If
        Next column
        For Each column As GridColumn In view.GroupedColumns
            Dim dataColumn As DataColumnInfo = view.DataController.Columns(column.FieldName)
            If dataColumn Is Nothing OrElse res.Contains(dataColumn) OrElse (Not IsAllowFindColumn(column)) Then
                Continue For
            End If
            res.Add(dataColumn)
            FieldsName &= column.FieldName & ","
        Next column
        Return res
    End Function

    Friend Shared Function IsAllowFindColumn(ByVal col As GridColumn) As Boolean
        If col Is Nothing OrElse String.IsNullOrEmpty(col.FieldName) Then
            Return False
        End If
        If TypeOf col.ColumnEdit Is RepositoryItemPictureEdit OrElse TypeOf col.ColumnEdit Is RepositoryItemImageEdit Then
            Return False
        End If
        Dim view As GridView = TryCast(col.View, GridView)
        If view.OptionsFind.FindFilterColumns = "*" Then
            Return True
        End If
        Return String.Concat(";", view.OptionsFind.FindFilterColumns, ";").Contains(String.Concat(";", col.FieldName, ";"))
    End Function

    Public Shared Function MyConvertFindPanelTextToCriteriaOperator(ByVal view As GridView) As CriteriaOperator
        Return ConvertFindPanelTextToCriteriaOperator(String.Format("""{0}""", view.FindFilterText), view, False)
    End Function
End Class

Public Class MyFindPanelFilterHelper

    Public Sub New(ByVal view As GridView)
        _View = view
        AddHandler view.CustomRowFilter, AddressOf view_CustomRowFilter

    End Sub

    Private lastCriteria As String
    Private lastEvaluator As ExpressionEvaluator
    Private _View As GridView
    Private Function GetExpressionEvaluator(ByVal criteria As CriteriaOperator) As ExpressionEvaluator
        If criteria.ToString() = lastCriteria Then
            Return lastEvaluator
        End If
        lastCriteria = criteria.ToString()
        Dim pdc As PropertyDescriptorCollection = (CType(_View.DataSource, ITypedList)).GetItemProperties(Nothing)
        lastEvaluator = New ExpressionEvaluator(pdc, criteria, False)
        Return lastEvaluator
    End Function

    Private Function GetFindPanelCriteria() As CriteriaOperator
        Dim criteria As CriteriaOperator = FilterCriteriaHelper.MyConvertFindPanelTextToCriteriaOperator(_View)
        Return criteria
    End Function
    Private Sub view_CustomRowFilter(ByVal sender As Object, ByVal e As RowFilterEventArgs)
        Try
            If Not String.IsNullOrEmpty(_View.FindFilterText) Then
                Dim criteria As CriteriaOperator = FilterCriteriaHelper.ReplaceFindPanelCriteria(_View.DataController.FilterCriteria, _View, GetFindPanelCriteria())
                Dim evaluator As ExpressionEvaluator = GetExpressionEvaluator(criteria)
                e.Handled = True
                e.Visible = evaluator.Fit((TryCast(_View.DataSource, DataView))(e.ListSourceRow))
            Else
                'Return
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class

Public Class LayoutsHelper
    Public Shared FolderLayouts As String = Application.StartupPath & "\System\Layouts\"
    Public Shared IsRestoreLayouts As Boolean = False

    Public Shared Sub RestoreLayouts(ByVal Control As Object, ByVal NamaFile As String)
        Try
            If IsRestoreLayouts Then
                NamaFile = NamaFile.ToLower.Replace(FolderLayouts.ToLower, Application.StartupPath & "\System\Layouts\Default\".ToLower)
                If System.IO.File.Exists(NamaFile) Then
                    Control.RestoreLayoutFromXml(NamaFile)
                End If
            Else
                If System.IO.File.Exists(NamaFile) Then
                    Control.RestoreLayoutFromXml(NamaFile)
                Else
                    NamaFile = NamaFile.ToLower.Replace(FolderLayouts.ToLower, Application.StartupPath & "\System\Layouts\Default\".ToLower)
                    If System.IO.File.Exists(NamaFile) Then
                        Control.RestoreLayoutFromXml(NamaFile)
                    End If
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Info Kesalahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Public Shared Sub SaveLayouts(ByVal Control As Object, ByVal NamaFile As String)
        Try
            Control.SaveLayoutToXml(NamaFile)
        Catch ex As Exception
            XtraMessageBox.Show("Info Kesalahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
End Class