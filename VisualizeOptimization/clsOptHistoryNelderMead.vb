Imports LibOptimization
Imports LibOptimization.Optimization

Public Class clsOptHistoryNelderMead : Inherits absOptimizationHistory
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
        Me.opt = New clsOptNelderMead(ai_func)
        If ai_fixRandomSeed = True Then
            Me.opt.Random = New LibOptimization.Util.clsRandomXorshift(12345678)
            LibOptimization.Util.clsRandomXorshiftSingleton.GetInstance().SetDefaultSeed()
        End If
        Me.opt.Init()

        'Get simplex history
        Dim tempSimplex As New List(Of List(Of Double))
        Dim tempSimplexEvals As New List(Of Double)
        For Each p As clsPoint In opt.Results
            Dim tempPoint As New clsPoint(p)
            tempSimplex.Add(tempPoint)
            tempSimplexEvals.Add(tempPoint.Eval)
        Next
        Me.histroyPoints.Add(tempSimplex)
        Me.histroyEvals.Add(tempSimplexEvals)
        While (Me.opt.DoIteration(1) = False)
            tempSimplex = New List(Of List(Of Double))
            tempSimplexEvals = New List(Of Double)
            For Each p As clsPoint In opt.Results
                Dim tempPoint As New clsPoint(p)
                tempSimplex.Add(tempPoint)
                tempSimplexEvals.Add(tempPoint.Eval)
            Next
            Me.histroyPoints.Add(tempSimplex)
            Me.histroyEvals.Add(tempSimplexEvals)
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
