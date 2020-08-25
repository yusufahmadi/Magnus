Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors

Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmLookup
    Public Strsql As String = ""
    Public NoID As Integer = -1
    Public Kode As String = ""
    Public Nama As String = ""
    Dim oda2 As SqlDataAdapter
    Public Shared ds As New DataSet
    Public FormName As String = ""
    Public NamaFormPemanggil As String = ""
    Public repckedit As New RepositoryItemCheckEdit
    Public BolehAmbilData As Boolean = False
    Public row As System.Data.DataRow
    Private Sub SimpleButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton6.Click
        Batal()
    End Sub
    Public Sub Batal()
        DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Public Overridable Sub AmbilData()
        Dim view As ColumnView = GC1.FocusedView
        Try
            row = view.GetDataRow(GV1.FocusedRowHandle)
            Dim dc As Integer = GV1.FocusedRowHandle
            NoID = Utils.ObjToInt(row("ID"))
            Kode = row("Kode").ToString
            Nama = row("Nama").ToString
            DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            XtraMessageBox.Show("Invalid Operation", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Public Overridable Sub SimpleButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        AmbilData()
    End Sub
    Public Overridable Sub RefreshData()
        Dim cn As New SqlConnection(conStr)
        Dim ocmd2 As New SqlCommand
        Try
            ocmd2.Connection = cn
            'Else
            'ocmd2.CommandText = "select * from " & TableName & " where noid=" & NoID.ToString
            'End If
            cn.Open()
            oda2 = New SqlDataAdapter(ocmd2)
            ocmd2.CommandText = Strsql

            oda2 = New SqlDataAdapter(ocmd2)
            If ds.Tables("MDetil") Is Nothing Then
            Else
                ds.Tables("MDetil").Clear()
            End If
            oda2.Fill(ds, "MDetil")
            GC1.DataSource = ds.Tables("MDetil")

            ocmd2.Dispose()
            cn.Close()
            cn.Dispose()
            For i As Integer = 0 To GV1.Columns.Count - 1
                ' MsgBox(GV1.Columns(i).fieldname.ToString)
                Select Case GV1.Columns(i).ColumnType.Name.ToLower
                    Case "int32", "int64", "int"
                        GV1.Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GV1.Columns(i).DisplayFormat.FormatString = "n2"
                    Case "decimal", "single", "money", "double"
                        GV1.Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        GV1.Columns(i).DisplayFormat.FormatString = "n2"
                    Case "string"
                        GV1.Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
                        GV1.Columns(i).DisplayFormat.FormatString = ""
                    Case "date", "datetime"
                        GV1.Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                        GV1.Columns(i).DisplayFormat.FormatString = "dd-MM-yyyy"
                        If GV1.Columns(i).FieldName.ToLower = "jam" Then
                            GV1.Columns(i).DisplayFormat.FormatString = "HH:mm:ss"
                        End If
                    Case "boolean"
                        GV1.Columns(i).ColumnEdit = repckedit

                End Select
            Next
            'If System.IO.File.Exists(folderLayouts &  FormName & "Grid Detil.xml") Then
            '    GridView1.RestoreLayoutFromXml(folderLayouts &  FormName & ".xml")
            'End If
            If System.IO.File.Exists(FolderLayouts & FormName & " Lookup.xml") Then
                GV1.RestoreLayoutFromXml(FolderLayouts & FormName & " Lookup.xml")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Overridable Sub frmLookup_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            GV1.SaveLayoutToXml(FolderLayouts & FormName & " Lookup.xml")
            ds.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Public Overridable Sub frmLookup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RefreshData()
        GV1.ShowFindPanel()
        'FungsiControl.SetForm(Me)
    End Sub

    Private Sub PanelControl2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PanelControl2.Paint

    End Sub

    Private Sub GC1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GC1.Click

    End Sub

    Public Overridable Sub GC1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GC1.DoubleClick
        If BolehAmbilData Then
            If btnOK.Visible = True Then
                AmbilData()
            End If
        End If
    End Sub

    Public Overridable Sub GC1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GC1.KeyDown
        If e.KeyCode = Keys.Enter Then
            AmbilData()
        ElseIf e.KeyCode = Keys.Escape Then
            DialogResult = Windows.Forms.DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub GC1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GC1.MouseDown
        Dim HI As New DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HI = GV1.CalcHitInfo(e.X, e.Y)
        If HI.InRow Then
            If FormName.ToUpper = "DetailManifest".ToUpper Or FormName.ToUpper = "DetailPenagihan".ToUpper Then
                BolehAmbilData = False
            Else
                BolehAmbilData = True
            End If
        Else
            BolehAmbilData = False
        End If
    End Sub

    Private Sub GV1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GV1.MouseDown
        If FormName.ToUpper = "DetailManifest".ToUpper Or FormName.ToUpper = "DetailPenagihan".ToUpper Then
            Dim View As GridView = CType(sender, GridView)
            If View Is Nothing Then Return
            ' obtaining hit info
            Dim hitInfo As GridHitInfo = View.CalcHitInfo(New System.Drawing.Point(e.X, e.Y))
            If (e.Button = Windows.Forms.MouseButtons.Right) And (hitInfo.InRow) And
              (Not View.IsGroupRow(hitInfo.RowHandle)) Then
                PopupMenu1.ShowPopup(Control.MousePosition)
            End If
        End If
    End Sub

End Class