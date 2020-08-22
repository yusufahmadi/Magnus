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
                        sql = "Select IsNull(Max(Versi),0) Versi From DBVersion"
                        com.CommandText = sql
                        Versi = Utils.ObjToInt(com.ExecuteScalar())
                    Catch ex As Exception

                    End Try
                    ps.Message = "Versi " & Versi
                    ps.Value = Versi
                    Select Case Versi
                        Case 0
                            UpdateV1()
                        Case 1
                            UpdateV2()
                        Case 2
                            UpdateV3()
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

    'NOTE + UPDATE VERSI JANGAN LUPA TAMBAHKAN DI PALING BAWAH (SEBELUM END) PANGGIL FUNCTION TERBARUNYA DI VERSI SEBELUMNYA 
    'UpdateV1()  -> UpdateV2() -> UpdateV3() -> DST 
    Public Function UpdateV3() As Pesan
        Dim ps As New Pesan With {.Message = "OK", .Hasil = True, .Value = ""}
        Dim sql As String = ""
        Using con As New SqlConnection(conStr)
            Using com As New SqlCommand()
                Using dlg As New WaitDialogForm("Update DB Version 3")
                    Try
                        con.Open()
                        com.Connection = con
                        com.CommandTimeout = con.ConnectionTimeout
                        com.Transaction = con.BeginTransaction
                        Try
                            'Perubahan Table MKaryawan Ke MRekanan
                            sql = "IF OBJECT_ID(N'MRekanan', N'U') IS NULL" & vbCrLf &
                                    "BEGIN " & vbCrLf &
                                    "CREATE TABLE [dbo].[MRekanan](" & vbCrLf &
                                    "[ID] [int] NOT NULL,[IDJenisRekanan] [smallint] NULL," & vbCrLf &
                                    "[Kode] [nvarchar](15) NOT NULL,[Nama] [nvarchar](80) NOT NULL," & vbCrLf &
                                    "[Alias] [nvarchar](50) NULL,[Keterangan] [nvarchar](250) NULL," & vbCrLf &
                                    "[Alamat] [nvarchar](250) NULL,[Alamat2] [nvarchar](250) NULL," & vbCrLf &
                                    "[HP] [nvarchar](50) NULL,[IsActive] [bit] NULL," & vbCrLf &
                                    "[NPWP] [nvarchar](25) NULL,[NamaWP] [nvarchar](80) NULL," & vbCrLf &
                                    "[NIK] [nvarchar](25) NULL, CONSTRAINT [PK_MRekanan] PRIMARY KEY CLUSTERED  " & vbCrLf &
                                    "([ID] ASC )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " & vbCrLf &
                                    ") ON [PRIMARY]; " & vbCrLf &
                                    " " & vbCrLf &
                                    "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 Karyawan 1 Supplier 2 Customer' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MRekanan', @level2type=N'COLUMN',@level2name=N'IDJenisRekanan' " & vbCrLf &
                                    "; " & vbCrLf &
                                    "ALTER TABLE [dbo].[MRekanan] ADD  CONSTRAINT [DF_MRekanan_IDJenisRekanan]  DEFAULT ((0)) FOR [IDJenisRekanan] " & vbCrLf &
                                    "; " & vbCrLf &
                                    "ALTER TABLE [dbo].[MRekanan] ADD  CONSTRAINT [DF_MRekanan_IsActive]  DEFAULT ((1)) FOR [IsActive] " & vbCrLf &
                                    "; " & vbCrLf &
                                    "END "
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = "IF OBJECT_ID(N'MKaryawan', N'U') IS NOT NULL" & vbCrLf &
                                  "BEGIN " & vbCrLf &
                                    "Insert Into MRekanan (ID,IDJenisRekanan,Kode,Nama,Alias,Keterangan,Alamat,Alamat2,HP,IsActive) " & vbCrLf &
                                    "select ID,1 IDJenisRekanan,Kode,Nama,Alias,Keterangan,Alamat,Alamat2,HP,IsActive From MKaryawan Where ID Not In(Select ID From MRekanan) " & vbCrLf &
                                  "END "
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = "IF OBJECT_ID(N'MJenisRekanan', N'U') IS NULL" & vbCrLf &
                                    "BEGIN " & vbCrLf &
                                    " CREATE TABLE dbo.MJenisRekanan (
                                      ID int NOT NULL,
                                      Kode nvarchar(10) NULL,
                                      Nama nvarchar(50) NULL,
                                      Keterangan nvarchar(50) NULL,
                                      IsActive bit NULL,
                                      CONSTRAINT PK_MJenisRekanan PRIMARY KEY (ID)
                                    );" & vbCrLf &
                                    "INSERT [dbo].[MJenisRekanan] ([ID], [Kode], [Nama], [Keterangan], [IsActive]) " & vbCrLf &
                                    "VALUES (0, N'NONE', N'-', NULL, 0),(1, N'KRY', N'KARYAWAN', NULL, 1)" & vbCrLf &
                                    ",(2, N'SUP', N'SUPPLIER', NULL, 1),(3, N'CUS', N'CUSTOMER', NULL, 1)" & vbCrLf &
                                    "END "
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            '--Karyawan
                            sql = "IF OBJECT_ID(N'MKaryawan', N'U') IS NOT NULL" & vbCrLf &
                                    "BEGIN " & vbCrLf &
                                    "Select * Into MKaryawanEx from MKategoriBiaya;
                                    Drop Table MKaryawan; " & vbCrLf &
                                    "END "
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = "IF OBJECT_ID(N'MKaryawan', N'V') IS NOT NULL" & vbCrLf &
                                   "Select 1 Eles Select 0"
                            com.CommandText = sql
                            If Utils.ObjToInt(com.ExecuteScalar()) = 1 Then
                                com.CommandText = "DROP VIEW [MKaryawan]"
                                com.ExecuteNonQuery()
                            End If
                            com.CommandText = "CREATE VIEW [dbo].[MKaryawan] AS
                                            SELECT ID, Kode, Nama, Alias, Keterangan, Alamat, Alamat2, HP, IsActive
                                            FROM dbo.MRekanan WHERE (IDJenisRekanan = 1)"
                            com.ExecuteNonQuery()
                            '-- Karyawan Ganti View

                            'Kas Bank Masuk
                            sql = "IF OBJECT_ID(N'KasBankMasuk', N'U') IS NULL" & vbCrLf &
                                    "BEGIN " & vbCrLf &
                                    "CREATE TABLE [dbo].[KasBankMasuk]( [ID] [int] NOT NULL, [IsPosted] [bit] NULL, [Tgl] [date] NOT NULL,[Kode] [nvarchar](15) NOT NULL,
                                    [IDKasBank] [nvarchar](15) NOT NULL, [IDRekanan] [int] NULL, [TipeForm] [smallint] NULL,  [KodeReff] [nvarchar](50) NULL,
                                    [Keterangan] [nvarchar](150) NULL,  [IsBG] [bit] NULL, [NoGiro] [nvarchar](15) NULL, [JTBG] [date] NULL,
                                    [UserBuat] [nvarchar](50) NULL,  [TanggalBuat] [datetime] NULL, [UserUbah] [nvarchar](50) NULL, [TanggalUbah] [datetime] NULL,
                                    CONSTRAINT [PK_KasBankMasuk] PRIMARY KEY CLUSTERED   (  [ID] ASC
                                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                                    ) ON [PRIMARY]
                                    ;
                                    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 All 1 Mutasi Kas Bank Masuk 2 Pelunasan Piutang' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'KasBankMasuk', @level2type=N'COLUMN',@level2name=N'TipeForm'
                                    ;
                                    ALTER TABLE [dbo].[KasBankMasuk] ADD  CONSTRAINT [DF_KasBankMasuk_IsPosted]  DEFAULT ((0)) FOR [IsPosted]
                                    ;
                                    ALTER TABLE [dbo].[KasBankMasuk] ADD  CONSTRAINT [DF_KasBankMasuk_IDRekanan]  DEFAULT ((0)) FOR [IDRekanan]
                                    ;
                                    ALTER TABLE [dbo].[KasBankMasuk] ADD  CONSTRAINT [DF_KasBankMasuk_TipeKasMasuk]  DEFAULT ((0)) FOR [TipeForm]
                                    ;
                                    ALTER TABLE [dbo].[KasBankMasuk] ADD  CONSTRAINT [DF_KasBankMasuk_IsBG]  DEFAULT ((0)) FOR [IsBG]
                                    ;" & vbCrLf &
                                    "END "
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = "IF OBJECT_ID(N'KasBankMasukD', N'U') IS NULL" & vbCrLf &
                                    "BEGIN " & vbCrLf &
                                    "CREATE TABLE [dbo].[KasBankMasukD]([ID] [bigint] NOT NULL,[IDKasBankMasuk] [int] NOT NULL,[IDAkun] [nvarchar](15) NOT NULL,
                                    [IDRekanan] [int] NOT NULL,[IDReff] [int] NOT NULL,[Nominal] [money] NOT NULL,[Kurs] [money] NOT NULL,
                                    [Catatan] [nvarchar](50) NULL,CONSTRAINT [PK_KasBankMasukD] PRIMARY KEY CLUSTERED ( [ID] ASC
                                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                                    ) ON [PRIMARY]
                                    ;
                                    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID Supplier Karyawan Customer' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'KasBankMasukD', @level2type=N'COLUMN',@level2name=N'IDRekanan'
                                    ;
                                    ALTER TABLE [dbo].[KasBankMasukD]  WITH CHECK ADD  CONSTRAINT [FK_KasBankMasukD_KasBankMasuk] FOREIGN KEY([IDKasBankMasuk])
                                    REFERENCES [dbo].[KasBankMasuk] ([ID])
                                    ON DELETE CASCADE
                                    ;
                                    ALTER TABLE [dbo].[KasBankMasukD] CHECK CONSTRAINT [FK_KasBankMasukD_KasBankMasuk]
                                    ;
                                    ALTER TABLE [dbo].[KasBankMasukD] ADD  CONSTRAINT [DF_KasBankMasukD_IDReferensi]  DEFAULT ((0)) FOR [IDRekanan]
                                    ;
                                    ALTER TABLE [dbo].[KasBankMasukD] ADD  CONSTRAINT [DF_KasBankMasukD_IDTransaksi]  DEFAULT ((0)) FOR [IDReff]
                                    ;
                                    ALTER TABLE [dbo].[KasBankMasukD] ADD  CONSTRAINT [DF_KasBankMasukD_Kurs]  DEFAULT ((1)) FOR [Kurs]
                                    ; " & vbCrLf &
                                    "END "
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            'HutangPiutang
                            sql = "IF OBJECT_ID(N'MHutangPiutang', N'U') IS  NULL" & vbCrLf &
                                  "BEGIN" & vbCrLf &
                                  "CREATE TABLE [dbo].[MHutangPiutang]([ID] [int] NOT NULL,[Kode] [nvarchar](20) NOT NULL,[Tgl] [date] NOT NULL,
                                        [TglJT] [date] NULL,[KodeReff] [nvarchar](50) NULL,[Keterangan] [nvarchar](150) NULL,[Nominal] [money] NULL,
                                        [Kurs] [money] NULL,[IDAkun] [nvarchar](15) NOT NULL,[IDRekanan] [int] NOT NULL,[IDJenisTransaksi] [smallint] NULL,
                                        [IDTransaksi] [int] NULL,[IDReff] [int] NULL,
                                        CONSTRAINT [PK_MHutangPiutang] PRIMARY KEY CLUSTERED 
                                        ([ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                                        ) ON [PRIMARY];
                                        ALTER TABLE [dbo].[MHutangPiutang] ADD  CONSTRAINT [DF_MHutangPiutang_IDReff]  DEFAULT ((0)) FOR [IDReff]; " & vbCrLf &
                                  "END"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            'Akun
                            sql = "IF OBJECT_ID(N'MAkunLv1', N'U') IS  NULL" & vbCrLf &
                                  "BEGIN " & vbCrLf &
                                  "CREATE TABLE [dbo].[MAkunLv1](
                                    [ID] [nvarchar](2) NOT NULL,
                                    [Nama] [nvarchar](50) NOT NULL,
                                    [Keterangan] [nvarchar](50) NULL,
                                    [IsNeraca] [bit] NULL,
                                    [IsDebet] [bit] NULL,
                                    [IsActive] [bit] NULL,
                                    [Level] [smallint] NOT NULL,
                                    CONSTRAINT [PK_MAkunLv1] PRIMARY KEY CLUSTERED 
                                    ( [ID] ASC )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                                    ) ON [PRIMARY];
                                    ALTER TABLE [dbo].[MAkunLv1] ADD  CONSTRAINT [DF_MAkunLv1_IsNeraca]  DEFAULT ((0)) FOR [IsNeraca];
                                    ALTER TABLE [dbo].[MAkunLv1] ADD  CONSTRAINT [DF_MAkunLv1_IsDebet]  DEFAULT ((0)) FOR [IsDebet];
                                    ALTER TABLE [dbo].[MAkunLv1] ADD  CONSTRAINT [DF_MAkunLv1_IsActive]  DEFAULT ((1)) FOR [IsActive];
                                    ALTER TABLE [dbo].[MAkunLv1] ADD  CONSTRAINT [DF_MAkunLv1_Level]  DEFAULT ((1)) FOR [Level];" & vbCrLf &
                                  " " & vbCrLf &
                                  "INSERT [dbo].[MAkunLv1] ([ID], [Nama], [Keterangan], [IsNeraca], [IsDebet], [IsActive], [Level]) VALUES (N'01', N'AKTIVA / ASSET', N'Aktiva / Asset', 1, 1, 1, 1), 
                                    (N'02', N'PASSIVA /  HUTANG', N'Hutang / Liability / Kewajiban', 1, 0, 1, 1),(N'03', N'EQUITY / MODAL', N'Modal', 1, 0, 1, 1),(N'04', N'INCOME / PENDAPATAN', N'Penjualan', 0, 0, 1, 1)
                                    ,(N'05', N'COGS / HPP', N'Harga Pokok Penjualan / Cost Of Good Sold', 0, 1, 1, 1),(N'06', N'EXPENSE / BIAYA', N'Biaya Operasional', 0, 1, 1, 1)
                                    ,(N'07', N'OTHER INCOME / PEDAPATAN LAINYA', N'Pendapatan diluar usaha', 0, 0, 1, 1), (N'08', N'OTHER EXPENSE / BIAYA LAINYA', N'Biaya diluar usaha', 0, 0, 1, 1) " & vbCrLf &
                                  " " & vbCrLf &
                                  "END " & vbCrLf &
                                  " " & vbCrLf &
                                  "IF OBJECT_ID(N'MAkunLv2', N'U') IS  NULL" & vbCrLf &
                                  "BEGIN" & vbCrLf &
                                  "CREATE TABLE [dbo].[MAkunLv2]([IDAkunLv1] [nvarchar](2) NOT NULL,
                                    [ID] [nvarchar](4) NOT NULL,[Nama] [nvarchar](50) NOT NULL,
                                    [Keterangan] [nvarchar](150) NULL,[IsActive] [bit] NULL,
                                    [Level] [smallint] NOT NULL,
                                    CONSTRAINT [PK_MAkunLv2] PRIMARY KEY CLUSTERED 
                                    ([ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, 
                                    IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                                    ) ON [PRIMARY];
                                    ALTER TABLE [dbo].[MAkunLv2]  WITH CHECK ADD  CONSTRAINT [FK_MAkunLv2_MAkunLv1] FOREIGN KEY([IDAkunLv1])
                                    REFERENCES [dbo].[MAkunLv1] ([ID]);
                                    ALTER TABLE [dbo].[MAkunLv2] CHECK CONSTRAINT [FK_MAkunLv2_MAkunLv1];
                                    ALTER TABLE [dbo].[MAkunLv2] ADD  CONSTRAINT [DF_MAkunLv2_IsActive]  DEFAULT ((0)) FOR [IsActive];
                                    ALTER TABLE [dbo].[MAkunLv2] ADD  CONSTRAINT [DF_MAkunLv2_Level]  DEFAULT ((2)) FOR [Level]; " & vbCrLf &
                                    " " & vbCrLf &
                                    "INSERT [dbo].[MAkunLv2] ([IDAkunLv1], [ID], [Nama], [Keterangan], [IsActive], [Level]) VALUES  (N'01', N'0101', N'Aktiva Lancar', NULL, 1, 2)
                                    ,(N'01', N'0102', N'Aktiva Tetap', NULL, 1, 2),(N'02', N'0201', N'Hutang Usaha', NULL, 1, 2),(N'03', N'0301', N'Modal', NULL, 1, 2)
                                    ,(N'04', N'0401', N'Pendapatan Usaha', NULL, 1, 2),(N'05', N'0501', N'Harga Pokok Penjualan', NULL, 1, 2),(N'06', N'0601', N'Biaya', NULL, 1, 2)
                                    ,(N'07', N'0701', N'Pendapatan diluar Usaha', NULL, 1, 2),(N'08', N'0801', N'Biaya diluar Usaha', NULL, 1, 2)      " & vbCrLf &
                                    " " & vbCrLf &
                                  "END" & vbCrLf &
                                  " " & vbCrLf &
                                  "IF OBJECT_ID(N'MAkun', N'U') IS  NULL" & vbCrLf &
                                  "BEGIN " & vbCrLf &
                                  "CREATE TABLE [dbo].[MAkun]([IDAkunLv2] [nvarchar](4) NOT NULL,[IDParent] [nvarchar](12) NOT NULL,
                                    [ID] [nvarchar](12) NOT NULL,[Nama] [nvarchar](50) NULL,[Keterangan] [nvarchar](150) NULL,
                                    [IDJenisBukuPembantu] [smallint] NULL,[IsActive] [bit] NULL,[Level] [smallint] NOT NULL,
                                    [IsKas] [bit] NULL,CONSTRAINT [PK_MAkun] PRIMARY KEY CLUSTERED 
                                    ([ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, 
                                    ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ) ON [PRIMARY];
                                    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 Tidak Perlu Buku Pembantu 1 Kas dan Bank 2 Hutang Piutang 3 Persediaan' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MAkun', @level2type=N'COLUMN',@level2name=N'IDJenisBukuPembantu';
                                    ALTER TABLE [dbo].[MAkun]  WITH CHECK ADD  CONSTRAINT [FK_MAkun_MAkunLv2] FOREIGN KEY([IDAkunLv2])
                                    REFERENCES [dbo].[MAkunLv2] ([ID]);
                                    ALTER TABLE [dbo].[MAkun] CHECK CONSTRAINT [FK_MAkun_MAkunLv2];
                                    ALTER TABLE [dbo].[MAkun] ADD  CONSTRAINT [DF_MAkun_IDJenisBukuPembantu]  DEFAULT ((0)) FOR [IDJenisBukuPembantu];
                                    ALTER TABLE [dbo].[MAkun] ADD  CONSTRAINT [DF_MAkun_IsActive]  DEFAULT ((0)) FOR [IsActive];
                                    ALTER TABLE [dbo].[MAkun] ADD  CONSTRAINT [DF_Table_2_IsTransaction]  DEFAULT ((0)) FOR [Level];
                                    ALTER TABLE [dbo].[MAkun] ADD  CONSTRAINT [DF_MAkun_IsKas]  DEFAULT ((0)) FOR [IsKas];" & vbCrLf &
                                  "" & vbCrLf &
                                  "INSERT [dbo].[MAkun] ([IDAkunLv2], [IDParent], [ID], [Nama], [Keterangan], [IDJenisBukuPembantu], [IsActive], [Level], [IsKas]) VALUES 
                                    (N'0101', N'0', N'010101', N'Kas', NULL, 1, 1, 3, 1),(N'0101', N'0', N'0101020000', N'Bank', NULL, 1, 0, 3, 0),
                                    (N'0601', N'0', N'060101', N'Bensin, Tol dan Parkir', NULL, 0, 1, 3, 0),(N'0601', N'0', N'060102', N'Komisi & Fee', NULL, 0, 0, 0, 0),
                                    (N'0601', N'0', N'060103', N'Biaya Atk', NULL, 0, 0, 0, 0),(N'0601', N'0', N'060104', N'Biaya Kirim', NULL, 0, 1, 3, 0),
                                    (N'0601', N'0', N'060199', N'Biaya Lain-Lain', NULL, 0, 0, 0, 0) " & vbCrLf &
                                  " " & vbCrLf &
                                  "END"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            ''-- Kategori Biaya Ganti View
                            'sql = "IF OBJECT_ID(N'MKategoriBiaya', N'U') IS NOT NULL" & vbCrLf &
                            '        "BEGIN " & vbCrLf &
                            '        "Select * Into MKategoriBiayaEx from MKategoriBiaya;
                            '         Drop Table MKategoriBiaya; " & vbCrLf &
                            '        "END "
                            'com.CommandText = sql
                            'com.ExecuteNonQuery()

                            ''sql = "IF OBJECT_ID(N'MKategoriBiaya', N'V') IS NOT NULL" & vbCrLf &
                            ''       "Select 1 Eles Select 0"
                            ''com.CommandText = sql
                            ''If Utils.ObjToInt(com.ExecuteScalar()) = 1 Then
                            ''    com.CommandText = "DROP VIEW [MKategoriBiaya]"
                            ''    com.ExecuteNonQuery()
                            ''End If
                            ''com.CommandText = "CREATE VIEW [dbo].[MKategoriBiaya] AS
                            ''                SELECT ID, Kode, Nama, Alias, Keterangan, Alamat, Alamat2, HP, IsActive
                            ''                FROM dbo.MAkun Where ID"
                            ''com.ExecuteNonQuery()

                            'Update DB Version
                            sql = "Update [DBVersion] SET [Versi] =3, [TanggalUpdate] =Getdate()"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            If Not com.Transaction Is Nothing Then
                                com.Transaction.Commit()
                                com.Transaction = Nothing
                            End If
                        Catch ex As Exception
                            com.Transaction.Rollback()
                            If Not com.Transaction Is Nothing Then
                                com.Transaction = Nothing
                            End If
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
        UpdateV3()
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
        UpdateV2()
    End Function
End Module
