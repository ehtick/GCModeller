﻿#Region "Microsoft.VisualBasic::044a7ba99bd28582051008d265c4a415, R#\cytoscape_toolkit\dunnart.vb"

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

    '   Total Lines: 55
    '    Code Lines: 46 (83.64%)
    ' Comment Lines: 0 (0.00%)
    '    - Xml Docs: 0.00%
    ' 
    '   Blank Lines: 9 (16.36%)
    '     File Size: 2.38 KB


    ' Module dunnart
    ' 
    '     Function: CreateModel, graphObject, OptmizeGraph
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Data.visualize.Network.Graph
Imports Microsoft.VisualBasic.Imaging.Drawing2D.Colors
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports SMRUCC.genomics.Assembly.KEGG.DBGET.bGetObject
Imports SMRUCC.genomics.Model.Network.KEGG.Dunnart

<Package("dunnart")>
Module dunnart

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    <ExportAPI("as.graphObj")>
    Public Function graphObject(network As NetworkGraph,
                                Optional colorSet As String = "Paired:c12",
                                Optional group_key As String = "map",
                                Optional fillOpacity As Double = 0.5,
                                Optional lighten As Double = 0.1) As GraphObject

        Dim colors As Color() = Designer.GetColors(colorSet)
        Dim g = network.FromNetwork(colors, group_key, fillOpacity, lighten)

        Return g
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    <ExportAPI("network_map")>
    Public Function CreateModel(template As NetworkGraph, maps As Pathway(),
                                Optional desc As Boolean = False,
                                Optional colorSet As String = "Paired:c12",
                                Optional fillOpacity As Double = 0.5,
                                Optional lighten As Double = 0.1,
                                Optional isConnected As Boolean = True) As GraphObject

        Dim colors As Color() = Designer.GetColors(colorSet)

        Return template.CreateModel(
            maps, colors, desc,
            fillOpacity:=fillOpacity,
            lighten:=lighten,
            isConnected:=isConnected
        )
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    <ExportAPI("optmize")>
    Public Function OptmizeGraph(template As NetworkGraph,
                                 Optional optmize_iterations As Integer = 100,
                                 Optional lower_degrees As Integer = 3,
                                 Optional lower_adjcents As Integer = 2) As NetworkGraph

        Return template.OptmizeGraph(optmize_iterations, lower_degrees, lower_adjcents)
    End Function
End Module
