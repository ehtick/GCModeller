﻿#Region "Microsoft.VisualBasic::99f15150b711a670fb40da36ceb27881, analysis\RNA-Seq\WGCNA\WGCNAModules.vb"

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

    '   Total Lines: 33
    '    Code Lines: 29 (87.88%)
    ' Comment Lines: 0 (0.00%)
    '    - Xml Docs: 0.00%
    ' 
    '   Blank Lines: 4 (12.12%)
    '     File Size: 1.49 KB


    ' Module WGCNAModules
    ' 
    '     Function: LoadModules, ModsView
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports SMRUCC.genomics.Analysis.RNA_Seq.RTools.WGCNA.Network

<Package("WGCNA.Modules", Publisher:="xie.guigang@gcmodeller.org", Category:=APICategories.ResearchTools)>
Public Module WGCNAModules

    <ExportAPI("Load.Modules")>
    Public Function LoadModules(path As String) As CExprMods()
        Dim tokens As String() = IO.File.ReadAllLines(path).Skip(1).ToArray
        Dim resultSet As CExprMods() = tokens.Select(Function(line) CExprMods.CreateObject(line)).ToArray

        Return resultSet
    End Function

    <ExportAPI("Mods.View")>
    <Extension>
    Public Function ModsView(mods As IEnumerable(Of CExprMods)) As Dictionary(Of String, String())
        Dim groups = (From entity As CExprMods
                      In mods
                      Select entity
                      Group entity By entity.nodesPresent Into Group).ToArray
        Dim resultSet As Dictionary(Of String, String()) =
            groups.ToDictionary(Function([mod]) [mod].nodesPresent,
                                Function([mod])
                                    Return [mod].Group _
                                        .Select(Function(entity) entity.nodeName) _
                                        .ToArray
                                End Function)
        Return resultSet
    End Function
End Module
