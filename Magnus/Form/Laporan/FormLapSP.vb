Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraLayout
Public Class FormLapSP
    Public spName As String = "spTest"
    Public FormName As String = ""
    Public IsAllowSort As Boolean = False

    Private sql As String = ""

    Private ds As New DataSet
    Dim BS As New BindingSource
    Private Sub FormLaporan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If FormName = "" Then
            FormName = "laporan" & spName
        End If
        Me.Text = FormName
        XtraTabPage1.Text = FormName
        If IsAllowSort Then
            GV1.OptionsCustomization.AllowSort = True
        End If
        GenerateForm()
        RestoreLayout()
        'FungsiControl.SetForm(Me)
    End Sub

    Public WithEvents cb As New DevExpress.XtraEditors.Controls.EditorButton
    Public WithEvents txtEdit As DevExpress.XtraEditors.TextEdit
    Public WithEvents clcEdit As DevExpress.XtraEditors.CalcEdit
    Public WithEvents ckedit As DevExpress.XtraEditors.CheckEdit
    Public WithEvents dtEdit As DevExpress.XtraEditors.DateEdit
    Public WithEvents tmEdit As DevExpress.XtraEditors.TimeEdit
    Public WithEvents cbedit As DevExpress.XtraEditors.CheckedComboBoxEdit
    Public WithEvents picEdit As DevExpress.XtraEditors.PictureEdit
    Public WithEvents lkEdit As DXSample.CustomSearchLookUpEdit


    Public WithEvents RbEdit As RadioButton
    Public WithEvents Page As New DevExpress.XtraTab.XtraTabPage
    Public WithEvents LC As LayoutControl
    Public WithEvents GC As New DevExpress.XtraGrid.GridControl
    Public WithEvents GV As New GridView

    Public Params As String = ""
    Sub GenerateForm()
        'Try
        On Error GoTo 0
        Dim con As New SqlConnection(conStr)
        Dim cmd As New SqlCommand
        Dim itemLC As LayoutControlItem
        Dim da As SqlDataAdapter

        con.Open()
        cmd.Connection = con
        sql = "declare @spname as nvarchar(250)='" & spName & "'" & vbCrLf &
               "select " & vbCrLf &
               "'Parameter_name' = name, " & vbCrLf &
               "'Type'   = type_name(user_type_id), " & vbCrLf &
               "'Length'   = max_length, " & vbCrLf &
               "'Prec'   = case when type_name(system_type_id) = 'uniqueidentifier' " & vbCrLf &
               "          then precision " & vbCrLf &
               "          else OdbcPrec(system_type_id, max_length, precision) end, " & vbCrLf &
               "'Scale'   = OdbcScale(system_type_id, scale),   " & vbCrLf &
               "'Param_order'  = parameter_id,   " & vbCrLf &
               "'Collation'   = convert(sysname, " & vbCrLf &
               "               case when system_type_id in (35, 99, 167, 175, 231, 239) " & vbCrLf &
               "               then ServerProperty('collation') end) " & vbCrLf &
               "from sys.parameters where object_id = object_id(@spname)"
        cmd.CommandText = sql
        da = New SqlDataAdapter(cmd)
        ds.Tables.Add("Edit")
        da.Fill(ds, "Master")
        Dim ObjName As String = ""
        Dim FldName As String = ""
        Dim Prec As Integer = 0 'MaxLength
        Dim Scale As Integer = 0 'Angka Belakang Koma
        If ds.Tables("Master").Rows.Count > 0 Then
            Params = "fl:"
        End If

        If ds.Tables("Edit").Rows.Count <= 0 Then
            ds.Tables("Edit").Rows.Add()
        End If
        BS.DataSource = ds.Tables("Edit")
        For i = 0 To ds.Tables("Master").Rows.Count - 1
            ObjName = NullToStr(ds.Tables("Master").Rows(i).Item("Parameter_name")).Replace("@", "")
            FldName = NullToStr(ds.Tables("Master").Rows(i).Item("Type")).ToLower
            'SP fl:{TglDari}, {TglSampai}, {IDPos},{IDKasir}
            'Query Select "fl: and TglPenerimaan >= {TglDari} | and TglPenerimaan < DATEADD(d, 1, {TglSampai}) | and IDCustomer = {IDCustomer} ;"
            Params &= IIf(i = 0, "{" & ObjName & "}", ",{" & ObjName & "}")
            addColumntoDT(ObjName, FldName)

            Prec = ObjToInt(ds.Tables("Master").Rows(i).Item("Prec"))
            Scale = ObjToInt(ds.Tables("Master").Rows(i).Item("Scale"))
            Select Case FldName
                Case "int", "bigint", "tinyint", "smallint", "real"
                    If ObjName.Length > 1 AndAlso ObjName.Substring(0, 2) = "ID" Then
                        'Lookup
                        Dim dsLookUp As New DataSet
                        cb.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete
                        lkEdit = New DXSample.CustomSearchLookUpEdit
                        lkEdit.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis))
                        lkEdit.Properties.Buttons.Add(cb)
                        lkEdit.Properties.NullText = ""
                        lkEdit.Name = "txt" + ObjName
                        lkEdit.Properties.DisplayMember = "Nama"
                        lkEdit.Properties.ValueMember = "ID"
                        lkEdit.Properties.View.Name = "GV" + ObjName
                        lkEdit.EnterMoveNextControl = True
                        lkEdit.EditValue = -1
                        lkEdit.TabStop = Not lkEdit.Properties.ReadOnly
                        itemLC = New LayoutControlItem
                        itemLC.Name = ObjName
                        itemLC.Parent = LC1.Root
                        itemLC.Control = lkEdit
                        AddHandler itemLC.Click, AddressOf itemLC_Click

                        cmd.CommandText = "Select ID,Kode,Nama,IsActive from M" & ObjName.Substring(2, ObjName.Length - 2)

                        'lkEdit.Tag = IIf(NullToStr(lkEdit.Tag).Length > 0, lkEdit.Tag & ";", "").ToString & "sqllookup:" & cmd.CommandText

                        'cmd.CommandText = isiKurawaByBS(cmd.CommandText)

                        da = New SqlDataAdapter(cmd)
                        da.Fill(dsLookUp, ObjName)

                        'Tambahh Row untuk NoID kosong
                        'If ObjToBool((ds.Tables("Master").Rows(i).Item("autoinc"))) Then
                        '    Dim TblName As String = NullToStr(ds.Tables("Master").Rows(i).Item("tablelookup"))
                        '    Dim ValName As String = NullToStr(ds.Tables("Master").Rows(i).Item("lookupvalue"))
                        '    Dim DspName As String = NullToStr(ds.Tables("Master").Rows(i).Item("lookupdisplay"))
                        '    If dsLookUp.Tables(TblName).Select(ValName & "=0").Length <= 0 Then
                        '        Dim R As DataRow = dsLookUp.Tables(TblName).NewRow
                        '        R(ValName) = 0
                        '        R(DspName) = "TANPA " & NullToStr(ds.Tables("Master").Rows(i).Item("caption")).ToUpper
                        '        dsLookUp.Tables(TblName).Rows.InsertAt(R, 0)
                        '    End If
                        'End If

                        lkEdit.Properties.DataSource = dsLookUp.Tables(ObjName)
                        AddHandler lkEdit.EditValueChanged, AddressOf lkEdit_EditValueChanged
                        AddHandler lkEdit.ButtonClick, AddressOf lkEdit_ButtonClick
                        AddHandler lkEdit.Click, AddressOf lkEdit_Click
                        AddHandler lkEdit.Popup, AddressOf lkEdit_Popup
                        AddHandler lkEdit.Properties.View.DataSourceChanged, AddressOf GV_DataSourceChanged

                        dsLookUp.Dispose()
                    Else
                        clcEdit = New DevExpress.XtraEditors.CalcEdit
                        clcEdit.Name = "txt" + ObjName
                        clcEdit.EnterMoveNextControl = True
                        clcEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
                        clcEdit.Properties.Mask.EditMask = "n0"
                        clcEdit.Properties.Mask.UseMaskAsDisplayFormat = True
                        clcEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                        clcEdit.EditValue = 0
                        AddHandler clcEdit.KeyDown, AddressOf FungsiControl.txtNumeric_KeyDown

                        clcEdit.TabStop = Not clcEdit.Properties.ReadOnly

                        clcEdit.Properties.MaxLength = Prec

                        itemLC = New LayoutControlItem
                        itemLC.Name = ObjName
                        itemLC.Parent = LC1.Root
                        AddHandler itemLC.Click, AddressOf itemLC_Click
                        itemLC.Control = clcEdit
                        AddHandler clcEdit.LostFocus, AddressOf clcEdit_LostFocus
                        AddHandler clcEdit.GotFocus, AddressOf clcEdit_GotFocus
                    End If
                Case "varchar", "nvarchar", "char"
                    If ObjName.Length > 1 AndAlso ObjName.Substring(0, 2) = "ID" Then
                        'Lookup
                        Dim dsLookUp As New DataSet
                        cb.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete
                        lkEdit = New DXSample.CustomSearchLookUpEdit
                        lkEdit.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis))
                        lkEdit.Properties.Buttons.Add(cb)
                        lkEdit.Properties.NullText = ""
                        lkEdit.Name = "txt" + ObjName
                        lkEdit.Properties.DisplayMember = "Nama"
                        lkEdit.Properties.ValueMember = "ID"
                        lkEdit.Properties.View.Name = "GV" + ObjName
                        lkEdit.EnterMoveNextControl = True
                        lkEdit.EditValue = -1
                        lkEdit.TabStop = Not lkEdit.Properties.ReadOnly
                        itemLC = New LayoutControlItem
                        itemLC.Name = ObjName
                        itemLC.Parent = LC1.Root
                        itemLC.Control = lkEdit
                        AddHandler itemLC.Click, AddressOf itemLC_Click

                        cmd.CommandText = "Select ID,Nama,IsActive from M" & ObjName.Substring(2, ObjName.Length - 2)
                        da = New SqlDataAdapter(cmd)
                        da.Fill(dsLookUp, ObjName)

                        lkEdit.Properties.DataSource = dsLookUp.Tables(ObjName)
                        AddHandler lkEdit.EditValueChanged, AddressOf lkEdit_EditValueChanged
                        AddHandler lkEdit.ButtonClick, AddressOf lkEdit_ButtonClick
                        AddHandler lkEdit.Click, AddressOf lkEdit_Click
                        AddHandler lkEdit.Popup, AddressOf lkEdit_Popup
                        AddHandler lkEdit.Properties.View.DataSourceChanged, AddressOf GV_DataSourceChanged

                        dsLookUp.Dispose()
                    Else

                        txtEdit = New DevExpress.XtraEditors.TextEdit
                        txtEdit.Properties.CharacterCasing = CharacterCasing.Normal
                        txtEdit.Name = "txt" + ObjName

                        txtEdit.EnterMoveNextControl = True
                        txtEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

                        If False Then 'Jika ada masking -> Nomasking
                            txtEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple
                            txtEdit.Properties.Mask.EditMask = "" 'ds.Tables("Master").Rows(i).Item("format")
                            'If NullToStr(ds.Tables("Master").Rows(i).Item("format")).ToLower = "alphanumeric" Then
                            '    txtEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
                            '    txtEdit.Properties.Mask.EditMask = "[a-z|A-Z|0-9]+"
                            'End If
                            txtEdit.Properties.Mask.UseMaskAsDisplayFormat = True
                            txtEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False
                            txtEdit.EditValue = ""
                        Else
                            txtEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
                            txtEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False
                            txtEdit.EditValue = ""
                        End If
                        txtEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near


                        txtEdit.TabStop = Not txtEdit.Properties.ReadOnly

                        txtEdit.Properties.MaxLength = Prec

                        itemLC = New LayoutControlItem
                        itemLC.Name = ObjName
                        itemLC.Parent = LC1.Root
                        AddHandler itemLC.Click, AddressOf itemLC_Click
                        itemLC.Control = txtEdit

                        AddHandler txtEdit.GotFocus, AddressOf txtEdit_GotFocus
                        AddHandler txtEdit.LostFocus, AddressOf txtEdit_LostFocus
                    End If
                Case "money", "numeric", "decimal"
                    clcEdit = New DevExpress.XtraEditors.CalcEdit
                    clcEdit.Name = "txt" + ObjName
                    clcEdit.EnterMoveNextControl = True

                    clcEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
                    clcEdit.Properties.Mask.EditMask = "n" & IIf(Scale > 0, "2", "0")
                    clcEdit.Properties.Mask.UseMaskAsDisplayFormat = True
                    clcEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    clcEdit.EditValue = 0
                    AddHandler clcEdit.KeyDown, AddressOf FungsiControl.txtNumeric_KeyDown

                    clcEdit.TabStop = Not clcEdit.Properties.ReadOnly


                    clcEdit.Properties.MaxLength = Prec

                    itemLC = New LayoutControlItem
                    itemLC.Name = ObjName
                    itemLC.Parent = LC1.Root
                    AddHandler itemLC.Click, AddressOf itemLC_Click
                    itemLC.Control = clcEdit
                    AddHandler clcEdit.LostFocus, AddressOf clcEdit_LostFocus
                    AddHandler clcEdit.GotFocus, AddressOf clcEdit_GotFocus
                Case "date"
                    dtEdit = New DevExpress.XtraEditors.DateEdit
                    dtEdit.Name = "txt" + ObjName
                    dtEdit.EnterMoveNextControl = True
                    dtEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
                    dtEdit.Properties.Mask.EditMask = "dd-MM-yyyy"

                    dtEdit.DateTime = Date.Today

                    dtEdit.Properties.Mask.UseMaskAsDisplayFormat = True
                    dtEdit.TabStop = Not dtEdit.Properties.ReadOnly


                    itemLC = New LayoutControlItem
                    itemLC.Name = ObjName
                    itemLC.Parent = LC1.Root
                    itemLC.Control = dtEdit

                    AddHandler itemLC.Click, AddressOf itemLC_Click
                    AddHandler dtEdit.LostFocus, AddressOf txtEdit_LostFocus
                    AddHandler dtEdit.DateTimeChanged, AddressOf txtEdit_LostFocus
                    AddHandler dtEdit.EditValueChanged, AddressOf txtEdit_LostFocus
                    AddHandler dtEdit.ButtonClick, AddressOf txtEdit_LostFocus
                    AddHandler dtEdit.Click, AddressOf txtEdit_LostFocus
                    AddHandler dtEdit.TextChanged, AddressOf txtEdit_LostFocus
                Case "datetime"

                    dtEdit = New DevExpress.XtraEditors.DateEdit
                    dtEdit.Name = "txt" + ObjName
                    dtEdit.EnterMoveNextControl = True
                    dtEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
                    dtEdit.Properties.Mask.EditMask = "dd-MM-yyyy HH:mm:ss"

                    dtEdit.DateTime = Date.Today

                    dtEdit.Properties.Mask.UseMaskAsDisplayFormat = True
                    dtEdit.TabStop = Not dtEdit.Properties.ReadOnly


                    itemLC = New LayoutControlItem
                    itemLC.Name = ObjName
                    itemLC.Parent = LC1.Root
                    itemLC.Control = dtEdit

                    AddHandler itemLC.Click, AddressOf itemLC_Click
                    AddHandler dtEdit.LostFocus, AddressOf txtEdit_LostFocus
                    AddHandler dtEdit.DateTimeChanged, AddressOf txtEdit_LostFocus
                    AddHandler dtEdit.EditValueChanged, AddressOf txtEdit_LostFocus
                    AddHandler dtEdit.ButtonClick, AddressOf txtEdit_LostFocus
                    AddHandler dtEdit.Click, AddressOf txtEdit_LostFocus
                    AddHandler dtEdit.TextChanged, AddressOf txtEdit_LostFocus

                Case "time"
                    tmEdit = New DevExpress.XtraEditors.TimeEdit
                    tmEdit.Name = "txt" + ObjName
                    tmEdit.EnterMoveNextControl = True
                    tmEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
                    tmEdit.Properties.Mask.EditMask = "HH:mm:ss"

                    tmEdit.Time = Date.Today

                    tmEdit.Properties.Mask.UseMaskAsDisplayFormat = True
                    tmEdit.TabStop = Not dtEdit.Properties.ReadOnly


                    itemLC = New LayoutControlItem
                    itemLC.Name = ObjName
                    itemLC.Parent = LC1.Root
                    itemLC.Control = tmEdit
                    AddHandler itemLC.Click, AddressOf itemLC_Click
                    AddHandler tmEdit.LostFocus, AddressOf txtEdit_LostFocus
                    'AddHandler tmEdit.DateTimeChanged, AddressOf txtEdit_LostFocus
                    AddHandler tmEdit.EditValueChanged, AddressOf txtEdit_LostFocus
                    AddHandler tmEdit.ButtonClick, AddressOf txtEdit_LostFocus
                    AddHandler tmEdit.Click, AddressOf txtEdit_LostFocus
                    AddHandler tmEdit.TextChanged, AddressOf txtEdit_LostFocus
                Case "bit"

                    ckedit = New DevExpress.XtraEditors.CheckEdit
                    ckedit.Name = "txt" + ObjName
                    ckedit.Text = ObjName
                    ckedit.Checked = False
                    ckedit.TabStop = Not ckedit.Properties.ReadOnly


                    itemLC = New LayoutControlItem
                    itemLC.Name = ObjName
                    AddHandler itemLC.Click, AddressOf itemLC_Click
                    itemLC.Parent = LC1.Root
                    itemLC.Control = ckedit

                    AddHandler ckedit.LostFocus, AddressOf txtEdit_LostFocus
            End Select
        Next

        Dim STR() As String
        For Each ctrl In LC1.Controls
            If ctrl.tag <> "" Then
                STR = Split(ctrl.tag, ";")
                For i = 0 To UBound(STR)
                    If STR(i).Substring(0, 2).Trim.ToLower = "lu" Then
                        lkEdit_EditValueChanged(ctrl, Nothing)
                    End If
                Next
            End If
        Next

        AddHandler GV1.RowStyle, AddressOf GV_RowStyle
        AddHandler GV1.DataSourceChanged, AddressOf GV_DataSourceChanged
        For Each Pg In XtraTabControl1.Controls
            If TypeOf Pg Is DevExpress.XtraTab.XtraTabPage Then
                For Each ctl In Pg.Controls
                    If TypeOf ctl Is DevExpress.XtraGrid.GridControl Then
                        GC = ctl
                        GV = GC.Views.Item(0)
                        GenerateRbEdit(GV)
                        Exit For
                    End If
                Next
            End If
        Next
        'Catch ex As Exception
        '    XtraMessageBox.Show("Kesalahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub GV_DataSourceChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        RestoreGVLayouts(sender)
    End Sub
    Private Sub GenerateRbEdit(GV As GridView)
        Dim RE As New System.Text.RegularExpressions.Regex("\{.*?\}")
        Dim tag As String = ""
        Dim Fld As String = ""
        Dim Cap As String = ""
        Dim str As String()
        Dim operasi As String()
        Dim Ekspresi As String()
        Dim Formula As String()
        Dim Splt As String() = "=,<>,<,>,<=,>=".Split(",")

        If GV.Name = GV1.Name Then
            tag = Params.Trim
        Else
            tag = GV.Tag
        End If
        Try
            str = tag.Trim.Split(";")
            For H = 0 To UBound(str)
                If str(H).Substring(0, 3) = "cl:" Then 'cl:Caption[Formula=Hasil]Warna
                    Page = GV.GridControl.Parent
                    LC = New LayoutControl
                    LC.Parent = Page
                    LC.Dock = DockStyle.Top
                    LC.SendToBack()
                    LC.Name = "LC" & Page.Name

                    RbEdit = New RadioButton
                    RbEdit.Name = "RbAll_" & Page.Name
                    RbEdit.Text = "Tampilkan Semua"
                    RbEdit.Checked = True
                    AddHandler RbEdit.CheckedChanged, AddressOf RbEdit_CheckedChanged
                    LC.Height = RbEdit.Height * 2.1

                    Dim itemLC As LayoutControlItem
                    itemLC = New LayoutControlItem
                    itemLC.Name = "TampilkanSemua" & Page.Name
                    itemLC.Parent = LC1.Root
                    itemLC.Control = RbEdit

                    operasi = str(H).Substring(3).Split("|") 'Caption=[Formula=Hasil]=Warna
                    For i = 0 To UBound(operasi)
                        Fld = ""
                        Formula = operasi(i).Split({"[", "]"}, StringSplitOptions.RemoveEmptyEntries)
                        Ekspresi = Formula(1).Split(Splt, StringSplitOptions.RemoveEmptyEntries)
                        For Each match As System.Text.RegularExpressions.Match In RE.Matches(Formula(1))
                            Fld = Replace(Replace(match.Value.ToString, "{", ""), "}", "")
                            Cap = Replace(Formula(1), match.Value.ToString, Fld)
                            'Exit For
                        Next
                        If Fld <> "" Then
                            RbEdit = New RadioButton
                            RbEdit.Tag = Cap
                            Cap = Formula(0)
                            RbEdit.Name = "Rb" & Fld & Cap.Trim(" ") & "_" & Page.Name
                            RbEdit.Text = Cap
                            AddHandler RbEdit.CheckedChanged, AddressOf RbEdit_CheckedChanged
                            If UBound(Formula) >= 2 Then
                                Dim Col As New System.Drawing.Color
                                If IsNumeric(Formula(2).Trim) Then
                                    Col = System.Drawing.Color.FromArgb(ObjToInt(Formula(2).Trim))
                                Else
                                    Col = System.Drawing.Color.FromName(Formula(2).Trim)
                                End If
                                RbEdit.ForeColor = Col
                                'RbEdit.Name = "Rb" & Fld & Formula(2).Trim(" ") & "_" & Page.Name
                            End If

                            itemLC = New LayoutControlItem
                            itemLC.Name = Fld & Cap.Trim(" ") & Page.Name
                            itemLC.Parent = LC1.Root
                            itemLC.Control = RbEdit
                            itemLC.Text = Cap
                            Continue For
                        End If
                    Next
                    Continue For
                End If
            Next
        Catch ex As Exception
            'XtraMessageBox.Show("Kesalahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Dim Unique As New List(Of String)
    Dim oda2 As SqlDataAdapter
    Dim dsT2 As New DataSet
    Dim CalcField As String = ""
    Sub Refresher()
        Dim dlg As DevExpress.Utils.WaitDialogForm = Nothing
        Dim cn As New SqlConnection(conStr)
        Dim ocmd2 As New SqlCommand
        Dim Cur As Cursor = Windows.Forms.Cursor.Current

        Dim JumlahWhere As String()
        Dim strsql As String = ""
        Dim SQL As String = ""
        Dim FilterSQL As String = " "
        Dim TabelDS As String = "Data"
        Dim TempFilt As String = ""
        Dim IsSp As Boolean = False

        Dim operasi As String()
        Dim str As String()
        Dim ekspresi As String()
        Dim RE As New System.Text.RegularExpressions.Regex("\{.*?\}")
        Dim Rplc As Object = DBNull.Value
        Dim fld As String = ""
        Dim tp As Type = GetType(String)
        Dim DBtp As DbType = DbType.String

        Dim OleDBcn As OleDbConnection = Nothing
        Dim OleDBocmd As OleDbCommand = Nothing
        Try
            Dim curentcursor As Cursor = Windows.Forms.Cursor.Current
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            dlg = New DevExpress.Utils.WaitDialogForm(String.Format("Sedang merefresh data.{0}MOHON TUNGGU ...", vbCrLf), Application.ProductName)
            dlg.TopMost = False
            dlg.Show()
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            'strsql = IIf(InStr(TableName, "select", CompareMethod.Text) > 0, TableName, "select * from " & TableName)
            ocmd2.Connection = cn
            ocmd2.CommandType = CommandType.Text
            cn.Open()

            For Each ctl In XtraTabControl1.SelectedTabPage.Controls
                If TypeOf ctl Is DevExpress.XtraGrid.GridControl Then
                    GC = ctl
                    GV = GC.Views.Item(0)
                    Exit For
                End If
            Next
            If XtraTabControl1.SelectedTabPageIndex = 0 Then
                SQL = spName 'TableName
            Else
                operasi = GC.Tag.split(";")
                For i = 0 To UBound(operasi)
                    If operasi(i).Substring(0, 4) = "sql:" Then
                        SQL = operasi(i).Substring(4).Trim
                    End If
                Next
                TabelDS &= XtraTabControl1.SelectedTabPageIndex.ToString
            End If

            If SQL.Split(" ").Length = 1 Then
                ocmd2.CommandText = "select 1 from sys.objects where (type = 'U' or Type = 'V') and name = '" & SQL & "'"
                If ObjToBool(ocmd2.ExecuteScalar) Then
                    strsql = "select * from " & SQL & " where 1 = 1 {Parameter} "
                    IsSp = False
                Else
                    ocmd2.CommandText = "select 1 from sys.objects where (type = 'P') and name = '" & SQL & "'"
                    If ObjToBool(ocmd2.ExecuteScalar) Then
                        strsql = "exec " & SQL
                        IsSp = True
                    Else
                        XtraMessageBox.Show(SQL & " Tidak Ditemukan di Store Table, View, dan Store Procedure !!", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            Else
                If InStr(SQL, "select", CompareMethod.Text) > 0 Then
                    JumlahWhere = Split(SQL.ToLower, "where")
                    If JumlahWhere.Length >= 2 Then
                        strsql = SQL
                    Else
                        strsql = SQL & " where 1 = 1 "
                    End If
                    IsSp = False
                Else
                    strsql = SQL
                    IsSp = True
                End If
            End If

            ocmd2.Parameters.Clear()
            If Params.Trim <> "" Then
                CalcField = ""
                str = Split(Params.Trim, ";")
                For H = 0 To UBound(str)
                    If str(H).Length >= 3 AndAlso str(H).Substring(0, 3) = "fl:" Then
                        ekspresi = Split(str(H).Substring(3).Trim.ToLower, "|")
                        For j = 0 To UBound(ekspresi)
                            TempFilt = ekspresi(j)
                            For Each match As System.Text.RegularExpressions.Match In RE.Matches(ekspresi(j))
                                fld = Replace(Replace(match.Value.ToString, "{", ""), "}", "")
                                If Not ds.Tables("Edit").Columns(fld) Is Nothing Then
                                    tp = ds.Tables("Edit").Columns(fld).DataType
                                    DBtp = GetDBType(tp)
                                    For Each ctrl In LC1.Controls
                                        If ctrl.name.ToString.ToLower = "txt" & fld.ToLower Then
                                            If Not CekUniqueOrNull(ctrl) Then
                                                Exit Try
                                            End If
                                            If tp = GetType(DateTime) Or tp = GetType(Date) Then
                                                CalcField &= "c" & fld & "=" & tp.Name.ToString & "=#" & Format(ctrl.datetime, "yyyy/MM/dd HH:mm").ToString & "#|"
                                            ElseIf tp = GetType(Boolean) Then
                                                CalcField &= "c" & fld & "=String='" & ctrl.checked & "'|"
                                            Else
                                                CalcField &= "c" & fld & "=String='" & ctrl.text & "'|"
                                            End If
                                            If (ctrl.text <> "") OrElse IsSp Then
                                                Rplc = ctrl.editvalue
                                                'If IsDBNull(Rplc) AndAlso IsSp Then
                                                '    If (tp = GetType(Integer) Or tp = GetType(Long)) Then
                                                '        Rplc = ObjToLong(Rplc)
                                                '        Rplc = IIf(Rplc Is Nothing AndAlso Strings.Left(fld, 2) = "id", 0, Rplc)
                                                '    ElseIf tp = GetType(Date) Or tp = GetType(DateTime) Then
                                                '        Rplc = ObjToDate(Rplc)
                                                '    ElseIf tp = GetType(Double) Then
                                                '        Rplc = ObjToDbl(Rplc)
                                                '        Rplc = IIf(Rplc Is Nothing AndAlso Strings.Left(fld, 2) = "id", 0.0, Rplc)
                                                '    ElseIf tp = GetType(String) Then
                                                '        Rplc = NullToStr(Rplc)
                                                '        Rplc = IIf(Rplc Is Nothing AndAlso Strings.Left(fld, 2) = "id", "", Rplc)
                                                '    ElseIf tp = GetType(Boolean) Then
                                                '        Rplc = ObjToBool(Rplc)
                                                '    End If
                                                'End If
                                                TempFilt = Replace(TempFilt, match.Value.ToString, "@" & fld)
                                                'If Rplc Is Nothing AndAlso Strings.Left(fld, 2) = "id" Then
                                                Select Case tp
                                                        Case GetType(Date), GetType(DateTime)
                                                            Rplc = ObjToDate(Rplc)
                                                        Case GetType(String)
                                                            Rplc = NullToStr(Rplc)
                                                        Case GetType(Integer), GetType(Long)
                                                            Rplc = ObjToLong(Rplc)
                                                        Case GetType(Double)
                                                            Rplc = ObjToDbl(Rplc)
                                                        Case GetType(Boolean)
                                                            Rplc = ObjToBool(Rplc)
                                                    End Select
                                                'End If
                                                ocmd2.Parameters.Add(New SqlParameter("@" & fld, DBtp)).Value = Rplc
                                                Exit For
                                            Else
                                                    TempFilt = ""
                                            End If
                                            Continue For
                                        End If
                                    Next
                                ElseIf match.Value.ToString <> IsiVariabelDef(match.Value.ToString) Then
                                    Rplc = IsiVariabelDef(match.Value.ToString)
                                    CalcField &= "c" & fld & "=String='" & Rplc & "'|"
                                    TempFilt = Replace(TempFilt, match.Value.ToString, "@" & fld)
                                    ocmd2.Parameters.Add(New SqlParameter("@" & fld, DBtp)).Value = Rplc
                                    Continue For
                                Else
                                    XtraMessageBox.Show("Kesalahan : Filter (" & fld & ") Belum Didefinisikan!!", "Gagal Refresh Data!!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Try
                                End If
                            Next
                            FilterSQL &= " " & TempFilt
                        Next
                    End If
                Next
            End If
            If CalcField.EndsWith("|") Then
                CalcField = CalcField.Remove(CalcField.Length - 1)
            End If
            If FilterSQL.Trim <> "" AndAlso Not IsSp AndAlso Not (FilterSQL.Trim.StartsWith("and") Or FilterSQL.Trim.StartsWith("or") Or
                FilterSQL.Trim.StartsWith("in") Or FilterSQL.Trim.StartsWith("not in")) Then
                FilterSQL = " and " & FilterSQL
            End If
            If strsql.ToLower.Contains("{parameter}") Then
                ocmd2.CommandText = strsql.Replace("{parameter}", FilterSQL)
            Else
                ocmd2.CommandText = strsql & FilterSQL
            End If
            oda2 = New SqlDataAdapter(ocmd2)
            If ds.Tables(TabelDS) Is Nothing Then
                TempFilt = ""
            Else
                TempFilt = NullToStr(ds.Tables(TabelDS).DefaultView.RowFilter)
                ds.Tables(TabelDS).Clear()

                ds.Tables(TabelDS).DefaultView.RowFilter = ""
                Application.DoEvents()
            End If
            oda2.Fill(ds, TabelDS)
            GC.DataSource = ds.Tables(TabelDS)
            dsT2 = ds
            RestoreGVLayouts(GV)
            ds.Tables(TabelDS).DefaultView.RowFilter = ""
            GC.RefreshDataSource()
            Application.DoEvents()
            ds.Tables(TabelDS).DefaultView.RowFilter = TempFilt
            GC.RefreshDataSource()
            Application.DoEvents()
            'Klo GV Kosong Kasih Pesan
            If GV.RowCount = 0 Then
                XtraMessageBox.Show("Tidak Ada Data !", NamaAplikasi)
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Kesalahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ocmd2.Dispose()
            cn.Close()
            cn.Dispose()
            Windows.Forms.Cursor.Current = Cur
            Windows.Forms.Cursor.Current = Cursors.Default
            dlg.Close()
            dlg.Dispose()
            'GV.ShowFindPanel()
        End Try
    End Sub


    Sub Cetak()
        Dim Action As ActionReport = IIf(IsEditReport, ActionReport.Edit, ActionReport.Preview)
        Dim NamaFile As String = "CetakReport" & FormName & ".repx"
        Dim PathFile As String = Application.StartupPath & "\Report\"
        Dim str, operasi As String()
        Dim RgText As String = ""
        'Dim CalcField As String = "TglDari=Datetime=#" & TglDari.DateTime.ToString("yyyy/MM/dd") & "#|TglSampai=Datetime=#" & TglSampai.DateTime.ToString("yyyy/MM/dd") & "#"

        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            NamaFile = "CetakReport" & FormName & ".repx"
        Else
            NamaFile = "CetakReport" & FormName & XtraTabControl1.SelectedTabPage.Text & ".repx"
        End If

        PathFile &= NamaFile
        If System.IO.File.Exists(PathFile) OrElse Action = ActionReport.Edit Then
            Refresher()
            str = NullToStr(CalcRb).Split("|")
            For i = 0 To UBound(str)
                operasi = str(i).Split(";")
                If operasi(0) = XtraTabControl1.SelectedTabPage.Name Then
                    RgText = operasi(1)
                    Exit For
                Else
                    RgText = "Tampilkan Semua"
                End If
            Next
            RgText = IIf(CalcField <> "", "|", "") & "Radio=String='Status: " & RgText & "'"
            clsDXReport.ViewXtraReport(Me.MdiParent, Action, PathFile, XtraTabControl1.SelectedTabPage.Text, NamaFile, dsT2, , CalcField & RgText)
        Else
            For Each ctrl In XtraTabControl1.SelectedTabPage.Controls
                If TypeOf ctrl Is DevExpress.XtraGrid.GridControl Then
                    'TryCast(ctrl, DevExpress.XtraGrid.GridControl).ShowPrintPreview()
                    'TryCast(ctrl, DevExpress.XtraGrid.GridControl).ShowRibbonPrintPreview()
                    clsDXReport.NewPreview(FormName, TryCast(ctrl, DevExpress.XtraGrid.GridControl), XtraTabControl1.SelectedTabPage.Text)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Function CekUniqueOrNull(ctrl As Object) As Boolean
        Dim Atrib As String() = {""}
        Dim Filternya As String = ""
        Dim txtValid As TextEdit = Nothing
        Try
            txtValid = TryCast(ctrl, TextEdit)
        Catch ex As Exception

        End Try
        If txtValid Is Nothing Then
            Return True
        End If
        Try
            'Cek Unique
            For y As Long = 0 To Unique.Count - 1
                Atrib = Unique.Item(y).Split("|")
                'Atrib(0) = Object Name
                'Atrib(1) = Old Value
                'Atrib(2) = Field Name
                'Atrib(3) = Filter
                'Atrib(4) = Caption
                If ctrl.name.tolower = "txt" & Atrib(0).ToLower Then
                    If txtValid.Text.Trim = "" Then
                        XtraMessageBox.Show(Atrib(4) & " Masih Kosong!", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtValid.Focus()
                        Return False
                    Else
                        Filternya = isiKurawaByBS(Atrib(3))
                        If Filternya <> "" AndAlso Not (Filternya.Trim.StartsWith("and") Or Filternya.Trim.StartsWith("or")) Then
                            Filternya = " and " & Filternya
                        End If
                        'If CekKodeValid(Trim(txtValid.EditValue), Trim(Atrib(1)), TableName, Atrib(2), Not isNew, Filternya) Then

                    End If
                    Continue For
                End If
            Next
            Atrib = {""}



            Return True
        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function
    Dim CalcRb As String
    Private Sub RbEdit_CheckedChanged(sender As Object, e As EventArgs)
        If Not sender.checked Then Exit Sub
        Dim PageName As String = sender.name.ToString.Split("_")(1)
        Dim Tag As String = ""
        Dim rgText As String = ""
        Dim Fld As String = ""
        Dim Hasil As String = ""
        Dim Str As String()
        Dim Operasi As String()
        Dim Formula As String()
        Dim Splt As String() = "=,<>,<,>,<=,>=".Split(",")
        Dim TableDS As String = "Data"
        Page = Nothing
        If XtraTabControl1.SelectedTabPageIndex <> 0 Then
            TableDS = "Data" & XtraTabControl1.SelectedTabPageIndex
        End If
        Try
            For Each Pg In XtraTabControl1.TabPages
                If Pg.name = PageName Then
                    Page = Pg
                    rgText = PageName & ";" & sender.text & "|"
                    Str = NullToStr(CalcRb).Split("|")
                    For i = 0 To UBound(Str)
                        Operasi = Str(i).Split(";")
                        If Operasi(0) = PageName Then
                            CalcRb = CalcRb.Replace(Str(i) & "|", "")
                            Exit For
                        End If
                    Next
                    CalcRb &= rgText
                    If ds.Tables(TableDS) Is Nothing Then
                        Refresher()
                    End If
                    If NullToStr(sender.tag) = "" Then
                        ds.Tables(TableDS).DefaultView.RowFilter = NullToStr(sender.Tag)
                        GC.RefreshDataSource()
                        Application.DoEvents()
                        If GV.RowCount = 0 Then
                            XtraMessageBox.Show("Tidak Ada Data !", NamaAplikasi)
                        End If
                    Else
                        For Each ctl In Page.Controls
                            If TypeOf ctl Is DevExpress.XtraGrid.GridControl Then
                                GC = ctl
                                GV = GC.Views.Item(0)
                                Formula = sender.Tag.Split(Splt, StringSplitOptions.RemoveEmptyEntries)
                                Fld = Formula(0).Trim
                                Hasil = Formula(1).Trim.Replace("'", "")
                                If ds.Tables(TableDS).Columns(Fld) IsNot Nothing Then
                                    'Tag = sender.tag
                                    Select Case GV.Columns.Item(Fld).ColumnType.Name.ToLower
                                        Case "int32", "int64", "int", "int16", "decimal", "single", "money", "double", "long"
                                            Hasil = Format(ObjToDbl(Hasil), GV.Columns.Item(Fld).DisplayFormat.FormatString).ToString
                                        Case "string"
                                            Hasil = "'" & NullToStr(Hasil).ToString & "'"
                                        Case "date", "datetime"
                                            Hasil = "'" & Format(ObjToDate(Hasil), GV.Columns.Item(Fld).DisplayFormat.FormatString).ToString & "'"
                                        Case "boolean"
                                            Hasil = ObjToBool(Hasil).ToString
                                    End Select
                                    Tag = sender.tag.ToString.Replace(Formula(1), Hasil)

                                End If
                                If ds.Tables(TableDS).DefaultView.RowFilter = NullToStr(Tag) Then
                                    ds.Tables(TableDS).DefaultView.RowFilter = ""
                                    GC.RefreshDataSource()
                                    Application.DoEvents()
                                Else
                                    ds.Tables(TableDS).DefaultView.RowFilter = NullToStr(Tag)
                                    GC.RefreshDataSource()
                                End If

                                If GV.RowCount = 0 Then
                                    XtraMessageBox.Show("Tidak Ada Data !", NamaAplikasi)
                                End If
                                Continue For
                            End If
                        Next
                        Continue For
                    End If
                End If
            Next
            'End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GV_RowStyle(sender As Object, e As RowStyleEventArgs)
        Dim RE As New System.Text.RegularExpressions.Regex("\{.*?\}")
        Dim tag As String = ""
        Dim Fld As String = ""
        Dim Cap As String = ""
        Dim str As String()
        Dim operasi As String()
        Dim Ekspresi As String()
        Dim Formula As String()
        Dim Splt As String() = "=,<>,<,>,<=,>=".Split(",")
        Dim Tanya As Object = DBNull.Value
        Dim Hasil As Object = DBNull.Value
        Dim Benar As Object = DBNull.Value
        If sender.name = GV1.Name Then
            tag = Params.Trim
        Else
            tag = sender.tag
        End If
        If e.RowHandle >= 0 Then
            'Default Value by Type Data
            Try
                With sender
                    For x As Integer = 0 To .Columns.Count - 1
                        If IsDBNull(.GetRowCellValue(e.RowHandle, .Columns(x))) Then
                            Select Case .Columns(x).ColumnType.Name.ToLower
                                Case "int32", "int64", "int", "int16", "decimal", "single", "money", "double", "long"
                                    .SetRowCellValue(e.RowHandle, .Columns(x), 0)
                                Case "string"
                                    .SetRowCellValue(e.RowHandle, .Columns(x), "")
                                Case "date", "datetime"
                                    .SetRowCellValue(e.RowHandle, .Columns(x), ObjToDate(0))
                                Case "boolean"
                                    .SetRowCellValue(e.RowHandle, .Columns(x), False)
                            End Select
                            Continue For
                        End If
                    Next
                End With
            Catch ex As Exception

            End Try

            'Pewarna
            Try
                str = tag.Trim.Split(";")
                For H = 0 To UBound(str)
                    If str(H).Trim.Substring(0, 3) = "cl:" Then 'cl:Caption=[Formula=Hasil]=Warna
                        operasi = str(H).Trim.Substring(3).Split("|")
                        For i = 0 To UBound(operasi)
                            Formula = operasi(i).Trim.Split({"[", "]"}, StringSplitOptions.RemoveEmptyEntries)
                            Ekspresi = Formula(1).Trim.Split(Splt, StringSplitOptions.RemoveEmptyEntries)
                            For Each match As System.Text.RegularExpressions.Match In RE.Matches(Formula(1).Trim)
                                Fld = Replace(Replace(match.Value.ToString, "{", ""), "}", "")
                                'Cap = Replace(Replace(Formula(1).Trim, match.Value.ToString, ""), Ekspresi(1).Trim, "")
                                Cap = Formula(1).GetCharsBetween(Ekspresi(0), Ekspresi(1)).Trim
                                'Hasil = (sender.GetDataRow(e.RowHandle).Item(Fld))

                                Tanya = (sender.GetRowCellDisplayText(e.RowHandle, Fld))
                                Hasil = Ekspresi(1).Trim.Replace("'", "")
                                Select Case sender.Columns.Item(Fld).ColumnType.Name.ToLower
                                    Case "int32", "int64", "int", "int16", "decimal", "single", "money", "double", "long"
                                        Hasil = Format(ObjToDbl(Hasil), sender.Columns.Item(Fld).DisplayFormat.FormatString).ToString
                                    Case "string"
                                        Hasil = NullToStr(Hasil).ToString
                                    Case "date", "datetime"
                                        Hasil = Format(ObjToDate(Hasil), sender.Columns.Item(Fld).DisplayFormat.FormatString).ToString
                                    Case "boolean"
                                        Hasil = ObjToBool(Hasil).ToString
                                End Select
                                Select Case Cap.Trim
                                    Case = "="
                                        Benar = (Tanya = Hasil)
                                    Case "<>"
                                        Benar = (Tanya <> Hasil)
                                    Case "<"
                                        Benar = (Tanya < Hasil)
                                    Case ">"
                                        Benar = (Tanya > Hasil)
                                    Case "<="
                                        Benar = (Tanya <= Hasil)
                                    Case ">="
                                        Benar = (Tanya >= Hasil)
                                End Select
                                'Exit For
                            Next
                            If ObjToBool(Benar) Then
                                If UBound(Formula) >= 2 Then
                                    Dim Col As New System.Drawing.Color
                                    If IsNumeric(Formula(2).Trim) Then
                                        Col = System.Drawing.Color.FromArgb(ObjToInt(Formula(2).Trim))
                                    Else
                                        Col = System.Drawing.Color.FromName(Formula(2).Trim)
                                    End If
                                    e.Appearance.ForeColor = Col
                                End If
                                'Exit For
                            End If
                        Next
                        Continue For
                    End If
                Next
            Catch ex As Exception
                'XtraMessageBox.Show("Kesalahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If
    End Sub

    Private Sub itemLC_Click(sender As Object, e As EventArgs)
        Dim LC As LayoutControlItem = sender
        'LC.Control.Enabled = Not LC.Control.Enabled
        If Not TryCast(LC.Control, Object).properties.readonly Then
            TryCast(LC.Control, Object).Editvalue = Nothing
        End If
    End Sub
    Sub addColumntoDT(NamaKolom As String, Tipedata As String)
        Try
            Dim Tipe As Type = GetType(String)
            If Tipedata = "varchar" Or Tipedata.Contains("varchar") Then
                Tipe = GetType(String)
            ElseIf Tipedata = "tinyint" OrElse Tipedata = "smallint" OrElse Tipedata = "int" Then
                Tipe = GetType(Integer)
            ElseIf Tipedata = "bigint" Then
                Tipe = GetType(Long)
            ElseIf Tipedata = "bit" Then
                Tipe = GetType(Boolean)
            ElseIf Tipedata = "datetime" Then
                Tipe = GetType(DateTime)
            ElseIf Tipedata = "date" Then
                Tipe = GetType(Date)
            ElseIf Tipedata = "time" Then
                Tipe = GetType(String)
            ElseIf Tipedata = "money" Or Tipedata = "float" Or Tipedata = "numeric" Then
                Tipe = GetType(Double)
            ElseIf Tipedata = "real" Then
                Tipe = GetType(Long)
            ElseIf Tipedata = "image" Then
                Tipe = GetType(Image)
            End If

            ds.Tables("Edit").Columns.Add(NamaKolom, Tipe)
            BS.DataSource = ds.Tables("Edit")

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtEdit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txt = TryCast(sender, TextEdit)
        If txt.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
            txt.SelectAll()
        End If
    End Sub

    Private Sub clcEdit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txt = TryCast(sender, CalcEdit)
        If txt.Properties.Mask.MaskType = Mask.MaskType.Numeric Then
            txt.SelectAll()
        End If
    End Sub
    Dim isNew As Boolean = False
    Dim KodeLama As String = "'"
    Private Sub txtEdit_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim str As String()
        Dim ekspresi As String()
        Dim operasi As String()
        EditValueToDS(sender)
        If sender.name.ToString.ToLower = "txtkode".ToLower AndAlso Not isNew Then
            KodeLama = NullToStr(sender.editvalue)
        End If
        If sender.tag <> "" Then
            str = Split(sender.tag, ";")
            For i = 0 To UBound(str)
                If str(i).Substring(0, 3) = "fn:" Then 'fn:[Jumlah]=[Qty]*[Harga]*(1-[Disk1]/100) | [Disk1Rp]=[Harga]*[Disk1]/100
                    ekspresi = Split(str(i).Substring(3).Trim.ToLower, "|")
                    For j = 0 To UBound(ekspresi)
                        operasi = (Split(ekspresi(j), "="))
                        For Each ctrl In LC1.Controls
                            'MsgBox(ctrl.name.ToString)
                            If ctrl.name.ToString.ToLower = "txt" + operasi(0).Trim.ToLower Then
                                ctrl.editvalue = getValueFromFunction(operasi(1).Trim.ToLower)
                                EditValueToDS(ctrl)
                                Exit For
                            End If
                        Next
                    Next
                ElseIf str(i).Substring(0, 3) = "lu:" Then 'lu:[Alamat]=[mkota].[alamat] | [Telp]=[mkota].[Telp]
                    ekspresi = Split(str(i).Substring(3).Trim.ToLower, "|")
                    For j = 0 To UBound(ekspresi)
                        operasi = (Split(ekspresi(j), "="))
                        For Each ctrl In LC1.Controls
                            'MsgBox(ctrl.name.ToString)
                            If ctrl.name.ToString.ToLower = "txt" + operasi(0).Trim.ToLower Then
                                ctrl.editvalue = getValueFromLookup(sender, operasi(1).Trim.ToLower)
                                EditValueToDS(ctrl)
                                Exit For
                            End If

                        Next
                    Next
                ElseIf str(i).Substring(0, 3) = "dt:" Then
                    ekspresi = Split(str(i).Substring(3).Trim.ToLower, "|")
                    For j = 0 To UBound(ekspresi)
                        operasi = (Split(ekspresi(j), "="))
                        For Each ctrl In LC1.Controls
                            'MsgBox(ctrl.name.ToString)
                            If ctrl.name.ToString.ToLower = "txt" + operasi(0).Trim.ToLower Then
                                ctrl.editvalue = getDateFromFunction(operasi(1).Trim.ToLower)
                                EditValueToDS(ctrl)
                                Exit For
                            End If
                        Next
                    Next
                ElseIf str(i).Substring(0, 3).ToLower = "Rf:".ToLower Then
                    Dim SQLQry As String() = {""}
                    ekspresi = Split(str(i).Substring(3).Trim.ToLower, "|")
                    For j = 0 To UBound(ekspresi)
                        operasi = (Split(ekspresi(j), "="))
                        For Each ctrl In LC1.Controls
                            'MsgBox(ctrl.name.ToString)
                            If ctrl.name.ToString.ToLower = "txt" + operasi(0).Trim.ToLower Then
                                Dim L As Integer
                                L = InStr(ctrl.tag.ToString, "sqllookup:")
                                If L >= 1 Then
                                    'SQLQry = Split(ctrl.tag.ToString.Substring((j - 1) + 11 - 1), ";")
                                    SQLQry = Split(ctrl.tag.ToString, ";")
                                Else
                                    XtraMessageBox.Show("Table lookup belum didefinisikan", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                End If
                                For s = 0 To UBound(SQLQry)
                                    If SQLQry(s).Trim.StartsWith("sqllookup:") Then
                                        RefreshCtrlOnChange(ctrl, SQLQry(s).Trim.Substring(10))
                                    End If
                                Next
                                Exit For
                            End If
                        Next
                    Next
                End If
            Next
        End If
    End Sub

    Private Sub RefreshCtrlOnChange(ByVal ctrl As Object, SQLQry As String)
        Dim dsCtrl As DataSet = Nothing
        Try
            dsCtrl = New DataSet
            dsCtrl = Query.ExecuteDataSet(isiKurawaByBS(SQLQry), ctrl.Name)
            If ctrl.GetType.ToString() = "DXSample.CustomSearchLookUpEdit" Then
                TryCast(ctrl, DXSample.CustomSearchLookUpEdit).Properties.DataSource = dsCtrl.Tables(ctrl.Name) 'DXSample.Custom
            ElseIf ctrl.GetType.ToString() = "DevExpress.XtraEditors.TextEdit" Then
                'If Not isNew Then TryCast(ctrl, DevExpress.XtraEditors.TextEdit).EditValue = getKode(StrKode)
            ElseIf ctrl.GetType.ToString() = "DevExpress.XtraEditors.CheckedComboBoxEdit" Then
                TryCast(ctrl, DevExpress.XtraEditors.CheckedComboBoxEdit).Properties.DataSource = dsCtrl.Tables(ctrl.Name)
            End If
        Catch ex As Exception

        Finally
            If dsCtrl IsNot Nothing Then dsCtrl.Dispose()
        End Try

    End Sub

    Private Sub EditValueToDS(sender As Object)
        Try
            If ds.Tables("Edit") IsNot Nothing AndAlso ds.Tables("Edit").Rows.Count > 0 AndAlso sender.DataBindings.Count > 0 Then
                Dim Fld As String = sender.DataBindings.Item("Editvalue").BindingMemberInfo.BindingField
                If Fld <> "" Then
                    If ds.Tables("Edit").Rows.Count = 0 Then
                        ds.Tables("Edit").Rows.Add()
                    End If
                    If TryCast(sender.editvalue, Object) IsNot Nothing Then
                        ds.Tables("Edit").Rows(0)(Fld) = TryCast(sender.editvalue, Object)
                    Else
                        ds.Tables("Edit").Rows(0)(Fld) = DBNull.Value
                    End If
                End If
            End If
        Catch ex As Exception
            'XtraMessageBox.Show("Kesalahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Function getValueFromFunction(ByVal ekspresi As String) As Double
        '[qty]*[harga]*(1-[disk1]/100)
        Dim lastexpresi As String = ekspresi
        Dim nmobject As String
        While lastexpresi.Contains("[")
            Dim a As Integer = Strings.InStr(lastexpresi, "[")
            Dim b As Integer = Strings.InStr(lastexpresi, "]")
            nmobject = lastexpresi.Substring(a, b - a - 1)
            For Each ctrl In LC1.Controls
                If ctrl.name.ToString.ToLower = "txt" + nmobject.ToLower Then
                    If ctrl.GetType.ToString = "DevExpress.XtraEditors.DateEdit" Then
                        If ObjToDate(ctrl.editvalue) = ObjToDate(0) Then
                            lastexpresi = Replace(lastexpresi, "[" + nmobject + "]", 0)
                        Else
                            Dim dt As DateTime = TryCast(ctrl, DateEdit).DateTime
                            lastexpresi = Replace(lastexpresi, "[" + nmobject + "]", dt.Year * IIf(dt.Year Mod 4 = 0, 366, 365) + dt.DayOfYear)
                        End If
                    Else
                        lastexpresi = Replace(lastexpresi, "[" + nmobject + "]", ObjToDbl(ctrl.editvalue).ToString)
                    End If
                    'lastexpresi = Replace(lastexpresi, "[" + nmobject + "]", ObjToDbl(ctrl.editvalue).ToString)
                    Exit For
                End If
            Next
        End While
        Return Evaluate(lastexpresi)
    End Function
    Private Sub clcEdit_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim str As String()
        Dim ekspresi As String()
        Dim operasi As String()
        EditValueToDS(sender)
        If sender.tag <> "" Then
            str = Split(sender.tag, ";")
            For i = 0 To UBound(str)
                If str(i).Substring(0, 3) = "fn:" Then 'fn:[Jumlah]=[Qty]*[Harga]*(1-[Disk1]/100) | [Disk1Rp]=[Harga]*[Disk1]/100
                    ekspresi = Split(str(i).Substring(3).Trim.ToLower, "|")
                    For j = 0 To UBound(ekspresi)
                        operasi = (Split(ekspresi(j), "="))
                        For Each ctrl In LC1.Controls
                            'MsgBox(ctrl.name.ToString)
                            If ctrl.name.ToString.ToLower = "txt" + operasi(0).Trim.ToLower Then
                                ctrl.editvalue = getValueFromFunction(operasi(1).Trim.ToLower)
                                EditValueToDS(ctrl)
                                Exit For
                            End If

                        Next
                    Next
                ElseIf str(i).Substring(0, 3) = "lu:" Then 'lu:[Alamat]=[mkota].[alamat] | [Telp]=[mkota].[Telp]
                    ekspresi = Split(str(i).Substring(3).Trim.ToLower, "|")
                    For j = 0 To UBound(ekspresi)
                        operasi = (Split(ekspresi(j), "="))
                        For Each ctrl In LC1.Controls
                            'MsgBox(ctrl.name.ToString)
                            If ctrl.name.ToString.ToLower = "txt" + operasi(0).Trim.ToLower Then
                                ctrl.editvalue = getValueFromLookup(sender, operasi(1).Trim.ToLower)
                                EditValueToDS(ctrl)
                                Exit For
                            End If

                        Next
                    Next
                End If
            Next
        End If
    End Sub


    Private Sub lkEdit_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'If FormName.ToLower <> "" AndAlso isProsesLoad Then Exit Sub
        'If FormName.ToLower <> "" AndAlso Then Exit Sub
        Dim str As String()
        Dim ekspresi As String()
        Dim operasi As String()
        EditValueToDS(sender)
        If sender.tag <> "" Then
            str = Split(sender.tag, ";")
            For i = 0 To UBound(str)
                If str(i).Substring(0, 3) = "fn:" Then 'fn:[Jumlah]=[Qty]*[Harga]*(1-[Disk1]/100) | [Disk1Rp]=[Harga]*[Disk1]/100
                    ekspresi = Split(str(i).Substring(3).Trim.ToLower, "|")
                    For j = 0 To UBound(ekspresi)
                        operasi = (Split(ekspresi(j), "="))
                        For Each ctrl In LC1.Controls
                            'MsgBox(ctrl.name.ToString)
                            If ctrl.name.ToString.ToLower = "txt" + operasi(0).Trim.ToLower Then
                                If isProsesLoad And ctrl.DataBindings.count > 0 Then
                                    Exit For
                                End If
                                ctrl.editvalue = getValueFromFunction(operasi(1).Trim.ToLower)
                                EditValueToDS(ctrl)
                                Exit For
                            End If
                        Next
                    Next
                ElseIf str(i).Substring(0, 3) = "lu:" Then 'lu:[Alamat]=[mkota].[alamat] | [Telp]=[mkota].[Telp]
                    ekspresi = Split(str(i).Substring(3).Trim, "|")
                    For j = 0 To UBound(ekspresi)
                        operasi = (Split(ekspresi(j), "="))
                        For Each ctrl In LC1.Controls
                            'MsgBox(ctrl.name.ToString)
                            'MsgBox(ctrl.name.ToString)
                            If ctrl.name.ToString.ToLower = "txt" + operasi(0).Trim.ToLower Then
                                If isProsesLoad AndAlso ctrl.DataBindings.count > 0 Then
                                    Exit For
                                End If
                                'If TryCast(sender, DXSample.CustomSearchLookUpEdit).Text = "" Then
                                If sender.Text = "" Then
                                    If TryCast(ctrl, TextEdit).Properties.Mask.MaskType = Mask.MaskType.Numeric Then
                                        TryCast(ctrl, TextEdit).EditValue = -1
                                    ElseIf TryCast(ctrl, TextEdit).Properties.Mask.MaskType = Mask.MaskType.DateTime Then
                                        TryCast(ctrl, DateEdit).DateTime = Date.Today
                                    Else
                                        TryCast(ctrl, TextEdit).EditValue = ""
                                    End If
                                Else
                                    If ctrl.GetType.ToString.ToLower = "DevExpress.XtraEditors.CheckEdit".ToLower Then
                                        ctrl.checked = ObjToBool(getValueFromLookup(sender, operasi(1).Trim))
                                    Else
                                        ctrl.editvalue = getValueFromLookup(sender, operasi(1).Trim)
                                    End If
                                    'ctrl.editvalue = getValueFromLookup(sender, operasi(1).Trim)
                                End If
                                EditValueToDS(ctrl)
                                Exit For
                            End If

                        Next
                    Next
                ElseIf str(i).Substring(0, 3) = "dt:" Then
                    ekspresi = Split(str(i).Substring(3).Trim.ToLower, "|")
                    For j = 0 To UBound(ekspresi)
                        operasi = (Split(ekspresi(j), "="))
                        For Each ctrl In LC1.Controls
                            'MsgBox(ctrl.name.ToString)
                            If ctrl.name.ToString.ToLower = "txt" + operasi(0).Trim.ToLower Then
                                If isProsesLoad And ctrl.DataBindings.count > 0 Then
                                    Exit For
                                End If
                                ctrl.editvalue = getDateFromFunction(operasi(1).Trim.ToLower)
                                EditValueToDS(ctrl)
                                Exit For
                            End If
                        Next
                    Next
                ElseIf str(i).Substring(0, 3).ToLower = "Rf:".ToLower Then
                    Dim SQLQry As String() = {""}
                    ekspresi = Split(str(i).Substring(3).Trim.ToLower, "|")
                    For j = 0 To UBound(ekspresi)
                        operasi = (Split(ekspresi(j), "="))
                        For Each ctrl In LC1.Controls
                            'MsgBox(ctrl.name.ToString)
                            If ctrl.name.ToString.ToLower = "txt" + operasi(0).Trim.ToLower Then
                                'ctrl.editvalue = getValueFromLookup(sender, operasi(1).Trim.ToLower)
                                Dim L As Integer
                                L = InStr(ctrl.tag.ToString, "sqllookup:")
                                If L >= 1 Then
                                    'SQLQry = Split(ctrl.tag.ToString.Substring((j - 1) + 11 - 1), ";")
                                    SQLQry = Split(ctrl.tag.ToString, ";")
                                Else
                                    XtraMessageBox.Show("Table lookup belum didefinisikan", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                End If
                                For s = 0 To UBound(SQLQry)
                                    If SQLQry(s).Trim.StartsWith("sqllookup:") Then
                                        RefreshCtrlOnChange(ctrl, SQLQry(s).Trim.Substring(10))
                                    End If
                                Next
                                Exit For
                            End If
                        Next
                    Next
                End If
            Next
        End If
        If FormName = "EntriGudang" AndAlso TryCast(sender, DXSample.CustomSearchLookUpEdit).Name.ToLower = "txtidwilayah".ToLower Then 'DXSample.Custom
            Dim SQL As String
            Dim dswil As New DataSet
            Try
                SQL = "SELECT * FROM MWilayah WHERE NoID=" & ObjToLong(TryCast(sender, DXSample.CustomSearchLookUpEdit).EditValue) 'DXSample.Custom
                dswil = Query.ExecuteDataSet(SQL, "MWilayah")
                If dswil.Tables("MWilayah").Rows.Count >= 1 Then
                    For Each ctl In LC1.Controls
                        If ctl.Name.ToLower = "txtidkota".ToLower Then
                            TryCast(ctl, DXSample.CustomSearchLookUpEdit).EditValue = ObjToLong(dswil.Tables(0).Rows(0).Item("IDKota")) 'DXSample.Custom
                        ElseIf ctl.Name.ToLower = "txtalamat".ToLower Then
                            TryCast(ctl, TextEdit).Text = NullToStr(dswil.Tables(0).Rows(0).Item("Alamat"))
                        ElseIf ctl.Name.ToLower = "txttelpon".ToLower Then
                            TryCast(ctl, TextEdit).Text = NullToStr(dswil.Tables(0).Rows(0).Item("Telp"))
                        End If
                    Next
                End If
            Catch ex As Exception

            Finally
                dswil.Dispose()
            End Try

        End If
    End Sub

    Function getDateFromFunction(ByVal ekspresi As String) As DateTime
        '[qty]*[harga]*(1-[disk1]/100)
        Dim lastexpresi As String = ekspresi
        Dim nmobject As String
        While lastexpresi.Contains("[")
            Dim a As Integer = Strings.InStr(lastexpresi, "[")
            Dim b As Integer = Strings.InStr(lastexpresi, "]")
            nmobject = lastexpresi.Substring(a, b - a - 1)
            For Each ctrl In LC1.Controls
                If ctrl.name.ToString.ToLower = "txt" + nmobject.ToLower Then
                    lastexpresi = Replace(lastexpresi, "[" + nmobject + "]", ObjToDate(ctrl.editvalue).ToString)
                    Exit For
                End If
            Next
        End While
        Return lastexpresi
    End Function

    Private Sub lkEdit_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Dim lked As DXSample.CustomSearchLookUpEdit = Nothing 'DXSample.Custom
        lked = TryCast(sender, DXSample.CustomSearchLookUpEdit) 'DXSample.Custom
        If lked.Properties.ReadOnly = True Then Exit Sub
        ' lkEdit = sender
        Dim df() As String, dfval As String = ""
        Try
            Select Case e.Button.Kind
                Case DevExpress.XtraEditors.Controls.ButtonPredefines.Delete
                    If sender.Tag.Contains("df:") Then
                        df = Split(sender.Tag, ";")
                        For i = 0 To UBound(df)
                            If df(i).Substring(0, 2).Trim.ToLower = "df" Then
                                dfval = Split(df(i).ToString, ":")(1)
                                Exit For
                            End If
                        Next
                    End If
                    sender.editvalue = IIf(sender.Tag.Contains("df:"), ObjToLong(dfval), -1) '-1 
                    lkEdit_EditValueChanged(sender, e)
                Case DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis
                    Dim strsql() As String = {""}
                    Dim i As Integer
                    i = InStr(sender.tag.ToString, "sqllookup:")
                    If i >= 1 Then
                        strsql = Split(sender.tag.ToString.Substring((i - 1) + 11 - 1), ";")
                    End If
                    If strsql(0) <> "" Then
                        Dim frmlookup As New frmLookup
                        frmlookup.Strsql = isiKurawaByBS(strsql(0))
                        'frmlookup.FormName = sender.name
                        frmlookup.FormName = lked.Properties.View.Name
                        frmlookup.NamaFormPemanggil = FormName
                        If frmlookup.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                            sender.editvalue = frmlookup.NoID
                            lkEdit_EditValueChanged(sender, e)
                        End If
                        RestoreGVLayouts(lked.Properties.View)
                        frmlookup.Dispose()
                    Else
                        XtraMessageBox.Show("Table lookup belum didefinisikan", NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Public NoID As Long = -1
    Public IDParent As Long = -1
    Private Function isiKurawaByBS(ByVal Qry As String, Optional ByRef dt As DataTable = Nothing, Optional ByRef row As Long = 0) As String
        Dim Patern As String = "\{.*?\}|\[.*?\]"
        Dim RE As New System.Text.RegularExpressions.Regex(Patern)
        Dim Rplc As Object = DBNull.Value
        Dim Fld As String = ""
        If dt Is Nothing Then
            dt = ds.Tables("Edit")
        End If
        For Each match As System.Text.RegularExpressions.Match In RE.Matches(Qry)
            Rplc = match.Value.ToString
            Fld = Rplc.ToString.Substring(1, Rplc.ToString.Length - 2)
            If Rplc.ToString.ToLower = "{noid}" Then
                Rplc = NoID
            ElseIf Rplc.ToString.ToLower = "{idparent}" Then
                Rplc = IDParent
            ElseIf dt.Rows.Count > 0 AndAlso dt.Columns(Fld) IsNot Nothing Then
                Rplc = dt.Rows(row).Item(Fld)
                Select Case dt.Columns.Item(Fld).DataType.Name.ToLower
                    Case "int32", "int64", "int", "int16", "decimal", "single", "money", "double", "long"
                        Rplc = ObjToDbl(Rplc)
                    Case "string"
                        Rplc = NullToStr(Rplc).ToString
                    Case "date", "datetime"
                        Rplc = ObjToDate(Rplc)
                    Case "boolean"
                        Rplc = ObjToBool(Rplc).ToString
                End Select
            ElseIf Rplc <> IsiVariabelDef(Rplc) Then
                Rplc = IsiVariabelDef(Rplc)
            End If

            Qry = Replace(Qry, match.Value.ToString, Rplc)
        Next

        Return Qry
    End Function

    Dim repckedit As New RepositoryItemCheckEdit
    Dim repdateedit As New RepositoryItemDateEdit
    Dim reptextedit As New RepositoryItemTextEdit
    Dim reppicedit As New RepositoryItemPictureEdit
    Dim isProsesLoad As Boolean = True
    Private Sub lkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'If isProsesLoad Then Exit Sub
        Dim str As String()
        Dim ekspresi As String()
        Dim operasi As String()
        EditValueToDS(sender)
        If sender.tag <> "" Then
            str = Split(sender.tag, ";")
            For i = 0 To UBound(str)
                If str(i).Substring(0, 3) = "fn:" Then 'fn:[Jumlah]=[Qty]*[Harga]*(1-[Disk1]/100) | [Disk1Rp]=[Harga]*[Disk1]/100
                    ekspresi = Split(str(i).Substring(3).Trim.ToLower, "|")
                    For j = 0 To UBound(ekspresi)
                        operasi = (Split(ekspresi(j), "="))
                        For Each ctrl In LC1.Controls
                            'MsgBox(ctrl.name.ToString)
                            If ctrl.name.ToString.ToLower = "txt" + operasi(0).Trim.ToLower Then
                                If isProsesLoad And ctrl.DataBindings.count > 0 Then
                                    Exit For
                                End If
                                ctrl.editvalue = getValueFromFunction(operasi(1).Trim.ToLower)
                                EditValueToDS(ctrl)
                                Exit For
                            End If

                        Next
                    Next
                ElseIf str(i).Substring(0, 3) = "lu:" Then 'lu:[Alamat]=[mkota].[alamat] | [Telp]=[mkota].[Telp]
                    ekspresi = Split(str(i).Substring(3).Trim, "|")
                    For j = 0 To UBound(ekspresi)
                        operasi = (Split(ekspresi(j), "="))
                        For Each ctrl In LC1.Controls
                            'MsgBox(ctrl.name.ToString)
                            'MsgBox(ctrl.name.ToString)
                            If ctrl.name.ToString.ToLower = "txt" + operasi(0).Trim.ToLower Then
                                If isProsesLoad And ctrl.DataBindings.count > 0 Then
                                    Exit For
                                End If
                                ctrl.editvalue = getValueFromLookup(sender, operasi(1).Trim)
                                EditValueToDS(ctrl)
                                Exit For
                            End If

                        Next
                    Next
                End If
            Next
        End If
    End Sub

    Private Sub lkEdit_Popup(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lu As DXSample.CustomSearchLookUpEdit = CType(sender, DXSample.CustomSearchLookUpEdit)
        Dim Pop As Popup.PopupSearchLookUpEditForm = TryCast((TryCast(lu, DevExpress.Utils.Win.IPopupControl)).PopupWindow, Popup.PopupSearchLookUpEditForm)
        Dim H As Long = Ini.BacaIniPath(LayoutsHelper.FolderLayouts & FormName & "PopupSize.ini", lu.Name, "Height", Pop.Size.Height)
        Dim W As Long = Ini.BacaIniPath(LayoutsHelper.FolderLayouts & FormName & "PopupSize.ini", lu.Name, "Width", Pop.Size.Width)
        Pop.Size = New Size(W, H)
    End Sub
    Private Sub lkEdit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim dslk As New DataSet
        Dim lkedit As DXSample.CustomSearchLookUpEdit = Nothing 'DXSample.Custom
        Try
            If FormName.ToLower = "entrirulemutasigudang" Then
                For Each ctrl In LC1.Controls
                    If ctrl.name.ToString.ToLower = "txtidgudangasal".ToLower Then
                        lkedit = CType(ctrl, DXSample.CustomSearchLookUpEdit) 'DXSample.Custom
                        Exit For
                    End If
                Next
                If Not lkedit Is Nothing Then
                    'ds = ExecuteDataset("MGudang", "SELECT MGudang.NoID, MGudang.Kode, MGudang.Nama, MWilayah.Nama AS Wilayah FROM MGudang LEFT JOIN MWilayah ON MWilayah.NoID=MGudang.IDWilayah WHERE MGudang.IDWilayah=" & ObjToLong(EksekusiSQlSkalarNew("SELECT IDWilayah FROM MGudang WHERE NoID=" & ObjToLong(sender.editvalue))))
                    'dslk = ExecuteDataset("MGudang", SQLtxtGudangTujuan & " AND MGudang.IDWilayah=" & ObjToLong(EksekusiSQlSkalarNew("SELECT IDWilayah FROM MGudang WHERE NoID=" & ObjToLong(lkedit.EditValue))))
                    'If Not dslk Is Nothing Then
                    '    For Each ctrl In LC1.Controls
                    '        If ctrl.name.ToString.ToLower = "txtidgudangtujuan" Then
                    '            ctrl.properties.datasource = dslk.Tables("MGudang")
                    '        End If
                    '    Next
                    'End If
                End If
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Informasi " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            'dslk.Dispose()
        End Try
    End Sub

    Private Sub windowsUIButtonPanelMain_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles windowsUIButtonPanelMain.ButtonClick
        Dim x As New Pesan With {.Hasil = False, .Message = "", .Value = Nothing}
        Dim _button = (e.Button.ToString().Substring(e.Button.ToString().LastIndexOf("=") + 1).Trim())
        Select Case _button
            Case "0", "'Refresh'"
                Refresher()
            Case "1", "'Cetak'"
                Cetak()
            Case "2", "'Close'"
                Me.Close()
        End Select
    End Sub


    Private Sub RestoreLayout()
        If System.IO.File.Exists(LayoutsHelper.FolderLayouts & FormName & ".xml") Then
            LC1.RestoreLayoutFromXml(LayoutsHelper.FolderLayouts & FormName & ".xml")
        End If

        For Each Pg In XtraTabControl1.Controls
            If TypeOf Pg Is DevExpress.XtraTab.XtraTabPage Then
                For Each ctl In Pg.Controls
                    If TypeOf ctl Is DevExpress.XtraGrid.GridControl Then
                        GC = ctl
                        GV = GC.Views.Item(0)
                        RestoreGVLayouts(GV)
                        'Exit For
                    ElseIf TypeOf ctl Is LayoutControl Then
                        If System.IO.File.Exists(LayoutsHelper.FolderLayouts & FormName & ctl.name & ".xml") Then
                            ctl.RestoreLayoutFromXml(LayoutsHelper.FolderLayouts & FormName & ctl.name & ".xml")
                        End If
                    End If
                Next
            End If
        Next

        'RestoreGVLayouts(GV1)
    End Sub

    Private Sub RestoreGVLayouts(GV As GridView)
        With GV
            GV.OptionsView.ColumnAutoWidth = False
            GV.OptionsView.BestFitMaxRowCount = -1
            GV.BestFitColumns()
            .OptionsDetail.SmartDetailExpandButtonMode = DetailExpandButtonMode.AlwaysEnabled
            If System.IO.File.Exists(LayoutsHelper.FolderLayouts & FormName & GV.Name & ".xml") Then
                .RestoreLayoutFromXml(LayoutsHelper.FolderLayouts & FormName & GV.Name & ".xml")
            End If
            For i As Integer = 0 To .Columns.Count - 1
                Select Case .Columns(i).ColumnType.Name.ToLower
                    Case "int32", "int64", "int"
                        .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        .Columns(i).DisplayFormat.FormatString = "n0"
                    Case "decimal", "single", "money", "double"
                        .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        .Columns(i).DisplayFormat.FormatString = "n2"
                    Case "string"
                        .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
                        .Columns(i).DisplayFormat.FormatString = ""
                    Case "date", "datetime"
                        If .Columns(i).FieldName.Trim.ToLower = "jam" Then
                            .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                            .Columns(i).DisplayFormat.FormatString = "HH:mm"
                        ElseIf .Columns(i).FieldName.Trim.ToLower = "tanggalstart" Or .Columns(i).FieldName.Trim.ToLower = "tanggalend" Then
                            .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                            .Columns(i).DisplayFormat.FormatString = "dd-MM-yyyy HH:mm"
                        Else
                            .Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                            .Columns(i).DisplayFormat.FormatString = "dd-MM-yyyy"
                        End If
                    Case "byte[]"
                        reppicedit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
                        .Columns(i).OptionsColumn.AllowGroup = False
                        .Columns(i).OptionsColumn.AllowSort = False
                        .Columns(i).OptionsFilter.AllowFilter = False
                        .Columns(i).ColumnEdit = reppicedit
                    Case "boolean"
                        repckedit.DisplayValueUnchecked = "False"
                        repckedit.DisplayValueChecked = "True"
                        .Columns(i).ColumnEdit = repckedit
                End Select
            Next
        End With
    End Sub

    Private Sub FormLap_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Dim xOtorisasi As New FormOtorisasi
        Try
            If True Then
                LC1.SaveLayoutToXml(LayoutsHelper.FolderLayouts & FormName & ".xml")
                For Each Pg In XtraTabControl1.Controls
                    If TypeOf Pg Is DevExpress.XtraTab.XtraTabPage Then
                        For Each ctl In Pg.Controls
                            If TypeOf ctl Is DevExpress.XtraGrid.GridControl Then
                                GC = ctl
                                GV = GC.Views.Item(0)
                                If GC.DataSource IsNot Nothing Then
                                    GV.SaveLayoutToXml(LayoutsHelper.FolderLayouts & FormName & GV.Name & ".xml")
                                End If
                                'Exit For
                            ElseIf TypeOf ctl Is LayoutControl Then
                                ctl.SaveLayoutToXml(LayoutsHelper.FolderLayouts & FormName & ctl.name & ".xml")
                                'Exit For
                            End If
                        Next
                    End If
                Next
                'GV1.SaveLayoutToXml(LayoutsHelper.FolderLayouts & FormName & GV1.Name & ".xml")
                Dim lu As New DXSample.CustomSearchLookUpEdit 'DXSample.Custom
                For Each ctrl In LC1.Controls
                    If TypeOf ctrl Is DXSample.CustomSearchLookUpEdit Then
                        lu = CType(ctrl, DXSample.CustomSearchLookUpEdit)
                        'lu.Properties.View.SaveLayoutToXml(LayoutsHelper.FolderLayouts & FormName & lu.Properties.View.Name & ".xml")
                        Dim Pop As Popup.PopupSearchLookUpEditForm = TryCast((TryCast(lu, DevExpress.Utils.Win.IPopupControl)).PopupWindow, Popup.PopupSearchLookUpEditForm)
                        If Pop IsNot Nothing Then
                            lu.Properties.View.SaveLayoutToXml(LayoutsHelper.FolderLayouts & FormName & lu.Properties.View.Name & ".xml")
                            Ini.TulisIniPath(LayoutsHelper.FolderLayouts & FormName & "PopupSize.ini", lu.Name, "Height", Pop.Size.Height)
                            Ini.TulisIniPath(LayoutsHelper.FolderLayouts & FormName & "PopupSize.ini", lu.Name, "Width", Pop.Size.Width)
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            XtraMessageBox.Show("Kesalahan : " & ex.Message, NamaAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            xOtorisasi.Dispose()
        End Try
    End Sub
End Class