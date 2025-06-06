﻿#Region "Microsoft.VisualBasic::5ebc87eb36c5df8cd25c5ba5bfec5968, models\BioCyc\Models\Model.vb"

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
    '    Code Lines: 38 (66.67%)
    ' Comment Lines: 8 (14.04%)
    '    - Xml Docs: 100.00%
    ' 
    '   Blank Lines: 11 (19.30%)
    '     File Size: 1.84 KB


    ' Class Model
    ' 
    '     Properties: citations, comment, commonName, credits, instanceNameTemplate
    '                 synonyms, types, uniqueId
    ' 
    '     Function: GetDbLinks, ToString
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Microsoft.VisualBasic.Data.Framework.IO.CSVFile
Imports Microsoft.VisualBasic.Linq
Imports SMRUCC.genomics.ComponentModel.DBLinkBuilder

''' <summary>
''' the abstract biocyc element model
''' </summary>
Public MustInherit Class Model : Implements IReadOnlyId

    ''' <summary>
    ''' the unique reference id of current feature 
    ''' element object.
    ''' </summary>
    ''' <returns></returns>
    <AttributeField("UNIQUE-ID")>
    Public Property uniqueId As String Implements IReadOnlyId.Identity

    <AttributeField("TYPES")>
    Public Property types As String()

    <AttributeField("COMMON-NAME")>
    Public Property commonName As String

    <AttributeField("CITATIONS")>
    Public Property citations As String()

    <AttributeField("COMMENT")>
    Public Property comment As String

    <AttributeField("CREDITS")>
    Public Property credits As String()

    <AttributeField("INSTANCE-NAME-TEMPLATE")>
    Public Property instanceNameTemplate As String
    <AttributeField("SYNONYMS")>
    Public Property synonyms As String()

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Overrides Function ToString() As String
        Return If(commonName, uniqueId)
    End Function

    Public Shared Iterator Function GetDbLinks(db_xrefs As IEnumerable(Of String)) As IEnumerable(Of DBLink)
        For Each id As String In db_xrefs.SafeQuery
            Dim tokens = Tokenizer _
                .CharsParser(id.GetStackValue("(", ")"), delimiter:=" "c) _
                .ToArray

            Yield New DBLink With {
                .DBName = tokens(0),
                .entry = tokens(1)
            }
        Next
    End Function
End Class
