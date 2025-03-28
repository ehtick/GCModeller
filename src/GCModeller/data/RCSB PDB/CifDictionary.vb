﻿#Region "Microsoft.VisualBasic::55b36d67d68157ed9cf34bd4b2fd4125, data\RCSB PDB\CifDictionary.vb"

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

    '   Total Lines: 33
    '    Code Lines: 25 (75.76%)
    ' Comment Lines: 0 (0.00%)
    '    - Xml Docs: 0.00%
    ' 
    '   Blank Lines: 8 (24.24%)
    '     File Size: 1.29 KB


    ' Class CifDictionary
    ' 
    '     Properties: Sections
    ' 
    '     Function: Load
    '     Class Section
    ' 
    '         Properties: KeyValuePairs
    ' 
    '         Function: TryParse
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Public Class CifDictionary

    Public Property Sections As Section()

    Public Class Section

        Public Property KeyValuePairs As KeyValuePair(Of String, String)()

        Const SPLIT_REGX As String = "(.+)?"

        Protected Friend Shared Function TryParse(strData As String) As Section
            Dim Tokens As String() = (From strLine As String In Strings.Split(strData, vbLf)
                                      Let str As String = strLine.TrimNewLine
                                      Where Not String.IsNullOrEmpty(str)
                                      Select str).ToArray
            Dim PairList As New List(Of KeyValuePair(Of String, String))
            For Each item As String In Tokens

            Next
            Throw New NotImplementedException
        End Function
    End Class

    Public Shared Function Load(Path As String) As CifDictionary
        Dim FileContent As String = FileIO.FileSystem.ReadAllText(Path)
        Dim Tokens As String() = Strings.Split(FileContent, "# ")
        Dim LQuery = (From strData As String In Tokens Select Section.TryParse(strData)).ToArray

        Dim CifDict As CifDictionary = New CifDictionary
        CifDict.Sections = LQuery
        Return CifDict
    End Function
End Class
