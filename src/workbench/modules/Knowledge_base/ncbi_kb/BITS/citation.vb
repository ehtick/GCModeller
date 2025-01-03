﻿Imports System.Runtime.CompilerServices
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.Linq

Namespace BITS

    Public Class RefList

        <XmlAttribute("id")> Public Property id As String
        <XmlElement> Public Property ref As ref()

        Public Iterator Function GetCitations() As IEnumerable(Of Citation)
            For Each r As ref In ref.SafeQuery
                Dim cite As Citation = CreateCitation(r)

                If Not cite Is Nothing Then
                    Yield cite
                End If
            Next
        End Function

        Public Shared Function CreateCitation(r As ref) As Citation
            Dim authors As String()
            Dim cite As MixedCitation

            If r.mixed_citation IsNot Nothing Then
                cite = r.mixed_citation
                authors = cite.string_names _
                    .SafeQuery _
                    .Select(Function(name) name.ToString) _
                    .ToArray
            ElseIf r.element_citation IsNot Nothing Then
                cite = r.element_citation
                authors = cite.person_group _
                    .AsEnumerable _
                    .Select(Function(name) name.ToString) _
                    .ToArray
            Else
                Return Nothing
            End If

            Dim doi = cite.pub_id.SafeQuery.Where(Function(p) p.pub_id_type = "doi").FirstOrDefault?.id
            Dim pmid = cite.pub_id.SafeQuery.Where(Function(p) p.pub_id_type = "pmid").FirstOrDefault?.id

            Dim citation As New Citation With {
                .authors = authors,
                .abstract = cite.annotation _
                    .SafeQuery _
                    .Select(Function(a) a.GetContentText) _
                    .JoinBy(vbCrLf & vbCrLf),
                .doi = doi,
                .pubmed_id = pmid,
                .fpage = cite.fpage,
                .lpage = cite.lpage,
                .journal = cite.source,
                .title = cite.title?.GetTextContent(),
                .volume = cite.volume,
                .year = cite.year
            }

            If citation.authors.IsNullOrEmpty AndAlso citation.title.StringEmpty AndAlso citation.journal.StringEmpty Then
                ' needs to be parsed from the text?
                Call Citation.TryParse(cite.GetTextContent, citation)
            End If

            Return citation
        End Function

    End Class

    Public Class ref

        <XmlAttribute> Public Property id As String

        <XmlElement("mixed-citation")>
        Public Property mixed_citation As MixedCitation

        <XmlElement("element-citation")>
        Public Property element_citation As MixedCitation

        Public Overrides Function ToString() As String
            Return id
        End Function

    End Class

    Public Class personGroup : Implements Enumeration(Of StringName)

        <XmlElement("name")> Public Property names As StringName()

        Public Iterator Function GenericEnumerator() As IEnumerator(Of StringName) Implements Enumeration(Of StringName).GenericEnumerator
            If names Is Nothing Then
                Return
            End If

            For Each name As StringName In Me.names
                Yield name
            Next
        End Function
    End Class

    Public Class MixedCitation : Inherits Paragraph

        <XmlAttribute("publication-type")> Public Property publication_type As String
        <XmlElement> Public Property annotation As Annotation()
        <XmlElement("string-name")> Public Property string_names As StringName()
        <XmlElement("person-group")> Public Property person_group As personGroup
        Public Property etal As String

        ''' <summary>
        ''' the article-title
        ''' </summary>
        ''' <returns></returns>
        <XmlElement("article-title")> Public Property title As Paragraph
        Public Property source As String
        Public Property year As String
        Public Property volume As String
        Public Property fpage As String
        Public Property lpage As String
        <XmlElement("pub-id")> Public Property pub_id As PubId()

        Public Property collab As String
        Public Property issue As String

    End Class

    Public Class PubId

        <XmlAttribute("pub-id-type")>
        Public Property pub_id_type As String

        <XmlText> Public Property id As String

        Public Overrides Function ToString() As String
            Return id
        End Function

    End Class

    <XmlType("string-name")>
    Public Class StringName

        <XmlAttribute("name-style")> Public Property name_style As String
        Public Property surname As String
        <XmlElement("given-names")> Public Property given_names As String

        Public Overrides Function ToString() As String
            Return surname & " " & given_names
        End Function

    End Class

    Public Class Annotation

        <XmlElement> Public Property p As Paragraph()

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetContentText() As String
            Return p.Select(Function(pi) pi.GetTextContent).JoinBy(vbCrLf & vbCrLf)
        End Function

    End Class
End Namespace