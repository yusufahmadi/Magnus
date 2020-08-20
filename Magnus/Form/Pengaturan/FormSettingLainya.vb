Imports System.Data.SqlClient
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraEditors.Controls
Imports Magnus.Ini
Imports Magnus.Utils

Public Class FormSettingLainya
    Private Sub FormSettingCalc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ds As New DataSet
        Try
            If Not ds Is Nothing Then
                ds = Query.ExecuteDataSet("Select ID,Kode,Nama From MKategori")
                TxtIDKategoriBahanLabel.Properties.DataSource = ds.Tables(0)
                TxtIDKategoriBahanLabel.Properties.DisplayMember = "Nama"
                TxtIDKategoriBahanLabel.Properties.ValueMember = "ID"

                TxtIDKategoriBahanRibbon.Properties.DataSource = ds.Tables(0)
                TxtIDKategoriBahanRibbon.Properties.DisplayMember = "Nama"
                TxtIDKategoriBahanRibbon.Properties.ValueMember = "ID"

                TxtIDKategoriBahanTaffeta.Properties.DataSource = ds.Tables(0)
                TxtIDKategoriBahanTaffeta.Properties.DisplayMember = "Nama"
                TxtIDKategoriBahanTaffeta.Properties.ValueMember = "ID"
                ds.Dispose()
                ds = Query.ExecuteDataSet("Select * From MSettingCalc")
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        With ds.Tables(0).Rows(0)
                            TxtIDKategoriBahanLabel.EditValue = ObjToInt(.Item(0))
                            TxtIDKategoriBahanRibbon.EditValue = ObjToInt(.Item(1))
                            TxtIDKategoriBahanTaffeta.EditValue = ObjToInt(.Item(2))
                        End With
                    End If
                    ds.Dispose()
                End If

                ds = Query.ExecuteDataSet("Select * From MSettingLainya")
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        With ds.Tables(0).Rows(0)
                            ComboBox1.SelectedIndex = ObjToInt(.Item(0))
                            CheckEdit1.Checked = ObjToBool(.Item(1))
                            CheckEdit2.Checked = ObjToBool(.Item(2))
                        End With
                    End If
                    ds.Dispose()
                End If
            End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(Me, ex.Message, NamaAplikasi)
        End Try
    End Sub

    Private Sub windowsUIButtonPanelMain_ButtonClick(sender As Object, e As ButtonEventArgs) Handles windowsUIButtonPanelMain.ButtonClick
        Dim x As New Pesan With {.Hasil = False, .Message = "", .Value = Nothing}
        Dim _button = (e.Button.ToString().Substring(e.Button.ToString().LastIndexOf("=") + 1).Trim())
        Select Case _button
            Case "0", "'Save'"
                Try
                    Dim sql As String = ""
                    sql = "Select Count(*) From MSettingCalc"
                    If ObjToInt(Query.ExecuteScalar(sql)) = 0 Then
                        sql = "Insert Into [MSettingCalc] ([Df_IDKategoriBahanLabel] ," & vbCrLf &
                          " [Df_IDKategoriBahanRibbon] ,[Df_IDKategoriBahanTaffeta]) " & vbCrLf &
                          " Values ('" & ObjToInt(TxtIDKategoriBahanLabel.EditValue) & "'," & vbCrLf &
                          " '" & ObjToInt(TxtIDKategoriBahanLabel.EditValue) & "'," & vbCrLf &
                          " '" & ObjToInt(TxtIDKategoriBahanLabel.EditValue) & "')"
                    Else
                        sql = "Update [MSettingCalc] Set " & vbCrLf &
                          " [Df_IDKategoriBahanLabel] ='" & ObjToInt(TxtIDKategoriBahanLabel.EditValue) & "'," & vbCrLf &
                          " [Df_IDKategoriBahanRibbon] ='" & ObjToInt(TxtIDKategoriBahanRibbon.EditValue) & "'," & vbCrLf &
                          " [Df_IDKategoriBahanTaffeta] ='" & ObjToInt(TxtIDKategoriBahanTaffeta.EditValue) & "'"
                    End If
                    x = Query.Execute(sql)
                    If x.Hasil = False Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Me, x.Message, NamaAplikasi)
                        Exit Sub
                    End If

                    sql = "Select Count(*) From MSettingLainya"
                    If ObjToInt(Query.ExecuteScalar(sql)) = 0 Then
                        sql = "Insert Into [MSettingLainya] ([TipeTanggal] ," & vbCrLf &
                          " [IsLockTglInHeader] ,[IsLockTglInDetail]) " & vbCrLf &
                          " Values (" & ObjToInt(ComboBox1.SelectedIndex) & "," & vbCrLf &
                          " " & ObjToBit(CheckEdit1.Checked) & "," & vbCrLf &
                          " " & ObjToBit(CheckEdit2.Checked) & ")"
                    Else
                        sql = "Update [MSettingLainya] Set " & vbCrLf &
                          " [TipeTanggal] =" & ObjToInt(ComboBox1.SelectedIndex) & "," & vbCrLf &
                          " [IsLockTglInHeader] =" & ObjToBit(CheckEdit1.Checked) & "," & vbCrLf &
                          " [IsLockTglInDetail] =" & ObjToBit(CheckEdit2.Checked) & ""
                    End If
                    x = Query.Execute(sql)
                    If x.Hasil Then
                        DialogResult = DialogResult.OK
                        Me.Close()
                    Else
                        DevExpress.XtraEditors.XtraMessageBox.Show(Me, x.Message, NamaAplikasi)
                    End If
                Catch ex As Exception
                    DevExpress.XtraEditors.XtraMessageBox.Show(Me, ex.Message, NamaAplikasi)
                End Try
            Case "1", "'Reset'"
                Me.FormSettingCalc_Load(sender, e)
            Case "2", "'Close'"
                Me.Close()
        End Select
    End Sub
End Class