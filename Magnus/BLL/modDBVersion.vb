Imports System.Data.SqlClient
Imports DevExpress.Utils

Public Module modDBVersion

    Public Function UpdateDBVersion() As Pesan
        Dim ps As New Pesan With {.Message = "", .Hasil = False, .Value = ""}
        Dim sql As String = ""
        Using con As New SqlConnection(conStr)
            Using com As New SqlCommand()
                con.Open()
                com.Connection = con
                com.CommandTimeout = con.ConnectionTimeout
                Dim Versi As Integer = 0

                Try
                    Try
                        sql = "Select Versi From DBVersion"
                        com.CommandText = sql
                        Versi = Utils.ObjToInt(com.ExecuteScalar())
                    Catch ex As Exception

                    End Try
                    ps.Message = "Versi " & Versi
                    ps.Value = Versi
                    Select Case Versi
                        Case 0
                            UpdateV1()
                            UpdateV2()
                        Case 1
                            UpdateV2()
                    End Select
                    ps.Hasil = True
                Catch ex As Exception
                    With ps
                        .Message = ex.Message
                        .Hasil = False
                        .Value = Nothing
                    End With
                End Try
            End Using
        End Using
        Return ps
    End Function

    Public Function UpdateV2() As Pesan
        Dim ps As New Pesan With {.Message = "OK", .Hasil = True, .Value = ""}
        Dim sql As String = ""
        Using con As New SqlConnection(conStr)
            Using com As New SqlCommand()
                Using dlg As New WaitDialogForm("Update DB Version 2")
                    Try
                        con.Open()
                        com.Connection = con
                        com.CommandTimeout = con.ConnectionTimeout
                        Try
                            sql = "CREATE TABLE dbo.StokMasuk (
                              ID int NOT NULL,
                              Tgl date NOT NULL,
                              Kode nvarchar(50) NOT NULL,
                              Keterangan nvarchar(500) NULL,
                              IsPosted bit NULL,
                              TanggalBuat datetime NULL,
                              UserBuat nvarchar(50) NULL,
                              TanggalUbah datetime NULL,
                              UserUbah nvarchar(50) NULL,
                              CONSTRAINT PK_TStokMasuk PRIMARY KEY (ID)
                            )"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                        Catch ex As Exception
                            With ps
                                .Message = ex.Message
                                .Hasil = False
                                .Value = Nothing
                            End With
                        End Try
                        Try
                            sql = "CREATE TABLE dbo.StokMasukD (
                              ID bigint NOT NULL,
                              IDStokMasuk int NOT NULL,
                              NoUrut int NULL,
                              IDBarang int NOT NULL,
                              Qty numeric(18, 2) NULL,
                              IDSatuan int NULL,
                              Isi int NULL,
                              Harga money NULL,
                              Jumlah money NULL,
                              Catatan nvarchar(100) NULL,
                              CONSTRAINT PK_TStokMasukD PRIMARY KEY (ID),
                              CONSTRAINT FK_TStokMasukD_TStokMasuk FOREIGN KEY (IDStokMasuk) REFERENCES dbo.StokMasuk (ID) ON DELETE CASCADE ON UPDATE CASCADE
                            )"
                            com.CommandText = sql
                            com.ExecuteNonQuery()
                        Catch ex As Exception
                            With ps
                                .Message = ex.Message
                                .Hasil = False
                                .Value = Nothing
                            End With
                        End Try

                        Try
                            sql = "CREATE TABLE dbo.StokKeluar (
                              ID int NOT NULL,
                              Tgl date NOT NULL,
                              Kode nvarchar(50) NOT NULL,
                              Keterangan nvarchar(500) NULL,
                              IsPosted bit NULL,
                              TanggalBuat datetime NULL,
                              UserBuat nvarchar(50) NULL,
                              TanggalUbah datetime NULL,
                              UserUbah nvarchar(50) NULL,
                              CONSTRAINT PK_TStokKeluar PRIMARY KEY (ID)
                            )"
                            com.CommandText = sql
                            com.ExecuteNonQuery()
                        Catch ex As Exception
                            With ps
                                .Message = ex.Message
                                .Hasil = False
                                .Value = Nothing
                            End With
                        End Try
                        Try
                            sql = "CREATE TABLE dbo.StokKeluarD (
                              ID bigint NOT NULL,
                              IDStokKeluar int NOT NULL,
                              NoUrut int NULL,
                              IDBarang int NOT NULL,
                              Qty numeric(18, 2) NULL,
                              IDSatuan int NULL,
                              Isi int NULL,
                              Harga money NULL,
                              Jumlah money NULL,
                              Catatan nvarchar(100) NULL,
                              CONSTRAINT PK_TStokKeluarD PRIMARY KEY (ID),
                              CONSTRAINT FK_TStokKeluarD_TStokKeluar FOREIGN KEY (IDStokKeluar) REFERENCES dbo.StokKeluar (ID) ON DELETE CASCADE ON UPDATE CASCADE
                            )"
                            com.CommandText = sql
                            com.ExecuteNonQuery()
                        Catch ex As Exception
                            With ps
                                .Message = ex.Message
                                .Hasil = False
                                .Value = Nothing
                            End With
                        End Try
                        Try
                            sql = "Update [DBVersion] SET [Versi] =2, [TanggalUpdate] =Getdate()"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                        Catch ex As Exception
                            With ps
                                .Message = ex.Message
                                .Hasil = False
                                .Value = Nothing
                            End With
                        End Try
                    Catch ex As Exception
                        With ps
                            .Message = ex.Message
                            .Hasil = False
                            .Value = Nothing
                        End With
                    End Try
                End Using
            End Using
        End Using
        Return ps
    End Function

    Public Function UpdateV1() As Pesan
        Dim ps As New Pesan With {.Message = "OK", .Hasil = True, .Value = ""}
        Dim sql As String = ""
        Using con As New SqlConnection(conStr)
            Using com As New SqlCommand()
                Using dlg As New WaitDialogForm("Update DB Version 1")
                    Try
                        con.Open()
                        com.Connection = con
                        com.CommandTimeout = con.ConnectionTimeout
                        Try
                            sql = "Alter Table MUser Add  IsSupervisor bit NULL"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                        Catch ex As Exception
                            With ps
                                .Message = ex.Message
                                .Hasil = False
                                .Value = Nothing
                            End With
                        End Try
                        Try
                            sql = "Update MUser Set IsSupervisor=1 Where Kode='ADM'"
                            com.CommandText = sql
                            com.ExecuteNonQuery()
                        Catch ex As Exception
                            With ps
                                .Message = ex.Message
                                .Hasil = False
                                .Value = Nothing
                            End With
                        End Try

                        Try
                            sql = "CREATE TABLE [dbo].[DBVersion](
                        [Versi] [int] NULL,
                        [TanggalUpdate] [datetime] NULL
                            ) ON [PRIMARY]"
                            com.CommandText = sql
                            com.ExecuteNonQuery()
                        Catch ex As Exception
                            With ps
                                .Message = ex.Message
                                .Hasil = False
                                .Value = Nothing
                            End With
                        End Try
                        Try
                            sql = "INSERT INTO [DBVersion](
                        [Versi] , [TanggalUpdate]) VALUES(1,Getdate())"
                            com.CommandText = sql
                            com.ExecuteNonQuery()
                        Catch ex As Exception
                            With ps
                                .Message = ex.Message
                                .Hasil = False
                                .Value = Nothing
                            End With
                        End Try

                    Catch ex As Exception
                        With ps
                            .Message = ex.Message
                            .Hasil = False
                            .Value = Nothing
                        End With
                    End Try
                End Using
            End Using
        End Using
        Return ps
    End Function
End Module
