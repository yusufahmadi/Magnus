﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormKasBankMasuk
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormKasBankMasuk))
        Me.GC1 = New DevExpress.XtraGrid.GridControl()
        Me.GV1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GColID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnNoUrut = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GColIDAkun = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemSearchLookUpAkun = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.RepositoryItemSearchLookUpEdit1GViewAkun = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GColIDRekanan = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemSearchLookUpRekanan = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.RepositoryItemSearchLookUpEdit2GViewRekanan = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCollIDReff = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemSearchLookUpTransaksi = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.RepositoryItemSearchLookUpEdit3GViewTransaksi = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GColNominal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtNominal = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.GridColumnCatatan = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GColKurs = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtKurs = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.colId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mainRibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.bbiSave = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiSaveAndClose = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiSaveAndNew = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiReset = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiDelete = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiClose = New DevExpress.XtraBars.BarButtonItem()
        Me.mainRibbonPage = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.mainRibbonPageGroup = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.txtTgl = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.ckIsBG = New DevExpress.XtraEditors.CheckEdit()
        Me.cbTipeForm = New System.Windows.Forms.ComboBox()
        Me.txtKodeReff = New DevExpress.XtraEditors.TextEdit()
        Me.txtIDKasBank = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtKode = New DevExpress.XtraEditors.TextEdit()
        Me.txtKeterangan = New DevExpress.XtraEditors.MemoEdit()
        Me.txtJTBG = New DevExpress.XtraEditors.DateEdit()
        Me.txtNoGiro = New DevExpress.XtraEditors.TextEdit()
        Me.txtIDRekanan = New DevExpress.XtraEditors.LookUpEdit()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroupGiro = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.DxErrorProvider1 = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.BarButtonHapusItem = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.GC1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpAkun, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit1GViewAkun, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpRekanan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit2GViewRekanan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpTransaksi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit3GViewTransaksi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNominal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKurs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mainRibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTgl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.ckIsBG.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKodeReff.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIDKasBank.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKeterangan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtJTBG.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtJTBG.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoGiro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIDRekanan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroupGiro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GC1
        '
        Me.GC1.Location = New System.Drawing.Point(12, 156)
        Me.GC1.MainView = Me.GV1
        Me.GC1.Name = "GC1"
        Me.GC1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.txtNominal, Me.RepositoryItemSearchLookUpAkun, Me.RepositoryItemSearchLookUpRekanan, Me.RepositoryItemSearchLookUpTransaksi, Me.txtKurs})
        Me.GC1.Size = New System.Drawing.Size(998, 348)
        Me.GC1.TabIndex = 1
        Me.GC1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GV1})
        '
        'GV1
        '
        Me.GV1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GColID, Me.GridColumnNoUrut, Me.GColIDAkun, Me.GColIDRekanan, Me.GCollIDReff, Me.GColNominal, Me.GridColumnCatatan, Me.GColKurs})
        Me.GV1.GridControl = Me.GC1
        Me.GV1.Name = "GV1"
        Me.GV1.OptionsView.ColumnAutoWidth = False
        Me.GV1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.GV1.OptionsView.ShowFooter = True
        Me.GV1.OptionsView.ShowGroupPanel = False
        '
        'GColID
        '
        Me.GColID.Caption = "ID"
        Me.GColID.FieldName = "ID"
        Me.GColID.Name = "GColID"
        Me.GColID.OptionsColumn.AllowEdit = False
        Me.GColID.OptionsColumn.AllowMove = False
        Me.GColID.Visible = True
        Me.GColID.VisibleIndex = 0
        Me.GColID.Width = 35
        '
        'GridColumnNoUrut
        '
        Me.GridColumnNoUrut.Caption = "NoUrut"
        Me.GridColumnNoUrut.FieldName = "NoUrut"
        Me.GridColumnNoUrut.Name = "GridColumnNoUrut"
        Me.GridColumnNoUrut.OptionsColumn.AllowEdit = False
        Me.GridColumnNoUrut.OptionsColumn.AllowMove = False
        Me.GridColumnNoUrut.OptionsFilter.AllowAutoFilter = False
        Me.GridColumnNoUrut.Visible = True
        Me.GridColumnNoUrut.VisibleIndex = 1
        Me.GridColumnNoUrut.Width = 45
        '
        'GColIDAkun
        '
        Me.GColIDAkun.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GColIDAkun.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GColIDAkun.AppearanceCell.Options.UseBackColor = True
        Me.GColIDAkun.AppearanceHeader.Options.UseTextOptions = True
        Me.GColIDAkun.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GColIDAkun.Caption = "Akun"
        Me.GColIDAkun.ColumnEdit = Me.RepositoryItemSearchLookUpAkun
        Me.GColIDAkun.FieldName = "IDAkun"
        Me.GColIDAkun.Name = "GColIDAkun"
        Me.GColIDAkun.OptionsColumn.AllowMove = False
        Me.GColIDAkun.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "IDBarang", "{0}")})
        Me.GColIDAkun.Visible = True
        Me.GColIDAkun.VisibleIndex = 2
        Me.GColIDAkun.Width = 100
        '
        'RepositoryItemSearchLookUpAkun
        '
        Me.RepositoryItemSearchLookUpAkun.AutoHeight = False
        Me.RepositoryItemSearchLookUpAkun.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemSearchLookUpAkun.Name = "RepositoryItemSearchLookUpAkun"
        Me.RepositoryItemSearchLookUpAkun.NullText = "[Pilih Akun]"
        Me.RepositoryItemSearchLookUpAkun.View = Me.RepositoryItemSearchLookUpEdit1GViewAkun
        '
        'RepositoryItemSearchLookUpEdit1GViewAkun
        '
        Me.RepositoryItemSearchLookUpEdit1GViewAkun.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemSearchLookUpEdit1GViewAkun.Name = "RepositoryItemSearchLookUpEdit1GViewAkun"
        Me.RepositoryItemSearchLookUpEdit1GViewAkun.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemSearchLookUpEdit1GViewAkun.OptionsView.ShowGroupPanel = False
        '
        'GColIDRekanan
        '
        Me.GColIDRekanan.Caption = "Rekanan"
        Me.GColIDRekanan.ColumnEdit = Me.RepositoryItemSearchLookUpRekanan
        Me.GColIDRekanan.FieldName = "IDRekanan"
        Me.GColIDRekanan.Name = "GColIDRekanan"
        Me.GColIDRekanan.OptionsColumn.AllowMove = False
        Me.GColIDRekanan.Visible = True
        Me.GColIDRekanan.VisibleIndex = 3
        Me.GColIDRekanan.Width = 122
        '
        'RepositoryItemSearchLookUpRekanan
        '
        Me.RepositoryItemSearchLookUpRekanan.AutoHeight = False
        Me.RepositoryItemSearchLookUpRekanan.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemSearchLookUpRekanan.Name = "RepositoryItemSearchLookUpRekanan"
        Me.RepositoryItemSearchLookUpRekanan.View = Me.RepositoryItemSearchLookUpEdit2GViewRekanan
        '
        'RepositoryItemSearchLookUpEdit2GViewRekanan
        '
        Me.RepositoryItemSearchLookUpEdit2GViewRekanan.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemSearchLookUpEdit2GViewRekanan.Name = "RepositoryItemSearchLookUpEdit2GViewRekanan"
        Me.RepositoryItemSearchLookUpEdit2GViewRekanan.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemSearchLookUpEdit2GViewRekanan.OptionsView.ShowGroupPanel = False
        '
        'GCollIDReff
        '
        Me.GCollIDReff.AppearanceHeader.Options.UseTextOptions = True
        Me.GCollIDReff.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GCollIDReff.Caption = "Reff"
        Me.GCollIDReff.ColumnEdit = Me.RepositoryItemSearchLookUpTransaksi
        Me.GCollIDReff.FieldName = "IDReff"
        Me.GCollIDReff.Name = "GCollIDReff"
        Me.GCollIDReff.OptionsColumn.AllowMove = False
        Me.GCollIDReff.Width = 100
        '
        'RepositoryItemSearchLookUpTransaksi
        '
        Me.RepositoryItemSearchLookUpTransaksi.AutoHeight = False
        Me.RepositoryItemSearchLookUpTransaksi.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemSearchLookUpTransaksi.Name = "RepositoryItemSearchLookUpTransaksi"
        Me.RepositoryItemSearchLookUpTransaksi.View = Me.RepositoryItemSearchLookUpEdit3GViewTransaksi
        '
        'RepositoryItemSearchLookUpEdit3GViewTransaksi
        '
        Me.RepositoryItemSearchLookUpEdit3GViewTransaksi.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemSearchLookUpEdit3GViewTransaksi.Name = "RepositoryItemSearchLookUpEdit3GViewTransaksi"
        Me.RepositoryItemSearchLookUpEdit3GViewTransaksi.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemSearchLookUpEdit3GViewTransaksi.OptionsView.ShowGroupPanel = False
        '
        'GColNominal
        '
        Me.GColNominal.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GColNominal.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GColNominal.AppearanceCell.BorderColor = System.Drawing.Color.RoyalBlue
        Me.GColNominal.AppearanceCell.Options.UseBackColor = True
        Me.GColNominal.AppearanceCell.Options.UseBorderColor = True
        Me.GColNominal.AppearanceHeader.Options.UseTextOptions = True
        Me.GColNominal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GColNominal.Caption = "Nominal"
        Me.GColNominal.ColumnEdit = Me.txtNominal
        Me.GColNominal.DisplayFormat.FormatString = "n2"
        Me.GColNominal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GColNominal.FieldName = "Nominal"
        Me.GColNominal.GroupFormat.FormatString = "n2"
        Me.GColNominal.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GColNominal.Name = "GColNominal"
        Me.GColNominal.OptionsColumn.AllowMove = False
        Me.GColNominal.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Nominal", "GT : {0:#,###}")})
        Me.GColNominal.Visible = True
        Me.GColNominal.VisibleIndex = 4
        Me.GColNominal.Width = 109
        '
        'txtNominal
        '
        Me.txtNominal.AutoHeight = False
        Me.txtNominal.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtNominal.DisplayFormat.FormatString = "n2"
        Me.txtNominal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtNominal.EditFormat.FormatString = "n2"
        Me.txtNominal.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtNominal.Mask.BeepOnError = True
        Me.txtNominal.Mask.UseMaskAsDisplayFormat = True
        Me.txtNominal.Name = "txtNominal"
        '
        'GridColumnCatatan
        '
        Me.GridColumnCatatan.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridColumnCatatan.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridColumnCatatan.AppearanceCell.Options.UseBackColor = True
        Me.GridColumnCatatan.Caption = "Catatan"
        Me.GridColumnCatatan.FieldName = "Catatan"
        Me.GridColumnCatatan.Name = "GridColumnCatatan"
        Me.GridColumnCatatan.Visible = True
        Me.GridColumnCatatan.VisibleIndex = 5
        Me.GridColumnCatatan.Width = 199
        '
        'GColKurs
        '
        Me.GColKurs.AppearanceHeader.Options.UseTextOptions = True
        Me.GColKurs.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GColKurs.Caption = "Kurs"
        Me.GColKurs.ColumnEdit = Me.txtKurs
        Me.GColKurs.DisplayFormat.FormatString = "n2"
        Me.GColKurs.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GColKurs.FieldName = "Kurs"
        Me.GColKurs.Name = "GColKurs"
        Me.GColKurs.OptionsColumn.AllowFocus = False
        Me.GColKurs.Width = 120
        '
        'txtKurs
        '
        Me.txtKurs.AutoHeight = False
        Me.txtKurs.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtKurs.DisplayFormat.FormatString = "n2"
        Me.txtKurs.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtKurs.EditFormat.FormatString = "n2"
        Me.txtKurs.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtKurs.Mask.BeepOnError = True
        Me.txtKurs.Mask.UseMaskAsDisplayFormat = True
        Me.txtKurs.Name = "txtKurs"
        '
        'colId
        '
        Me.colId.AppearanceHeader.Options.UseTextOptions = True
        Me.colId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colId.Caption = "Mã vật tư"
        Me.colId.FieldName = "Item_Id"
        Me.colId.Name = "colId"
        Me.colId.Visible = True
        Me.colId.VisibleIndex = 0
        '
        'colName
        '
        Me.colName.AppearanceHeader.Options.UseTextOptions = True
        Me.colName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colName.Caption = "Tên vật tư"
        Me.colName.FieldName = "Item_Name"
        Me.colName.Name = "colName"
        Me.colName.Visible = True
        Me.colName.VisibleIndex = 1
        '
        'mainRibbonControl
        '
        Me.mainRibbonControl.ExpandCollapseItem.Id = 0
        Me.mainRibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.mainRibbonControl.ExpandCollapseItem, Me.bbiSave, Me.bbiSaveAndClose, Me.bbiSaveAndNew, Me.bbiReset, Me.bbiDelete, Me.bbiClose, Me.BarButtonHapusItem})
        Me.mainRibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.mainRibbonControl.MaxItemId = 11
        Me.mainRibbonControl.Name = "mainRibbonControl"
        Me.mainRibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.mainRibbonPage})
        Me.mainRibbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.OfficeUniversal
        Me.mainRibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.mainRibbonControl.Size = New System.Drawing.Size(1022, 83)
        Me.mainRibbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden
        '
        'bbiSave
        '
        Me.bbiSave.Caption = "Save"
        Me.bbiSave.Id = 2
        Me.bbiSave.ImageOptions.ImageUri.Uri = "Save"
        Me.bbiSave.Name = "bbiSave"
        '
        'bbiSaveAndClose
        '
        Me.bbiSaveAndClose.Caption = "Save And Close"
        Me.bbiSaveAndClose.Id = 3
        Me.bbiSaveAndClose.ImageOptions.ImageUri.Uri = "SaveAndClose"
        Me.bbiSaveAndClose.Name = "bbiSaveAndClose"
        '
        'bbiSaveAndNew
        '
        Me.bbiSaveAndNew.Caption = "Save And New"
        Me.bbiSaveAndNew.Id = 4
        Me.bbiSaveAndNew.ImageOptions.ImageUri.Uri = "SaveAndNew"
        Me.bbiSaveAndNew.Name = "bbiSaveAndNew"
        '
        'bbiReset
        '
        Me.bbiReset.Caption = "Reset Changes"
        Me.bbiReset.Id = 5
        Me.bbiReset.ImageOptions.ImageUri.Uri = "Reset"
        Me.bbiReset.Name = "bbiReset"
        '
        'bbiDelete
        '
        Me.bbiDelete.Caption = "Delete"
        Me.bbiDelete.Id = 6
        Me.bbiDelete.ImageOptions.ImageUri.Uri = "Delete"
        Me.bbiDelete.Name = "bbiDelete"
        '
        'bbiClose
        '
        Me.bbiClose.Caption = "Close"
        Me.bbiClose.Id = 7
        Me.bbiClose.ImageOptions.ImageUri.Uri = "Close"
        Me.bbiClose.Name = "bbiClose"
        '
        'mainRibbonPage
        '
        Me.mainRibbonPage.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.mainRibbonPageGroup})
        Me.mainRibbonPage.MergeOrder = 0
        Me.mainRibbonPage.Name = "mainRibbonPage"
        Me.mainRibbonPage.Text = "Action"
        '
        'mainRibbonPageGroup
        '
        Me.mainRibbonPageGroup.AllowTextClipping = False
        Me.mainRibbonPageGroup.ItemLinks.Add(Me.bbiSave)
        Me.mainRibbonPageGroup.ItemLinks.Add(Me.bbiSaveAndClose)
        Me.mainRibbonPageGroup.ItemLinks.Add(Me.bbiSaveAndNew)
        Me.mainRibbonPageGroup.ItemLinks.Add(Me.bbiReset)
        Me.mainRibbonPageGroup.ItemLinks.Add(Me.bbiDelete)
        Me.mainRibbonPageGroup.ItemLinks.Add(Me.bbiClose)
        Me.mainRibbonPageGroup.Name = "mainRibbonPageGroup"
        Me.mainRibbonPageGroup.ShowCaptionButton = False
        Me.mainRibbonPageGroup.Text = "Tasks"
        '
        'txtTgl
        '
        Me.txtTgl.EditValue = New Date(2020, 1, 1, 6, 41, 9, 0)
        Me.txtTgl.Location = New System.Drawing.Point(83, 24)
        Me.txtTgl.MenuManager = Me.mainRibbonControl
        Me.txtTgl.Name = "txtTgl"
        Me.txtTgl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtTgl.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtTgl.Properties.Mask.BeepOnError = True
        Me.txtTgl.Properties.Mask.EditMask = "dd-MM-yyyy"
        Me.txtTgl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.txtTgl.Properties.MinValue = New Date(2020, 1, 1, 0, 0, 0, 0)
        Me.txtTgl.Size = New System.Drawing.Size(107, 20)
        Me.txtTgl.StyleController = Me.LayoutControl1
        Me.txtTgl.TabIndex = 13
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.ckIsBG)
        Me.LayoutControl1.Controls.Add(Me.cbTipeForm)
        Me.LayoutControl1.Controls.Add(Me.txtKodeReff)
        Me.LayoutControl1.Controls.Add(Me.txtIDKasBank)
        Me.LayoutControl1.Controls.Add(Me.txtKode)
        Me.LayoutControl1.Controls.Add(Me.GC1)
        Me.LayoutControl1.Controls.Add(Me.txtKeterangan)
        Me.LayoutControl1.Controls.Add(Me.txtTgl)
        Me.LayoutControl1.Controls.Add(Me.txtJTBG)
        Me.LayoutControl1.Controls.Add(Me.txtNoGiro)
        Me.LayoutControl1.Controls.Add(Me.txtIDRekanan)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.HiddenItems.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7})
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 83)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1022, 516)
        Me.LayoutControl1.TabIndex = 14
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'ckIsBG
        '
        Me.ckIsBG.Location = New System.Drawing.Point(194, 24)
        Me.ckIsBG.MenuManager = Me.mainRibbonControl
        Me.ckIsBG.Name = "ckIsBG"
        Me.ckIsBG.Properties.Caption = "Giro"
        Me.ckIsBG.Size = New System.Drawing.Size(45, 19)
        Me.ckIsBG.StyleController = Me.LayoutControl1
        Me.ckIsBG.TabIndex = 18
        '
        'cbTipeForm
        '
        Me.cbTipeForm.FormattingEnabled = True
        Me.cbTipeForm.Items.AddRange(New Object() {"0 All", "1 Mutasi Kas Bank", "2 Penerimaan Setoran / Pelunasan Piutang"})
        Me.cbTipeForm.Location = New System.Drawing.Point(71, 60)
        Me.cbTipeForm.Name = "cbTipeForm"
        Me.cbTipeForm.Size = New System.Drawing.Size(156, 21)
        Me.cbTipeForm.TabIndex = 16
        '
        'txtKodeReff
        '
        Me.txtKodeReff.Location = New System.Drawing.Point(83, 120)
        Me.txtKodeReff.MenuManager = Me.mainRibbonControl
        Me.txtKodeReff.Name = "txtKodeReff"
        Me.txtKodeReff.Size = New System.Drawing.Size(156, 20)
        Me.txtKodeReff.StyleController = Me.LayoutControl1
        Me.txtKodeReff.TabIndex = 17
        '
        'txtIDKasBank
        '
        Me.txtIDKasBank.Location = New System.Drawing.Point(83, 48)
        Me.txtIDKasBank.MenuManager = Me.mainRibbonControl
        Me.txtIDKasBank.Name = "txtIDKasBank"
        Me.txtIDKasBank.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtIDKasBank.Size = New System.Drawing.Size(156, 20)
        Me.txtIDKasBank.StyleController = Me.LayoutControl1
        Me.txtIDKasBank.TabIndex = 16
        '
        'txtKode
        '
        Me.txtKode.Location = New System.Drawing.Point(83, 72)
        Me.txtKode.MenuManager = Me.mainRibbonControl
        Me.txtKode.Name = "txtKode"
        Me.txtKode.Size = New System.Drawing.Size(156, 20)
        Me.txtKode.StyleController = Me.LayoutControl1
        Me.txtKode.TabIndex = 14
        '
        'txtKeterangan
        '
        Me.txtKeterangan.Location = New System.Drawing.Point(476, 28)
        Me.txtKeterangan.MenuManager = Me.mainRibbonControl
        Me.txtKeterangan.Name = "txtKeterangan"
        Me.txtKeterangan.Size = New System.Drawing.Size(534, 124)
        Me.txtKeterangan.StyleController = Me.LayoutControl1
        Me.txtKeterangan.TabIndex = 15
        '
        'txtJTBG
        '
        Me.txtJTBG.EditValue = New Date(2020, 1, 1, 6, 41, 9, 0)
        Me.txtJTBG.Location = New System.Drawing.Point(267, 80)
        Me.txtJTBG.Name = "txtJTBG"
        Me.txtJTBG.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtJTBG.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtJTBG.Properties.Mask.BeepOnError = True
        Me.txtJTBG.Properties.Mask.EditMask = "dd-MM-yyyy"
        Me.txtJTBG.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.txtJTBG.Properties.MinValue = New Date(2020, 1, 1, 0, 0, 0, 0)
        Me.txtJTBG.Size = New System.Drawing.Size(193, 20)
        Me.txtJTBG.StyleController = Me.LayoutControl1
        Me.txtJTBG.TabIndex = 13
        '
        'txtNoGiro
        '
        Me.txtNoGiro.Location = New System.Drawing.Point(267, 40)
        Me.txtNoGiro.Name = "txtNoGiro"
        Me.txtNoGiro.Size = New System.Drawing.Size(193, 20)
        Me.txtNoGiro.StyleController = Me.LayoutControl1
        Me.txtNoGiro.TabIndex = 14
        '
        'txtIDRekanan
        '
        Me.txtIDRekanan.Location = New System.Drawing.Point(83, 96)
        Me.txtIDRekanan.Name = "txtIDRekanan"
        Me.txtIDRekanan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtIDRekanan.Size = New System.Drawing.Size(156, 20)
        Me.txtIDRekanan.StyleController = Me.LayoutControl1
        Me.txtIDRekanan.TabIndex = 16
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.cbTipeForm
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(219, 25)
        Me.LayoutControlItem7.Text = "Tipe Form"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4, Me.LayoutControlItem3, Me.LayoutControlGroupGiro, Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1022, 516)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.GC1
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 144)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(1002, 352)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtKeterangan
        Me.LayoutControlItem3.Location = New System.Drawing.Point(464, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(538, 144)
        Me.LayoutControlItem3.Text = "Keterangan"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlGroupGiro
        '
        Me.LayoutControlGroupGiro.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem9, Me.LayoutControlItem10})
        Me.LayoutControlGroupGiro.Location = New System.Drawing.Point(243, 0)
        Me.LayoutControlGroupGiro.Name = "LayoutControlGroupGiro"
        Me.LayoutControlGroupGiro.Size = New System.Drawing.Size(221, 144)
        Me.LayoutControlGroupGiro.Text = "Giro"
        Me.LayoutControlGroupGiro.TextVisible = False
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.txtJTBG
        Me.LayoutControlItem9.CustomizationFormText = "Tgl"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 40)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(197, 80)
        Me.LayoutControlItem9.Text = "Tgl JT"
        Me.LayoutControlItem9.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.txtNoGiro
        Me.LayoutControlItem10.CustomizationFormText = "Kode"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(197, 40)
        Me.LayoutControlItem10.Text = "No Giro"
        Me.LayoutControlItem10.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem5, Me.LayoutControlItem1, Me.LayoutControlItem8, Me.LayoutControlItem6, Me.LayoutControlItem11})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(243, 144)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.txtKode
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(219, 24)
        Me.LayoutControlItem2.Text = "Kode"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.txtIDKasBank
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(219, 24)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(219, 24)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(219, 24)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Kas / Bank"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.txtTgl
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(170, 24)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(170, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(170, 24)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Tgl"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.ckIsBG
        Me.LayoutControlItem8.Location = New System.Drawing.Point(170, 0)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(49, 24)
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.txtKodeReff
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(219, 24)
        Me.LayoutControlItem6.Text = "Kode Reff"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.txtIDRekanan
        Me.LayoutControlItem11.CustomizationFormText = "Kas / Bank"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem11.MaxSize = New System.Drawing.Size(219, 24)
        Me.LayoutControlItem11.MinSize = New System.Drawing.Size(219, 24)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(219, 24)
        Me.LayoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem11.Text = "Rekanan"
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(56, 13)
        '
        'DxErrorProvider1
        '
        Me.DxErrorProvider1.ContainerControl = Me
        '
        'PopupMenu1
        '
        Me.PopupMenu1.ItemLinks.Add(Me.BarButtonHapusItem)
        Me.PopupMenu1.Name = "PopupMenu1"
        Me.PopupMenu1.Ribbon = Me.mainRibbonControl
        '
        'BarButtonHapusItem
        '
        Me.BarButtonHapusItem.Caption = "Hapus Item"
        Me.BarButtonHapusItem.Id = 10
        Me.BarButtonHapusItem.ImageOptions.Image = CType(resources.GetObject("BarButtonHapusItem.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonHapusItem.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonHapusItem.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonHapusItem.Name = "BarButtonHapusItem"
        '
        'FormKasBankMasuk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1022, 599)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.mainRibbonControl)
        Me.Name = "FormKasBankMasuk"
        Me.Ribbon = Me.mainRibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Kas Masuk"
        CType(Me.GC1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpAkun, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit1GViewAkun, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpRekanan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit2GViewRekanan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpTransaksi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit3GViewTransaksi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNominal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKurs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mainRibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTgl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.ckIsBG.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKodeReff.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIDKasBank.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKeterangan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtJTBG.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtJTBG.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoGiro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIDRekanan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroupGiro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents GC1 As DevExpress.XtraGrid.GridControl
    Private WithEvents GV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents GColIDAkun As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GCollIDReff As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GColNominal As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents txtNominal As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Private WithEvents GColKurs As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents colId As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents colName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemSearchLookUpAkun As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents RepositoryItemSearchLookUpEdit1GViewAkun As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GColID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnCatatan As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnNoUrut As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents mainRibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Private WithEvents bbiSave As DevExpress.XtraBars.BarButtonItem
    Private WithEvents bbiSaveAndClose As DevExpress.XtraBars.BarButtonItem
    Private WithEvents bbiSaveAndNew As DevExpress.XtraBars.BarButtonItem
    Private WithEvents bbiReset As DevExpress.XtraBars.BarButtonItem
    Private WithEvents bbiDelete As DevExpress.XtraBars.BarButtonItem
    Private WithEvents bbiClose As DevExpress.XtraBars.BarButtonItem
    Private WithEvents mainRibbonPage As DevExpress.XtraBars.Ribbon.RibbonPage
    Private WithEvents mainRibbonPageGroup As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Friend WithEvents txtTgl As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtKode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtKeterangan As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents DxErrorProvider1 As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Friend WithEvents txtIDKasBank As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtKodeReff As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cbTipeForm As ComboBox
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ckIsBG As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroupGiro As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents txtJTBG As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtNoGiro As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GColIDRekanan As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemSearchLookUpRekanan As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents RepositoryItemSearchLookUpEdit2GViewRekanan As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemSearchLookUpTransaksi As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents RepositoryItemSearchLookUpEdit3GViewTransaksi As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtKurs As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents txtIDRekanan As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BarButtonHapusItem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
End Class
