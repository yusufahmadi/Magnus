<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class FormKaryawan
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
        Me.components = New System.ComponentModel.Container()
        Me.dataLayoutControl1 = New DevExpress.XtraDataLayout.DataLayoutControl()
        Me.txtKeterangan = New DevExpress.XtraEditors.MemoEdit()
        Me.mainRibbonControl = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.bbiSave = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiSaveAndClose = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiSaveAndNew = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiReset = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiDelete = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiClose = New DevExpress.XtraBars.BarButtonItem()
        Me.mainRibbonPage = New DevExpress.XtraBars.Ribbon.RibbonPage()
        Me.mainRibbonPageGroup = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
        Me.ckIsActive = New DevExpress.XtraEditors.CheckEdit()
        Me.txtAlias = New DevExpress.XtraEditors.TextEdit()
        Me.txtNama = New DevExpress.XtraEditors.TextEdit()
        Me.txtKode = New DevExpress.XtraEditors.TextEdit()
        Me.txtAlamat = New DevExpress.XtraEditors.TextEdit()
        Me.txtAlamat2 = New DevExpress.XtraEditors.TextEdit()
        Me.txtHP = New DevExpress.XtraEditors.TextEdit()
        Me.layoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.layoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.DxErrorProvider1 = New DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(Me.components)
        CType(Me.dataLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dataLayoutControl1.SuspendLayout()
        CType(Me.txtKeterangan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mainRibbonControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ckIsActive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAlias.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAlamat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAlamat2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dataLayoutControl1
        '
        Me.dataLayoutControl1.AllowCustomization = False
        Me.dataLayoutControl1.Controls.Add(Me.txtKeterangan)
        Me.dataLayoutControl1.Controls.Add(Me.ckIsActive)
        Me.dataLayoutControl1.Controls.Add(Me.txtAlias)
        Me.dataLayoutControl1.Controls.Add(Me.txtNama)
        Me.dataLayoutControl1.Controls.Add(Me.txtKode)
        Me.dataLayoutControl1.Controls.Add(Me.txtAlamat)
        Me.dataLayoutControl1.Controls.Add(Me.txtAlamat2)
        Me.dataLayoutControl1.Controls.Add(Me.txtHP)
        Me.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dataLayoutControl1.Location = New System.Drawing.Point(0, 146)
        Me.dataLayoutControl1.Name = "dataLayoutControl1"
        Me.dataLayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(841, 263, 450, 400)
        Me.dataLayoutControl1.Root = Me.layoutControlGroup1
        Me.dataLayoutControl1.Size = New System.Drawing.Size(332, 382)
        Me.dataLayoutControl1.TabIndex = 0
        '
        'txtKeterangan
        '
        Me.txtKeterangan.Location = New System.Drawing.Point(12, 148)
        Me.txtKeterangan.MenuManager = Me.mainRibbonControl
        Me.txtKeterangan.Name = "txtKeterangan"
        Me.txtKeterangan.Size = New System.Drawing.Size(308, 78)
        Me.txtKeterangan.StyleController = Me.dataLayoutControl1
        Me.txtKeterangan.TabIndex = 10
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
        Me.mainRibbonControl.Size = New System.Drawing.Size(332, 146)
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
        'ckIsActive
        '
        Me.ckIsActive.Location = New System.Drawing.Point(12, 351)
        Me.ckIsActive.MenuManager = Me.mainRibbonControl
        Me.ckIsActive.Name = "ckIsActive"
        Me.ckIsActive.Properties.Caption = "Aktif"
        Me.ckIsActive.Size = New System.Drawing.Size(308, 19)
        Me.ckIsActive.StyleController = Me.dataLayoutControl1
        Me.ckIsActive.TabIndex = 9
        '
        'txtAlias
        '
        Me.txtAlias.Location = New System.Drawing.Point(12, 108)
        Me.txtAlias.MenuManager = Me.mainRibbonControl
        Me.txtAlias.Name = "txtAlias"
        Me.txtAlias.Size = New System.Drawing.Size(308, 20)
        Me.txtAlias.StyleController = Me.dataLayoutControl1
        Me.txtAlias.TabIndex = 6
        '
        'txtNama
        '
        Me.txtNama.Location = New System.Drawing.Point(12, 68)
        Me.txtNama.MenuManager = Me.mainRibbonControl
        Me.txtNama.Name = "txtNama"
        Me.txtNama.Size = New System.Drawing.Size(308, 20)
        Me.txtNama.StyleController = Me.dataLayoutControl1
        Me.txtNama.TabIndex = 5
        '
        'txtKode
        '
        Me.txtKode.Location = New System.Drawing.Point(12, 28)
        Me.txtKode.MenuManager = Me.mainRibbonControl
        Me.txtKode.Name = "txtKode"
        Me.txtKode.Size = New System.Drawing.Size(308, 20)
        Me.txtKode.StyleController = Me.dataLayoutControl1
        Me.txtKode.TabIndex = 4
        '
        'txtAlamat
        '
        Me.txtAlamat.Location = New System.Drawing.Point(12, 246)
        Me.txtAlamat.Name = "txtAlamat"
        Me.txtAlamat.Size = New System.Drawing.Size(308, 20)
        Me.txtAlamat.StyleController = Me.dataLayoutControl1
        Me.txtAlamat.TabIndex = 4
        '
        'txtAlamat2
        '
        Me.txtAlamat2.Location = New System.Drawing.Point(12, 286)
        Me.txtAlamat2.Name = "txtAlamat2"
        Me.txtAlamat2.Size = New System.Drawing.Size(308, 20)
        Me.txtAlamat2.StyleController = Me.dataLayoutControl1
        Me.txtAlamat2.TabIndex = 5
        '
        'txtHP
        '
        Me.txtHP.Location = New System.Drawing.Point(12, 326)
        Me.txtHP.Name = "txtHP"
        Me.txtHP.Size = New System.Drawing.Size(308, 20)
        Me.txtHP.StyleController = Me.dataLayoutControl1
        Me.txtHP.TabIndex = 6
        '
        'layoutControlGroup1
        '
        Me.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.layoutControlGroup1.GroupBordersVisible = False
        Me.layoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.layoutControlGroup2})
        Me.layoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.layoutControlGroup1.Name = "Root"
        Me.layoutControlGroup1.Size = New System.Drawing.Size(332, 382)
        Me.layoutControlGroup1.TextVisible = False
        '
        'layoutControlGroup2
        '
        Me.layoutControlGroup2.AllowDrawBackground = False
        Me.layoutControlGroup2.GroupBordersVisible = False
        Me.layoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem7, Me.LayoutControlItem6, Me.LayoutControlItem8, Me.LayoutControlItem2})
        Me.layoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.layoutControlGroup2.Name = "autoGeneratedGroup0"
        Me.layoutControlGroup2.Size = New System.Drawing.Size(312, 362)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.txtKode
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(312, 40)
        Me.LayoutControlItem1.Text = "Kode"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtAlias
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 80)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(312, 40)
        Me.LayoutControlItem3.Text = "Alias"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.txtAlamat
        Me.LayoutControlItem4.CustomizationFormText = "Address line 1:"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 218)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(404, 0)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(116, 40)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(312, 40)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Alamat 1"
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.txtAlamat2
        Me.LayoutControlItem5.CustomizationFormText = "Address line 2:"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 258)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(404, 0)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(116, 40)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(312, 40)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Alamat 2"
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.txtHP
        Me.LayoutControlItem7.CustomizationFormText = "City:"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 298)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(404, 0)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(116, 40)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(312, 41)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.Text = "HP:"
        Me.LayoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.ckIsActive
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 339)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(312, 23)
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.txtKeterangan
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(312, 98)
        Me.LayoutControlItem8.Text = "Keterangan"
        Me.LayoutControlItem8.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(56, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.txtNama
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 40)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(312, 40)
        Me.LayoutControlItem2.Text = "Nama"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(56, 13)
        '
        'DxErrorProvider1
        '
        Me.DxErrorProvider1.ContainerControl = Me
        '
        'FormKaryawan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(332, 528)
        Me.Controls.Add(Me.dataLayoutControl1)
        Me.Controls.Add(Me.mainRibbonControl)
        Me.Name = "FormKaryawan"
        Me.Ribbon = Me.mainRibbonControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Karyawan"
        CType(Me.dataLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dataLayoutControl1.ResumeLayout(False)
        CType(Me.txtKeterangan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mainRibbonControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ckIsActive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAlias.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAlamat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAlamat2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DxErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtAlias As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ckIsActive As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtAlamat As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtAlamat2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtHP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtKeterangan As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents DxErrorProvider1 As DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider
End Class
