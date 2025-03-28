﻿#Region "Microsoft.VisualBasic::2585480b81dfa4c4380a6a37fb32223f, data\STRING\tsv\Coordinates.vb"

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

    '   Total Lines: 20
    '    Code Lines: 13 (65.00%)
    ' Comment Lines: 4 (20.00%)
    '    - Xml Docs: 100.00%
    ' 
    '   Blank Lines: 3 (15.00%)
    '     File Size: 812 B


    ' Class Coordinates
    ' 
    '     Properties: annotation, color, node, x_position, y_position
    ' 
    '     Function: ToString
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Data.Framework.StorageProvider.Reflection
Imports Microsoft.VisualBasic.Imaging.LayoutModel

''' <summary>
''' Tsv table reader for string-db export result ``string_network_coordinates.txt``.
''' (这个表格文件定义的是网络的节点的位置和蛋白的注释信息)
''' </summary>
Public Class Coordinates : Implements ILayoutCoordinate

    <Column("#node")>
    Public Property node As String Implements ILayoutCoordinate.ID
    Public Property x_position As Double Implements ILayoutCoordinate.X
    Public Property y_position As Double Implements ILayoutCoordinate.Y
    Public Property color As String
    Public Property annotation As String

    Public Overrides Function ToString() As String
        Return annotation
    End Function
End Class
