Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid

''' <summary>
''' Summary description for PopupForm.
''' </summary>
Public Partial Class PopupForm
    Inherits DevExpress.XtraEditors.XtraForm
    Public Sub New()
        '
        ' Required for Windows Form Designer support
        '
        InitializeComponent()

        '
        ' TODO: Add any constructor code after InitializeComponent call
        '
    End Sub

    Public ReadOnly Property Row() As DataRow
        Get
            Return cardView1.GetDataRow(0)
        End Get
    End Property

    Private Sub InitLocation(ByVal frm As Form)
        Me.Top = frm.Top + (frm.Height - Me.Height) \ 2
        Me.Left = frm.Left + (frm.Width - Me.Width) \ 2
    End Sub

    Public Sub InitData(ByVal frm As Form, ByVal grid As GridControl, ByVal view As GridView, ByVal row As DataRow)
        InitLocation(frm)
        For Each col As GridColumn In view.Columns
            Dim column As GridColumn = cardView1.Columns.Add()
            column.Caption = col.GetTextCaption()
            column.FieldName = col.FieldName
            column.ColumnEdit = col.ColumnEdit
            column.DisplayFormat.Assign(col.DisplayFormat)
            column.VisibleIndex = col.VisibleIndex
        Next col
        Dim tbl As DataTable = (CType(grid.DataSource, DataView)).Table.Clone()
        tbl.Rows.Add(row.ItemArray)
        gridControl1.DataSource = tbl
        cardView1.FocusedColumn = cardView1.Columns(0)
    End Sub

    Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles simpleButton1.Click
        Row.EndEdit()
    End Sub
End Class
