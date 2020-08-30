Select * From KasBankMasuk A
Select * From KasBankMasukD B

Alter VIEW vKasBankMasuk
As 
Select 'D' Jenis,A.Tgl,A.Kode,MKB.Nama,MRH.Nama Rekanan,A.Keterangan,
B.IDAkun,MA.Nama Akun,MRD.Nama Sub,B.Nominal,B.Catatan ,A.UserBuat,A.UserUbah,A.TanggalBuat,A.TanggalUbah
From KasBankMasuk A
Inner Join KasBankMasukD B On A.ID=B.IDKasBankMasuk
Left Join MAkun MKB on MKB.ID=A.IDKasBank
Left Join MRekanan MRH on MRH.ID=A.IDRekanan
Left Join MAkun MA on MA.ID=B.IDAkun
Left Join MRekanan MRD on MRD.ID=B.IDRekanan


Alter View vKasBankKeluar
as 
Select 'K' Jenis,A.Tgl,A.Kode,MKB.Nama,MRH.Nama Rekanan,A.Keterangan,
B.IDAkun,MA.Nama Akun,MRD.Nama Sub,B.Nominal,B.Catatan ,A.UserBuat,A.UserUbah,A.TanggalBuat,A.TanggalUbah
From KasBankKeluar A
Inner Join KasBankKeluarD B On A.ID=B.IDKasBankKeluar
Left Join MAkun MKB on MKB.ID=A.IDKasBank
Left Join MRekanan MRH on MRH.ID=A.IDRekanan
Left Join MAkun MA on MA.ID=B.IDAkun
Left Join MRekanan MRD on MRD.ID=B.IDRekanan



Alter Procedure spLapKasHarian(@T1 as Date='2020-01-02',@T2 as Date='2020-08-31')
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
	
END


Procedure spLapKasPerKategori (@T1 as Date='2020-01-02',@T2 as Date='2020-08-31',@IDAkun as varchar(20)='')
As Begin
	Select * from vKasBankKeluar  S
	Where S.Tgl Between @T1 And Dateadd(day,1,@T2) 
	And Case when @IDAkun='' then @IDAkun else S.IDAkun end= @IDAkun
    Order By S.Tgl,S.Jenis
END