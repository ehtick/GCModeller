﻿#Region "Microsoft.VisualBasic::b30b0c65e1d5e0e62733ff62b6992a96, R#\proteomics_toolkit\ptfKit.vb"

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

    ' Module ptfKit
    ' 
    '     Function: ensurePtfFile, filterBykey, fromUniProt, loadPtf, NCBITaxonomy
    '               SampleAnnotations, savePtf, split, unifyProteinId
    ' 
    ' /********************************************************************************/

#End Region

Imports System.IO
Imports System.Reflection
Imports Microsoft.VisualBasic.ApplicationServices.Development
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports SMRUCC.genomics.Annotation
Imports SMRUCC.genomics.Annotation.Ptf
Imports SMRUCC.genomics.Assembly.Uniprot.XML
Imports SMRUCC.genomics.Data
Imports SMRUCC.Rsharp
Imports SMRUCC.Rsharp.Runtime
Imports SMRUCC.Rsharp.Runtime.Components
Imports SMRUCC.Rsharp.Runtime.Internal.Object
Imports SMRUCC.Rsharp.Runtime.Interop
Imports REnv = SMRUCC.Rsharp.Runtime

''' <summary>
''' toolkit for handle ptf annotation data set
''' </summary>
''' 
<Package("ptfKit")>
Module ptfKit

    Friend Function ensurePtfFile(ptf As Object, env As Environment) As [Variant](Of Message, PtfFile)
        If TypeOf ptf Is PtfFile Then
            Return DirectCast(ptf, PtfFile)
        Else
            Dim annotations = pipeline.TryCreatePipeline(Of ProteinAnnotation)(ptf, env)

            If annotations.isError Then
                Return annotations.getError
            End If

            Return New PtfFile With {
                .proteins = annotations _
                    .populates(Of ProteinAnnotation)(env) _
                    .ToArray
            }
        End If
    End Function

    ''' <summary>
    ''' Try to unify all protein id to uniprot id
    ''' </summary>
    ''' <param name="ptf"></param>
    ''' <param name="proteins"></param>
    ''' <param name="env"></param>
    ''' <returns></returns>
    <ExportAPI("as.uniprot_id")>
    Public Function unifyProteinId(<RRawVectorArgument> ptf As Object, <RRawVectorArgument> proteins As Object, Optional env As Environment = Nothing) As Object
        Dim inputs As pipeline = pipeline.TryCreatePipeline(Of INamedValue)(proteins, env)

        If inputs.isError Then
            Return inputs.getError
        End If

        Dim annotations As [Variant](Of Message, PtfFile) = ensurePtfFile(ptf, env)

        If annotations Like GetType(Message) Then
            Return annotations.TryCast(Of Message)
        End If

        Dim list = inputs.populates(Of INamedValue)(env).ToArray
        Dim generic_type As Type = list(Scan0).GetType
        Dim generic_input As Array = REnv.asVector(list, generic_type, env)
        Dim generic_api As MethodInfo = GetType(IDMapping).GetMethod(NameOf(IDMapping.Mapping)).MakeGenericMethod(generic_type)
        Dim result As IEnumerable = generic_api.Invoke(Nothing, {annotations.TryCast(Of PtfFile), generic_input})

        Return REnv.asVector(result.ToArray(Of INamedValue), generic_type, env)
    End Function

    ''' <summary>
    ''' Create the unify protein annotation models from a given uniprot database entries.
    ''' </summary>
    ''' <param name="uniprot"></param>
    ''' <param name="includesNCBITaxonomy"></param>
    ''' <param name="keys"></param>
    ''' <param name="env"></param>
    ''' <returns></returns>
    <ExportAPI("uniprot.ptf")>
    Public Function fromUniProt(<RRawVectorArgument>
                                uniprot As Object,
                                Optional includesNCBITaxonomy As Boolean = False,
                                Optional scientificName As Boolean = False,
                                <RRawVectorArgument(GetType(String))>
                                Optional keys As Object = "KEGG,KO,GO,Pfam,RefSeq,EC,InterPro,BioCyc,eggNOG",
                                Optional env As Environment = Nothing) As Object

        Dim source = getUniprotData(uniprot, env)
        Dim keyList As String = DirectCast(REnv.asVector(Of String)(keys), String()).JoinBy(",")

        If source Like GetType(Message) Then
            Return source.TryCast(Of Message)
        End If

        Return source _
            .TryCast(Of IEnumerable(Of entry)) _
            .Select(Function(protein)
                        Return protein.toPtf(
                            includesNCBITaxonomy:=includesNCBITaxonomy,
                            keys:=keyList,
                            scientificName:=scientificName
                        )
                    End Function) _
            .DoCall(AddressOf pipeline.CreateFromPopulator)
    End Function

    <ExportAPI("load.ptf")>
    Public Function loadPtf(file As Object, Optional lazy As Boolean = True, Optional env As Environment = Nothing) As Object
        Dim stream = GetFileStream(file, FileAccess.Read, env)

        If stream Like GetType(Message) Then
            Return stream.TryCast(Of Message)
        End If

        Dim tryClose = Sub()
                           If TypeOf file Is String Then
                               Try
                                   Call stream.TryCast(Of Stream).Close()
                               Catch ex As Exception

                               End Try
                           End If
                       End Sub

        If lazy Then
            Return PtfFile _
                .ReadAnnotations(stream.TryCast(Of Stream)) _
                .DoCall(Function(anno)
                            Return pipeline.CreateFromPopulator(
                                upstream:=anno,
                                finalize:=tryClose
                            )
                        End Function)
        Else
            Dim data As PtfFile = PtfFile.Load(stream.TryCast(Of Stream))
            Call tryClose()
            Return data
        End If
    End Function

    <ExportAPI("filter")>
    Public Function filterBykey(<RRawVectorArgument> ptf As Object, key$, Optional env As Environment = Nothing) As pipeline
        Dim upstream As pipeline = pipeline.TryCreatePipeline(Of ProteinAnnotation)(ptf, env)

        If upstream.isError Then
            Return upstream
        End If

        Return upstream _
            .populates(Of ProteinAnnotation)(env) _
            .Where(Function(protein)
                       Return protein.attributes.ContainsKey(key)
                   End Function) _
            .DoCall(AddressOf pipeline.CreateFromPopulator)
    End Function

    <ExportAPI("save.ptf")>
    <RApiReturn(GetType(Boolean))>
    Public Function savePtf(<RRawVectorArgument> ptf As Object, file As Object, Optional meta As list = Nothing, Optional env As Environment = Nothing) As Object
        Dim stream = GetFileStream(file, FileAccess.Write, env)
        Dim anno As pipeline = pipeline.TryCreatePipeline(Of ProteinAnnotation)(ptf, env)

        If anno.isError Then
            Return anno.getError
        End If
        If stream Like GetType(Message) Then
            Return stream.TryCast(Of Message)
        End If
        If meta Is Nothing Then
            meta = New list With {.slots = New Dictionary(Of String, Object)}
        End If

        Dim core = GetType(ProteinAnnotation).Assembly.FromAssembly

        If Not meta.hasName("version") Then
            meta.slots.Add("version", core.AssemblyVersion)
        End If
        If Not meta.hasName("built") Then
            meta.slots.Add("built", core.BuiltTime.ToString)
        End If
        If Not meta.hasName("program") Then
            meta.slots.Add("program", "GCModeller+R#")
        End If
        If Not meta.hasName("author") Then
            meta.slots.Add("author", My.User.Name)
        End If

        Using writer As New StreamWriter(stream) With {.NewLine = vbLf}
            Call PtfFile.WriteStream(
                annotation:=anno.populates(Of ProteinAnnotation)(env),
                file:=writer,
                attributes:=meta.AsGeneric(Of String())(env, {})
            )
        End Using

        Return True
    End Function

    <ExportAPI("extract.taxonomy")>
    Public Function NCBITaxonomy(<RRawVectorArgument> ptf As Object, Optional env As Environment = Nothing) As Object
        Dim anno As pipeline = pipeline.TryCreatePipeline(Of ProteinAnnotation)(ptf, env)

        If anno.isError Then
            Return anno.getError
        End If

        Return anno.populates(Of ProteinAnnotation)(env).Where(Function(protein) protein.attributes.ContainsKey(""))
    End Function

    <ExportAPI("ptf.split")>
    Public Function split(<RRawVectorArgument> ptf As Object, key$, outputdir$, Optional env As Environment = Nothing) As Object
        Dim anno As pipeline = pipeline.TryCreatePipeline(Of ProteinAnnotation)(ptf, env)

        If anno.isError Then
            Return anno.getError
        End If

        Call anno.populates(Of ProteinAnnotation)(env).SplitAnnotations(key, outputdir)

        Return True
    End Function

    ''' <summary>
    ''' export protein annotations
    ''' </summary>
    ''' <returns></returns>
    <ExportAPI("protein.annotations")>
    Public Function SampleAnnotations(<RRawVectorArgument> ptf As Object, geneIDs$(), Optional env As Environment = Nothing) As Object
        Dim annotations As [Variant](Of Message, PtfFile) = ensurePtfFile(ptf, env)

        If annotations Like GetType(Message) Then
            Return annotations.TryCast(Of Message)
        End If

        Dim output As New List(Of AnnotationTable)
        Dim ptfFile As PtfFile = annotations.TryCast(Of PtfFile)
        Dim proteinIndex = ptfFile.proteins.GroupBy(Function(a) a.geneId).ToDictionary(Function(a) a.Key, Function(a) a.First)

        For Each geneID As String In IDMapping.UnifyIdMapping(ptfFile, geneIDs)
            If proteinIndex.ContainsKey(geneID) Then
                output.Add(AnnotationTable.FromUnifyPtf(proteinIndex(geneID)))
            Else
                output.Add(AnnotationTable.NA(geneID))
            End If
        Next

        Return output.ToArray
    End Function
End Module
