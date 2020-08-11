Imports System.IO
Imports System.Security.Cryptography
Public Module modPub
    Public conStr As String = "Server=(local);Database=Magnus;User Id=sa;Password=Sg1;"

    Public AppName As String = "Magnus"
    Public Username As String = ""
    Public IDRoleUser As Integer = 0
    Public RoleUser As String = ""
    Public IDTypeLayout As Integer = 0

    Private enc As System.Text.UTF8Encoding
    Private encryptor As ICryptoTransform
    Private decryptor As ICryptoTransform
    Public Function strEncrypt(ByVal sPlainText As String, Optional ByRef Pesan As String = "") As String
        Dim Result As String = ""
        Try
            If Not String.IsNullOrEmpty(sPlainText) Then
                Dim memoryStream As MemoryStream = New MemoryStream()
                Dim cryptoStream As CryptoStream = New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
                cryptoStream.Write(enc.GetBytes(sPlainText), 0, sPlainText.Length)
                cryptoStream.FlushFinalBlock()
                Result = Convert.ToBase64String(memoryStream.ToArray())
                Pesan = "Success"
                memoryStream.Close()
                cryptoStream.Close()
            End If
        Catch ex As Exception
            Pesan = ex.Message
            Result = ""
        End Try
        Return Result
    End Function
    Public Function strDecrypt(ByVal sPlainText As String, ByRef Pesan As String) As String
        Dim Result As String = ""
        Try
            Dim cypherTextBytes As Byte() = Convert.FromBase64String(sPlainText)
            Dim memoryStream As MemoryStream = New MemoryStream(cypherTextBytes)
            Dim cryptoStream As CryptoStream = New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)
            Dim plainTextBytes(cypherTextBytes.Length) As Byte
            Dim decryptedByteCount As Integer = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)
            memoryStream.Close()
            cryptoStream.Close()
            Result = enc.GetString(plainTextBytes, 0, decryptedByteCount)
            Pesan = "Success"
        Catch ex As Exception
            Pesan = ex.Message
            Result = ""
        End Try
        Return Result
    End Function
End Module
