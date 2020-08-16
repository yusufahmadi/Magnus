Imports Magnus.Utils
Public Class FormSetting

    Private Sub windowsUIButtonPanelMain_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles windowsUIButtonPanelMain.ButtonClick
        Dim x As New Pesan With {.Hasil = False, .Message = "", .Value = Nothing}
        Dim a = (e.Button.ToString().Substring(e.Button.ToString().LastIndexOf("=") + 1).Trim())
        Select Case a
            Case "0", "'Save'"
                Dim sql As String = "Select count(*) from Msetting"
                If Query.ExecuteScalar(sql) = 0 Then
                    sql = "Insert Into MSetting (NamaPerusahaan,AlamatPerusahaan,KotaPerusahaan,PathLayout)" & vbCrLf &
                          "Values ('" & FixApostropi(TextEdit1.Text) & "','" & FixApostropi(TextEdit2.Text) & "','" & FixApostropi(TextEdit3.Text) & "','" & FixApostropi(TextEdit4.Text) & "')"
                Else
                    sql = "Update MSetting Set " & vbCrLf &
                          "NamaPerusahaan='" & FixApostropi(TextEdit1.Text) & "'," & vbCrLf &
                          "AlamatPerusahaan='" & FixApostropi(TextEdit2.Text) & "'," & vbCrLf &
                          "KotaPerusahaan='" & FixApostropi(TextEdit3.Text) & "'," & vbCrLf &
                          "PathLayout='" & FixApostropi(TextEdit4.Text) & "')"
                End If
                x = Query.Execute(sql)
                If x.Hasil Then
                    DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    MsgBox(x.Message)
                End If
            Case "1", "'Reset'"
                Me.FormSetting_Load(sender, e)
            Case "2", "'Close'"
                Me.Close()
        End Select
    End Sub

    Private Sub FormSetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ds As New DataSet
        ds = Query.ExecuteDataSet("Select * from MSetting")
        If Not ds Is Nothing Then
            With ds.Tables(0).Rows(0)
                TextEdit1.EditValue = .Item("NamaPerusahaan").ToString
                TextEdit2.EditValue = .Item("AlamatPerusahaan").ToString
                TextEdit3.EditValue = .Item("KotaPerusahaan").ToString
                TextEdit4.EditValue = .Item("PathLayout").ToString
            End With
            ds.Dispose()
        End If
    End Sub
End Class