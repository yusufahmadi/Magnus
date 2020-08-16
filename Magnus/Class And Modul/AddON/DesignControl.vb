Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.UserDesigner

Namespace Reporting
    Friend Interface IDesignForm
        ReadOnly Property Form() As Form
        ReadOnly Property ActiveXRDesignPanel() As XRDesignPanel
        Sub OpenReport(ByVal newReport As DevExpress.XtraReports.UI.XtraReport)
    End Interface

    Friend Class StandardFormWrapper
        Implements IDesignForm

        Private wrappedForm As frmDesignReportDX
        Private myDataset As DataSet

        Public ReadOnly Property Form() As Form Implements IDesignForm.Form
            Get
                Return wrappedForm
            End Get
        End Property
        Public ReadOnly Property ActiveXRDesignPanel() As XRDesignPanel Implements IDesignForm.ActiveXRDesignPanel
            Get
                Return wrappedForm.ActiveXRDesignPanel
            End Get
        End Property
        Public Sub New(ByVal NewDataset As DataSet)
            wrappedForm = New frmDesignReportDX()
            myDataset = NewDataset
        End Sub
        Public Sub OpenReport(ByVal newReport As XtraReport) Implements IDesignForm.OpenReport
            wrappedForm.OpenReport(newReport, myDataset)
        End Sub
    End Class
End Namespace
