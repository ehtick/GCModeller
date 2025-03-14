﻿#Region "Microsoft.VisualBasic::fd3b4c240fd02837f1b1042215a3e37c, Data\BinaryData\Feather\Impl\Metadata\PrimitiveArray.vb"

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

    '   Total Lines: 111
    '    Code Lines: 97 (87.39%)
    ' Comment Lines: 6 (5.41%)
    '    - Xml Docs: 83.33%
    ' 
    '   Blank Lines: 8 (7.21%)
    '     File Size: 4.84 KB


    '     Class PrimitiveArray
    ' 
    '         Properties: ByteBuffer, Encoding, Length, NullCount, Offset
    '                     TotalBytes, Type
    ' 
    '         Function: __assign, CreatePrimitiveArray, EndPrimitiveArray, (+2 Overloads) GetRootAsPrimitiveArray
    ' 
    '         Sub: __init, AddEncoding, AddLength, AddNullCount, AddOffset
    '              AddTotalBytes, AddType, StartPrimitiveArray
    ' 
    ' 
    ' /********************************************************************************/

#End Region

' automatically generated by the FlatBuffers compiler, do not modify

Imports Microsoft.VisualBasic.DataStorage.FeatherFormat.FlatBuffers

Namespace Impl.FbsMetadata

    Friend Class PrimitiveArray
        Implements IFlatbufferObject
        Private __p As Table = New Table()
        Public ReadOnly Property ByteBuffer As ByteBuffer Implements IFlatbufferObject.ByteBuffer
            Get
                Return __p.bb
            End Get
        End Property
        Public Shared Function GetRootAsPrimitiveArray(_bb As ByteBuffer) As PrimitiveArray
            Return GetRootAsPrimitiveArray(_bb, New PrimitiveArray())
        End Function
        Public Shared Function GetRootAsPrimitiveArray(_bb As ByteBuffer, obj As PrimitiveArray) As PrimitiveArray
            Return obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)
        End Function
        Public Sub __init(_i As Integer, _bb As ByteBuffer) Implements IFlatbufferObject.__init
            __p.bb_pos = _i
            __p.bb = _bb
        End Sub
        Public Function __assign(_i As Integer, _bb As ByteBuffer) As PrimitiveArray
            __init(_i, _bb)
            Return Me
        End Function

        Public ReadOnly Property Type As Type
            Get
                Dim o = __p.__offset(4)
                Return If(o <> 0, CType(__p.bb.GetSbyte(o + __p.bb_pos), Type), Type.BOOL)
            End Get
        End Property
        Public ReadOnly Property Encoding As Encoding
            Get
                Dim o = __p.__offset(6)
                Return If(o <> 0, CType(__p.bb.GetSbyte(o + __p.bb_pos), Encoding), Encoding.PLAIN)
            End Get
        End Property
        ''' Relative memory offset of the start of the array data excluding the size
        ''' of the metadata
        Public ReadOnly Property Offset As Long
            Get
                Dim o = __p.__offset(8)
                Return If(o <> 0, __p.bb.GetLong(o + __p.bb_pos), 0)
            End Get
        End Property
        ''' The number of logical values in the array
        Public ReadOnly Property Length As Long
            Get
                Dim o = __p.__offset(10)
                Return If(o <> 0, __p.bb.GetLong(o + __p.bb_pos), 0)
            End Get
        End Property
        ''' The number of observed nulls
        Public ReadOnly Property NullCount As Long
            Get
                Dim o = __p.__offset(12)
                Return If(o <> 0, __p.bb.GetLong(o + __p.bb_pos), 0)
            End Get
        End Property
        ''' The total size of the actual data in the file
        Public ReadOnly Property TotalBytes As Long
            Get
                Dim o = __p.__offset(14)
                Return If(o <> 0, __p.bb.GetLong(o + __p.bb_pos), 0)
            End Get
        End Property

        Public Shared Function CreatePrimitiveArray(builder As FlatBufferBuilder, Optional type As Type = Type.BOOL, Optional encoding As Encoding = Encoding.PLAIN, Optional offset As Long = 0, Optional length As Long = 0, Optional null_count As Long = 0, Optional total_bytes As Long = 0) As Offset(Of PrimitiveArray)
            builder.StartObject(6)
            AddTotalBytes(builder, total_bytes)
            AddNullCount(builder, null_count)
            AddLength(builder, length)
            AddOffset(builder, offset)
            AddEncoding(builder, encoding)
            AddType(builder, type)
            Return EndPrimitiveArray(builder)
        End Function

        Public Shared Sub StartPrimitiveArray(builder As FlatBufferBuilder)
            builder.StartObject(6)
        End Sub
        Public Shared Sub AddType(builder As FlatBufferBuilder, type As Type)
            builder.AddSbyte(0, type, 0)
        End Sub
        Public Shared Sub AddEncoding(builder As FlatBufferBuilder, encoding As Encoding)
            builder.AddSbyte(1, encoding, 0)
        End Sub
        Public Shared Sub AddOffset(builder As FlatBufferBuilder, offset As Long)
            builder.AddLong(2, offset, 0)
        End Sub
        Public Shared Sub AddLength(builder As FlatBufferBuilder, length As Long)
            builder.AddLong(3, length, 0)
        End Sub
        Public Shared Sub AddNullCount(builder As FlatBufferBuilder, nullCount As Long)
            builder.AddLong(4, nullCount, 0)
        End Sub
        Public Shared Sub AddTotalBytes(builder As FlatBufferBuilder, totalBytes As Long)
            builder.AddLong(5, totalBytes, 0)
        End Sub
        Public Shared Function EndPrimitiveArray(builder As FlatBufferBuilder) As Offset(Of PrimitiveArray)
            Dim o As Integer = builder.EndObject()
            Return New Offset(Of PrimitiveArray)(o)
        End Function
    End Class


End Namespace
