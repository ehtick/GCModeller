﻿#Region "Microsoft.VisualBasic::856db2966ab965924e09c0f33fce7273, mzkit\src\metadb\Massbank\Public\NCBI\MeSH\Reader.vb"

' Author:
' 
'       xieguigang (gg.xie@bionovogene.com, BioNovoGene Co., LTD.)
' 
' Copyright (c) 2018 gg.xie@bionovogene.com, BioNovoGene Co., LTD.
' 
' 
' MIT License
' 
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy
' of this software and associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
' copies of the Software, and to permit persons to whom the Software is
' furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all
' copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
' SOFTWARE.



' /********************************************************************************/

' Summaries:


' Code Statistics:

'   Total Lines: 48
'    Code Lines: 40
' Comment Lines: 0
'   Blank Lines: 8
'     File Size: 1.59 KB


'     Module Reader
' 
'         Function: ParseTree
' 
' 
' /********************************************************************************/

#End Region

Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Data.GraphTheory
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Language.Values

Namespace MeSH.Tree

    Public Module Reader

        Public Function ParseCategory(tree As String) As MeshCategory
            Static category_chars As Dictionary(Of Char, MeshCategory) = Enums(Of MeshCategory)() _
                .ToDictionary(Function(c)
                                  Return MeshCategory.C.ToString.First
                              End Function)

            Return category_chars(tree.ToUpper.First)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function ReadTerms(s As Stream) As IEnumerable(Of Term)
            Return ReadTerms(New StreamReader(s))
        End Function

        Public Function ParseTree(file As String) As Tree(Of Term)
            Using s As Stream = file.OpenReadonly
                Return ParseTree(New StreamReader(s))
            End Using
        End Function

        Public Function ParseTree(file As Stream) As Tree(Of Term)
            Return ParseTree(New StreamReader(file))
        End Function

        Private Iterator Function ReadTerms(file As StreamReader) As IEnumerable(Of Term)
            Dim line As Value(Of String) = ""
            Dim str As String()
            Dim term As Term

            Do While Not (line = file.ReadLine) Is Nothing
                str = line.Split(";"c)
                term = New Term With {
                    .term = str(Scan0),
                    .tree = str(1).Split("."c)
                }

                Yield term
            Loop
        End Function

        Public Function ParseTree(file As StreamReader) As Tree(Of Term)
            Dim tree As New Tree(Of Term) With {
                .Data = New Term With {.term = "/", .tree = {}},
                .Childs = New Dictionary(Of String, Tree(Of Term)),
                .label = "NCBI MeSH"
            }
            Dim node As Tree(Of Term)

            For Each term As Term In ReadTerms(file)
                ' reset the node to tree root
                node = tree
                ' walk the tree path
                For Each lv As String In term.tree
                    If Not node.Childs.ContainsKey(lv) Then
                        node.Childs.Add(lv, New Tree(Of Term) With {
                            .Childs = New Dictionary(Of String, Tree(Of Term)),
                            .label = lv,
                            .Parent = node
                        })
                    End If

                    node = node.Childs(lv)
                Next

                node.Data = term
                node.label = term.term
            Next

            Return tree
        End Function
    End Module
End Namespace