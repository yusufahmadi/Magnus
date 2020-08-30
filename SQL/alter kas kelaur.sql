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
ALTER TABLE dbo.KasBankKeluarD
	DROP CONSTRAINT FK_KasBankKeluarD_KasBankKeluar
GO
ALTER TABLE dbo.KasBankKeluar SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.KasBankKeluarD
	DROP CONSTRAINT DF_KasBankKeluarD_IDReferensi
GO
ALTER TABLE dbo.KasBankKeluarD
	DROP CONSTRAINT DF_KasBankKeluarD_IDTransaksi
GO
ALTER TABLE dbo.KasBankKeluarD
	DROP CONSTRAINT DF_KasBankKeluarD_Kurs
GO
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
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_KasBankKeluarD SET (LOCK_ESCALATION = TABLE)
GO
DECLARE @v sql_variant 
SET @v = N'ID Supplier Karyawan Customer'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_KasBankKeluarD', N'COLUMN', N'IDRekanan'
GO
ALTER TABLE dbo.Tmp_KasBankKeluarD ADD CONSTRAINT
	DF_KasBankKeluarD_IDReferensi DEFAULT ((0)) FOR IDRekanan
GO
ALTER TABLE dbo.Tmp_KasBankKeluarD ADD CONSTRAINT
	DF_KasBankKeluarD_IDTransaksi DEFAULT ((0)) FOR IDReff
GO
ALTER TABLE dbo.Tmp_KasBankKeluarD ADD CONSTRAINT
	DF_KasBankKeluarD_Kurs DEFAULT ((1)) FOR Kurs
GO
IF EXISTS(SELECT * FROM dbo.KasBankKeluarD)
	 EXEC('INSERT INTO dbo.Tmp_KasBankKeluarD (ID, IDKasBankKeluar, IDAkun, IDRekanan, IDReff, Nominal, Kurs, Catatan)
		SELECT ID, IDKasBankKeluar, IDAkun, IDRekanan, IDReff, Nominal, Kurs, Catatan FROM dbo.KasBankKeluarD WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.KasBankKeluarD
GO
EXECUTE sp_rename N'dbo.Tmp_KasBankKeluarD', N'KasBankKeluarD', 'OBJECT' 
GO
ALTER TABLE dbo.KasBankKeluarD ADD CONSTRAINT
	PK_KasBankKeluarD PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.KasBankKeluarD ADD CONSTRAINT
	FK_KasBankKeluarD_KasBankKeluar FOREIGN KEY
	(
	IDKasBankKeluar
	) REFERENCES dbo.KasBankKeluar
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
COMMIT
