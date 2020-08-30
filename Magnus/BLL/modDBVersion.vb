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
                        Case 3
                            UpdateV4()
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

    'Tambah Menu
    Public Function AddMenu(NamaForm As String, Caption As String, IsActive As Boolean, NamaTabel As String)
        Dim ps As New Pesan With {.Message = "OK", .Hasil = True, .Value = ""}
        Dim sql As String = ""
        Using con As New SqlConnection(conStr)
            Using com As New SqlCommand()
                Using dlg As New WaitDialogForm("Update Menu")
                    Try
                        con.Open()
                        com.Connection = con
                        com.CommandTimeout = con.ConnectionTimeout
                        com.Transaction = con.BeginTransaction
                        Try

                            sql = "Declare @NamaForm as varchar(50)='" & NamaForm & "',@Caption as varchar(150)='" & Caption & "',@IsActive as Bit=" & ObjToBit(IsActive) & ",@NamaTabel as varchar(50)='" & NamaTabel & "' " & vbCrLf &
                                  "IF Not Exists (Select ID FROM MMenu Where NamaForm=@NamaForm) " & vbCrLf &
                                  "BEGIN " & vbCrLf &
                                  " Declare @ID As Int=0  " & vbCrLf &
                                  " Select @ID=IsNull(Max(ID),0)+1 From MMenu" & vbCrLf &
                                  " Insert Into MMenu (ID,NamaForm,Caption,IsActive,NamaTabel)" & vbCrLf &
                                  " SELECT @ID,@NamaForm,@Caption,@IsActive,@NamaTabel " & vbCrLf &
                                  "END"
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

                    End Try
                End Using
            End Using
        End Using
    End Function


    'NOTE + UPDATE VERSI JANGAN LUPA TAMBAHKAN DI PALING BAWAH (SEBELUM END) PANGGIL FUNCTION TERBARUNYA DI VERSI SEBELUMNYA 
    'UpdateV1()  -> UpdateV2() -> UpdateV3() -> DST 

    Public Function UpdateV4() As Pesan
        Dim ps As New Pesan With {.Message = "OK", .Hasil = True, .Value = ""}
        Dim sql As String = ""
        Using con As New SqlConnection(conStr)
            Using com As New SqlCommand()
                Using dlg As New WaitDialogForm("Update DB Version 4")
                    Try
                        con.Open()
                        com.Connection = con
                        com.CommandTimeout = con.ConnectionTimeout
                        com.Transaction = con.BeginTransaction
                        Try
                            sql = "ALTER TRIGGER [dbo].[InsertDefaultMenuRoleUser]
                                    ON  [dbo].[MMenu]
                                    AFTER INSERT " & vbCrLf &
                                    " AS  BEGIN " & vbCrLf &
                                    "   SET NOCOUNT ON; " & vbCrLf &
                                    "   Declare @IDForm as int=0,@IsActive as bit=0 " & vbCrLf &
                                    "    Select @IDForm=i.ID,@IsActive=i.IsActive From Inserted as i " & vbCrLf &
                                    "   INSERT INTO [dbo].[MRoleUserD]
                                            ([IDRoleUser],[IDMenu],[IsEnable],[IsBaru]
                                            ,[IsUbah],[IsHapus],[IsCetak],[IDTypeLayout])
                                        Select ID As [IDRoleUser],@IDForm As [IDMenu]
                                            ,@IsActive [IsEnable],1 [IsBaru],1 [IsUbah],1 [IsHapus],1 [IsCetak],[IDTypeLayout]  " & vbCrLf &
                                    "   From MRoleUser " & vbCrLf &
                                    "END "
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = "IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[DeleteDefaultMenuRoleUser]')) " & vbCrLf &
                                    " DROP TRIGGER [dbo].[DeleteDefaultMenuRoleUser]; "
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = " CREATE TRIGGER DeleteDefaultMenuRoleUser " & vbCrLf &
                                    " ON  MMenu " & vbCrLf &
                                    " AFTER DELETE " & vbCrLf &
                                    " AS  BEGIN " & vbCrLf &
                                    "   SET NOCOUNT ON; " & vbCrLf &
                                    "   Declare @IDForm as int=0,@IsActive as bit=0 " & vbCrLf &
                                    "   Select @IDForm=i.ID,@IsActive=i.IsActive From deleted as i " & vbCrLf &
                                    "   Delete [dbo].[MRoleUserD] " & vbCrLf &
                                    "   Where [IDMenu] = @IDForm " & vbCrLf &
                                    "END "
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = "Create VIEW vKasBankMasuk
                                    As 
                                    Select 'D' Jenis,A.Tgl,A.Kode,MKB.Nama,MRH.Nama Rekanan,A.Keterangan,
                                    B.IDAkun,MA.Nama Akun,MRD.Nama Sub,B.Nominal,B.Catatan ,A.UserBuat,A.UserUbah,A.TanggalBuat,A.TanggalUbah
                                    From KasBankMasuk A
                                    Inner Join KasBankMasukD B On A.ID=B.IDKasBankMasuk
                                    Left Join MAkun MKB on MKB.ID=A.IDKasBank
                                    Left Join MRekanan MRH on MRH.ID=A.IDRekanan
                                    Left Join MAkun MA on MA.ID=B.IDAkun
                                    Left Join MRekanan MRD on MRD.ID=B.IDRekanan"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = " Create View vKasBankKeluar
                                    as 
                                    Select 'K' Jenis,A.Tgl,A.Kode,MKB.Nama,MRH.Nama Rekanan,A.Keterangan,
                                    B.IDAkun,MA.Nama Akun,MRD.Nama Sub,B.Nominal,B.Catatan ,A.UserBuat,A.UserUbah,A.TanggalBuat,A.TanggalUbah
                                    From KasBankKeluar A
                                    Inner Join KasBankKeluarD B On A.ID=B.IDKasBankKeluar
                                    Left Join MAkun MKB on MKB.ID=A.IDKasBank
                                    Left Join MRekanan MRH on MRH.ID=A.IDRekanan
                                    Left Join MAkun MA on MA.ID=B.IDAkun
                                    Left Join MRekanan MRD on MRD.ID=B.IDRekanan"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = "Create Procedure spLapKasHarian(@T1 as Date='2020-01-02',@T2 as Date='2020-08-31')
                                    As Begin
                                    DECLARE @Temp AS TABLE (
                                    [I] [int] IDENTITY(1,1) NOT NULL,
                                    [Tgl] [date] NOT NULL,
                                    [Kode] [nvarchar](50) NOT NULL,
                                    [Akun] [nvarchar](50) NULL,
                                    [Keterangan] [nvarchar](500) NULL,
                                    [SaldoAwal] [numeric](18, 2) NULL,
                                    [Debet] [numeric](18, 2) NULL,
                                    [Kredit] [numeric](18, 2) NULL,
                                    [SaldoAkhir] [numeric](18, 2) NULL,
                                    [UserBuat] [nvarchar](50) NULL,
                                    [UserUbah] [nvarchar](50) NULL,
                                    [TanggalBuat] [datetime] NULL,
                                    [TanggalUbah] [datetime] NULL)

                                    Insert Into @Temp
                                    Select Tgl,Kode,Akun,Catatan,0 SaldoAwal,
                                    Case When Jenis='D' Then Nominal Else 0 End Debet, 
                                    Case When Jenis='K' Then Nominal Else 0 End Kredit ,0 SaldoAkhir,UserBuat,UserUbah,TanggalBuat,TanggalUbah
                                    From (
                                    Select * from vKasBankMasuk
                                    Union All
                                    Select * from vKasBankKeluar
                                    ) S
                                    Where S.Tgl Between @T1 And Dateadd(day,1,@T2) 
                                    --And Case when @IDBarang=0 then @IDBarang else S.IDBarang end= @IDBarang
                                    Order By S.Tgl,S.Jenis
 
                                    Declare @SaldoAwal as Money =0.0
                                    Select @SaldoAwal=Sum(IsNull(S.Nominal,0)) 
                                    From
                                    ( 
                                    Select Sum(Isnull(Nominal,0)) Nominal From vKasBankMasuk Where  Tgl <@T1 --And Case when @IDBarang=0 then @IDBarang else IDBarang end= @IDBarang
                                    Union All 
                                    Select Sum(-1*Isnull(Nominal,0)) Nominal From vKasBankKeluar Where Tgl <@T1 --And Case when @IDBarang=0 then @IDBarang else IDBarang end= @IDBarang 
                                    ) As S


                                    DECLARE @i AS INT, @iMax AS INT,@Nominal as [numeric](18, 2)=0
                                    --SELECT @i=1, @iMax=MAX(NoID) FROM TempStok T
                                    SELECT @i=0, @iMax=Max(I) FROM @Temp T
                                    WHILE (@i<=@iMax)
                                    BEGIN
                                    Select @Nominal=Debet-Kredit From @Temp Where I=@i
                                    update @Temp Set SaldoAwal= @SaldoAwal,SaldoAkhir=@SaldoAwal+@Nominal Where I=@i
                                    Set @SaldoAwal=@SaldoAwal+@Nominal
                                    Set @i+=1
                                    END
	
                                    Select * from @Temp --Order By Tgl,Debet
	
                                    END"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = " Create Procedure spLapKasPerKategori (@T1 as Date='2020-01-02',@T2 as Date='2020-08-31',@IDAkun as varchar(20)='')
                                    As Begin
                                    Select Year(S.Tgl) Tahun,Month(S.Tgl) Bulan, S.Akun,Sum(IsNull(Nominal,0)) Total from vKasBankKeluar  S
                                    Where S.Tgl Between @T1 And Dateadd(day,1,@T2) 
                                    And Case when @IDAkun='' then @IDAkun else S.IDAkun end= @IDAkun
                                    Group By Year(S.Tgl),Month(S.Tgl), S.Akun
                                    Order By Year(S.Tgl),Month(S.Tgl),Sum(IsNull(Nominal,0)) desc
                                    END"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = "BEGIN TRANSACTION
                                    SET QUOTED_IDENTIFIER ON
                                    SET ARITHABORT ON
                                    SET NUMERIC_ROUNDABORT OFF
                                    SET CONCAT_NULL_YIELDS_NULL ON
                                    SET ANSI_NULLS ON
                                    SET ANSI_PADDING ON
                                    SET ANSI_WARNINGS ON
                                    COMMIT
                                    BEGIN TRANSACTION;
                                    ALTER TABLE dbo.KasBankMasukD
                                    DROP CONSTRAINT FK_KasBankMasukD_KasBankMasuk;
                                    ALTER TABLE dbo.KasBankMasuk SET (LOCK_ESCALATION = TABLE);
                                    COMMIT
                                    BEGIN TRANSACTION;
                                    ALTER TABLE dbo.KasBankMasukD
                                    DROP CONSTRAINT DF_KasBankMasukD_IDReferensi;
                                    ALTER TABLE dbo.KasBankMasukD
                                    DROP CONSTRAINT DF_KasBankMasukD_IDTransaksi;
                                    ALTER TABLE dbo.KasBankMasukD
                                    DROP CONSTRAINT DF_KasBankMasukD_Kurs;
                                    CREATE TABLE dbo.Tmp_KasBankMasukD
                                    (
                                    ID bigint NOT NULL,
                                    IDKasBankMasuk int NOT NULL,
                                    IDAkun nvarchar(15) NOT NULL,
                                    IDRekanan int NOT NULL,
                                    IDReff int NOT NULL,
                                    Nominal money NOT NULL,
                                    Kurs money NOT NULL,
                                    Catatan nvarchar(150) NULL
                                    )  ON [PRIMARY];
                                    ALTER TABLE dbo.Tmp_KasBankMasukD SET (LOCK_ESCALATION = TABLE);
                                    DECLARE @v sql_variant 
                                    SET @v = N'ID Supplier Karyawan Customer'
                                    EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_KasBankMasukD', N'COLUMN', N'IDRekanan';
                                    ALTER TABLE dbo.Tmp_KasBankMasukD ADD CONSTRAINT
                                    DF_KasBankMasukD_IDReferensi DEFAULT ((0)) FOR IDRekanan;
                                    ALTER TABLE dbo.Tmp_KasBankMasukD ADD CONSTRAINT
                                    DF_KasBankMasukD_IDTransaksi DEFAULT ((0)) FOR IDReff;
                                    ALTER TABLE dbo.Tmp_KasBankMasukD ADD CONSTRAINT
                                    DF_KasBankMasukD_Kurs DEFAULT ((1)) FOR Kurs;
                                    IF EXISTS(SELECT * FROM dbo.KasBankMasukD)
                                    EXEC('INSERT INTO dbo.Tmp_KasBankMasukD (ID, IDKasBankMasuk, IDAkun, IDRekanan, IDReff, Nominal, Kurs, Catatan)
                                            SELECT ID, IDKasBankMasuk, IDAkun, IDRekanan, IDReff, Nominal, Kurs, Catatan FROM dbo.KasBankMasukD WITH (HOLDLOCK TABLOCKX)');
                                    DROP TABLE dbo.KasBankMasukD;
                                    EXECUTE sp_rename N'dbo.Tmp_KasBankMasukD', N'KasBankMasukD', 'OBJECT' ;
                                    ALTER TABLE dbo.KasBankMasukD ADD CONSTRAINT
                                    PK_KasBankMasukD PRIMARY KEY CLUSTERED 
                                    (
                                    ID
                                    ) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
                                    ALTER TABLE dbo.KasBankMasukD ADD CONSTRAINT
                                    FK_KasBankMasukD_KasBankMasuk FOREIGN KEY
                                    (
                                    IDKasBankMasuk
                                    ) REFERENCES dbo.KasBankMasuk
                                    (
                                    ID
                                    ) ON UPDATE  NO ACTION 
                                    ON DELETE  CASCADE ;
                                    COMMIT"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = "BEGIN TRANSACTION
                                    SET QUOTED_IDENTIFIER ON
                                    SET ARITHABORT ON
                                    SET NUMERIC_ROUNDABORT OFF
                                    SET CONCAT_NULL_YIELDS_NULL ON
                                    SET ANSI_NULLS ON
                                    SET ANSI_PADDING ON
                                    SET ANSI_WARNINGS ON
                                    COMMIT
                                    BEGIN TRANSACTION;
                                    ALTER TABLE dbo.KasBankKeluarD
                                    DROP CONSTRAINT FK_KasBankKeluarD_KasBankKeluar;
                                    ALTER TABLE dbo.KasBankKeluar SET (LOCK_ESCALATION = TABLE);
                                    COMMIT
                                    BEGIN TRANSACTION;
                                    ALTER TABLE dbo.KasBankKeluarD
                                    DROP CONSTRAINT DF_KasBankKeluarD_IDReferensi;
                                    ALTER TABLE dbo.KasBankKeluarD
                                    DROP CONSTRAINT DF_KasBankKeluarD_IDTransaksi;
                                    ALTER TABLE dbo.KasBankKeluarD
                                    DROP CONSTRAINT DF_KasBankKeluarD_Kurs;
                                    CREATE TABLE dbo.Tmp_KasBankKeluarD
                                    (
                                    ID bigint NOT NULL,
                                    IDKasBankKeluar int NOT NULL,
                                    IDAkun nvarchar(15) NOT NULL,
                                    IDRekanan int NOT NULL,
                                    IDReff int NOT NULL,
                                    Nominal money NOT NULL,
                                    Kurs money NOT NULL,
                                    Catatan nvarchar(150) NULL
                                    )  ON [PRIMARY];
                                    ALTER TABLE dbo.Tmp_KasBankKeluarD SET (LOCK_ESCALATION = TABLE);
                                    DECLARE @v sql_variant 
                                    SET @v = N'ID Supplier Karyawan Customer'
                                    EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_KasBankKeluarD', N'COLUMN', N'IDRekanan';
                                    ALTER TABLE dbo.Tmp_KasBankKeluarD ADD CONSTRAINT
                                    DF_KasBankKeluarD_IDReferensi DEFAULT ((0)) FOR IDRekanan;
                                    ALTER TABLE dbo.Tmp_KasBankKeluarD ADD CONSTRAINT
                                    DF_KasBankKeluarD_IDTransaksi DEFAULT ((0)) FOR IDReff;
                                    ALTER TABLE dbo.Tmp_KasBankKeluarD ADD CONSTRAINT
                                    DF_KasBankKeluarD_Kurs DEFAULT ((1)) FOR Kurs;
                                    IF EXISTS(SELECT * FROM dbo.KasBankKeluarD)
                                    EXEC('INSERT INTO dbo.Tmp_KasBankKeluarD (ID, IDKasBankKeluar, IDAkun, IDRekanan, IDReff, Nominal, Kurs, Catatan)
                                    SELECT ID, IDKasBankKeluar, IDAkun, IDRekanan, IDReff, Nominal, Kurs, Catatan FROM dbo.KasBankKeluarD WITH (HOLDLOCK TABLOCKX)');
                                    DROP TABLE dbo.KasBankKeluarD;
                                    EXECUTE sp_rename N'dbo.Tmp_KasBankKeluarD', N'KasBankKeluarD', 'OBJECT' ;
                                    ALTER TABLE dbo.KasBankKeluarD ADD CONSTRAINT
                                    PK_KasBankKeluarD PRIMARY KEY CLUSTERED 
                                    (
                                    ID
                                    ) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
                                    ALTER TABLE dbo.KasBankKeluarD ADD CONSTRAINT
                                    FK_KasBankKeluarD_KasBankKeluar FOREIGN KEY
                                    (
                                    IDKasBankKeluar
                                    ) REFERENCES dbo.KasBankKeluar
                                    (
                                    ID
                                    ) ON UPDATE  NO ACTION 
                                    ON DELETE  CASCADE ;
                                    COMMIT"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            'Update DB Version
                            sql = "Update [DBVersion] SET [Versi] =4, [TanggalUpdate] =Getdate()"
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
        AddMenu("MasterSatuan", "Master Satuan", True, "MSatuan")
        Return ps
    End Function

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
                                    (N'0101', N'0', N'010101', N'Kas', NULL, 1, 1, 3, 1),
                                    (N'0101', N'0', N'010102', N'Bank', NULL, 1, 0, 3, 0),
                                    (N'0201', N'0', N'020101', N'Hutang', NULL, 0, 1, 3, 0),
                                    (N'0301', N'0', N'030101', N'Modal', NULL, 0, 1, 3, 0),
                                    (N'0601', N'0', N'060101', N'Bensin, Tol dan Parkir', NULL, 0, 1, 3, 0),
                                    (N'0601', N'0', N'060102', N'Komisi & Fee', NULL, 0, 1, 3, 0),
                                    (N'0601', N'0', N'060103', N'Biaya Atk', NULL, 0, 0, 0, 0),
                                    (N'0601', N'0', N'060104', N'Biaya Kirim', NULL, 0, 1, 3, 0),
                                    (N'0601', N'0', N'060199', N'Biaya Lain-Lain', NULL, 0, 0, 0, 0) " & vbCrLf &
                                  " " & vbCrLf &
                                  "END"
                            com.CommandText = sql
                            com.ExecuteNonQuery()


                            sql = "IF OBJECT_ID(N'KasBankKeluar', N'U') IS  NULL" & vbCrLf &
                                  "BEGIN " & vbCrLf &
                                  "CREATE TABLE [dbo].[KasBankKeluar]([ID] [int] NOT NULL,[IsPosted] [bit] NULL,
                                    [Tgl] [date] NOT NULL,[Kode] [nvarchar](15) NOT NULL,[IDKasBank] [nvarchar](15) NOT NULL,
                                    [IDRekanan] [int] NULL,[TipeForm] [smallint] NULL,[KodeReff] [nvarchar](50) NULL,
                                    [Keterangan] [nvarchar](150) NULL,[IsBG] [bit] NULL,[NoGiro] [nvarchar](15) NULL,
                                    [JTBG] [date] NULL,[UserBuat] [nvarchar](50) NULL,[TanggalBuat] [datetime] NULL,
                                    [UserUbah] [nvarchar](50) NULL,[TanggalUbah] [datetime] NULL,
                                    CONSTRAINT [PK_KasBankKeluar] PRIMARY KEY CLUSTERED 
                                    ([ID] ASC )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, 
                                    ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY];
                                    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 All 1 Mutasi Kas Bank Keluar 2 Pelunasan Piutang' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'KasBankKeluar', @level2type=N'COLUMN',@level2name=N'TipeForm';
                                    ALTER TABLE [dbo].[KasBankKeluar] ADD  CONSTRAINT [DF_KasBankKeluar_IsPosted]  DEFAULT ((0)) FOR [IsPosted];
                                    ALTER TABLE [dbo].[KasBankKeluar] ADD  CONSTRAINT [DF_KasBankKeluar_IDRekanan]  DEFAULT ((0)) FOR [IDRekanan];
                                    ALTER TABLE [dbo].[KasBankKeluar] ADD  CONSTRAINT [DF_KasBankKeluar_TipeKasKeluar]  DEFAULT ((0)) FOR [TipeForm];
                                    ALTER TABLE [dbo].[KasBankKeluar] ADD  CONSTRAINT [DF_KasBankKeluar_IsBG]  DEFAULT ((0)) FOR [IsBG];" & vbCrLf &
                                  " " & vbCrLf &
                                  "END"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = "IF OBJECT_ID(N'KasBankKeluarD', N'U') IS  NULL" & vbCrLf &
                                  "BEGIN " & vbCrLf &
                                  "CREATE TABLE [dbo].[KasBankKeluarD](
                                    [ID] [bigint] NOT NULL,
                                    [IDKasBankKeluar] [int] NOT NULL,
                                    [IDAkun] [nvarchar](15) NOT NULL,
                                    [IDRekanan] [int] NOT NULL,
                                    [IDReff] [int] NOT NULL,
                                    [Nominal] [money] NOT NULL,
                                    [Kurs] [money] NOT NULL,
                                    [Catatan] [nvarchar](50) NULL,
                                    CONSTRAINT [PK_KasBankKeluarD] PRIMARY KEY CLUSTERED 
                                    ( [ID] ASC )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                                    ) ON [PRIMARY];
                                    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID Supplier Karyawan Customer' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'KasBankKeluarD', @level2type=N'COLUMN',@level2name=N'IDRekanan';
                                    ALTER TABLE [dbo].[KasBankKeluarD]  WITH CHECK ADD  CONSTRAINT [FK_KasBankKeluarD_KasBankKeluar] FOREIGN KEY([IDKasBankKeluar])
                                    REFERENCES [dbo].[KasBankKeluar] ([ID])
                                    ON DELETE CASCADE;
                                    ALTER TABLE [dbo].[KasBankKeluarD] CHECK CONSTRAINT [FK_KasBankKeluarD_KasBankKeluar];
                                    ALTER TABLE [dbo].[KasBankKeluarD] ADD  CONSTRAINT [DF_KasBankKeluarD_IDReferensi]  DEFAULT ((0)) FOR [IDRekanan];
                                    ALTER TABLE [dbo].[KasBankKeluarD] ADD  CONSTRAINT [DF_KasBankKeluarD_IDTransaksi]  DEFAULT ((0)) FOR [IDReff];
                                    ALTER TABLE [dbo].[KasBankKeluarD] ADD  CONSTRAINT [DF_KasBankKeluarD_Kurs]  DEFAULT ((1)) FOR [Kurs];" & vbCrLf &
                                  " " & vbCrLf &
                                  "END"
                            com.CommandText = sql
                            com.ExecuteNonQuery()


                            sql = "IF OBJECT_ID('vStokMasuk', 'V') IS NOT NULL
                                        DROP VIEW vStokMasuk;"
                            com.CommandText = sql
                            com.ExecuteNonQuery()
                            sql = "CREATE VIEW [dbo].[vStokMasuk]
                                    AS
                                    SELECT     'In' AS Jenis, M.ID, M.Tgl, M.Kode, M.Keterangan, MB.Kode AS KodeBarang, MB.Nama AS NamaBarang, D.Qty, dbo.MSatuan.Nama AS Unit, D.Catatan, M.UserBuat, M.TanggalBuat, M.TanggalUbah, 
                                                          M.UserUbah, D.IDBarang, dbo.MKategori.Kode + N' - ' + dbo.MKategori.Nama AS Kategori, MB.IDKategori
                                    FROM         dbo.StokMasuk AS M INNER JOIN
                                                          dbo.StokMasukD AS D ON M.ID = D.IDStokMasuk INNER JOIN
                                                          dbo.MBarang AS MB ON MB.ID = D.IDBarang LEFT OUTER JOIN
                                                          dbo.MKategori ON MB.IDKategori = dbo.MKategori.ID LEFT OUTER JOIN
                                                          dbo.MSatuan ON D.IDSatuan = dbo.MSatuan.ID"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = "IF OBJECT_ID('vStokKeluar', 'V') IS NOT NULL
                                        DROP VIEW vStokKeluar;"
                            com.CommandText = sql
                            com.ExecuteNonQuery()
                            sql = "CREATE VIEW [dbo].[vStokKeluar]
                                    AS
                                    SELECT     'Out' AS Jenis, M.ID, M.Tgl, M.Kode, M.Keterangan, MB.Kode AS KodeBarang, MB.Nama AS NamaBarang, D.Qty, dbo.MSatuan.Nama AS Unit, D.Catatan, M.UserBuat, M.TanggalBuat, 
                                                          M.TanggalUbah, M.UserUbah, D.IDBarang, dbo.MKategori.Kode + N' - ' + dbo.MKategori.Nama AS Kategori, MB.IDKategori
                                    FROM         dbo.StokKeluar AS M INNER JOIN
                                                          dbo.StokKeluarD AS D ON M.ID = D.IDStokKeluar INNER JOIN
                                                          dbo.MBarang AS MB ON MB.ID = D.IDBarang LEFT OUTER JOIN
                                                          dbo.MKategori ON MB.IDKategori = dbo.MKategori.ID LEFT OUTER JOIN
                                                          dbo.MSatuan ON D.IDSatuan = dbo.MSatuan.ID"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = "CREATE Procedure dbo.spLapKartuStok(@T1 as Date='2020-01-02',@T2 as Date='2020-08-31',@IDBarang as Int=9)
                                    As Begin

                                        DECLARE @Temp AS TABLE (
                                        [I] [int] IDENTITY(1,1) NOT NULL,
                                        [Jenis] [varchar](3) NOT NULL,
                                        [ID] [int] NOT NULL,
                                        [Tgl] [date] NOT NULL,
                                        [Kode] [nvarchar](50) NOT NULL,
                                        [Keterangan] [nvarchar](500) NULL,
                                        [KodeBarang] [nvarchar](13) NOT NULL,
                                        [NamaBarang] [nvarchar](50) NOT NULL,
                                        [SaldoAwal] [int] NULL,
                                        [Qty] [numeric](18, 2) NULL,
                                        [SaldoAkhir] [int] NULL,
                                        [Unit] [nvarchar](50) NULL,
                                        [Catatan] [nvarchar](100) NULL,
                                        [UserBuat] [nvarchar](50) NULL,
                                        [TanggalBuat] [datetime] NULL,
                                        [UserUbah] [nvarchar](50) NULL,
                                        [TanggalUbah] [datetime] NULL,
                                        [IDBarang] [int] NOT NULL)
	
	
                                        Insert Into @Temp
                                        Select * From
                                        (
                                        Select 
                                        --Jenis,ID,Tgl,Kode,Keterangan,KodeBarang,NamaBarang,				SaldoAwal,Qty,			   SaldoAkhir,Unit,Catatan,UserBuat,TanggalBuat,TanggalUbah,UserUbah,IDBarang,UserReport
                                        Jenis,ID,Tgl,Kode,Keterangan,KodeBarang,NamaBarang,Null SaldoAwal,Qty,Null SaldoAkhir,Unit,Catatan,UserBuat,TanggalBuat,UserUbah,TanggalUbah,IDBarang 
                                        From vStokMasuk
                                        Union All 
                                        Select 
                                        Jenis,ID,Tgl,Kode,Keterangan,KodeBarang,NamaBarang,Null SaldoAwal,Qty*-1 As Qty,Null SaldoAkhir,Unit,Catatan,UserBuat,TanggalBuat,UserUbah,TanggalUbah,IDBarang 
                                        From vStokKeluar
                                        ) As S
                                        Where S.Tgl Between @T1 And Dateadd(day,1,@T2) And
                                        Case when @IDBarang=0 then @IDBarang else S.IDBarang end= @IDBarang
                                        Order By S.Tgl,S.Jenis
	
                                        Declare @SaldoAwal as int =0
                                        Select @SaldoAwal=Sum(IsNull(S.Qty,0)) 
                                        From
                                        ( 
                                        Select Sum(Isnull(Qty,0)) Qty From vStokMasuk Where  Tgl <@T1 And Case when @IDBarang=0 then @IDBarang else IDBarang end= @IDBarang
                                        Union All 
                                        Select Sum(-1*Isnull(Qty,0)) Qty From vStokKeluar Where Tgl <@T1 And Case when @IDBarang=0 then @IDBarang else IDBarang end= @IDBarang 
                                        ) As S
	
                                        DECLARE @i AS INT, @iMax AS INT,@Qty as Int=0
                                        --SELECT @i=1, @iMax=MAX(NoID) FROM TempStok T
                                        SELECT @i=0, @iMax=Max(I) FROM @Temp T
                                        WHILE (@i<=@iMax)
                                        BEGIN
                                                Select @Qty=Qty From @Temp Where I=@i
                                                update @Temp Set SaldoAwal= @SaldoAwal,SaldoAkhir=@SaldoAwal+@Qty Where I=@i
                                                Set @SaldoAwal=@SaldoAwal+@Qty
                                                Set @i+=1
                                        END
	
                                        Select * from @Temp 
                                        --Select Jenis,ID,Tgl,Kode,Keterangan,KodeBarang,NamaBarang,SaldoAwal,QtyIn,QtyOut,SaldoAkhir,Unit,Catatan,UserBuat,TanggalBuat,UserUbah,TanggalUbah,IDBarang
                                    End"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = "CREATE Procedure dbo.spLapSaldoStok(@Tanggal as Date='2020-08-31',@IDKategori as Int=0)
                                    As Begin
                                    Select MK.Kode + ' - ' + MK.Nama Kategori,MB.Kode KodeBarang,MB.Nama NamaBarang,Sum(S.Qty) Saldo,S.IDBarang,MB.Catatan
                                    From
                                    (
                                        Select 
                                        IDBarang ,Sum(IsNull(Qty,0)) as Qty
                                        From vStokMasuk Where Tgl < DateAdd(day,1,@Tanggal)
                                        Group By IDBarang
                                        Union All 
                                        Select 
                                        IDBarang ,Sum(-1*IsNull(Qty,0)) as Qty
                                        From vStokKeluar Where Tgl < DateAdd(day,1,@Tanggal)
                                        Group By IDBarang
                                    ) As S 
                                    Inner Join MBarang MB on MB.ID=S.IDBarang
                                    Left Join MKategori MK on MK.ID=MB.IDKategori
                                    Where Case when @IDKategori<=0 then @IDKategori else MB.IDkategori end= @IDKategori
                                    Group By MK.Kode + ' - ' + MK.Nama ,MB.Nama ,S.IDBarang,MB.Kode,MB.Catatan
                                    Order By MB.Nama
	
                                    End"
                            com.CommandText = sql
                            com.ExecuteNonQuery()

                            sql = "CREATE Procedure dbo.spLapTopItem(@TglAwal as Date='2019-08-1',@TglAkhir as Date='2020-08-31',@IDKategori as Int=0)
                                    As Begin
                                    Select Kategori,KodeBarang,NamaBarang,Sum(IsNULL(Qty,0)) TotalItem 
                                    from vStokKeluar S
                                    Where  S.Tgl Between @TglAwal And Dateadd(day,1,@TglAkhir) And
	                                    Case when @IDKategori<=0 then @IDKategori else S.IDKategori end= @IDKategori
                                    Group By Kategori,KodeBarang,NamaBarang
                                    Order By Sum(IsNULL(Qty,0)) desc
                                    END"
                            com.CommandText = sql
                            com.ExecuteNonQuery()


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
        UpdateV4()
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
