<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.oPlot = New OxyPlot.WindowsForms.Plot()
        Me.oneShotTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ctrlPanel = New System.Windows.Forms.Panel()
        Me.cbxSelectOptmization = New System.Windows.Forms.ComboBox()
        Me.lblIndex = New System.Windows.Forms.Label()
        Me.tbxSkip = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnInit = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.ctrlPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'oPlot
        '
        Me.oPlot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.oPlot.Location = New System.Drawing.Point(0, 0)
        Me.oPlot.Name = "oPlot"
        Me.oPlot.PanCursor = System.Windows.Forms.Cursors.Hand
        Me.oPlot.Size = New System.Drawing.Size(400, 400)
        Me.oPlot.TabIndex = 1
        Me.oPlot.Text = "Plot1"
        Me.oPlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE
        Me.oPlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE
        Me.oPlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS
        '
        'oneShotTimer
        '
        Me.oneShotTimer.Interval = 1
        '
        'ctrlPanel
        '
        Me.ctrlPanel.Controls.Add(Me.cbxSelectOptmization)
        Me.ctrlPanel.Controls.Add(Me.lblIndex)
        Me.ctrlPanel.Controls.Add(Me.tbxSkip)
        Me.ctrlPanel.Controls.Add(Me.Label1)
        Me.ctrlPanel.Controls.Add(Me.btnInit)
        Me.ctrlPanel.Controls.Add(Me.btnNext)
        Me.ctrlPanel.Controls.Add(Me.btnBack)
        Me.ctrlPanel.Location = New System.Drawing.Point(12, 12)
        Me.ctrlPanel.Name = "ctrlPanel"
        Me.ctrlPanel.Size = New System.Drawing.Size(269, 56)
        Me.ctrlPanel.TabIndex = 2
        '
        'cbxSelectOptmization
        '
        Me.cbxSelectOptmization.FormattingEnabled = True
        Me.cbxSelectOptmization.Items.AddRange(New Object() {"NelderMead", "Genetic Algorithm REX with JGG", "Genetic Algorithm SPX with JGG"})
        Me.cbxSelectOptmization.Location = New System.Drawing.Point(122, 30)
        Me.cbxSelectOptmization.Name = "cbxSelectOptmization"
        Me.cbxSelectOptmization.Size = New System.Drawing.Size(141, 20)
        Me.cbxSelectOptmization.TabIndex = 3
        '
        'lblIndex
        '
        Me.lblIndex.AutoSize = True
        Me.lblIndex.Location = New System.Drawing.Point(3, 33)
        Me.lblIndex.Name = "lblIndex"
        Me.lblIndex.Size = New System.Drawing.Size(78, 12)
        Me.lblIndex.TabIndex = 6
        Me.lblIndex.Text = "Index aaa/bbb"
        '
        'tbxSkip
        '
        Me.tbxSkip.Location = New System.Drawing.Point(222, 5)
        Me.tbxSkip.Name = "tbxSkip"
        Me.tbxSkip.Size = New System.Drawing.Size(41, 19)
        Me.tbxSkip.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(162, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 12)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "SkipIndex"
        '
        'btnInit
        '
        Me.btnInit.Location = New System.Drawing.Point(3, 3)
        Me.btnInit.Name = "btnInit"
        Me.btnInit.Size = New System.Drawing.Size(75, 23)
        Me.btnInit.TabIndex = 7
        Me.btnInit.Text = "Init"
        Me.btnInit.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(122, 3)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(32, 23)
        Me.btnNext.TabIndex = 9
        Me.btnNext.Text = ">"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(84, 3)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(32, 23)
        Me.btnBack.TabIndex = 8
        Me.btnBack.Text = "<"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 400)
        Me.Controls.Add(Me.ctrlPanel)
        Me.Controls.Add(Me.oPlot)
        Me.Name = "Form1"
        Me.Text = "VisualizeOptimization"
        Me.ctrlPanel.ResumeLayout(False)
        Me.ctrlPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents oPlot As OxyPlot.WindowsForms.Plot
    Friend WithEvents oneShotTimer As System.Windows.Forms.Timer
    Friend WithEvents ctrlPanel As System.Windows.Forms.Panel
    Friend WithEvents lblIndex As System.Windows.Forms.Label
    Friend WithEvents tbxSkip As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnInit As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents cbxSelectOptmization As System.Windows.Forms.ComboBox

End Class
