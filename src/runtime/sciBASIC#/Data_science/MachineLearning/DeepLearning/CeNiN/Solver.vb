﻿#Region "Microsoft.VisualBasic::28daf6e7f24459077fa1676589aa5ea5, Data_science\MachineLearning\DeepLearning\CeNiN\Solver.vb"

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

    '   Total Lines: 88
    '    Code Lines: 72 (81.82%)
    ' Comment Lines: 0 (0.00%)
    '    - Xml Docs: 0.00%
    ' 
    '   Blank Lines: 16 (18.18%)
    '     File Size: 3.51 KB


    '     Module Solver
    ' 
    '         Function: DetectObject
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.MachineLearning.Convolutional.ImageProcessor
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

Namespace Convolutional

    Public Module Solver

        <Extension>
        Public Function DetectObject(cnn As CeNiN,
                                     image As Bitmap,
                                     Optional resize As ResizingMethod = ResizingMethod.ZeroPad,
                                     Optional dev As TextWriter = Nothing) As NamedValue(Of Double)()

            Dim currentLayer As Layer = cnn.inputLayer.setInput(image, resizingMethod:=resize)
            Dim i As Integer = 0
            Dim start As Long = App.NanoTime

            dev = dev Or App.StdOut

            While currentLayer.nextLayer IsNot Nothing
                If i = 0 Then
                    dev.WriteLine("Loading bitmap data...")
                Else
                    dev.WriteLine("Layer " & i & " (" & currentLayer.type.Description & ") ...")
                End If

                currentLayer = currentLayer.feedNext().nextLayer
                i += 1

                Call dev.Flush()
            End While

            Dim OutputLayer As Output = CType(currentLayer, Output)

            Call dev.WriteLine("Finished in " & TimeSpan.FromTicks(App.NanoTime - start).FormatTime & " seconds")

            Dim Decision As String = OutputLayer.getDecision()
            Dim HLine As String = New String("-"c, 100)

            Call dev.WriteLine(HLine, "")
            Call dev.WriteLine("THE HIGHEST 3 PROBABILITIES: ", "")

            For i = 0 To 2
                Call dev.WriteLine(" #" & (i + 1) & "   " & OutputLayer.sortedClasses(i) & " (" & std.Round(OutputLayer.probabilities(i), 3) & ")", "")
            Next

            Call dev.WriteLine(HLine, "")
            Call dev.WriteLine("DECISION: " & Decision)
            Call dev.WriteLine(HLine, "")

            Return OutputLayer.sortedClasses _
                .Select(Function(tag, j)
                            Return New NamedValue(Of Double)(tag, OutputLayer.probabilities(j))
                        End Function) _
                .ToArray
        End Function
    End Module
End Namespace
