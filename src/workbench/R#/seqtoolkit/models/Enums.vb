﻿#Region "Microsoft.VisualBasic::42698506b0f6fd5081f8fd3954d68955, R#\seqtoolkit\models\Enums.vb"

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

    '   Total Lines: 18
    '    Code Lines: 13 (72.22%)
    ' Comment Lines: 3 (16.67%)
    '    - Xml Docs: 100.00%
    ' 
    '   Blank Lines: 2 (11.11%)
    '     File Size: 306 B


    ' Enum TableTypes
    ' 
    '     BBH, Mapping, SBH
    ' 
    '  
    ' 
    ' 
    ' 
    ' Enum BBHAlgorithm
    ' 
    '     BHR, HybridBHR, Naive, TaxonomySupports
    ' 
    '  
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.ComponentModel

Public Enum TableTypes
    SBH
    BBH
    ''' <summary>
    ''' blastn mapping of the short reads
    ''' </summary>
    Mapping
End Enum

Public Enum BBHAlgorithm
    Naive
    BHR
    <Description("Hybrid-BHR")>
    HybridBHR
    TaxonomySupports
End Enum
