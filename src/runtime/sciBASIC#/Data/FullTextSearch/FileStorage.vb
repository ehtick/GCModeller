﻿#Region "Microsoft.VisualBasic::ee31c41ab14f42512a21e8eba6dfe2be, Data\FullTextSearch\FileStorage.vb"

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

    '   Total Lines: 126
    '    Code Lines: 87 (69.05%)
    ' Comment Lines: 15 (11.90%)
    '    - Xml Docs: 20.00%
    ' 
    '   Blank Lines: 24 (19.05%)
    '     File Size: 4.44 KB


    ' Class FileStorage
    ' 
    '     Constructor: (+1 Overloads) Sub New
    ' 
    '     Function: GenericEnumerator, GetDocument, ReadIndex
    ' 
    '     Sub: (+2 Overloads) Dispose, Save, WriteIndex
    ' 
    ' /********************************************************************************/

#End Region

Imports System.IO
Imports System.Text
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Data.IO
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq

''' <summary>
''' document text file 
''' </summary>
Public Class FileStorage : Implements Enumeration(Of Long), IDisposable

    ReadOnly doc_stream As BinaryDataReader
    ReadOnly file As Stream
    ReadOnly offsets As New List(Of Long)

    Private disposedValue As Boolean

    Sub New(offsets As Long(), doc As Stream)
        Me.offsets = New List(Of Long)(offsets)
        Me.doc_stream = New BinaryDataReader(doc, Encoding.UTF8)
        Me.file = doc
    End Sub

    Public Sub Save(text As String)
        Dim writer As New BinaryDataWriter(file, Encoding.UTF8)

        offsets.Add(file.Length)

        writer.Seek(file.Length, SeekOrigin.Begin)
        writer.Write(text, BinaryStringFormat.DwordLengthPrefix)
        writer.Flush()
    End Sub

    Public Function GetDocument(id As Integer) As String
        Dim offset As Long = offsets(id)
        doc_stream.Seek(offset, SeekOrigin.Begin)
        Return doc_stream.ReadString(BinaryStringFormat.DwordLengthPrefix)
    End Function

    Public Shared Function ReadIndex(file As Stream, ByRef offsets As Long()) As InvertedIndex
        Dim index As InvertedIndex

        If file.Length = 0 Then
            offsets = Nothing
            index = New InvertedIndex
        Else
            Dim reader As New BinaryDataReader(file, Encoding.UTF8)
            Dim nsize As Integer = reader.ReadInt32
            Dim lastId As Integer = reader.ReadInt32
            Dim ids As New Dictionary(Of String, List(Of Integer))
            Dim token As String
            Dim idsize As Integer

            offsets = reader.ReadInt64s(reader.ReadInt32)

            For i As Integer = 0 To nsize - 1
                token = reader.ReadString(BinaryStringFormat.ByteLengthPrefix)
                idsize = reader.ReadInt32
                ids.Add(token, New List(Of Integer)(reader.ReadInt32s(idsize)))
            Next

            index = New InvertedIndex(ids, lastId:=lastId)
        End If

        Call file.Close()
        Call file.Dispose()

        Return index
    End Function

    Public Shared Sub WriteIndex(index As InvertedIndex, offsets As Long(), file As Stream)
        Dim bin As New BinaryDataWriter(file, Encoding.UTF8)

        ' last id is not equals to the offset length
        ' due to the reason of empty doc may change the id un-expected?
        bin.Write(index.size)
        bin.Write(index.lastId)
        bin.Write(offsets.Length)
        bin.Write(offsets)

        For Each token As NamedCollection(Of Integer) In index.AsEnumerable
            Call bin.Write(token.name, BinaryStringFormat.ByteLengthPrefix)
            Call bin.Write(token.Length)
            Call bin.Write(token.value)
        Next

        Call bin.Flush()
        Call bin.Close()
        Call bin.Dispose()
    End Sub

    Public Iterator Function GenericEnumerator() As IEnumerator(Of Long) Implements Enumeration(Of Long).GenericEnumerator
        For Each offset_l As Long In offsets
            Yield offset_l
        Next
    End Function

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)
                Call file.Flush()
                Call file.Close()
                Call file.Dispose()
            End If

            ' TODO: 释放未托管的资源(未托管的对象)并重写终结器
            ' TODO: 将大型字段设置为 null
            disposedValue = True
        End If
    End Sub

    ' ' TODO: 仅当“Dispose(disposing As Boolean)”拥有用于释放未托管资源的代码时才替代终结器
    ' Protected Overrides Sub Finalize()
    '     ' 不要更改此代码。请将清理代码放入“Dispose(disposing As Boolean)”方法中
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' 不要更改此代码。请将清理代码放入“Dispose(disposing As Boolean)”方法中
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class