﻿#Region "Microsoft.VisualBasic::d5b29951421bad29453779336b43c412, data\RCSB PDB\PDB\AminoAcid.vb"

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

    '   Total Lines: 44
    '    Code Lines: 30 (68.18%)
    ' Comment Lines: 10 (22.73%)
    '    - Xml Docs: 100.00%
    ' 
    '   Blank Lines: 4 (9.09%)
    '     File Size: 1.44 KB


    ' Class AminoAcid
    ' 
    '     Properties: AA_ID, Atoms, Carbon, Index
    ' 
    '     Function: SequenceGenerator
    ' 
    ' /********************************************************************************/

#End Region

Imports SMRUCC.genomics.Data.RCSB.PDB.Keywords

''' <summary>
''' 氨基酸残基
''' </summary>
''' <remarks></remarks>
Public Class AminoAcid

    Public Property Index As Integer
    Public Property AA_ID As String
    Public Property Atoms As Keywords.AtomUnit()

    ''' <summary>
    ''' 中心的碳原子
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Carbon As Keywords.AtomUnit
        Get
            Dim CLQuery = (From Atom In Atoms Where String.Equals(Atom.Atom, "C") Select Atom).FirstOrDefault
            If CLQuery Is Nothing Then
                Return Atoms.First
            Else
                Return CLQuery
            End If
        End Get
    End Property

    Public Shared Function SequenceGenerator(Atoms As Keywords.Atom) As AminoAcid()
        Dim res = (From atom As AtomUnit
                   In Atoms
                   Select atom
                   Group atom By atom.AA_IDX Into Group).ToArray
        Dim LQuery = (From item In res
                      Select AA = New AminoAcid With {
                          .Index = item.AA_IDX,
                          .AA_ID = item.Group.First.AA_ID,
                          .Atoms = item.Group.ToArray
                      }
                      Order By AA.Index Ascending).ToArray
        Return LQuery
    End Function
End Class
