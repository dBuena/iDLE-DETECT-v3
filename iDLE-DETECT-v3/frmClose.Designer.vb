<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmClose
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.tmrCountDown = New System.Windows.Forms.Timer(Me.components)
        Me.tmrCloseApps = New System.Windows.Forms.Timer(Me.components)
        Me.bgMain = New System.ComponentModel.BackgroundWorker()
        Me.progMain = New iDLE_DETECT_v3.ProgressBarEx()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(524, 93)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "<< PC IS IDLE >>" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "YOU ARE IDLE FOR 5 MINUTES." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "PRESS CLOSE TO CANCEL AUTO CLEAN U" & _
    "P."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(225, 112)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'tmrCountDown
        '
        '
        'tmrCloseApps
        '
        '
        'bgMain
        '
        Me.bgMain.WorkerReportsProgress = True
        Me.bgMain.WorkerSupportsCancellation = True
        '
        'progMain
        '
        Me.progMain.BackColor = System.Drawing.Color.Transparent
        Me.progMain.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.progMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.progMain.GradiantColor = System.Drawing.Color.Black
        Me.progMain.GradiantPosition = iDLE_DETECT_v3.ProgressBarEx.GradiantArea.None
        Me.progMain.Image = Nothing
        Me.progMain.Location = New System.Drawing.Point(0, 0)
        Me.progMain.Maximum = 60
        Me.progMain.Name = "progMain"
        Me.progMain.RoundedCorners = False
        Me.progMain.ShowText = True
        Me.progMain.Size = New System.Drawing.Size(524, 16)
        '
        'frmClose
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(524, 140)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.progMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmClose"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents progMain As iDLE_DETECT_v3.ProgressBarEx
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents tmrCountDown As System.Windows.Forms.Timer
    Friend WithEvents tmrCloseApps As System.Windows.Forms.Timer
    Friend WithEvents bgMain As System.ComponentModel.BackgroundWorker
End Class
