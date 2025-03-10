﻿#Region "Microsoft.VisualBasic::b28a888558178071e821db030a34ae20, visualize\DataVisualizationExtensions\DEGPlot\GSVADiffBar.vb"

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

    '   Total Lines: 145
    '    Code Lines: 123 (84.83%)
    ' Comment Lines: 7 (4.83%)
    '    - Xml Docs: 42.86%
    ' 
    '   Blank Lines: 15 (10.34%)
    '     File Size: 5.61 KB


    ' Class GSVADiffBar
    ' 
    '     Constructor: (+1 Overloads) Sub New
    '     Sub: PlotInternal
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Microsoft.VisualBasic.ComponentModel.Ranges
Imports Microsoft.VisualBasic.Data.ChartPlots.Graphic
Imports Microsoft.VisualBasic.Data.ChartPlots.Graphic.Axis
Imports Microsoft.VisualBasic.Data.ChartPlots.Graphic.Canvas
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Imaging.Drawing2D
Imports Microsoft.VisualBasic.Math.LinearAlgebra
Imports Microsoft.VisualBasic.MIME.Html
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

Public Class GSVADiffBar : Inherits Plot

    ReadOnly diff As GSVADiff()
    ReadOnly posColor As Brush = "#104e8b".GetBrush
    ReadOnly negColor As Brush = "#7ccd7c".GetBrush
    ReadOnly notsigColor As Brush = "#cdc9c9".GetBrush
    ''' <summary>
    ''' abs cutoff value of the t-static value
    ''' </summary>
    ReadOnly cut As Double = 2

    Public Sub New(diff As GSVADiff(), theme As Theme)
        MyBase.New(theme)

        Me.diff = diff.OrderByDescending(Function(t) t.t).ToArray
    End Sub

    Protected Overrides Sub PlotInternal(ByRef g As IGraphics, canvas As GraphicsRegion)
        Dim env As CSSEnvirnment = g.LoadEnvironment
        Dim rect As Rectangle = canvas.PlotRegion(env)
        Dim stroke As New Pen(Brushes.Black, 3)
        Dim t As Double() = diff _
            .Select(Function(d) std.Abs(d.t)) _
            .Where(Function(n) Not n.IsNaNImaginary) _
            .Max _
            .SymmetricalRange _
            .CreateAxisTicks(ticks:=5)
        Dim scaleX = d3js.scale _
            .linear() _
            .domain(values:=t) _
            .range(integers:={rect.Left, rect.Right})
        Dim axisPen As Pen = env.GetPen(CSS.Stroke.TryParse(theme.axisStroke))
        Dim axisTickStroke As Pen = env.GetPen(CSS.Stroke.TryParse(theme.axisTickStroke))
        Dim axisTickFont As Font = env.GetFont(theme.axisTickCSS)
        Dim axis As New XAxis(
            plotRegion:=rect,
            scaler:=scaleX,
            ticks:=t.AsVector,
            pen:=axisPen,
            overridesTickLine:=-1,
            noTicks:=False,
            tickFormat:="F2",
            tickfont:=axisTickFont,
            tickColor:=Brushes.Black,
            label:="t-value of GSVA score",
            labelFont:=theme.axisLabelCSS,
            labelColor:=Brushes.Black,
            htmlLabel:=False,
            xRotate:=0
        )

        Call g.DrawRectangle(stroke, rect)
        Call axis.Draw(g, XAxisLayoutStyles.Bottom, rect.Bottom, Nothing)

        Dim dbar As Double = 5
        Dim dy As Double = (rect.Height - diff.Length * dbar) / diff.Length
        Dim x As Double
        Dim y As Double = rect.Top - dy
        Dim zeroX As Double = scaleX(0)
        Dim bar As Rectangle
        Dim color As Brush
        Dim labelFont As Font = env.GetFont(CSSFont.TryParse(theme.tagCSS))
        Dim labelPos As PointF
        Dim labelSize As SizeF
        Dim labelColor As Brush

        For Each line As GSVADiff In diff
            x = scaleX(line.t)
            y += dy + dbar
            labelSize = g.MeasureString(line.pathName, labelFont)

            If x > zeroX Then
                ' pos
                bar = New Rectangle(zeroX, y, x - zeroX, dy)
                color = posColor
                ' right align
                labelPos = New PointF(zeroX - labelSize.Width, y - labelSize.Height / 2)
            Else
                ' neg
                bar = New Rectangle(x, y, zeroX - x, dy)
                color = negColor
                ' left align
                labelPos = New PointF(zeroX, y - labelSize.Height / 2)
            End If

            If std.Abs(line.t) < cut Then
                color = notsigColor
                labelColor = notsigColor
            Else
                labelColor = Brushes.Black
            End If

            Call g.FillRectangle(color, bar)
            Call g.DrawString(line.pathName, labelFont, labelColor, labelPos)
        Next

        Dim cutline As New Pen(Brushes.White, 2) With {
            .DashStyle = DashStyle.Dash
        }

        Call g.DrawLine(cutline, New PointF(scaleX(-cut), rect.Top), New PointF(scaleX(-cut), rect.Bottom))
        Call g.DrawLine(cutline, New PointF(scaleX(cut), rect.Top), New PointF(scaleX(cut), rect.Bottom))

    End Sub
End Class
