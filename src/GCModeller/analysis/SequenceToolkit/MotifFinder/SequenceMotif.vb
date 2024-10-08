﻿#Region "Microsoft.VisualBasic::ebe9c0ca721ad8d126eece75f266743e, analysis\SequenceToolkit\MotifFinder\SequenceMotif.vb"

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

    '   Total Lines: 52
    '    Code Lines: 28 (53.85%)
    ' Comment Lines: 16 (30.77%)
    '    - Xml Docs: 100.00%
    ' 
    '   Blank Lines: 8 (15.38%)
    '     File Size: 1.35 KB


    ' Class SequenceMotif
    ' 
    '     Properties: alignments, AverageScore, RSD, seeds, SignificantSites
    '                 width
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.Math
Imports SMRUCC.genomics.Analysis.SequenceTools.MSA

Public Class SequenceMotif : Inherits Probability

    Public Property seeds As MSAOutput

    ''' <summary>
    ''' the alignment score vector of current motif PWM with the source seeds
    ''' </summary>
    ''' <returns></returns>
    <XmlAttribute>
    Public Property alignments As Double()

    Public ReadOnly Property RSD As Double
        Get
            Return alignments.RSD
        End Get
    End Property

    ''' <summary>
    ''' the length of the MSA alignment
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property width As Integer
        Get
            Return seeds.MSA(Scan0).Length
        End Get
    End Property

    ''' <summary>
    ''' score / motif with
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property AverageScore As Double
        Get
            Return score / seeds.MSA.Length
        End Get
    End Property

    ''' <summary>
    ''' get number of the conserved sites
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SignificantSites As Integer
        Get
            Return region.Where(Function(r) r.isConserved).Count
        End Get
    End Property

End Class
