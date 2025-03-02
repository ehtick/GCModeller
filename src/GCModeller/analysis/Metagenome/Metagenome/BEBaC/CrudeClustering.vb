﻿#Region "Microsoft.VisualBasic::c8f71f194ec8b7f1281ccbbc98b7c9ac, analysis\Metagenome\Metagenome\BEBaC\CrudeClustering.vb"

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

    '   Total Lines: 61
    '    Code Lines: 11 (18.03%)
    ' Comment Lines: 45 (73.77%)
    '    - Xml Docs: 97.78%
    ' 
    '   Blank Lines: 5 (8.20%)
    '     File Size: 3.00 KB


    '     Module CrudeClustering
    ' 
    '         Function: InitializePartitions, StochasticSearch
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices

Namespace BEBaC

    Public Module CrudeClustering

        ''' <summary>
        ''' **Initialization**
        ''' 
        ''' Cluster ``y(N)`` into ``Kmax`` clusters Using complete linkage algorithm
        ''' </summary>
        ''' <param name="s"></param>
        ''' <param name="kmax"></param>
        ''' <returns></returns>
        <Extension>
        Public Iterator Function InitializePartitions(s As IEnumerable(Of I3merVector), kmax As Integer) As IEnumerable(Of Cluster)

        End Function

        ''' <summary>
        ''' Apply each of the four search operators described below
        ''' to the the current partition S in a random order. Then, if
        ''' the resulting partition leads To a higher marginal likelihood, 
        ''' update the current partition S, otherwise keep
        ''' the current partition. If all operators fail To update the
        ''' current partition, then Stop And Set the best partition S'
        ''' as the current partition S.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' + In a random order relocate all vectors in a pregroup
        '''   to another cluster that leads to the maximal increase
        '''   in the marginal likelihood. The option of moving vectors 
        '''   into an empty cluster is also considered, unless the 
        '''   total number Of clusters exceeds Kmax.
        ''' + In a random order, merge the two clusters which leads 
        '''   to the maximum increase in the marginal likelihood. 
        '''   This operator considers also merging of singleton clusters
        '''   (only one pregroup in the cluster) that might be generated 
        '''   by the other operators.
        ''' + In a random order, split each cluster into two subclusters 
        '''   using complete linkage clustering algorithm, where the 
        '''   distance between two pregroups are calculated As the average 
        '''   linear correlation coefficient between vectors In the two 
        '''   pregroups. Then Try reassigning Each subcluster To another 
        '''   cluster including empty clusters. Choose the split And 
        '''   reassignment that leads To the maximal increase In the 
        '''   marginal likelihood(5).
        ''' + In a random order, split each cluster into m subclusters 
        '''   using complete linkage clustering algorithm as described 
        '''   in operator (iii), where m=min(20, nPregroup/5) And 
        '''   nPregroup Is the total number Of pregroups In the cluster. 
        '''   Then Try to reassign each subcluster to another cluster; 
        '''   choose the split And reassignment that leads To the maximal 
        '''   increase In the marginal likelihood.
        ''' </remarks>
        Public Function StochasticSearch()
            Throw New NotImplementedException
        End Function
    End Module
End Namespace
