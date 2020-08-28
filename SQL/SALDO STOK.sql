USE Magnus
GO
/****** Object:  StoredProcedure dbo.spLapKartuStok    Script Date: 08/28/2020 21:56:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter Procedure dbo.spLapSaldoStok(@Tanggal as Date='2020-08-31',@IDKategori as Int=0)
As Begin
Select MK.Kode + ' - ' + MK.Nama Kategori,MB.Nama NamaBarang,Sum(S.Qty) Saldo,S.IDBarang,MB.Kode KodeBarang
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
Group By MK.Kode + ' - ' + MK.Nama ,MB.Nama ,S.IDBarang,MB.Kode
Order By MB.Nama
	
End