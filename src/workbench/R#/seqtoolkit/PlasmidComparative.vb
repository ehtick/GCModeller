﻿#Region "Microsoft.VisualBasic::2dfe1e697ee7c3f33b68457c3f6e5ee8, R#\seqtoolkit\PlasmidComparative.vb"

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

    '   Total Lines: 82
    '    Code Lines: 71 (86.59%)
    ' Comment Lines: 5 (6.10%)
    '    - Xml Docs: 80.00%
    ' 
    '   Blank Lines: 6 (7.32%)
    '     File Size: 4.72 KB


    ' Module PlasmidComparative
    ' 
    '     Function: __generateCols, __row, CreateDeltaMatrix, PlasmidPartitioning
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Data.Framework
Imports Microsoft.VisualBasic.Data.Framework.IO
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports SMRUCC.genomics.Analysis.SequenceTools.DNA_Comparative
Imports SMRUCC.genomics.ComponentModel.Annotation
Imports SMRUCC.genomics.Interops.NCBI.Extensions.Tasks.Models
Imports SMRUCC.genomics.SequenceModel
Imports SMRUCC.genomics.SequenceModel.FASTA
Imports SMRUCC.genomics.SequenceModel.NucleotideModels

''' <summary>
''' 根据BBH结果所计算出来的保守片段之间进行delta值的相互比较
''' </summary>
''' <remarks></remarks>
''' 
<Package("Comparative.Plasmid")>
Public Module PlasmidComparative

    <ExportAPI("Plasmid.Partitioning")>
    Public Function PlasmidPartitioning(Besthits As SpeciesBesthit, CdsInfo As GeneTable(), Fasta As FastaSeq) As PartitioningData()
        Dim ConservedRegions = Besthits.GetConservedRegions
        Dim ORF = (From gene As GeneTable
                   In CdsInfo
                   Select gene
                   Group By gene.locus_id Into Group) _
                         .ToDictionary(Function(gene) gene.locus_id,
                                       Function(gene)
                                           Return gene.Group.First
                                       End Function)
        Dim Regions As List(Of String()) =
            New List(Of String())(ConservedRegions) + From id As String
                                                      In Besthits.GetUnConservedRegions(ConservedRegions)
                                                      Select New String() {id}
        Dim LQuery As PartitioningData() =
            LinqAPI.Exec(Of PartitioningData) <= From ls As String()
                                                 In Regions
                                                 Let pos As Integer() = (From id As String
                                                                         In ls
                                                                         Let nn As GeneTable = ORF(id)
                                                                         Select {nn.left, nn.right}).ToVector
                                                 Let left As Integer = pos.Min
                                                 Let right As Integer = pos.Max
                                                 Select New PartitioningData With {
                                                     .GenomeID = Fasta.Title,
                                                     .ORFList = ls,
                                                     .PartitioningTag = String.Join(", ", ls),
                                                     .LociLeft = left,
                                                     .LociRight = right,
                                                     .SequenceData = Fasta.CutSequenceLinear(left, right).SequenceData
                                                 }
        Return LQuery.OrderBy(Function(x) x.PartitioningTag).ToArray
    End Function

    <ExportAPI("Plasmid.DeltaMatrix")>
    Public Function CreateDeltaMatrix(partitions As IEnumerable(Of PartitioningData)) As File
        Dim df As New IO.File
        Dim cache = (From part As PartitioningData In partitions Select CacheData = New NucleicAcid(part.SequenceData), part).ToArray ' 因为要保持一一对应关系，所以这里不可以使用并行化拓展了
        Dim y As NucleicAcid() = cache.Select(Function(x) x.CacheData).ToArray

        df += ("X/Y" + (From part As PartitioningData In partitions Select part.PartitioningTag).AsList)
        df += From x In cache
              Let cols As List(Of String) =
                  __generateCols(x.CacheData, y)
              Select __row(x.part, cols) ' 因为要保持一一对应关系，所以这里不可以使用并行化拓展了
        Return df
    End Function

    Private Function __generateCols(x As NucleicAcid, cache As IEnumerable(Of NucleicAcid)) As List(Of String)
        Return LinqAPI.MakeList(Of String) <= From y As NucleicAcid
                                              In cache
                                              Let n As Double = 1000 * DeltaSimilarity1998.Sigma(x, y)
                                              Select CStr(CInt(n))
    End Function

    Private Function __row(item As PartitioningData, cols As List(Of String)) As RowObject
        Dim row As New RowObject(item.PartitioningTag + cols)
        Return row
    End Function
End Module
