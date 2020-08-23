Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports Magnus.Utils

Partial Public Class FormDaftar
    Inherits DevExpress.XtraEditors.XtraForm

    Public idFrm As IDForm
    Public Enum IDForm
        F_User = 0
        F_RoleUser = 1
        F_MBarang = 2
        F_MKategori = 3
        F_MKategoriBiaya = 4
        F_MKaryawan = 5
        F_TypeTaffeta = 6
    End Enum
    Public NamaForm As String = "Undefined"
    Private Sub FormDaftar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowMenuRoleUser()
        BarButtonRefresh.PerformClick()
    End Sub

    Sub ShowMenuRoleUser()
        Dim newList As List(Of MenuRoleUser) = listMenu.Where(Function(m) m.NamaForm = Me.NamaForm).[Select](Function(m) New MenuRoleUser With {
            .Caption = m.Caption,
            .IsBaru = m.IsBaru,
            .IsUbah = m.IsUbah,
            .IsHapus = m.IsHapus,
            .IsCetak = m.IsCetak,
            .IsExport = m.IsExport}).ToList()
        BarButtonBaru.Enabled = newList(0).IsBaru
        BarButtonUbah.Enabled = newList(0).IsUbah
        BarButtonHapus.Enabled = newList(0).IsHapus
        BarButtonExport.Enabled = newList(0).IsExport
        BarButtonCetak.Enabled = newList(0).IsCetak
        Me.Text = IIf(newList(0).Caption = "", NamaForm, newList(0).Caption).ToString
    End Sub

    Private Sub BarButtonRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonRefresh.ItemClick
        Refresher()
    End Sub

    Dim repckedit As New RepositoryItemCheckEdit
    Dim repdateedit As New RepositoryItemDateEdit
    Dim reptextedit As New RepositoryItemTextEdit
    Dim reppicedit As New RepositoryItemPictureEdit
    Sub Refresher()
        Dim ds As New DataSet
        Dim sql As String = ""
        Select Case idFrm
            Case IDForm.F_User
                sql = "Select A.*,B.Nama As Role,C.Nama As TypeLayout From MUser A Left Join (MRoleUser B Left Join MTypeLayout C on B.IDTypeLayout=C.ID) on A.IDRoleUser=B.ID Order By A.Username"
            Case IDForm.F_RoleUser
                sql = "Select R.ID,R.Kode,R.Nama,R.Keterangan,R.IsActive,T.Nama TypeLayout,R.MenuSettingUser From MRoleUser R Left Join MTypeLayout T On R.IDTypeLayout=T.ID "
            Case IDForm.F_MBarang
                If IDTypeLayout = 1 Then 'A.P,A.L,A.T,A.Isi,
                    sql = "Select A.ID,B.Kode + ' - ' + B.Nama KodeKategori, A.Kode,A.Nama,A.Catatan,A.IsActive,A.HargaBeli,A.TanggalBuat,A.UserBuat,A.TanggalUbah,A.UserUbah "
                Else
                    sql = "Select A.ID,B.Kode KodeKategori,B.Nama Kategori, A.Kode,A.Nama,A.Catatan,A.IsActive,A.TanggalBuat,A.UserBuat,A.TanggalUbah,A.UserUbah "
                End If
                sql = sql & " From MBarang A Left Join MKategori B on A.IDKategori=B.ID "
            Case IDForm.F_MKategori
                sql = "Select * From MKategori "
            Case IDForm.F_MKategoriBiaya
                sql = "Select ID,ID Kode,Nama,Keterangan,IsActive From MAkun Where IDAkunLv2='0601' "
            Case IDForm.F_MKaryawan
                sql = "Select * From MKaryawan "
            Case IDForm.F_TypeTaffeta
                sql = "Select * From MTaffeta "
        End Select
        ds = Query.ExecuteDataSet(sql)
        If Not ds Is Nothing Then
            GridControl1.DataSource = ds.Tables(0)
            ds.Dispose()
            GridControl1.Refresh()

            GridView1.OptionsView.ColumnAutoWidth = False
            GridView1.OptionsView.BestFitMaxRowCount = -1
            GridView1.BestFitColumns()
            With GridView1
                For i As Integer = 0 To .Columns.Count - 1
                    Select Case GridView1.Columns(i).ColumnType.Name.ToLower
                        Case "int32", "int64", "int"
                            .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .Columns(i).DisplayFormat.FormatString = "n0"
                        Case "decimal", "single", "double", "numeric"
                            .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .Columns(i).DisplayFormat.FormatString = "n2"
                        Case "money"
                            .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .Columns(i).DisplayFormat.FormatString = "c2"
                        Case "string"
                            .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
                            .Columns(i).DisplayFormat.FormatString = ""
                        Case "date", "datetime"
                            .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                            .Columns(i).DisplayFormat.FormatString = "dd-MM-yyyy"
                        Case "byte[]"
                            reppicedit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
                            .Columns(i).OptionsColumn.AllowGroup = False
                            .Columns(i).OptionsColumn.AllowSort = False
                            .Columns(i).OptionsFilter.AllowFilter = False
                            .Columns(i).ColumnEdit = reppicedit
                        Case "boolean", "bit"
                            .Columns(i).ColumnEdit = repckedit
                    End Select
                    If .Columns(i).FieldName.Length >= 4 AndAlso .Columns(i).FieldName.Substring(0, 4).ToLower = "Kode".ToLower Then
                        .Columns(i).Fixed = FixedStyle.Left
                    ElseIf .Columns(i).FieldName.ToLower = "Nama".ToLower Then
                        .Columns(i).Fixed = FixedStyle.Left
                    ElseIf .Columns(i).FieldName.ToLower = "Kurs" Then
                        .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        .Columns(i).DisplayFormat.FormatString = "n4"
                    End If
                Next
            End With
        End If
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonBaru.ItemClick
        Baru()
    End Sub
    Sub Baru()
        Select Case idFrm
            Case IDForm.F_User
                Using f As New FormUser
                    f._IsNew = True
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                    End If
                End Using
            Case IDForm.F_RoleUser
                Using f As New FormRoleUser
                    f._IsNew = True
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                    End If
                End Using
            Case IDForm.F_MBarang
                Using f As New FormBarang
                    f._IsNew = True
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                    End If
                End Using
            Case IDForm.F_MKategori
                Using f As New FormBasic
                    f._IsNew = True
                    f.FormName = "Kategori"
                    f.TableName = "MKategori"
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                    End If
                End Using
            Case IDForm.F_MKategoriBiaya
                Using f As New FormKategoriBiaya
                    f._IsNew = True
                    f.FormName = "Kategori Biaya"
                    f.TableName = "MAkun"
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                    End If
                End Using
            Case IDForm.F_MKaryawan
                Using f As New FormKaryawan
                    f._IsNew = True
                    f.FormName = "Karyawan"
                    f.TableName = "MKaryawan"
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                    End If
                End Using
            Case IDForm.F_TypeTaffeta
                Using f As New FormTaffeta
                    f._IsNew = True
                    f.FormName = "Taffeta"
                    f.TableName = "MTaffeta"
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                    End If
                End Using
        End Select
    End Sub

    Private Sub BarButtonUbah_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonUbah.ItemClick
        If GridView1.RowCount > 0 Then
            Ubah()
        End If
    End Sub

    Sub Ubah()
        Dim view As ColumnView = GridControl1.FocusedView
        Select Case idFrm
            Case IDForm.F_User
                Using f As New FormUser
                    f._IsNew = False
                    f._User = view.GetDataRow(GridView1.FocusedRowHandle)("Username").ToString
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                        GridView1.ClearSelection()
                        GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("Username"), f._User)
                        GridView1.SelectRow(GridView1.FocusedRowHandle)
                    End If
                End Using
            Case IDForm.F_RoleUser
                Using f As New FormRoleUser
                    f._IsNew = False
                    f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                        GridView1.ClearSelection()
                        GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), f._ID.ToString)
                        GridView1.SelectRow(GridView1.FocusedRowHandle)
                    End If
                End Using
            Case IDForm.F_MBarang
                Using f As New FormBarang
                    f._IsNew = False
                    f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                        GridView1.ClearSelection()
                        GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), f._ID.ToString)
                        GridView1.SelectRow(GridView1.FocusedRowHandle)
                    End If
                End Using
            Case IDForm.F_MKategori
                Using f As New FormBasic
                    f._IsNew = False
                    f.FormName = "Kategori"
                    f.TableName = "MKategori"
                    f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                        GridView1.ClearSelection()
                        GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), f._ID.ToString)
                        GridView1.SelectRow(GridView1.FocusedRowHandle)
                    End If
                End Using
            Case IDForm.F_MKategoriBiaya
                Using f As New FormKategoriBiaya
                    f._IsNew = False
                    f.FormName = "Kategori Biaya"
                    f.TableName = "MAkun"
                    f._ID = NullToStr(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                        GridView1.ClearSelection()
                        GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), f._ID.ToString)
                        GridView1.SelectRow(GridView1.FocusedRowHandle)
                    End If
                End Using
            Case IDForm.F_MKaryawan
                Using f As New FormKaryawan
                    f._IsNew = False
                    f.FormName = "Karyawan"
                    f.TableName = "MKaryawan"
                    f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                        GridView1.ClearSelection()
                        GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), f._ID.ToString)
                        GridView1.SelectRow(GridView1.FocusedRowHandle)
                    End If
                End Using
            Case IDForm.F_TypeTaffeta
                Using f As New FormTaffeta
                    f._IsNew = False
                    f.FormName = "Taffeta"
                    f.TableName = "MTaffeta"
                    f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                    If f.ShowDialog() = DialogResult.OK Then
                        BarButtonRefresh.PerformClick()
                        GridView1.ClearSelection()
                        GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), f._ID.ToString)
                        GridView1.SelectRow(GridView1.FocusedRowHandle)
                    End If
                End Using
        End Select
    End Sub

    Private Sub BarButtonHapus_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonHapus.ItemClick
        If GridView1.RowCount > 0 Then
            Hapus()
        End If
    End Sub

    Sub Hapus()
        Dim view As ColumnView = GridControl1.FocusedView
        Dim ID As Integer = 0
        If IDForm.F_User <> idFrm Then
            ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
        End If
        Select Case idFrm
            Case IDForm.F_User
                Dim User As String = ""
                User = view.GetDataRow(GridView1.FocusedRowHandle)("Username").ToString
                Dim f As Pesan = Query.DeleteDataMaster("MUser", "Username='" & User.Trim & "'")
                If f.Hasil = True Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                    BarButtonRefresh.PerformClick()

                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("Username"), User.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                End If
            Case IDForm.F_RoleUser
                If ID = 1 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, "Role user utama tidak dapat dinonaktifkan/dihapus.", NamaAplikasi)
                    Exit Select
                End If
                Dim f As Pesan = Query.DeleteDataMaster("MRoleUser", "ID=" & ID & "")
                If f.Hasil = True Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                End If
            Case IDForm.F_MBarang
                Dim f As Pesan = Query.DeleteDataMaster("MBarang", "ID=" & ID & "")
                If f.Hasil = True Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                End If
            Case IDForm.F_MKategori
                Dim f As Pesan = Query.DeleteDataMaster("MKategori", "ID=" & ID & "")
                If f.Hasil = True Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                End If
            Case IDForm.F_MKategoriBiaya
                Dim f As Pesan = Query.DeleteDataMaster("MKategoriBiaya", "ID=" & ID & "")
                If f.Hasil = True Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                End If
            Case IDForm.F_MKaryawan
                Dim f As Pesan = Query.DeleteDataMaster("MKaryawan", "ID=" & ID & "")
                If f.Hasil = True Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                End If
            Case IDForm.F_TypeTaffeta
                Dim f As Pesan = Query.DeleteDataMaster("MTaffeta", "ID=" & ID & "")
                If f.Hasil = True Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, f.Message, NamaAplikasi)
                End If
        End Select
    End Sub

    Private Sub BarButtonExport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonExport.ItemClick
        Using dlgsave As New SaveFileDialog
            dlgsave.Title = "Export " & Me.Text & " ke Excel"
            dlgsave.Filter = "Excel Files|*.xls"
            If dlgsave.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                GridControl1.ExportToXls(dlgsave.FileName)
                BukaFile(dlgsave.FileName)
            End If
            dlgsave.Dispose()
        End Using
    End Sub

    Private Sub BarButtonCetak_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonCetak.ItemClick
        Cetak()
    End Sub
    Sub Cetak()
        Dim action As ActionReport = Nothing
        If (IsEditReport) Then
            action = ActionReport.Edit
        Else
            action = ActionReport.Preview
        End If
        Dim PathFile As String = Application.StartupPath & "\Report\"
        Dim str, operasi As String()
        Dim RgText As String = ""
        Dim CalcRb As String = ""
        Dim CalcField As String = ""

        Dim namafile As String = "Print" & NamaForm & ".repx"
        PathFile &= namafile
        If IO.File.Exists(PathFile) OrElse action = ActionReport.Edit Then
            Refresher()
            str = (CalcRb).ToString().Split("|")
            For i = 0 To UBound(str)
                operasi = str(i).Split(";")
            Next

            If (CalcField) <> "" Then
                RgText = "|" & "Radio=String='Status: " & RgText & "'"
            Else
                RgText = "" & "Radio=String='Status: " & RgText & "'"
            End If
            clsDXReport.ViewXtraReport(Me.MdiParent, action, PathFile, NamaPerusahaan, namafile, GCtoDSRowFiltered(GridControl1), , CalcField & RgText)
        Else
            clsDXReport.NewPreview(NamaForm, GridControl1, Me.Text, GridView1.ActiveFilterString.Replace("[", "").Replace("]", ""))
        End If
    End Sub
    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        If BarButtonUbah.Enabled Then
            Ubah()
        End If
    End Sub
End Class