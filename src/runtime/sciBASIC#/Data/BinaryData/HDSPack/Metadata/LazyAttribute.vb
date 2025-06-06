﻿#Region "Microsoft.VisualBasic::76602baabe6535c855827ff626baa7c1, Data\BinaryData\HDSPack\Metadata\LazyAttribute.vb"

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

    '   Total Lines: 112
    '    Code Lines: 86 (76.79%)
    ' Comment Lines: 9 (8.04%)
    '    - Xml Docs: 100.00%
    ' 
    '   Blank Lines: 17 (15.18%)
    '     File Size: 4.56 KB


    ' Class LazyAttribute
    ' 
    '     Properties: attributes
    ' 
    '     Function: BuildDynamicTypeObject, GetBuffer, GetEnumerator, (+2 Overloads) GetValue, IEnumerable_GetEnumerator
    '               ToArray
    ' 
    '     Sub: Add
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.Data.IO
Imports Microsoft.VisualBasic.Data.IO.MessagePack
Imports Microsoft.VisualBasic.ValueTypes

Public Class LazyAttribute : Implements IEnumerable(Of String)

    Public Property attributes As New Dictionary(Of String, AttributeMetadata)

    ''' <summary>
    ''' add key-value paired data into the attribute list
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="value"></param>
    Public Sub Add(name As String, value As Object)
        Dim attr As AttributeMetadata

        If value Is Nothing Then
            attr = New AttributeMetadata With {
                .name = name,
                .data = Nothing,
                .type = Nothing
            }
        Else
            attr = New AttributeMetadata With {
                .name = name,
                .type = value.GetType.FullName,
                .data = GetBuffer(value)
            }
        End If

        attributes(name) = attr
    End Sub

    ''' <summary>
    ''' build a dynamics object that could be used for show in property grid controld
    ''' </summary>
    ''' <returns></returns>
    Public Function BuildDynamicTypeObject() As Object
        Dim attrs As New Dictionary(Of String, Object)

        For Each attr As String In attributes.Keys
            attrs(attr) = GetValue(attr)
        Next

        Return DynamicType.Create(attrs)
    End Function

    Public Function GetValue(name As String) As Object
        Dim attr As AttributeMetadata = attributes.TryGetValue(name)

        If attr Is Nothing Then
            Return Nothing
        Else
            Return GetValue(attr)
        End If
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Friend Function ToArray() As AttributeMetadata()
        Return attributes.Values.ToArray
    End Function

    Public Shared Function GetValue(attr As AttributeMetadata) As Object
        If attr.data.IsNullOrEmpty Then
            Return Nothing
        End If

        Select Case attr.GetUnderlyingType
            Case GetType(Date) : Return FromUnixTimeStamp(NetworkByteOrderBitConvertor.ToDouble(attr.data, Scan0))
            Case GetType(String) : Return Encoding.UTF8.GetString(attr.data)
            Case GetType(Single) : Return NetworkByteOrderBitConvertor.ToSingle(attr.data, Scan0)
            Case GetType(Double) : Return NetworkByteOrderBitConvertor.ToDouble(attr.data, Scan0)
            Case GetType(Short) : Return NetworkByteOrderBitConvertor.ToInt16(attr.data, Scan0)
            Case GetType(Integer) : Return NetworkByteOrderBitConvertor.ToInt32(attr.data, Scan0)
            Case GetType(Long) : Return NetworkByteOrderBitConvertor.ToInt64(attr.data, Scan0)
            Case GetType(Byte) : Return attr.data(Scan0)
            Case Else
                Return MsgPackSerializer.Deserialize(attr.GetUnderlyingType, attr.data)
        End Select
    End Function

    Public Shared Function GetBuffer(val As Object) As Byte()
        Select Case val.GetType
            Case GetType(Date) : Return NetworkByteOrderBitConvertor.GetBytes(DirectCast(val, Date).UnixTimeStamp)
            Case GetType(String) : Return Encoding.UTF8.GetBytes(DirectCast(val, String))
            Case GetType(Integer) : Return NetworkByteOrderBitConvertor.GetBytes(DirectCast(val, Integer))
            Case GetType(Long) : Return NetworkByteOrderBitConvertor.GetBytes(DirectCast(val, Long))
            Case GetType(Short) : Return NetworkByteOrderBitConvertor.GetBytes(DirectCast(val, Short))
            Case GetType(Single) : Return NetworkByteOrderBitConvertor.GetBytes(DirectCast(val, Single))
            Case GetType(Double) : Return NetworkByteOrderBitConvertor.GetBytes(DirectCast(val, Double))
            Case GetType(Byte) : Return {DirectCast(val, Byte)}
            Case Else
                Return MsgPackSerializer.SerializeObject(val)
        End Select
    End Function

    Public Iterator Function GetEnumerator() As IEnumerator(Of String) Implements IEnumerable(Of String).GetEnumerator
        If attributes.IsNullOrEmpty Then
            Return
        End If

        For Each tag As String In attributes.Keys
            Yield tag
        Next
    End Function

    Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Yield GetEnumerator()
    End Function
End Class
