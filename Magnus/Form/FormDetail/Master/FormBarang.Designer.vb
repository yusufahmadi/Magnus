<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class FormBarang
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Windows Form Designer generated code"

    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.dataLayoutControl1 = New DevExpress.XtraDataLayout.DataLayoutControl()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn0 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.repo_cbIDSatuan = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mainRibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.bbiSave = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiSaveAndClose = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiSaveAndNew = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiReset = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiDelete = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiClose = New DevExpress.XtraBars.BarButtonItem()
        Me.mainRibbonPage = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.mainRibbonPageGroup = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.txtTanggalUbah = New DevExpress.XtraEditors.DateEdit()
        Me.txtTanggalBuat = New DevExpress.XtraEditors.DateEdit()
        Me.txtUserUbah = New DevExpress.XtraEditors.TextEdit()
        Me.txtUserBuat = New DevExpress.XtraEditors.TextEdit()
        Me.txtIDSatuanTerkecil = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtHargaJual = New DevExpress.XtraEditors.TextEdit()
        Me.txtProsenUp = New DevExpress.XtraEditors.TextEdit()
        Me.txtIsi = New DevExpress.XtraEditors.TextEdit()
        Me.txtHargaBeli = New DevExpress.XtraEditors.TextEdit()
        Me.txtIDSatuanTerbesar = New DevExpress.XtraEditors.LookUpEdit()
        Me.ckIsNonStok = New DevExpress.XtraEditors.CheckEdit()
        Me.ckIsActive = New DevExpress.XtraEditors.CheckEdit()
        Me.txtIDKategori = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtNama = New DevExpress.XtraEditors.TextEdit()
        Me.txtKode = New DevExpress.XtraEditors.TextEdit()
        Me.txtP = New DevExpress.XtraEditors.TextEdit()
        Me.txtL = New DevExpress.XtraEditors.TextEdit()
        Me.txtT = New DevExpress.XtraEditors.TextEdit()
        Me.txtCatatan = New DevExpress.XtraEditors.MemoEdit()
        Me.layoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.layoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItemIsActive = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItemIsNonStock = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItemP = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItemL = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItemT = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItemIDSatuanTerbesar = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItemHargaBeli = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItemIsi = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItemIDSatuanTerkecil = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItemUserBuat = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItemUserUbah = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItemTanggalBuat = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItemTanggalUbah = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItemGVD = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItemProsenUp = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItemHargaJual = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.dataLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dataLayoutControl1.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repo_cbIDSatuan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mainRibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTanggalUbah.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTanggalUbah.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTanggalBuat.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTanggalBuat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUserUbah.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUserBuat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIDSatuanTerkecil.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHargaJual.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProsenUp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIsi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHargaBeli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIDSatuanTerbesar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ckIsNonStok.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ckIsActive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIDKategori.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCatatan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItemIsActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItemIsNonStock, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItemP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItemL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItemT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItemIDSatuanTerbesar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItemHargaBeli, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItemIsi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItemIDSatuanTerkecil, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItemUserBuat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItemUserUbah, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItemTanggalBuat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItemTanggalUbah, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItemGVD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItemProsenUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItemHargaJual, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dataLayoutControl1
        '
        Me.dataLayoutControl1.AllowCustomization = False
        Me.dataLayoutControl1.Controls.Add(Me.GridControl1)
        Me.dataLayoutControl1.Controls.Add(Me.txtTanggalUbah)
        Me.dataLayoutControl1.Controls.Add(Me.txtTanggalBuat)
        Me.dataLayoutControl1.Controls.Add(Me.txtUserUbah)
        Me.dataLayoutControl1.Controls.Add(Me.txtUserBuat)
        Me.dataLayoutControl1.Controls.Add(Me.txtIDSatuanTerkecil)
        Me.dataLayoutControl1.Controls.Add(Me.txtHargaJual)
        Me.dataLayoutControl1.Controls.Add(Me.txtProsenUp)
        Me.dataLayoutControl1.Controls.Add(Me.txtIsi)
        Me.dataLayoutControl1.Controls.Add(Me.txtHargaBeli)
        Me.dataLayoutControl1.Controls.Add(Me.txtIDSatuanTerbesar)
        Me.dataLayoutControl1.Controls.Add(Me.ckIsNonStok)
        Me.dataLayoutControl1.Controls.Add(Me.ckIsActive)
        Me.dataLayoutControl1.Controls.Add(Me.txtIDKategori)
        Me.dataLayoutControl1.Controls.Add(Me.txtNama)
        Me.dataLayoutControl1.Controls.Add(Me.txtKode)
        Me.dataLayoutControl1.Controls.Add(Me.txtP)
        Me.dataLayoutControl1.Controls.Add(Me.txtL)
        Me.dataLayoutControl1.Controls.Add(Me.txtT)
        Me.dataLayoutControl1.Controls.Add(Me.txtCatatan)
        Me.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dataLayoutControl1.Location = New System.Drawing.Point(0, 146)
        Me.dataLayoutControl1.Name = "dataLayoutControl1"
        Me.dataLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(701, 596, 810, 400)
        Me.dataLayoutControl1.Root = Me.layoutControlGroup1
        Me.dataLayoutControl1.Size = New System.Drawing.Size(887, 350)
        Me.dataLayoutControl1.TabIndex = 0
        '
        'GridControl1
        '
        Me.GridControl1.Location = New System.Drawing.Point(451, 28)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.MenuManager = Me.mainRibbonControl
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.repo_cbIDSatuan})
        Me.GridControl1.Size = New System.Drawing.Size(424, 310)
        Me.GridControl1.TabIndex = 22
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn0, Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn0
        '
        Me.GridColumn0.Caption = "ID"
        Me.GridColumn0.FieldName = "ID"
        Me.GridColumn0.Name = "GridColumn0"
        Me.GridColumn0.OptionsColumn.AllowEdit = False
        Me.GridColumn0.OptionsColumn.AllowMove = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "IDBarang"
        Me.GridColumn1.FieldName = "IDBarang"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowMove = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Barcode"
        Me.GridColumn2.FieldName = "Barcode"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowMove = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 101
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Satuan"
        Me.GridColumn3.ColumnEdit = Me.repo_cbIDSatuan
        Me.GridColumn3.FieldName = "IDSatuan"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowMove = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 108
        '
        'repo_cbIDSatuan
        '
        Me.repo_cbIDSatuan.AutoHeight = False
        Me.repo_cbIDSatuan.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.repo_cbIDSatuan.Name = "repo_cbIDSatuan"
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Isi"
        Me.GridColumn4.FieldName = "Isi"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowMove = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        Me.GridColumn4.Width = 63
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "ProsenUp"
        Me.GridColumn5.FieldName = "ProsenUp"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 3
        Me.GridColumn5.Width = 59
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "HargaJual"
        Me.GridColumn6.FieldName = "HargaJual"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 4
        Me.GridColumn6.Width = 90
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "UserBuat"
        Me.GridColumn7.FieldName = "UserBuat"
        Me.GridColumn7.Name = "GridColumn7"
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "UserUpdate"
        Me.GridColumn8.FieldName = "UserUpdate"
        Me.GridColumn8.Name = "GridColumn8"
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "TanggalBuat"
        Me.GridColumn9.FieldName = "TanggalBuat"
        Me.GridColumn9.Name = "GridColumn9"
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "TanggalUpdate"
        Me.GridColumn10.FieldName = "TanggalUpdate"
        Me.GridColumn10.Name = "GridColumn10"
        '
        'mainRibbonControl
        '
        Me.mainRibbonControl.ExpandCollapseItem.Id = 0
        Me.mainRibbonControl.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.mainRibbonControl.ExpandCollapseItem, Me.bbiSave, Me.bbiSaveAndClose, Me.bbiSaveAndNew, Me.bbiReset, Me.bbiDelete, Me.bbiClose})
        Me.mainRibbonControl.Location = New System.Drawing.Point(0, 0)
        Me.mainRibbonControl.MaxItemId = 10
        Me.mainRibbonControl.Name = "mainRibbonControl"
        Me.mainRibbonControl.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.mainRibbonPage})
        Me.mainRibbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013
        Me.mainRibbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.[False]
        Me.mainRibbonControl.Size = New System.Drawing.Size(887, 146)
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
        'txtTanggalUbah
        '
        Me.txtTanggalUbah.EditValue = Nothing
        Me.txtTanggalUbah.Location = New System.Drawing.Point(321, 318)
        Me.txtTanggalUbah.MenuManager = Me.mainRibbonControl
        Me.txtTanggalUbah.Name = "txtTanggalUbah"
        Me.txtTanggalUbah.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtTanggalUbah.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtTanggalUbah.Properties.ReadOnly = True
        Me.txtTanggalUbah.Size = New System.Drawing.Size(126, 20)
        Me.txtTanggalUbah.StyleController = Me.dataLayoutControl1
        Me.txtTanggalUbah.TabIndex = 21
        '
        'txtTanggalBuat
        '
        Me.txtTanggalBuat.EditValue = Nothing
        Me.txtTanggalBuat.Location = New System.Drawing.Point(95, 318)
        Me.txtTanggalBuat.MenuManager = Me.mainRibbonControl
        Me.txtTanggalBuat.Name = "txtTanggalBuat"
        Me.txtTanggalBuat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtTanggalBuat.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtTanggalBuat.Properties.ReadOnly = True
        Me.txtTanggalBuat.Size = New System.Drawing.Size(139, 20)
        Me.txtTanggalBuat.StyleController = Me.dataLayoutControl1
        Me.txtTanggalBuat.TabIndex = 20
        '
        'txtUserUbah
        '
        Me.txtUserUbah.Location = New System.Drawing.Point(321, 294)
        Me.txtUserUbah.Name = "txtUserUbah"
        Me.txtUserUbah.Properties.ReadOnly = True
        Me.txtUserUbah.Size = New System.Drawing.Size(126, 20)
        Me.txtUserUbah.StyleController = Me.dataLayoutControl1
        Me.txtUserUbah.TabIndex = 19
        '
        'txtUserBuat
        '
        Me.txtUserBuat.Location = New System.Drawing.Point(95, 294)
        Me.txtUserBuat.Name = "txtUserBuat"
        Me.txtUserBuat.Properties.ReadOnly = True
        Me.txtUserBuat.Size = New System.Drawing.Size(139, 20)
        Me.txtUserBuat.StyleController = Me.dataLayoutControl1
        Me.txtUserBuat.TabIndex = 18
        '
        'txtIDSatuanTerkecil
        '
        Me.txtIDSatuanTerkecil.Location = New System.Drawing.Point(395, 192)
        Me.txtIDSatuanTerkecil.MenuManager = Me.mainRibbonControl
        Me.txtIDSatuanTerkecil.Name = "txtIDSatuanTerkecil"
        Me.txtIDSatuanTerkecil.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtIDSatuanTerkecil.Properties.NullText = ""
        Me.txtIDSatuanTerkecil.Size = New System.Drawing.Size(52, 20)
        Me.txtIDSatuanTerkecil.StyleController = Me.dataLayoutControl1
        Me.txtIDSatuanTerkecil.TabIndex = 17
        '
        'txtHargaJual
        '
        Me.txtHargaJual.EditValue = "0.00"
        Me.txtHargaJual.Location = New System.Drawing.Point(257, 240)
        Me.txtHargaJual.Name = "txtHargaJual"
        Me.txtHargaJual.Properties.Mask.EditMask = "n2"
        Me.txtHargaJual.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtHargaJual.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtHargaJual.Size = New System.Drawing.Size(190, 20)
        Me.txtHargaJual.StyleController = Me.dataLayoutControl1
        Me.txtHargaJual.TabIndex = 16
        '
        'txtProsenUp
        '
        Me.txtProsenUp.EditValue = "0.00"
        Me.txtProsenUp.Location = New System.Drawing.Point(95, 240)
        Me.txtProsenUp.Name = "txtProsenUp"
        Me.txtProsenUp.Properties.Mask.EditMask = "n2"
        Me.txtProsenUp.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtProsenUp.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtProsenUp.Size = New System.Drawing.Size(75, 20)
        Me.txtProsenUp.StyleController = Me.dataLayoutControl1
        Me.txtProsenUp.TabIndex = 15
        '
        'txtIsi
        '
        Me.txtIsi.EditValue = "1"
        Me.txtIsi.Location = New System.Drawing.Point(257, 192)
        Me.txtIsi.Name = "txtIsi"
        Me.txtIsi.Properties.Mask.EditMask = "n0"
        Me.txtIsi.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtIsi.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtIsi.Size = New System.Drawing.Size(51, 20)
        Me.txtIsi.StyleController = Me.dataLayoutControl1
        Me.txtIsi.TabIndex = 14
        '
        'txtHargaBeli
        '
        Me.txtHargaBeli.EditValue = "0.00"
        Me.txtHargaBeli.Location = New System.Drawing.Point(95, 216)
        Me.txtHargaBeli.Name = "txtHargaBeli"
        Me.txtHargaBeli.Properties.Mask.EditMask = "n4"
        Me.txtHargaBeli.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtHargaBeli.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtHargaBeli.Size = New System.Drawing.Size(352, 20)
        Me.txtHargaBeli.StyleController = Me.dataLayoutControl1
        Me.txtHargaBeli.TabIndex = 13
        '
        'txtIDSatuanTerbesar
        '
        Me.txtIDSatuanTerbesar.Location = New System.Drawing.Point(95, 192)
        Me.txtIDSatuanTerbesar.MenuManager = Me.mainRibbonControl
        Me.txtIDSatuanTerbesar.Name = "txtIDSatuanTerbesar"
        Me.txtIDSatuanTerbesar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtIDSatuanTerbesar.Properties.NullText = ""
        Me.txtIDSatuanTerbesar.Size = New System.Drawing.Size(75, 20)
        Me.txtIDSatuanTerbesar.StyleController = Me.dataLayoutControl1
        Me.txtIDSatuanTerbesar.TabIndex = 12
        '
        'ckIsNonStok
        '
        Me.ckIsNonStok.Location = New System.Drawing.Point(206, 169)
        Me.ckIsNonStok.MenuManager = Me.mainRibbonControl
        Me.ckIsNonStok.Name = "ckIsNonStok"
        Me.ckIsNonStok.Properties.Caption = "Non Stok"
        Me.ckIsNonStok.Size = New System.Drawing.Size(241, 19)
        Me.ckIsNonStok.StyleController = Me.dataLayoutControl1
        Me.ckIsNonStok.TabIndex = 11
        '
        'ckIsActive
        '
        Me.ckIsActive.Location = New System.Drawing.Point(12, 169)
        Me.ckIsActive.MenuManager = Me.mainRibbonControl
        Me.ckIsActive.Name = "ckIsActive"
        Me.ckIsActive.Properties.Caption = "Aktif"
        Me.ckIsActive.Size = New System.Drawing.Size(190, 19)
        Me.ckIsActive.StyleController = Me.dataLayoutControl1
        Me.ckIsActive.TabIndex = 9
        '
        'txtIDKategori
        '
        Me.txtIDKategori.Location = New System.Drawing.Point(95, 12)
        Me.txtIDKategori.MenuManager = Me.mainRibbonControl
        Me.txtIDKategori.Name = "txtIDKategori"
        Me.txtIDKategori.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtIDKategori.Properties.NullText = ""
        Me.txtIDKategori.Size = New System.Drawing.Size(352, 20)
        Me.txtIDKategori.StyleController = Me.dataLayoutControl1
        Me.txtIDKategori.TabIndex = 7
        '
        'txtNama
        '
        Me.txtNama.Location = New System.Drawing.Point(95, 60)
        Me.txtNama.MenuManager = Me.mainRibbonControl
        Me.txtNama.Name = "txtNama"
        Me.txtNama.Properties.MaxLength = 50
        Me.txtNama.Size = New System.Drawing.Size(352, 20)
        Me.txtNama.StyleController = Me.dataLayoutControl1
        Me.txtNama.TabIndex = 5
        '
        'txtKode
        '
        Me.txtKode.Location = New System.Drawing.Point(95, 36)
        Me.txtKode.MenuManager = Me.mainRibbonControl
        Me.txtKode.Name = "txtKode"
        Me.txtKode.Properties.MaxLength = 13
        Me.txtKode.Size = New System.Drawing.Size(352, 20)
        Me.txtKode.StyleController = Me.dataLayoutControl1
        Me.txtKode.TabIndex = 4
        '
        'txtP
        '
        Me.txtP.Location = New System.Drawing.Point(95, 145)
        Me.txtP.Name = "txtP"
        Me.txtP.Size = New System.Drawing.Size(75, 20)
        Me.txtP.StyleController = Me.dataLayoutControl1
        Me.txtP.TabIndex = 4
        '
        'txtL
        '
        Me.txtL.Location = New System.Drawing.Point(257, 145)
        Me.txtL.Name = "txtL"
        Me.txtL.Size = New System.Drawing.Size(51, 20)
        Me.txtL.StyleController = Me.dataLayoutControl1
        Me.txtL.TabIndex = 5
        '
        'txtT
        '
        Me.txtT.Location = New System.Drawing.Point(395, 145)
        Me.txtT.Name = "txtT"
        Me.txtT.Size = New System.Drawing.Size(52, 20)
        Me.txtT.StyleController = Me.dataLayoutControl1
        Me.txtT.TabIndex = 6
        '
        'txtCatatan
        '
        Me.txtCatatan.Location = New System.Drawing.Point(95, 84)
        Me.txtCatatan.MenuManager = Me.mainRibbonControl
        Me.txtCatatan.Name = "txtCatatan"
        Me.txtCatatan.Properties.MaxLength = 250
        Me.txtCatatan.Size = New System.Drawing.Size(352, 57)
        Me.txtCatatan.StyleController = Me.dataLayoutControl1
        Me.txtCatatan.TabIndex = 10
        '
        'layoutControlGroup1
        '
        Me.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.layoutControlGroup1.GroupBordersVisible = False
        Me.layoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.layoutControlGroup2})
        Me.layoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.layoutControlGroup1.Name = "Root"
        Me.layoutControlGroup1.Size = New System.Drawing.Size(887, 350)
        Me.layoutControlGroup1.TextVisible = False
        '
        'layoutControlGroup2
        '
        Me.layoutControlGroup2.AllowDrawBackground = False
        Me.layoutControlGroup2.GroupBordersVisible = False
        Me.layoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem1, Me.LayoutControlItemIsActive, Me.LayoutControlItem5, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItemIsNonStock, Me.LayoutControlItem4, Me.LayoutControlItemP, Me.LayoutControlItemL, Me.LayoutControlItemT, Me.LayoutControlItemIDSatuanTerbesar, Me.LayoutControlItemHargaBeli, Me.LayoutControlItemIsi, Me.LayoutControlItemIDSatuanTerkecil, Me.LayoutControlItemUserBuat, Me.LayoutControlItemUserUbah, Me.LayoutControlItemTanggalBuat, Me.LayoutControlItemTanggalUbah, Me.LayoutControlItemGVD, Me.LayoutControlItemProsenUp, Me.LayoutControlItemHargaJual})
        Me.layoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.layoutControlGroup2.Name = "autoGeneratedGroup0"
        Me.layoutControlGroup2.Size = New System.Drawing.Size(867, 330)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 252)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(439, 30)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItemIsActive
        '
        Me.LayoutControlItemIsActive.Control = Me.ckIsActive
        Me.LayoutControlItemIsActive.Location = New System.Drawing.Point(0, 157)
        Me.LayoutControlItemIsActive.Name = "LayoutControlItemIsActive"
        Me.LayoutControlItemIsActive.Size = New System.Drawing.Size(194, 23)
        Me.LayoutControlItemIsActive.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItemIsActive.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.txtCatatan
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(439, 61)
        Me.LayoutControlItem5.Text = "Catatan"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.txtKode
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(439, 24)
        Me.LayoutControlItem1.Text = "Kode"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.txtNama
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(439, 24)
        Me.LayoutControlItem2.Text = "Nama"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItemIsNonStock
        '
        Me.LayoutControlItemIsNonStock.Control = Me.ckIsNonStok
        Me.LayoutControlItemIsNonStock.Location = New System.Drawing.Point(194, 157)
        Me.LayoutControlItemIsNonStock.Name = "LayoutControlItemIsNonStock"
        Me.LayoutControlItemIsNonStock.Size = New System.Drawing.Size(245, 23)
        Me.LayoutControlItemIsNonStock.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItemIsNonStock.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.txtIDKategori
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(439, 24)
        Me.LayoutControlItem4.Text = "Kategori"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItemP
        '
        Me.LayoutControlItemP.Control = Me.txtP
        Me.LayoutControlItemP.CustomizationFormText = "layoutControlItem1"
        Me.LayoutControlItemP.Location = New System.Drawing.Point(0, 133)
        Me.LayoutControlItemP.Name = "LayoutControlItemP"
        Me.LayoutControlItemP.Size = New System.Drawing.Size(162, 24)
        Me.LayoutControlItemP.Text = "P"
        Me.LayoutControlItemP.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItemP.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItemL
        '
        Me.LayoutControlItemL.Control = Me.txtL
        Me.LayoutControlItemL.CustomizationFormText = "layoutControlItem2"
        Me.LayoutControlItemL.Location = New System.Drawing.Point(162, 133)
        Me.LayoutControlItemL.Name = "LayoutControlItemL"
        Me.LayoutControlItemL.Size = New System.Drawing.Size(138, 24)
        Me.LayoutControlItemL.Text = "L"
        Me.LayoutControlItemL.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItemL.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItemT
        '
        Me.LayoutControlItemT.Control = Me.txtT
        Me.LayoutControlItemT.CustomizationFormText = "layoutControlItem3"
        Me.LayoutControlItemT.Location = New System.Drawing.Point(300, 133)
        Me.LayoutControlItemT.Name = "LayoutControlItemT"
        Me.LayoutControlItemT.Size = New System.Drawing.Size(139, 24)
        Me.LayoutControlItemT.Text = "T"
        Me.LayoutControlItemT.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItemT.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItemIDSatuanTerbesar
        '
        Me.LayoutControlItemIDSatuanTerbesar.Control = Me.txtIDSatuanTerbesar
        Me.LayoutControlItemIDSatuanTerbesar.Location = New System.Drawing.Point(0, 180)
        Me.LayoutControlItemIDSatuanTerbesar.Name = "LayoutControlItemIDSatuanTerbesar"
        Me.LayoutControlItemIDSatuanTerbesar.Size = New System.Drawing.Size(162, 24)
        Me.LayoutControlItemIDSatuanTerbesar.Text = "Satuan Terbesar"
        Me.LayoutControlItemIDSatuanTerbesar.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItemHargaBeli
        '
        Me.LayoutControlItemHargaBeli.Control = Me.txtHargaBeli
        Me.LayoutControlItemHargaBeli.Location = New System.Drawing.Point(0, 204)
        Me.LayoutControlItemHargaBeli.Name = "LayoutControlItemHargaBeli"
        Me.LayoutControlItemHargaBeli.Size = New System.Drawing.Size(439, 24)
        Me.LayoutControlItemHargaBeli.Text = "HargaBeli"
        Me.LayoutControlItemHargaBeli.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItemIsi
        '
        Me.LayoutControlItemIsi.Control = Me.txtIsi
        Me.LayoutControlItemIsi.Location = New System.Drawing.Point(162, 180)
        Me.LayoutControlItemIsi.Name = "LayoutControlItemIsi"
        Me.LayoutControlItemIsi.Size = New System.Drawing.Size(138, 24)
        Me.LayoutControlItemIsi.Text = "Isi"
        Me.LayoutControlItemIsi.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItemIDSatuanTerkecil
        '
        Me.LayoutControlItemIDSatuanTerkecil.Control = Me.txtIDSatuanTerkecil
        Me.LayoutControlItemIDSatuanTerkecil.Location = New System.Drawing.Point(300, 180)
        Me.LayoutControlItemIDSatuanTerkecil.Name = "LayoutControlItemIDSatuanTerkecil"
        Me.LayoutControlItemIDSatuanTerkecil.Size = New System.Drawing.Size(139, 24)
        Me.LayoutControlItemIDSatuanTerkecil.Text = "Satuan"
        Me.LayoutControlItemIDSatuanTerkecil.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItemUserBuat
        '
        Me.LayoutControlItemUserBuat.Control = Me.txtUserBuat
        Me.LayoutControlItemUserBuat.Location = New System.Drawing.Point(0, 282)
        Me.LayoutControlItemUserBuat.Name = "LayoutControlItemUserBuat"
        Me.LayoutControlItemUserBuat.Size = New System.Drawing.Size(226, 24)
        Me.LayoutControlItemUserBuat.Text = "User Buat"
        Me.LayoutControlItemUserBuat.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItemUserUbah
        '
        Me.LayoutControlItemUserUbah.Control = Me.txtUserUbah
        Me.LayoutControlItemUserUbah.Location = New System.Drawing.Point(226, 282)
        Me.LayoutControlItemUserUbah.Name = "LayoutControlItemUserUbah"
        Me.LayoutControlItemUserUbah.Size = New System.Drawing.Size(213, 24)
        Me.LayoutControlItemUserUbah.Text = "User Ubah"
        Me.LayoutControlItemUserUbah.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItemTanggalBuat
        '
        Me.LayoutControlItemTanggalBuat.Control = Me.txtTanggalBuat
        Me.LayoutControlItemTanggalBuat.Location = New System.Drawing.Point(0, 306)
        Me.LayoutControlItemTanggalBuat.Name = "LayoutControlItemTanggalBuat"
        Me.LayoutControlItemTanggalBuat.Size = New System.Drawing.Size(226, 24)
        Me.LayoutControlItemTanggalBuat.Text = "Tanggal Buat"
        Me.LayoutControlItemTanggalBuat.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItemTanggalUbah
        '
        Me.LayoutControlItemTanggalUbah.Control = Me.txtTanggalUbah
        Me.LayoutControlItemTanggalUbah.Location = New System.Drawing.Point(226, 306)
        Me.LayoutControlItemTanggalUbah.Name = "LayoutControlItemTanggalUbah"
        Me.LayoutControlItemTanggalUbah.Size = New System.Drawing.Size(213, 24)
        Me.LayoutControlItemTanggalUbah.Text = "Tanggal Ubah"
        Me.LayoutControlItemTanggalUbah.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItemGVD
        '
        Me.LayoutControlItemGVD.Control = Me.GridControl1
        Me.LayoutControlItemGVD.Location = New System.Drawing.Point(439, 0)
        Me.LayoutControlItemGVD.Name = "LayoutControlItemGVD"
        Me.LayoutControlItemGVD.Size = New System.Drawing.Size(428, 330)
        Me.LayoutControlItemGVD.Text = "Detail"
        Me.LayoutControlItemGVD.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItemGVD.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItemProsenUp
        '
        Me.LayoutControlItemProsenUp.Control = Me.txtProsenUp
        Me.LayoutControlItemProsenUp.Location = New System.Drawing.Point(0, 228)
        Me.LayoutControlItemProsenUp.Name = "LayoutControlItemProsenUp"
        Me.LayoutControlItemProsenUp.Size = New System.Drawing.Size(162, 24)
        Me.LayoutControlItemProsenUp.Text = "Up"
        Me.LayoutControlItemProsenUp.TextSize = New System.Drawing.Size(80, 13)
        '
        'LayoutControlItemHargaJual
        '
        Me.LayoutControlItemHargaJual.Control = Me.txtHargaJual
        Me.LayoutControlItemHargaJual.Location = New System.Drawing.Point(162, 228)
        Me.LayoutControlItemHargaJual.Name = "LayoutControlItemHargaJual"
        Me.LayoutControlItemHargaJual.Size = New System.Drawing.Size(277, 24)
        Me.LayoutControlItemHargaJual.Text = "HargaJual"
        Me.LayoutControlItemHargaJual.TextSize = New System.Drawing.Size(80, 13)
        '
        'FormBarang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(887, 496)
        Me.Controls.Add(Me.dataLayoutControl1)
        Me.Controls.Add(Me.mainRibbonControl)
        Me.Name = "FormBarang"
        Me.Ribbon = Me.mainRibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Barang"
        CType(Me.dataLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dataLayoutControl1.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repo_cbIDSatuan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mainRibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTanggalUbah.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTanggalUbah.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTanggalBuat.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTanggalBuat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUserUbah.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUserBuat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIDSatuanTerkecil.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHargaJual.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProsenUp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIsi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHargaBeli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIDSatuanTerbesar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ckIsNonStok.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ckIsActive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIDKategori.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCatatan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItemIsActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItemIsNonStock, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItemP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItemL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItemT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItemIDSatuanTerbesar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItemHargaBeli, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItemIsi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItemIDSatuanTerkecil, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItemUserBuat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItemUserUbah, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItemTanggalBuat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItemTanggalUbah, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItemGVD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItemProsenUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItemHargaJual, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Private WithEvents dataLayoutControl1 As DevExpress.XtraDataLayout.DataLayoutControl
    Private WithEvents layoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Private WithEvents mainRibbonControl As DevExpress.XtraBars.Ribbon.RibbonControl
    Private WithEvents mainRibbonPage As DevExpress.XtraBars.Ribbon.RibbonPage
    Private WithEvents mainRibbonPageGroup As DevExpress.XtraBars.Ribbon.RibbonPageGroup
    Private WithEvents bbiSave As DevExpress.XtraBars.BarButtonItem
    Private WithEvents bbiSaveAndClose As DevExpress.XtraBars.BarButtonItem
    Private WithEvents bbiSaveAndNew As DevExpress.XtraBars.BarButtonItem
    Private WithEvents bbiReset As DevExpress.XtraBars.BarButtonItem
    Private WithEvents bbiDelete As DevExpress.XtraBars.BarButtonItem
    Private WithEvents bbiClose As DevExpress.XtraBars.BarButtonItem
    Private WithEvents layoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents txtKode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents txtNama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ckIsActive As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItemIsActive As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ckIsNonStok As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtIDKategori As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItemIsNonStock As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtIDSatuanTerbesar As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtL As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtT As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItemP As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItemL As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItemT As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItemIDSatuanTerbesar As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtIsi As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtHargaBeli As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItemHargaBeli As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItemIsi As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtHargaJual As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtProsenUp As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItemProsenUp As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItemHargaJual As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtIDSatuanTerkecil As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItemIDSatuanTerkecil As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtTanggalUbah As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtTanggalBuat As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtUserUbah As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUserBuat As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItemUserBuat As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItemUserUbah As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItemTanggalBuat As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItemTanggalUbah As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItemGVD As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn0 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents repo_cbIDSatuan As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtCatatan As DevExpress.XtraEditors.MemoEdit
End Class
