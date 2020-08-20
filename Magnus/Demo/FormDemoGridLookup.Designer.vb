<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDemoGridLookup
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
        Me.GC1 = New DevExpress.XtraGrid.GridControl()
        Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumnID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnIDbarang = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemSearchLookUpEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.RepositoryItemSearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtQty = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.colPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnCatatan = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumnNoUrut = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.GC1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GC1
        '
        Me.GC1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GC1.Location = New System.Drawing.Point(0, 29)
        Me.GC1.MainView = Me.gridView1
        Me.GC1.Name = "GC1"
        Me.GC1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.txtQty, Me.RepositoryItemSearchLookUpEdit1})
        Me.GC1.Size = New System.Drawing.Size(800, 421)
        Me.GC1.TabIndex = 1
        Me.GC1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridView1})
        '
        'gridView1
        '
        Me.gridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumnID, Me.GridColumnIDbarang, Me.GridColumn1, Me.colUnit, Me.colQty, Me.colPrice, Me.colAmount, Me.GridColumnCatatan, Me.GridColumnNoUrut})
        Me.gridView1.GridControl = Me.GC1
        Me.gridView1.Name = "gridView1"
        Me.gridView1.OptionsView.ColumnAutoWidth = False
        Me.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.gridView1.OptionsView.ShowFooter = True
        Me.gridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumnID
        '
        Me.GridColumnID.Caption = "ID"
        Me.GridColumnID.FieldName = "ID"
        Me.GridColumnID.Name = "GridColumnID"
        Me.GridColumnID.OptionsColumn.AllowEdit = False
        Me.GridColumnID.OptionsColumn.AllowMove = False
        Me.GridColumnID.Visible = True
        Me.GridColumnID.VisibleIndex = 0
        Me.GridColumnID.Width = 35
        '
        'GridColumnIDbarang
        '
        Me.GridColumnIDbarang.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumnIDbarang.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumnIDbarang.Caption = "Kode Barang"
        Me.GridColumnIDbarang.ColumnEdit = Me.RepositoryItemSearchLookUpEdit1
        Me.GridColumnIDbarang.FieldName = "IDBarang"
        Me.GridColumnIDbarang.Name = "GridColumnIDbarang"
        Me.GridColumnIDbarang.OptionsColumn.AllowMove = False
        Me.GridColumnIDbarang.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "IDBarang", "Total Baris : {0}")})
        Me.GridColumnIDbarang.Visible = True
        Me.GridColumnIDbarang.VisibleIndex = 1
        Me.GridColumnIDbarang.Width = 100
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
        'GridColumn1
        '
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Nama Barang"
        Me.GridColumn1.FieldName = "NamaBarang"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.OptionsColumn.AllowMove = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 2
        Me.GridColumn1.Width = 200
        '
        'colUnit
        '
        Me.colUnit.AppearanceHeader.Options.UseTextOptions = True
        Me.colUnit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colUnit.Caption = "Unit"
        Me.colUnit.FieldName = "Unit"
        Me.colUnit.Name = "colUnit"
        Me.colUnit.OptionsColumn.AllowEdit = False
        Me.colUnit.OptionsColumn.AllowFocus = False
        Me.colUnit.OptionsColumn.AllowMove = False
        Me.colUnit.Visible = True
        Me.colUnit.VisibleIndex = 3
        Me.colUnit.Width = 50
        '
        'colQty
        '
        Me.colQty.AppearanceHeader.Options.UseTextOptions = True
        Me.colQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colQty.Caption = "Qty"
        Me.colQty.ColumnEdit = Me.txtQty
        Me.colQty.DisplayFormat.FormatString = "n2"
        Me.colQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colQty.FieldName = "Qty"
        Me.colQty.GroupFormat.FormatString = "n2"
        Me.colQty.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colQty.Name = "colQty"
        Me.colQty.OptionsColumn.AllowMove = False
        Me.colQty.Visible = True
        Me.colQty.VisibleIndex = 4
        Me.colQty.Width = 70
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
        'colPrice
        '
        Me.colPrice.AppearanceHeader.Options.UseTextOptions = True
        Me.colPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colPrice.Caption = "Harga"
        Me.colPrice.DisplayFormat.FormatString = "#,#"
        Me.colPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colPrice.FieldName = "Harga"
        Me.colPrice.Name = "colPrice"
        Me.colPrice.OptionsColumn.AllowEdit = False
        Me.colPrice.OptionsColumn.AllowFocus = False
        Me.colPrice.Width = 120
        '
        'colAmount
        '
        Me.colAmount.AppearanceHeader.Options.UseTextOptions = True
        Me.colAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colAmount.Caption = "Jumlah"
        Me.colAmount.DisplayFormat.FormatString = "#,#"
        Me.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colAmount.FieldName = "Jumlah"
        Me.colAmount.Name = "colAmount"
        Me.colAmount.OptionsColumn.AllowEdit = False
        Me.colAmount.OptionsColumn.AllowFocus = False
        Me.colAmount.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Jumlah", "Jumlah ={0:#,#}")})
        Me.colAmount.Width = 150
        '
        'GridColumnCatatan
        '
        Me.GridColumnCatatan.Caption = "Catatan"
        Me.GridColumnCatatan.FieldName = "Catatan"
        Me.GridColumnCatatan.Name = "GridColumnCatatan"
        Me.GridColumnCatatan.Visible = True
        Me.GridColumnCatatan.VisibleIndex = 5
        Me.GridColumnCatatan.Width = 199
        '
        'GridColumnNoUrut
        '
        Me.GridColumnNoUrut.Caption = "NoUrut"
        Me.GridColumnNoUrut.FieldName = "NoUrut"
        Me.GridColumnNoUrut.Name = "GridColumnNoUrut"
        Me.GridColumnNoUrut.Visible = True
        Me.GridColumnNoUrut.VisibleIndex = 6
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
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(3, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.Button1)
        Me.FlowLayoutPanel1.Controls.Add(Me.Button2)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(800, 29)
        Me.FlowLayoutPanel1.TabIndex = 3
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(84, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'FormDemoGridLookup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.GC1)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Name = "FormDemoGridLookup"
        Me.Text = "FormDemo2"
        CType(Me.GC1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents GC1 As DevExpress.XtraGrid.GridControl
    Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents GridColumnIDbarang As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents colUnit As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents colQty As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents txtQty As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Private WithEvents colPrice As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents colAmount As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents colId As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents colName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemSearchLookUpEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents RepositoryItemSearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnCatatan As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumnNoUrut As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Button1 As Button
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents Button2 As Button
End Class
