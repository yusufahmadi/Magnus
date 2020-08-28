ALTER Procedure spLapTopItem(@TglAwal as Date='2019-08-1',@TglAkhir as Date='2020-08-31',@IDKategori as Int=0)
As Begin
Select Kategori,KodeBarang,NamaBarang,Sum(IsNULL(Qty,0)) TotalItem 
from vStokKeluar S
Where  S.Tgl Between @TglAwal And Dateadd(day,1,@TglAkhir) And
	Case when @IDKategori<=0 then @IDKategori else S.IDKategori end= @IDKategori
Group By Kategori,KodeBarang,NamaBarang
Order By Sum(IsNULL(Qty,0)) desc
END
