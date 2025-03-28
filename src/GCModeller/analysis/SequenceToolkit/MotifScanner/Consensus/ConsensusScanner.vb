﻿#Region "Microsoft.VisualBasic::defa7e06053b50045f745fe73afac34b, analysis\SequenceToolkit\MotifScanner\Consensus\ConsensusScanner.vb"

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

    '   Total Lines: 108
    '    Code Lines: 89 (82.41%)
    ' Comment Lines: 5 (4.63%)
    '    - Xml Docs: 60.00%
    ' 
    '   Blank Lines: 14 (12.96%)
    '     File Size: 4.91 KB


    ' Class ConsensusScanner
    ' 
    '     Constructor: (+1 Overloads) Sub New
    ' 
    '     Function: GetKOMaps, GetKOUpstream, ParseUpstream, (+2 Overloads) PopulateMotifs
    ' 
    '     Sub: DumpSequence
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Linq
Imports SMRUCC.genomics.ContextModel.Promoter
Imports SMRUCC.genomics.SequenceModel.FASTA

''' <summary>
''' 扫描出相同的KO编号的基因的上游区域的片段的相似片段
''' </summary>
Public Class ConsensusScanner

    Dim KOUpstream As Dictionary(Of String, FastaSeq())
    Dim KO As Dictionary(Of String, String())

    Sub New(genomes As IEnumerable(Of genomic), Optional length As PrefixLength = PrefixLength.L300)
        With genomes.ToArray
            Dim upstreams = .Select(Function(g) ParseUpstream(g, length)).ToArray

            KO = .DoCall(AddressOf GetKOMaps)
            KOUpstream = KO.ToDictionary(Function(id) id.Key,
                                         Function(consensus)
                                             Return GetKOUpstream(upstreams, consensus)
                                         End Function)
        End With
    End Sub

    Private Shared Function GetKOUpstream(upstreams As Dictionary(Of String, FastaSeq)(), consensus As KeyValuePair(Of String, String())) As FastaSeq()
        Dim geneIDs$() = consensus.Value
        Dim upstream = geneIDs _
            .Select(Function(ID)
                        Dim selected = upstreams _
                            .Where(Function(genome) genome.ContainsKey(ID)) _
                            .FirstOrDefault

                        If selected Is Nothing Then
                            Console.WriteLine(ID)
                            Return Nothing
                        Else
                            Return selected(ID)
                        End If
                    End Function) _
            .Where(Function(seq) Not seq Is Nothing) _
            .ToArray
        Return upstream
    End Function

    Private Shared Function GetKOMaps(genomes As genomic()) As Dictionary(Of String, String())
        Return genomes.Select(Function(genome)
                                  Return genome.organism _
                                    .genome _
                                    .Select(Function(pathway) pathway.genes) _
                                    .IteratesALL _
                                    .Where(Function(g)
                                               Return Not g.KO.StringEmpty
                                           End Function)
                              End Function) _
            .IteratesALL _
            .GroupBy(Function(gene) gene.KO) _
            .ToDictionary(Function(id) id.Key,
                        Function(genes)
                            ' KEGG之中的基因编号都会存在一个物种缩写的前缀
                            ' 在这里移除掉
                            Return genes.Select(Function(g) g.geneId).ToArray
                        End Function)
    End Function

    Private Shared Function ParseUpstream(genomic As genomic, length As PrefixLength) As Dictionary(Of String, FastaSeq)
        Dim up = genomic.GetUpstreams(length)

        Return up.ToDictionary(Function(g) g.Key.ToUpper,
                               Function(g)
                                   Return g.Value
                               End Function)
    End Function

    Public Iterator Function PopulateMotifs(Optional leastN% = 10,
                                            Optional cleanMotif As Double = 0.5,
                                            Optional param As PopulatorParameter = Nothing) As IEnumerable(Of NamedCollection(Of SequenceMotif))
        For Each KO As String In Me.KO.Keys
            Dim motifs = PopulateMotifs(KO, leastN, cleanMotif, param) _
                .OrderByDescending(Function(m)
                                       Return m.score / m.seeds.MSA.Length
                                   End Function) _
                .ToArray

            Yield New NamedCollection(Of SequenceMotif) With {
                .name = KO,
                .value = motifs
            }
        Next
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Sub DumpSequence(KO$, path$)
        Call New FastaFile(KOUpstream(KO)).Save(path, Encoding.ASCII)
    End Sub

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Function PopulateMotifs(KO$,
                                   Optional leastN% = 10,
                                   Optional cleanMotif As Double = 0.5,
                                   Optional param As PopulatorParameter = Nothing) As IEnumerable(Of SequenceMotif)

        Return KOUpstream(KO).PopulateMotifs(param Or PopulatorParameter.DefaultParameter, leastN, cleanMotif)
    End Function
End Class
