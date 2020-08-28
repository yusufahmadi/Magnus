<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLapSP
    Inherits DevExpress.XtraEditors.XtraForm

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
        Dim WindowsUIButtonImageOptions1 As DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions = New DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormLapSP))
        Dim WindowsUIButtonImageOptions2 As DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions = New DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions()
        Dim WindowsUIButtonImageOptions3 As DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions = New DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions()
        Me.windowsUIButtonPanelMain = New DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel()
        Me.LC1 = New DevExpress.XtraLayout.LayoutControl()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GV1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LC1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LC1.SuspendLayout()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'windowsUIButtonPanelMain
        '
        Me.windowsUIButtonPanelMain.AppearanceButton.Hovered.BackColor = System.Drawing.Color.FromArgb(CType(CType(130, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.windowsUIButtonPanelMain.AppearanceButton.Hovered.FontSizeDelta = -1
        Me.windowsUIButtonPanelMain.AppearanceButton.Hovered.ForeColor = System.Drawing.Color.FromArgb(CType(CType(130, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.windowsUIButtonPanelMain.AppearanceButton.Hovered.Options.UseBackColor = True
        Me.windowsUIButtonPanelMain.AppearanceButton.Hovered.Options.UseFont = True
        Me.windowsUIButtonPanelMain.AppearanceButton.Hovered.Options.UseForeColor = True
        Me.windowsUIButtonPanelMain.AppearanceButton.Normal.FontSizeDelta = -1
        Me.windowsUIButtonPanelMain.AppearanceButton.Normal.Options.UseFont = True
        Me.windowsUIButtonPanelMain.AppearanceButton.Pressed.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(159, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.windowsUIButtonPanelMain.AppearanceButton.Pressed.FontSizeDelta = -1
        Me.windowsUIButtonPanelMain.AppearanceButton.Pressed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(159, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.windowsUIButtonPanelMain.AppearanceButton.Pressed.Options.UseBackColor = True
        Me.windowsUIButtonPanelMain.AppearanceButton.Pressed.Options.UseFont = True
        Me.windowsUIButtonPanelMain.AppearanceButton.Pressed.Options.UseForeColor = True
        Me.windowsUIButtonPanelMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(63, Byte), Integer))
        WindowsUIButtonImageOptions1.Image = CType(resources.GetObject("WindowsUIButtonImageOptions1.Image"), System.Drawing.Image)
        WindowsUIButtonImageOptions1.ImageUri.Uri = "SaveAndClose;Size32x32;GrayScaled"
        WindowsUIButtonImageOptions2.Image = CType(resources.GetObject("WindowsUIButtonImageOptions2.Image"), System.Drawing.Image)
        WindowsUIButtonImageOptions3.ImageUri.Uri = "Edit/Delete;Size32x32;GrayScaled"
        Me.windowsUIButtonPanelMain.Buttons.AddRange(New DevExpress.XtraEditors.ButtonPanel.IBaseButton() {New DevExpress.XtraBars.Docking2010.WindowsUISeparator(), New DevExpress.XtraBars.Docking2010.WindowsUIButton("Refresh", True, WindowsUIButtonImageOptions1), New DevExpress.XtraBars.Docking2010.WindowsUISeparator(), New DevExpress.XtraBars.Docking2010.WindowsUIButton("Cetak", True, WindowsUIButtonImageOptions2), New DevExpress.XtraBars.Docking2010.WindowsUISeparator(), New DevExpress.XtraBars.Docking2010.WindowsUIButton("Close", True, WindowsUIButtonImageOptions3)})
        Me.windowsUIButtonPanelMain.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.windowsUIButtonPanelMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.windowsUIButtonPanelMain.EnableImageTransparency = True
        Me.windowsUIButtonPanelMain.ForeColor = System.Drawing.Color.White
        Me.windowsUIButtonPanelMain.Location = New System.Drawing.Point(0, 390)
        Me.windowsUIButtonPanelMain.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.windowsUIButtonPanelMain.MaximumSize = New System.Drawing.Size(0, 60)
        Me.windowsUIButtonPanelMain.MinimumSize = New System.Drawing.Size(60, 60)
        Me.windowsUIButtonPanelMain.Name = "windowsUIButtonPanelMain"
        Me.windowsUIButtonPanelMain.Size = New System.Drawing.Size(800, 60)
        Me.windowsUIButtonPanelMain.TabIndex = 6
        Me.windowsUIButtonPanelMain.Text = "windowsUIButtonPanelMain"
        Me.windowsUIButtonPanelMain.UseButtonBackgroundImages = False
        '
        'LC1
        '
        Me.LC1.Controls.Add(Me.XtraTabControl1)
        Me.LC1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LC1.Location = New System.Drawing.Point(0, 0)
        Me.LC1.Name = "LC1"
        Me.LC1.Root = Me.LayoutControlGroup1
        Me.LC1.Size = New System.Drawing.Size(800, 390)
        Me.LC1.TabIndex = 7
        Me.LC1.Text = "LayoutControl1"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Location = New System.Drawing.Point(12, 12)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl1.Size = New System.Drawing.Size(776, 366)
        Me.XtraTabControl1.TabIndex = 6
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.GridControl1)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(774, 341)
        Me.XtraTabPage1.Text = "XtraTabPage1"
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControl1.Location = New System.Drawing.Point(0, 0)
        Me.GridControl1.MainView = Me.GV1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(774, 341)
        Me.GridControl1.TabIndex = 5
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GV1})
        '
        'GV1
        '
        Me.GV1.GridControl = Me.GridControl1
        Me.GV1.Name = "GV1"
        Me.GV1.OptionsBehavior.Editable = False
        Me.GV1.OptionsCustomization.AllowSort = False
        Me.GV1.OptionsView.ShowGroupPanel = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(800, 390)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.XtraTabControl1
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(780, 370)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'FormLapSP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.LC1)
        Me.Controls.Add(Me.windowsUIButtonPanelMain)
        Me.Name = "FormLapSP"
        Me.Text = "FormLap"
        CType(Me.LC1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LC1.ResumeLayout(False)
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GV1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents windowsUIButtonPanelMain As DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel
    Friend WithEvents LC1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GV1 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
