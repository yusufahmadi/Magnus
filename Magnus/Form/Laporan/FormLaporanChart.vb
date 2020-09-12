Public Class FormLaporanChart
    Public FormName As String = ""
    Private Sub ChartControl1_Click(sender As Object, e As EventArgs) Handles ChartControl1.Click

    End Sub

    Private Sub FormLaporanChart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = FormName
        'TODO: This line of code loads data into the 'MagnusDataSet1.vKasBankKeluar' table. You can move, or remove it, as needed.
        DateEdit1.DateTime = ObjToDate(Query.ExecuteScalar("Select GetDate()")).ToString("yyyy-MM-01") ' DateTime.Now.ToString("yyyy-MM-01")
        DateEdit2.DateTime = ObjToDate(Query.ExecuteScalar("Select GetDate()")).ToString("yyyy-MM-dd") ' DateTime.Now.ToString("yyyy-MM-dd")
        Refresher()
    End Sub

    Sub Refresher()
        Dim ds As DataSet
        Dim sql As String = String.Empty
        sql = "Select CONVERT(nvarchar(6), Tgl, 112) Periode,Akun,Sum(Nominal) Nominal From vKasBankKeluar Where Tgl between " & ObjToStrDateSql(DateEdit1.DateTime) & " and " & ObjToStrDateSql(DateEdit2.DateTime) & vbCrLf &
            " group by CONVERT(nvarchar(6), Tgl, 112) , Akun Order by CONVERT(nvarchar(6), Tgl, 112) "
        ds = Query.ExecuteDataSet(sql, "Data")
        If Not ds Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                ChartControl1.SeriesTemplate.ChangeView(DevExpress.XtraCharts.ViewType.Bar)

                ChartControl1.DataSource = ds.Tables("Data")
                ChartControl1.SeriesTemplate.ArgumentDataMember = "Periode"
                ChartControl1.SeriesDataMember = "Akun"
                ChartControl1.SeriesTemplate.ValueDataMembers.AddRange(New String() {"Nominal"})

                ChartControl3.DataSource = ds.Tables("Data")
                ChartControl3.SeriesTemplate.ArgumentDataMember = "Periode"
                ChartControl3.SeriesDataMember = "Akun"
                ChartControl3.SeriesTemplate.ValueDataMembers.AddRange(New String() {"Nominal"})
                ChartControl3.SeriesTemplate.ChangeView(DevExpress.XtraCharts.ViewType.SplineArea3D)

                'sql = "Select Akun,Sum(Nominal) Nominal From vKasBankKeluar Where Tgl between " & ObjToStrDateSql(DateEdit1.DateTime) & " and " & ObjToStrDateSql(DateEdit2.DateTime) & vbCrLf &
                '    " group by Akun "
                'ds = Query.ExecuteDataSet(sql, "Data2")
                'ChartControl2.SeriesTemplate.ChangeView(DevExpress.XtraCharts.ViewType.Doughnut)
                'ChartControl2.DataSource = ds.Tables("Data2")
                'ChartControl2.SeriesTemplate.ArgumentDataMember = "Akun"
                'ChartControl2.SeriesDataMember = "Akun"
                'ChartControl2.SeriesTemplate.ValueDataMembers.AddRange(New String() {"Nominal"})

            End If
        Else
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Refresher()
    End Sub
End Class