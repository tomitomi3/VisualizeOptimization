Imports LibOptimization
Imports LibOptimization.Optimization

Public Class clsOptHistoryStepestDescent : Inherits absOptimizationHistory
    Private histroyPoints As New List(Of List(Of List(Of Double)))
    Private histroyEvals As New List(Of List(Of Double))

    ''' <summary>
    ''' Default constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        'nop
    End Sub

    ''' <summary>
    ''' Initialize
    ''' </summary>
    ''' <param name="ai_func"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Init(ai_func As absObjectiveFunction, ByVal ai_fixRandomSeed As Boolean)
        Me.opt = New clsOptSteepestDescent(ai_func)
        If ai_fixRandomSeed = True Then
            Me.opt.Random = New LibOptimization.Util.clsRandomXorshift(12345678)
            LibOptimization.Util.clsRandomXorshiftSingleton.GetInstance().SetDefaultSeed()
        End If
        Me.opt.Init()

        'initial value
        Dim oneStepPoints As New List(Of List(Of Double))
        Dim oneStepEvals As New List(Of Double)

        Dim tempPoint = New clsPoint(opt.Result)
        oneStepPoints.Add(tempPoint)
        oneStepEvals.Add(tempPoint.Eval)

        Me.histroyPoints.Add(oneStepPoints)
        Me.histroyEvals.Add(oneStepEvals)

        'all step
        While (Me.opt.DoIteration(1) = False)
            oneStepPoints = New List(Of List(Of Double))
            oneStepEvals = New List(Of Double)

            tempPoint = New clsPoint(opt.Result)
            oneStepPoints.Add(tempPoint)
            oneStepEvals.Add(tempPoint.Eval)

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
