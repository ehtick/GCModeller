﻿#Region "Microsoft.VisualBasic::3f65f6477b3b3dc5579be1130a25227c, models\Networks\KEGG\ReactionNetwork\NetworkExpansion.vb"

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

    '   Total Lines: 133
    '    Code Lines: 103 (77.44%)
    ' Comment Lines: 18 (13.53%)
    '    - Xml Docs: 77.78%
    ' 
    '   Blank Lines: 12 (9.02%)
    '     File Size: 6.20 KB


    '     Module NetworkExpansion
    ' 
    '         Function: doNetworkExtension
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Data.visualize.Network.FileStream.Generic
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq

#If NET48 Then
Imports Pen = System.Drawing.Pen
Imports Pens = System.Drawing.Pens
Imports Brush = System.Drawing.Brush
Imports Font = System.Drawing.Font
Imports Brushes = System.Drawing.Brushes
Imports SolidBrush = System.Drawing.SolidBrush
Imports DashStyle = System.Drawing.Drawing2D.DashStyle
Imports Image = System.Drawing.Image
Imports Bitmap = System.Drawing.Bitmap
Imports GraphicsPath = System.Drawing.Drawing2D.GraphicsPath
Imports FontStyle = System.Drawing.FontStyle
#Else
Imports Pen = Microsoft.VisualBasic.Imaging.Pen
Imports Pens = Microsoft.VisualBasic.Imaging.Pens
Imports Brush = Microsoft.VisualBasic.Imaging.Brush
Imports Font = Microsoft.VisualBasic.Imaging.Font
Imports Brushes = Microsoft.VisualBasic.Imaging.Brushes
Imports SolidBrush = Microsoft.VisualBasic.Imaging.SolidBrush
Imports DashStyle = Microsoft.VisualBasic.Imaging.DashStyle
Imports Image = Microsoft.VisualBasic.Imaging.Image
Imports Bitmap = Microsoft.VisualBasic.Imaging.Bitmap
Imports GraphicsPath = Microsoft.VisualBasic.Imaging.GraphicsPath
Imports FontStyle = Microsoft.VisualBasic.Imaging.FontStyle
#End If

Namespace ReactionNetwork

    ''' <summary>
    ''' Contains algorithm code for KEGG compound reaction network expansion 
    ''' </summary>
    Public Module NetworkExpansion

        ''' <summary>
        ''' Extends network by using other compounds as intermediary
        ''' </summary>
        ''' <param name="cpdGroups"></param>
        ''' <param name="a"></param>
        ''' <param name="b"></param>
        ''' <param name="gray"></param>
        ''' <param name="addEdge"></param>
        ''' <param name="nodes"></param>
        ''' <param name="reactionIDlist"></param>
        ''' <returns></returns>
        <Extension>
        Friend Iterator Function doNetworkExtension(cpdGroups As Dictionary(Of String, String()),
                                                    a As Node, b As Node,
                                                    gray As SolidBrush,
                                                    addEdge As Action(Of Edge),
                                                    nodes As CompoundNodeTable,
                                                    reactionIDlist As List(Of String)) As IEnumerable(Of Node)

            Dim indexA = cpdGroups(a.label).Indexing
            Dim indexB = cpdGroups(b.label).Indexing

            For Each x In cpdGroups.Where(Function(compound)
                                              ' C00001 是水,很多代谢过程都存在的
                                              ' 在这里就没有必要添加进来了
                                              Return Not compound.Key Like nodes.ignores
                                          End Function)
                Dim list As String() = x.Value

                If list.Any(Function(r) indexA(r) > -1) AndAlso list.Any(Function(r) indexB(r) > -1) Then

                    ' 这是一个间接的拓展链接，将其加入到边列表之中
                    ' X也添加进入拓展节点列表之中
                    Yield New Node With {
                        .label = x.Key,
                        .data = New NodeData With {
                            .color = gray,
                            .label = x.Key,
                            .origID = x.Key,
                            .Properties = New Dictionary(Of String, String) From {
                                {"name", x.Key},
                                {"is_extended", True},
                                {NamesOf.REFLECTION_ID_MAPPING_NODETYPE, list.JoinBy(Delimiter)}
                            }
                        }
                    }

                    Dim populate = Iterator Function()
                                       Yield (a.label, indexA)
                                       Yield (b.label, indexB)
                                   End Function

                    For Each n As (ID$, list As Index(Of String)) In populate()
                        Dim interactions As String() = n.list.Objects.Intersect(list).ToArray
                        Dim edge As New Edge With {
                            .U = nodes(n.ID),
                            .V = nodes(x.Key),
                            .data = New EdgeData With {
                                .length = interactions.Length,
                                .Properties = New Dictionary(Of String, String) From {
                                    {NamesOf.REFLECTION_ID_MAPPING_INTERACTION_TYPE, interactions.JoinBy("|")}
                                }
                            }
                        }

                        If edge.U Is Nothing Then
                            edge.U = New Node With {
                                .label = n.ID,
                                .data = New NodeData With {
                                    .label = n.ID,
                                    .origID = n.ID
                                }
                            }.DoCall(AddressOf nodes.add)
                        End If
                        If edge.V Is Nothing Then
                            edge.V = New Node With {
                                .label = x.Key,
                                .data = New NodeData With {
                                    .label = x.Key,
                                    .origID = x.Key
                                }
                            }.DoCall(AddressOf nodes.add)
                        End If

                        Call addEdge(edge)
                        Call reactionIDlist.AddRange(interactions)
                    Next
                End If
            Next
        End Function
    End Module
End Namespace
