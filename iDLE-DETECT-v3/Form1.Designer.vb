<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.picStatus = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.bgDetectBlackList = New System.ComponentModel.BackgroundWorker()
        Me.tmrIsActive = New System.Windows.Forms.Timer(Me.components)
        Me.bgDownloader = New System.ComponentModel.BackgroundWorker()
        CType(Me.picStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picStatus
        '
        Me.picStatus.BackColor = System.Drawing.Color.Lime
        Me.picStatus.Location = New System.Drawing.Point(55, 9)
        Me.picStatus.Name = "picStatus"
        Me.picStatus.Size = New System.Drawing.Size(100, 16)
        Me.picStatus.TabIndex = 0
        Me.picStatus.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Status:"
        '
        'bgDetectBlackList
        '
        Me.bgDetectBlackList.WorkerReportsProgress = True
        Me.bgDetectBlackList.WorkerSupportsCancellation = True
        '
        'tmrIsActive
        '
        '
        'bgDownloader
        '
        Me.bgDownloader.WorkerReportsProgress = True
        Me.bgDownloader.WorkerSupportsCancellation = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(181, 39)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.picStatus)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.picStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picStatus As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents bgDetectBlackList As System.ComponentModel.BackgroundWorker
    Friend WithEvents tmrIsActive As System.Windows.Forms.Timer
    Friend WithEvents bgDownloader As System.ComponentModel.BackgroundWorker

End Class
