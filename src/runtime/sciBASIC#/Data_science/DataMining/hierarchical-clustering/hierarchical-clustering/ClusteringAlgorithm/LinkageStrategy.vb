﻿#Region "Microsoft.VisualBasic::a0fa2573925b769d1957c5a39ce38d77, Data_science\DataMining\hierarchical-clustering\hierarchical-clustering\ClusteringAlgorithm\LinkageStrategy.vb"

    ' Author:
    ' 
    '       asuka (amethyst.asuka@gcmodeller.org)
    '       xie (genetics@smrucc.org)
    '       xieguigang (xie.guigang@live.com)
    ' 
    ' Copyright (c) 2018 GPL3 Licensed
    ' 
    ' 
    ' GNU GENERAL PUBLIC LICENSE (GPL3)
    ' 
    ' 
    ' This program is free software: you can redistribute it and/or modify
    ' it under the terms of the GNU General Public License as published by
    ' the Free Software Foundation, either version 3 of the License, or
    ' (at your option) any later version.
    ' 
    ' This program is distributed in the hope that it will be useful,
    ' but WITHOUT ANY WARRANTY; without even the implied warranty of
    ' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    ' GNU General Public License for more details.
    ' 
    ' You should have received a copy of the GNU General Public License
    ' along with this program. If not, see <http://www.gnu.org/licenses/>.



    ' /********************************************************************************/

    ' Summaries:


    ' Code Statistics:

    '   Total Lines: 151
    '    Code Lines: 101 (66.89%)
    ' Comment Lines: 17 (11.26%)
    '    - Xml Docs: 0.00%
    ' 
    '   Blank Lines: 33 (21.85%)
    '     File Size: 4.93 KB


    ' Interface LinkageStrategy
    ' 
    '     Function: (+2 Overloads) CalculateDistance
    ' 
    ' Class SingleLinkageStrategy
    ' 
    '     Function: (+2 Overloads) CalculateDistance
    ' 
    ' Class WeightedLinkageStrategy
    ' 
    '     Function: (+2 Overloads) CalculateDistance
    ' 
    ' Class CompleteLinkageStrategy
    ' 
    '     Function: (+2 Overloads) CalculateDistance
    ' 
    ' Class AverageLinkageStrategy
    ' 
    '     Function: (+2 Overloads) CalculateDistance
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.DataMining.HierarchicalClustering.Hierarchy

'
'*****************************************************************************
' Copyright 2013 Lars Behnke
' 
' Licensed under the Apache License, Version 2.0 (the "License");
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
' 
'   http://www.apache.org/licenses/LICENSE-2.0
' 
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.
' *****************************************************************************
'

Public Interface LinkageStrategy

    Function CalculateDistance(distances As ICollection(Of Distance)) As Distance
    Function CalculateDistance(a As Distance, b As Distance) As Double

End Interface

Public Class SingleLinkageStrategy : Implements LinkageStrategy

    Public Function CalculateDistance(distances As ICollection(Of Distance)) As Distance Implements LinkageStrategy.CalculateDistance
        Dim min As Double = Double.MaxValue

        For Each dist As Distance In distances
            If dist.Distance < min Then
                min = dist.Distance
            End If
        Next

        Return New Distance(min)
    End Function

    Public Function CalculateDistance(a As Distance, b As Distance) As Double Implements LinkageStrategy.CalculateDistance
        Dim min As Double = Double.MaxValue

        If a IsNot Nothing AndAlso a.Distance < min Then
            min = a.Distance
        End If
        If b IsNot Nothing AndAlso b.Distance < min Then
            min = b.Distance
        End If

        Return min
    End Function
End Class

Public Class WeightedLinkageStrategy : Implements LinkageStrategy

    Public Function CalculateDistance(distances As ICollection(Of Distance)) As Distance Implements LinkageStrategy.CalculateDistance
        Dim sum As Double = 0
        Dim weightTotal As Double = 0

        For Each distance As Distance In distances
            weightTotal += distance.Weight
            sum += distance.Distance * distance.Weight
        Next

        Return New Distance(sum / weightTotal, weightTotal)
    End Function

    Public Function CalculateDistance(a As Distance, b As Distance) As Double Implements LinkageStrategy.CalculateDistance
        Dim sum As Double = 0
        Dim weightTotal As Double = 0

        If Not a Is Nothing Then
            weightTotal += a.Weight
            sum += a.Distance * a.Weight
        End If
        If Not b Is Nothing Then
            weightTotal += b.Weight
            sum += b.Distance * b.Weight
        End If

        Return sum / weightTotal
    End Function
End Class

Public Class CompleteLinkageStrategy : Implements LinkageStrategy

    Public Function CalculateDistance(distances As ICollection(Of Distance)) As Distance Implements LinkageStrategy.CalculateDistance
        Dim max As Double = Double.MinValue

        For Each dist As Distance In distances
            If dist.Distance > max Then max = dist.Distance
        Next

        Return New Distance(max)
    End Function

    Public Function CalculateDistance(a As Distance, b As Distance) As Double Implements LinkageStrategy.CalculateDistance
        Dim max As Double = Double.MinValue

        If a IsNot Nothing AndAlso a.Distance > max Then
            max = a.Distance
        End If
        If b IsNot Nothing AndAlso b.Distance > max Then
            max = b.Distance
        End If

        Return max
    End Function
End Class

Public Class AverageLinkageStrategy : Implements LinkageStrategy

    Public Function CalculateDistance(distances As ICollection(Of Distance)) As Distance Implements LinkageStrategy.CalculateDistance
        Dim sum As Double = 0
        Dim result As Double

        For Each dist As Distance In distances
            sum += dist.Distance
        Next

        If distances.Count > 0 Then
            result = sum / distances.Count
        Else
            result = 0.0
        End If

        Return New Distance(result)
    End Function

    Public Function CalculateDistance(a As Distance, b As Distance) As Double Implements LinkageStrategy.CalculateDistance
        Dim sum As Double
        Dim n As Integer = 0

        If Not a Is Nothing Then
            n += 1
            sum += a.Distance
        End If
        If Not b Is Nothing Then
            n += 1
            sum += b.Distance
        End If

        If n > 0 Then
            Return sum / n
        Else
            Return 0
        End If
    End Function
End Class
