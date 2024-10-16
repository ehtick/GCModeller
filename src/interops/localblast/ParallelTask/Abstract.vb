﻿#Region "Microsoft.VisualBasic::c91600aa5e071405b882807f7994ba43, localblast\ParallelTask\Abstract.vb"

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

    '   Total Lines: 10
    '    Code Lines: 1 (10.00%)
    ' Comment Lines: 9 (90.00%)
    '    - Xml Docs: 100.00%
    ' 
    '   Blank Lines: 0 (0.00%)
    '     File Size: 525 B


    ' Delegate Function
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

''' <summary>
''' The formatdb and blast operation should be include in this function pointer.(在这个句柄之中必须要包含有formatdb和blast这两个步骤)
''' </summary>
''' <param name="Query"></param>
''' <param name="Subject"></param>
''' <param name="Evalue"></param>
''' <param name="Export"></param>
''' <returns>返回blast的日志文件名</returns>
''' <remarks></remarks>
Public Delegate Function BlastInvoker(query$, subject$, num_threads%, evalue$, EXPORT$, [overrides] As Boolean) As String
