﻿#Region "Microsoft.VisualBasic::83526ae37f84846a13f15c4f807f324d, visualize\Cytoscape\Cytoscape\Session\cyTables.vb"

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

    '   Total Lines: 35
    '    Code Lines: 24 (68.57%)
    ' Comment Lines: 3 (8.57%)
    '    - Xml Docs: 100.00%
    ' 
    '   Blank Lines: 8 (22.86%)
    '     File Size: 1.17 KB


    '     Class cyTables
    ' 
    '         Properties: virtualColumns
    ' 
    '         Function: GenericEnumerator
    ' 
    '     Class virtualColumn
    ' 
    '         Properties: immutable, name, sourceColumn, sourceJoinKey, sourceTable
    '                     targetJoinKey, targetTable
    ' 
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.Linq

Namespace Session

    Public Class cyTables : Implements Enumeration(Of virtualColumn)

        Public Property virtualColumns As virtualColumn()

        Public Iterator Function GenericEnumerator() As IEnumerator(Of virtualColumn) Implements Enumeration(Of virtualColumn).GenericEnumerator
            For Each item In virtualColumns
                Yield item
            Next
        End Function
    End Class

    ''' <summary>
    ''' the column data
    ''' </summary>
    Public Class virtualColumn

        <XmlAttribute> Public Property name As String
        <XmlAttribute> Public Property sourceColumn As String
        <XmlAttribute> Public Property sourceTable As String
        <XmlAttribute> Public Property sourceJoinKey As String
        <XmlAttribute> Public Property targetTable As String
        <XmlAttribute> Public Property targetJoinKey As String
        <XmlAttribute> Public Property immutable As Boolean

        Public Overrides Function ToString() As String
            Return sourceTable
        End Function

    End Class
End Namespace
