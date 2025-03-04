﻿#Region "Microsoft.VisualBasic::d8f7b2afe6a0f4d38076696ebbcf47ba, models\SBML\SBML\Components\Notes.vb"

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

    '   Total Lines: 100
    '    Code Lines: 74 (74.00%)
    ' Comment Lines: 3 (3.00%)
    '    - Xml Docs: 100.00%
    ' 
    '   Blank Lines: 23 (23.00%)
    '     File Size: 3.26 KB


    '     Class Notes
    ' 
    '         Properties: body, Properties
    ' 
    '         Function: GetText, ToString
    ' 
    '     Class Body
    ' 
    '         Properties: Passage, Text
    ' 
    '         Function: GetProperties, GetText, ToString
    ' 
    '     Class [Property]
    ' 
    '         Properties: Name, value
    ' 
    '         Function: Parser, ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Text
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Microsoft.VisualBasic.Linq.Extensions
Imports Microsoft.VisualBasic.Text
Imports ASCII = Microsoft.VisualBasic.Text.ASCII

Namespace Components

    <XmlType("notes")> Public Class Notes : Inherits Body

        <XmlElement("body", [Namespace]:="http://www.w3.org/1999/xhtml")>
        Public Property body As Body

        Public ReadOnly Property Properties As [Property]()
            Get
                If body Is Nothing Then
                    Return New [Property]() {}
                Else
                    Return body.GetProperties
                End If
            End Get
        End Property

        Public Overrides Function GetText() As String
            Dim sb As New StringBuilder

            If Not Text.StringEmpty Then
                Call sb.AppendLine(Text)
            End If

            If Not body Is Nothing Then
                Call sb.AppendLine(body.GetText)
            End If

            If Not Passage.IsNullOrEmpty Then
                Call Passage.DoEach(AddressOf sb.AppendLine)
            End If

            Return sb.ToString.Trim(" "c, ASCII.TAB, ASCII.CR, ASCII.LF)
        End Function

        Public Overrides Function ToString() As String
            Return GetText()
        End Function
    End Class

    ''' <summary>
    ''' A note document body
    ''' </summary>
    <XmlType("body", [Namespace]:="http://www.w3.org/1999/xhtml")> Public Class Body

        <XmlElement("p")> Public Property Passage As String()
        <XmlText> Public Overridable Property Text As String

        Public Function GetProperties() As [Property]()
            Return Passage.Select(Function(s) [Property].Parser(s)).ToArray
        End Function

        Public Overridable Function GetText() As String
            Dim sb As New StringBuilder

            If Not Text.StringEmpty Then
                Call sb.AppendLine(Text)
            End If

            If Not Passage.IsNullOrEmpty Then
                Call Passage.DoEach(AddressOf sb.AppendLine)
            End If

            Return sb.ToString
        End Function

        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class

    Public Class [Property] : Implements IReadOnlyId, INamedValue
        Implements IKeyValuePairObject(Of String, String)

        Public Property Name As String Implements IKeyValuePairObject(Of String, String).Key, IReadOnlyId.Identity, INamedValue.Key
        Public Property value As String Implements IKeyValuePairObject(Of String, String).Value

        Public Overrides Function ToString() As String
            Return $"[{Name}]  {value}"
        End Function

        Public Shared Function Parser(s As String) As [Property]
            Dim i As Integer = InStr(s, ":")
            Dim name As String = Mid(s, 1, i - 1)
            Dim value As String = Mid(s, i + 1).Trim

            Return New [Property] With {
                .Name = name,
                .value = value
            }
        End Function
    End Class
End Namespace
