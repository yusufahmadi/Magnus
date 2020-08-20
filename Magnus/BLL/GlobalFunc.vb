Public Class GlobalFunc
    Public Shared Function GetKodeTransaksi(ByVal Tanggal As Date, ByVal KodeDepan As String, ByVal TableName As String, Optional ByVal Sparator As String = ".", Optional digit As Integer = 4) As Pesan
        Dim formating As String = ""
        Dim Kode As String = "", sql As String = ""
        Dim e As New Pesan With {.Hasil = False, .Message = "Generate Kode Gagal", .Value = "Gen Kode Gagal"}
        Try
            Kode = KodeDepan & Sparator & Tanggal.ToString("yy") & Tanggal.ToString("MM") & Sparator
            For i As Integer = 0 To digit - 1
                formating &= "0"
            Next
            sql = "Select IsNull(MAX(SUBSTRING(KODE," & (Kode.Length + 1) & "," & digit & ")),0)+1 " & vbCrLf &
                  " From " & TableName & " " & vbCrLf &
                  " Where Left(KODE," & Kode.Length & ")='" & Kode & "'"
            Kode = Kode & Format(Utils.ObjToInt(Query.ExecuteScalar(sql)), formating)
            With e
                e.Hasil = False
                e.Message = "OK"
                e.Value = Kode
            End With
        Catch ex As Exception
            With e
                e.Hasil = False
                e.Message = ex.Message
                e.Value = "Gen Kode Gagal"
            End With
        End Try
        Return e
    End Function
End Class
