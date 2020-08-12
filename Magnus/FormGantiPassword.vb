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
        If TextBox1.Text = "" Then
            TextBox1.Focus()
            Exit Sub
        End If
        If TextBox2.Text = "" Then
            TextBox2.Focus()
            Exit Sub
        End If
        If TextBox3.Text = "" Then
            TextBox3.Focus()
            Exit Sub
        End If
        If TextBox2.Text <> TextBox3.Text Then
            MsgBox("Verifikasi password baru harus sama.")
            TextBox3.Focus()
            Exit Sub
        End If
        If Utils.ObjToInt(Query.ExecuteScalar("Select Count(*) From Muser WHere Username='" & Username & "' And Password ='" & AES_Encrypt(TextBox1.Text, "Kia") & "'")) > 0 Then
            Query.Execute("Update Muser Set Password='" & AES_Encrypt(TextBox3.Text, "Kia") & "' Where Username='" & Username & "' And Password ='" & AES_Encrypt(TextBox1.Text, "Kia") & "'")
            MsgBox("Penggantian password berhasil.")
            btnClose.PerformClick()
        Else
            MsgBox("Password lama salah.")
        End If
    End Sub
End Class
