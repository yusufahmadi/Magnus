<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDemo
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormDemo))
        Me.xtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.xtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.icbNewItemRow = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.label1 = New System.Windows.Forms.Label()
        Me.xtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.lbEvent = New System.Windows.Forms.Label()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.sbStart = New DevExpress.XtraEditors.SimpleButton()
        Me.xtraTabPage3 = New DevExpress.XtraTab.XtraTabPage()
        Me.chEdit = New DevExpress.XtraEditors.CheckEdit()
        Me.icbButtons = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.label2 = New System.Windows.Forms.Label()
        Me.xtraTabPage4 = New DevExpress.XtraTab.XtraTabPage()
        Me.icbSelectMode = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.label3 = New System.Windows.Forms.Label()
        Me.panel2 = New System.Windows.Forms.Panel()
        Me.sbRecords = New DevExpress.XtraEditors.SimpleButton()
        Me.ceMultiSelect = New DevExpress.XtraEditors.CheckEdit()
        Me.splitter1 = New DevExpress.XtraEditors.PanelControl()
        Me.timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.repositoryItemLookUpEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.gridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.repositoryItemCalcEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.gridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.repositoryItemSpinEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.gridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.repositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        CType(Me.xtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtraTabControl1.SuspendLayout()
        Me.xtraTabPage1.SuspendLayout()
        CType(Me.icbNewItemRow.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtraTabPage2.SuspendLayout()
        Me.panel1.SuspendLayout()
        Me.xtraTabPage3.SuspendLayout()
        CType(Me.chEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.icbButtons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtraTabPage4.SuspendLayout()
        CType(Me.icbSelectMode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel2.SuspendLayout()
        CType(Me.ceMultiSelect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.splitter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repositoryItemLookUpEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtraTabControl1
        '
        Me.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.xtraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.xtraTabControl1.Name = "xtraTabControl1"
        Me.xtraTabControl1.SelectedTabPage = Me.xtraTabPage1
        Me.xtraTabControl1.Size = New System.Drawing.Size(800, 68)
        Me.xtraTabControl1.TabIndex = 6
        Me.xtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtraTabPage1, Me.xtraTabPage2, Me.xtraTabPage3, Me.xtraTabPage4})
        Me.xtraTabControl1.Tag = ""
        '
        'xtraTabPage1
        '
        Me.xtraTabPage1.Controls.Add(Me.icbNewItemRow)
        Me.xtraTabPage1.Controls.Add(Me.label1)
        Me.xtraTabPage1.Name = "xtraTabPage1"
        Me.xtraTabPage1.Size = New System.Drawing.Size(798, 43)
        Me.xtraTabPage1.Text = "New Item Row"
        '
        'icbNewItemRow
        '
        Me.icbNewItemRow.EditValue = "imageComboBoxEdit1"
        Me.icbNewItemRow.Location = New System.Drawing.Point(153, 9)
        Me.icbNewItemRow.Name = "icbNewItemRow"
        Me.icbNewItemRow.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.icbNewItemRow.Size = New System.Drawing.Size(124, 20)
        Me.icbNewItemRow.TabIndex = 3
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Location = New System.Drawing.Point(4, 12)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(152, 16)
        Me.label1.TabIndex = 2
        Me.label1.Text = "New Item Row Position:"
        '
        'xtraTabPage2
        '
        Me.xtraTabPage2.Controls.Add(Me.lbEvent)
        Me.xtraTabPage2.Controls.Add(Me.panel1)
        Me.xtraTabPage2.Name = "xtraTabPage2"
        Me.xtraTabPage2.Padding = New System.Windows.Forms.Padding(4)
        Me.xtraTabPage2.Size = New System.Drawing.Size(798, 43)
        Me.xtraTabPage2.Tag = "search"
        Me.xtraTabPage2.Text = "Incremental Search"
        '
        'lbEvent
        '
        Me.lbEvent.BackColor = System.Drawing.Color.Transparent
        Me.lbEvent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbEvent.Location = New System.Drawing.Point(4, 4)
        Me.lbEvent.Name = "lbEvent"
        Me.lbEvent.Size = New System.Drawing.Size(674, 35)
        Me.lbEvent.TabIndex = 1
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.Transparent
        Me.panel1.Controls.Add(Me.sbStart)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.panel1.Location = New System.Drawing.Point(678, 4)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(116, 35)
        Me.panel1.TabIndex = 2
        '
        'sbStart
        '
        Me.sbStart.Location = New System.Drawing.Point(4, 4)
        Me.sbStart.Name = "sbStart"
        Me.sbStart.Size = New System.Drawing.Size(108, 24)
        Me.sbStart.TabIndex = 0
        Me.sbStart.Text = "Start Searching"
        '
        'xtraTabPage3
        '
        Me.xtraTabPage3.Controls.Add(Me.chEdit)
        Me.xtraTabPage3.Controls.Add(Me.icbButtons)
        Me.xtraTabPage3.Controls.Add(Me.label2)
        Me.xtraTabPage3.Name = "xtraTabPage3"
        Me.xtraTabPage3.Size = New System.Drawing.Size(798, 43)
        Me.xtraTabPage3.Tag = "editing"
        Me.xtraTabPage3.Text = "Editing && Navigation"
        '
        'chEdit
        '
        Me.chEdit.Location = New System.Drawing.Point(427, 10)
        Me.chEdit.Name = "chEdit"
        Me.chEdit.Properties.Caption = "Editable"
        Me.chEdit.Size = New System.Drawing.Size(116, 19)
        Me.chEdit.TabIndex = 6
        '
        'icbButtons
        '
        Me.icbButtons.EditValue = "imageComboBoxEdit1"
        Me.icbButtons.Location = New System.Drawing.Point(164, 8)
        Me.icbButtons.Name = "icbButtons"
        Me.icbButtons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.icbButtons.Size = New System.Drawing.Size(192, 20)
        Me.icbButtons.TabIndex = 5
        '
        'label2
        '
        Me.label2.BackColor = System.Drawing.Color.Transparent
        Me.label2.Location = New System.Drawing.Point(4, 12)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(196, 16)
        Me.label2.TabIndex = 4
        Me.label2.Text = "Editor Button Display Mode:"
        '
        'xtraTabPage4
        '
        Me.xtraTabPage4.Controls.Add(Me.icbSelectMode)
        Me.xtraTabPage4.Controls.Add(Me.label3)
        Me.xtraTabPage4.Controls.Add(Me.panel2)
        Me.xtraTabPage4.Controls.Add(Me.ceMultiSelect)
        Me.xtraTabPage4.Name = "xtraTabPage4"
        Me.xtraTabPage4.Size = New System.Drawing.Size(798, 43)
        Me.xtraTabPage4.Tag = "multiselect"
        Me.xtraTabPage4.Text = "Multiple Row Selection"
        '
        'icbSelectMode
        '
        Me.icbSelectMode.EditValue = "imageComboBoxEdit1"
        Me.icbSelectMode.Location = New System.Drawing.Point(248, 8)
        Me.icbSelectMode.Name = "icbSelectMode"
        Me.icbSelectMode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.icbSelectMode.Size = New System.Drawing.Size(112, 20)
        Me.icbSelectMode.TabIndex = 5
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(136, 12)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(100, 16)
        Me.label3.TabIndex = 6
        Me.label3.Text = "MultiSelectMode:"
        '
        'panel2
        '
        Me.panel2.BackColor = System.Drawing.Color.Transparent
        Me.panel2.Controls.Add(Me.sbRecords)
        Me.panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.panel2.Location = New System.Drawing.Point(612, 0)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(186, 43)
        Me.panel2.TabIndex = 6
        '
        'sbRecords
        '
        Me.sbRecords.Location = New System.Drawing.Point(8, 8)
        Me.sbRecords.Name = "sbRecords"
        Me.sbRecords.Size = New System.Drawing.Size(172, 24)
        Me.sbRecords.TabIndex = 0
        '
        'ceMultiSelect
        '
        Me.ceMultiSelect.Location = New System.Drawing.Point(4, 10)
        Me.ceMultiSelect.Name = "ceMultiSelect"
        Me.ceMultiSelect.Properties.Caption = "Multi Select"
        Me.ceMultiSelect.Size = New System.Drawing.Size(116, 19)
        Me.ceMultiSelect.TabIndex = 4
        '
        'splitter1
        '
        Me.splitter1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.splitter1.Location = New System.Drawing.Point(0, 68)
        Me.splitter1.Name = "splitter1"
        Me.splitter1.Size = New System.Drawing.Size(800, 6)
        Me.splitter1.TabIndex = 7
        '
        'timer1
        '
        Me.timer1.Interval = 500
        '
        'imageList1
        '
        Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imageList1.TransparentColor = System.Drawing.Color.Magenta
        Me.imageList1.Images.SetKeyName(0, "")
        '
        'gridControl1
        '
        Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.gridControl1.EmbeddedNavigator.Buttons.Edit.ImageIndex = 0
        Me.gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.gridControl1.EmbeddedNavigator.Buttons.ImageList = Me.imageList1
        Me.gridControl1.Location = New System.Drawing.Point(0, 74)
        Me.gridControl1.MainView = Me.gridView1
        Me.gridControl1.Name = "gridControl1"
        Me.gridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.repositoryItemLookUpEdit1, Me.repositoryItemCalcEdit1, Me.repositoryItemSpinEdit1, Me.repositoryItemTextEdit1})
        Me.gridControl1.Size = New System.Drawing.Size(800, 376)
        Me.gridControl1.TabIndex = 8
        Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridView1})
        '
        'gridView1
        '
        Me.gridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gridColumn1, Me.gridColumn2, Me.gridColumn3, Me.gridColumn4, Me.gridColumn5})
        Me.gridView1.GridControl = Me.gridControl1
        Me.gridView1.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "OrderID", Nothing, "")})
        Me.gridView1.Name = "gridView1"
        Me.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top
        '
        'gridColumn1
        '
        Me.gridColumn1.Caption = "Order ID"
        Me.gridColumn1.FieldName = "OrderID"
        Me.gridColumn1.Name = "gridColumn1"
        Me.gridColumn1.Visible = True
        Me.gridColumn1.VisibleIndex = 0
        Me.gridColumn1.Width = 86
        '
        'gridColumn2
        '
        Me.gridColumn2.Caption = "Product"
        Me.gridColumn2.ColumnEdit = Me.repositoryItemLookUpEdit1
        Me.gridColumn2.FieldName = "ProductID"
        Me.gridColumn2.Name = "gridColumn2"
        Me.gridColumn2.Visible = True
        Me.gridColumn2.VisibleIndex = 1
        Me.gridColumn2.Width = 225
        '
        'repositoryItemLookUpEdit1
        '
        Me.repositoryItemLookUpEdit1.AutoHeight = False
        Me.repositoryItemLookUpEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.repositoryItemLookUpEdit1.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ProductName", "Product Name")})
        Me.repositoryItemLookUpEdit1.DisplayMember = "ProductName"
        Me.repositoryItemLookUpEdit1.DropDownRows = 10
        Me.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1"
        Me.repositoryItemLookUpEdit1.PopupWidth = 220
        Me.repositoryItemLookUpEdit1.ValueMember = "ProductID"
        '
        'gridColumn3
        '
        Me.gridColumn3.Caption = "Unit Price"
        Me.gridColumn3.ColumnEdit = Me.repositoryItemCalcEdit1
        Me.gridColumn3.DisplayFormat.FormatString = "c"
        Me.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.gridColumn3.FieldName = "UnitPrice"
        Me.gridColumn3.Name = "gridColumn3"
        Me.gridColumn3.Visible = True
        Me.gridColumn3.VisibleIndex = 2
        Me.gridColumn3.Width = 104
        '
        'repositoryItemCalcEdit1
        '
        Me.repositoryItemCalcEdit1.AutoHeight = False
        Me.repositoryItemCalcEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.repositoryItemCalcEdit1.Mask.EditMask = "c"
        Me.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1"
        '
        'gridColumn4
        '
        Me.gridColumn4.Caption = "Quantity"
        Me.gridColumn4.ColumnEdit = Me.repositoryItemSpinEdit1
        Me.gridColumn4.FieldName = "Quantity"
        Me.gridColumn4.Name = "gridColumn4"
        Me.gridColumn4.Visible = True
        Me.gridColumn4.VisibleIndex = 3
        Me.gridColumn4.Width = 104
        '
        'repositoryItemSpinEdit1
        '
        Me.repositoryItemSpinEdit1.AutoHeight = False
        Me.repositoryItemSpinEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.repositoryItemSpinEdit1.IsFloatValue = False
        Me.repositoryItemSpinEdit1.Mask.EditMask = "N00"
        Me.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1"
        '
        'gridColumn5
        '
        Me.gridColumn5.Caption = "Discount"
        Me.gridColumn5.ColumnEdit = Me.repositoryItemTextEdit1
        Me.gridColumn5.DisplayFormat.FormatString = "p"
        Me.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.gridColumn5.FieldName = "Discount"
        Me.gridColumn5.Name = "gridColumn5"
        Me.gridColumn5.Visible = True
        Me.gridColumn5.VisibleIndex = 4
        Me.gridColumn5.Width = 121
        '
        'repositoryItemTextEdit1
        '
        Me.repositoryItemTextEdit1.AutoHeight = False
        Me.repositoryItemTextEdit1.Mask.EditMask = "p"
        Me.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1"
        '
        'FormDemo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.gridControl1)
        Me.Controls.Add(Me.splitter1)
        Me.Controls.Add(Me.xtraTabControl1)
        Me.Name = "FormDemo"
        Me.Text = "FormDemo"
        CType(Me.xtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtraTabControl1.ResumeLayout(False)
        Me.xtraTabPage1.ResumeLayout(False)
        CType(Me.icbNewItemRow.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtraTabPage2.ResumeLayout(False)
        Me.panel1.ResumeLayout(False)
        Me.xtraTabPage3.ResumeLayout(False)
        CType(Me.chEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.icbButtons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtraTabPage4.ResumeLayout(False)
        CType(Me.icbSelectMode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel2.ResumeLayout(False)
        CType(Me.ceMultiSelect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.splitter1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repositoryItemLookUpEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents xtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Private WithEvents xtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Private WithEvents icbNewItemRow As DevExpress.XtraEditors.ImageComboBoxEdit
    Private WithEvents label1 As Label
    Private WithEvents xtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Private WithEvents lbEvent As Label
    Private WithEvents panel1 As Panel
    Private WithEvents sbStart As DevExpress.XtraEditors.SimpleButton
    Private WithEvents xtraTabPage3 As DevExpress.XtraTab.XtraTabPage
    Private WithEvents chEdit As DevExpress.XtraEditors.CheckEdit
    Private WithEvents icbButtons As DevExpress.XtraEditors.ImageComboBoxEdit
    Private WithEvents label2 As Label
    Private WithEvents xtraTabPage4 As DevExpress.XtraTab.XtraTabPage
    Private WithEvents icbSelectMode As DevExpress.XtraEditors.ImageComboBoxEdit
    Private WithEvents label3 As Label
    Private WithEvents panel2 As Panel
    Private WithEvents sbRecords As DevExpress.XtraEditors.SimpleButton
    Private WithEvents ceMultiSelect As DevExpress.XtraEditors.CheckEdit
    Private WithEvents splitter1 As DevExpress.XtraEditors.PanelControl
    Private WithEvents timer1 As Timer
    Private WithEvents imageList1 As ImageList
    Private WithEvents gridControl1 As DevExpress.XtraGrid.GridControl
    Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents gridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents repositoryItemLookUpEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Private WithEvents gridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents repositoryItemCalcEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Private WithEvents gridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents repositoryItemSpinEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Private WithEvents gridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents repositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
End Class
