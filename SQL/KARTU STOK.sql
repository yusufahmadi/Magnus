USE Magnus
GO
/****** Object:  StoredProcedure dbo.spLapKartuStok    Script Date: 08/28/2020 21:56:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER Procedure dbo.spLapKartuStok(@T1 as Date='2020-01-02',@T2 as Date='2020-08-31',@IDBarang as Int=9)
As Begin

	--Delete TempStok Where UserReport = @Username
	
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
End