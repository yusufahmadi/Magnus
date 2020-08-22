Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Public Module modPub
    Public Function conStr(Optional ByVal strCon As String = "") As String '= "Server=(local);Database=Magnus;User Id=sa;Password=Sg1;"
        If strCon <> "" Then
            conStr = strCon
        Else
            If Utils.ObjToBool(Ini.BacaIni("Application", "WindowsAuth", "False")) Then
                conStr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=" & Ini.BacaIni("Application", "Database", "Magnus") &
                    ";Data Source=" & Ini.BacaIni("Application", "Server", ".") &
                    ";Connect Timeout=" & Ini.BacaIni("Application", "Timeout", "10") & ";"
            Else
                conStr = "Server=" & Ini.BacaIni("Application", "Server", ".") &
                    ";Database=" & Ini.BacaIni("Application", "Database", "Magnus") &
                    ";User Id=" & Ini.BacaIni("Application", "User", "sa") &
                    ";Password=" & AES_Decrypt(Ini.BacaIni("Application", "Password", "")) &
                    ";Connect Timeout=" & Ini.BacaIni("Application", "Timeout", "10") & ";"
            End If
        End If
        Return conStr
    End Function

    Public NamaAplikasi As String = "Magnus"
    Public NamaPerusahaan As String = ""
    Public AlamatPerusahaan As String = ""
    Public KotaPerusahaan As String = ""
    Public FolderLayouts As String = Application.StartupPath & "\System\Layouts\"
    Public Username As String = ""
    Public IDRoleUser As Integer = 0
    Public RoleUser As String = ""
    Public IDTypeLayout As Integer = 0
    Public KeyPas As String = My.Settings.KeyPass

    Public LevelPerkiraan As Integer = 3

    Public listMenu As New List(Of MenuRoleUser)
    Public listAkunFull As New List(Of Akun)
    Public listAkunLv2 As New List(Of AkunLv2)
    Public listAkunLv1 As New List(Of AkunLv1)

    Private enc As System.Text.UTF8Encoding
    Private encryptor As ICryptoTransform
    Private decryptor As ICryptoTransform

    Public IsEditReport As Boolean = False
    Public Enum ActionReport
        Edit = 0
        Preview = 1
        Print = 2
    End Enum

    Public Sub RefreshListAkunFull()
        Dim ds As New DataSet
        Dim Sql As String = ""
        Try
            Sql = "SELECT * From MAkun ORDER BY ID ASC"
            ds = Query.ExecuteDataSet(Sql, "Akun")
            If Not ds Is Nothing Then
                If ds.Tables("Akun").Rows.Count > 0 Then
                    listAkunFull = New List(Of Akun)
                    Dim e As Akun = Nothing
                    listAkunFull.Clear()
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        e = New Akun
                        With e
                            .ID = Utils.NullToStr(ds.Tables(0).Rows(i).Item("ID"))
                            .IDAkunLv2 = Utils.NullToStr(ds.Tables(0).Rows(i).Item("IDAkunLv2"))
                            .IDParent = Utils.NullToStr(ds.Tables(0).Rows(i).Item("IDParent"))
                            .Nama = Utils.NullToStr(ds.Tables(0).Rows(i).Item("Nama"))
                            .Level = Utils.ObjToInt(ds.Tables(0).Rows(i).Item("Level"))
                            .IDJenisBukuPembantu = Utils.ObjToInt(ds.Tables(0).Rows(i).Item("IDJenisBukuPembantu"))
                            .IsActive = Utils.ObjToBool(ds.Tables(0).Rows(i).Item("IsActive"))
                            .IsKas = Utils.ObjToBool(ds.Tables(0).Rows(i).Item("IsKas"))
                        End With
                        listAkunFull.Add(e)
                    Next
                End If
            End If
        Catch ex As Exception
        Finally
            If Not ds Is Nothing Then
                ds.Dispose()
            End If
        End Try
    End Sub

    ', Optional ByVal pass As String = "Kia"
    Public Function AES_Encrypt(ByVal input As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(KeyPas))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return encrypted
        Catch ex As Exception
            Return input 'If encryption fails, return the unaltered input.
        End Try
    End Function
    'Decrypt a string with AES 
    ', ByVal pass As String
    Public Function AES_Decrypt(ByVal input As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(KeyPas))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
            Return input 'If decryption fails, return the unaltered input.
        End Try
    End Function
End Module
