﻿#Region "Microsoft.VisualBasic::ac3864dad1ac9d0174d3fad541777370, core\Bio.Assembly\Assembly\Expasy\csv\Enzyme.vb"

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

    '   Total Lines: 30
    '    Code Lines: 25 (83.33%)
    ' Comment Lines: 0 (0.00%)
    '    - Xml Docs: 0.00%
    ' 
    '   Blank Lines: 5 (16.67%)
    '     File Size: 1.12 KB


    '     Class Enzyme
    ' 
    '         Properties: AlternateName, Cofactor, Comments, Description, Identification
    '                     PROSITE
    ' 
    '         Function: CreateObject, ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.SchemaMaps

Namespace Assembly.Expasy.Database.csv

    Public Class Enzyme

        <Column(Name:="id")> Public Property Identification As String
        Public Property Description As String
        Public Property AlternateName As String()
        Public Property Cofactor As String()
        Public Property PROSITE As String()
        Public Property Comments As String

        Public Overrides Function ToString() As String
            Return Identification
        End Function

        Public Shared Function CreateObject(EnzymeData As Database.Enzyme) As Enzyme
            Dim EnzymeObject As New Enzyme With {
                .AlternateName = EnzymeData.AlternateName,
                .Cofactor = EnzymeData.Cofactor,
                .Comments = EnzymeData.Comments,
                .Description = EnzymeData.Description,
                .Identification = EnzymeData.Identification,
                .PROSITE = EnzymeData.PROSITE
            }
            Return EnzymeObject
        End Function
    End Class
End Namespace
