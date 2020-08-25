Namespace BLL
    Public Class Login
        Public Shared Function goLogin(ByVal User As String, ByVal Pwd As String, Optional ByRef e As Pesan = Nothing) As Boolean
            Try
                If CInt(Query.ExecuteScalar("Select Count(*) From MUser")) = 0 Then
                    Query.Execute("Insert Into MUser(Username,Alias,Password,IsActive,IDRoleUser) " & vbCrLf &
                                  " Values ('Administrator','Super Admin','" & AES_Encrypt("12345") & "',1,1)")
                End If
                If CInt(Query.ExecuteScalar("Select Count(*) From MUser " & vbCrLf &
                                  " Where Username='" & User & "'")) > 0 Then
                    If CInt(Query.ExecuteScalar("Select Count(*) From MUser " & vbCrLf &
                                  " Where Username='" & User & "' and Isnull(IsActive,0)=1")) > 0 Then
                        If CInt(Query.ExecuteScalar("Select Count(*) From MUser " & vbCrLf &
                                      " Where Username='" & User & "' And " & vbCrLf &
                                      " Password='" & AES_Encrypt(Pwd) & "'")) > 0 Then
                            Dim ds As New DataSet
                            ds = Query.ExecuteDataSet("Select MUser.*,MRoleUser.Nama RoleUser, MRoleUser.IDTypeLayout " & vbCrLf &
                                          " From MUser " & vbCrLf &
                                          " Left Join MRoleUser on MUser.IDRoleUser=MRoleUser.ID " & vbCrLf &
                                          " " & vbCrLf &
                                          " Where Username='" & User & "' And " & vbCrLf &
                                          " Password='" & AES_Encrypt(Pwd) & "'")
                            If Not ds Is Nothing Then
                                With ds.Tables(0).Rows(0)
                                    Username = User
                                    IDRoleUser = CInt(.Item("IDRoleUser"))
                                    RoleUser = .Item("RoleUser").ToString()
                                    IDTypeLayout = CInt(.Item("IDTypeLayout"))
                                    UpdateDBVersion()
                                    TanggalSystem = Query.ExecuteScalar("DECLARE @TZ SMALLINT; " & vbCrLf &
                                                 "SELECT @TZ=DATEPART(TZ, SYSDATETIMEOFFSET()); " & vbCrLf &
                                                 "SELECT DATEADD(HOUR, -1*@TZ/60, GETDATE()) AS Tanggal")
                                    TimeZoneInformation.TimeZoneFunctionality.SetTime(System.TimeZone.CurrentTimeZone.ToLocalTime(TanggalSystem))
                                End With
                                With e
                                    e.Message = "Login Berhasil"
                                    e.Hasil = True
                                    e.Value = User & " - " & RoleUser
                                End With
                                ds.Dispose()
                            End If
                        Else
                            With e
                                e.Message = "Password Salah"
                                e.Hasil = False
                                e.Value = ""
                            End With
                        End If
                    Else
                        With e
                            e.Message = "Username sudah tidak aktif. Hub Administrator"
                            e.Hasil = False
                            e.Value = ""
                        End With
                    End If
                Else
                    With e
                        e.Message = "Username tidak di temukan"
                        e.Hasil = False
                        e.Value = ""
                    End With
                End If

            Catch ex As Exception
                With e
                    e.Message = ex.Message
                    e.Hasil = False
                    e.Value = Nothing
                End With
            End Try
            Return e.Hasil
        End Function


    End Class
End Namespace