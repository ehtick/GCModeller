﻿#Region "Microsoft.VisualBasic::9aa95ad49be33060aa13eb97c24bf8ca, visualize\DataVisualizationExtensions\DEGPlot\ClassChanges.vb"

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

    '   Total Lines: 172
    '    Code Lines: 144 (83.72%)
    ' Comment Lines: 2 (1.16%)
    '    - Xml Docs: 0.00%
    ' 
    '   Blank Lines: 26 (15.12%)
    '     File Size: 7.09 KB


    ' Class ClassChanges
    ' 
    '     Constructor: (+1 Overloads) Sub New
    '     Sub: PlotInternal
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.ComponentModel.Ranges.Model
Imports Microsoft.VisualBasic.Data.ChartPlots.Graphic
Imports Microsoft.VisualBasic.Data.ChartPlots.Graphic.Axis
Imports Microsoft.VisualBasic.Data.ChartPlots.Graphic.Canvas
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D
Imports Microsoft.VisualBasic.Imaging.Drawing2D.Colors
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.Html.CSS
Imports Microsoft.VisualBasic.MIME.Html.Render
Imports SMRUCC.genomics.GCModeller.Workbench.ExperimentDesigner
Imports std = System.Math

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

Public Class ClassChanges : Inherits Plot

    ReadOnly degClass As NamedCollection(Of DEGModel)()
    ReadOnly radius As DoubleRange

    Public Sub New(deg As IEnumerable(Of DEGModel), orderByClass$, radius As DoubleRange, theme As Theme)
        MyBase.New(theme)

        Me.radius = radius
        Me.degClass = deg _
            .GroupBy(Function(a) a.class) _
            .Select(Function(group)
                        Return New NamedCollection(Of DEGModel)(group.Key, group.ToArray)
                    End Function) _
            .ToArray

        If orderByClass <> "none" Then
            If orderByClass = "asc" Then
                Me.degClass = Me.degClass.OrderBy(Function(a) a.Count).ToArray
            Else
                Me.degClass = Me.degClass.OrderByDescending(Function(a) a.Count).ToArray
            End If
        End If
    End Sub

    Protected Overrides Sub PlotInternal(ByRef g As IGraphics, canvas As GraphicsRegion)
        Dim xTicks As Double() = degClass _
            .Select(Function(d) d.Select(Function(gi) gi.logFC)) _
            .IteratesALL _
            .Range _
            .CreateAxisTicks
        Dim css As CSSEnvirnment = g.LoadEnvironment
        Dim dpi As Integer = g.Dpi
        Dim plotregion As Rectangle = canvas.PlotRegion(css)
        Dim y As Double = degClass.Length
        Dim x As Double
        Dim axisStroke As Pen = css.GetPen(Stroke.TryParse(theme.axisStroke))
        Dim tickStroke As Pen = css.GetPen(Stroke.TryParse(theme.axisTickStroke))
        Dim a As PointF
        Dim b As PointF
        Dim xscale = d3js.scale.linear.domain(values:=xTicks).range(integers:={plotregion.Left, plotregion.Right})
        Dim labelText As String
        Dim labelSize As SizeF
        Dim labelFont As Font = css.GetFont(CSSFont.TryParse(theme.axisTickCSS))
        Dim tickPadding As Double = g.MeasureString("0", labelFont).Height / 2
        Dim dh As Double = plotregion.Height / degClass.Length
        Dim colors As Color() = Designer _
            .GetColors(theme.colorSet, degClass.Length) _
            .ToArray

        ' X
        a = New PointF(plotregion.Left, plotregion.Bottom)
        b = New PointF(plotregion.Right, plotregion.Bottom)

        Call g.DrawLine(axisStroke, a, b)

        For Each tick As Double In xTicks
            x = xscale(tick)
            a = New PointF(x, plotregion.Bottom)
            b = New PointF(x, plotregion.Bottom + tickPadding)
            labelText = tick.ToString(theme.XaxisTickFormat)
            labelSize = g.MeasureString(labelText, labelFont)
            x = x - labelSize.Width / 2
            y = plotregion.Bottom + tickPadding

            Call g.DrawLine(tickStroke, a, b)
            Call g.DrawString(labelText, labelFont, Brushes.Black, x, y)
        Next

        labelFont = css.GetFont(CSSFont.TryParse(theme.axisLabelCSS))
        labelSize = g.MeasureString(Me.xlabel, labelFont)
        x = plotregion.Left + (plotregion.Width - labelSize.Width) / 2
        y = plotregion.Bottom + tickPadding * 3

        g.DrawString(Me.xlabel, labelFont, Brushes.Black, x, y)

        If degClass.Any(Function(gi) gi.Any(Function(d) d.logFC < 0)) Then
            Dim zeroX As Double = xscale(0)

            a = New PointF(zeroX, plotregion.Bottom)
            b = New PointF(zeroX, plotregion.Top)

            Call g.DrawLine(New Pen(Brushes.Gray, 5) With {.DashStyle = DashStyle.Dash}, a, b)
        End If

        ' Y
        a = New PointF(plotregion.Left, plotregion.Bottom)
        b = New PointF(plotregion.Left, plotregion.Top)

        Call g.DrawLine(axisStroke, a, b)

        Dim i As Integer = 1
        Dim radius As Double
        Dim color As Color
        Dim tagFont As Font = css.GetFont(CSSFont.TryParse(theme.tagCSS))
        Dim radiusScales As DoubleRange = degClass _
            .Select(Function(cls)
                        Return cls.Select(Function(d) -Math.Log10(d.pvalue))
                    End Function) _
            .IteratesALL _
            .Range

        labelFont = css.GetFont(CSSFont.TryParse(theme.axisTickCSS))

        For Each [class] As NamedCollection(Of DEGModel) In degClass
            labelText = [class].name
            labelSize = g.MeasureString(labelText, labelFont)
            y = plotregion.Top + i * dh - dh / 2
            a = New PointF(plotregion.Left, y)
            b = New PointF(plotregion.Left - tickPadding, y)
            color = colors(i - 1)

            Call g.DrawLine(tickStroke, a, b)
            Call g.DrawString(labelText, labelFont, Brushes.Black, plotregion.Left - tickPadding - labelSize.Width, y - labelSize.Height / 2)

            For Each deg As DEGModel In [class]
                x = xscale(deg.logFC)
                radius = -Math.Log10(deg.pvalue)

                Call g.DrawCircle(New PointF(x, y), color, Pens.Black, radiusScales.ScaleMapping(radius, Me.radius))

                If Not deg.label.StringEmpty Then
                    Call g.DrawString(deg.label, tagFont, Brushes.Black, x, y)
                End If
            Next

            i += 1
        Next
    End Sub
End Class
