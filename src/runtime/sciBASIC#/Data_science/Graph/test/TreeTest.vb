﻿#Region "Microsoft.VisualBasic::1e8821b842bffa8b00636e1bb08d46db, Data_science\Graph\test\TreeTest.vb"

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

    '   Total Lines: 70
    '    Code Lines: 0 (0.00%)
    ' Comment Lines: 53 (75.71%)
    '    - Xml Docs: 0.00%
    ' 
    '   Blank Lines: 17 (24.29%)
    '     File Size: 2.01 KB


    ' 
    ' /********************************************************************************/

#End Region

'#Region "Microsoft.VisualBasic::763644d6b47d19b79abfdddadb9041d2, Data_science\Graph\test\TreeTest.vb"

'    ' Author:
'    ' 
'    '       asuka (amethyst.asuka@gcmodeller.org)
'    '       xie (genetics@smrucc.org)
'    '       xieguigang (xie.guigang@live.com)
'    ' 
'    ' Copyright (c) 2018 GPL3 Licensed
'    ' 
'    ' 
'    ' GNU GENERAL PUBLIC LICENSE (GPL3)
'    ' 
'    ' 
'    ' This program is free software: you can redistribute it and/or modify
'    ' it under the terms of the GNU General Public License as published by
'    ' the Free Software Foundation, either version 3 of the License, or
'    ' (at your option) any later version.
'    ' 
'    ' This program is distributed in the hope that it will be useful,
'    ' but WITHOUT ANY WARRANTY; without even the implied warranty of
'    ' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    ' GNU General Public License for more details.
'    ' 
'    ' You should have received a copy of the GNU General Public License
'    ' along with this program. If not, see <http://www.gnu.org/licenses/>.



'    ' /********************************************************************************/

'    ' Summaries:


'    ' Code Statistics:

'    '   Total Lines: 17
'    '    Code Lines: 12 (70.59%)
'    ' Comment Lines: 0 (0.00%)
'    '    - Xml Docs: 0.00%
'    ' 
'    '   Blank Lines: 5 (29.41%)
'    '     File Size: 368 B


'    ' Module TreeTest
'    ' 
'    '     Sub: Main
'    ' 
'    ' /********************************************************************************/

'#End Region

'Imports Microsoft.VisualBasic.Data.Graph

'Module TreeTest

'    Sub Main()
'        Dim tree As BinaryTree(Of String) = BinaryTree(Of String).ROOT
'        Dim rand As New Random

'        For i As Integer = 10 To 100
'            tree.Insert(i, rand.Next(10, 10000000))
'        Next

'        Dim g = tree.CreateGraph

'        Pause()
'    End Sub
'End Module
