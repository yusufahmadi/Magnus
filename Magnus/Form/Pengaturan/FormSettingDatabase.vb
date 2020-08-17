Imports System.Data.SqlClient
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraEditors.Controls
Imports Magnus.Ini
Imports Magnus.Utils

Public Class FormSettingDatabase
    Private Sub CheckEditUseWinAuth_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEditUseWinAuth.CheckedChanged
        If CheckEditUseWinAuth.Checked Then
            txtUID.Properties.ReadOnly = True
            txtPwd.Properties.ReadOnly = True
        Else
            txtUID.Properties.ReadOnly = False
            txtPwd.Properties.ReadOnly = False
        End If
    End Sub


    Private Sub FormSettingDatabase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddSavedHostName()
        Dim FileSetting As String = Application.StartupPath & "/System/Setting.ini"
        Try
            If Not Utils.FileExists(FileSetting) Then
                Dim sw As IO.StreamWriter
                If Not (IO.File.Exists(FileSetting)) Then
                    sw = IO.File.CreateText(FileSetting)
                Else
                End If
            End If
            txtServer.Text = Ini.BacaIni("Application", "Server", ".")
            txtUID.Text = Ini.BacaIni("Application", "User", "sa")
            If Ini.BacaIni("Application", "Password", "") = "" Then
                txtPwd.Text = "Sg1"
            Else
                txtPwd.Text = AES_Decrypt(Ini.BacaIni("Application", "Password", ""))
            End If

            'Defaultkan Ke Table
            Dim Table1 As DataTable
            Table1 = New DataTable("Temp")
            Table1.Columns.Add("Database", GetType(System.String))
            Table1.Columns.Add("Status", GetType(System.String))
            Table1.Rows.Add(Ini.BacaIni("Application", "Database", "Magnus"), "-")
            txtDatabase.Properties.DataSource = Table1
            txtDatabase.Properties.DisplayMember = "Database"
            txtDatabase.Properties.ValueMember = "Database"
            txtDatabase.Text = Ini.BacaIni("Application", "Database", "Magnus")

            txtTimeout.Text = Ini.BacaIni("Application", "Timeout", "10")

            CheckEditUseWinAuth.Checked = ObjToBool(Ini.BacaIni("Application", "WindowsAuth", "False"))
            'If CheckEditUseWinAuth.Checked Then
            '    conStr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=" & txtDatabase.Text.Trim & ";Data Source=" & txtServer.Text.Trim & ";Connect Timeout=" & txtTimeout.Text & ";"
            'Else
            '    conStr = "Server=" & txtServer.Text.Trim &
            '        ";Database=" & txtDatabase.Text.Trim &
            '        ";User Id=" & txtUID.Text.Trim &
            '        ";Password=" & txtPwd.Text.Trim &
            '        ";Connect Timeout=" & txtTimeout.Text & ";"
            'End If
        Catch ex As Exception
            DevExpress.XtraEditors.XtraMessageBox.Show(Me, ex.Message, NamaAplikasi)
        End Try
    End Sub


    Private Sub WindowsUIButtonPanelMain_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles windowsUIButtonPanelMain.ButtonClick
        Dim x As New Pesan With {.Hasil = False, .Message = "", .Value = Nothing}
        Dim _button = (e.Button.ToString().Substring(e.Button.ToString().LastIndexOf("=") + 1).Trim())
        Select Case _button
            Case "0", "'Save'"
                Dim tempCon As String = ""
                If ObjToBool(CheckEditUseWinAuth.Checked) Then
                    tempCon = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=" & txtDatabase.Text.Trim & ";Data Source=" & txtServer.Text.Trim & ";Connect Timeout=" & txtTimeout.Text & ";"
                Else
                    tempCon = "Server=" & txtServer.Text.Trim & ";Database=" & txtDatabase.Text.Trim & ";User Id=" & txtUID.Text.Trim & ";Password=" & txtPwd.Text.Trim & ";Connect Timeout=" & txtTimeout.Text & ";"
                End If
                Using cn As New SqlConnection(tempCon)
                    Try
                        cn.Open()
                        'Connection OK -> Replace Main Connection
                        conStr(tempCon)
                        TulisIni("Application", "Server", txtServer.Text)
                        TulisIni("Application", "User", txtUID.Text)
                        TulisIni("Application", "Password", AES_Encrypt(txtPwd.Text))
                        TulisIni("Application", "Database", txtDatabase.Text)
                        TulisIni("Application", "WindowsAuth", ObjToBool(CheckEditUseWinAuth.Checked).ToString)
                        TulisIni("Application", "Timeout", ObjToInt(txtTimeout.EditValue).ToString)

                        DialogResult = DialogResult.OK
                        If cn.State = ConnectionState.Open Then
                            cn.Close()
                        End If
                        SaveHostName()
                        Me.Close()
                    Catch ex As Exception
                        DevExpress.XtraEditors.XtraMessageBox.Show(Me, ex.Message, NamaAplikasi)
                    End Try
                End Using
            Case "1", "'Reset'"
                Me.FormSettingDatabase_Load(sender, e)
            Case "2", "'Close'"
                Me.Close()
        End Select
    End Sub

    Sub AddSavedHostName()
        Try
            Dim xFile As String = Application.StartupPath & "\System\" & "Host.txt"
            If FileExists(xFile) Then
                txtServer.Properties.Items.Clear()
                Using objReader As New System.IO.StreamReader(xFile)
                    'objReader.ReadToEnd()
                    Dim arr As String() = objReader.ReadToEnd.Split(";")
                    For i As Integer = 0 To arr.Length - 1
                        If Not arr(i).Trim = "" Then
                            txtServer.Properties.Items.Add(arr(i).Trim)
                        End If
                    Next
                End Using
            End If
        Catch ex As Exception
        End Try
    End Sub
    Sub SaveHostName()
        Try
            Dim xFile As String = Application.StartupPath & "\System\" & "Host.txt"
            If Not txtServer.Text.Trim = "" Then
                If Not System.IO.File.Exists(xFile) Then
                    System.IO.File.Create(xFile).Dispose()
                End If
                If FileExists(xFile) Then
                    Using objReader As New System.IO.StreamReader(xFile)
                        Dim arr As String() = objReader.ReadToEnd.Split(";")
                        For i As Integer = 0 To arr.Length - 1
                            If Not arr(i).Trim = "" Then
                                If txtServer.Text.Trim = arr(i).Trim Then
                                    Exit Sub
                                End If
                            End If
                        Next
                    End Using
                    Using stream As New System.IO.FileStream(xFile, IO.FileMode.Append, IO.FileAccess.Write)
                        Using write As New System.IO.StreamWriter(stream)
                            If Not txtServer.Text.Trim = "" Then
                                write.Write(txtServer.Text.Trim & ";")
                            End If
                        End Using
                    End Using
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LookUpEdit1_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles txtDatabase.ButtonClick
        Select Case e.Button.Index
            Case 0
            Case 1
                Dim koneksi As String = ""
                If CheckEditUseWinAuth.Checked Then
                    'koneksi = "Server=" & txtServer.Text.Trim & ";Database=master;User Id=" & txtUID.Text.Trim & ";Password=" & txtPwd.Text.Trim & ";"
                    koneksi = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=master;Data Source=" & txtServer.Text.Trim & ";Connect Timeout=10;"
                Else
                    koneksi = "Server=" & txtServer.Text.Trim & ";Database=master;User Id=" & txtUID.Text.Trim & ";Password=" & txtPwd.Text.Trim & ";Connect Timeout=10;"
                End If
                Dim ds As DataSet, Sql As String = ""
                Using cn As New SqlConnection(koneksi)
                    Try
                        cn.Open()
                        If cn.State = ConnectionState.Open Then
                            cn.Close()
                            Using dlg As New DevExpress.Utils.WaitDialogForm("Load Database", NamaAplikasi)
                                dlg.TopMost = True
                                Sql = "Select name [Database], state_desc [Status]  " & vbCrLf &
                                  " From sys.databases  " & vbCrLf &
                                  " Where name not in('master','tempdb','model','msdb') and state <>6 " & vbCrLf &
                                  " Order By name"
                                ds = Query.ExecuteDataSet(Sql, koneksi)
                                If Not ds Is Nothing Then
                                    txtDatabase.Properties.DataSource = ds.Tables(0)
                                    txtDatabase.Properties.DisplayMember = "Database"
                                    txtDatabase.Properties.ValueMember = "Database"
                                End If
                            End Using
                        End If
                    Catch ex As Exception
                        DevExpress.XtraEditors.XtraMessageBox.Show(Me, ex.Message, NamaAplikasi)
                    End Try
                End Using
        End Select
    End Sub
End Class