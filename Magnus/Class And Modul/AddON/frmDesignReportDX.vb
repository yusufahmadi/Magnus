Imports System.ComponentModel.Design
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.UserDesigner

Namespace Reporting
    Public Class frmDesignReportDX
        Private Report As DevExpress.XtraReports.UI.XtraReport
        Private MyDataset As DataSet

        Public Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
        End Sub

        Public ReadOnly Property ActiveXRDesignPanel() As XRDesignPanel
            Get
                Return ReportDesigner1.ActiveDesignPanel
            End Get
        End Property

        Public Sub OpenReport(ByVal newReport As DevExpress.XtraReports.UI.XtraReport, ByVal newDataset As DataSet)
            Report = newReport
            MyDataset = newDataset
            ReportDesigner1.OpenReport(Report)
            AddDataSource()
        End Sub
        Public Sub CreateNewReport()
            ReportDesigner1.CreateNewReport()
        End Sub

        Private Sub AddDataSource()
            Dim host As IDesignerHost = TryCast(ActiveXRDesignPanel.GetService(GetType(IDesignerHost)), IDesignerHost)

            If host IsNot Nothing Then
                host.Container.Add(MyDataset)
                DevExpress.XtraReports.Design.DesignTool.AddToContainer(host, MyDataset)
            End If
        End Sub

        Private Sub xrDesignPanel1_DesignerHostLoaded(sender As Object, e As DesignerLoadedEventArgs)
            e.DesignerHost.Container.Add(MyDataset)
        End Sub

        Private Sub frmDesignReportDX_Load(sender As Object, e As EventArgs) Handles Me.Load

        End Sub
    End Class
End Namespace
