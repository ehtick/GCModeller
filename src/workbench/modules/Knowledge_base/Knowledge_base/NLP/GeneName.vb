﻿Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Algorithm.BinaryTree
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.DataMining.BinaryTree
Imports Microsoft.VisualBasic.DataMining.DynamicProgramming
Imports Microsoft.VisualBasic.DataMining.DynamicProgramming.NeedlemanWunsch
Imports std = System.Math

Public Module GeneName

    <Extension>
    Public Function GroupBy(Of EntityObject As {INamedValue, DynamicPropertyBase(Of String)})(genes As IEnumerable(Of EntityObject), field As String, Optional cutoff As Double = 0.3) As IEnumerable(Of NamedCollection(Of EntityObject))
        Return genes.GroupBy(Function(d) d(field), cutoff)
    End Function

    <Extension>
    Public Iterator Function GroupBy(Of T As INamedValue)(genes As IEnumerable(Of T), field As Func(Of T, String), Optional cutoff As Double = 0.3) As IEnumerable(Of NamedCollection(Of T))
        Dim tree As New AVLTree(Of String, String)(New TextSimilar(cutoff).GetComparer)
        Dim gene_id As New Dictionary(Of String, T)

        For Each gene As T In genes
            gene_id.Add(gene.Key, gene)
            tree.Add(field(gene), gene.Key)
        Next

        Dim text_clusters = tree.root.PopulateNodes.ToArray

        For Each cluster As BinaryTree(Of String, String) In text_clusters
            Yield New NamedCollection(Of T)(cluster.Key, cluster.Members.Select(Function(id) gene_id(id)))
        Next
    End Function

    Private Class TextSimilar : Inherits ComparisonProvider

        ReadOnly matrix As ScoreMatrix(Of Char)
        ReadOnly symbol As GenericSymbol(Of Char) = GetGeneralCharSymbol()

        Sub New(cutoff As Double)
            MyBase.New(cutoff, cutoff / 2)
            matrix = New ScoreMatrix(Of Char)(symbol)
        End Sub

        Private Function GetDPSimilarity(x As String, y As String) As Double
            Dim gnw As New NeedlemanWunsch(Of Char)(x, y, matrix, symbol)
            Dim best As GlobalAlign(Of Char) = gnw _
                .Compute() _
                .PopulateAlignments _
                .OrderByDescending(Function(a) a.score) _
                .FirstOrDefault

            Return best.score / best.Length
        End Function

        Public Overrides Function GetSimilarity(x As String, y As String) As Double
            Dim tx As String() = x.Split
            Dim ty As String() = y.Split
            Dim len As Integer = std.Min(tx.Length, ty.Length)

            For i As Integer = 0 To len - 1
                If Not tx(i).TextEquals(ty(i)) Then
                    Return i / len
                End If
            Next

            Return 1.0
        End Function

        Public Overrides Function GetObject(id As String) As Object
            Return id
        End Function
    End Class
End Module