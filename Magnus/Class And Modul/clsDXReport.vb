Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports
Imports DevExpress.XtraEditors
Imports System.Data
Imports DevExpress.Utils
Imports System.IO
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.Native
Imports DevExpress.XtraReports.UserDesigner
Imports DevExpress.XtraPrinting.Native
Imports DevExpress.XtraPrinting.Preview
Imports DevExpress.XtraPrinting.Control
Imports DevExpress.XtraEditors.Repository
Imports System.Collections.Generic
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraReports.Extensions
Imports Magnus.Utils

Public Class clsDXReport
    Public Shared Function TextFileSave(ByVal strData As String,
         ByVal FullPath As String,
           Optional ByVal ErrInfo As String = "") As Boolean
        Dim Contents As String = ""
        Dim bAns As Boolean = False
        Dim objReader As StreamWriter
        Dim InfoFile As FileInfo = Nothing
        Try
            InfoFile = New FileInfo(FullPath)
            If Not InfoFile.Directory.Exists Then
                InfoFile.Directory.Create()
            End If
            objReader = New StreamWriter(FullPath)
            objReader.Write(strData)
            objReader.Close()
            bAns = True
        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try
        Return bAns
    End Function
    Public Shared Function TextFileGet(ByVal FullPath As String,
           Optional ByRef ErrInfo As String = "") As String
        Dim strContents As String = ""
        Dim objReader As StreamReader
        Try
            objReader = New StreamReader(FullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try
        Return strContents
    End Function
    Public Shared Function ViewXtraReport(ByVal frmParent As XtraForm, ByVal action As ActionReport, ByVal sReportName As String, ByVal Judul As String, ByVal RptName As String, ByVal DS As DataSet, Optional ByVal UkuranKertas As String = "", Optional ByVal CalculateFields As String = "", Optional ByVal ParameterField As String = "", Optional ByVal FilterString As String = "") As Boolean
        Dim Hasil As Boolean = False
        'Dim PakaiFormMdi As Boolean = True
        Dim XtraReport As DevExpress.XtraReports.UI.XtraReport = Nothing
        Dim dlg As WaitDialogForm = Nothing
        Dim Parameter() As String = Nothing
        Dim ValueField() As String = Nothing
        ' Create a new Security Permission which denies any File IO operations.
        Dim permission As New ScriptSecurityPermission("System.Security.Permissions.FileIOPermission")
        Try
            dlg = New WaitDialogForm("Sedang diproses...", "Mohon Tunggu Sebentar.")
            dlg.Show()
            dlg.TopMost = False

            If System.IO.File.Exists(sReportName) Then
                XtraReport = New DevExpress.XtraReports.UI.XtraReport
                XtraReport.LoadLayout(sReportName)
                If Not DS Is Nothing Then
                    If Not DirectoryExists(Application.StartupPath & "\Report\XCD") Then
                        System.IO.Directory.CreateDirectory(Application.StartupPath & "\Report\XCD")
                    End If
                    DS.WriteXmlSchema(Application.StartupPath & "\Report\XCD\" & Replace(RptName.ToUpper, ".REPX", "") & ".xsd")
                    XtraReport.DataSource = DS
                End If
                XtraReport.DataSourceSchema = Application.StartupPath & "\Report\XCD\" & Replace(RptName.ToUpper, ".REPX", "") & ".xsd"
                'XtraReport.PrinterName = ""
                If UkuranKertas <> "" Then
                    XtraReport.PaperKind = System.Drawing.Printing.PaperKind.Custom
                    XtraReport.PaperName = UkuranKertas
                End If
                For i As Integer = 0 To XtraReport.CalculatedFields.Count - 1
                    Select Case XtraReport.CalculatedFields(i).Name.ToUpper
                        Case "NamaPerusahaan".ToUpper
                            XtraReport.CalculatedFields(i).Expression = "'" & FixApostropi(NamaPerusahaan) & "'"
                        Case "AlamatPerusahaan".ToUpper
                            XtraReport.CalculatedFields(i).Expression = "'" & FixApostropi(AlamatPerusahaan) & "'"
                        Case "KotaPerusahaan".ToUpper
                            XtraReport.CalculatedFields(i).Expression = "'" & FixApostropi(KotaPerusahaan) & "'"
                        Case Else 'Selain Settingan Default
                            If CalculateFields <> "" Then
                                Parameter = CalculateFields.Split("|")
                                For x As Integer = 0 To Parameter.Length - 1
                                    ValueField = Parameter(x).Split("=")
                                    Select Case ValueField(1).ToUpper
                                        Case "Bit".ToUpper, "Boolean".ToUpper
                                            If XtraReport.CalculatedFields(i).Name.ToUpper = (ValueField(0).ToString).ToUpper Then
                                                XtraReport.CalculatedFields(i).Expression = CBool(ValueField(2)).ToString
                                            End If
                                        Case "Date".ToUpper, "Time".ToUpper, "Datetime".ToUpper
                                            If XtraReport.CalculatedFields(i).Name.ToUpper = NullToStr(ValueField(0)).ToUpper Then
                                                XtraReport.CalculatedFields(i).Expression = CDate(ValueField(2)).ToString("#yyyy-MM-dd#")
                                            End If
                                        Case "Int".ToUpper, "Integer".ToUpper, "Single".ToUpper, "Long".ToUpper
                                            If XtraReport.CalculatedFields(i).Name.ToUpper = NullToStr(ValueField(0)).ToUpper Then
                                                XtraReport.CalculatedFields(i).Expression = FixKoma(CLng(ValueField(2)))
                                            End If
                                        Case "Double".ToUpper, "Numeric".ToUpper, "Decimal".ToUpper
                                            If XtraReport.CalculatedFields(i).Name.ToUpper = NullToStr(ValueField(0)).ToUpper Then
                                                XtraReport.CalculatedFields(i).Expression = FixKoma(CDbl(ValueField(2)))
                                            End If
                                        Case "Money".ToUpper, "Currency".ToUpper
                                            If XtraReport.CalculatedFields(i).Name.ToUpper = NullToStr(ValueField(0)).ToUpper Then
                                                XtraReport.CalculatedFields(i).Expression = FixKoma(CDbl(ValueField(2)))
                                            End If
                                        Case Else
                                            If XtraReport.CalculatedFields(i).Name.ToUpper = NullToStr(ValueField(0)).ToUpper Then
                                                XtraReport.CalculatedFields(i).Expression = NullToStr(ValueField(2))
                                            End If
                                    End Select
                                Next
                            End If
                    End Select
                Next
                If ParameterField <> "" Then
                    Parameter = ParameterField.Split("|")
                    For i As Integer = 0 To Parameter.Length - 1
                        ValueField = Parameter(i).Split("=")
                        Select Case ValueField(1).ToUpper
                            Case "Bit".ToUpper, "Boolean".ToUpper
                                XtraReport.Parameters(ValueField(0)).Value = CBool(ValueField(2))
                            Case "Date".ToUpper, "Time".ToUpper, "Datetime".ToUpper
                                XtraReport.Parameters(ValueField(0)).Value = CDate(ValueField(2))
                            Case "Int".ToUpper, "Integer".ToUpper, "Single".ToUpper, "Long".ToUpper
                                XtraReport.Parameters(ValueField(0)).Value = CLng(ValueField(2))
                            Case "Double".ToUpper, "Numeric".ToUpper, "Decimal".ToUpper
                                XtraReport.Parameters(ValueField(0)).Value = CDbl(ValueField(2))
                            Case "Money".ToUpper, "Currency".ToUpper
                                XtraReport.Parameters(ValueField(0)).Value = CDbl(ValueField(2))
                            Case Else
                                XtraReport.Parameters(ValueField(0)).Value = CStr(ValueField(2))
                        End Select
                    Next
                    XtraReport.RequestParameters = False
                End If
                XtraReport.ReportUnit = ReportUnit.TenthsOfAMillimeter
                XtraReport.ScriptLanguage = ScriptLanguage.VisualBasic
                permission.Deny = True
                If FilterString <> "" Then
                    XtraReport.FilterString = FilterString
                End If

                ' Add this permission to a report's list of permissions for scripts.
                XtraReport.ScriptSecurityPermissions.Add(permission)
                'For Each i In XtraReport.ScriptSecurityPermissions
                '    XtraReport.ScriptSecurityPermissions.Item(i).Deny = True
                'Next

                'XtraReport.ScriptsSource.ToString()
                XtraReport.Name = Replace(RptName.ToUpper, ".REPX", "")
                XtraReport.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ClosePreview, DevExpress.XtraPrinting.CommandVisibility.None)
                XtraReport.DisplayName = RptName
                'XtraReport.CreateDocument(True)

                If action = ActionReport.Edit Then
                    EditReport(Function() New Reporting.StandardFormWrapper(DS), XtraReport, frmParent)
                ElseIf action = ActionReport.Preview Then
                    If frmParent Is Nothing Then
                        XtraReport.ShowPreviewDialog()
                    Else
                        'Pakai Form
                        Dim x As New frmCetakMDIDX
                        Try
                            x.Text = Judul
                            x.DocumentViewer1.DocumentSource = XtraReport
                            x.MdiParent = frmParent
                            x.StartPosition = FormStartPosition.CenterParent
                            x.WindowState = FormWindowState.Maximized
                            x.Show()
                            x.Focus()
                        Catch ex As Exception
                            XtraMessageBox.Show("Info Kesalahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Try
                    End If
                ElseIf action = ActionReport.Print Then
                    XtraReport.PrintDialog()
                End If
            Else
                If XtraMessageBox.Show("File tidak ditemukan, lakukan mode design ?", NamaAplikasi, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    XtraReport = New DevExpress.XtraReports.UI.XtraReport
                    If Not DS Is Nothing Then
                        If Not DirectoryExists(Application.StartupPath & "\Report\XCD") Then
                            System.IO.Directory.CreateDirectory(Application.StartupPath & "\Report\XCD")
                        End If
                        DS.WriteXmlSchema(Application.StartupPath & "\Report\XCD\" & Replace(RptName.ToUpper, ".REPX", "") & ".xsd")
                        XtraReport.DataSource = DS
                    End If
                    XtraReport.DataSourceSchema = Application.StartupPath & "\Report\XCD\" & Replace(RptName.ToUpper, ".REPX", "") & ".xsd"
                    XtraReport.ReportUnit = ReportUnit.TenthsOfAMillimeter
                    XtraReport.PaperKind = System.Drawing.Printing.PaperKind.Custom
                    XtraReport.PrinterName = ""
                    XtraReport.PaperName = UkuranKertas
                    XtraReport.Name = Replace(RptName.ToUpper, ".REPX", "")
                    XtraReport.DisplayName = RptName
                    EditReport(Function() New Reporting.StandardFormWrapper(DS), XtraReport, frmParent)
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Info Kesalahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            dlg.Close()
            dlg.Dispose()
            If Not DS Is Nothing AndAlso action = ActionReport.Print Then
                DS.Dispose()
            End If
            If Not XtraReport Is Nothing AndAlso action = ActionReport.Print Then
                XtraReport.Dispose()
            End If
        End Try
        Return Hasil
    End Function
    Private Shared Sub EditReport(ByVal createForm As Func(Of Reporting.IDesignForm), XtraReport As XtraReport, frmParent As Form)
        If frmParent Is Nothing AndAlso XtraReport Is Nothing Then
            XtraReport.ShowRibbonDesignerDialog()
        Else
            Dim designForm As Reporting.IDesignForm = createForm()
            designForm.OpenReport(XtraReport)

            If designForm.Form Is Nothing OrElse frmParent Is Nothing Then
                Return
            End If
            designForm.Form.WindowState = FormWindowState.Maximized
            designForm.Form.StartPosition = FormStartPosition.CenterScreen
            designForm.Form.Text = "Report Design By " & NamaAplikasi
            designForm.Form.Show()
            designForm.Form.Focus()
        End If
    End Sub
    Public Shared Sub NewPreview(ByVal NamaFormPemanggil As String, ByVal GCPrint As DevExpress.XtraGrid.GridControl, ByVal Caption As String, Optional ByVal Filter1 As String = "", Optional ByVal Filter2 As String = "")
        Try
            Dim PrintSys As PrintingSystem = Nothing
            Dim PrintableComp As PrintableComponentLink = Nothing
            Dim HeadArea As PageHeaderArea = Nothing
            Dim FootArea As PageFooterArea = Nothing
            Dim FH As DevExpress.XtraPrinting.PageHeaderFooter = Nothing

            Dim PathLayouts As String = FolderLayouts & NamaFormPemanggil & GCPrint.DefaultView.Name & "PrintPreview"
            Dim OldLayouts As String = FolderLayouts & NamaFormPemanggil & GCPrint.DefaultView.Name & "PrintPreview_OldLayouts"
            Dim L As Integer = 50
            Dim R As Integer = 50
            Dim T As Integer = 100
            Dim B As Integer = 50
            Dim H As Integer = 1100
            Dim W As Integer = 850
            Dim IsLandscape As Boolean = False

            HeadArea = New PageHeaderArea
            HeadArea.LineAlignment = BrickAlignment.Near
            HeadArea.Content.Add(NamaPerusahaan & vbCrLf & Caption & vbCrLf & Filter1) 'NamaPerusahaan
            HeadArea.Content.Add(Nothing)
            HeadArea.Content.Add(" " & vbCrLf & " " & vbCrLf & Filter2) '"." & vbCrLf & Caption
            HeadArea.Font = New Font("Arial", 11, FontStyle.Bold)

            FootArea = New PageFooterArea
            FootArea.LineAlignment = BrickAlignment.Near
            FootArea.Content.Add("Printed on : [Date Printed] [Time Printed]")
            FootArea.Content.Add("Printed by : " & Username)
            FootArea.Content.Add("[Page # of Pages #]")

            FH = New DevExpress.XtraPrinting.PageHeaderFooter(HeadArea, FootArea)

            If GCPrint.DataSource IsNot Nothing Then
                GCPrint.DefaultView.SaveLayoutToXml(OldLayouts & ".xml")
            End If
            If System.IO.File.Exists(PathLayouts & ".xml") Then
                If XtraMessageBox.Show("Ingin Load Layouts Kolom dari Cetakan Sebelumnya?", "Load Layout Kolom", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    GCPrint.DefaultView.RestoreLayoutFromXml(PathLayouts & ".xml")
                    IsLandscape = ObjToBool(Ini.BacaIniPath(PathLayouts & ".ini", "Page", "Landscape", IsLandscape.ToString))
                    H = ObjToInt(Ini.BacaIniPath(PathLayouts & ".ini", "Page", "H", H.ToString))
                    W = ObjToInt(Ini.BacaIniPath(PathLayouts & ".ini", "Page", "W", W.ToString))
                    L = ObjToInt(Ini.BacaIniPath(PathLayouts & ".ini", "Margin", "L", L.ToString))
                    R = ObjToInt(Ini.BacaIniPath(PathLayouts & ".ini", "Margin", "R", R.ToString))
                    T = ObjToInt(Ini.BacaIniPath(PathLayouts & ".ini", "Margin", "T", T.ToString))
                    B = ObjToInt(Ini.BacaIniPath(PathLayouts & ".ini", "Margin", "B", B.ToString))
                End If
            End If
            PrintSys = New PrintingSystem
            PrintableComp = New PrintableComponentLink(PrintSys)
            PrintableComp.Component = GCPrint
            PrintableComp.PageHeaderFooter = FH

            PrintableComp.Landscape = IsLandscape
            PrintableComp.CustomPaperSize = New Size(W, H)
            PrintableComp.Margins = New System.Drawing.Printing.Margins(L, R, T, B)

            PrintableComp.CreateDocument()
            PrintableComp.ShowRibbonPreviewDialog(FormMain.LookAndFeel)

            If GCPrint.DataSource IsNot Nothing Then
                GCPrint.DefaultView.SaveLayoutToXml(PathLayouts & ".xml")
                GCPrint.DefaultView.RestoreLayoutFromXml(OldLayouts & ".xml")
            End If
            If System.IO.File.Exists(OldLayouts & ".xml") Then System.IO.File.Delete(OldLayouts & ".xml")
            Ini.TulisIniPath(PathLayouts & ".ini", "Page", "Landscape", PrintableComp.Landscape.ToString)
            Ini.TulisIniPath(PathLayouts & ".ini", "Page", "H", PrintableComp.CustomPaperSize.Height.ToString)
            Ini.TulisIniPath(PathLayouts & ".ini", "Page", "W", PrintableComp.CustomPaperSize.Width.ToString)
            Ini.TulisIniPath(PathLayouts & ".ini", "Margin", "L", PrintableComp.Margins.Left.ToString)
            Ini.TulisIniPath(PathLayouts & ".ini", "Margin", "R", PrintableComp.Margins.Right.ToString)
            Ini.TulisIniPath(PathLayouts & ".ini", "Margin", "T", PrintableComp.Margins.Top.ToString)
            Ini.TulisIniPath(PathLayouts & ".ini", "Margin", "B", PrintableComp.Margins.Bottom.ToString)

        Catch ex As Exception
            XtraMessageBox.Show("Gagal Preview : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
