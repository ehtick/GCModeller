﻿#Region "Microsoft.VisualBasic::3fdbe0d9d6a9237c63678f7e06f05cb9, modules\Knowledge_base\ncbi_kb\Citation.vb"

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

    '   Total Lines: 98
    '    Code Lines: 65 (66.33%)
    ' Comment Lines: 16 (16.33%)
    '    - Xml Docs: 100.00%
    ' 
    '   Blank Lines: 17 (17.35%)
    '     File Size: 3.10 KB


    ' Class Citation
    ' 
    '     Properties: abstract, authors, doi, fpage, journal
    '                 lpage, pubmed_id, title, volume, year
    ' 
    '     Function: ama_cite, apa_cite, mla_cite, nlm_cite, ToString
    ' 
    '     Sub: TryParse
    ' 
    ' /********************************************************************************/

#End Region

Public Class Citation

    Public Property authors As String()
    Public Property title As String
    Public Property journal As String
    Public Property year As String
    Public Property volume As String
    Public Property fpage As String
    Public Property lpage As String
    Public Property doi As String
    Public Property pubmed_id As UInteger
    Public Property abstract As String

    Public Overrides Function ToString() As String
        Return nlm_cite()
    End Function

    Public Shared Sub TryParse(cite_text As String, ByRef citation As Citation)
        Try
            Dim cite_str As String = cite_text
            Dim tokens As String() = cite_str.StringSplit("\.\s+")
            Dim authors = tokens(0).StringSplit(",\s+")

            cite_str = tokens(2)
            citation.authors = authors
            citation.title = Strings.Trim(tokens(1))

            tokens = cite_str.Split(";"c)
            cite_str = tokens(0)

            citation.year = cite_str.Match("\d{4}")

            If Not citation.year.StringEmpty Then
                citation.journal = cite_str.Replace(citation.year, "").Trim
            End If

            tokens = tokens(1).Split(":"c)

            citation.volume = Strings.Trim(tokens(0))
            tokens = tokens(1).Split("-"c)
            citation.fpage = Strings.Trim(tokens(0))
            citation.lpage = Strings.Trim(tokens(1))
        Catch ex As Exception
            ex = New Exception(cite_text, ex)
            Call App.LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' NLM
    ''' </summary>
    ''' <returns></returns>
    Public Function nlm_cite() As String
        Dim authors = Me.authors.JoinBy(", ")
        Return $"{authors}. {title}. {journal}. {year};{volume}:{fpage}-{lpage}. doi: {doi}. PMID: {pubmed_id}"
    End Function

    ''' <summary>
    ''' AMA
    ''' </summary>
    ''' <returns></returns>
    Public Function ama_cite() As String
        Dim authors = Me.authors.JoinBy(", ")
        Return $"{authors}. {title}. {journal}. {year};{volume}:{fpage}-{lpage}. doi:{doi}"
    End Function

    ''' <summary>
    ''' APA
    ''' </summary>
    ''' <returns></returns>
    Public Function apa_cite() As String
        Dim authors As String

        If Me.authors.Length = 1 Then
            authors = Me.authors(0)
        Else
            authors = Me.authors.First & $", & {Me.authors.Last}"
        End If

        Return $"{authors}. ({year}). {title}. {journal}, {volume}, {fpage}-{lpage}. https://doi.org/{doi}"
    End Function

    ''' <summary>
    ''' MLA
    ''' </summary>
    ''' <returns></returns>
    Public Function mla_cite() As String
        Dim authors As String

        If Me.authors.Length = 1 Then
            authors = Me.authors(0)
        Else
            authors = Me.authors.First & $", and {Me.authors.Last}"
        End If

        Return $"{authors}. ""{title}."" {journal} vol. {volume}({year}): {fpage}-{lpage}. doi:{doi}"
    End Function
End Class
