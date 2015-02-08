Imports LibOptimization
Imports OxyPlot.Series
Imports OxyPlot
Imports LibOptimization.Optimization

Public Class Form1
    Private nowStepIndex As Integer = 0
    Dim isClick As Boolean = False
    Dim previousPoint As Drawing.Point
    Dim optType As OptSeries = OptSeries.NelderMead
    Dim hist As absOptimizationHistory = Nothing

    Declare Function AllocConsole Lib "kernel32.dll" () As Boolean

    Public Enum OptSeries
        NelderMead
        GA_REX
        GA_SPX
    End Enum

    ''' <summary>
    ''' Load イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'init plot
        Me.oPlot.Model = New OxyPlot.PlotModel()
        Me.oPlot.Model.PlotType = OxyPlot.PlotType.XY

        'sample code
        'Dim Series = New OxyPlot.Series.FunctionSeries(Function(x As Double) As Double
        '                                                   Return 1 / Math.Sqrt(2 * Math.PI) * Math.Exp(-x * x / 2)
        '                                               End Function, -100, 100, 0.1, "標準正規分布"

        Me.UpdateLabel()
        Me.tbxSkip.Text = "1"

        'oneshot timer
        oneShotTimer.Start()

        'select
        Me.cbxSelectOptmization.SelectedIndex = 0
        optType = OptSeries.NelderMead

        'alloc console
        AllocConsole()
    End Sub

    ''' <summary>
    ''' Init button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnInit_Click(sender As Object, e As EventArgs) Handles btnInit.Click
        InitOpt()
    End Sub

    ''' <summary>
    ''' "＜"button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If nowStepIndex <= 0 Then
            Return
        Else
            nowStepIndex -= CInt(Me.tbxSkip.Text)
            If nowStepIndex < 0 Then
                nowStepIndex = 0
            End If
        End If
        Me.Draw()
    End Sub

    ''' <summary>
    ''' "＞"button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If nowStepIndex + 1 >= hist.IterationCount Then
            Return
        Else
            nowStepIndex += CInt(Me.tbxSkip.Text)
            If nowStepIndex >= hist.IterationCount Then
                nowStepIndex = hist.IterationCount - 1
            End If
        End If
        Me.Draw()
    End Sub

    ''' <summary>
    ''' Ontshot
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub oneShotTimer_Tick(sender As Object, e As EventArgs) Handles oneShotTimer.Tick
        oneShotTimer.Stop()
        Me.DrawInitAxis(-1, -1, 2, 2, False)
    End Sub

    ''' <summary>
    ''' Init axis
    ''' </summary>
    ''' <param name="ai_x"></param>
    ''' <param name="ai_y"></param>
    ''' <param name="ai_width"></param>
    ''' <param name="ai_height"></param>
    ''' <param name="ai_isDraw"></param>
    ''' <remarks></remarks>
    Private Sub DrawInitAxis(ByVal ai_x As Double, ByVal ai_y As Double, ByVal ai_width As Double, ByVal ai_height As Double, Optional ByVal ai_isDraw As Boolean = True)
        Me.oPlot.Model.Series.Clear()
        Me.oPlot.Model.Axes.Clear()

        'plot setting
        Me.oPlot.BackColor = Color.White
        Me.oPlot.Model.PlotMargins = New OxyThickness(0, 0, 0, 0)
        Me.oPlot.Model.Padding = New OxyThickness(0, 0, 0, 0)

        'set view
        Dim x = New Axes.LinearAxis()
        x.Position = Axes.AxisPosition.Bottom
        x.Minimum = ai_x
        x.Maximum = ai_x + ai_width
        x.PositionAtZeroCrossing = True
        'x.MajorGridlineStyle = LineStyle.Automatic
        'x.MajorGridlineThickness = 0.5
        'x.MaximumPadding = 0
        'x.MinimumPadding = 0
        Me.oPlot.Model.Axes.Add(x)

        Dim y = New Axes.LinearAxis()
        y.Position = Axes.AxisPosition.Left
        y.Minimum = ai_y
        y.Maximum = ai_y + ai_height
        y.PositionAtZeroCrossing = True
        'y.MajorGridlineStyle = LineStyle.Automatic
        'y.MajorGridlineThickness = 0.5
        'y.MaximumPadding = 0
        'y.MinimumPadding = 0
        Me.oPlot.Model.Axes.Add(y)

        'Draw
        If ai_isDraw = True Then
            Me.Draw()
        Else
            Me.oPlot.InvalidatePlot(True)
        End If
    End Sub

    ''' <summary>
    ''' Draw
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Draw()
        Me.oPlot.Model.Series.Clear()

        'draw
        If Me.optType = OptSeries.NelderMead Then
            'Line
            Dim simplex = New LineSeries()
            simplex.Points.Add(New DataPoint(Me.hist.Points(nowStepIndex)(0)(0), Me.hist.Points(nowStepIndex)(0)(1)))
            simplex.Points.Add(New DataPoint(Me.hist.Points(nowStepIndex)(1)(0), Me.hist.Points(nowStepIndex)(1)(1)))
            simplex.Points.Add(New DataPoint(Me.hist.Points(nowStepIndex)(2)(0), Me.hist.Points(nowStepIndex)(2)(1)))
            simplex.Points.Add(New DataPoint(Me.hist.Points(nowStepIndex)(0)(0), Me.hist.Points(nowStepIndex)(0)(1)))
            simplex.Color = OxyColors.Blue
            simplex.StrokeThickness = 1
            Me.oPlot.Model.Series.Add(simplex)
            Console.WriteLine("{0} , {1} , {2}", Me.hist.Evals(Me.nowStepIndex)(0), Me.hist.Evals(Me.nowStepIndex)(1), Me.hist.Evals(Me.nowStepIndex)(2))
            Console.WriteLine(" {0} , {1}", hist.Points(Me.nowStepIndex)(0)(0), hist.Points(Me.nowStepIndex)(0)(1))
            Console.WriteLine(" {0} , {1}", hist.Points(Me.nowStepIndex)(1)(0), hist.Points(Me.nowStepIndex)(1)(1))
            Console.WriteLine(" {0} , {1}", hist.Points(Me.nowStepIndex)(2)(0), hist.Points(Me.nowStepIndex)(2)(1))
        Else
            Dim allPoints = New ScatterSeries()
            allPoints.MarkerType = MarkerType.Circle
            For i As Integer = 0 To Me.hist.Evals(0).Count - 1
                allPoints.Points.Add(New ScatterPoint(Me.hist.Points(nowStepIndex)(i)(0), Me.hist.Points(nowStepIndex)(i)(1)))
            Next
            allPoints.MarkerSize = 1.5
            Me.oPlot.Model.Series.Add(allPoints)
        End If

        'BestPoint
        Dim points = New ScatterSeries()
        points.MarkerType = MarkerType.Circle
        points.Points.Add(New ScatterPoint(1, 1))
        points.Points.Add(New ScatterPoint(1, 1))
        points.MarkerSize = 2
        Me.oPlot.Model.Series.Add(points)

        'update label
        Me.UpdateLabel()
        Me.oPlot.InvalidatePlot(True)
    End Sub

    Private Sub UpdateLabel()
        If Me.hist Is Nothing Then
            Me.lblIndex.Text = ""
        ElseIf hist.IterationCount = 0 Then
            Me.lblIndex.Text = String.Format("Step : {0}/{1}", 0, 0)
        Else
            Me.lblIndex.Text = String.Format("Step : {0}/{1}", Me.nowStepIndex + 1, hist.IterationCount)
        End If
    End Sub

    Private Function GetEnvelope() As Double()
        'get envelope
        Dim arx As New List(Of Double)
        Dim ary As New List(Of Double)
        For i As Integer = 0 To Me.hist.Points(0).Count - 1
            arx.Add(Me.hist.Points(0)(i)(0))
            ary.Add(Me.hist.Points(0)(i)(1))
        Next
        arx.Sort()
        ary.Sort()
        Return New Double() {arx(0), ary(0), arx(arx.Count - 1) - arx(0), ary(ary.Count - 1) - ary(0)}
    End Function

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles ctrlPanel.MouseDown
        isClick = True
        previousPoint = e.Location
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles ctrlPanel.MouseMove
        If isClick Then
            Dim movePoint As Drawing.Point
            movePoint.X = ctrlPanel.Location.X + e.X - previousPoint.X
            movePoint.Y = ctrlPanel.Location.Y + e.Y - previousPoint.Y
            ctrlPanel.Location = movePoint
        End If
    End Sub

    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles ctrlPanel.MouseUp
        isClick = False
    End Sub

    Private Sub cbxSelectOptmization_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxSelectOptmization.SelectedIndexChanged
        If Me.cbxSelectOptmization.SelectedIndex = OptSeries.NelderMead Then
            Me.optType = OptSeries.NelderMead
        ElseIf Me.cbxSelectOptmization.SelectedIndex = OptSeries.GA_REX Then
            Me.optType = OptSeries.GA_REX
        Else
            Me.optType = OptSeries.GA_SPX
        End If
    End Sub

    Private Sub InitOpt()
        If Me.optType = OptSeries.NelderMead Then
            Me.hist = New clsOptHistoryNelderMead()
        ElseIf Me.optType = OptSeries.GA_REX Then
            Me.hist = New clsOptHistoryRGAREX()
        Else : Me.optType = OptSeries.GA_SPX
            Me.hist = New clsOptHistoryRGASPX()
        End If

        Me.nowStepIndex = 0

        Dim tempAxis() As Double = Me.GetEnvelope()
        Me.DrawInitAxis(tempAxis(0), tempAxis(1), tempAxis(2), tempAxis(3))
    End Sub
End Class
