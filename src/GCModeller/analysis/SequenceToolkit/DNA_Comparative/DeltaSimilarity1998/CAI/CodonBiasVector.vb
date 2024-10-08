﻿#Region "Microsoft.VisualBasic::171eb4a8236469eb4602f0dba86eca15, analysis\SequenceToolkit\DNA_Comparative\DeltaSimilarity1998\CAI\CodonBiasVector.vb"

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

    '   Total Lines: 57
    '    Code Lines: 29 (50.88%)
    ' Comment Lines: 20 (35.09%)
    '    - Xml Docs: 95.00%
    ' 
    '   Blank Lines: 8 (14.04%)
    '     File Size: 1.89 KB


    '     Structure CodonBiasVector
    ' 
    '         Function: EuclideanNormalization, (+2 Overloads) PopulateTriples, ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.Math.Correlations
Imports SMRUCC.genomics.SequenceModel

Namespace DeltaSimilarity1998.CAI

    ''' <summary>
    ''' triple vector
    ''' </summary>
    Public Structure CodonBiasVector

        ''' <summary>
        ''' 三联体密码子
        ''' </summary>
        <XmlAttribute> Dim Codon As String
        <XmlAttribute> Dim XY#, YZ#, XZ#

        ''' <summary>
        ''' 对Profile进行归一化处理
        ''' </summary>
        ''' <returns></returns>
        Public Function EuclideanNormalization() As Double
            Return {XY, YZ, XZ}.EuclideanDistance
        End Function

        Public Overrides Function ToString() As String
            Return $"{Codon} -> (pXY={XY}, pYZ={YZ}, pXZ={XZ})"
        End Function

        ''' <summary>
        ''' 简单的产生三个残基单元产生的Triple片段对象
        ''' </summary>
        ''' <param name="seq"></param>
        ''' <returns></returns>
        ''' 
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Function PopulateTriples(seq As SeqTypes) As IEnumerable(Of String)
            Return PopulateTriples(vec:=seq.GetVector)
        End Function

        ''' <summary>
        ''' 简单的产生三个残基单元产生的Triple片段对象
        ''' </summary>
        ''' <returns></returns>
        Public Shared Iterator Function PopulateTriples(vec As IReadOnlyCollection(Of Char)) As IEnumerable(Of String)
            For Each i As Char In vec
                For Each j As Char In vec
                    For Each k As Char In vec
                        Yield New String({i, j, k})
                    Next
                Next
            Next
        End Function

    End Structure
End Namespace
