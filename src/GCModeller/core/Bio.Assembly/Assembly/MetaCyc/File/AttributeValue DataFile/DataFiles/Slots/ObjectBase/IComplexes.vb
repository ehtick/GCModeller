﻿#Region "Microsoft.VisualBasic::ad0999980e58682b251f8023ce8b66e1, core\Bio.Assembly\Assembly\MetaCyc\File\AttributeValue DataFile\DataFiles\Slots\ObjectBase\IComplexes.vb"

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

    '   Total Lines: 19
    '    Code Lines: 7 (36.84%)
    ' Comment Lines: 10 (52.63%)
    '    - Xml Docs: 100.00%
    ' 
    '   Blank Lines: 2 (10.53%)
    '     File Size: 600 B


    '     Interface IComplexes
    ' 
    '         Properties: Components
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Language

Namespace Assembly.MetaCyc.File.DataFiles.Slots

    ''' <summary>
    ''' 复合物对象的接口
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IComplexes
        ''' <summary>
        ''' The components module of this regulator entity.(构成本复合物对象的组件模块的UniqueId列表)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property Components As List(Of String)
    End Interface
End Namespace
