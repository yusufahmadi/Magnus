Imports System.Globalization
Imports System.Threading
Imports DevExpress.LookAndFeel

Public Class LoginForm

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See https://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If txtUsername.Text.Trim = "" Then
            txtUsername.Focus()
            Exit Sub
        End If
        If txtPassword.Text.Trim = "" Then
            txtPassword.Focus()
            Exit Sub
        End If
        Dim psn As New Pesan With {.Hasil = False, .Message = "", .Value = Nothing}
        If BLL.Login.goLogin(txtUsername.Text.Trim.ToUpper, txtPassword.Text.Trim, psn) = True Then
            Me.Hide()
            DevExpress.XtraEditors.XtraMessageBox.Show(Me, psn.Message & vbCrLf & psn.Value.ToString, NamaAplikasi)
            Dim f As New FormMain
            f.Show()
        Else
            DevExpress.XtraEditors.XtraMessageBox.Show(Me, psn.Message, NamaAplikasi)
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Using f As New FormSettingDatabase
            If DialogResult.OK = f.ShowDialog() Then

            End If
        End Using
    End Sub

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Change current culture
        Dim culture As CultureInfo
        culture = CultureInfo.CreateSpecificCulture("en-us")
        Thread.CurrentThread.CurrentCulture = culture
        Thread.CurrentThread.CurrentUICulture = culture
        'Dim info As CultureInfo = TryCast(Thread.CurrentThread.CurrentCulture.Clone(), CultureInfo)
        'info.NumberFormat.NumberGroupSeparator = ","
        Application.CurrentCulture = culture

        UserLookAndFeel.Default.SkinName = My.Settings.ApplicationSkinName
    End Sub
End Class
