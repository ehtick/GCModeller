﻿#Region "Microsoft.VisualBasic::63069cb4570b40601c515b003a500b10, engine\Dynamics\Core\Kinetics\Controls\BaselineControls.vb"

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
    '    Code Lines: 14 (77.78%)
    ' Comment Lines: 0 (0.00%)
    '    - Xml Docs: 0.00%
    ' 
    '   Blank Lines: 4 (22.22%)
    '     File Size: 392 B


    '     Class BaselineControls
    ' 
    '         Properties: coefficient
    ' 
    '         Constructor: (+2 Overloads) Sub New
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace Core

    Public Class BaselineControls : Inherits Controls

        Public Overrides ReadOnly Property coefficient As Double
            Get
                Return baseline
            End Get
        End Property

        Sub New(baseline As Double)
            Me.baseline = baseline
        End Sub

        Sub New()
        End Sub

        Public Overrides Function ToString() As String
            Return $"[baseline] {baseline}"
        End Function
    End Class
End Namespace
