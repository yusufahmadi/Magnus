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

    Sub Refresher()
        Dim ds As New DataSet
        Dim sql As String = ""
        Select Case idFrm
            Case IDForm.F_User
                sql = "Select A.*,B.Nama As Role,C.Nama As TypeLayout From MUser A Left Join (MRoleUser B Left Join MTypeLayout C on B.IDTypeLayout=C.ID) on A.IDRoleUser=B.ID Order By A.Username"
            Case IDForm.F_RoleUser
                sql = "Select R.ID,R.Kode,R.Nama,R.Keterangan,R.IsActive,T.Nama TypeLayout,R.MenuSettingUser From MRoleUser R Left Join MTypeLayout T On R.IDTypeLayout=T.ID "
            Case IDForm.F_MBarang
                If IDTypeLayout = 1 Then
                    sql = "Select A.ID,B.Kode KodeKategori,B.Nama Kategori, A.Kode,A.Nama,A.Catatan,A.HargaBeli,A.P,A.L,A.T,A.Isi,A.TanggalBuat,A.UserBuat,A.TanggalUpdate,A.UserUpdate "
                Else
                    sql = "Select A.ID,B.Kode KodeKategori,B.Nama Kategori, A.Kode,A.Nama,A.Catatan,A.P,A.L,A.T,A.Isi,A.TanggalBuat,A.UserBuat,A.TanggalUpdate,A.UserUpdate "
                End If
                sql = sql & " From MBarang A Left Join MKategori B on A.IDKategori=B.ID"
            Case IDForm.F_MKategori
                sql = "Select * From MKategori "
            Case IDForm.F_MKategoriBiaya
                sql = "Select * From MKategoriBiaya "
            Case IDForm.F_MKaryawan
                sql = "Select * From MKaryawan "
        End Select
        ds = Query.ExecuteDataSet(sql)
        If Not ds Is Nothing Then
            GridControl1.DataSource = ds.Tables(0)
            ds.Dispose()
            GridControl1.Refresh()

            GridView1.OptionsView.ColumnAutoWidth = False
            GridView1.OptionsView.BestFitMaxRowCount = -1
            GridView1.BestFitColumns()
        End If
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonBaru.ItemClick
        Baru()
    End Sub
    Sub Baru()
        Select Case idFrm
            Case IDForm.F_User
                Dim f As New FormUser
                f._IsNew = True
                If f.ShowDialog() = DialogResult.OK Then
                    BarButtonRefresh.PerformClick()
                End If
            Case IDForm.F_RoleUser
                Dim f As New FormRoleUser
                f._IsNew = True
                If f.ShowDialog() = DialogResult.OK Then
                    BarButtonRefresh.PerformClick()
                End If
            Case IDForm.F_MBarang
                Dim f As New FormBarang
                f._IsNew = True
                If f.ShowDialog() = DialogResult.OK Then
                    BarButtonRefresh.PerformClick()
                End If
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
                Dim f As New FormUser
                f._IsNew = False
                f._User = view.GetDataRow(GridView1.FocusedRowHandle)("Username").ToString
                If f.ShowDialog() = DialogResult.OK Then
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("Username"), f._User)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                End If
            Case IDForm.F_RoleUser
                Dim f As New FormRoleUser
                f._IsNew = False
                f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                If f.ShowDialog() = DialogResult.OK Then
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), f._ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                End If
            Case IDForm.F_MBarang
                Dim f As New FormBarang
                f._IsNew = True
                f._ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                If f.ShowDialog() = DialogResult.OK Then
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), f._ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                End If
        End Select
    End Sub

    Private Sub BarButtonHapus_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonHapus.ItemClick
        If GridView1.RowCount > 0 Then
            Hapus()
        End If
    End Sub

    Sub Hapus()
        Dim view As ColumnView = GridControl1.FocusedView
        Select Case idFrm
            Case IDForm.F_User
                Dim User As String = ""
                User = View.GetDataRow(GridView1.FocusedRowHandle)("Username").ToString
                Dim f As Pesan = Query.DeleteDataMaster("MUser", "Username='" & User.Trim & "'")
                If f.Hasil = True Then
                    MsgBox(f.Message)
                    BarButtonRefresh.PerformClick()

                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("Username"), User.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    MsgBox(f.Message)
                End If
            Case IDForm.F_RoleUser
                Dim ID As Integer = 0
                ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                Dim f As Pesan = Query.DeleteDataMaster("MRoleUser", "ID=" & ID & "")
                If f.Hasil = True Then
                    MsgBox(f.Message)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    MsgBox(f.Message)
                End If
            Case IDForm.F_MBarang
                Dim ID As Integer = 0
                ID = ObjToInt(view.GetDataRow(GridView1.FocusedRowHandle)("ID"))
                Dim f As Pesan = Query.DeleteDataMaster("MUser", "ID=" & ID & "")
                If f.Hasil = True Then
                    MsgBox(f.Message)
                    BarButtonRefresh.PerformClick()
                    GridView1.ClearSelection()
                    GridView1.FocusedRowHandle = GridView1.LocateByDisplayText(0, GridView1.Columns("ID"), ID.ToString)
                    GridView1.SelectRow(GridView1.FocusedRowHandle)
                Else
                    MsgBox(f.Message)
                End If
        End Select
    End Sub

    Private Sub BarButtonExport_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonExport.ItemClick

    End Sub

    Private Sub BarButtonCetak_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonCetak.ItemClick

    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        Ubah()
    End Sub
End Class