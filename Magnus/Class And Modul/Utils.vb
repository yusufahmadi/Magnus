Imports System.Data.SqlClient
Imports System.Net
Imports System.Threading
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports Magnus.modParser

Public Module Utils
    Public Property DefIDDepartemen As Object
    Public Property DefIDSupplier As Object
    Public Property DefIDSatuanfrmBarang As Object
    Public Property DefIDCustomer As Object
    Public Property DefIDPegawai As Object
    Public Property DefIDSatuan As Object
    Public Property DefIDGudang As Object
    Public Property DefIDWilayah As Object
    Public Parser As New modParser

    Public Function GetTableNamebyFormname(ByVal Value As Object, ByRef strCaption As String) As String
        Dim SQLconnect As New SqlConnection(conStr)
        Dim SQLcommand As SqlCommand
        Dim odr As SqlDataReader
        Dim hasil As String = ""
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "SELECT namatabel,namatabeldetil,caption FROM sysformheader where namaform='" & NullToStr(Value) & "'"
        odr = SQLcommand.ExecuteReader
        If odr.Read Then
            hasil = NullToStr(odr.GetValue(0)) & IIf(NullToStr(odr.GetValue(1)).Trim = "", "", "," & NullToStr(odr.GetValue(1)))
            strCaption = NullToStr(odr.GetValue(2))
        End If
        odr.Close()
        SQLcommand.Dispose()
        SQLconnect.Close()
        SQLconnect.Dispose()

        Return hasil
    End Function
    Public Function ObjToInt(ByVal Obj As Object) As Integer
        Try
            ObjToInt = Convert.ToInt32(Obj)
        Catch ex As Exception
            ObjToInt = 0
        End Try
        Return ObjToInt
    End Function

    Public Function ObjToLong(ByVal Obj As Object) As Long
        Try
            ObjToLong = Convert.ToInt64(Obj)
        Catch ex As Exception
            ObjToLong = 0
        End Try
        Return ObjToLong
    End Function
    Public Function ObjToDbl(ByVal Obj As Object) As Double
        Try
            ObjToDbl = Convert.ToDouble(Obj)
        Catch ex As Exception
            ObjToDbl = 0
        End Try
        Return ObjToDbl
    End Function
    Public Function ObjToBool(ByVal Obj As Object) As Boolean
        Try
            ObjToBool = Convert.ToBoolean(Obj)
        Catch ex As Exception
            ObjToBool = False
        End Try
        Return ObjToBool
    End Function

    Public Function ObjToBit(ByVal Obj As Object) As Short
        Try
            If Convert.ToBoolean(Obj) Then
                ObjToBit = 1
            Else
                ObjToBit = 0
            End If
        Catch ex As Exception
            ObjToBit = 0
        End Try
        Return ObjToBit
    End Function

    Public Function ObjToDateTime(ByVal X As Object) As DateTime
        If TypeOf X Is Date OrElse IsDate(X) Then
            Return Convert.ToDateTime(X)
        Else
            Return Convert.ToDateTime("1/1/1900 00:00")
        End If
    End Function

    Public Function ObjToDate(ByVal X As Object) As Date
        If TypeOf X Is Date OrElse IsDate(X) Then
            Return CDate(X)
        Else
            Return CDate("1/1/1900")
        End If
    End Function
    Public Function ObjToStrDateSql(ByVal X As Object) As String
        If TypeOf X Is Date Then
            Return "'" & Format(CDate(X), "yyyy-MM-dd") & "'"
        Else
            Return "NULL"
        End If
    End Function

    Public Function ObjToDateMDB(ByVal X As Object) As String
        If TypeOf X Is Date Then
            Return "#" & Format(CDate(X), "MM/dd/yyyy") & "#"
        Else
            Return "NULL"
        End If
    End Function

    Public Function GetLocalIP() As String
        Dim result As String = ""
        Dim IPList As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName)
        For Each IPaddress In IPList.AddressList
            'Only return IPv4 routable IPs
            If (IPaddress.AddressFamily = Sockets.AddressFamily.InterNetwork) Then 'AndAlso (Not IsPrivateIP(IPaddress.ToString)) 
                If result.Trim = String.Empty Then
                    result = IPaddress.ToString
                Else
                    result = result & " / " & IPaddress.ToString
                End If
            End If
        Next
        Return result
    End Function

    Public Function IsPrivateIP(ByVal CheckIP As String) As Boolean
        Dim Quad1, Quad2 As Integer

        Quad1 = CInt(CheckIP.Substring(0, CheckIP.IndexOf(".")))
        Quad2 = CInt(CheckIP.Substring(CheckIP.IndexOf(".") + 1).Substring(0, CheckIP.IndexOf(".")))
        Select Case Quad1
            Case 10
                Return True
            Case 172
                If Quad2 >= 16 And Quad2 <= 31 Then Return True
            Case 192
                If Quad2 = 168 Then Return True
        End Select
        Return False
    End Function
    Public Function NullToStr(ByVal Value As Object) As String
        If IsDBNull(Value) Then
            Return ""
        ElseIf Value Is Nothing Then
            Return ""
        Else
            Return Value.ToString
        End If
    End Function
    Public Function FixApostropi(ByVal obj As Object) As String
        Dim x As String = ""
        Try
            x = obj.ToString.Replace("'", "''")
        Catch ex As Exception
            x = ""
        End Try
        Return x
    End Function
    Public Function FixKoma(ByVal obj As Object) As String
        Dim x As String = ""
        Try
            x = obj.ToString.Replace(",", ".")
        Catch ex As Exception
            x = ""
        End Try
        Return x
    End Function
    Public Sub BukaFile(ByVal nmfile As String)
        Try
            Dim p As New System.Diagnostics.ProcessStartInfo
            p.Verb = "Open"
            p.WindowStyle = ProcessWindowStyle.Normal
            p.FileName = nmfile
            p.UseShellExecute = True
            System.Diagnostics.Process.Start(p)
        Catch ex As Exception
            XtraMessageBox.Show("Ada Kesalahan :" & vbCrLf & ex.Message & vbCrLf & "File : " & nmfile, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Public Function CheckFileFunction(ByVal xFile As String) As Boolean
        Return System.IO.File.Exists(xFile)
    End Function
    Public Function FileExists(ByVal xFile As String, Optional ByVal timeout As Integer = 5000) As Boolean
        Dim exists As Boolean = True
        Dim t As New Thread(DirectCast(Function() CheckFileFunction(xFile), ThreadStart))
        t.Start()
        Dim completed As Boolean = t.Join(timeout)
        If Not completed Then
            exists = False
            t.Abort()
        Else
            exists = CheckFileFunction(xFile)
        End If
        Return exists
    End Function

    Public Function CheckPathFunction(ByVal path As String) As Boolean
        Return System.IO.Directory.Exists(path)
    End Function
    Public Function DirectoryExists(ByVal path As String, Optional ByVal timeout As Integer = 10000) As Boolean 'milisecond
        Dim exists As Boolean = True
        Dim t As New Thread(DirectCast(Function() CheckPathFunction(path), ThreadStart))
        t.Start()
        Dim completed As Boolean = t.Join(timeout)
        If Not completed Then
            exists = False
            t.Abort()
        Else
            exists = CheckPathFunction(path)
        End If
        Return exists
    End Function
    Public Function GCtoDSRowFiltered(ByVal view As GridControl, Optional ByVal Filter As String = "") As DataSet
        Using dsX As New DataSet
            Dim dv As New DataView
            dv = TryCast(view.DataSource, DataTable).Copy().DefaultView
            dv.RowFilter = Filter
            If Not dsX Is Nothing Then
                dsX.Tables.Clear()
                dsX.Clear()
            End If
            dsX.Tables.Add(dv.ToTable())
            Return dsX
        End Using
    End Function

    Public Function GetDBType(ByVal SysType As Type) As SqlDbType
        Dim Par As SqlClient.SqlParameter
        Dim TConv As System.ComponentModel.TypeConverter

        Par = New SqlClient.SqlParameter()
        TConv = System.ComponentModel.TypeDescriptor.GetConverter(Par.DbType)
        If TConv.CanConvertFrom(SysType) Then
            Par.DbType = TConv.ConvertFrom(SysType.Name)
        Else
            Try
                Par.DbType = TConv.ConvertFrom(SysType.Name)
            Catch ex As Exception

            End Try
        End If
        Return Par.DbType
    End Function

    Public Function IsiVariabelDef(ByVal strsql As String) As String
        Dim strsql1 As String = strsql
        If InStr(strsql.ToLower, "{DefIDDepartemen}".ToLower, CompareMethod.Text) > 0 Then
            strsql1 = Replace(strsql1, "{DefIDDepartemen}", DefIDDepartemen.ToString)
        End If
        If InStr(strsql1, "{DefIDWilayah}".ToLower, CompareMethod.Text) > 0 Then
            strsql1 = Replace(strsql1, "{DefIDWilayah}", DefIDWilayah.ToString)
        End If
        If InStr(strsql1, "{DefIDGudang}".ToLower, CompareMethod.Text) > 0 Then
            strsql1 = Replace(strsql1, "{DefIDGudang}", DefIDGudang.ToString)
        End If
        If InStr(strsql1, "{DefIDSatuan}".ToLower, CompareMethod.Text) > 0 Then
            strsql1 = Replace(strsql1, "{DefIDSatuan}", DefIDSatuan.ToString)
        End If
        If InStr(strsql1, "{DefIDPegawai}".ToLower, CompareMethod.Text) > 0 Then
            strsql1 = Replace(strsql1, "{DefIDPegawai}", DefIDPegawai.ToString)
        End If
        If InStr(strsql1, "{DefIDCustomer}".ToLower, CompareMethod.Text) > 0 Then
            strsql1 = Replace(strsql1, "{DefIDCustomer}", DefIDCustomer.ToString)
        End If
        If InStr(strsql1, "{DefIDSatuanfrmBarang}", CompareMethod.Text) > 0 Then
            strsql1 = Replace(strsql1, "{DefIDSatuanfrmBarang}", DefIDSatuanfrmBarang.ToString)
        End If
        If InStr(strsql1, "{DefIDSupplier}".ToLower, CompareMethod.Text) > 0 Then
            strsql1 = Replace(strsql1, "{DefIDSupplier}", DefIDSupplier.ToString)
        End If
        'If InStr(strsql1, "{iduseraktif}".ToLower, CompareMethod.Text) > 0 Then
        '    strsql1 = Replace(strsql1, "{iduseraktif}", IDUserAktif.ToString)
        'End If
        If InStr(strsql1, "{namauseraktif}".ToLower, CompareMethod.Text) > 0 Then
            strsql1 = Replace(strsql1, "{namauseraktif}", Username.ToString)
        End If
        Return strsql1
    End Function

    Public Function CekUnique(ByVal Kode As String, ByVal KodeOld As String, ByVal NamaTabel As String, ByVal Field As String, ByVal IsEdit As Boolean, Optional ByVal Filternya As String = "", Optional ByRef NoID As Long = -1, Optional ByRef FieldNoID As String = "NoID") As Boolean
        Dim x As Boolean
        Dim dbs As New DataSet
        Dim rs As String
        Try
            If IsEdit Then
                rs = "SELECT " & Field & " FROM " & NamaTabel &
                     " WHERE " & Field & "='" & Replace(Kode, "'", "''") & "' and " & FieldNoID & "<>" & NoID & " " & Filternya
                '" WHERE " & Field & "='" & Replace(Kode, "'", "''") & "' and " & Field & "<>'" & Replace(KodeOld, "'", "''") & "'" & " " & Filternya
            Else
                rs = "SELECT " & Field & " FROM " & NamaTabel &
                     " WHERE " & Field & "='" & Replace(Kode, "'", "''") & "'" & " " & Filternya
            End If
            dbs = Query.ExecuteDataSet(NamaTabel, rs)
            If dbs.Tables(NamaTabel).Rows.Count >= 1 Then
                x = True
            Else
                x = False
            End If
        Catch ex As Exception
            x = False
        Finally
            dbs.Dispose()
        End Try
        Return x
    End Function
    Public Function Evaluate(ByVal kalimat As String) As Double
        Dim DecSep As String
        Dim Nfi As System.Globalization.NumberFormatInfo = System.Globalization.CultureInfo.InstalledUICulture.NumberFormat
        DecSep = Nfi.NumberDecimalSeparator
        kalimat = kalimat.Replace(".", DecSep).Replace(",", DecSep)
        Parser.Function = kalimat
        Parser.BuildFunctionTree()
        Return Parser.Result
    End Function

    Public Function getValueFromLookup(ByVal sender As Object, ByVal fieldname As String) As String
        Dim lu As SearchLookUpEdit = CType(sender, SearchLookUpEdit) 'DXSample.Custom
        Dim strtablefield As String() = Split(fieldname, ".")
        Dim row As System.Data.DataRow
        Dim val As String = ""
        Try
            If lu.Properties.View.Columns Is Nothing Or lu.Properties.View.DataRowCount = 0 Then
                If strtablefield(0) <> "" And strtablefield(1) <> "" Then
                    val = NullToStr(Query.ExecuteScalar("SELECT " & strtablefield(1).ToString & " from " & strtablefield(0).ToString & " where " & lu.Properties.ValueMember.ToString & "=" & lu.EditValue))
                End If
            Else
                row = lu.Properties.View.GetDataRow(lu.Properties.View.FocusedRowHandle)
                val = NullToStr(row(strtablefield(1)))
            End If
        Catch ex As Exception
            val = ""
        End Try
        Return val
    End Function
End Module


