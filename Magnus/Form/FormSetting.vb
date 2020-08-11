Imports System.Data.SqlClient

Public Class FormSetting
    Private Sub SettingControl1_Load(sender As Object, e As EventArgs)
        If True Then
            txtServer.Text = "."
            txtUID.Text = "sa"
            txtPwd.Text = "Sg1"
            txtDatabase.Text = "Magnus"
            CheckEditUseWinAuth.Checked = False
            'conStr=""
        Else
            txtServer.Text = ""
            txtUID.Text = ""
            txtPwd.Text = ""
            txtDatabase.Text = ""
            CheckEditUseWinAuth.Checked = False
        End If
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click

        Me.Close()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Using cn As New SqlConnection(conStr)
            Try
                cn.Open()
                DialogResult = DialogResult.OK
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        Me.Close()
    End Sub

    Private Sub CheckEditUseWinAuth_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEditUseWinAuth.CheckedChanged
        If CheckEditUseWinAuth.Checked Then
            txtUID.Properties.ReadOnly = True
            txtPwd.Properties.ReadOnly = True
        Else
            txtUID.Properties.ReadOnly = False
            txtPwd.Properties.ReadOnly = False
        End If
    End Sub
End Class