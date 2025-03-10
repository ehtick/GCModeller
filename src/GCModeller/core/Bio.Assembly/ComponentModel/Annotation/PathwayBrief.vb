﻿#Region "Microsoft.VisualBasic::31c1302b0e00a7132bd7504cf4861121, core\Bio.Assembly\ComponentModel\Annotation\PathwayBrief.vb"

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

    '   Total Lines: 68
    '    Code Lines: 28 (41.18%)
    ' Comment Lines: 32 (47.06%)
    '    - Xml Docs: 96.88%
    ' 
    '   Blank Lines: 8 (11.76%)
    '     File Size: 2.53 KB


    '     Class PathwayBrief
    ' 
    '         Properties: briteID, description, EntryId, name
    ' 
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.ComponentModel
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel

Namespace ComponentModel.Annotation

    ''' <summary>
    ''' An abstract biological pathway model
    ''' </summary>
    Public MustInherit Class PathwayBrief : Inherits XmlDataModel
        Implements IKeyValuePairObject(Of String, String)
        Implements INamedValue

        ''' <summary>
        ''' the reference id of the current pathway object
        ''' </summary>
        ''' <returns></returns>
        <XmlAttribute("id")>
        Public Overridable Property EntryId As String Implements INamedValue.Key, IKeyValuePairObject(Of String, String).Key

        ''' <summary>
        ''' The map title display name
        ''' </summary>
        ''' <returns>The name value of this pathway object</returns>
        <XmlElement>
        Public Property name As String

        ''' <summary>
        ''' the function description text of the current pathway object
        ''' </summary>
        ''' <returns></returns>
        Public Property description As String Implements IKeyValuePairObject(Of String, String).Value

        ''' <summary>
        ''' Gets the pathway related genes.
        ''' </summary>
        ''' <returns>
        ''' name - gene id
        ''' value - ontology id, example as KO
        ''' description - gene name or function description
        ''' </returns>
        Public MustOverride Function GetPathwayGenes() As IEnumerable(Of NamedValue(Of String))
        ''' <summary>
        ''' Gets the pathway related metabolite compounds.
        ''' </summary>
        ''' <returns></returns>
        Public MustOverride Function GetCompoundSet() As IEnumerable(Of NamedValue(Of String))

        ''' <summary>
        ''' 和具体的物种的编号无关的在KEGG数据库之中的参考对象的编号
        ''' </summary>
        ''' <returns></returns>
        ''' 
        <XmlIgnore>
        Public Overridable ReadOnly Property briteID As String
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                Return EntryId
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return String.Format("{0}: {1}", EntryId, description)
        End Function
    End Class
End Namespace
