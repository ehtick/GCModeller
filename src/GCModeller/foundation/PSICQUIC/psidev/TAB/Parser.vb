﻿#Region "Microsoft.VisualBasic::f29bdf2322c0ea98cc33343e6512a4e9, foundation\PSICQUIC\psidev\TAB\Parser.vb"

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

    '   Total Lines: 31
    '    Code Lines: 25 (80.65%)
    ' Comment Lines: 0 (0.00%)
    '    - Xml Docs: 0.00%
    ' 
    '   Blank Lines: 6 (19.35%)
    '     File Size: 1.14 KB


    '     Module Parser
    ' 
    '         Function: LoadMItab
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.SchemaMaps.DataFrameColumnAttribute
Imports Microsoft.VisualBasic.Linq
Imports ASCII = Microsoft.VisualBasic.Text.ASCII

Namespace TAB

    Public Module Parser

        <Extension>
        Public Iterator Function LoadMItab(Of T)(path As String) As IEnumerable(Of T)
            Dim schema = LoadMapping(Of T)(mapsAll:=True)
            Dim header As String() = path.ReadFirstLine.Split(ASCII.TAB)
            Dim index As Dictionary(Of String, Integer) = header _
                .SeqIterator _
                .ToDictionary(Function(x) x.value,
                              Function(x) x.i)

            For Each line As String In path.IterateAllLines.Skip(1)
                Dim tokens As String() = line.Split(ASCII.TAB)
                Dim x As T = Activator.CreateInstance(Of T)

                For Each p In schema.Values
                    Call p.SetValue(x, tokens(index(p.Field.Name)))
                Next

                Yield x
            Next
        End Function
    End Module
End Namespace
