﻿#Region "Microsoft.VisualBasic::f8fd953668705fe91043e8f8433077ca, Data_science\Mathematica\Math\Math\Distributions\BinBox\Violin.vb"

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

    '   Total Lines: 96
    '    Code Lines: 66 (68.75%)
    ' Comment Lines: 15 (15.62%)
    '    - Xml Docs: 100.00%
    ' 
    '   Blank Lines: 15 (15.62%)
    '     File Size: 3.52 KB


    '     Class Violin
    ' 
    '         Properties: mean, nsize, quartile, range, sd
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: Bandwidth, KDE, PercentileValue, ViolinDensity
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ComponentModel.Ranges.Model
Imports Microsoft.VisualBasic.Math.LinearAlgebra
Imports Microsoft.VisualBasic.Math.Quantile
Imports std = System.Math

Namespace Distributions.BinBox

    Public Class Violin

        ReadOnly data As Double()

        Public ReadOnly Property sd As Double
        Public ReadOnly Property mean As Double

        Public ReadOnly Property nsize As Integer
        Public ReadOnly Property quartile As DataQuartile
        ''' <summary>
        ''' upper and lower bound range of the plot
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property range As DoubleRange

        Sub New(x As IEnumerable(Of Double))
            data = x.NAremove.OrderBy(Function(xi) xi).ToArray

            If data.Any Then
                mean = data.Average
                sd = data.SD
                nsize = data.Length
                quartile = data.Quartile
                range = New DoubleRange(
                    data.Min - quartile.IQR * 1.5,
                    data.Max + quartile.IQR * 1.5
                )
            End If
        End Sub

        ''' <summary>
        ''' generates the plot data
        ''' </summary>
        ''' <param name="nPoints"></param>
        ''' <returns></returns>
        Public Iterator Function ViolinDensity(Optional nPoints As Integer = 100) As IEnumerable(Of Density)
            Dim binSize = (range.Max - range.Min) / (nPoints - 1)
            Dim bandwidth = Me.Bandwidth
            Dim axis = Enumerable.Range(0, nPoints).Select(Function(i) range.Min + i * binSize).ToArray
            Dim densities = Me.KDE(axis, bandwidth)

            For i As Integer = 0 To densities.Length - 1
                Yield New Density With {
                    .axis = axis(i),
                    .density = densities(i)
                }
            Next
        End Function

        ''' <summary>
        ''' Silverman带宽计算法则
        ''' </summary>
        Private Function Bandwidth() As Double
            Dim iqr = PercentileValue(0.75) - PercentileValue(0.25)
            Dim sigma = std.Min(sd, iqr / 1.34)
            Return 0.9 * sigma * std.Pow(data.Count, -0.2)
        End Function

        Private Function PercentileValue(percentile As Double) As Double
            Dim index = percentile * (nsize - 1)
            Dim integerPart = CInt(std.Floor(index))
            Dim fractionalPart = index - integerPart

            If integerPart >= nsize - 1 Then
                Return data(nsize - 1)
            End If

            Return data(integerPart) * (1 - fractionalPart) + data(integerPart + 1) * fractionalPart
        End Function

        ''' <summary>
        ''' 计算核密度估计
        ''' </summary>
        Private Function KDE(yValues As Double(), bandwidth As Double) As Double()
            Dim factor = 1.0 / (nsize * bandwidth)
            Dim sqrt2Pi = std.Sqrt(2 * std.PI)

            Return yValues _
                .Select(Function(y)
                            Return (Aggregate x As Double
                                    In data
                                    Let u = (y - x) / bandwidth
                                    Let var = std.Exp(-0.5 * u * u) / sqrt2Pi
                                    Into Sum(var)) * factor
                        End Function) _
                .ToArray()
        End Function
    End Class
End Namespace
