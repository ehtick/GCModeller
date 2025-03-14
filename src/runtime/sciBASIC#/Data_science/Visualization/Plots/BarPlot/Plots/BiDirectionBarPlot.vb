﻿#Region "Microsoft.VisualBasic::7f3bb6c8adf7024ab7c11f63d68e83e0, Data_science\Visualization\Plots\BarPlot\Plots\BiDirectionBarPlot.vb"

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

    '   Total Lines: 187
    '    Code Lines: 141 (75.40%)
    ' Comment Lines: 11 (5.88%)
    '    - Xml Docs: 27.27%
    ' 
    '   Blank Lines: 35 (18.72%)
    '     File Size: 8.68 KB


    '     Class BiDirectionBarPlot
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Sub: PlotInternal
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports Microsoft.VisualBasic.Data.ChartPlots.BarPlot.Data
Imports Microsoft.VisualBasic.Data.ChartPlots.Graphic
Imports Microsoft.VisualBasic.Data.ChartPlots.Graphic.Axis
Imports Microsoft.VisualBasic.Data.ChartPlots.Graphic.Canvas
Imports Microsoft.VisualBasic.Data.ChartPlots.Graphic.Legend
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.MIME.Html.CSS
Imports Microsoft.VisualBasic.MIME.Html.Render
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
#End If

Namespace BarPlot

    ''' <summary>
    ''' compare two data set
    ''' </summary>
    Public Class BiDirectionBarPlot : Inherits Plot

        ReadOnly data As BiDirectionData
        ReadOnly colorFactor1 As SolidBrush
        ReadOnly colorFactor2 As SolidBrush

        Public Sub New(data As BiDirectionData, color1 As Color, color2 As Color, theme As Theme)
            MyBase.New(theme)

            Me.data = data
            Me.colorFactor1 = New SolidBrush(color1)
            Me.colorFactor2 = New SolidBrush(color2)
        End Sub

        Protected Overrides Sub PlotInternal(ByRef g As IGraphics, canvas As GraphicsRegion)
            Dim css As CSSEnvirnment = g.LoadEnvironment
            Dim rect As Rectangle = canvas.PlotRegion(css)
            Dim padding As PaddingLayout = PaddingLayout.EvaluateFromCSS(css, canvas.Padding)
            Dim dh As Double = rect.Height / data.size
            Dim barHeight As Double = dh * 0.7
            Dim labelFont As Font = CSS.GetFont(CSSFont.TryParse(theme.axisLabelCSS))
            Dim maxLen As Double = g.MeasureString(data.samples.Select(Function(d) d.tag).MaxLengthString, labelFont).Width
            Dim boxLeft As Double = rect.Left + maxLen
            Dim boxWidth As Double = rect.Right - boxLeft
            Dim center As Double = boxLeft + boxWidth / 2
            Dim dataValues = data.samples.Select(Function(d) d.data).IteratesALL.Range(scale:=1.125)
            Dim scale = d3js.scale.linear().domain(values:={0, dataValues.Max * 1.125}).range(values:={0.0, boxWidth / 2})

            rect = New Rectangle(boxLeft, rect.Top, rect.Width - (boxLeft - rect.Left), rect.Height)

            Call g.DrawRectangle(css.GetPen(Stroke.TryParse(theme.axisStroke)), rect)
            Call g.DrawLine(css.GetPen(Stroke.TryParse(theme.gridStrokeY)), New PointF(center, rect.Top), New PointF(center, rect.Bottom))

            Dim y As Double = rect.Top - dh + dh * 0.15
            Dim charWidth As Single = g.MeasureString("X", labelFont).Width

            ' draw main title
            Dim titleFont As Font = css.GetFont(CSSFont.TryParse(theme.mainCSS))
            Dim labelSize As SizeF = g.MeasureString(main, titleFont)
            Dim labelPos As New PointF With {
                .X = rect.Left + (rect.Width - labelSize.Width) / 2,
                .Y = (padding.Top - labelSize.Height) / 2
            }
            Dim tickLabelFont As Font = css.GetFont(CSSFont.TryParse(theme.axisTickCSS))

            Call g.DrawString(main, titleFont, Brushes.Black, labelPos)

            For i As Integer = 0 To data.size - 1
                Dim sample As BarDataSample = data(i)

                y += dh

                ' draw left
                Dim len1 = scale(sample.data(0))
                Dim bar As New Rectangle(center - len1, y, len1, barHeight)

                g.FillRectangle(colorFactor1, bar)
                labelSize = g.MeasureString(sample.data(0).ToString(theme.XaxisTickFormat), tickLabelFont)
                g.DrawString(sample.data(0).ToString(theme.XaxisTickFormat), tickLabelFont, Brushes.Black, New PointF(bar.Left - labelSize.Width, bar.Top + (bar.Height - labelSize.Height) / 2))

                Dim len2 = scale(sample.data(1))

                bar = New Rectangle(center, y, len2, barHeight)
                g.FillRectangle(colorFactor2, bar)
                labelSize = g.MeasureString(sample.data(1).ToString(theme.XaxisTickFormat), tickLabelFont)
                g.DrawString(sample.data(1).ToString(theme.XaxisTickFormat), tickLabelFont, Brushes.Black, New PointF(bar.Right, bar.Top + (bar.Height - labelSize.Height) / 2))

                ' draw label
                labelSize = g.MeasureString(sample.tag, labelFont)
                labelPos = New Point With {
                    .X = boxLeft - charWidth - labelSize.Width,
                    .Y = y + (dh - labelSize.Height) / 2
                }

                Call g.DrawString(sample.tag, labelFont, Brushes.Black, labelPos)
            Next

            ' draw axis
            Dim ticks As Double() = data.samples.Select(Function(d) d.data).IteratesALL.CreateAxisTicks(ticks:=3)

            y = rect.Bottom + 10
            labelFont = css.GetFont(CSSFont.TryParse(theme.axisTickCSS))
            labelSize = g.MeasureString(0, labelFont)

            ' draw ZERO
            labelPos = New Point(center - labelSize.Width / 2, y)
            g.DrawString(0, labelFont, Brushes.Black, labelPos)

            Dim offset As Double
            Dim x As Double

            labelSize = g.MeasureString("0", labelFont)
            x = center - labelSize.Width / 2

            labelPos = New Point(x, y)
            g.DrawString(0, labelFont, Brushes.Black, labelPos)
            g.DrawLine(Pens.Black, New PointF(x, y), New PointF(x, rect.Bottom))

            For Each tick As Double In ticks
                If std.Abs(tick) < 0.1 Then
                    Continue For
                End If

                offset = scale(tick)
                labelSize = g.MeasureString(tick.ToString(theme.XaxisTickFormat), labelFont)

                ' left
                x = center + offset - labelSize.Width / 2
                labelPos = New Point(x, y)
                g.DrawString(tick.ToString(theme.XaxisTickFormat), labelFont, Brushes.Black, labelPos)
                x = center + offset
                g.DrawLine(Pens.Black, New PointF(x, y), New PointF(x, rect.Bottom))

                ' right
                x = center - offset - labelSize.Width / 2
                labelPos = New Point(x, y)
                g.DrawString(tick.ToString(theme.XaxisTickFormat), labelFont, Brushes.Black, labelPos)
                g.DrawLine(Pens.Black, New PointF(center - offset, y), New PointF(center - offset, rect.Bottom))
            Next

            labelFont = css.GetFont(CSSFont.TryParse(theme.axisLabelCSS))
            labelSize = g.MeasureString(xlabel, labelFont)
            labelPos = New Point With {
                .X = rect.Left + (rect.Width - labelSize.Width) / 2,
                .Y = rect.Bottom + labelSize.Height
            }

            Call g.DrawString(xlabel, labelFont, Brushes.Black, labelPos)

            ' draw legends
            Dim legends As LegendObject() = {
                New LegendObject With {.color = colorFactor1.Color.ToHtmlColor, .fontstyle = theme.legendLabelCSS, .style = LegendStyles.Square, .title = data.Factor1},
                New LegendObject With {.color = colorFactor2.Color.ToHtmlColor, .fontstyle = theme.legendLabelCSS, .style = LegendStyles.Square, .title = data.Factor2}
            }
            Dim plotRect = canvas.PlotRegion(css)

            theme.legendLayout = New Absolute With {
                .x = plotRect.Right + 20,
                .y = plotRect.Top + 20
            }

            Call DrawLegends(g, legends, showBorder:=False, canvas:=canvas)
        End Sub
    End Class
End Namespace
