<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormDaftarTransaksi
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDaftarTransaksi))
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.BarEditTglDari = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemTglDari = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.BarEditTglSampai = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryTglSampai = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.BarButtonRefresh = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonBaru = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonEdit = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonHapus = New DevExpress.XtraBars.BarButtonItem()
        Me.Bar3 = New DevExpress.XtraBars.Bar()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockingMenuItem1 = New DevExpress.XtraBars.BarDockingMenuItem()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BarButtonCetak = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTglDari, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTglDari.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryTglSampai, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryTglSampai.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2, Me.Bar3})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonBaru, Me.BarButtonEdit, Me.BarButtonHapus, Me.BarButtonRefresh, Me.BarDockingMenuItem1, Me.BarEditTglDari, Me.BarEditTglSampai, Me.BarButtonCetak})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 13
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTglDari, Me.RepositoryTglSampai})
        Me.BarManager1.StatusBar = Me.Bar3
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(CType((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle Or DevExpress.XtraBars.BarLinkUserDefines.Width), DevExpress.XtraBars.BarLinkUserDefines), Me.BarEditTglDari, "", False, True, True, 109, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(CType((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle Or DevExpress.XtraBars.BarLinkUserDefines.Width), DevExpress.XtraBars.BarLinkUserDefines), Me.BarEditTglSampai, "", False, True, True, 111, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonRefresh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonBaru, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonHapus, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonCetak, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'BarEditTglDari
        '
        Me.BarEditTglDari.Caption = "Tanggal"
        Me.BarEditTglDari.Edit = Me.RepositoryItemTglDari
        Me.BarEditTglDari.Id = 10
        Me.BarEditTglDari.Name = "BarEditTglDari"
        '
        'RepositoryItemTglDari
        '
        Me.RepositoryItemTglDari.AutoHeight = False
        Me.RepositoryItemTglDari.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemTglDari.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemTglDari.CalendarTimeProperties.ValidateOnEnterKey = True
        Me.RepositoryItemTglDari.MinValue = New Date(2020, 1, 1, 0, 0, 0, 0)
        Me.RepositoryItemTglDari.Name = "RepositoryItemTglDari"
        '
        'BarEditTglSampai
        '
        Me.BarEditTglSampai.Caption = "Sampai"
        Me.BarEditTglSampai.Edit = Me.RepositoryTglSampai
        Me.BarEditTglSampai.Id = 11
        Me.BarEditTglSampai.Name = "BarEditTglSampai"
        '
        'RepositoryTglSampai
        '
        Me.RepositoryTglSampai.AutoHeight = False
        Me.RepositoryTglSampai.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryTglSampai.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryTglSampai.CalendarTimeProperties.ValidateOnEnterKey = True
        Me.RepositoryTglSampai.MinValue = New Date(2020, 1, 1, 0, 0, 0, 0)
        Me.RepositoryTglSampai.Name = "RepositoryTglSampai"
        '
        'BarButtonRefresh
        '
        Me.BarButtonRefresh.Caption = "Refresh"
        Me.BarButtonRefresh.Id = 8
        Me.BarButtonRefresh.ImageOptions.Image = Global.Magnus.My.Resources.Resources.convert_16x16
        Me.BarButtonRefresh.ImageOptions.LargeImage = Global.Magnus.My.Resources.Resources.convert_32x32
        Me.BarButtonRefresh.Name = "BarButtonRefresh"
        '
        'BarButtonBaru
        '
        Me.BarButtonBaru.Caption = "Baru"
        Me.BarButtonBaru.Id = 5
        Me.BarButtonBaru.ImageOptions.Image = Global.Magnus.My.Resources.Resources.addfile_16x16
        Me.BarButtonBaru.ImageOptions.LargeImage = Global.Magnus.My.Resources.Resources.addfile_32x32
        Me.BarButtonBaru.Name = "BarButtonBaru"
        '
        'BarButtonEdit
        '
        Me.BarButtonEdit.Caption = "Ubah"
        Me.BarButtonEdit.Id = 6
        Me.BarButtonEdit.ImageOptions.Image = Global.Magnus.My.Resources.Resources.additem_16x16
        Me.BarButtonEdit.ImageOptions.LargeImage = Global.Magnus.My.Resources.Resources.additem_32x321
        Me.BarButtonEdit.Name = "BarButtonEdit"
        '
        'BarButtonHapus
        '
        Me.BarButtonHapus.Caption = "Hapus"
        Me.BarButtonHapus.Id = 7
        Me.BarButtonHapus.ImageOptions.Image = Global.Magnus.My.Resources.Resources.cancel_16x16
        Me.BarButtonHapus.ImageOptions.LargeImage = Global.Magnus.My.Resources.Resources.cancel_32x32
        Me.BarButtonHapus.Name = "BarButtonHapus"
        '
        'Bar3
        '
        Me.Bar3.BarName = "Status bar"
        Me.Bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.Bar3.DockCol = 0
        Me.Bar3.DockRow = 0
        Me.Bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.Bar3.OptionsBar.AllowQuickCustomization = False
        Me.Bar3.OptionsBar.DrawDragBorder = False
        Me.Bar3.OptionsBar.UseWholeRow = True
        Me.Bar3.Text = "Status bar"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(808, 26)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 402)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(808, 18)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 26)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 376)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(808, 26)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 376)
        '
        'BarDockingMenuItem1
        '
        Me.BarDockingMenuItem1.Caption = "BarDockingMenuItem1"
        Me.BarDockingMenuItem1.Id = 9
        Me.BarDockingMenuItem1.Name = "BarDockingMenuItem1"
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControl1.Location = New System.Drawing.Point(0, 26)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.MenuManager = Me.BarManager1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(808, 376)
        Me.GridControl1.TabIndex = 4
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'BarButtonCetak
        '
        Me.BarButtonCetak.Caption = "Cetak"
        Me.BarButtonCetak.Id = 12
        Me.BarButtonCetak.ImageOptions.Image = CType(resources.GetObject("BarButtonItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonCetak.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonCetak.Name = "BarButtonCetak"
        '
        'FormDaftarTransaksi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(808, 420)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "FormDaftarTransaksi"
        Me.Text = "FormDaftarTransaksi"
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTglDari.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTglDari, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryTglSampai.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryTglSampai, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents Bar3 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BarButtonBaru As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonEdit As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonHapus As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonRefresh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarEditTglDari As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemTglDari As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents BarEditTglSampai As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryTglSampai As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents BarDockingMenuItem1 As DevExpress.XtraBars.BarDockingMenuItem
    Friend WithEvents BarButtonCetak As DevExpress.XtraBars.BarButtonItem
End Class
