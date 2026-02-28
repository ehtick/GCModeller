Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Math
Imports Microsoft.VisualBasic.Scripting.Expressions
Imports SMRUCC.genomics.SequenceModel.FQ

''' <summary>
''' 用于存储单个序列统计指标的结构体
''' </summary>
Public Structure ReadStats
    Public Property Length As Integer
    Public Property MeanQuality As Double

    Public Sub New(len As Integer, qual As Double)
        Me.Length = len
        Me.MeanQuality = qual
    End Sub
End Structure

''' <summary>
''' 汇总统计结果模型
''' </summary>
Public Class NanoSummary
    Public Property TotalReads As Long
    Public Property TotalBases As Long
    Public Property MeanLength As Double
    Public Property MedianLength As Double
    Public Property N50 As Long
    Public Property MinLength As Long
    Public Property MaxLength As Long

    Public Property MeanQuality As Double
    Public Property MedianQuality As Double
    Public Property MinQuality As Double
    Public Property MaxQuality As Double

    ' 可以根据需要添加 Q20, Q7 等比例统计
End Class

''' <summary>
''' 直方图分箱数据，用于绘图
''' </summary>
Public Class HistogramBin
    Public Property Label As String
    Public Property Count As Integer
    Public Property Start As Double
    Public Property [End] As Double
End Class

''' <summary>
''' 散点图数据点
''' </summary>
Public Class ScatterPoint
    Public Property X As Double
    Public Property Y As Double
End Class

''' <summary>
''' 模拟 NanoPlot 的统计计算模块
''' </summary>
Public Module NanoPlotEngine

    ''' <summary>
    ''' 主入口：计算所有统计数据
    ''' </summary>
    ''' <param name="reads">解析后的 FastQ 集合</param>
    ''' <returns>包含汇总统计和绘图数据的对象</returns>
    Public Function CalculateNanoPlotData(reads As List(Of FastQ)) As (Summary As NanoSummary, LengthHist As List(Of HistogramBin), QualHist As List(Of HistogramBin), ScatterData As List(Of ScatterPoint))
        ' 1. 提取基础数据 (长度和质量)
        ' 优化：使用并行处理加速大数据集计算
        Dim rawData As List(Of ReadStats) = reads.AsParallel().Select(Function(r) New ReadStats(r.Length, CalculateMeanQuality(r))).AsList()

        If rawData.Count = 0 Then
            Return Nothing
        End If

        ' 2. 计算汇总统计
        Dim summary As NanoSummary = CalculateSummary(rawData)

        ' 3. 生成绘图数据
        ' 长度直方图 (通常使用对数坐标或较大的分箱)
        Dim lenHist = CreateHistogram(rawData.Select(Function(r) CDbl(r.Length)), 50, doLog:=True)

        ' 质量直方图
        Dim qualHist = CreateHistogram(rawData.Select(Function(r) r.MeanQuality), 50, doLog:=False)

        ' 长度 vs 质量 散点图 (进行降采样以提高前端渲染性能)
        Dim scatter = CreateScatterData(rawData, sampleSize:=10000)

        Return (summary, lenHist, qualHist, scatter)
    End Function

    ''' <summary>
    ''' 计算一条序列的平均质量
    ''' </summary>
    Private Function CalculateMeanQuality(read As FastQ) As Double
        If String.IsNullOrEmpty(read.Quality) OrElse read.Quality.Length = 0 Then Return 0.0

        Dim q = FastQ.GetQualityOrder(read.Quality).ToArray
        Return q.Average
    End Function

    ''' <summary>
    ''' 计算汇总指标，包括 N50
    ''' </summary>
    Private Function CalculateSummary(data As List(Of ReadStats)) As NanoSummary
        Dim summary As New NanoSummary With {
            .TotalReads = data.Count,
            .TotalBases = data.Sum(Function(r) r.Length),
            .MinLength = data.Min(Function(r) r.Length),
            .MaxLength = data.Max(Function(r) r.Length),
            .MinQuality = data.Min(Function(r) r.MeanQuality),
            .MaxQuality = data.Max(Function(r) r.MeanQuality),
            .MeanLength = .TotalBases / .TotalReads  ' 计算平均长度
        }


        ' 计算中位数 (注意：大数据集排序可能较慢)
        Dim sortedLengths = data.Select(Function(r) r.Length).OrderBy(Function(x) x).ToList()
        summary.MedianLength = Median(sortedLengths)

        Dim sortedQuals = data.Select(Function(r) r.MeanQuality).OrderBy(Function(x) x).ToList()
        summary.MedianQuality = Median(sortedQuals)

        ' 计算平均质量
        summary.MeanQuality = data.Average(Function(r) r.MeanQuality)

        ' 计算 N50
        summary.N50 = CalculateN50(sortedLengths, summary.TotalBases)

        Return summary
    End Function

    Private Function Median(sortedData As IList(Of Double)) As Double
        If sortedData.Count Mod 2 = 0 Then
            Return (sortedData(sortedData.Count \ 2 - 1) + sortedData(sortedData.Count \ 2)) / 2.0
        Else
            Return sortedData(sortedData.Count \ 2)
        End If
    End Function

    Private Function Median(sortedData As IList(Of Integer)) As Double
        If sortedData.Count Mod 2 = 0 Then
            Return (sortedData(sortedData.Count \ 2 - 1) + sortedData(sortedData.Count \ 2)) / 2.0
        Else
            Return sortedData(sortedData.Count \ 2)
        End If
    End Function

    ''' <summary>
    ''' 计算 N50 值
    ''' </summary>
    Private Function CalculateN50(sortedLengths As List(Of Integer), totalBases As Long) As Long
        Dim halfTotal As Long = totalBases \ 2
        Dim runningSum As Long = 0

        ' sortedLengths 应该已经是升序排列，但N50通常是从大到小累加
        ' 为了性能，我们从后向前遍历（相当于降序）
        For i As Integer = sortedLengths.Count - 1 To 0 Step -1
            runningSum += sortedLengths(i)
            If runningSum >= halfTotal Then
                Return sortedLengths(i)
            End If
        Next
        Return 0
    End Function

    ''' <summary>
    ''' 生成分箱数据用于直方图展示
    ''' </summary>
    Private Function CreateHistogram(values As IEnumerable(Of Double), bins As Integer, doLog As Boolean) As List(Of HistogramBin)
        Dim list = values.ToList()
        If list.Count = 0 Then Return New List(Of HistogramBin)()

        ' 如果是对数分箱，先对数据取 Log10
        Dim processedValues = If(doLog,
            list.Where(Function(v) v > 0).Select(Function(v) Math.Log10(v)).ToList(),
            list)

        If processedValues.Count = 0 Then Return New List(Of HistogramBin)()

        Dim minVal = processedValues.Min()
        Dim maxVal = processedValues.Max()
        Dim stepSize = (maxVal - minVal) / bins

        ' 防止所有值相同导致除零
        If stepSize = 0 Then stepSize = 1

        Dim histogram = New Dictionary(Of Integer, Integer)()

        ' 初始化桶
        For i = 0 To bins - 1
            histogram(i) = 0
        Next

        ' 数据分箱
        For Each Val As Double In processedValues
            Dim index As Integer = CInt(Math.Floor((Val - minVal) / stepSize))
            If index >= bins Then index = bins - 1 ' 处理最大值边界情况
            If index < 0 Then index = 0
            histogram(index) += 1
        Next

        ' 转换为绘图用的 Bin 列表
        Dim result As New List(Of HistogramBin)()
        For i = 0 To bins - 1
            Dim startVal = minVal + i * stepSize
            Dim endVal = startVal + stepSize

            ' 如果之前做了 Log 转换，这里还原回去作为 Label
            Dim label As String
            If doLog Then
                label = $"{Math.Pow(10, startVal):F0} - {Math.Pow(10, endVal):F0}"
            Else
                label = $"{startVal:F1} - {endVal:F1}"
            End If

            result.Add(New HistogramBin With {
                .Start = startVal,
                .End = endVal,
                .Count = histogram(i),
                .Label = label
            })
        Next

        Return result
    End Function

    ''' <summary>
    ''' 生成散点图数据，包含降采样逻辑
    ''' </summary>
    Private Function CreateScatterData(data As List(Of ReadStats), sampleSize As Integer) As List(Of ScatterPoint)
        ' 如果数据量小于采样数，直接返回全部
        If data.Count <= sampleSize Then
            Return data.Select(Function(r) New ScatterPoint With {.X = r.Length, .Y = r.MeanQuality}).ToList()
        End If

        ' 简单的随机降采样
        Dim rand As New Random()
        Dim result As New List(Of ScatterPoint)(sampleSize)

        ' 使用蓄水池抽样算法 或者简单的随机索引
        Dim indices As New HashSet(Of Integer)()
        While indices.Count < sampleSize
            indices.Add(rand.Next(0, data.Count))
        End While

        For Each idx In indices
            result.Add(New ScatterPoint With {
                .X = data(idx).Length,
                .Y = data(idx).MeanQuality
            })
        Next

        Return result
    End Function

End Module