﻿#Region "Microsoft.VisualBasic::fd776c2bd9df9f5348df8b53ae5d19c7, modules\Knowledge_base\ncbi_kb\MeSH\MeshTree\Term.vb"

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
    '    Code Lines: 31 (56.36%)
    ' Comment Lines: 13 (23.64%)
    '    - Xml Docs: 84.62%
    ' 
    '   Blank Lines: 11 (20.00%)
    '     File Size: 1.66 KB


    '     Class Term
    ' 
    '         Properties: description, term, tree
    ' 
    '         Function: GetClass, isSimpleTree, ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports SMRUCC.genomics.ComponentModel.DBLinkBuilder

Namespace MeSH.Tree

    ' term              tree path
    ' Cerebellar Cortex;A08.186.211.132.810.428.200.212

    ''' <summary>
    ''' the mesh tree
    ''' </summary>
    Public Class Term : Inherits Synonym

        ''' <summary>
        ''' the mesh term name
        ''' </summary>
        ''' <returns></returns>
        Public Property term As String

        ''' <summary>
        ''' the tree path
        ''' </summary>
        ''' <returns></returns>
        Public Property tree As String()
        Public Property description As String

        Public ReadOnly Iterator Property category As IEnumerable(Of MeshCategory)
            Get
                If isSimpleTree() Then
                    Yield Reader.ParseCategory(tree.First)
                Else
                    For Each tree As String In Me.tree
                        Yield Reader.ParseCategory(tree)
                    Next
                End If
            End Get
        End Property

        Public Overrides Function ToString() As String
            If isSimpleTree() Then
                Return $"[{tree.JoinBy("->")}] {term}"
            End If

            Return $"[{accessionID}] {term} ({description})"
        End Function

        Private Function isSimpleTree() As Boolean
            Return accessionID.StringEmpty AndAlso tree.All(Function(ti) ti.IndexOf("."c) = -1)
        End Function

        Public Shared Function GetClass(tree As String) As MeshCategory
            Return Reader.ParseCategory(tree)
        End Function

    End Class
End Namespace
