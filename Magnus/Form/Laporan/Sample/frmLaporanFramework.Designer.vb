<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLaporanFramework
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLaporanFramework))
        Me.GC1 = New DevExpress.XtraGrid.GridControl()
        Me.GV1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.lbDaftar = New DevExpress.XtraEditors.LabelControl()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.BarSubItem1 = New DevExpress.XtraBars.BarSubItem()
        Me.BarButtonItem5 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem6 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.mnRefresh = New DevExpress.XtraBars.BarButtonItem()
        Me.mnSaveLayouts = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.barPreview2 = New DevExpress.XtraBars.BarButtonItem()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.btnPreview2 = New DevExpress.XtraEditors.SimpleButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnPreview = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton4 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton6 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton8 = New DevExpress.XtraEditors.SimpleButton()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.SplitterControl1 = New DevExpress.XtraEditors.SplitterControl()
        Me.LC1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.TabGrid = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintDialog2 = New System.Windows.Forms.PrintDialog()
        CType(Me.GC1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LC1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LC1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GC1
        '
        Me.GC1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GC1.Location = New System.Drawing.Point(0, 0)
        Me.GC1.MainView = Me.GV1
        Me.GC1.Name = "GC1"
        Me.GC1.Size = New System.Drawing.Size(1160, 393)
        Me.GC1.TabIndex = 5
        Me.GC1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GV1})
        '
        'GV1
        '
        Me.GV1.GridControl = Me.GC1
        Me.GV1.Name = "GV1"
        Me.GV1.OptionsBehavior.AllowIncrementalSearch = True
        Me.GV1.OptionsBehavior.Editable = False
        Me.GV1.OptionsSelection.MultiSelect = True
        Me.GV1.OptionsView.ColumnAutoWidth = False
        Me.GV1.OptionsView.ShowFooter = True
        Me.GV1.OptionsView.ShowGroupPanel = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.lbDaftar)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 24)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1186, 28)
        Me.PanelControl1.TabIndex = 0
        '
        'lbDaftar
        '
        Me.lbDaftar.Appearance.Font = New System.Drawing.Font("Rockwell", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbDaftar.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lbDaftar.Appearance.Options.UseFont = True
        Me.lbDaftar.Appearance.Options.UseForeColor = True
        Me.lbDaftar.Location = New System.Drawing.Point(25, 6)
        Me.lbDaftar.Name = "lbDaftar"
        Me.lbDaftar.Size = New System.Drawing.Size(50, 19)
        Me.lbDaftar.TabIndex = 1
        Me.lbDaftar.Text = "Daftar"
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarSubItem1, Me.BarButtonItem4, Me.BarButtonItem5, Me.BarButtonItem6, Me.mnRefresh, Me.mnSaveLayouts, Me.barPreview2})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 25
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem1)})
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        Me.Bar2.Visible = False
        '
        'BarSubItem1
        '
        Me.BarSubItem1.Caption = "Action"
        Me.BarSubItem1.Id = 0
        Me.BarSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem5), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem6), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem4, True), New DevExpress.XtraBars.LinkPersistInfo(Me.mnRefresh), New DevExpress.XtraBars.LinkPersistInfo(Me.mnSaveLayouts)})
        Me.BarSubItem1.Name = "BarSubItem1"
        '
        'BarButtonItem5
        '
        Me.BarButtonItem5.Caption = "Export Excel"
        Me.BarButtonItem5.Id = 5
        Me.BarButtonItem5.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11)
        Me.BarButtonItem5.Name = "BarButtonItem5"
        '
        'BarButtonItem6
        '
        Me.BarButtonItem6.Caption = "Preview"
        Me.BarButtonItem6.Id = 6
        Me.BarButtonItem6.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
        Me.BarButtonItem6.Name = "BarButtonItem6"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "Tutup"
        Me.BarButtonItem4.Id = 4
        Me.BarButtonItem4.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'mnRefresh
        '
        Me.mnRefresh.Caption = "&Refresh"
        Me.mnRefresh.Id = 16
        Me.mnRefresh.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
        Me.mnRefresh.Name = "mnRefresh"
        '
        'mnSaveLayouts
        '
        Me.mnSaveLayouts.Caption = "Save Layouts"
        Me.mnSaveLayouts.Id = 23
        Me.mnSaveLayouts.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F10))
        Me.mnSaveLayouts.Name = "mnSaveLayouts"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(1186, 24)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 540)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(1186, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 24)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 516)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1186, 24)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 516)
        '
        'barPreview2
        '
        Me.barPreview2.Caption = "Preview2"
        Me.barPreview2.Id = 24
        Me.barPreview2.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
        Me.barPreview2.Name = "barPreview2"
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.btnPreview2)
        Me.PanelControl3.Controls.Add(Me.btnPreview)
        Me.PanelControl3.Controls.Add(Me.SimpleButton4)
        Me.PanelControl3.Controls.Add(Me.SimpleButton6)
        Me.PanelControl3.Controls.Add(Me.SimpleButton8)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl3.Location = New System.Drawing.Point(0, 494)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(1186, 46)
        Me.PanelControl3.TabIndex = 2
        '
        'btnPreview2
        '
        Me.btnPreview2.Appearance.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview2.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.btnPreview2.Appearance.Options.UseFont = True
        Me.btnPreview2.Appearance.Options.UseForeColor = True
        Me.btnPreview2.ImageOptions.ImageIndex = 8
        Me.btnPreview2.ImageOptions.ImageList = Me.ImageList1
        Me.btnPreview2.Location = New System.Drawing.Point(359, 11)
        Me.btnPreview2.Name = "btnPreview2"
        Me.btnPreview2.Size = New System.Drawing.Size(154, 31)
        Me.btnPreview2.TabIndex = 12
        Me.btnPreview2.Text = "&Preview 2"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "F1.png")
        Me.ImageList1.Images.SetKeyName(1, "F2.png")
        Me.ImageList1.Images.SetKeyName(2, "F3.png")
        Me.ImageList1.Images.SetKeyName(3, "F4.png")
        Me.ImageList1.Images.SetKeyName(4, "F5.png")
        Me.ImageList1.Images.SetKeyName(5, "F6.png")
        Me.ImageList1.Images.SetKeyName(6, "F7.png")
        Me.ImageList1.Images.SetKeyName(7, "F8.png")
        Me.ImageList1.Images.SetKeyName(8, "F9.png")
        Me.ImageList1.Images.SetKeyName(9, "F10.png")
        Me.ImageList1.Images.SetKeyName(10, "F11.png")
        Me.ImageList1.Images.SetKeyName(11, "F12.png")
        '
        'btnPreview
        '
        Me.btnPreview.Appearance.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.btnPreview.Appearance.Options.UseFont = True
        Me.btnPreview.Appearance.Options.UseForeColor = True
        Me.btnPreview.ImageOptions.ImageIndex = 7
        Me.btnPreview.ImageOptions.ImageList = Me.ImageList1
        Me.btnPreview.Location = New System.Drawing.Point(199, 11)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(154, 31)
        Me.btnPreview.TabIndex = 11
        Me.btnPreview.Text = "&Preview"
        '
        'SimpleButton4
        '
        Me.SimpleButton4.Appearance.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SimpleButton4.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.SimpleButton4.Appearance.Options.UseFont = True
        Me.SimpleButton4.Appearance.Options.UseForeColor = True
        Me.SimpleButton4.ImageOptions.ImageIndex = 10
        Me.SimpleButton4.ImageOptions.ImageList = Me.ImageList1
        Me.SimpleButton4.Location = New System.Drawing.Point(5, 11)
        Me.SimpleButton4.Name = "SimpleButton4"
        Me.SimpleButton4.Size = New System.Drawing.Size(82, 31)
        Me.SimpleButton4.TabIndex = 10
        Me.SimpleButton4.Text = "&Excel"
        '
        'SimpleButton6
        '
        Me.SimpleButton6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SimpleButton6.Appearance.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SimpleButton6.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.SimpleButton6.Appearance.Options.UseFont = True
        Me.SimpleButton6.Appearance.Options.UseForeColor = True
        Me.SimpleButton6.ImageOptions.ImageIndex = 2
        Me.SimpleButton6.ImageOptions.ImageList = Me.ImageList1
        Me.SimpleButton6.Location = New System.Drawing.Point(1074, 11)
        Me.SimpleButton6.Name = "SimpleButton6"
        Me.SimpleButton6.Size = New System.Drawing.Size(100, 31)
        Me.SimpleButton6.TabIndex = 8
        Me.SimpleButton6.Text = "&Tutup"
        '
        'SimpleButton8
        '
        Me.SimpleButton8.Appearance.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SimpleButton8.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.SimpleButton8.Appearance.Options.UseFont = True
        Me.SimpleButton8.Appearance.Options.UseForeColor = True
        Me.SimpleButton8.ImageOptions.ImageIndex = 4
        Me.SimpleButton8.ImageOptions.ImageList = Me.ImageList1
        Me.SimpleButton8.Location = New System.Drawing.Point(93, 11)
        Me.SimpleButton8.Name = "SimpleButton8"
        Me.SimpleButton8.Size = New System.Drawing.Size(100, 31)
        Me.SimpleButton8.TabIndex = 7
        Me.SimpleButton8.Text = "&Refresh"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Location = New System.Drawing.Point(12, 12)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl1.Size = New System.Drawing.Size(1162, 418)
        Me.XtraTabControl1.TabIndex = 8
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.GC1)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1160, 393)
        Me.XtraTabPage1.Text = "Laporan"
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.CustomizationFormText = "Kode Customer"
        Me.LayoutControlItem19.Location = New System.Drawing.Point(548, 0)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(347, 26)
        Me.LayoutControlItem19.Text = "Kode Customer"
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(83, 13)
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.CustomizationFormText = "Nama Customer"
        Me.LayoutControlItem18.Location = New System.Drawing.Point(548, 26)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Size = New System.Drawing.Size(347, 26)
        Me.LayoutControlItem18.Text = "Nama Customer"
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(83, 13)
        '
        'SplitterControl1
        '
        Me.SplitterControl1.Location = New System.Drawing.Point(1, 23)
        Me.SplitterControl1.Name = "SplitterControl1"
        Me.SplitterControl1.Size = New System.Drawing.Size(12, 385)
        Me.SplitterControl1.TabIndex = 1
        Me.SplitterControl1.TabStop = False
        '
        'LC1
        '
        Me.LC1.Controls.Add(Me.XtraTabControl1)
        Me.LC1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LC1.Location = New System.Drawing.Point(0, 52)
        Me.LC1.Name = "LC1"
        Me.LC1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(647, 447, 450, 389)
        Me.LC1.Root = Me.LayoutControlGroup1
        Me.LC1.Size = New System.Drawing.Size(1186, 442)
        Me.LC1.TabIndex = 13
        Me.LC1.Text = "LayoutControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.TabGrid})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1186, 442)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'TabGrid
        '
        Me.TabGrid.Control = Me.XtraTabControl1
        Me.TabGrid.Location = New System.Drawing.Point(0, 0)
        Me.TabGrid.Name = "TabGrid"
        Me.TabGrid.Size = New System.Drawing.Size(1166, 422)
        Me.TabGrid.TextLocation = DevExpress.Utils.Locations.Top
        Me.TabGrid.TextSize = New System.Drawing.Size(0, 0)
        Me.TabGrid.TextVisible = False
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintDialog2
        '
        Me.PrintDialog2.UseEXDialog = True
        '
        'frmLaporanFramework
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1186, 540)
        Me.Controls.Add(Me.LC1)
        Me.Controls.Add(Me.PanelControl3)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmLaporanFramework"
        Me.Text = "Laporan"
        CType(Me.GC1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LC1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LC1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GC1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents SimpleButton6 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton8 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents BarSubItem1 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents btnPreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton4 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BarButtonItem5 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem6 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents lbDaftar As DevExpress.XtraEditors.LabelControl
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents mnRefresh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GV1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtWilayah As DevExpress.XtraEditors.SearchLookUpEdit 'DXSample.Custom
    Friend WithEvents gvWilayah As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtKodeCustomer As DevExpress.XtraEditors.SearchLookUpEdit 'DXSample.Custom
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lbCustomer As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNamaCustomer As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lbBrg As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNamaBrg As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtKodeBrg As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView9 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SplitterControl1 As DevExpress.XtraEditors.SplitterControl
    Friend WithEvents LC1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents TabGrid As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents mnSaveLayouts As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents PrintDialog2 As PrintDialog
    Friend WithEvents btnPreview2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents barPreview2 As DevExpress.XtraBars.BarButtonItem
End Class
