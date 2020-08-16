Public Class FormGantiPassword
    Sub New()
        InitializeComponent()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FormGantiPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Height = 170
    End Sub

    Private Sub btnGantiPassword_Click(sender As Object, e As EventArgs) Handles btnGantiPassword.Click
        Me.Height = 340
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If txtOldPwd.Text = "" Then
            txtOldPwd.Focus()
            Exit Sub
        End If
        If txtNewPwd.Text = "" Then
            txtNewPwd.Focus()
            Exit Sub
        End If
        If txtReEnterNewPwd.Text = "" Then
            txtReEnterNewPwd.Focus()
            Exit Sub
        End If
        If txtNewPwd.Text <> txtReEnterNewPwd.Text Then
            MsgBox("Verifikasi password baru harus sama.")
            txtReEnterNewPwd.Focus()
            Exit Sub
        End If
        If Utils.ObjToInt(Query.ExecuteScalar("Select Count(*) From Muser WHere Username='" & Username & "' And Password ='" & AES_Encrypt(txtOldPwd.Text) & "'")) > 0 Then
            Query.Execute("Update Muser Set Password='" & AES_Encrypt(txtReEnterNewPwd.Text) & "' Where Username='" & Username & "' And Password ='" & AES_Encrypt(txtOldPwd.Text) & "'")
            MsgBox("Penggantian password berhasil.")
            btnClose.PerformClick()
        Else
            MsgBox("Password lama salah.")
        End If
    End Sub
End Class
