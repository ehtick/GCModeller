Imports Microsoft.VisualBasic.Imaging.LayoutModel

Namespace FQ.NanoPlot

    Public Class NanoPlotResult

        Public Property Summary As NanoSummary
        Public Property LengthHist As HistogramBin()
        Public Property QualHist As HistogramBin()
        Public Property ScatterData As Point2D()

    End Class

End Namespace