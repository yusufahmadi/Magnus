<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormSettingDatabase
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnBatal = New System.Windows.Forms.Button()
        Me.btnSimpan = New System.Windows.Forms.Button()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.CheckEditUseWinAuth = New DevExpress.XtraEditors.CheckEdit()
        Me.txtDatabase = New DevExpress.XtraEditors.ButtonEdit()
        Me.txtPwd = New DevExpress.XtraEditors.TextEdit()
        Me.txtServer = New DevExpress.XtraEditors.ButtonEdit()
        Me.txtUID = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.CheckEditUseWinAuth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDatabase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPwd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnBatal
        '
        Me.btnBatal.Location = New System.Drawing.Point(140, 131)
        Me.btnBatal.Name = "btnBatal"
        Me.btnBatal.Size = New System.Drawing.Size(123, 22)
        Me.btnBatal.TabIndex = 12
        Me.btnBatal.Text = "Batal"
        Me.btnBatal.UseVisualStyleBackColor = True
        '
        'btnSimpan
        '
        Me.btnSimpan.Location = New System.Drawing.Point(12, 131)
        Me.btnSimpan.Name = "btnSimpan"
        Me.btnSimpan.Size = New System.Drawing.Size(124, 22)
        Me.btnSimpan.TabIndex = 11
        Me.btnSimpan.Text = "Simpan"
        Me.btnSimpan.UseVisualStyleBackColor = True
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.btnBatal)
        Me.LayoutControl1.Controls.Add(Me.btnSimpan)
        Me.LayoutControl1.Controls.Add(Me.CheckEditUseWinAuth)
        Me.LayoutControl1.Controls.Add(Me.txtDatabase)
        Me.LayoutControl1.Controls.Add(Me.txtPwd)
        Me.LayoutControl1.Controls.Add(Me.txtServer)
        Me.LayoutControl1.Controls.Add(Me.txtUID)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(975, 67, 450, 400)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(275, 165)
        Me.LayoutControl1.TabIndex = 13
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'CheckEditUseWinAuth
        '
        Me.CheckEditUseWinAuth.Location = New System.Drawing.Point(85, 36)
        Me.CheckEditUseWinAuth.Name = "CheckEditUseWinAuth"
        Me.CheckEditUseWinAuth.Properties.Caption = "Use Windows Authentication"
        Me.CheckEditUseWinAuth.Size = New System.Drawing.Size(178, 19)
        Me.CheckEditUseWinAuth.StyleController = Me.LayoutControl1
        Me.CheckEditUseWinAuth.TabIndex = 8
        '
        'txtDatabase
        '
        Me.txtDatabase.Location = New System.Drawing.Point(85, 107)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtDatabase.Size = New System.Drawing.Size(178, 20)
        Me.txtDatabase.StyleController = Me.LayoutControl1
        Me.txtDatabase.TabIndex = 7
        '
        'txtPwd
        '
        Me.txtPwd.Location = New System.Drawing.Point(85, 83)
        Me.txtPwd.Name = "txtPwd"
        Me.txtPwd.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPwd.Size = New System.Drawing.Size(178, 20)
        Me.txtPwd.StyleController = Me.LayoutControl1
        Me.txtPwd.TabIndex = 6
        '
        'txtServer
        '
        Me.txtServer.Location = New System.Drawing.Point(85, 12)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtServer.Size = New System.Drawing.Size(178, 20)
        Me.txtServer.StyleController = Me.LayoutControl1
        Me.txtServer.TabIndex = 5
        '
        'txtUID
        '
        Me.txtUID.Location = New System.Drawing.Point(85, 59)
        Me.txtUID.Name = "txtUID"
        Me.txtUID.Size = New System.Drawing.Size(178, 20)
        Me.txtUID.StyleController = Me.LayoutControl1
        Me.txtUID.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem7})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(275, 165)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.txtUID
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 47)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(255, 24)
        Me.LayoutControlItem1.Text = "User"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(70, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.txtServer
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(255, 24)
        Me.LayoutControlItem2.Text = "Server"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(70, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtPwd
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 71)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(255, 24)
        Me.LayoutControlItem3.Text = "Password"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(70, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.txtDatabase
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 95)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(255, 24)
        Me.LayoutControlItem4.Text = "Database"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(70, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.CheckEditUseWinAuth
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(255, 23)
        Me.LayoutControlItem5.Text = "Authentication"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(70, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.btnSimpan
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 119)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(128, 26)
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.btnBatal
        Me.LayoutControlItem7.Location = New System.Drawing.Point(128, 119)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(127, 26)
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextVisible = False
        '
        'FormSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(275, 165)
        Me.ControlBox = False
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormSetting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Setting"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.CheckEditUseWinAuth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDatabase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPwd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnBatal As Button
    Friend WithEvents btnSimpan As Button
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents CheckEditUseWinAuth As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtDatabase As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents txtPwd As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtServer As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents txtUID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
End Class
