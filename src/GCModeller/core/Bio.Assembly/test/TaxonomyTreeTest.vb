﻿#Region "Microsoft.VisualBasic::cfc78d4a0f0128e76eea71c00315235d, core\Bio.Assembly\Test\TaxonomyTreeTest.vb"

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

    '   Total Lines: 12
    '    Code Lines: 9 (75.00%)
    ' Comment Lines: 0 (0.00%)
    '    - Xml Docs: 0.00%
    ' 
    '   Blank Lines: 3 (25.00%)
    '     File Size: 391 B


    ' Module TaxonomyTreeTest
    ' 
    '     Sub: Main1
    ' 
    ' /********************************************************************************/

#End Region

Imports SMRUCC.genomics.Assembly.NCBI.Taxonomy

Module TaxonomyTreeTest
    Sub Main1()
        Dim tree As New NcbiTaxonomyTree("T:\Resources\NCBI_taxonomy")

        Dim nodes = tree.GetAscendantsWithRanksAndNames(526962, only_std_ranks:=True).BuildBIOM
        Dim lll = tree.GetAscendantsWithRanksAndNames(1051655, only_std_ranks:=True)

        Pause()
    End Sub
End Module
