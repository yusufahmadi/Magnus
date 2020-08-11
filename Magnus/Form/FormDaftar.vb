Imports DevExpress.XtraGrid.Views.Base

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
        Me.Text = NamaForm
        BarButtonRefresh.PerformClick()
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

        End Select
        ds = Query.ExecuteDataSet(sql)
        If Not ds Is Nothing Then
            GridControl1.DataSource = ds.Tables(0)
            ds.Dispose()
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

        End Select
    End Sub

    Private Sub BarButtonUbah_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonUbah.ItemClick
        If GridView1.RowCount > 0 Then
            Ubah()
        End If
    End Sub

    Sub Ubah()
        Select Case idFrm
            Case IDForm.F_User
                Dim f As New FormUser
                f._IsNew = False
                Dim view As ColumnView = GridControl1.FocusedView
                f._User = view.GetDataRow(GridView1.FocusedRowHandle)("Username").ToString
                If f.ShowDialog() = DialogResult.OK Then
                    BarButtonRefresh.PerformClick()
                End If
            Case IDForm.F_RoleUser

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
                Else
                    MsgBox(f.Message)
                End If
            Case IDForm.F_RoleUser

        End Select
    End Sub
End Class