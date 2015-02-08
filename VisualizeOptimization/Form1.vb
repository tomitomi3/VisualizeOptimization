Imports LibOptimization
Imports OxyPlot.Series
Imports OxyPlot
Imports LibOptimization.Optimization

Public Class Form1
    Private opt As Optimization.absOptimization = Nothing
    Private nowStepIndex As Integer = 0
    Private vertexs As New List(Of List(Of List(Of Double)))
    Private evals As New List(Of List(Of Double))

    Declare Function AllocConsole Lib "kernel32.dll" () As Boolean

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

        'alloc console
        AllocConsole()
    End Sub

    Private Sub btnInit_Click(sender As Object, e As EventArgs) Handles btnInit.Click
        'init opt
        Me.opt = New clsOptNelderMead(New BenchmarkFunction.clsBenchRosenblock(2))
        Me.opt.Init()

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

        Dim hoge() As Double = Me.GetEnvelope()

        Me.DrawInitAxis(hoge(0), hoge(1), hoge(2), hoge(3))
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

        'draw simplex

        'Line
        Dim simplexPoint = New LineSeries()
        simplexPoint.Points.Add(New DataPoint(Me.vertexs(nowStepIndex)(0)(0), Me.vertexs(nowStepIndex)(0)(1)))
        simplexPoint.Points.Add(New DataPoint(Me.vertexs(nowStepIndex)(1)(0), Me.vertexs(nowStepIndex)(1)(1)))
        simplexPoint.Points.Add(New DataPoint(Me.vertexs(nowStepIndex)(2)(0), Me.vertexs(nowStepIndex)(2)(1)))
        simplexPoint.Points.Add(New DataPoint(Me.vertexs(nowStepIndex)(0)(0), Me.vertexs(nowStepIndex)(0)(1)))
        simplexPoint.Color = OxyColors.Red
        simplexPoint.StrokeThickness = 1
        Me.oPlot.Model.Series.Add(simplexPoint)

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

    Private Function GetEnvelope() As Double()
        'get envelope
        Dim arx As New List(Of Double)
        Dim ary As New List(Of Double)
        arx.Add(1.0)
        ary.Add(1.0)
        For i As Integer = 0 To Me.vertexs(0).Count - 1
            arx.Add(Me.vertexs(0)(i)(0))
            ary.Add(Me.vertexs(0)(i)(1))
        Next
        arx.Sort()
        ary.Sort()

        Return New Double() {arx(0), ary(0), arx(arx.Count - 1) - arx(0), ary(ary.Count - 1) - ary(0)}
    End Function

    Dim isClick As Boolean = False
    Dim previousPoint As Drawing.Point
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
End Class
