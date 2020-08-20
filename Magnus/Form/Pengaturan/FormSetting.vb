Imports Magnus.Utils
Public Class FormSetting
    Private Sub WindowsUIButtonPanelMain_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles windowsUIButtonPanelMain.ButtonClick
        Dim x As New Pesan With {.Hasil = False, .Message = "", .Value = Nothing}
        Dim _button = (e.Button.ToString().Substring(e.Button.ToString().LastIndexOf("=") + 1).Trim())
        Select Case _button
            Case "0", "'Save'"
                Dim sql As String = "Select count(*) from Msetting"
                If ObjToInt(Query.ExecuteScalar(sql)) = 0 Then
                    sql = "Insert Into MSetting (NamaPerusahaan,AlamatPerusahaan,KotaPerusahaan,PathLayout,NPWP)" & vbCrLf &
                                  "Values ('" & FixApostropi(TextEdit1.Text) & "','" & FixApostropi(TextEdit2.Text) & "','" & FixApostropi(TextEdit3.Text) & "','" & FixApostropi(TextEdit4.Text) & "','" & FixApostropi(TextEdit5.Text) & "')"
                Else
                    sql = "Update MSetting Set " & vbCrLf &
                                  "NamaPerusahaan='" & FixApostropi(TextEdit1.Text) & "'," & vbCrLf &
                                  "AlamatPerusahaan='" & FixApostropi(TextEdit2.Text) & "'," & vbCrLf &
                                  "KotaPerusahaan='" & FixApostropi(TextEdit3.Text) & "'," & vbCrLf &
                                  "NPWP='" & FixApostropi(TextEdit5.Text) & "'," & vbCrLf &
                                  "PathLayout='" & FixApostropi(TextEdit4.Text) & "'"
                End If
                x = Query.Execute(sql)
                If x.Hasil Then
                    Query.GetApplicationSetting()
                    DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, x.Message, NamaAplikasi)
                End If
            Case "1", "'Reset'"
                Me.FormSetting_Load(sender, e)
            Case "2", "'Close'"
                Me.Close()
            Case "2", "'Menu'"
                Using d As New FormSettingLainya
                    d.TopMost = True
                    d.ShowDialog()
                End Using
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
                TextEdit5.EditValue = .Item("NPWP").ToString
            End With
            ds.Dispose()
        End If
    End Sub

    Private Sub WindowsUIButtonPanelMain_Click(sender As Object, e As EventArgs) Handles windowsUIButtonPanelMain.Click

    End Sub
End Class