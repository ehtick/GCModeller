﻿#Region "Microsoft.VisualBasic::520436457114201d207fa4da069ffdc5, core\Bio.Assembly\Test\gbTest.vb"

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

    '   Total Lines: 20
    '    Code Lines: 16 (80.00%)
    ' Comment Lines: 0 (0.00%)
    '    - Xml Docs: 0.00%
    ' 
    '   Blank Lines: 4 (20.00%)
    '     File Size: 760 B


    ' Module gbTest
    ' 
    '     Sub: dbXref2LocationGuid, Main1
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports SMRUCC.genomics.Assembly.NCBI.GenBank

Module gbTest
    Sub Main1()
        Call dbXref2LocationGuid()
    End Sub

    Sub dbXref2LocationGuid()
        Dim gb = GBFF.File.Load("K:\20191112\wildtype\Yersinia_pseudotuberculosis_IP_32953..gbff")
        Dim xrefs As Index(Of String) = "K:\20191112\wildtype\EG\1025.txt".ReadAllLines
        Dim genes = gb.Features.Where(Function(f)
                                          Return f.Query("db_xref") Like xrefs
                                      End Function).Select(Function(g) g.Location.ToString).Distinct.ToArray

        Call genes.SaveTo("K:\20191112\wildtype\EG\1025_EG.txt")

        Pause()
    End Sub
End Module
