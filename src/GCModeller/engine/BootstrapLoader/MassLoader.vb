﻿#Region "Microsoft.VisualBasic::c106e51e42755d0ed993ca3fefeddfb1, engine\BootstrapLoader\MassLoader.vb"

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

'   Total Lines: 57
'    Code Lines: 40 (70.18%)
' Comment Lines: 7 (12.28%)
'    - Xml Docs: 57.14%
' 
'   Blank Lines: 10 (17.54%)
'     File Size: 2.41 KB


'     Class MassLoader
' 
'         Properties: massTable
' 
'         Constructor: (+1 Overloads) Sub New
'         Sub: doMassLoadingOn
' 
' 
' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Linq
Imports SMRUCC.genomics.GCModeller.ModellingEngine.BootstrapLoader.Engine
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Dynamics.Core
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Model.Cellular
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Model.Cellular.Molecule
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Model.Cellular.Process

Namespace ModelLoader

    Public Class MassLoader

        Public ReadOnly Property massTable As MassTable

        ''' <summary>
        ''' link mapping from protein to protein complex
        ''' </summary>
        Public ReadOnly proteinComplex As New Dictionary(Of String, String)

        Sub New(loader As Loader)
            massTable = loader.massTable
        End Sub

        ''' <summary>
        ''' create mass table from the virtual cell model
        ''' </summary>
        ''' <param name="cell"></param>
        Public Sub doMassLoadingOn(cell As CellularModule)
            ' 在这里需要首选构建物质列表
            ' 否则下面的转录和翻译过程的构建会出现找不到物质因子对象的问题
            For Each reaction As Reaction In cell.Phenotype.fluxes
                For Each compound In reaction.AllCompounds
                    If Not massTable.Exists(compound) Then
                        Call massTable.AddNew(compound, MassRoles.compound)
                    End If
                Next
            Next

            Dim complexID As String

            ' 20241113 protein id maybe duplicated, due to the reason of
            ' some gene translate the protein with identicial protein sequence data
            ' so reference to the identical protein model
            For Each complex As Protein In cell.Phenotype.proteins
                complexID = massTable.AddNew(complex.ProteinID & ".complex", MassRoles.protein)

                If proteinComplex.ContainsKey(complex.ProteinID) Then
                    Dim warn As String = $"duplicated protein id: '{complex.ProteinID}' was found."

                    Call warn.Warning
                    Call VBDebugger.EchoLine("[warn] " & warn)
                Else
                    Call proteinComplex.Add(complex.ProteinID, complexID)
                End If
            Next
        End Sub

    End Class
End Namespace
