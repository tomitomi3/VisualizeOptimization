Imports LibOptimization
Imports LibOptimization.Optimization

Public Class clsOptHistoryHookeJeeves : Inherits absOptimizationHistory
    Private histroyPoints As New List(Of List(Of List(Of Double)))
    Private histroyEvals As New List(Of List(Of Double))

    ''' <summary>
    ''' Default constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Me.opt = New clsOptPatternSearch(New BenchmarkFunction.clsBenchRosenblock(2))
        Me.opt.Init()

        'initial value
        Dim oneStepPoints As New List(Of List(Of Double))
        Dim oneStepEvals As New List(Of Double)
        For Each p As clsPoint In opt.ResultForDebug
            Dim tempPoint As New clsPoint(p)
            oneStepPoints.Add(tempPoint)
            oneStepEvals.Add(tempPoint.Eval)
        Next
        Me.histroyPoints.Add(oneStepPoints)
        Me.histroyEvals.Add(oneStepEvals)

        'all step
        While (Me.opt.DoIteration(1) = False)
            oneStepPoints = New List(Of List(Of Double))
            oneStepEvals = New List(Of Double)
            For Each p As clsPoint In opt.ResultForDebug
                Dim tempPoint As New clsPoint(p)
                oneStepPoints.Add(tempPoint)
                oneStepEvals.Add(tempPoint.Eval)
            Next
            Me.histroyPoints.Add(oneStepPoints)
            Me.histroyEvals.Add(oneStepEvals)
        End While
    End Sub

    Public Overrides ReadOnly Property IterationCount As Integer
        Get
            Return Me.histroyPoints.Count
        End Get
    End Property

    Public Overrides Function Points(ai_index As Integer) As List(Of List(Of Double))
        If ai_index >= 0 AndAlso ai_index < IterationCount Then
            Return Me.histroyPoints(ai_index)
        End If
        Return Nothing
    End Function

    Public Overrides Function Evals(ai_index As Integer) As List(Of Double)
        If ai_index >= 0 AndAlso ai_index < IterationCount Then
            Return Me.histroyEvals(ai_index)
        End If
        Return Nothing
    End Function
End Class
