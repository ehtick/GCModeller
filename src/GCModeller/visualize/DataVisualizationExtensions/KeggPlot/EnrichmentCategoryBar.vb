﻿Imports Microsoft.VisualBasic.Data.ChartPlots.Graphic
Imports Microsoft.VisualBasic.Data.ChartPlots.Graphic.Canvas

Public Class ClassEnrichmentCategoryBar : Inherits Plot

    Public Sub New(theme As Theme)
        MyBase.New(theme)
    End Sub

    Protected Overrides Sub PlotInternal(ByRef g As Imaging.IGraphics, canvas As Imaging.Drawing2D.GraphicsRegion)
        Throw New NotImplementedException()
    End Sub
End Class