<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormStokMasuk
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormStokMasuk))
        Me.GC1 = New DevExpress.XtraGrid.GridControl()
        Me.GV1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GColID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnNoUrut = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GColIDBarang = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemSearchLookUpEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.RepositoryItemSearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GColNamaBarang = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GColQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtQty = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.GColUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GColHarga = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GColJumlah = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnCatatan = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mainRibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.bbiSave = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiSaveAndClose = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiSaveAndNew = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiReset = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiDelete = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiClose = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonHapusItem = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.mainRibbonPage = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.mainRibbonPageGroup = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.txtTgl = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.txtKode = New DevExpress.XtraEditors.TextEdit()
        Me.txtKeterangan = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.DxErrorProvider1 = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        CType(Me.GC1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mainRibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTgl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTgl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtKode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKeterangan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GC1
        '
        Me.GC1.Location = New System.Drawing.Point(12, 60)
        Me.GC1.MainView = Me.GV1
        Me.GC1.Name = "GC1"
        Me.GC1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.txtQty, Me.RepositoryItemSearchLookUpEdit1})
        Me.GC1.Size = New System.Drawing.Size(776, 295)
        Me.GC1.TabIndex = 1
        Me.GC1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GV1})
        '
        'GV1
        '
        Me.GV1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GColID, Me.GridColumnNoUrut, Me.GColIDBarang, Me.GColNamaBarang, Me.GColQty, Me.GColUnit, Me.GColHarga, Me.GColJumlah, Me.GridColumnCatatan})
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
        'GColIDBarang
        '
        Me.GColIDBarang.AppearanceHeader.Options.UseTextOptions = True
        Me.GColIDBarang.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GColIDBarang.Caption = "Kode Barang"
        Me.GColIDBarang.ColumnEdit = Me.RepositoryItemSearchLookUpEdit1
        Me.GColIDBarang.FieldName = "IDBarang"
        Me.GColIDBarang.Name = "GColIDBarang"
        Me.GColIDBarang.OptionsColumn.AllowMove = False
        Me.GColIDBarang.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "IDBarang", "Total Baris : {0}")})
        Me.GColIDBarang.Visible = True
        Me.GColIDBarang.VisibleIndex = 2
        Me.GColIDBarang.Width = 100
        '
        'RepositoryItemSearchLookUpEdit1
        '
        Me.RepositoryItemSearchLookUpEdit1.AutoHeight = False
        Me.RepositoryItemSearchLookUpEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemSearchLookUpEdit1.Name = "RepositoryItemSearchLookUpEdit1"
        Me.RepositoryItemSearchLookUpEdit1.View = Me.RepositoryItemSearchLookUpEdit1View
        '
        'RepositoryItemSearchLookUpEdit1View
        '
        Me.RepositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemSearchLookUpEdit1View.Name = "RepositoryItemSearchLookUpEdit1View"
        Me.RepositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'GColNamaBarang
        '
        Me.GColNamaBarang.AppearanceHeader.Options.UseTextOptions = True
        Me.GColNamaBarang.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GColNamaBarang.Caption = "Nama Barang"
        Me.GColNamaBarang.FieldName = "NamaBarang"
        Me.GColNamaBarang.Name = "GColNamaBarang"
        Me.GColNamaBarang.OptionsColumn.AllowEdit = False
        Me.GColNamaBarang.OptionsColumn.AllowFocus = False
        Me.GColNamaBarang.OptionsColumn.AllowMove = False
        Me.GColNamaBarang.Visible = True
        Me.GColNamaBarang.VisibleIndex = 3
        Me.GColNamaBarang.Width = 200
        '
        'GColQty
        '
        Me.GColQty.AppearanceHeader.Options.UseTextOptions = True
        Me.GColQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GColQty.Caption = "Qty"
        Me.GColQty.ColumnEdit = Me.txtQty
        Me.GColQty.DisplayFormat.FormatString = "n2"
        Me.GColQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GColQty.FieldName = "Qty"
        Me.GColQty.GroupFormat.FormatString = "n2"
        Me.GColQty.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GColQty.Name = "GColQty"
        Me.GColQty.OptionsColumn.AllowMove = False
        Me.GColQty.Visible = True
        Me.GColQty.VisibleIndex = 4
        Me.GColQty.Width = 70
        '
        'txtQty
        '
        Me.txtQty.AutoHeight = False
        Me.txtQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtQty.DisplayFormat.FormatString = "n2"
        Me.txtQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtQty.EditFormat.FormatString = "n2"
        Me.txtQty.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtQty.Mask.UseMaskAsDisplayFormat = True
        Me.txtQty.Name = "txtQty"
        '
        'GColUnit
        '
        Me.GColUnit.AppearanceHeader.Options.UseTextOptions = True
        Me.GColUnit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GColUnit.Caption = "Unit"
        Me.GColUnit.FieldName = "Unit"
        Me.GColUnit.Name = "GColUnit"
        Me.GColUnit.OptionsColumn.AllowEdit = False
        Me.GColUnit.OptionsColumn.AllowFocus = False
        Me.GColUnit.OptionsColumn.AllowMove = False
        Me.GColUnit.Visible = True
        Me.GColUnit.VisibleIndex = 5
        Me.GColUnit.Width = 50
        '
        'GColHarga
        '
        Me.GColHarga.AppearanceHeader.Options.UseTextOptions = True
        Me.GColHarga.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GColHarga.Caption = "Harga"
        Me.GColHarga.DisplayFormat.FormatString = "#,#"
        Me.GColHarga.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GColHarga.FieldName = "Harga"
        Me.GColHarga.Name = "GColHarga"
        Me.GColHarga.OptionsColumn.AllowEdit = False
        Me.GColHarga.OptionsColumn.AllowFocus = False
        Me.GColHarga.Width = 120
        '
        'GColJumlah
        '
        Me.GColJumlah.AppearanceHeader.Options.UseTextOptions = True
        Me.GColJumlah.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GColJumlah.Caption = "Jumlah"
        Me.GColJumlah.DisplayFormat.FormatString = "#,#"
        Me.GColJumlah.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GColJumlah.FieldName = "Jumlah"
        Me.GColJumlah.Name = "GColJumlah"
        Me.GColJumlah.OptionsColumn.AllowEdit = False
        Me.GColJumlah.OptionsColumn.AllowFocus = False
        Me.GColJumlah.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Jumlah", "Jumlah ={0:#,#}")})
        Me.GColJumlah.Width = 150
        '
        'GridColumnCatatan
        '
        Me.GridColumnCatatan.Caption = "Catatan"
        Me.GridColumnCatatan.FieldName = "Catatan"
        Me.GridColumnCatatan.Name = "GridColumnCatatan"
        Me.GridColumnCatatan.Visible = True
        Me.GridColumnCatatan.VisibleIndex = 6
        Me.GridColumnCatatan.Width = 199
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
        Me.mainRibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.mainRibbonControl.ExpandCollapseItem, Me.bbiSave, Me.bbiSaveAndClose, Me.bbiSaveAndNew, Me.bbiReset, Me.bbiDelete, Me.bbiClose, Me.BarButtonHapusItem, Me.BarButtonItem2})
        Me.mainRibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.mainRibbonControl.MaxItemId = 12
        Me.mainRibbonControl.Name = "mainRibbonControl"
        Me.mainRibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.mainRibbonPage})
        Me.mainRibbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.OfficeUniversal
        Me.mainRibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.mainRibbonControl.Size = New System.Drawing.Size(800, 83)
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
        'BarButtonHapusItem
        '
        Me.BarButtonHapusItem.Caption = "+ Barang Baru"
        Me.BarButtonHapusItem.Id = 10
        Me.BarButtonHapusItem.ImageOptions.Image = CType(resources.GetObject("BarButtonHapusItem.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonHapusItem.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonHapusItem.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonHapusItem.Name = "BarButtonHapusItem"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Hapus Item"
        Me.BarButtonItem2.Id = 11
        Me.BarButtonItem2.ImageOptions.Image = CType(resources.GetObject("BarButtonItem2.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem2.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem2.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem2.Name = "BarButtonItem2"
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
        Me.mainRibbonPageGroup.ItemLinks.Add(Me.BarButtonHapusItem)
        Me.mainRibbonPageGroup.Name = "mainRibbonPageGroup"
        Me.mainRibbonPageGroup.ShowCaptionButton = False
        Me.mainRibbonPageGroup.Text = "Tasks"
        '
        'txtTgl
        '
        Me.txtTgl.EditValue = New Date(2020, 1, 1, 6, 41, 9, 0)
        Me.txtTgl.Location = New System.Drawing.Point(71, 12)
        Me.txtTgl.MenuManager = Me.mainRibbonControl
        Me.txtTgl.Name = "txtTgl"
        Me.txtTgl.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtTgl.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtTgl.Properties.Mask.BeepOnError = True
        Me.txtTgl.Properties.Mask.EditMask = "dd-MM-yyyy"
        Me.txtTgl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.txtTgl.Properties.MinValue = New Date(2020, 1, 1, 0, 0, 0, 0)
        Me.txtTgl.Size = New System.Drawing.Size(155, 20)
        Me.txtTgl.StyleController = Me.LayoutControl1
        Me.txtTgl.TabIndex = 13
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.txtKode)
        Me.LayoutControl1.Controls.Add(Me.GC1)
        Me.LayoutControl1.Controls.Add(Me.txtKeterangan)
        Me.LayoutControl1.Controls.Add(Me.txtTgl)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 83)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(800, 367)
        Me.LayoutControl1.TabIndex = 14
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'txtKode
        '
        Me.txtKode.Location = New System.Drawing.Point(71, 36)
        Me.txtKode.MenuManager = Me.mainRibbonControl
        Me.txtKode.Name = "txtKode"
        Me.txtKode.Size = New System.Drawing.Size(155, 20)
        Me.txtKode.StyleController = Me.LayoutControl1
        Me.txtKode.TabIndex = 14
        '
        'txtKeterangan
        '
        Me.txtKeterangan.Location = New System.Drawing.Point(533, 28)
        Me.txtKeterangan.MenuManager = Me.mainRibbonControl
        Me.txtKeterangan.Name = "txtKeterangan"
        Me.txtKeterangan.Size = New System.Drawing.Size(255, 28)
        Me.txtKeterangan.StyleController = Me.LayoutControl1
        Me.txtKeterangan.TabIndex = 15
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.EmptySpaceItem1, Me.LayoutControlItem4, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(800, 367)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.txtTgl
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(218, 24)
        Me.LayoutControlItem1.Text = "Tgl"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.txtKode
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(218, 24)
        Me.LayoutControlItem2.Text = "Kode"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(56, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(218, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(303, 48)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.GC1
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(780, 299)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtKeterangan
        Me.LayoutControlItem3.Location = New System.Drawing.Point(521, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(259, 48)
        Me.LayoutControlItem3.Text = "Keterangan"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(56, 13)
        '
        'DxErrorProvider1
        '
        Me.DxErrorProvider1.ContainerControl = Me
        '
        'PopupMenu1
        '
        Me.PopupMenu1.ItemLinks.Add(Me.BarButtonItem2)
        Me.PopupMenu1.Name = "PopupMenu1"
        Me.PopupMenu1.Ribbon = Me.mainRibbonControl
        '
        'FormStokMasuk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.mainRibbonControl)
        Me.Name = "FormStokMasuk"
        Me.Ribbon = Me.mainRibbonControl
        Me.Text = "Stok Masuk"
        CType(Me.GC1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mainRibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTgl.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTgl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtKode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKeterangan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents GC1 As DevExpress.XtraGrid.GridControl
    Private WithEvents GV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents GColIDBarang As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GColUnit As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GColQty As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents txtQty As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Private WithEvents GColHarga As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents GColJumlah As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents colId As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents colName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemSearchLookUpEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents RepositoryItemSearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GColNamaBarang As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents txtKode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtKeterangan As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents DxErrorProvider1 As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
    Friend WithEvents BarButtonHapusItem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
End Class
