Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

Public Class FormDemo
    Private updateLayout As Boolean = False
    Private firstSearch As Boolean = True
    Private searshString As String() = New String() {"c", "a", "{BS}", "h", "a", "n", "{BS}", "{BS}", "e", "f", " ", "a", "n", "t", "o", "n", "'", "s", " ", "c", "^{DOWN}", "^{DOWN}", "^{DOWN}", "^{DOWN}", "^{DOWN}", "^{DOWN}", "^{HOME}", "{RIGHT}"}
    Private searchKeyIndex As Integer = 0
    Private Sub FormDemo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitData()
        InitEditing()
        gridView1_Layout(gridView1, EventArgs.Empty)
    End Sub

#Region "Init"
    'Public Overrides ReadOnly Property ExportView() As BaseView
    '    Get
    '        Return gridView1
    '    End Get
    'End Property

    'Public Overrides ReadOnly Property ShowViewOptions() As Boolean
    '    Get
    '        Return True
    '    End Get
    'End Property
    Dim Sql As String
    Private Sub InitData()

        Dim ds As DataSet = New DataSet()
        Dim tblGrid As String = "[Details]"
        Dim tblLookUp As String = "Products"
        Using d As New WaitDialogForm("Loading Order Details...")
            Refresher()
            d.SetCaption("Loading Products...")
            Sql = "SELECT ID ProductID,Kode,Nama ProductName  " &
                               " From MBarang MB " &
                               " Where IsActive=1 ORDER BY MB.Nama ASC"
            ds = Query.ExecuteDataSet(Sql, tblLookUp)
            If Not ds Is Nothing Then
                repositoryItemLookUpEdit1.DataSource = ds.Tables(tblLookUp)
                ds.Dispose()
            End If
        End Using
    End Sub


    Dim repckedit As New RepositoryItemCheckEdit
    Dim repdateedit As New RepositoryItemDateEdit
    Dim reptextedit As New RepositoryItemTextEdit
    Dim reppicedit As New RepositoryItemPictureEdit
    Sub Refresher()
        Dim ds As New DataSet
        Dim tblGrid As String = "[Order Details]"
        Try
            'TD.Harga,TD.Jumlah,
            Sql = "Select TD.ID, TD.IDBarang ,MB.Nama NamaBarang,TD.Qty,TD.Catatan
                                    From TStokMasuk T Inner Join TStokMasukD TD on T.ID=TD.IDStokMasuk 
                                    Inner Join MBarang MB on MB.ID=TD.IDBarang 
                                    Where T.ID=1"
            ds = Query.ExecuteDataSet(Sql, tblGrid)
            Dim dvManager As DataViewManager = New DataViewManager(ds)
            Dim dv As DataView = dvManager.CreateDataView(ds.Tables(tblGrid))

            If Not ds Is Nothing Then
                gridControl1.DataSource = dv
                ds.Dispose()
                'gridControl1.Refresh()

                'gridView1.OptionsView.ColumnAutoWidth = False
                'gridView1.OptionsView.BestFitMaxRowCount = -1
                'gridView1.BestFitColumns()

                'With gridView1
                '    For i As Integer = 0 To .Columns.Count - 1
                '        Select Case gridView1.Columns(i).ColumnType.Name.ToLower
                '            Case "int32", "int64", "int"
                '                .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                '                .Columns(i).DisplayFormat.FormatString = "n0"
                '            Case "decimal", "single", "double", "numeric"
                '                .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                '                .Columns(i).DisplayFormat.FormatString = "n2"
                '            Case "money"
                '                .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                '                .Columns(i).DisplayFormat.FormatString = "c2"
                '            Case "string"
                '                .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
                '                .Columns(i).DisplayFormat.FormatString = ""
                '            Case "date", "datetime"
                '                .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                '                .Columns(i).DisplayFormat.FormatString = "dd-MM-yyyy"
                '            Case "byte[]"
                '                reppicedit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
                '                .Columns(i).OptionsColumn.AllowGroup = False
                '                .Columns(i).OptionsColumn.AllowSort = False
                '                .Columns(i).OptionsFilter.AllowFilter = False
                '                .Columns(i).ColumnEdit = reppicedit
                '            Case "boolean", "bit"
                '                .Columns(i).ColumnEdit = repckedit
                '        End Select
                '        If .Columns(i).FieldName.Length >= 4 AndAlso .Columns(i).FieldName.Substring(0, 4).ToLower = "Kode".ToLower Then
                '            .Columns(i).Fixed = FixedStyle.Left
                '        ElseIf .Columns(i).FieldName.ToLower = "Nama".ToLower Then
                '            .Columns(i).Fixed = FixedStyle.Left
                '        ElseIf .Columns(i).FieldName.ToLower = "Kurs" Then
                '            .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                '            .Columns(i).DisplayFormat.FormatString = "n4"
                '        End If
                '    Next
                'End With
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub InitEditing()
        icbNewItemRow.Properties.Items.Add(New ImageComboBoxItem("None", NewItemRowPosition.None, -1))
        icbNewItemRow.Properties.Items.Add(New ImageComboBoxItem("Top", NewItemRowPosition.Top, -1))
        icbNewItemRow.Properties.Items.Add(New ImageComboBoxItem("Bottom", NewItemRowPosition.Bottom, -1))
        xtraTabControl1.SelectedTabPageIndex = 0
        icbButtons.Properties.Items.Add(New ImageComboBoxItem("Default", ShowButtonModeEnum.Default, -1))
        icbButtons.Properties.Items.Add(New ImageComboBoxItem("Show Always", ShowButtonModeEnum.ShowAlways, -1))
        icbButtons.Properties.Items.Add(New ImageComboBoxItem("Show For Focused Cell", ShowButtonModeEnum.ShowForFocusedCell, -1))
        icbButtons.Properties.Items.Add(New ImageComboBoxItem("Show For Focused Row", ShowButtonModeEnum.ShowForFocusedRow, -1))
        icbButtons.Properties.Items.Add(New ImageComboBoxItem("Show Only In Editor", ShowButtonModeEnum.ShowOnlyInEditor, -1))
        icbButtons.EditValue = gridView1.ShowButtonMode
        icbSelectMode.Properties.Items.Add(New ImageComboBoxItem("Row Select", GridMultiSelectMode.RowSelect, -1))
        icbSelectMode.Properties.Items.Add(New ImageComboBoxItem("Cell Select", GridMultiSelectMode.CellSelect, -1))
        icbSelectMode.EditValue = gridView1.OptionsSelection.MultiSelectMode
        chEdit.Checked = gridView1.OptionsBehavior.Editable
    End Sub
#End Region
#Region "Editing"

    Private Sub gridView1_Layout(ByVal sender As Object, ByVal e As System.EventArgs)
        updateLayout = True
        icbNewItemRow.EditValue = gridView1.OptionsView.NewItemRowPosition
        ceMultiSelect.Checked = gridView1.OptionsSelection.MultiSelect
        SetPosition()
        updateLayout = False
    End Sub

    Private Sub xtraTabControl1_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtraTabControl1.SelectedPageChanged
        If "search".Equals(e.Page.Tag) Then
            gridView1.OptionsBehavior.AllowIncrementalSearch = True
            If firstSearch Then
                StartSearch()
            End If
            firstSearch = False
        Else
            gridView1.OptionsBehavior.AllowIncrementalSearch = False
            If (Not firstSearch) Then
                StopSearch()
            End If
        End If
        gridControl1.UseEmbeddedNavigator = "editing".Equals(e.Page.Tag)
        ceMultiSelect.Checked = "multiselect".Equals(e.Page.Tag)
    End Sub
#End Region
#Region "NewItemRow"
    Private Sub icbNewItemRow_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles icbNewItemRow.SelectedIndexChanged
        If updateLayout Then
            Return
        End If
        gridView1.OptionsView.NewItemRowPosition = CType(icbNewItemRow.EditValue, NewItemRowPosition)
        SetPosition()
        'GridRibbonMenuManager.RefreshOptionsMenu(gridView1)
    End Sub

    Private Sub SetPosition()
        If gridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom AndAlso gridView1.SortInfo.GroupCount = 0 Then
            gridView1.FocusedRowHandle = gridView1.RowCount - 2
            gridView1.MakeRowVisible(gridView1.FocusedRowHandle, False)
        End If
    End Sub

    Private Sub gridView1_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs)
        Dim row As DataRow = gridView1.GetDataRow(e.RowHandle)
        Try
            row("Quantity") = 1
            row("UnitPrice") = 0
            row("Discount") = 0
            row("OrderID") = 99999
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#Region "Incremental Search"

    Private Sub StartSearch()
        sbStart.Enabled = False
        searchKeyIndex = 0
        lbEvent.Text = ConstStrings.TableView_AutoSearch
        gridView1.FocusedColumn = gridColumn2
        timer1.Start()
    End Sub

    Private Sub StopSearch()
        sbStart.Enabled = True
        timer1.Stop()
        lbEvent.Text = ConstStrings.TableView_IncrementalSearch
    End Sub



    Private Sub timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timer1.Tick

    End Sub

    Private Sub gridControl1_ProcessGridKey(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Escape Then
            StopSearch()
        End If
    End Sub

    Private Sub gridControl1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        StopSearch()
    End Sub

    Private Sub sbStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sbStart.Click
        StartSearch()
    End Sub
#End Region
#Region "Editing And Navigation"
    Private Sub icbButtons_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles icbButtons.SelectedIndexChanged
        gridView1.ShowButtonMode = CType(icbButtons.EditValue, ShowButtonModeEnum)
    End Sub

    Private Sub chEdit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chEdit.CheckedChanged
        gridView1.OptionsBehavior.Editable = chEdit.Checked
    End Sub

    Private Function EditRecord() As Boolean
        Dim row As DataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle)
        If row Is Nothing Then
            Return False
        End If
        Dim frm As PopupForm = New PopupForm()
        frm.InitData(Me.FindForm(), gridControl1, gridView1, row)
        Dim ret As Boolean = frm.ShowDialog() = System.Windows.Forms.DialogResult.OK
        If ret Then
            row.ItemArray = frm.Row.ItemArray
            row.EndEdit()
        End If
        Return ret
    End Function

    Private Sub gridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles gridControl1.EmbeddedNavigator.ButtonClick
        If e.Button.ButtonType = DevExpress.XtraEditors.NavigatorButtonType.Edit Then
            EditRecord()
            e.Handled = True
        End If
        If e.Button.ButtonType = DevExpress.XtraEditors.NavigatorButtonType.Append Then
            gridView1.AddNewRow()
            If EditRecord() Then
                gridView1.UpdateCurrentRow()
            Else
                gridView1.CancelUpdateCurrentRow()
            End If
            e.Handled = True
        End If
    End Sub
#End Region
#Region "MultiSelect"
    Private Sub UpdateSelection()
        Dim updateCells As Boolean = gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect
        If ceMultiSelect.Checked Then
            If (Not updateCells) Then
                gridView1.SelectRange(4, 30)
                sbRecords.Text = "Show Selected Records"
            Else
                gridView1.ClearSelection()
                gridView1.SelectCells(1, gridView1.Columns("ProductID"), 11, gridView1.Columns("Quantity"))
                sbRecords.Text = "Show Selected Values"
            End If
        End If
    End Sub
    Private Sub ceMultiSelect_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ceMultiSelect.CheckedChanged
        SetButtonEnabled()
        If updateLayout Then
            Return
        End If
        gridView1.OptionsSelection.MultiSelect = ceMultiSelect.Checked
        UpdateSelection()
        'GridRibbonMenuManager.RefreshOptionsMenu(gridView1)
    End Sub

    Private Sub SetButtonEnabled()
        sbRecords.Enabled = gridView1.SelectedRowsCount > 0 AndAlso ceMultiSelect.Checked
        icbSelectMode.Enabled = ceMultiSelect.Checked
    End Sub

    Private Sub gridView1_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs)
        SetButtonEnabled()
    End Sub

    Private Sub sbRecords_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sbRecords.Click
        If gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect Then
            '    DemosHelper.ShowDescriptionForm(Control.MousePosition, GetSelectedRows(gridView1), "Selected Rows")
            'Else
            '    DemosHelper.ShowDescriptionForm(Control.MousePosition, GetSelectedRows(gridView1), "Selected Cells")
        End If
    End Sub

    Private Function GetSelectedRows(ByVal view As GridView) As String
        Dim ret As String = ""
        Dim rowIndex As Integer = -1
        If view.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect Then
            For Each i As Integer In gridView1.GetSelectedRows()
                Dim row As DataRow = gridView1.GetDataRow(i)
                If ret <> "" Then
                    ret &= Constants.vbCrLf
                End If
                ret &= String.Format("Order: #{0} {1}", row("OrderID"), gridView1.GetRowCellDisplayText(i, gridColumn2))
            Next i
        Else
            For Each cell As GridCell In view.GetSelectedCells()
                If rowIndex <> cell.RowHandle Then
                    If ret <> "" Then
                        ret &= Constants.vbCrLf
                    End If
                    ret &= String.Format("Row: #{0}", cell.RowHandle)
                End If
                ret &= Constants.vbCrLf & "    " & view.GetRowCellDisplayText(cell.RowHandle, cell.Column)
                rowIndex = cell.RowHandle
            Next cell
        End If
        Return ret
    End Function
    Private Sub icbSelectMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles icbSelectMode.SelectedIndexChanged
        gridView1.OptionsSelection.MultiSelectMode = CType(icbSelectMode.EditValue, GridMultiSelectMode)
        UpdateSelection()
    End Sub
#End Region
End Class
Public Class ConstStrings
    'warning
    Public Const ActiveGrid_Warning As String = "This operation might take a few minutes." & Constants.vbCrLf & "Are you sure you want to continue?"
    'Master-Detail
    Public Const ActiveGrid_BestFit As String = "To apply Best Fit to a column" & Constants.vbCrLf & " double-click its right edge."
    Public Const ActiveGrid_SynhronizedViews As String = "By default the XtraGrid synchronizes the layout of the columns in detail views." & Constants.vbCrLf & "The SynhronizeClones property can be used to change the default behavior."
    Public Const ActiveGrid_ViewZoom As String = "You can zoom in on any grid view" & Constants.vbCrLf & "by clicking the Zoom button."
    Public Const ActiveGrid_ViewZoomSubDetail As String = "Click this button " & Constants.vbCrLf & "to zoom in on the current detail view."
    Public Const ActiveGrid_ViewUnzoom As String = "To display the master view again just click the Zoom Out button."
    Public Const ActiveGrid_JoinedGroupPanel As String = "Grouping columns can be optionally displayed" & Constants.vbCrLf & "within the current detail's group panel..."
    Public Const ActiveGrid_JoinedGroupPanel2 As String = "...or within the main view's group panel."
    Public Const ActiveGrid_EmbeddedNavigator As String = "The Embedded Navigator helps end-users" & Constants.vbCrLf & "to navigate through the grid." & Constants.vbCrLf & "You can display or hide any button."
    '100k records
    Public Const ActiveGrid_100kRecords_DataReloading As String = "Data reloading: {0} sec."
    Public Const ActiveGrid_100kRecords_CurrencySorting As String = "Sorting by the Currency column: {0} sec."
    Public Const ActiveGrid_100kRecords_TextGrouping As String = "Grouping by Text column: {0} sec."
    Public Const ActiveGrid_100kRecords_DateGrouping As String = "Grouping by the Text and Date columns: {0} sec."
    Public Const ActiveGrid_100kRecords_ClearGrouping As String = "Clear grouping: {0} sec."
    'Folder
    Public Const ActiveGrid_Folder_Sorting As String = "When sorting is applied to a view, first the Folders and then the Files are sorted." & Constants.vbCrLf & "This custom sorting algorithm is implemented via a single event."
    Public Const ActiveGrid_Folder_IncrementalSearch As String = "The incremental search feature " & Constants.vbCrLf & "can be enabled by setting a single property." & Constants.vbCrLf & "Experiment with the search " & Constants.vbCrLf & "by typing directly within the cells."
    'Table View
    Public Const TableView_AutoSearch As String = "Press ESC to stop the automatic incremental search."
    Public Const TableView_IncrementalSearch As String = "Use Ctrl+Down to find the next match. Use Ctrl+Up to find the previous match."
End Class