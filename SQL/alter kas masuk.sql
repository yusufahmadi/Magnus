/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.KasBankMasukD
	DROP CONSTRAINT FK_KasBankMasukD_KasBankMasuk
GO
ALTER TABLE dbo.KasBankMasuk SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.KasBankMasukD
	DROP CONSTRAINT DF_KasBankMasukD_IDReferensi
GO
ALTER TABLE dbo.KasBankMasukD
	DROP CONSTRAINT DF_KasBankMasukD_IDTransaksi
GO
ALTER TABLE dbo.KasBankMasukD
	DROP CONSTRAINT DF_KasBankMasukD_Kurs
GO
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
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_KasBankMasukD SET (LOCK_ESCALATION = TABLE)
GO
DECLARE @v sql_variant 
SET @v = N'ID Supplier Karyawan Customer'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_KasBankMasukD', N'COLUMN', N'IDRekanan'
GO
ALTER TABLE dbo.Tmp_KasBankMasukD ADD CONSTRAINT
	DF_KasBankMasukD_IDReferensi DEFAULT ((0)) FOR IDRekanan
GO
ALTER TABLE dbo.Tmp_KasBankMasukD ADD CONSTRAINT
	DF_KasBankMasukD_IDTransaksi DEFAULT ((0)) FOR IDReff
GO
ALTER TABLE dbo.Tmp_KasBankMasukD ADD CONSTRAINT
	DF_KasBankMasukD_Kurs DEFAULT ((1)) FOR Kurs
GO
IF EXISTS(SELECT * FROM dbo.KasBankMasukD)
	 EXEC('INSERT INTO dbo.Tmp_KasBankMasukD (ID, IDKasBankMasuk, IDAkun, IDRekanan, IDReff, Nominal, Kurs, Catatan)
		SELECT ID, IDKasBankMasuk, IDAkun, IDRekanan, IDReff, Nominal, Kurs, Catatan FROM dbo.KasBankMasukD WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.KasBankMasukD
GO
EXECUTE sp_rename N'dbo.Tmp_KasBankMasukD', N'KasBankMasukD', 'OBJECT' 
GO
ALTER TABLE dbo.KasBankMasukD ADD CONSTRAINT
	PK_KasBankMasukD PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.KasBankMasukD ADD CONSTRAINT
	FK_KasBankMasukD_KasBankMasuk FOREIGN KEY
	(
	IDKasBankMasuk
	) REFERENCES dbo.KasBankMasuk
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
COMMIT
