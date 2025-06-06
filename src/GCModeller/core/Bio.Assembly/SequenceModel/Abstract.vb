﻿#Region "Microsoft.VisualBasic::399b99f077dbba599cdfe417cc8c3d41, core\Bio.Assembly\SequenceModel\Abstract.vb"

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

    '   Total Lines: 70
    '    Code Lines: 25 (35.71%)
    ' Comment Lines: 37 (52.86%)
    '    - Xml Docs: 100.00%
    ' 
    '   Blank Lines: 8 (11.43%)
    '     File Size: 2.19 KB


    '     Enum SeqTypes
    ' 
    '         DNA, RNA
    ' 
    '  
    ' 
    ' 
    ' 
    '     Interface IPolymerSequenceModel
    ' 
    '         Properties: SequenceData
    ' 
    '     Class ISequenceBuilder
    ' 
    '         Properties: Length, Name
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Xml.Serialization

Namespace SequenceModel

    Public Enum SeqTypes As Integer
        ''' <summary>
        ''' the unknow sequence type
        ''' </summary>
        Generic = 0
        ''' <summary>
        ''' Deoxyribonucleotide - DNA(ATGC)
        ''' </summary>
        DNA
        ''' <summary>
        ''' Ribonucleotide - RNA(AUGC)
        ''' </summary>
        RNA
        ''' <summary>
        ''' Polypeptide
        ''' </summary>
        <Description("prot")> Protein
    End Enum

    ''' <summary>
    ''' Sequence model for a macro biomolecule sequence.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IPolymerSequenceModel

        ''' <summary>
        ''' <see cref="System.String"></see> type sequence data for the target <see cref="ISequenceModel"/> sequence model object.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property SequenceData As String
    End Interface

    ''' <summary>
    ''' This class can be using for build a <see cref="FASTA.FastaSeq"/> object.
    ''' </summary>
    Public MustInherit Class ISequenceBuilder

        ''' <summary>
        ''' <see cref="GetSequenceData()"/> length.(序列的长度)
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Length As Integer
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                Return Len(GetSequenceData)
            End Get
        End Property

        ''' <summary>
        ''' This property is using for generates the fasta sequence title.(用于进行序列标识的标题摘要)
        ''' </summary>
        ''' <returns></returns>
        <XmlAttribute>
        Public Overridable Property Name As String

        ''' <summary>
        ''' Data source method for gets the sequence data to create a fasta object.
        ''' </summary>
        ''' <returns></returns>
        Public MustOverride Function GetSequenceData() As String
    End Class
End Namespace
