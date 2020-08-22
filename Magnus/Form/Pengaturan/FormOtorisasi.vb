Imports System.Data.SqlClient
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraEditors.Controls
Imports Magnus.Ini
Imports Magnus.Utils

Public Class FormOtorisasi
    Private Sub FormOtorisasi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub windowsUIButtonPanelMain_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles windowsUIButtonPanelMain.ButtonClick
        Dim x As New Pesan With {.Hasil = False, .Message = "", .Value = Nothing}
        Dim _button = (e.Button.ToString().Substring(e.Button.ToString().LastIndexOf("=") + 1).Trim())
        Select Case _button
            Case "0", "'Apply'"
                Saved()
            Case "1", "'Reset'"
                txtUID.Text = ""
                txtUID.Focus()
                txtPwd.Text = ""
            Case "2", "'Close'"
                Me.Close()
        End Select
    End Sub
    Sub Saved()
        If txtUID.Text = "" Then
            txtUID.Focus()
            Exit Sub
        End If
        If txtPwd.Text = "" Then
            txtPwd.Focus()
            Exit Sub
        End If
        If CInt(Query.ExecuteScalar("Select Count(*) From MUser " & vbCrLf &
                                      " Where IsSupervisor=1 And Username='" & txtUID.Text & "' And " & vbCrLf &
                                      " Password='" & AES_Encrypt(txtPwd.Text) & "'")) > 0 Then
            DialogResult = DialogResult.OK
            Me.Close()
        Else
            DevExpress.XtraEditors.XtraMessageBox.Show(Me, "Supervisor Only / Wrong Password", NamaAplikasi)
            txtUID.Focus()
        End If
    End Sub
    Private Sub txtPwd_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPwd.KeyDown
        If e.KeyCode = Keys.KeyCode.Enter Then
            Saved()
        End If
    End Sub

    Private Sub txtUID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUID.KeyDown
        If e.KeyCode = Keys.KeyCode.Enter Then
            txtPwd.Focus()
        End If
    End Sub
End Class