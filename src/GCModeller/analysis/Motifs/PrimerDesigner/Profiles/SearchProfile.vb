﻿#Region "Microsoft.VisualBasic::5867b606bed98762fb2fc571fe719673, analysis\Motifs\PrimerDesigner\Profiles\SearchProfile.vb"

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
    '    Code Lines: 12 (100.00%)
    ' Comment Lines: 0 (0.00%)
    '    - Xml Docs: 0.00%
    ' 
    '   Blank Lines: 0 (0.00%)
    '     File Size: 445 B


    ' Class SearchProfile
    ' 
    '     Properties: AntisenseRestricted, DeltaGC, DeltaTm, MaxGC, MaxLength
    '                 MaxTm, MinGC, MinLength, MinTm, SenseRestricted
    ' 
    ' /********************************************************************************/

#End Region

Public Class SearchProfile
    Public Property MinLength As Integer
    Public Property MaxLength As Integer
    Public Property SenseRestricted As String
    Public Property AntisenseRestricted As String
    Public Property MinGC As Double
    Public Property MaxGC As Double
    Public Property MinTm As Double
    Public Property MaxTm As Double
    Public Property DeltaTm As Double
    Public Property DeltaGC As Double
End Class
