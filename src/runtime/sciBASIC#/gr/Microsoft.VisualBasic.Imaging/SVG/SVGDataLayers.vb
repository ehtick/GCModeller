﻿#Region "Microsoft.VisualBasic::f4250eed450ea676bd97de4125c5cd32, gr\Microsoft.VisualBasic.Imaging\SVG\SVGDataLayers.vb"

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

    '   Total Lines: 69
    '    Code Lines: 35 (50.72%)
    ' Comment Lines: 22 (31.88%)
    '    - Xml Docs: 100.00%
    ' 
    '   Blank Lines: 12 (17.39%)
    '     File Size: 2.20 KB


    '     Class SVGDataLayers
    ' 
    '         Properties: styles
    ' 
    '         Constructor: (+2 Overloads) Sub New
    ' 
    '         Function: ApplyFilter
    ' 
    '         Sub: ApplyFilter, Clear
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports Microsoft.VisualBasic.Imaging.SVG.CSS
Imports Microsoft.VisualBasic.Imaging.SVG.XML
Imports Microsoft.VisualBasic.Language

Namespace SVG

    ''' <summary>
    ''' 使用<see cref="SvgGroup"/>图层的方式构建出一个完整的SVG模型
    ''' </summary>
    Public Class SVGDataLayers

        Protected Friend bg$
        Protected Friend size As Size

        ''' <summary>
        ''' the svg xml document data
        ''' </summary>
        Protected Friend svg As SvgDocument
        Protected Friend layers As New List(Of SvgContainer)

        ''' <summary>
        ''' <see cref="Filter.id"/>为字典的键名
        ''' </summary>
        Protected Friend filters As New Dictionary(Of String, Filter)

        Public ReadOnly Property styles As New List(Of String)

        Sub New(size As Size, Optional bg As String = "white")
            Me.svg = SvgDocument.Create.Size(size)
            Me.bg = bg
            Me.size = size
        End Sub

        Sub New(svg As SvgDocument)
            Me.svg = svg
            Me.bg = svg.Fill
            Me.size = New Size(svg.Width, svg.Height)
        End Sub

        ''' <summary>
        ''' reset
        ''' </summary>
        Public Sub Clear()
            layers.Clear()
            styles.Clear()
            filters.Clear()
            svg = SvgDocument.Create.Size(Size)
        End Sub

        ''' <summary>
        ''' Apply of the graphics filter to a specific layer
        ''' </summary>
        ''' <param name="zindex">图层的编号</param>
        ''' <param name="filter">filter的id编号</param>
        Public Sub ApplyFilter(zindex%, filter$)
            layers(zindex).filter = $"url(#{filter})"
        End Sub

        ''' <summary>
        ''' 通过元素选择器来设置滤镜，函数返回所有<paramref name="selector"/>查找成功的图层的编号
        ''' </summary>
        ''' <param name="selector"></param>
        ''' <param name="filter"></param>
        Public Iterator Function ApplyFilter(selector$, filter$) As IEnumerable(Of Integer)

        End Function
    End Class
End Namespace
