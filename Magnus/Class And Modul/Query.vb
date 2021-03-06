﻿Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports DevExpress.XtraEditors
Imports Newtonsoft.Json

Public Class Query
    Public Shared Function Execute(ByVal sql As String, Optional ByVal strKoneksi As String = "") As Pesan
        Dim e As New Pesan With {.Hasil = True, .Message = "", .Value = Nothing}
        If strKoneksi = "" Then
            strKoneksi = conStr
        End If
        Using cn As New SqlConnection(strKoneksi)
            Using cm As New SqlCommand()
                Try
                    cn.Open()
                    cm.Connection = cn
                    cm.CommandTimeout = cn.ConnectionTimeout

                    cm.CommandText = sql
                    With e
                        .Value = cm.ExecuteNonQuery()
                        .Hasil = True
                        .Message = "Query Execute Success"
                    End With
                Catch ex As Exception
                    With e
                        .Hasil = False
                        .Message = ex.Message
                        .Value = ""
                    End With
                End Try
            End Using
        End Using
        Return e
    End Function
    Public Shared Function ExecuteScalar(ByVal sql As String, Optional ByVal strKoneksi As String = "", Optional ByRef IsReturnJson As Boolean = False) As String
        Dim e As New Pesan With {.Hasil = False, .Message = "", .Value = Nothing}
        If strKoneksi = "" Then
            strKoneksi = conStr
        End If
        Using cn As New SqlConnection(strKoneksi)
            Using cm As New SqlCommand()
                Try
                    cn.Open()
                    cm.Connection = cn
                    cm.CommandTimeout = cn.ConnectionTimeout

                    cm.CommandText = sql
                    e.Value = Utils.NullToStr(cm.ExecuteScalar())
                    e.Hasil = True
                    e.Message = "Sukses"
                Catch ex As Exception
                    DevExpress.XtraEditors.XtraMessageBox.Show("Execute Scalar " & ex.Message, NamaAplikasi)
                    e.Hasil = False
                    e.Message = ex.Message
                    e.Value = ""
                End Try
            End Using
        End Using
        If IsReturnJson Then
            Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
            serializer.MaxJsonLength = Int32.MaxValue '; //increase MaxJsonLength.  This could be read in from the web.config if you prefer
            Return serializer.Serialize(e)
        Else
            Return e.Value.ToString
        End If
    End Function
    Public Shared Function ExecuteDataSet(ByVal sql As String, Optional ByVal TableName As String = "Data", Optional ByVal strKoneksi As String = "") As DataSet
        Dim ds As New DataSet
        If strKoneksi = "" Then
            strKoneksi = conStr()
        End If
        Using cn As New SqlConnection(strKoneksi)
            Using cm As New SqlCommand()
                Using da As New SqlDataAdapter
                    Try
                        cn.Open()
                        cm.Connection = cn
                        cm.CommandTimeout = cn.ConnectionTimeout
                        da.SelectCommand = cm
                        cm.CommandText = sql
                        da.Fill(ds, TableName)
                    Catch ex As Exception
                        ds = Nothing
                    End Try
                End Using
            End Using
        End Using
        Return ds
    End Function

    Public Shared Function ExecuteDataSetToJson(ByVal sql As String, Optional ByVal strKoneksi As String = "") As String
        Dim e As New Pesan With {.Hasil = True, .Message = "", .Value = Nothing}
        If strKoneksi = "" Then
            strKoneksi = conStr
        End If
        Dim ds As DataSet = Nothing
        Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
        Using cn As New SqlConnection(strKoneksi)
            Using cm As New SqlCommand()
                Using da As New SqlDataAdapter
                    Try
                        cn.Open()
                        cm.Connection = cn
                        cm.CommandTimeout = cn.ConnectionTimeout
                        da.SelectCommand = cm
                        cm.CommandText = sql
                        da.Fill(ds)
                        e.Hasil = True
                        e.Message = "Sukses"
                        e.Value = JsonConvert.SerializeObject(ds, Formatting.Indented)
                    Catch ex As Exception
                        e.Hasil = False
                        e.Message = ex.Message
                        e.Value = ""
                    End Try
                End Using
            End Using
        End Using
        serializer.MaxJsonLength = Int32.MaxValue '; //increase MaxJsonLength.  This could be read in from the web.config if you prefer
        Return serializer.Serialize(e)
    End Function

    Public Shared Function DeleteDataMaster(ByVal Table As String, ByVal FilterPkColumn_PkValue As String, Optional ByVal CheckIsActive As Boolean = True, Optional ByVal strKoneksi As String = "") As Pesan
        Dim e As New Pesan With {.Hasil = False, .Message = "", .Value = Nothing}
        If strKoneksi = "" Then
            strKoneksi = conStr
        End If
        Dim sql As String = ""
        sql = "Select IsActive From " & Table & " Where " & FilterPkColumn_PkValue
        If CheckIsActive AndAlso Utils.ObjToBool(Query.ExecuteScalar(sql)) Then
            If XtraMessageBox.Show("Nonaktifkan data ?", NamaAplikasi, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                sql = "Update " & Table & " Set IsActive=0 Where " & FilterPkColumn_PkValue
                Query.Execute(sql)
                With e
                    e.Message = "Data telah di non aktifkan."
                    e.Value = ""
                    e.Hasil = True
                End With
            Else
                With e
                    e.Message = "Non aktifkan data dibatalkan."
                    e.Value = ""
                    e.Hasil = False
                End With
            End If
        Else
            If XtraMessageBox.Show("Hapus data dari database (permanen) ?", NamaAplikasi, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                sql = "Delete " & Table & " Where " & FilterPkColumn_PkValue
                e = Query.Execute(sql)
                If e.Hasil Then
                    e.Message = "Data telah di hapus permanen."
                Else
                    If e.Message.Contains("REFERENCE constraint") Then
                        e.Message = "Data ini telah terpakai/menjadi referensi untuk data lainya." & vbCrLf & e.Message
                    Else
                        'Default
                    End If
                End If
            Else
                With e
                    e.Message = "Delete permanen data dibatalkan."
                    e.Value = ""
                    e.Hasil = False
                End With
            End If
        End If
        Return e
    End Function
    Public Shared Sub GetApplicationSetting()
        Dim ds As New DataSet
        ds = ExecuteDataSet("Select * From MSetting")
        If Not ds Is Nothing Then
            With ds.Tables(0).Rows(0)
                NamaPerusahaan = .Item("NamaPerusahaan").ToString
                AlamatPerusahaan = .Item("AlamatPerusahaan").ToString
                KotaPerusahaan = .Item("KotaPerusahaan").ToString
                FolderLayouts = .Item("PathLayout").ToString
            End With
            ds.Dispose()
        End If
    End Sub
End Class
