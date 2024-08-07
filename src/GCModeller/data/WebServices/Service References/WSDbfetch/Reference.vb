﻿#Region "Microsoft.VisualBasic::a631b96f91a399df1bda14c846694966, data\WebServices\Service References\WSDbfetch\Reference.vb"

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

    '     Interface WSDBFetchServer
    ' 
    '         Sub: fetchBatch, fetchBatchAsync, fetchData, fetchDataAsync, getDbFormats
    '              getDbFormatsAsync, getFormatStyles, getFormatStylesAsync, getSupportedDBs, getSupportedDBsAsync
    '              getSupportedFormats, getSupportedFormatsAsync, getSupportedStyles, getSupportedStylesAsync
    ' 
    '     Interface WSDBFetchServerChannel
    ' 
    ' 
    ' 
    '     Class WSDBFetchServerClient
    ' 
    '         Constructor: (+5 Overloads) Sub New
    '         Sub: fetchBatch, fetchBatchAsync, fetchData, fetchDataAsync, getDbFormats
    '              getDbFormatsAsync, getFormatStyles, getFormatStylesAsync, getSupportedDBs, getSupportedDBsAsync
    '              getSupportedFormats, getSupportedFormatsAsync, getSupportedStyles, getSupportedStylesAsync
    ' 
    ' 
    ' /********************************************************************************/

#End Region

'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace WSDbfetch
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://www.ebi.ac.uk/ws/services/WSDbfetch", ConfigurationName:="WSDbfetch.WSDBFetchServer")>  _
    Public Interface WSDBFetchServer
        
        <System.ServiceModel.OperationContractAttribute()>  _
        Sub getDbFormats()
        
        <System.ServiceModel.OperationContractAttribute()>  _
        Sub getDbFormatsAsync()
        
        <System.ServiceModel.OperationContractAttribute()>  _
        Sub fetchData()
        
        <System.ServiceModel.OperationContractAttribute()>  _
        Sub fetchDataAsync()
        
        <System.ServiceModel.OperationContractAttribute()>  _
        Sub fetchBatch()
        
        <System.ServiceModel.OperationContractAttribute()>  _
        Sub fetchBatchAsync()
        
        <System.ServiceModel.OperationContractAttribute()>  _
        Sub getSupportedDBs()
        
        <System.ServiceModel.OperationContractAttribute()>  _
        Sub getSupportedDBsAsync()
        
        <System.ServiceModel.OperationContractAttribute()>  _
        Sub getSupportedFormats()
        
        <System.ServiceModel.OperationContractAttribute()>  _
        Sub getSupportedFormatsAsync()
        
        <System.ServiceModel.OperationContractAttribute()>  _
        Sub getFormatStyles()
        
        <System.ServiceModel.OperationContractAttribute()>  _
        Sub getFormatStylesAsync()
        
        <System.ServiceModel.OperationContractAttribute()>  _
        Sub getSupportedStyles()
        
        <System.ServiceModel.OperationContractAttribute()>  _
        Sub getSupportedStylesAsync()
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface WSDBFetchServerChannel
        Inherits WSDbfetch.WSDBFetchServer, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class WSDBFetchServerClient
        Inherits System.ServiceModel.ClientBase(Of WSDbfetch.WSDBFetchServer)
        Implements WSDbfetch.WSDBFetchServer
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        Public Sub getDbFormats() Implements WSDbfetch.WSDBFetchServer.getDbFormats
            MyBase.Channel.getDbFormats
        End Sub
        
        Public Sub getDbFormatsAsync() Implements WSDbfetch.WSDBFetchServer.getDbFormatsAsync
            MyBase.Channel.getDbFormatsAsync
        End Sub
        
        Public Sub fetchData() Implements WSDbfetch.WSDBFetchServer.fetchData
            MyBase.Channel.fetchData
        End Sub
        
        Public Sub fetchDataAsync() Implements WSDbfetch.WSDBFetchServer.fetchDataAsync
            MyBase.Channel.fetchDataAsync
        End Sub
        
        Public Sub fetchBatch() Implements WSDbfetch.WSDBFetchServer.fetchBatch
            MyBase.Channel.fetchBatch
        End Sub
        
        Public Sub fetchBatchAsync() Implements WSDbfetch.WSDBFetchServer.fetchBatchAsync
            MyBase.Channel.fetchBatchAsync
        End Sub
        
        Public Sub getSupportedDBs() Implements WSDbfetch.WSDBFetchServer.getSupportedDBs
            MyBase.Channel.getSupportedDBs
        End Sub
        
        Public Sub getSupportedDBsAsync() Implements WSDbfetch.WSDBFetchServer.getSupportedDBsAsync
            MyBase.Channel.getSupportedDBsAsync
        End Sub
        
        Public Sub getSupportedFormats() Implements WSDbfetch.WSDBFetchServer.getSupportedFormats
            MyBase.Channel.getSupportedFormats
        End Sub
        
        Public Sub getSupportedFormatsAsync() Implements WSDbfetch.WSDBFetchServer.getSupportedFormatsAsync
            MyBase.Channel.getSupportedFormatsAsync
        End Sub
        
        Public Sub getFormatStyles() Implements WSDbfetch.WSDBFetchServer.getFormatStyles
            MyBase.Channel.getFormatStyles
        End Sub
        
        Public Sub getFormatStylesAsync() Implements WSDbfetch.WSDBFetchServer.getFormatStylesAsync
            MyBase.Channel.getFormatStylesAsync
        End Sub
        
        Public Sub getSupportedStyles() Implements WSDbfetch.WSDBFetchServer.getSupportedStyles
            MyBase.Channel.getSupportedStyles
        End Sub
        
        Public Sub getSupportedStylesAsync() Implements WSDbfetch.WSDBFetchServer.getSupportedStylesAsync
            MyBase.Channel.getSupportedStylesAsync
        End Sub
    End Class
End Namespace
