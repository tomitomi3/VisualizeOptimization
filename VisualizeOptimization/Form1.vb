Imports LibOptimization
Imports OxyPlot.Series
Imports OxyPlot
Imports LibOptimization.Optimization

Public Class Form1
    Private opt As Optimization.clsOptNelderMead = Nothing
    Private nowStepIndex As Integer = 0
    Private vertexs As New List(Of List(Of List(Of Double)))
    Private evals As New List(Of List(Of Double))

    Declare Function AllocConsole Lib "kernel32.dll" () As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'init plot
        Me.oPlot.Model = New OxyPlot.PlotModel()
        Me.oPlot.Model.PlotType = OxyPlot.PlotType.XY

        'sample code
        'Dim Series = New OxyPlot.Series.FunctionSeries(Function(x As Double) As Double
        '                                                   Return 1 / Math.Sqrt(2 * Math.PI) * Math.Exp(-x * x / 2)
        '                                               End Function, -100, 100, 0.1, "標準正規分布"

        'init opt
        Me.opt = New clsOptNelderMead(New BenchmarkFunction.clsBenchRosenblock(2))

        Me.UpdateLabel()
        Me.tbxSkip.Text = "1"

        'oneshot timer
        oneShotTimer.Start()

        'alloc console
        'AllocConsole()
    End Sub

    Private Sub btnInit_Click(sender As Object, e As EventArgs) Handles btnInit.Click
        'init opt
        Me.opt.Init(New Double()() {New Double() {0, 0}, New Double() {0, 1}, New Double() {1, 0}})

        'Get simplex history
        vertexs.Clear()
        evals.Clear()
        Dim tempSimplex As New List(Of List(Of Double))
        Dim tempSimplexEvals As New List(Of Double)
        For Each p As clsPoint In CType(opt, clsOptNelderMead).AllVertexs
            Dim tempPoint As New clsPoint(p)
            tempSimplex.Add(tempPoint)
            tempSimplexEvals.Add(tempPoint.Eval)
        Next
        Me.vertexs.Add(tempSimplex)
        Me.evals.Add(tempSimplexEvals)
        While (Me.opt.DoIteration(1) = False)
            tempSimplex = New List(Of List(Of Double))
            tempSimplexEvals = New List(Of Double)
            For Each p As clsPoint In CType(opt, clsOptNelderMead).AllVertexs
                Dim tempPoint As New clsPoint(p)
                tempSimplex.Add(tempPoint)
                tempSimplexEvals.Add(tempPoint.Eval)
            Next
            Me.vertexs.Add(tempSimplex)
            Me.evals.Add(tempSimplexEvals)
        End While
        Me.nowStepIndex = 0

        Me.DrawInitAxis(-0.5, -0.5, 2, 2)
    End Sub

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

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If nowStepIndex + 1 >= vertexs.Count Then
            Return
        Else
            nowStepIndex += CInt(Me.tbxSkip.Text)
            If nowStepIndex >= Me.vertexs.Count Then
                nowStepIndex = Me.vertexs.Count - 1
            End If
        End If
        Me.Draw()
    End Sub

    Private Sub oneShotTimer_Tick(sender As Object, e As EventArgs) Handles oneShotTimer.Tick
        oneShotTimer.Stop()
        Me.DrawInitAxis(-1, -1, 2, 2, False)
    End Sub

    Private Sub oPlot_Paint(sender As Object, e As PaintEventArgs) Handles oPlot.Paint
        'Console.WriteLine("{0},{1}", Me.oPlot.Model.DefaultXAxis.ActualMinimum, Me.oPlot.Model.DefaultXAxis.ActualMaximum)
        'Dim zeroCrossXAxis = New LineSeries()
        'zeroCrossXAxis.Points.Add(New DataPoint(Me.oPlot.Model.DefaultXAxis.ActualMinimum, 0))
        'zeroCrossXAxis.Points.Add(New DataPoint(Me.oPlot.Model.DefaultXAxis.ActualMaximum, 0))
        'zeroCrossXAxis.Color = OxyColors.Black
        'zeroCrossXAxis.StrokeThickness = 0.5
        'Me.oPlot.Model.Series.Add(zeroCrossXAxis)

        'Dim zeroCrossYAxis = New LineSeries()
        'zeroCrossYAxis.Points.Add(New DataPoint(0, Me.oPlot.Model.DefaultYAxis.ActualMinimum))
        'zeroCrossYAxis.Points.Add(New DataPoint(0, Me.oPlot.Model.DefaultYAxis.ActualMaximum))
        'zeroCrossYAxis.Color = OxyColors.Black
        'zeroCrossYAxis.StrokeThickness = 0.5
        'Me.oPlot.Model.Series.Add(zeroCrossYAxis)
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
    Private Sub DrawInitAxis(ByVal ai_x As Double, ByVal ai_y As Double, ByVal ai_width As Double, ByVal ai_height As Double, _
                         Optional ByVal ai_isDraw As Boolean = True)
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

        'Point
        Dim points = New ScatterSeries()
        points.Points.Add(New ScatterPoint(1, 1))
        points.Points.Add(New ScatterPoint(1, 1))
        points.MarkerSize = 2
        points.TextColor = OxyColors.Black
        Me.oPlot.Model.Series.Add(points)

        'Line
        Dim firstSeries = New LineSeries()
        firstSeries.Points.Add(New DataPoint(Me.vertexs(nowStepIndex)(0)(0), Me.vertexs(nowStepIndex)(0)(1)))
        firstSeries.Points.Add(New DataPoint(Me.vertexs(nowStepIndex)(1)(0), Me.vertexs(nowStepIndex)(1)(1)))
        firstSeries.Points.Add(New DataPoint(Me.vertexs(nowStepIndex)(2)(0), Me.vertexs(nowStepIndex)(2)(1)))
        firstSeries.Points.Add(New DataPoint(Me.vertexs(nowStepIndex)(0)(0), Me.vertexs(nowStepIndex)(0)(1)))
        firstSeries.Color = OxyColors.Red
        firstSeries.StrokeThickness = 1
        Me.oPlot.Model.Series.Add(firstSeries)

        Console.WriteLine("{0} , {1} , {2}", Me.evals(Me.nowStepIndex)(0), Me.evals(Me.nowStepIndex)(1), Me.evals(Me.nowStepIndex)(2))
        Console.WriteLine(" {0} , {1}", vertexs(Me.nowStepIndex)(0)(0), vertexs(Me.nowStepIndex)(0)(1))
        Console.WriteLine(" {0} , {1}", vertexs(Me.nowStepIndex)(1)(0), vertexs(Me.nowStepIndex)(1)(1))
        Console.WriteLine(" {0} , {1}", vertexs(Me.nowStepIndex)(2)(0), vertexs(Me.nowStepIndex)(2)(1))

        'update label
        Me.UpdateLabel()
        Me.oPlot.InvalidatePlot(True)
    End Sub

    Private Sub UpdateLabel()
        If Me.vertexs.Count = 0 Then
            Me.lblIndex.Text = String.Format("Step : {0}/{1}", 0, 0)
        Else
            Me.lblIndex.Text = String.Format("Step : {0}/{1}", Me.nowStepIndex + 1, Me.vertexs.Count)
        End If
    End Sub
End Class
