Imports LibOptimization.Optimization

Public MustInherit Class absOptimizationHistory
    Protected opt As absOptimization = Nothing
    Public MustOverride Sub Init(ByVal ai_func As absObjectiveFunction, ByVal ai_fixRandomSeed As Boolean)
    Public MustOverride Function Points(ByVal ai_index As Integer) As List(Of List(Of Double))
    Public MustOverride Function Evals(ByVal ai_index As Integer) As List(Of Double)
    Public MustOverride ReadOnly Property IterationCount As Integer
End Class
