﻿Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.ComponentModel.TagData
Imports Microsoft.VisualBasic.Math.Quantile

Namespace Distributions

    Public Module AxisDensity

        <Extension>
        Public Iterator Function GetClusters(axis As IEnumerable(Of Double)) As IEnumerable(Of DoubleTagged(Of Double()))
            Dim sortted = axis.OrderBy(Function(xi) xi).ToArray
            Dim diff As Double() = NumberGroups.diff(sortted)
            Dim threshold As Double = diff.Quartile.Q3

            For Each group As NamedCollection(Of Double) In sortted.GroupBy(offset:=threshold)
                Yield New DoubleTagged(Of Double())(Val(group.name), group.value)
            Next
        End Function

    End Module
End Namespace