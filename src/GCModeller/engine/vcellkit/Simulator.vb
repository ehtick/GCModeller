﻿#Region "Microsoft.VisualBasic::3e5eb120fc734bd733b7cd1b20c41e9e, engine\vcellkit\Simulator.vb"

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

    '   Total Lines: 267
    '    Code Lines: 164 (61.42%)
    ' Comment Lines: 80 (29.96%)
    '    - Xml Docs: 92.50%
    ' 
    '   Blank Lines: 23 (8.61%)
    '     File Size: 11.86 KB


    ' Enum ModuleSystemLevels
    ' 
    '     Metabolome, Proteome, Transcriptome
    ' 
    '  
    ' 
    ' 
    ' 
    ' Module Simulator
    ' 
    '     Constructor: (+1 Overloads) Sub New
    ' 
    '     Function: ApplyModuleProfile, CreateObjectModel, CreateUnifyDefinition, CreateVCellEngine, FluxIndex
    '               GetDefaultDynamics, mass0, MassIndex
    ' 
    '     Sub: TakeStatusSnapshot
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices.Development
Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Language.Default
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports Microsoft.VisualBasic.Serialization.JSON
Imports SMRUCC.genomics.GCModeller.Assembly.GCMarkupLanguage.v2
Imports SMRUCC.genomics.GCModeller.ModellingEngine.BootstrapLoader.Definitions
Imports SMRUCC.genomics.GCModeller.ModellingEngine.BootstrapLoader.Engine
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Dynamics
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Dynamics.Engine
Imports SMRUCC.genomics.GCModeller.ModellingEngine.Model.Cellular
Imports SMRUCC.Rsharp.Runtime.Internal.ConsolePrinter
Imports SMRUCC.Rsharp.Runtime.Interop

''' <summary>
''' data type enumeration of the omics data
''' </summary>
Public Enum ModuleSystemLevels
    Transcriptome
    Proteome
    Metabolome
End Enum

''' <summary>
''' the GCModeller bio-system simulator
''' </summary>
<Package("simulator", Category:=APICategories.ResearchTools)>
Public Module Simulator

    Sub New()
        Call VBDebugger.WaitOutput()
        Call GetType(Engine).Assembly _
            .FromAssembly _
            .DoCall(Sub(assm)
                        CLITools.AppSummary(assm, "Welcome to use SMRUCC/GCModeller virtual cell simulator!", Nothing, App.StdOut)
                    End Sub)
        Call Console.WriteLine()
        Call printer.AttachConsoleFormatter(Of VirtualCell)(AddressOf VirtualCell.Summary)
    End Sub

    ''' <summary>
    ''' Create a new status profile data object with unify mass contents.
    ''' </summary>
    ''' <param name="vcell"></param>
    ''' <param name="mass"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' this function works for the data model which is based on the kegg database model
    ''' </remarks>
    <ExportAPI("kegg_mass")>
    <Extension>
    Public Function CreateUnifyDefinition(vcell As VirtualCell, Optional mass# = 5000) As Definition
        Return vcell.metabolismStructure.compounds _
            .Select(Function(c) c.ID) _
            .DoCall(Function(compounds)
                        Return Definition.KEGG(compounds, initMass:=mass)
                    End Function)
    End Function

    ''' <summary>
    ''' get the initial mass value
    ''' </summary>
    ''' <param name="vcell">
    ''' the initialize mass value has been defined inside this virtual cell model
    ''' </param>
    ''' <returns>
    ''' A mass environment for run vcell model in GCModeller
    ''' </returns>
    <ExportAPI("mass0")>
    Public Function mass0(vcell As VirtualCell) As Definition
        Dim kegg_ref = Definition.KEGG({})
        Dim pool = vcell.metabolismStructure
        Dim dnaseq = kegg_ref.NucleicAcid
        Dim prot = kegg_ref.AminoAcid
        Dim generic = kegg_ref.GenericCompounds
        Dim links = vcell.metabolismStructure.reactions.CompoundLinks

        Return New Definition With {
            .status = pool.compounds _
                .ToDictionary(Function(c) c.ID,
                              Function(c)
                                  Return c.mass0
                              End Function),
            .ADP = pool.GetKEGGMapping(kegg_ref.ADP, NameOf(kegg_ref.ADP), links).ID,
            .ATP = pool.GetKEGGMapping(kegg_ref.ATP, NameOf(kegg_ref.ATP), links).ID,
            .Oxygen = pool.GetKEGGMapping(kegg_ref.Oxygen, NameOf(kegg_ref.Oxygen), links).ID,
            .Water = pool.GetKEGGMapping(kegg_ref.Water, NameOf(kegg_ref.Water), links).ID,
            .NucleicAcid = New NucleicAcid With {
                .A = pool.GetKEGGMapping(dnaseq.A, "dnaseq->A", links).ID,
                .C = pool.GetKEGGMapping(dnaseq.C, "dnaseq->C", links).ID,
                .G = pool.GetKEGGMapping(dnaseq.G, "dnaseq->G", links).ID,
                .U = pool.GetKEGGMapping(dnaseq.U, "dnaseq->U", links).ID
            },
            .AminoAcid = New AminoAcid With {
                .A = pool.GetKEGGMapping(prot.A, "prot->A", links).ID,
                .U = pool.GetKEGGMapping(prot.U, "prot->U", links).ID,
                .G = pool.GetKEGGMapping(prot.G, "prot->G", links).ID,
                .C = pool.GetKEGGMapping(prot.C, "prot->C", links).ID,
                .D = pool.GetKEGGMapping(prot.D, "prot->D", links).ID,
                .E = pool.GetKEGGMapping(prot.E, "prot->E", links).ID,
                .F = pool.GetKEGGMapping(prot.F, "prot->F", links).ID,
                .H = pool.GetKEGGMapping(prot.H, "prot->H", links).ID,
                .I = pool.GetKEGGMapping(prot.I, "prot->I", links).ID,
                .K = pool.GetKEGGMapping(prot.K, "prot->K", links).ID,
                .L = pool.GetKEGGMapping(prot.L, "prot->L", links).ID,
                .M = pool.GetKEGGMapping(prot.M, "prot->M", links).ID,
                .N = pool.GetKEGGMapping(prot.N, "prot->N", links).ID,
                .O = pool.GetKEGGMapping(prot.O, "prot->O", links).ID,
                .P = pool.GetKEGGMapping(prot.P, "prot->P", links).ID,
                .Q = pool.GetKEGGMapping(prot.Q, "prot->Q", links).ID,
                .R = pool.GetKEGGMapping(prot.R, "prot->R", links).ID,
                .S = pool.GetKEGGMapping(prot.S, "prot->S", links).ID,
                .T = pool.GetKEGGMapping(prot.T, "prot->T", links).ID,
                .V = pool.GetKEGGMapping(prot.V, "prot->V", links).ID,
                .W = pool.GetKEGGMapping(prot.W, "prot->W", links).ID,
                .Y = pool.GetKEGGMapping(prot.Y, "prot->Y", links).ID
            },
            .GenericCompounds = New Dictionary(Of String, GeneralCompound)
        }
    End Function

    ''' <summary>
    ''' create a generic vcell object model from a loaded vcell xml file model
    ''' </summary>
    ''' <param name="vcell">the file model data of the GCModeller vcell</param>
    ''' <returns></returns>
    <ExportAPI("vcell.model")>
    Public Function CreateObjectModel(vcell As VirtualCell) As CellularModule
        Return vcell.CreateModel
    End Function

    ''' <summary>
    ''' get mass key reference index collection
    ''' </summary>
    ''' <param name="vcell"></param>
    ''' <returns></returns>
    <ExportAPI("vcell.mass.index")>
    Public Function MassIndex(vcell As CellularModule) As OmicsTuple(Of String())
        Return vcell.DoCall(AddressOf OmicsDataAdapter.GetMassTuples)
    End Function

    ''' <summary>
    ''' get flux key reference index collection
    ''' </summary>
    ''' <param name="vcell"></param>
    ''' <returns></returns>
    <ExportAPI("vcell.flux.index")>
    Public Function FluxIndex(vcell As CellularModule) As OmicsTuple(Of String())
        Return vcell.DoCall(AddressOf OmicsDataAdapter.GetFluxTuples)
    End Function

    ''' <summary>
    ''' create a new virtual cell engine
    ''' </summary>
    ''' <param name="inits">
    ''' the initial mass environment definition
    ''' </param>
    ''' <param name="vcell">The virtual cell object model, contains the definition of the cellular network graph data</param>
    ''' <param name="iterations">
    ''' the number of the iteration loops for run the simulation
    ''' </param>
    ''' <param name="time_resolutions">
    ''' the time steps
    ''' </param>
    ''' <param name="deletions">make a specific gene nodes deletions</param>
    ''' <param name="dynamics"></param>
    ''' <returns></returns>
    <ExportAPI("engine.load")>
    <RApiReturn(GetType(Engine))>
    Public Function CreateVCellEngine(vcell As CellularModule,
                                      Optional inits As Definition = Nothing,
                                      Optional iterations% = 100,
                                      Optional time_resolutions% = 10000,
                                      Optional deletions$() = Nothing,
                                      Optional dynamics As FluxBaseline = Nothing,
                                      Optional showProgress As Boolean = True,
                                      Optional debug As Boolean = False) As Object

        Static defaultDynamics As [Default](Of FluxBaseline) = New FluxBaseline
        ' do initialize of the virtual cell engine
        ' and then load virtual cell model into 
        ' engine kernel
        Return New Engine(
            def:=inits,
            dynamics:=dynamics Or defaultDynamics,
            iterations:=iterations,
            showProgress:=showProgress,
            timeResolution:=time_resolutions,
            debug:=debug
        ) _
        .LoadModel(vcell, deletions)
    End Function

    ''' <summary>
    ''' Create the default cell dynamics parameters
    ''' </summary>
    ''' <returns></returns>
    <ExportAPI("dynamics.default")>
    Public Function GetDefaultDynamics() As FluxBaseline
        Return New FluxBaseline
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="engine"></param>
    ''' <param name="profile"></param>
    ''' <param name="system">
    ''' the omics data type
    ''' </param>
    ''' <returns></returns>
    <ExportAPI("apply.module_profile")>
    Public Function ApplyModuleProfile(engine As Engine,
                                       profile As Dictionary(Of String, Double),
                                       Optional system As ModuleSystemLevels = ModuleSystemLevels.Transcriptome) As Engine

        If engine Is Nothing OrElse profile.IsNullOrEmpty Then
            Return engine
        End If

        Dim status As Definition = engine.initials

        Select Case system
            Case ModuleSystemLevels.Transcriptome

            Case ModuleSystemLevels.Proteome

            Case ModuleSystemLevels.Metabolome
                For Each compound In profile
                    status.status(compound.Key) = compound.Value
                Next

            Case Else
                Return engine
        End Select

        Return engine
    End Function

    ''' <summary>
    ''' make a snapshot of the mass and flux data
    ''' </summary>
    ''' <param name="engine"></param>
    ''' <param name="massIndex"></param>
    ''' <param name="fluxIndex"></param>
    ''' <param name="save$"></param>
    <ExportAPI("vcell.snapshot")>
    <Extension>
    Public Sub TakeStatusSnapshot(engine As Engine, massIndex As OmicsTuple(Of String()), fluxIndex As OmicsTuple(Of String()), save$)
        Dim massSnapshot = DirectCast(engine.dataStorageDriver, FinalSnapshotDriver).mass
        Dim fluxSnapshot = DirectCast(engine.dataStorageDriver, FinalSnapshotDriver).flux

        ' rRNA, tRNA会在这产生重复
        ' 所以在这里会需要进行一次去重操作
        Call massSnapshot.Subset(massIndex.transcriptome.Distinct.ToArray, ignoreMissing:=True).GetJson.SaveTo($"{save}/mass/transcriptome.json")
        Call massSnapshot.Subset(massIndex.proteome, ignoreMissing:=True).GetJson.SaveTo($"{save}/mass/proteome.json")
        Call massSnapshot.Subset(massIndex.metabolome, ignoreMissing:=True).GetJson.SaveTo($"{save}/mass/metabolome.json")

        Call fluxSnapshot.Subset(fluxIndex.transcriptome, ignoreMissing:=True).GetJson.SaveTo($"{save}/flux/transcriptome.json")
        Call fluxSnapshot.Subset(fluxIndex.proteome, ignoreMissing:=True).GetJson.SaveTo($"{save}/flux/proteome.json")
        Call fluxSnapshot.Subset(fluxIndex.metabolome, ignoreMissing:=True).GetJson.SaveTo($"{save}/flux/metabolome.json")
    End Sub
End Module
